using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using GIBS.Module.TaxRates.Services;

namespace GIBS.Module.TaxRates.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITaxRatesService, TaxRatesService>();
        }
    }
}
