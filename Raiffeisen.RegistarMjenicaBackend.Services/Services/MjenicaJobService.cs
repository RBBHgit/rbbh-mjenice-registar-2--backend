using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Raiffeisen.RegistarMjenica.Services.Exceptions;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;
using Raiffeisen.RegistarMjenicaBackend.Services.Exceptions;
using Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Services;

public class MjenicaJobService : IMjenicaJobService
{
     private readonly IMjenicaRepository _mjenicaRepository;
    private readonly IRfLogger _logger;

    public MjenicaJobService(IMjenicaRepository mjenicaRepository, IRfLogger logger)
    {
        _mjenicaRepository = mjenicaRepository;
        _logger = logger;
    }

    public async Task<MjenicaJobResult> UpdateContractStatus(Dictionary<string, string> statusBatch)
    {
        var numberOfUpdatedEntries = 0;
        var logId = 0;

        try
        {
            var matchingEntities = _mjenicaRepository.Entities
                .Where(entity => statusBatch.Keys.Any(key => key == entity.ContractNumber))
                .ToList();

            //bool entityHasAlreadySameStatus = matchingEntities.Any(item =>
            //statusBatch.ContainsKey(item.ContractNumber) && statusBatch[item.ContractState] == item.ContractState);

            var numOfFound = matchingEntities.Count;
            var numOfRecievedContractNo = statusBatch.Count;
            var numOfMatchingContractNo = matchingEntities.Count;

            if (numOfFound == 0)
                return new MjenicaJobResult(logId, StatusCodes.Status404NotFound, "Failed", "Podaci nisu pronađeni");

            if (numOfMatchingContractNo != numOfRecievedContractNo)
                return new MjenicaJobResult(logId, StatusCodes.Status500InternalServerError, "Failed",
                    $"Broj poslanih zapisa {statusBatch.Count} i broj pronađenih zapisa {numOfFound} nisu jednaki");

            //if (numOfMatchingContractNo == numOfRecievedContractNo && entityHasAlreadySameStatus)
            //{
            //    //ovdje ce se pozvati logging service i id koji vrati metoda za logiranje reinicijalizirati logId
            //    return new MjenicaJobResult(logId, StatusCodes.Status400BadRequest, "Failed", $"Broj poslanih zapisa {statusBatch.Count} i broj pronađenih zapisa {numOfFound}. Ažuriranje nije uspjelo jer jedan od statusa ugovora nema odgovarajući Contract number.");
            //}

            foreach (var entity in matchingEntities)
                if (statusBatch.TryGetValue(entity.ContractNumber, out var matchingValue))
                    entity.ContractStatus = matchingValue;

            numberOfUpdatedEntries = await _mjenicaRepository.UpdateBatchAsync(matchingEntities);
            if (numOfMatchingContractNo == numOfRecievedContractNo && numberOfUpdatedEntries == 0)
                return new MjenicaJobResult(logId, StatusCodes.Status400BadRequest, "Failed",
                    "Podaci nisu ažurirani jer je status/i već ažuriran/i");
        }
        catch (DbUpdateException ex)
        {
            var exception = new CustomDataAccessException("Updating contract status failed.", "Job");
            exception.DbUpdateException = ex;
            _logger.LogError(exception, exception.Message);
            throw exception;
        }
        catch (OperationCanceledException ex)
        {
            var exception = new CustomDataAccessException("Updating contract status cancelled.", "Job");
            exception.OperationCanceledException = ex;
            _logger.LogError(exception, exception.Message);
            throw exception;
        }

        return new MjenicaJobResult(logId, StatusCodes.Status200OK, "Success", "Podaci uspješno ažurirani");
    }
}