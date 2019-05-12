using System;
using System.Web.Mvc;
using Coursework_in_Java.Controllers;
using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coursework_in_Java.Tests.Controllers
{
    [TestClass]
    public class CitizenControllerTest
    {
        [TestMethod]
        public void PersonalInfo()
        {
            CitizenController controller = new CitizenController();

            ViewResult result = controller.PersonalInfo().Result as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void EditPersonalInfo()
        {
            CitizenController controller = new CitizenController();

            ViewResult result = controller.EditPersonalInfo(1).Result as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreatePersonalInfo()
        {
            CitizenController controller = new CitizenController();

            ViewResult result = controller.CreatePersonalInfo().Result as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
