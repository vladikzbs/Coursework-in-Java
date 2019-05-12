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

        /// <summary>
        /// Метод для выдачи представления главной страницы сайта
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Метод для выдачи представления со страницой "О сайте"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Опис";

            return View();
        }

        /// <summary>
        /// Метод для выдачи представления с контактами
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Access = "Доступ лише для користувачів!";
            ViewBag.Message = "Контакти";

            return View();
        }

        /// <summary>
        /// Метод для выдачи информации о инспекторах
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> InspectorsInfo()
        {
            IEnumerable<InspectorModel> inspectors = dbContext.Inspectors.Where(x => x.Name != "Default").ToList();

            return View(inspectors);
        }

        /// <summary>
        /// Метод для получения изображения
        /// </summary>
        /// <param name="inspector"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод для получения изображения по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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