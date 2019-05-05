using Coursework_in_Java.AppKernel.Managers;
using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Coursework_in_Java.Controllers
{
    [Authorize(Roles = "User")]
    public class CitizenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CitizenManager citizenManager = CitizenManager.Instance();
        public string UserTId { get; }

        public CitizenController()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            UserTId = identity.Claims.Where(x => x.Type == "userTId").Select(x => x.Value).SingleOrDefault();
        }

        // GET: PersonalInfo
        public async Task<ActionResult> PersonalInfo()
        {
            //var user = await db.CitizenInformation.Where(x => x.CitizenInformationDetail.TaxCardNumber == UserTId)
            //                                .Include(x => x.CitizenInformationDetail)
            //                                .Include(x=> x.CitizenInformationDetail.Address)
            //                                .Include(x=> x.CitizenInformationDetail.Phone)
            //                                .SingleOrDefaultAsync();

            var user = await citizenManager.GetCitizenByTaxIdAsync(db, this.UserTId);

            if (user == null)
            {
                return RedirectToAction("CreatePersonalInfo");
            }

            return View(user);
        }

        // GET: EditPersonalInfo
        [HttpGet]
        public async Task<ActionResult> EditPersonalInfo(int id)
        {
            //var user = await db.CitizenInformation.Where(x => x.Id == id && x.CitizenInformationDetail.TaxCardNumber == UserTId)
            //                                      .Include(x => x.CitizenInformationDetail)
            //                                      .Include(x=> x.CitizenInformationDetail.Address)
            //                                      .Include(x=> x.CitizenInformationDetail.Phone)
            //                                      .SingleOrDefaultAsync();

            var user = await citizenManager.GetCitizenByTaxIdAndUserIdAsync(db, id, this.UserTId);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPersonalInfo(CitizenInformationModel citizen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citizen).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("PersonalInfo");
            }
            return View(citizen);
        }

        // GET: CreatePersonalInfo
        [HttpGet]
        public async Task<ActionResult> CreatePersonalInfo()
        {
            var user = await db.CitizenInformation.Where(x => x.CitizenInformationDetail.TaxCardNumber == UserTId)
                                .Include(x => x.CitizenInformationDetail)
                                .SingleOrDefaultAsync();
            if (user == null)
            {
                CitizenInformationModel citizen = new CitizenInformationModel();

                return View(citizen);
            }
            else
            {
                return RedirectToAction("PersonalInfo");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreatePersonalInfo(CitizenInformationModel citizen)
        {
            citizen.CitizenInformationDetail.TaxCardNumber = UserTId;

            if (ModelState.IsValid)
            {
                db.CitizenInformation.Add(citizen);
                await db.SaveChangesAsync();

                return RedirectToAction("PersonalInfo");
            }

            return View(citizen);
        }
    }
}