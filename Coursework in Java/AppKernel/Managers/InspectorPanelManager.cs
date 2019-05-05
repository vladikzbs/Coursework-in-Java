using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace Coursework_in_Java.AppKernel.Managers
{
    public class InspectorPanelManager
    {
        private static InspectorPanelManager @this;

        protected InspectorPanelManager() { }

        public static InspectorPanelManager Instance()
        {
            return @this ?? (@this = new InspectorPanelManager());
        }

        public async Task<List<TaxDeclarationModel>> GetTaxDeclarationByInspectorIdAsync(ApplicationDbContext db, string inspectorId)
        {
            var taxDeclarations = await db.TaxDeclarations
                              .Include(x => x.DeclarationCheck)
                              .Include(x => x.DeclarationCheck.Inspector)
                              .Include(x => x.CitizenInformation)
                              .Where(x => x.DeclarationCheck.Inspector.SpecialNumber == inspectorId
                              && x.DeclarationCheck.Passed == false && x.DeclarationCheck.Checked == false)
                              .ToListAsync();

            return taxDeclarations;
        }

        public async Task<TaxDeclarationModel> GetTaxDeclarationsByIdAsync(ApplicationDbContext db, int id)
        {
            var taxDeclarations = await db.TaxDeclarations
                  .Include(x => x.DeclarationCheck)
                  .Include(x => x.DeclarationCheck.Inspector)
                  .Include(x => x.CitizenInformation.CitizenInformationDetail)
                  .Include(x => x.CitizenInformation.CitizenInformationDetail.Phone)
                  .Include(x => x.CitizenInformation.CitizenInformationDetail.Address)
                  .Include(x => x.CitizenInformation)
                  .Include(x => x.TaxDeclarationDetail)
                  .Include(x => x.TaxDeclarationDetail.Income)
                  .Include(x => x.TaxDeclarationDetail.Tax)
                  .Where(x => x.Id == id)
                  .ToListAsync();

            return taxDeclarations[0];
        }

        public async Task ConfirmEditAsync(ApplicationDbContext db, int id, bool passed, string message)
        {
            var declarationCheck = await db.DeclarationChecks.Where(x => x.DeclarationId == id).SingleOrDefaultAsync();

            declarationCheck.Passed = passed;
            declarationCheck.Checked = true;
            declarationCheck.Message = message;
            declarationCheck.DateOfEnd = DateTime.Now;

            db.Entry(declarationCheck).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<List<TaxDeclarationModel>> GetCheckedReportsAsync(ApplicationDbContext db, string inspectorId)
        {
            var taxDeclarations = await db.TaxDeclarations
                  .Include(x => x.DeclarationCheck)
                  .Include(x => x.DeclarationCheck.Inspector)
                  .Include(x => x.CitizenInformation)
                  .Where(x => x.DeclarationCheck.Inspector.SpecialNumber == inspectorId && x.DeclarationCheck.Checked == true)
                  .ToListAsync();

            return taxDeclarations;
        }
    }
}