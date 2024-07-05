using DataAccess.Entities;
using DataAccess.Responses;
using Microsoft.EntityFrameworkCore;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.SearchObjects;
using Raiffeisen.RegistarMjenica.Api.Services.Exceptions;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;
using Raiffeisen.RegistarMjenica.Api.Services.Mappers;

namespace Raiffeisen.RegistarMjenica.Api.Services.Services;

public class MjenicaExemptionHistoryService : IMjenicaExemptionHistoryService
{
    private readonly IRfLogger _logger;

    public MjenicaExemptionHistoryService(IMjenicaExemptionHistoryRepository mjenicaHistoryRepository, IRfLogger logger)
    {
        _mjenicaHistoryRepository = mjenicaHistoryRepository;
        _logger = logger;
    }

    private IMjenicaExemptionHistoryRepository _mjenicaHistoryRepository { get; }

    public async Task<bool> HasAnyAsync(int mjenicaId)
    {
        try
        {
            return await _mjenicaHistoryRepository.Entities.AnyAsync(x => x.MjenicaId == mjenicaId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting exemption history for bill of exchange {mjenicaId}.");
            throw;
        }
    }

    public async Task<PagedResponse<MjenicaHistoryGridModel>> Get(MjenicaHistorySearchObject search)
    {
        try
        {
            var result = await _mjenicaHistoryRepository.Get(search);

            if (result == null || !result.Data.Any())
            {
                return new PagedResponse<MjenicaHistoryGridModel>()
                {
                    Data = new List<MjenicaHistoryGridModel>(),
                    TotalCount = 0
                };
            }

            var mappedResult = MjenicaExemptionHistoryMapper.MapFromEntityList(result.Data);

            return new PagedResponse<MjenicaHistoryGridModel>
            {
                Data = mappedResult,
                TotalCount = result.TotalCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting exemption history for bill of exchange {search.MjenicaId}.");
            throw;
        }
    }
}