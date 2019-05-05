using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Coursework_in_Java.AppKernel.Managers
{
    public class CitizenManager
    {
        private static CitizenManager @this;

        protected CitizenManager()
        {

        }

        public static CitizenManager Instance()
        {
            return @this ?? (@this = new CitizenManager());
        }

        public async Task<CitizenInformationModel> GetCitizenByTaxIdAsync(ApplicationDbContext db, string taxId)
        {
            var user = await db.CitizenInformation.Where(x => x.CitizenInformationDetail.TaxCardNumber == taxId)
                                .Include(x => x.CitizenInformationDetail)
                                .Include(x => x.CitizenInformationDetail.Address)
                                .Include(x => x.CitizenInformationDetail.Phone)
                                .SingleOrDefaultAsync();

            return user;
        }

        public async Task<CitizenInformationModel> GetCitizenByTaxIdAndUserIdAsync(ApplicationDbContext db, int userId, string taxId)
        {
            var user = await db.CitizenInformation.Where(x => x.Id == userId && x.CitizenInformationDetail.TaxCardNumber == taxId)
                                                  .Include(x => x.CitizenInformationDetail)
                                                  .Include(x => x.CitizenInformationDetail.Address)
                                                  .Include(x => x.CitizenInformationDetail.Phone)
                                                  .SingleOrDefaultAsync();


            return user;
        }
    }
}