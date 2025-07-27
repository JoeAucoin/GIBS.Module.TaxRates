using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using GIBS.Module.TaxRates.Repository;
using GIBS.Module.TaxRates.Services;

namespace GIBS.Module.TaxRates.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITaxRatesService, ServerTaxRatesService>();
            services.AddDbContextFactory<TaxRatesContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
