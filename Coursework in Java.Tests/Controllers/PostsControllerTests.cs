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
    public class PostsControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            PostsController controller = new PostsController();

            ViewResult result = controller.Index().Result as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CreateTest()
        {
            PostsController controller = new PostsController();

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}