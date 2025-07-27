using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using GIBS.Module.TaxRates.Repository;
using System.Threading.Tasks;

namespace GIBS.Module.TaxRates.Manager
{
    public class TaxRatesManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly ITaxRatesRepository _TaxRatesRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public TaxRatesManager(ITaxRatesRepository TaxRatesRepository, IDBContextDependencies DBContextDependencies)
        {
            _TaxRatesRepository = TaxRatesRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new TaxRatesContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new TaxRatesContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.TaxRates> TaxRatess = _TaxRatesRepository.GetTaxRatess(module.ModuleId).ToList();
            if (TaxRatess != null)
            {
                content = JsonSerializer.Serialize(TaxRatess);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.TaxRates> TaxRatess = null;
            if (!string.IsNullOrEmpty(content))
            {
                TaxRatess = JsonSerializer.Deserialize<List<Models.TaxRates>>(content);
            }
            if (TaxRatess != null)
            {
                foreach(var TaxRates in TaxRatess)
                {
                    _TaxRatesRepository.AddTaxRates(new Models.TaxRates { ModuleId = module.ModuleId, Municipality = TaxRates.Municipality });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var TaxRates in _TaxRatesRepository.GetTaxRatess(pageModule.ModuleId))
           {
               if (TaxRates.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "GIBSTaxRates",
                       EntityId = TaxRates.TaxRatesId.ToString(),
                       Title = TaxRates.Municipality,
                       Body = TaxRates.Municipality,
                       ContentModifiedBy = TaxRates.ModifiedBy,
                       ContentModifiedOn = TaxRates.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
