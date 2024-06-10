using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;
using Raiffeisen.RegistarMjenicaBackend.Services.Exceptions;
using Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;
using Raiffeisen.RegistarMjenicaBackend.Services.Mappers;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Services;

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