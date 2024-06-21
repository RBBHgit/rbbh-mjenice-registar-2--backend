using DataAccess.Entities;
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

    public async Task<List<MjenicaHistoryGridModel>> Get(MjenicaHistorySearchObject search)
    {
        try
        {
            IEnumerable<MjenicaExemptionHistory> result = null;

            result = await _mjenicaHistoryRepository.Get(search);
            if (result is null || !result.Any()) return new List<MjenicaHistoryGridModel>();
            return MjenicaExemptionHistoryMapper.MapFromEntityList(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting exemption history for bill of exchange {search.MjenicaId}.");
            throw;
        }
    }
}