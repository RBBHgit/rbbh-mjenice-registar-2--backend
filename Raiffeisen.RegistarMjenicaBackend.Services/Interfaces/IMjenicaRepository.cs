

using DataAccess.Entities;
using DataAccess.Interfaces.Repositories;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

public interface IMjenicaRepository : IGenericRepository<Mjenica, MjenicaSearchObject>
{
}