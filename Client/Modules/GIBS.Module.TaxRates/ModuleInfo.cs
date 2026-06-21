using Oqtane.Models;
using Oqtane.Modules;

namespace GIBS.Module.TaxRates
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "TaxRates",
            Description = "Massachusetts Tax Rates",
            Version = "1.0.1",
            ServerManagerType = "GIBS.Module.TaxRates.Manager.TaxRatesManager, GIBS.Module.TaxRates.Server.Oqtane",
            ReleaseVersions = "1.0.0,1.0.1",
            Dependencies = "GIBS.Module.TaxRates.Shared.Oqtane",
            PackageName = "GIBS.Module.TaxRates" 
        };
    }
}
