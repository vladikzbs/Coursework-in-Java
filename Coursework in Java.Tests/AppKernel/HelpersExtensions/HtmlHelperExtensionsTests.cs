using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coursework_in_Java.AppKernel.HelpersExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework_in_Java.Models.Inspectors;
using System.Web.Mvc;

namespace Coursework_in_Java.Tests.HtmlExtensions
{
    [TestClass()]
    public class HtmlHelperExtensionsTests
    {
        [TestMethod()]
        public void HasInspectorTest()
        {
            InspectorModel inspector = new InspectorModel("Name", "Surname", "Patronymic", new byte[] { 1, 2, 3 });
            bool result = HtmlHelperExtensions.HasInspector(null, inspector);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void YesNoTest()
        {
            string result = HtmlHelperExtensions.YesNo(null, true);

            Assert.AreSame("Так", result);
        }

        [TestMethod()]
        public void YesNoTest1()
        {
            string result = HtmlHelperExtensions.YesNo(null, false);

            Assert.AreSame("Ні", result);
        }
    }
}