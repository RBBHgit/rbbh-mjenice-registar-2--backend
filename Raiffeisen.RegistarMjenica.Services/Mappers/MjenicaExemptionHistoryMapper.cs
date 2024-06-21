using DataAccess.Common.Extensions;
using DataAccess.Entities;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;

namespace Raiffeisen.RegistarMjenica.Api.Services.Mappers;

public static class MjenicaExemptionHistoryMapper
{
    public static MjenicaExemptionHistory MapToEntityCreate(MjenicaModel model)
    {
        return new MjenicaExemptionHistory
        {
            ObtainedOriginalDocuments = model.ObtainedOriginalDocuments,
            ExemptionReason = model.ExemptionReason.Value,
            ExemptionDate = model.ExemptionDate,
            MjenicaId = model.Id,
            ContractNumber = model.ContractNumber,
            TransferedToContractNumber = model.TransferedToContractNumber
        };
    }

    public static List<MjenicaHistoryGridModel> MapFromEntityList(IEnumerable<MjenicaExemptionHistory> entityList)
    {
        return entityList.Select(item => new MjenicaHistoryGridModel
        {
            ContractNumber = item.ContractNumber,
            ExemptedBy = item.ExemptedBy,
            ExemptionDate = item.ExemptionDate,
            ExemptionReason = item.ExemptionReason,
            ExemptionVerifiedBy = item.ExemptionVerifiedBy,
            ExemptionVerifiedDate = item.ExemptionVerifiedDate,
            ObtainedOriginalDocuments = item.ObtainedOriginalDocuments,
            TransferedToContractNumber = item.TransferedToContractNumber,
            BadgeColor = item.ExemptionReason.ReasonToBackColor()
        })
            .ToList();
    }
}