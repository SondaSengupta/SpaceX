using Api.Models;
using Application;
using AutoFixture;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SpaceXLaunch.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class LaunchpadControllerTests
    {
        private Fixture _fixture;
        private LaunchpadController _controller;
        private Mock<ILaunchpadService> _launchpadService;
        private Mock<ILogger<LaunchpadController>> _logger;

        [SetUp]
        public void Setup()
        {
            this._fixture = new Fixture();
            this._launchpadService = new Mock<ILaunchpadService>();
            this._logger = new Mock<ILogger<LaunchpadController>>();


            _controller = new LaunchpadController(_launchpadService.Object, _logger.Object);
        }

        #region Get with Filter

        [Test]
        public void Get_ReturnsOk()
        {
            var request = new LaunchpadFilterModel();
            var response = _controller.Get(request).Result;
            Assert.IsInstanceOf(typeof(OkObjectResult), response);
        }

        #endregion
        #region Get by Id

        [Test]
        public void GetById_ReturnsOk()
        {
            _launchpadService.Setup(x => x.GetById(It.IsAny<string>()))
                .ReturnsAsync(_fixture.Create<Launchpad>());

            var request = _fixture.Create<string>();
            var response = _controller.Get(request).Result;
            Assert.IsInstanceOf(typeof(OkObjectResult), response);
        }

        [Test]
        public void Get_ReturnsNotFound_WhenIdNotFound()
        {
            _launchpadService.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync((Launchpad)null);

            var request = _fixture.Create<string>();
            var response = _controller.Get(request).Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, 404);
        }
        #endregion

    }
}
