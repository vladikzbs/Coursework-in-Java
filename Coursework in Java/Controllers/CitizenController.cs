using System.Linq;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;

using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Users;
using Coursework_in_Java.AppKernel.Managers;
using Coursework_in_Java.AppKernel.DbRepositories;

namespace Coursework_in_Java.Controllers
{
    // Доступ к контроллеру есть только у роли - пользователь
    [Authorize(Roles = "User")]
    public class CitizenController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly CitizenManager citizenManager = CitizenManager.Instance();

        /// <summary>
        /// Уникальный налоговый номер пользователя
        /// </summary>
        public string UserTaxId { get; set; }

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public CitizenController()
        {
            UserTaxId = GetUserTaxId();

            //ApplicationDbContext db = new ApplicationDbContext();
            this.db = new ApplicationDbContext();
        }


        /// <summary>
        /// Метод для нахождения и демонстрирования персональной информации пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PersonalInfo()
        {
            // Выдача персональной информации о пользователе с помощью менеджера по налоговому номеру аккаунта
            var user = await citizenManager.GetCitizenByTaxIdAsync(db, this.UserTaxId);

            if (user == null)
            {
                // Если персональная информация не найдена, перенаправляем пользователя на страницу создания
                return RedirectToAction("CreatePersonalInfo");
            }

            // Если персональная информация найдена, показываем представление с этой информацией
            return View(user);
        }

        /// <summary>
        /// Метод для редактирования персональной информации пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EditPersonalInfo(int id)
        {
            // Получение персональной информации пользователя
            var user = await citizenManager.GetCitizenByTaxIdAndUserIdAsync(db, id, this.UserTaxId);

            if (user == null)
            {
                // Если персональная информация не найдена, перенаправляем на создание
                return RedirectToAction("CreatePersonalInfo");
            }

            // Если персональная информация была найдена, перенаправлем на форму с редактированием
            return View(user);
        }

        /// <summary>
        /// Метод для обработки формы на редактирование персональной информации пользователя
        /// </summary>
        /// <param name="citizen">Персональная информация пользователя</param>
        /// <returns>Представление редактирования</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPersonalInfo(CitizenInformationModel citizen)
        {
            // Проверка на валидность заполненных данных
            if (ModelState.IsValid)
            {
                // Редактирование данных через менеджера
                await citizenManager.EditPersonalInfo(db, citizen);

                // Перенаправление пользователя на представление с просмотром персональной информации пользавателя
                return RedirectToAction("PersonalInfo");
            }

            // Если данные не валидны, отправляем пользователя на форму для дальнейшего редактирования
            return View(citizen);
        }

        /// <summary>
        /// Создание персональных данных о пользователе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CreatePersonalInfo()
        {
            // Проверка, есть ли информация о пользователе с налоговым номером аккаунта в базе данных
            bool isCitizen = await citizenManager.IsCitizenInDbAsync(db, UserTaxId);

            if (isCitizen == false)
            {
                // Если нету инфоормации пользователя, то создаем пустой экземпляр и отдаем представление на его заполнение
                CitizenInformationModel citizen = new CitizenInformationModel();
                return View(citizen);
            }
            else
            {
                // Если информация о пользователе существует, то переводим пользователя на представление с ее просмотром 
                return RedirectToAction("PersonalInfo");
            }
        }

        /// <summary>
        /// Метод для обработки формы на создание персональной информации пользователя
        /// </summary>
        /// <param name="citizen"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreatePersonalInfo(CitizenInformationModel citizen)
        {
            // Добавление уникального налогового номера пользователю
            citizen.CitizenInformationDetail.TaxCardNumber = UserTaxId;

            // Проверка на валидность данных
            if (ModelState.IsValid)
            {
                // Добавление информации в базу данных через менеджера
                await citizenManager.AddCitizenAsync(db, citizen);

                // Перенаправление пользователя на представление с персональной информацией
                return RedirectToAction("PersonalInfo");
            }

            // Если данные были не валидны, возвращаем представление с формой для дальнейшнего заполнения
            return View(citizen);
        }

        /// <summary>
        /// Очистка управляемых ресурсов
        /// </summary>
        public new void Dispose()
        {
            if(db != null)
            {
                db.Dispose();
            }
        }

        private string GetUserTaxId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            return identity.Claims.Where(x => x.Type == "userTId").Select(x => x.Value).SingleOrDefault();
        }
    }
}