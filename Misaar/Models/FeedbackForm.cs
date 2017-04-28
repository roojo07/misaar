using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misaar.Models
{
    public class FeedbackForm
    {
        [Required]
        [Display(Name="Имя")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Номер телефона")]
        public string Phone { get; set; }

        [Required]
        [Display(Name ="Ваше сообщение")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}