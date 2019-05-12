using System.Linq;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;

using Coursework_in_Java.Models;
using Coursework_in_Java.AppKernel.Managers;

namespace Coursework_in_Java.Areas.Inspector.Controllers
{
    [Authorize(Roles = "Inspector")]
    public class PanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private InspectorPanelManager panelManager = InspectorPanelManager.Instance();
        public string UserInspectorId { get; }

        public PanelController()
        {
            UserInspectorId = GetInspectorId();
        }


        /// <summary>
        /// Метод для генерации панели инспектора
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Метод для генерации представления со всеми налоговыми отчетами для инспектора
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CheckReports()
        {
            // Поиск всех налоговых отчетов в менеджере
            var taxDeclarations = await panelManager.GetTaxDeclarationByInspectorIdAsync(db, this.UserInspectorId);

            // Если налоговых отчетов не было найдено, информируем инспектора об этом представлением с сообщением
            if (taxDeclarations == null || taxDeclarations.Count == 0)
            {
                return View("NotFoundReports");
            }

            return View(taxDeclarations);
        }

        /// <summary>
        /// Метод для генерации представления с информацией на проверку налового отчета для инспектора
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Check(int id)
        {
            // Получение подробного описания декларации на проверку инспектору по идентификатору
            var taxDeclaration = await panelManager.GetTaxDeclarationsByIdAsync(db, id);

            return View(taxDeclaration);
        }

        /// <summary>
        /// Метод для обработки формы на проверку налогового отчета
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taxChecked"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Check(int id, bool taxChecked, string message)
        {
            // Редактирование данных налогового отчета через менеджера
            await panelManager.ConfirmEditAsync(db, id, taxChecked, message);

            return View("DeclarationChecked");
        }

        /// <summary>
        /// Метод для выдачи представления с проверенными отчетами инспектора
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CheckedReports()
        {
            // Получение отчетов проверенных инспектором
            var taxDeclarations = await panelManager.GetCheckedReportsAsync(db, this.UserInspectorId);

            // Если отчетов не будет найдено, выдаем представление, что даст сообщение инспектору об этом
            if (taxDeclarations == null || taxDeclarations.Count == 0)
            {
                return View("NotFoundReports");
            }

            return View(taxDeclarations);
        }

        /// <summary>
        /// Освобождение управляемых ресурсов
        /// </summary>
        public new void Dispose()
        {
            this.db.Dispose();
        }

        /// <summary>
        /// Метод для получения идентификатора инспектора
        /// </summary>
        /// <returns></returns>
        private string GetInspectorId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            return identity.Claims.Where(x => x.Type == "userIId").Select(x => x.Value).SingleOrDefault();
        }

    }
}