using Misaar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Misaar.Models
{
    public class Category 
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [UIHint("tinymce_full")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Единица измерения")]
        public string Measure { get; set; }

        [Required]
        [Display(Name = "Позиция в меню")]
        public int MenuPosition { get; set; }

        [Display(Name = "Ключевые слова")]
        public string KeyWords { get; set; }

        [Display(Name = "Краткое описание для поисковиков")]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<File> Files { get; set; }


        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}", Id, Name).ToLower();
           
            string outPhrase = Translit(phrase);
            return outPhrase;
        }

       
        private static string Translit(string sourceText)
        {
            Dictionary<string, string> transliter = new Dictionary<string, string>();
            transliter.Add("а", "a");
            transliter.Add("б", "b");
            transliter.Add("в", "v");
            transliter.Add("г", "g");
            transliter.Add("д", "d");
            transliter.Add("е", "e");
            transliter.Add("ё", "yo");
            transliter.Add("ж", "zh");
            transliter.Add("з", "z");
            transliter.Add("и", "i");
            transliter.Add("й", "j");
            transliter.Add("к", "k");
            transliter.Add("л", "l");
            transliter.Add("м", "m");
            transliter.Add("н", "n");
            transliter.Add("о", "o");
            transliter.Add("п", "p");
            transliter.Add("р", "r");
            transliter.Add("с", "s");
            transliter.Add("т", "t");
            transliter.Add("у", "u");
            transliter.Add("ф", "f");
            transliter.Add("х", "h");
            transliter.Add("ц", "c");
            transliter.Add("ч", "ch");
            transliter.Add("ш", "sh");
            transliter.Add("щ", "sch");
            transliter.Add("ъ", "j");
            transliter.Add("ы", "y");
            transliter.Add("ь", "j");
            transliter.Add("э", "e");
            transliter.Add("ю", "yu");
            transliter.Add("я", "ya");
            transliter.Add(" ", "-");
            transliter.Add(",", "");

            StringBuilder ans = new StringBuilder();
            for (int i = 0; i < sourceText.Length; i++)
            {
                if (transliter.ContainsKey(sourceText[i].ToString()))
                {
                    ans.Append(transliter[sourceText[i].ToString()]);
                }
                else
                {
                    ans.Append(sourceText[i].ToString());
                }
            }
            return ans.ToString();
            
        }

    }
}