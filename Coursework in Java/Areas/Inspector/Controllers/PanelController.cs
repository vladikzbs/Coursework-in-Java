using Coursework_in_Java.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading;
using Coursework_in_Java.AppKernel.Managers;

namespace Coursework_in_Java.Areas.Inspector.Controllers
{
    [Authorize(Roles = "Inspector")]
    public class PanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private InspectorPanelManager panelManager = InspectorPanelManager.Instance();
        public string UserIId { get; }

        public PanelController()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            UserIId = identity.Claims.Where(x => x.Type == "userIId").Select(x => x.Value).SingleOrDefault();
        }


        // GET: Inspector/Panel
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CheckReports()
        {
            //var taxDeclarations = await db.TaxDeclarations
            //                              .Include(x => x.DeclarationCheck)
            //                              .Include(x => x.DeclarationCheck.Inspector)
            //                              .Include(x => x.CitizenInformation)
            //                              .Where(x => x.DeclarationCheck.Inspector.SpecialNumber == this.UserIId 
            //                              && x.DeclarationCheck.Passed == false && x.DeclarationCheck.Checked == false)
            //                              .ToListAsync();

            var taxDeclarations = await panelManager.GetTaxDeclarationByInspectorIdAsync(db, this.UserIId);

            if (taxDeclarations == null || taxDeclarations.Count == 0)
            {
                return View("NotFoundReports");
            }

            return View(taxDeclarations);
        }

        [HttpGet]
        public async Task<ActionResult> Check(int id)
        {
            //var taxDeclarations = await db.TaxDeclarations
            //                  .Include(x => x.DeclarationCheck)
            //                  .Include(x => x.DeclarationCheck.Inspector)
            //                  .Include(x => x.CitizenInformation.CitizenInformationDetail)
            //                  .Include(x => x.CitizenInformation.CitizenInformationDetail.Phone)
            //                  .Include(x => x.CitizenInformation.CitizenInformationDetail.Address)
            //                  .Include(x => x.CitizenInformation)
            //                  .Include(x => x.TaxDeclarationDetail)
            //                  .Include(x => x.TaxDeclarationDetail.Income)
            //                  .Include(x => x.TaxDeclarationDetail.Tax)
            //                  .Where(x => x.Id == id)
            //                  .ToListAsync();

            var taxDeclaration = await panelManager.GetTaxDeclarationsByIdAsync(db, id);

            return View(taxDeclaration);
        }

        [HttpPost]
        public async Task<ActionResult> Check(int id, bool taxChecked, string message)
        {
            await panelManager.ConfirmEditAsync(db, id, taxChecked, message);
            //var declarationCheck = await db.DeclarationChecks.Where(x => x.DeclarationId == id).SingleOrDefaultAsync();

            //declarationCheck.Passed = taxChecked;
            //declarationCheck.Checked = true;
            //declarationCheck.Message = message;
            //declarationCheck.DateOfEnd = DateTime.Now;

            //db.Entry(declarationCheck).State = EntityState.Modified;
            //await db.SaveChangesAsync();
            return View("DeclarationChecked");
        }

        public async Task<ActionResult> CheckedReports()
        {
            //var taxDeclarations = await db.TaxDeclarations
            //                  .Include(x => x.DeclarationCheck)
            //                  .Include(x => x.DeclarationCheck.Inspector)
            //                  .Include(x => x.CitizenInformation)
            //                  .Where(x => x.DeclarationCheck.Inspector.SpecialNumber == this.UserIId && x.DeclarationCheck.Checked == true)
            //                  .ToListAsync();

            var taxDeclarations = await panelManager.GetCheckedReportsAsync(db, this.UserIId);

            if (taxDeclarations == null || taxDeclarations.Count == 0)
            {
                return View("NotFoundReports");
            }

            return View(taxDeclarations);
        }
    }
}