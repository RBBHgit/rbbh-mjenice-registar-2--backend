using System.Linq.Dynamic.Core;
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Interfaces.Repositories;
using DataAccess.Responses;
using Microsoft.AspNetCore.Authorization;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.SearchObjects;
using Raiffeisen.RegistarMjenica.Api.Services.Exceptions;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;
using Raiffeisen.RegistarMjenica.Api.Services.Mappers;

namespace Raiffeisen.RegistarMjenica.Api.Services.Services;

public class MjenicaService : IMjenicaService
{
    private readonly IRfLogger _logger;
    private readonly IUnitOfWork _unitOfWorkTest;

    public MjenicaService(IMjenicaRepository mjenicaRepository, IUnitOfWork unitOfWorkTest,
        IMjenicaExemptionHistoryRepository mjenicaExemptionHistoryRepository,
        IRBBHApiService rBBHApiService, IRfLogger logger)
    {
        _mjenicaRepository = mjenicaRepository;
        _unitOfWorkTest = unitOfWorkTest;
        _rBBHApiService = rBBHApiService;
        _logger = logger;
    }

    private IMjenicaRepository _mjenicaRepository { get; }
    private IRBBHApiService _rBBHApiService { get; }

    [Authorize(Roles = "probis")]
    public async Task<MjenicaModel> CreateAsync(MjenicaModel model, string loggedInUser)
    {
        try
        {
            model.CreatedBy = loggedInUser;
            model.CreatedDate = DateTime.Now;

            var entity = MjenicaMapper.MapToEntityCreate(model);

            await _mjenicaRepository.AddAsync(entity);

            return MjenicaMapper.MapFromEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error creating bill of exchange for contract {model.ContractNumber}.");
            throw;
        }
    }

    [Authorize(Roles = "probis")]
    public async Task<MjenicaModel> UpdateAsync(MjenicaModel model, string username)
    {
        try
        {
            if (model.IsCreatedVerified) throw new Exception("Verified bill of exchange can not be updated.");

            if (string.IsNullOrEmpty(username)) throw new Exception("Unable to extract username from token.");
            ;

            model.CreatedBy = username;
            model.CreatedDate = DateTime.Now;

            var entity = MjenicaMapper.MapToEntityUpdate(model);

            await _mjenicaRepository.UpdateAsync(entity);

            return MjenicaMapper.MapFromEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating bill of exchange {model.Id}.");
            throw;
        }
    }

    public async Task<MjenicaModel> GetByIdAsync(int id, bool asNoTracking = default, bool includeHistory = default)
    {
        try
        {
            Mjenica? mjenica = null;

            if (includeHistory)
                mjenica = await _mjenicaRepository.GetByIdAsync(id, asNoTracking, x => x.MjenicaExemptionHistory);
            else
                mjenica = await _mjenicaRepository.GetByIdAsync(id, asNoTracking);

            var model = MjenicaMapper.MapFromEntity(mjenica);

            model.HasHistory = mjenica.MjenicaExemptionHistory == null || !mjenica.MjenicaExemptionHistory.Any()
                ? null
                : true;

            return model;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting bill of exchange {id}.");
            throw;
        }
    }

    public async Task<MjenicaModel> VerifyMjenicaAsync(MjenicaModel model, string username)
    {
        try
        {
            if (username == model.CreatedBy)
                throw new Exception("Bill of exchange can not be verified by it's creator.");

            model.IsCreatedVerified = true;
            model.CreationVerifiedBy = username;
            model.CreationVerifiedDate = DateTime.Now;

            var entity = MjenicaMapper.MapToEntityUpdate(model);

            await _mjenicaRepository.UpdateAsync(entity);

            return MjenicaMapper.MapFromEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error verifying bill of exchange {model.Id}");
            throw;
        }
    }

    public async Task<PagedResponse<MjenicaGridModel>> GetGridAsync(MjenicaSearchObject mjenicaSearch = default)
    {
        try
        {
            var result = await _mjenicaRepository.Get(mjenicaSearch);

            if (result == null || !result.Data.Any())
            {
                return new PagedResponse<MjenicaGridModel>
                {
                    Data = new List<MjenicaGridModel>(),
                    TotalCount = 0
                };
            }

            var mappedResult = MjenicaMapper.MapFromEntityList(result.Data);

            return new PagedResponse<MjenicaGridModel>
            {
                Data = mappedResult,
                TotalCount = result.TotalCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting bill of exchange grid.");
            throw;
        }
    }


    public async Task<MjenicaModel> ExemptMjenicaTransactionAsync(MjenicaModel model, string username)
    {
        try
        {
            var entity = await _mjenicaRepository.GetByIdAsync(model.Id, false);

            if (!(entity.IsCreatedVerified.HasValue && entity.IsCreatedVerified.Value))
                throw new Exception("Unverified bill of exchange cannot be exempted.");

            var isExamptedVerified = entity.IsExemptedVerified.HasValue && entity.IsExemptedVerified.Value;

            if (entity.IsExempted.HasValue && entity.IsExempted.Value && isExamptedVerified)
            {
                var history = CreateMjenicaHistory(entity);

                ExempteMjenica(model, username);
                var mjenica = MjenicaMapper.MapToEntityUpdate(model);

                try
                {
                    var mjenicaRepo = _unitOfWorkTest.Repository<Mjenica, MjenicaSearchObject>();
                    var historyRepo = _unitOfWorkTest.Repository<MjenicaExemptionHistory, MjenicaHistorySearchObject>();

                    await mjenicaRepo.UpdateTransactionAsync(mjenica);
                    await historyRepo.AddTransactionAsync(history);

                    await _unitOfWorkTest.SaveAsync(CancellationToken.None);
                    return MjenicaMapper.MapFromEntity(mjenica);
                }
                catch (Exception ex)
                {
                    _unitOfWorkTest.Rollback();
                    _unitOfWorkTest.Dispose();
                    throw;
                }
            }

            ExempteMjenica(model, username);

            var mappedEntity = MjenicaMapper.MapToEntityUpdate(model);

            await _mjenicaRepository.UpdateAsync(mappedEntity);
            return MjenicaMapper.MapFromEntity(mappedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error exempting bill of exchange {model.Id}.");
            throw;
        }
    }

    public async Task<MjenicaModel> VerifyExemptionMjenicaAsync(MjenicaModel model, string username)
    {
        try
        {
            if (username == model.ExemptedBy) throw new Exception("Exemption can not be verified by it's creator.");

            model.IsExemptedVerified = true;
            model.ExemptionVerifiedBy = username;
            model.ExemptionVerifiedDate = DateTime.Now;
            if (model.ExemptionReason == ExemptionReason.TransferredToAnotherContract)
                await TransferMjenicaToContract(model);

            var entity = MjenicaMapper.MapToEntityUpdate(model);

            await _mjenicaRepository.UpdateAsync(entity);

            return MjenicaMapper.MapFromEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error verifying exemption for bill of exchange {model.Id}.");
            throw;
        }
    }

    public async Task<List<MjenicaCard>> GetClientMjeniceAsync(string customerId)
    {
        try
        {
            var client = await _rBBHApiService.GetClientInfo(customerId);
            var agrements = await _rBBHApiService.GetClientAgreements(customerId);

            return agrements.Select(agreement => new MjenicaCard { Client = client, Agreement = agreement }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting bills of exchange for client {customerId}.");
            throw;
        }
    }

    [Authorize(Roles = "probis")]
    public async Task<int> CreateBatchAsync(List<MjenicaModel> models)
    {
        try
        {
            //var state = await AuthProvider.GetAuthenticationStateAsync();
            //var username = state.User.Identity.Name;

            //model.CreatedBy = username;
            //model.CreatedDate = DateTime.Now;

            var entities = new List<Mjenica>();

            foreach (var model in models) entities.Add(MjenicaMapper.MapToEntityCreate(model));

            var numOfCreatedRecords = await _mjenicaRepository.AddBatchAsync(entities);

            return numOfCreatedRecords;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating batch.");
            throw;
        }
    }

    private async Task TransferMjenicaToContract(MjenicaModel mjenica)
    {
        var temp = await _rBBHApiService.GetClientAgreements(mjenica.JMBG);
        var contract = temp.Where(agreement => agreement.ContractNo == mjenica.TransferedToContractNumber)
            .FirstOrDefault();
        mjenica.ContractNumber = contract.ContractNo;
        mjenica.ContractDate = contract.Date;
        mjenica.ContractStatus = contract.Status;
    }

    private void ExempteMjenica(MjenicaModel model, string username)
    {
        model.IsExempted = true;
        model.ExemptedBy = username;
        model.IsExemptedVerified = false;
        model.ExemptionVerifiedBy = null;
        model.ExemptionVerifiedDate = null;

        if (model.ExemptionReason != ExemptionReason.TransferredToAnotherContract)
            model.TransferedToContractNumber = string.Empty;
    }

    private MjenicaExemptionHistory CreateMjenicaHistory(Mjenica entity)
    {
        return new MjenicaExemptionHistory
        {
            MjenicaId = entity.Id,
            ExemptedBy = entity.ExemptedBy,
            ExemptionDate = entity.ExemptionDate,
            ExemptionReason = entity.ExemptionReason.Value,
            IsExemptionVerified = true,
            ExemptionVerifiedBy = entity.ExemptionVerifiedBy,
            ExemptionVerifiedDate = entity.ExemptionVerifiedDate,
            ObtainedOriginalDocuments = entity.ObtainedOriginalDocuments,
            ContractNumber = entity.ContractNumber,
            TransferedToContractNumber = entity.TransferedToContractNumber
        };
    }
}