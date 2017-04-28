using Misaar.Concrete;
using Misaar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Misaar.Controllers
{
    public class DiscountController : Controller
    {
        UnitOfWork unitOfWork;
        public DiscountController()
        {
            unitOfWork = new UnitOfWork();
        }
        // GET: Discount
        public async Task<ActionResult> List()
        {
            IEnumerable<Product> products = await unitOfWork.Products.GetAll();
            IEnumerable<Product> productsDiscount = products.Where(p => p.Discount != null);
            foreach(var product in productsDiscount)
            {
                if (product.Price != null && product.Discount != null)
                    product.DiscountedPrice = product.Price - (product.Price * product.Discount)/100;
            }
            IEnumerable<SeoData> tags = await unitOfWork.SeoTags.GetAll();
            tags = tags.Where(t => t.Category == "Скидки");
            string keyWords = "";
            string metaDesc = "";
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    keyWords += tag.KeyWords;
                    metaDesc += tag.MetaDescription;
                }
            }
            AllFilesModel model = new AllFilesModel
            {
                Products = productsDiscount
            };
            ViewBag.Title = "Скидки";
            ViewBag.KeyWords = keyWords;
            ViewBag.MetaDescription = metaDesc;
            return View(model);
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