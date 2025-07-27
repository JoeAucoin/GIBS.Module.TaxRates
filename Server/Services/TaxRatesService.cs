using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using GIBS.Module.TaxRates.Repository;

namespace GIBS.Module.TaxRates.Services
{
    public class ServerTaxRatesService : ITaxRatesService
    {
        private readonly ITaxRatesRepository _TaxRatesRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerTaxRatesService(ITaxRatesRepository TaxRatesRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _TaxRatesRepository = TaxRatesRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.TaxRates>> GetTaxRatessAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_TaxRatesRepository.GetTaxRatess(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.TaxRates> GetTaxRatesAsync(int TaxRatesId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_TaxRatesRepository.GetTaxRates(TaxRatesId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Get Attempt {TaxRatesId} {ModuleId}", TaxRatesId, ModuleId);
                return null;
            }
        }

        public Task<Models.TaxRates> AddTaxRatesAsync(Models.TaxRates TaxRates)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, TaxRates.ModuleId, PermissionNames.Edit))
            {
                TaxRates = _TaxRatesRepository.AddTaxRates(TaxRates);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "TaxRates Added {TaxRates}", TaxRates);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Add Attempt {TaxRates}", TaxRates);
                TaxRates = null;
            }
            return Task.FromResult(TaxRates);
        }

        public Task<Models.TaxRates> UpdateTaxRatesAsync(Models.TaxRates TaxRates)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, TaxRates.ModuleId, PermissionNames.Edit))
            {
                TaxRates = _TaxRatesRepository.UpdateTaxRates(TaxRates);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "TaxRates Updated {TaxRates}", TaxRates);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Update Attempt {TaxRates}", TaxRates);
                TaxRates = null;
            }
            return Task.FromResult(TaxRates);
        }

        public Task DeleteTaxRatesAsync(int TaxRatesId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _TaxRatesRepository.DeleteTaxRates(TaxRatesId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "TaxRates Deleted {TaxRatesId}", TaxRatesId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Delete Attempt {TaxRatesId} {ModuleId}", TaxRatesId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
