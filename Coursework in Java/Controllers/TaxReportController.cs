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
        public string UserTId { get; }

        public TaxReportController()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            UserTId = identity.Claims.Where(x => x.Type == "userTId").Select(x => x.Value).SingleOrDefault();
        }

        // GET: Index
        public async Task<ActionResult> Index()
        {
            var declarations = await db.TaxDeclarations.Where(x => x.CitizenInformation.CitizenInformationDetail.TaxCardNumber == UserTId)
                                                       //.Include(x => x.CitizenInformation)
                                                       //.Include(x => x.CitizenInformation.CitizenInformationDetail)
                                                       .Include(x => x.DeclarationCheck)
                                                       .Include(x => x.DeclarationCheck.Inspector)
                                                       .ToListAsync();

            return View(declarations);
        }

        // GET: CreateReport
        [HttpGet]
        public async Task<ActionResult> CreateReport()
        {
            //CitizenInformationModel citizen = await db.CitizenInformation
            //                                          .Where(x => x.CitizenInformationDetail.TaxCardNumber == UserTId)
            //                                          .Include(x => x.CitizenInformationDetail)
            //                                          .Include(x => x.CitizenInformationDetail.Phone)
            //                                          .Include(x => x.CitizenInformationDetail.Address)
            //                                          .SingleOrDefaultAsync();

            var citizen = await reportManager.GetCitizenByTaxIdAsync(this.db, this.UserTId);

            if (citizen == null)
            {
                return View("NotFoundPersonalData");
            }

            TaxDeclarationModel taxDeclaration = new TaxDeclarationModel();

            //SelectList list1 = reportManager.DeclarationTypes;
            //SelectList list2 = reportManager.PayerCategories;
            //SelectList list3 = GetInspectorsList();
            ViewBag.DeclarationTypeItems = reportManager.DeclarationTypes;
            ViewBag.PayerCaterogyItems = reportManager.PayerCategories;
            ViewBag.Inspectors = reportManager.GetInspectorsList(db);
            ViewBag.ReportNumber = new Random().Next(10000, 99999).ToString();

            return View(taxDeclaration);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReport(TaxDeclarationModel taxDeclaration, string number)
        {
            var inspector = await reportManager.GetInspectorBySpecialNumberAsync(db, number);

            if (ModelState.IsValid)
            {
                //CitizenInformationModel citizen = await db.CitizenInformation
                //                                          .Where(x => x.CitizenInformationDetail.TaxCardNumber == UserTId)
                //                                          .Include(x => x.CitizenInformationDetail)
                //                                          .Include(x => x.CitizenInformationDetail.Phone)
                //                                          .Include(x => x.CitizenInformationDetail.Address)
                //                                          .SingleOrDefaultAsync();

                var citizen = await reportManager.GetCitizenByTaxIdAsync(this.db, this.UserTId);

                if (citizen == null)
                {
                    return View("NotFoundPersonalData");
                }

                //var declarations = await db.DeclarationChecks.Where(x => x.TaxDeclaration.CitizenInformation.CitizenInformationDetail.TaxCardNumber == UserTId)
                //                                     .ToListAsync();

                //int lineItemId = 0;
                //int declarationId = 0;

                //if (declarations?.Count == 0)
                //{
                //    ;
                //}
                //else if (declarations?.Count > 0)
                //{
                //    lineItemId = declarations.Max(x => x.LineItem);
                //    declarationId = declarations.Max(x => x.DeclarationId);
                //}


                await reportManager.RegisterDeclarationAsync(db, citizen, taxDeclaration, inspector);

                //DeclarationCheckModel declarationCheckModel = new DeclarationCheckModel
                //{
                //    DeclarationId = -1,
                //    InspectorId = inspector.Id,
                //    Inspector = inspector,
                //    DateOfStart = DateTime.Now,
                //    Message = string.Empty,
                //    Checked = false,
                //    Passed = false

                //};
                //taxDeclaration.DeclarationCheck = declarationCheckModel;
                //taxDeclaration.CitizenInformation = citizen;
                //taxDeclaration.DateOfFilling = DateTime.Now;

                //db.TaxDeclarations.Add(taxDeclaration);
                //await db.SaveChangesAsync();

                //taxDeclaration.DeclarationCheck.DeclarationId = taxDeclaration.Id;
                //db.Entry(taxDeclaration.DeclarationCheck).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            //SelectList list1 = new SelectList((IEnumerable<DeclarationType>)Enum.GetValues(typeof(DeclarationType)));
            //SelectList list2 = new SelectList((IEnumerable<PayerCaterogy>)Enum.GetValues(typeof(PayerCaterogy)));
            //SelectList list3 = GetInspectorsList();
            ViewBag.DeclarationTypeItems = reportManager.DeclarationTypes;
            ViewBag.PayerCaterogyItems = reportManager.PayerCategories;
            ViewBag.Inspectors = reportManager.GetInspectorsList(db);
            ViewBag.ReportNumber = taxDeclaration.UniqueDeclarationId;

            return View(taxDeclaration);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
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

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var taxDeclaration = await db.TaxDeclarations.Where(x => x.Id == id)
                                  .Include(x => x.DeclarationCheck)
                                  .SingleOrDefaultAsync();

            if (taxDeclaration.DeclarationCheck.Checked == true && taxDeclaration.DeclarationCheck.Passed == true)
            {
                return View("DeleteCanceledPermission");
            }

            return View(taxDeclaration);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id, string UniqueDeclarationId)
        {
            //var taxDeclaration = await db.TaxDeclarations.Where(x => x.Id == Id && x.UniqueDeclarationId == UniqueDeclarationId)
            //                      .Include(x => x.TaxDeclarationDetail)
            //                      .Include(x => x.TaxDeclarationDetail.Income)
            //                      .Include(x => x.TaxDeclarationDetail.Tax)
            //                      .Include(x => x.DeclarationCheck)
            //                      .Include(x => x.DeclarationCheck.Inspector)
            //                      .Include(x => x.CitizenInformation)
            //                      .Include(x => x.CitizenInformation.CitizenInformationDetail)
            //                      .SingleOrDefaultAsync();

            //db.Taxes.Remove(taxDeclaration.TaxDeclarationDetail.Tax);
            //db.Incomes.Remove(taxDeclaration.TaxDeclarationDetail.Income);
            //db.TaxDeclarationDetails.Remove(taxDeclaration.TaxDeclarationDetail);
            //db.DeclarationChecks.Remove(taxDeclaration.DeclarationCheck);
            //db.TaxDeclarations.Remove(taxDeclaration);

            //await db.SaveChangesAsync();

            await reportManager.DeleteDeclarationByIdAndUniqueIdAsync(db, Id, UniqueDeclarationId);

            return View("DeleteSucceded");
        }

        [HttpGet]
        public async Task<ActionResult> EditReport(int id)
        {
            //var taxDeclaration = await db.TaxDeclarations.Where(x => x.Id == id)
            //                                  .Include(x => x.TaxDeclarationDetail)
            //                                  .Include(x => x.TaxDeclarationDetail.Income)
            //                                  .Include(x => x.TaxDeclarationDetail.Tax)
            //                                  .Include(x => x.DeclarationCheck)
            //                                  .Include(x => x.DeclarationCheck.Inspector)
            //                                  .Include(x => x.CitizenInformation)
            //                                  .Include(x => x.CitizenInformation.CitizenInformationDetail)
            //                                  .SingleOrDefaultAsync();

            var taxDeclaration = await reportManager.GetDeclarationByIdAsync(db, id);

            if (taxDeclaration.DeclarationCheck.Checked == true && taxDeclaration.DeclarationCheck.Passed == true)
            {
                return View("EditCanceledPermission");
            }

            ViewBag.Inspectors = reportManager.GetInspectorsList(db);


            return View(taxDeclaration);
        }

        [HttpPost]
        public async Task<ActionResult> EditReport(TaxDeclarationModel taxDeclaration, string number)
        {
            var inspector = await reportManager.GetInspectorBySpecialNumberAsync(db, number);

            if (ModelState.IsValid)
            {
                //var declarationCheck = await db.DeclarationChecks.Where(x => x.DeclarationId == taxDeclaration.Id).SingleOrDefaultAsync();
                //taxDeclaration.DeclarationCheck = declarationCheck;
                //taxDeclaration.DeclarationCheck.Inspector = inspector;
                //taxDeclaration.DeclarationCheck.Inspector.Id = inspector.Id;
                //taxDeclaration.DeclarationCheck.DateOfStart = DateTime.Now;
                //taxDeclaration.DeclarationCheck.Checked = false;
                //taxDeclaration.DeclarationCheck.Passed = false;

                //db.Entry(taxDeclaration).State = EntityState.Modified;
                //db.Entry(taxDeclaration.TaxDeclarationDetail).State = EntityState.Modified;
                //db.Entry(taxDeclaration.TaxDeclarationDetail).State = EntityState.Modified;
                //db.Entry(taxDeclaration.TaxDeclarationDetail.Income).State = EntityState.Modified;
                //db.Entry(taxDeclaration.TaxDeclarationDetail.Tax).State = EntityState.Modified;
                //db.Entry(taxDeclaration.DeclarationCheck.Inspector).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                await reportManager.ConfirmEditAsync(db, taxDeclaration, inspector);

                return RedirectToAction("Index");
            }

            ViewBag.Inspectors = reportManager.GetInspectorsList(db);
            return View(taxDeclaration);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Rules()
        {
            return View();
        }

        //private SelectList GetInspectorsList()
        //{
        //    var inspectors = db.Inspectors.Where(x => x.Name != "Default").ToList();

        //    //          new SelectList(
        //    //          new List<SelectListItem>
        //    //          {
        //    //              new SelectListItem { Text = "Homeowner", Value = ((int)UserType.Homeowner).ToString()},
        //    //              new SelectListItem { Text = "Contractor", Value = ((int)UserType.Contractor).ToString()},
        //    //          }, "Value", "Text");

        //    //List<SelectListItem> listItems = new List<SelectListItem>();
        //    //foreach (var inspector in inspectors)
        //    //{
        //    //    string text = inspector.Surname + " " + inspector.Name + " " + inspector.Patronymic;
        //    //    listItems.Add(new SelectListItem { Text = text, Value = inspector.SpecialNumber });
        //    //}

        //    SelectList listItems = new SelectList(inspectors, "SpecialNumber", "FullName");

        //    return listItems;
        //}

        //private async Task<InspectorModel> GetInspectorBySpecialNumber(string number)
        //{
        //    return await db.Inspectors.Where(x => x.SpecialNumber == number).SingleOrDefaultAsync();
        //}
    }
}