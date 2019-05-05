using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Inspectors;
using Coursework_in_Java.Models.Tax;
using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Coursework_in_Java.AppKernel.Managers
{
    public class TaxReportManager
    {
        private static TaxReportManager @this;

        protected TaxReportManager() { }

        public static TaxReportManager Instance()
        {
            return @this ?? (@this = new TaxReportManager());
        }

        public SelectList DeclarationTypes
        {
            get
            {
                return new SelectList((IEnumerable<DeclarationType>)Enum.GetValues(typeof(DeclarationType)));
            }
        }

        public SelectList PayerCategories
        {
            get
            {
                return new SelectList((IEnumerable<PayerCaterogy>)Enum.GetValues(typeof(PayerCaterogy)));
            }
        }

        public SelectList GetInspectorsList(ApplicationDbContext db)
        {
            var inspectors = db.Inspectors.Where(x => x.Name != "Default").ToList();

            SelectList listItems = new SelectList(inspectors, "SpecialNumber", "FullName");

            return listItems;
        }

        public async Task<CitizenInformationModel> GetCitizenByTaxIdAsync(ApplicationDbContext db, string taxId)
        {
            CitizenInformationModel citizen = await db.CitizenInformation
                                          .Where(x => x.CitizenInformationDetail.TaxCardNumber == taxId)
                                          .Include(x => x.CitizenInformationDetail)
                                          .Include(x => x.CitizenInformationDetail.Phone)
                                          .Include(x => x.CitizenInformationDetail.Address)
                                          .SingleOrDefaultAsync();

            return citizen;
        }

        public async Task RegisterDeclarationAsync(ApplicationDbContext db,
                                              CitizenInformationModel citizen,
                                              TaxDeclarationModel taxDeclaration,
                                              InspectorModel inspector)
        {
            DeclarationCheckModel declarationCheckModel = new DeclarationCheckModel
            {
                DeclarationId = -1,
                InspectorId = inspector.Id,
                Inspector = inspector,
                DateOfStart = DateTime.Now,
                Message = string.Empty,
                Checked = false,
                Passed = false

            };
            taxDeclaration.DeclarationCheck = declarationCheckModel;
            taxDeclaration.CitizenInformation = citizen;
            taxDeclaration.DateOfFilling = DateTime.Now;

            db.TaxDeclarations.Add(taxDeclaration);
            await db.SaveChangesAsync();

            taxDeclaration.DeclarationCheck.DeclarationId = taxDeclaration.Id;
            db.Entry(taxDeclaration.DeclarationCheck).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteDeclarationByIdAndUniqueIdAsync(ApplicationDbContext db, int dbId, string UniqueDeclarationId)
        {
            var taxDeclaration = await db.TaxDeclarations.Where(x => x.Id == dbId && x.UniqueDeclarationId == UniqueDeclarationId)
                      .Include(x => x.TaxDeclarationDetail)
                      .Include(x => x.TaxDeclarationDetail.Income)
                      .Include(x => x.TaxDeclarationDetail.Tax)
                      .Include(x => x.DeclarationCheck)
                      .Include(x => x.DeclarationCheck.Inspector)
                      .Include(x => x.CitizenInformation)
                      .Include(x => x.CitizenInformation.CitizenInformationDetail)
                      .SingleOrDefaultAsync();

            db.Taxes.Remove(taxDeclaration.TaxDeclarationDetail.Tax);
            db.Incomes.Remove(taxDeclaration.TaxDeclarationDetail.Income);
            db.TaxDeclarationDetails.Remove(taxDeclaration.TaxDeclarationDetail);
            db.DeclarationChecks.Remove(taxDeclaration.DeclarationCheck);
            db.TaxDeclarations.Remove(taxDeclaration);

            await db.SaveChangesAsync();
        }

        public async Task<TaxDeclarationModel> GetDeclarationByIdAsync(ApplicationDbContext db, int id)
        {
            var taxDeclaration = await db.TaxDeclarations.Where(x => x.Id == id)
                                              .Include(x => x.TaxDeclarationDetail)
                                              .Include(x => x.TaxDeclarationDetail.Income)
                                              .Include(x => x.TaxDeclarationDetail.Tax)
                                              .Include(x => x.DeclarationCheck)
                                              .Include(x => x.DeclarationCheck.Inspector)
                                              .Include(x => x.CitizenInformation)
                                              .Include(x => x.CitizenInformation.CitizenInformationDetail)
                                              .SingleOrDefaultAsync();

            return taxDeclaration;
        }

        public async Task<InspectorModel> GetInspectorBySpecialNumberAsync(ApplicationDbContext db, string number)
        {
            return await db.Inspectors.Where(x => x.SpecialNumber == number).SingleOrDefaultAsync();
        }

        public async Task ConfirmEditAsync(ApplicationDbContext db, TaxDeclarationModel taxDeclaration, InspectorModel inspector)
        {
            var declarationCheck = await db.DeclarationChecks.Where(x => x.DeclarationId == taxDeclaration.Id).SingleOrDefaultAsync();
            taxDeclaration.DeclarationCheck = declarationCheck;
            taxDeclaration.DeclarationCheck.Inspector = inspector;
            taxDeclaration.DeclarationCheck.Inspector.Id = inspector.Id;
            taxDeclaration.DeclarationCheck.DateOfStart = DateTime.Now;
            taxDeclaration.DeclarationCheck.Checked = false;
            taxDeclaration.DeclarationCheck.Passed = false;

            db.Entry(taxDeclaration).State = EntityState.Modified;
            db.Entry(taxDeclaration.TaxDeclarationDetail).State = EntityState.Modified;
            db.Entry(taxDeclaration.TaxDeclarationDetail).State = EntityState.Modified;
            db.Entry(taxDeclaration.TaxDeclarationDetail.Income).State = EntityState.Modified;
            db.Entry(taxDeclaration.TaxDeclarationDetail.Tax).State = EntityState.Modified;
            db.Entry(taxDeclaration.DeclarationCheck.Inspector).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}