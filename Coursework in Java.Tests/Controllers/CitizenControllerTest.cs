using System;
using System.Web.Mvc;
using Coursework_in_Java.Controllers;
using Coursework_in_Java.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coursework_in_Java.Tests.Controllers
{
    [TestClass]
    public class CitizenControllerTest
    {
        [TestMethod]
        public async void PersonalInfo()
        {
            CitizenController controller = new CitizenController();

            ViewResult result = await controller.PersonalInfoAsync() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void EditPersonalInfo()
        {
            CitizenController controller = new CitizenController();

            ViewResult result = await controller.EditPersonalInfoAsync(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void CreatePersonalInfo()
        {
            CitizenController controller = new CitizenController();

            ViewResult result = await controller.CreatePersonalInfoAsync() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
