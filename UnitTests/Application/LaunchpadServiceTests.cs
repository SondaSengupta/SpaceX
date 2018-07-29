using Application;
using AutoFixture;
using Domain;
using Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    class LaunchpadServiceTests
    {
        private Fixture _fixture;
        private LaunchpadService _launchpadService;
        private Mock<ILaunchpadRepository> _repo;

        [SetUp]
        public void Setup()
        {
            this._fixture = new Fixture();
            this._repo = new Mock<ILaunchpadRepository>();


            _launchpadService = new LaunchpadService(_repo.Object);
        }

        #region Get with Filter

        [Test]
        public void Get_Returns_Launchpads()
        {
            var response = _launchpadService.Get("status", "name").Result;
            Assert.IsInstanceOf(typeof(IEnumerable<Launchpad>), response);
        }

        [Test]
        public void Get_Filters_Launchpads_ByStatus()
        {
            var pad1 = _fixture.Build<Launchpad>().With(x => x.Status, "retired").Create();
            var pad2 = _fixture.Build<Launchpad>().With(x => x.Status, "active").Create();

            _repo.Setup(x => x.Get()).ReturnsAsync(new List<Launchpad> { pad1, pad2 });


            var response = _launchpadService.Get("active", null).Result;

            Assert.AreEqual(response.Count(), 1);
            Assert.AreEqual(response.First(), pad2);
        }

        [Test]
        public void Get_Filters_Launchpads_ByNameContains()
        {
            var pad1 = _fixture.Build<Launchpad>().With(x => x.Name, "Marvin the Martian").Create();
            var pad2 = _fixture.Build<Launchpad>().With(x => x.Name, "SpaceX Area 52").Create();

            _repo.Setup(x => x.Get()).ReturnsAsync(new List<Launchpad> { pad1, pad2 });


            var response = _launchpadService.Get(null, "SpaceX").Result;

            Assert.AreEqual(response.Count(), 1);
            Assert.AreEqual(response.First(), pad2);
        }

        #endregion
        #region Get By Id


        [Test]
        public void GetById_Returns_Launchpads()
        {
            var pad = _fixture.Create<Launchpad>();
            _repo.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(pad);

            var response = _launchpadService.GetById(It.IsAny<string>()).Result;
            Assert.AreEqual(response, pad);
        }
        #endregion
    }
}
