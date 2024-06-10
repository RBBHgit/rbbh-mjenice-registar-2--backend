using DataAccess.Entities;
using DataAccess.Interfaces.Repositories;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;
using Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;
using System.Linq.Dynamic.Core;
using Raiffeisen.RegistarMjenica.Services.Contexts;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Repositories;

 public class MjenicaRepository : GenericRepository<Mjenica, MjenicaSearchObject>, IMjenicaRepository
    {
        private readonly IGenericRepository<Mjenica, MjenicaSearchObject> _repository;
        //private readonly IGenericRepository<MjenicaExemptionHistory, MjenicaHistorySearchObject> _historyRepository;

        public MjenicaRepository(IGenericRepository<Mjenica, MjenicaSearchObject> repository, ApplicationDbContext dbContext) : base(dbContext)
        {
            _repository = repository;
        }

        public override IQueryable<Mjenica> AddFilter(IQueryable<Mjenica> query, MjenicaSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (search.Id.HasValue)
            {
                filteredQuery = filteredQuery.Where(x => x.Id == search.Id.Value).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(search.ClientMjenicaSerialNumber))
            {
                filteredQuery = filteredQuery.Where(x => x.ClientMjenicaSerialNumber.Contains(search.ClientMjenicaSerialNumber)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(search.GuarantorMjenicaSerialNumber))
            {
                filteredQuery = filteredQuery.Where(x => x.GuarantorMjenicaSerialNumber.Contains(search.GuarantorMjenicaSerialNumber)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(search.ClientName))
            {
                filteredQuery = filteredQuery.Where(x => x.ClientName.Contains(search.ClientName)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(search.JMBG))
            {
                filteredQuery = filteredQuery.Where(x => x.JMBG.Contains(search.JMBG)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(search.ContractNumber))
            {
                filteredQuery = filteredQuery.Where(x => x.ContractNumber.Contains(search.ContractNumber)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(search.ContractStatus))
            {
                filteredQuery = filteredQuery.Where(x => x.ContractStatus.Contains(search.ContractStatus)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(search.GroupNumber))
            {
                filteredQuery = filteredQuery.Where(x => x.GroupNumber.Contains(search.GroupNumber)).AsQueryable();
            }

            if (search.ContractDateStart != null)
            {
                filteredQuery = filteredQuery.Where(x => x.ContractDate.HasValue && x.ContractDate.Value.Date >= search.ContractDateStart).AsQueryable();
            }

            if (search.ContractDateEnd != null)
            {
                filteredQuery = filteredQuery.Where(x => x.ContractDate.HasValue && x.ContractDate.Value.Date <= search.ContractDateEnd).AsQueryable();
            }

            if (search.CreatedDateStart != null)
            {
                filteredQuery = filteredQuery.Where(x => x.CreatedDate.HasValue && x.CreatedDate.Value.Date >= search.CreatedDateStart).AsQueryable();
            }

            if (search.CreatedDateEnd != null)
            {
                filteredQuery = filteredQuery.Where(x => x.CreatedDate.HasValue && x.CreatedDate.Value.Date <= search.CreatedDateEnd).AsQueryable();
            }

            if (search.IsCreatedVerified != null)
            {
                filteredQuery = filteredQuery.Where(x => x.IsCreatedVerified == search.IsCreatedVerified).AsQueryable();
            }

            return filteredQuery;
        }

        public override IQueryable<Mjenica> ApplySort(IQueryable<Mjenica> query, MjenicaSearchObject search = null)
        {
            var sortedQuery = base.ApplySort(query, search);

            try
            {
                if (search.SortDescriptor != null)
                {
                    var propertyName = search.SortDescriptor.Property;

                    if (!string.IsNullOrEmpty(propertyName))
                    {
                        if (search.SortDescriptor.Ascending)
                            sortedQuery = sortedQuery.OrderBy(propertyName);
                        else
                            sortedQuery = sortedQuery.OrderBy(propertyName + " descending");
                    }
                }
            }
            catch (Exception ex)
            {
                return query;
            }

            return sortedQuery;
        }
    }