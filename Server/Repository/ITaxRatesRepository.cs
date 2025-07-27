using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.TaxRates.Repository
{
    public interface ITaxRatesRepository
    {
        IEnumerable<Models.TaxRates> GetTaxRatess(int ModuleId);
        Models.TaxRates GetTaxRates(int TaxRatesId);
        Models.TaxRates GetTaxRates(int TaxRatesId, bool tracking);
        Models.TaxRates AddTaxRates(Models.TaxRates TaxRates);
        Models.TaxRates UpdateTaxRates(Models.TaxRates TaxRates);
        void DeleteTaxRates(int TaxRatesId);
    }
}
