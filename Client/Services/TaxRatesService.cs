using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace GIBS.Module.TaxRates.Services
{
    public class TaxRatesService : ServiceBase, ITaxRatesService
    {
        public TaxRatesService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("TaxRates");

        public async Task<List<Models.TaxRates>> GetTaxRatessAsync(int ModuleId)
        {
            List<Models.TaxRates> TaxRatess = await GetJsonAsync<List<Models.TaxRates>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.TaxRates>().ToList());
            return TaxRatess.OrderBy(item => item.Municipality).ToList();
        }

        public async Task<Models.TaxRates> GetTaxRatesAsync(int TaxRatesId, int ModuleId)
        {
            return await GetJsonAsync<Models.TaxRates>(CreateAuthorizationPolicyUrl($"{Apiurl}/{TaxRatesId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.TaxRates> AddTaxRatesAsync(Models.TaxRates TaxRates)
        {
            return await PostJsonAsync<Models.TaxRates>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, TaxRates.ModuleId), TaxRates);
        }

        public async Task<Models.TaxRates> UpdateTaxRatesAsync(Models.TaxRates TaxRates)
        {
            return await PutJsonAsync<Models.TaxRates>(CreateAuthorizationPolicyUrl($"{Apiurl}/{TaxRates.TaxRatesId}", EntityNames.Module, TaxRates.ModuleId), TaxRates);
        }

        public async Task DeleteTaxRatesAsync(int TaxRatesId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{TaxRatesId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
