using Misaar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misaar.Models
{
    public class Article 
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Содержимое статьи")]
        [DataType(DataType.MultilineText)] 
        [UIHint("tinymce_full")]
        [AllowHtml]       
        public string Body { get; set; }

        [Required]
        [Display(Name ="Тип статьи")]
        public string Category { get; set; }

        [Display(Name = "Расположение картинки")]
        public string Align { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}