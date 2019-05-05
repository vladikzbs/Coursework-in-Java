using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Inspectors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Coursework_in_Java.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbContext = ApplicationDbContext.Create();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Описание";

            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult Contact()
        {
            ViewBag.Access = "Доступ разрешен только пользователям!";
            ViewBag.Message = "Контакты";

            return View();
        }

        public async Task<ActionResult> InspectorsInfo()
        {
            IEnumerable<InspectorModel> inspectors = dbContext.Inspectors.Where(x => x.Name != "Default").ToList();

            return View(inspectors);
        }

        public async Task<FileContentResult> GetImage(InspectorModel inspector)
        {
            if (inspector != null)
            {
                return File(inspector.Photo.Photo, "image/jpeg");
            }
            else
            {
                return null;
            }
        }

        public FileContentResult GetImageById(int id)
        {
            var inspector = dbContext.Inspectors.Where(x => x.Id == id).Include(x => x.Photo).FirstOrDefault();

            if (inspector != null)
            {
                return File(inspector.Photo.Photo, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
    }
}