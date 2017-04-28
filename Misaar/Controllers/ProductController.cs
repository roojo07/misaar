using Misaar.Concrete;
using Misaar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Misaar.Controllers
{
    //[OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
    public class ProductController : Controller
    {
        UnitOfWork unitOfWork;
        public ProductController()
        {
            unitOfWork = new UnitOfWork();
        }        

        public async Task<ActionResult> GetItem(int id)
        {
            Category category = await unitOfWork.Categories.Get(id);
            
            if (category.Products.Count > 0)
            {
                List<double?> prices = new List<double?>();
                foreach (var item in category.Products)
                {
                    prices.Add(item.Price);
                }

                double? minPrice = prices.Min();
                if(minPrice != null)
                {
                    ViewBag.MinPrice = minPrice;
                }                
            }
            
            return View(category);
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}