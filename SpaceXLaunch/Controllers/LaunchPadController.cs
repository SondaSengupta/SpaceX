using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SpaceXLaunch.Controllers
{
    [Route("api/launchpads")]
    public class LaunchpadController : Controller
    {
        private ILaunchpadService _launchPadService;
        private readonly ILogger<LaunchpadController> _logger;
        public LaunchpadController(ILaunchpadService launchPadService, ILogger<LaunchpadController> logger) {
            _launchPadService = launchPadService ?? throw new ArgumentNullException("LaunchpadService is Required");
            _logger = logger ?? throw new ArgumentNullException("Logger is Required.");
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] LaunchpadFilterModel filterRequest)
        {
            try
            {
                var launchpads = await _launchPadService.Get(filterRequest.Status, filterRequest.NameContains);
                var launchpadModels = launchpads.Select(x => new LaunchpadModel(x.Id, x.Name, x.Status)).ToList();
                return Ok(launchpadModels);
            }
            //These 500 catches are redundant, but just an example exception handling, logging, and hopefully enhance readability
            //There are two logs created: one catchs all app info connected to Core's ILogger, and one captures only Api and explicit message concerns
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id}", Name = "Get Launchpad By Id")]
        public async Task<IActionResult>  Get(string id)
        {
            try
            {
                var launchpad = await _launchPadService.GetById(id);
                if (launchpad == null) {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                var launchpadModel = new LaunchpadModel(launchpad.Id, launchpad.Name, launchpad.Status);

                return Ok(launchpadModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
