using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models
{
    public class Posts
    {
        public int Id { get; set; }
        [Required, Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required, Display(Name = "Коментар"), MaxLength(50)]
        public string Content { get; set; }
    }
}