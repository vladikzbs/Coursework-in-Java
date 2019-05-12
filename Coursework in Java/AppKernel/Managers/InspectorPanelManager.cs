using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Tax;

namespace Coursework_in_Java.AppKernel.Managers
{
    public class InspectorPanelManager
    {
        /// <summary>
        /// Ссылка на единый экземпляр InspectorPanelManager
        /// </summary>
        private static InspectorPanelManager @this;

        /// <summary>
        /// Конструктор по-умолчанию (Для наследников)
        /// </summary>
        protected InspectorPanelManager() { }

        /// <summary>
        /// Метод для создания экземпляра InspectorPanelManager (Singletone)
        /// </summary>
        /// <returns></returns>
        public static InspectorPanelManager Instance()
        {
            return @this ?? (@this = new InspectorPanelManager());
        }

        /// <summary>
        /// Метод для получения коллекции налоговых отчетов на проверку по идентификатору инспектора
        /// </summary>
        /// <param name="db"></param>
        /// <param name="inspectorId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод для получения полной информации о налоговом отчете по идентификатору отчета
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод для обновления налогового отчета в бд по указаниям инспектора
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="passed"></param>
        /// <param name="message"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод для получения проверенных инспектором налоговых отчетов из бд по идентификатору инспектора
        /// </summary>
        /// <param name="db"></param>
        /// <param name="inspectorId"></param>
        /// <returns></returns>
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