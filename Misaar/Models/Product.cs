using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misaar.Models
{
    public class Product 
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }       
        
        public Category Category { get; set; }

        [Display(Name = "Подкатегория (для пиломатериалов)")]
        public string Subcategory { get; set; }

        [Display(Name="Цена (если не заполнить - \"договорная\")")]        
        public double? Price { get; set; }    
        
        [Display(Name="Материал")]
        public string Material { get; set; }    
         
        [Display(Name="Сорт")]
        public string Sort { get; set; }
       
        [Display(Name="Топ продаж")]
        public bool IsTopSale { get; set; }

        [Display(Name="Кол-во дней на работу (если под заказ)")]
        public int? DaysToWork { get; set; }

        [Display(Name="Скрыть")]
        public bool IsHide { get; set; }
        
        [Display(Name="Скидка (%)")]
        public int? Discount { get; set; }

        public double? DiscountedPrice { get; set; }

        [Display(Name ="Размеры (мм)")]
        public string Size { get; set; }

        [Display(Name ="Длина (м)")]
        public string Length { get; set; }

        [Display(Name ="В наличии")]
        public string Presence { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        
    }

   

}