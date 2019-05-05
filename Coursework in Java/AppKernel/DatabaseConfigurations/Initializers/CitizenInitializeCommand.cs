using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers
{
    public class CitizenInitializeCommand : BaseCommand
    {
        public override void Execute(ApplicationDbContext db)
        {
            var admin = db.Users.Where(x => x.Email == "admin@java.com").SingleOrDefault();

            PhoneModel phone = new PhoneModel
            {
                MobilePhone1 = "099-123-00-00"
            };

            AddressModel address = new AddressModel
            {
                City = "Київ",
                Street = "Хрещатик",
                HouseNumber = "Дім 13",
                AppartmentNumber = "999"
            };

            CitizenInformationDetailModel citizenInformation = new CitizenInformationDetailModel
            {
                PostIndex = "232131",
                TaxCardNumber = admin.TaxIdentification,
                Phone = phone,
                Address = address
            };

            CitizenInformationModel citizen = new CitizenInformationModel
            {
                Name = "Влад",
                Surname = "Невідомий",
                Patronymic = "Анонім",
                Email = admin.Email,
                CitizenInformationDetail = citizenInformation
            };

            db.CitizenInformation.Add(citizen);
            db.SaveChanges();
        }

    }
}