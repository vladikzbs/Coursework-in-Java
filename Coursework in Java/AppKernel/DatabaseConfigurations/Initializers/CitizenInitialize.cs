using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers
{
    public class CitizenInitialize : IInitializeStrategy
    {
        public Usage UsageStatus { get; set; } = Usage.Yes;

        public void Initialize(ApplicationDbContext context)
        {
            var admin = context.Users.Where(x => x.Email == "admin@java.com").SingleOrDefault();

            PhoneModel phone = new PhoneModel
            {
                MobilePhone1 = "123-123-12-12"
            };

            AddressModel address = new AddressModel
            {
                City = "Kiyv",
                Street = "Kreshatik",
                HouseNumber = "House123",
                AppartmentNumber = "123"
            };

            CitizenInformationDetailModel citizenInformation = new CitizenInformationDetailModel
            {
                PostIndex = "PostIndex123",
                TaxCardNumber = admin.TaxIdentification,
                Phone = phone,
                Address = address
            };

            CitizenInformationModel citizen = new CitizenInformationModel
            {
                Name = "Влад",
                Surname = "Неизвестный",
                Patronymic = "Аноним",
                Email = admin.Email,
                CitizenInformationDetail = citizenInformation
            };

            context.CitizenInformation.Add(citizen);
            context.SaveChanges();
        }

    }
}