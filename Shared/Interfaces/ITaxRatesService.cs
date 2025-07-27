using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.TaxRates.Services
{
    public interface ITaxRatesService 
    {
        Task<List<Models.TaxRates>> GetTaxRatessAsync(int ModuleId);

        Task<Models.TaxRates> GetTaxRatesAsync(int TaxRatesId, int ModuleId);

        Task<Models.TaxRates> AddTaxRatesAsync(Models.TaxRates TaxRates);

        Task<Models.TaxRates> UpdateTaxRatesAsync(Models.TaxRates TaxRates);

        Task DeleteTaxRatesAsync(int TaxRatesId, int ModuleId);
    }
}
