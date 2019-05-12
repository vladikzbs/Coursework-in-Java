using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coursework_in_Java.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Coursework_in_Java.Tests.Controllers
{
    [TestClass()]
    public class TaxReportControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            TaxReportController controller = new TaxReportController();

            ViewResult result = controller.Index().Result as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CreateReportTest()
        {
            TaxReportController controller = new TaxReportController();

            ViewResult result = controller.CreateReport().Result as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            TaxReportController controller = new TaxReportController();

            ViewResult result = controller.Details(1).Result as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(AggregateException))]
        public void DeleteTest()
        {
            TaxReportController controller = new TaxReportController();

            ViewResult result = controller.Delete(1).Result as ViewResult;
        }

        [TestMethod()]
        [ExpectedException(typeof(AggregateException))]
        public void EditReportTest()
        {
            TaxReportController controller = new TaxReportController();

            ViewResult result = controller.EditReport(1).Result as ViewResult;
        }
    }
}