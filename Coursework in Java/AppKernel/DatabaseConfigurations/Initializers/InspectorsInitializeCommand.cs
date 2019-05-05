using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Inspectors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers
{
    public class InspectorsInitializeCommand : BaseCommand
    {
        public override void Execute(ApplicationDbContext db)
        {
            IReadOnlyCollection<InspectorModel> inspectors = GetInspectors();

            db.Inspectors.AddRange(inspectors);

            db.SaveChanges();
        }

        private IReadOnlyCollection<InspectorModel> GetInspectors()
        {
            List<InspectorModel> inspectors = new List<InspectorModel>();

            //inspectors.Add(new InspectorModel("Default", "Default", "Default", new byte[] { 1 }));
            inspectors.Add(new InspectorModel("Іван", "Чергов", "Олександрович", GetInspectorPhoto("inspector1")));
            inspectors.Add(new InspectorModel("Юлія", "Малишева", "Сергіївна", GetInspectorPhoto("inspector2")));
            inspectors.Add(new InspectorModel("Владислав", "Потяг", "Іванович", GetInspectorPhoto("inspector3")));
            inspectors.Add(new InspectorModel("Євгеній", "Нагорний", "Олегович", GetInspectorPhoto("inspector4")));
            inspectors.Add(new InspectorModel("Марина", "Сольникова", "Владиславівна", GetInspectorPhoto("inspector5")));
            inspectors.Add(new InspectorModel("Марк", "Уолберг", "Олександрович", GetInspectorPhoto("inspector6")));
            inspectors.Add(new InspectorModel("Олег", "Тетерич", "Петрович", GetInspectorPhoto("inspector7")));
            inspectors.Add(new InspectorModel("Вікторія", "Багрянова", "Іванівна", GetInspectorPhoto("inspector8")));
            inspectors.Add(new InspectorModel("Микола", "Стоян", "Олегович", GetInspectorPhoto("inspector9")));
            inspectors.Add(new InspectorModel("Вероніка", "Вінник", "Євгенівна", GetInspectorPhoto("inspector10")));

            return inspectors;
        }

        private byte[] GetInspectorPhoto(string value)
        {
            var path = HostingEnvironment.MapPath($"/Content/Images/InspectorsPhoto/");




            Image image = Image.FromFile(path + value + ".jpg");
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] b = memoryStream.ToArray();
            return b;
        }
    }
}