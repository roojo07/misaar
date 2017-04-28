using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misaar.Models
{
    public class Contact 
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Почтовый индекс")]
        public string PostIndex { get; set; }

        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Display(Name = "Адрес склада 1")]
        public string StorageAddr1 { get; set; }

        [Display(Name = "Адрес склада 2")]
        public string StorageAddr2 { get; set; }

        [Display(Name = "Адрес склада 3")]
        public string StorageAddr3 { get; set; }       

        public virtual ICollection<Phone> Phones { get; set; }
    }
}