using DataAccess.Entities;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;

namespace Raiffeisen.RegistarMjenica.Api.Services.Mappers;

public static class MjenicaMapper
{
    public static Mjenica MapToEntityCreate(MjenicaModel model)
    {
        return new Mjenica
        {
            ClientMjenicaSerialNumber = model.ClientMjenicaSerialNumber,
            GuarantorMjenicaSerialNumber = model.GuarantorMjenicaSerialNumber,
            JMBG = model.JMBG,
            ClientName = model.ClientName,
            ContractNumber = model.ContractNumber,
            ContractDate = model.ContractDate,
            ContractStatus = model.ContractStatus,
            GroupNumber = model.GroupNumber,
            FreeTextField = model.FreeTextField,
            CreatedBy = model.CreatedBy,
            CreatedDate = model.CreatedDate,
            IsCreatedVerified = model.IsCreatedVerified,
            IsExempted = model.IsExempted
        };
    }

    public static Mjenica MapToEntityUpdate(MjenicaModel model)
    {
        var entity = MapToEntityCreate(model);

        entity.Id = model.Id;
        entity.CreationVerifiedBy = model.CreationVerifiedBy;
        entity.CreationVerifiedDate = model.CreationVerifiedDate;
        entity.ExemptedBy = model.ExemptedBy;
        entity.ExemptionDate = model.ExemptionDate;
        entity.IsExemptedVerified = model.IsExemptedVerified;
        entity.ExemptionVerifiedBy = model.ExemptionVerifiedBy;
        entity.ExemptionVerifiedDate = model.ExemptionVerifiedDate;
        entity.ExemptionReason = model.ExemptionReason;
        entity.ObtainedOriginalDocuments = model.ObtainedOriginalDocuments;
        entity.TransferedToContractNumber = model.TransferedToContractNumber;

        return entity;
    }

    public static MjenicaModel MapFromEntity(Mjenica entity)
    {
        var model = new MjenicaModel();

        model.Id = entity.Id;
        model.ClientMjenicaSerialNumber = entity.ClientMjenicaSerialNumber;
        model.GuarantorMjenicaSerialNumber = entity.GuarantorMjenicaSerialNumber;
        model.JMBG = entity.JMBG;
        model.ClientName = entity.ClientName;
        model.ContractNumber = entity.ContractNumber;
        model.ContractDate = entity.ContractDate;
        model.ContractStatus = entity.ContractStatus;
        model.ExemptionDate = entity.ExemptionDate;
        model.ExemptionReason = entity.ExemptionReason;
        model.ExemptionVerifiedBy = entity.ExemptionVerifiedBy;
        model.ExemptionVerifiedDate = entity.ExemptionVerifiedDate;
        model.CreatedBy = entity.CreatedBy;
        model.CreatedDate = entity.CreatedDate;
        model.IsCreatedVerified = entity.IsCreatedVerified ?? false;
        model.CreationVerifiedBy = entity.CreationVerifiedBy;
        model.ObtainedOriginalDocuments = entity.ObtainedOriginalDocuments;
        model.FreeTextField = entity.FreeTextField;
        model.CreationVerifiedDate = entity.CreationVerifiedDate;
        model.ExemptedBy = entity.ExemptedBy;
        model.IsExemptedVerified = entity.IsExemptedVerified;
        model.IsExempted = entity.IsExempted ?? false;
        model.GroupNumber = entity.GroupNumber;
        model.TransferedToContractNumber = entity.TransferedToContractNumber;

        return model;
    }

    public static List<MjenicaGridModel> MapFromEntityList(IEnumerable<Mjenica> entityList)
    {
        return entityList.Select(item => new MjenicaGridModel
            {
                Id = item.Id.ToString(),
                ClientMjenicaSerialNumber = item.ClientMjenicaSerialNumber,
                GuarantorMjenicaSerialNumber = item.GuarantorMjenicaSerialNumber,
                JMBG = item.JMBG,
                ClientName = item.ClientName,
                ContractDate = item.ContractDate,
                ContractStatus = item.ContractStatus,
                ContractNumber = item.ContractNumber,
                CreatedDate = item.CreatedDate,
                IsCreatedVerified = item.IsCreatedVerified ?? false,
                GroupNumber = item.GroupNumber
            })
            .ToList();
    }
}