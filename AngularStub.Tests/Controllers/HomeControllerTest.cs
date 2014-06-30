using System.Web.Mvc;
using AngularStub.Api.Controllers;
using AngularStub.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AngularStub.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _controller;
        private ViewResult _result;

        [TestInitialize]
        public void GivenHomeController()
        {
            _controller = new HomeController();
        }

        private void WhenIndexIsCalled()
        {
            _result = _controller.Index() as ViewResult;
        }

        private void ThenViewIsReturned()
        {
            Assert.IsNotNull(_result);
            Assert.IsInstanceOfType(_result, typeof(ViewResult));
            Assert.AreEqual("Home Page", _result.ViewBag.Title);
        }

        [TestMethod]
        public void Index()
        {
            // GivenHomeController();
            WhenIndexIsCalled();
            ThenViewIsReturned();
        }

        [TestCleanup]
        public void CloseConnection()
        {
            _controller.Dispose();
        }
    }
}