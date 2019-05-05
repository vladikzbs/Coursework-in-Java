using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace Coursework_in_Java.Models.Inspectors
{
    public class InspectorModel
    {
        public InspectorModel()
        {

        }

        public InspectorModel(string name, string surname, string patronymic, byte[] photo)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            SpecialNumber = Guid.NewGuid().ToString();
            Photo = new PhotoModel { Photo = photo };
        }

        public int Id { get; set; }

        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Display(Name = "Фамілія")]
        public string Surname { get; set; }

        [Display(Name = "Прізвище")]
        public string Patronymic { get; set; }

        [Display(Name = "Номер інспектора")]
        public string SpecialNumber { get; set; }

        public PhotoModel Photo { get; set; }

        //[Display(Name = "Фото інспектора")]
        //public byte[] Photo { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return Surname + " " + Name + " " + Patronymic;
            }
        }

        //public override string ToString()
        //{
        //    return new StringBuilder().Append(Surname)
        //                              .Append(" ")
        //                              .Append(Name)
        //                              .Append(" ")
        //                              .Append(Patronymic)
        //                              .ToString();
        //}
    }
}