using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misaar.Models
{
    public class Phone 
    {
        public int Id { get; set; }

        [Display(Name = "Номер телефона")]
        public string Number { get; set; }

        [Display(Name = "Оператор связи")]
        public string Operator { get; set; }

        [Display(Name = "Viber")]
        public bool HasViber { get; set; }

        public int ContactId { get; set; }
    }
}