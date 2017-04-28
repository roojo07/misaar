using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misaar.Models
{
    public class SeoData 
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Теги для страницы")]
        public string Category { get; set; }

        [Display(Name = "Ключевые слова")]
        public string KeyWords { get; set; }

        [Display(Name = "Краткое описание для поисковиков")]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
    }
}