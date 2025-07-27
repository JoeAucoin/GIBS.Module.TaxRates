using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace GIBS.Module.TaxRates.Repository
{
    public class TaxRatesRepository : ITaxRatesRepository, ITransientService
    {
        private readonly IDbContextFactory<TaxRatesContext> _factory;

        public TaxRatesRepository(IDbContextFactory<TaxRatesContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.TaxRates> GetTaxRatess(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.TaxRates.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.TaxRates GetTaxRates(int TaxRatesId)
        {
            return GetTaxRates(TaxRatesId, true);
        }

        public Models.TaxRates GetTaxRates(int TaxRatesId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.TaxRates.Find(TaxRatesId);
            }
            else
            {
                return db.TaxRates.AsNoTracking().FirstOrDefault(item => item.TaxRatesId == TaxRatesId);
            }
        }

        public Models.TaxRates AddTaxRates(Models.TaxRates TaxRates)
        {
            using var db = _factory.CreateDbContext();
            db.TaxRates.Add(TaxRates);
            db.SaveChanges();
            return TaxRates;
        }

        public Models.TaxRates UpdateTaxRates(Models.TaxRates TaxRates)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(TaxRates).State = EntityState.Modified;
            db.SaveChanges();
            return TaxRates;
        }

        public void DeleteTaxRates(int TaxRatesId)
        {
            using var db = _factory.CreateDbContext();
            Models.TaxRates TaxRates = db.TaxRates.Find(TaxRatesId);
            db.TaxRates.Remove(TaxRates);
            db.SaveChanges();
        }
    }
}
