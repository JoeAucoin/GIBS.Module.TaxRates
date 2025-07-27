using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace GIBS.Module.TaxRates.Repository
{
    public class TaxRatesContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.TaxRates> TaxRates { get; set; }

        public TaxRatesContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.TaxRates>().ToTable(ActiveDatabase.RewriteName("GIBSTaxRates"));
        }
    }
}
