using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Inspectors
{
    public class PhotoModel 
    {
        public int Id { get; set; }

        [Display(Name = "Фото інспектора")]
        public byte[] Photo { get; set; }
        public InspectorModel Inspector { get; set; }
    }
}