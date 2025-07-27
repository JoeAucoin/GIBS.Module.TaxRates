using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.TaxRates.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace GIBS.Module.TaxRates.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class TaxRatesController : ModuleControllerBase
    {
        private readonly ITaxRatesService _TaxRatesService;

        public TaxRatesController(ITaxRatesService TaxRatesService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _TaxRatesService = TaxRatesService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.TaxRates>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _TaxRatesService.GetTaxRatessAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.TaxRates> Get(int id, int moduleid)
        {
            Models.TaxRates TaxRates = await _TaxRatesService.GetTaxRatesAsync(id, moduleid);
            if (TaxRates != null && IsAuthorizedEntityId(EntityNames.Module, TaxRates.ModuleId))
            {
                return TaxRates;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Get Attempt {TaxRatesId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.TaxRates> Post([FromBody] Models.TaxRates TaxRates)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, TaxRates.ModuleId))
            {
                TaxRates = await _TaxRatesService.AddTaxRatesAsync(TaxRates);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Post Attempt {TaxRates}", TaxRates);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                TaxRates = null;
            }
            return TaxRates;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.TaxRates> Put(int id, [FromBody] Models.TaxRates TaxRates)
        {
            if (ModelState.IsValid && TaxRates.TaxRatesId == id && IsAuthorizedEntityId(EntityNames.Module, TaxRates.ModuleId))
            {
                TaxRates = await _TaxRatesService.UpdateTaxRatesAsync(TaxRates);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Put Attempt {TaxRates}", TaxRates);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                TaxRates = null;
            }
            return TaxRates;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.TaxRates TaxRates = await _TaxRatesService.GetTaxRatesAsync(id, moduleid);
            if (TaxRates != null && IsAuthorizedEntityId(EntityNames.Module, TaxRates.ModuleId))
            {
                await _TaxRatesService.DeleteTaxRatesAsync(id, TaxRates.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TaxRates Delete Attempt {TaxRatesId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
