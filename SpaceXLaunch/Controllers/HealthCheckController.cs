using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SpaceXLaunch.Controllers
{
    [Route("api/healthcheck")]
    public class HealthCheckController : Controller
    {
        private ILaunchpadRepository _launchPadRepo;
        private readonly ILogger<HealthCheckController> _logger;
        public HealthCheckController(ILaunchpadRepository launchPadRepo, ILogger<HealthCheckController> logger) {
            _launchPadRepo = launchPadRepo ?? throw new ArgumentNullException("Launchpad Repo is Required");
            _logger = logger ?? throw new ArgumentNullException("Logger is Required.");
        }


        //Healthcheck can easily be added to deployment process to ensure that the app is functioning correctly
        //and all integrations are connected and ready
        [HttpGet]
        public async Task<IActionResult> GetHealthCheck()
        {

            try
            {
                var result = await _launchPadRepo.Get();
                return Ok("Ok, ready to launch!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
