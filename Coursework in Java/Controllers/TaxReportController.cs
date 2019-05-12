using Coursework_in_Java.AppKernel.Managers;
using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Inspectors;
using Coursework_in_Java.Models.Tax;
using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Coursework_in_Java.Controllers
{
    [Authorize(Roles = "User")]
    public class TaxReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TaxReportManager reportManager = TaxReportManager.Instance();
        public string UserTaxId { get; }

        public TaxReportController()
        {
            UserTaxId = GetUserTaxId();
        }

        /// <summary>
        /// Метод для генерации представления со списком всех деклараций пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var declarations = await db.TaxDeclarations.Where(x => x.CitizenInformation.CitizenInformationDetail.TaxCardNumber == UserTaxId)
                                                       //.Include(x => x.CitizenInformation)
                                                       //.Include(x => x.CitizenInformation.CitizenInformationDetail)
                                                       .Include(x => x.DeclarationCheck)
                                                       .Include(x => x.DeclarationCheck.Inspector)
                                                       .ToListAsync();

            return View(declarations);
        }

        /// <summary>
        /// Метод для создания представления о создании налового отчета
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CreateReport()
        {
            // Получение информации о пользователе по налоговому номеру
            var citizen = await reportManager.GetCitizenByTaxIdAsync(this.db, this.UserTaxId);

            // Если информация не найдена, выдаем представление, что данные не заполнены
            if (citizen == null)
            {
                return View("NotFoundPersonalData");
            }

            // Создание пустого экземплара налогового отчета
            TaxDeclarationModel taxDeclaration = new TaxDeclarationModel();

            // Получение типов деклараций
            ViewBag.DeclarationTypeItems = reportManager.DeclarationTypes;
            // Получение типов оплаты
            ViewBag.PayerCaterogyItems = reportManager.PayerCategories;
            // Получение инспекторов
            ViewBag.Inspectors = reportManager.GetInspectorsList(db);
            // Получение уникального номера декларации
            ViewBag.ReportNumber = new Random().Next(10000, 99999).ToString();

            return View(taxDeclaration);
        }

        /// <summary>
        /// Метод для обработки формы о создании отчета
        /// </summary>
        /// <param name="taxDeclaration"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateReport(TaxDeclarationModel taxDeclaration, string number)
        {
            // Получение инспектора по номеру через менеджера
            var inspector = await reportManager.GetInspectorBySpecialNumberAsync(db, number);

            // Проверка на валидность данных отчета
            if (ModelState.IsValid)
            {
                // Получение информации о пользователе через менеджера по налоговому номеру
                var citizen = await reportManager.GetCitizenByTaxIdAsync(this.db, this.UserTaxId);

                // Если пользователь не найден, отдаем представление, что нет персональных данных
                if (citizen == null)
                {
                    return View("NotFoundPersonalData");
                }

                // Регистрация заполненного налогового отчета
                await reportManager.RegisterDeclarationAsync(db, citizen, taxDeclaration, inspector);

                return RedirectToAction("Index");
            }

            // Получение типов деклараций
            ViewBag.DeclarationTypeItems = reportManager.DeclarationTypes;
            // Получение типов оплаты
            ViewBag.PayerCaterogyItems = reportManager.PayerCategories;
            // Получение инспекторов
            ViewBag.Inspectors = reportManager.GetInspectorsList(db);
            // Получение уникального номера декларации
            ViewBag.ReportNumber = taxDeclaration.UniqueDeclarationId;

            return View(taxDeclaration);
        }

        /// <summary>
        /// Метод для выдачи представления с подробной информацией об налоговом отчете
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            // Получение всей информации о отчете по ид из бд
            var taxDeclaration = await db.TaxDeclarations.Where(x => x.Id == id)
                                              .Include(x => x.TaxDeclarationDetail)
                                              .Include(x => x.TaxDeclarationDetail.Income)
                                              .Include(x => x.TaxDeclarationDetail.Tax)
                                              .Include(x => x.DeclarationCheck.Inspector)
                                              .Include(x => x.CitizenInformation)
                                              .Include(x => x.CitizenInformation.CitizenInformationDetail)
                                              .SingleOrDefaultAsync();

            return View(taxDeclaration);
        }

        /// <summary>
        /// Метод для выдачи представления для удаления налогового отчета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            // Получение информации о налоговом отчете по идентификатору в бд
            var taxDeclaration = await db.TaxDeclarations.Where(x => x.Id == id)
                                  .Include(x => x.DeclarationCheck)
                                  .SingleOrDefaultAsync();

            // Если декларация прошла проверку, то ее проверка запрещена. Показываем представление пользователю
            if (taxDeclaration.DeclarationCheck.Checked == true && taxDeclaration.DeclarationCheck.Passed == true)
            {
                return View("DeleteCanceledPermission");
            }

            return View(taxDeclaration);
        }

        /// <summary>
        /// Метод для обработки формы по удалению налогового отчета
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UniqueDeclarationId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int Id, string UniqueDeclarationId)
        {
            // Удаление налогового отчета по идентификаторам через менеджера
            await reportManager.DeleteDeclarationByIdAndUniqueIdAsync(db, Id, UniqueDeclarationId);

            return View("DeleteSucceded");
        }

        /// <summary>
        /// Метод для генерации представления с редактированием отчета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EditReport(int id)
        {
            // Получение налогового отчета по идентификатору из бд через менеджера
            var taxDeclaration = await reportManager.GetDeclarationByIdAsync(db, id);

            // Если декларация прошла проверку, то ее проверка запрещена. Показываем представление пользователю
            if (taxDeclaration.DeclarationCheck.Checked == true && taxDeclaration.DeclarationCheck.Passed == true)
            {
                return View("EditCanceledPermission");
            }

            // Получение списка инспекторов из бд через менеджера
            ViewBag.Inspectors = reportManager.GetInspectorsList(db);

            return View(taxDeclaration);
        }

        /// <summary>
        /// Метод для обработки формы по редактированию налогового отчета
        /// </summary>
        /// <param name="taxDeclaration"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditReport(TaxDeclarationModel taxDeclaration, string number)
        {
            // Получение экземпляра инспектора по номеру
            var inspector = await reportManager.GetInspectorBySpecialNumberAsync(db, number);

            // Проверка на валидность заполненных данных
            if (ModelState.IsValid)
            {
                // Подтверждение обновления данных в бд через менеджера
                await reportManager.ConfirmEditAsync(db, taxDeclaration, inspector);

                // Перенаправление пользователя на страницу со списком отчетов
                return RedirectToAction("Index");
            }

            // Получение списка инспекторов из бд через менеджера
            ViewBag.Inspectors = reportManager.GetInspectorsList(db);
            return View(taxDeclaration);
        }

        /// <summary>
        /// Метод для выдачи представления с правилами заполнения налогового отчета
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Rules()
        {
            return View();
        }

        /// <summary>
        /// Освобождение управляемых ресурсов
        /// </summary>
        public new void Dispose()
        {
            this.db.Dispose();
        }

        /// <summary>
        /// Получение налогового идентификатора для аккаунта
        /// </summary>
        /// <returns></returns>
        private string GetUserTaxId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            return identity.Claims.Where(x => x.Type == "userTId").Select(x => x.Value).SingleOrDefault();
        }

    }
}