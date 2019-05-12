using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Users;

namespace Coursework_in_Java.AppKernel.Managers
{
    public class CitizenManager
    {
        /// <summary>
        /// Ссылка на единый экземпляр CitizenManager
        /// </summary>
        private static CitizenManager @this;

        /// <summary>
        /// Конструктор по-умолчанию (Для наследников)
        /// </summary>
        protected CitizenManager()
        {

        }

        /// <summary>
        /// Метод для создания экземпляра CitizenManager (Singletone)
        /// </summary>
        /// <returns></returns>
        public static CitizenManager Instance()
        {
            return @this ?? (@this = new CitizenManager());
        }

        /// <summary>
        /// Метод для поиска пользователя по налоговому номеру
        /// </summary>
        /// <param name="db"></param>
        /// <param name="taxId"></param>
        /// <returns></returns>
        public async Task<CitizenInformationModel> GetCitizenByTaxIdAsync(ApplicationDbContext db, string taxId)
        {
            var user = await db.CitizenInformation.Where(x => x.CitizenInformationDetail.TaxCardNumber == taxId)
                                                  .Include(x => x.CitizenInformationDetail)
                                                  .Include(x => x.CitizenInformationDetail.Address)
                                                  .Include(x => x.CitizenInformationDetail.Phone)
                                                  .SingleOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Метод для поиска пользователя по налоговому номеру и его идентификатору в базе данных
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userId"></param>
        /// <param name="taxId"></param>
        /// <returns></returns>
        public async Task<CitizenInformationModel> GetCitizenByTaxIdAndUserIdAsync(ApplicationDbContext db, int userId, string taxId)
        {
            var user = await db.CitizenInformation.Where(x => x.Id == userId && x.CitizenInformationDetail.TaxCardNumber == taxId)
                               .Include(x => x.CitizenInformationDetail)
                               .Include(x => x.CitizenInformationDetail.Address)
                               .Include(x => x.CitizenInformationDetail.Phone)
                               .SingleOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Метод для редактирования персональной информации пользователя
        /// </summary>
        /// <param name="db"></param>
        /// <param name="citizen"></param>
        /// <returns></returns>
        public async Task EditPersonalInfo(ApplicationDbContext db, CitizenInformationModel citizen)
        {
            db.Entry(citizen).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Метод-предикат для поиска пользователя по налоговому номеру
        /// </summary>
        /// <param name="db"></param>
        /// <param name="taxId"></param>
        /// <returns></returns>
        public async Task<bool> IsCitizenInDbAsync(ApplicationDbContext db, string taxId)
        {
            var user = await db.CitizenInformation
                               .Include(x => x.CitizenInformationDetail)
                               .Where(x => x.CitizenInformationDetail.TaxCardNumber == taxId)
                               .SingleOrDefaultAsync();

            bool result = user != null;

            return result;
        }

        /// <summary>
        /// Метод для добавления информации о пользователе в базу данных
        /// </summary>
        /// <param name="db"></param>
        /// <param name="citizen"></param>
        /// <returns></returns>
        public async Task AddCitizenAsync(ApplicationDbContext db, CitizenInformationModel citizen)
        {
            db.CitizenInformation.Add(citizen);
            await db.SaveChangesAsync();
        }

        #region Old code

        //public async Task<CitizenInformationModel> GetCitizenByTaxIdAsync(ApplicationDbContext db, string taxId)
        //{
        //    var user = await db.CitizenInformation.Where(x => x.CitizenInformationDetail.TaxCardNumber == taxId)
        //                        .Include(x => x.CitizenInformationDetail)
        //                        .Include(x => x.CitizenInformationDetail.Address)
        //                        .Include(x => x.CitizenInformationDetail.Phone)
        //                        .SingleOrDefaultAsync();

        //    return user;
        //}

        //public async Task<CitizenInformationModel> GetCitizenByTaxIdAndUserIdAsync(ApplicationDbContext db, int userId, string taxId)
        //{
        //    var user = await db.CitizenInformation.Where(x => x.Id == userId && x.CitizenInformationDetail.TaxCardNumber == taxId)
        //                                          .Include(x => x.CitizenInformationDetail)
        //                                          .Include(x => x.CitizenInformationDetail.Address)
        //                                          .Include(x => x.CitizenInformationDetail.Phone)
        //                                          .SingleOrDefaultAsync();


        //    return user;
        //}

        #endregion    }
    }
}