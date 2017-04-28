using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Misaar.Models;
using Misaar.Concrete;

namespace Misaar.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        UnitOfWork unitOfWork;
        public ProductsController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Admin
        public async Task<ActionResult> Index(string category)
        {
            IEnumerable<Product> products = await unitOfWork.Products.GetAll();
            if (!String.IsNullOrEmpty(category) && !category.Equals("Все"))
            {
                products = products.Where(p => p.Category.Name == category);
            }
            
            List<Category> categories = (List<Category>)await unitOfWork.Categories.GetAll();
            string[] categNames = new string[categories.Count()+1];
            categNames[0] = "Все";
            for(int i = 1; i < categNames.Length; i++)
            {
                categNames[i] = categories[i - 1].Name;
            }

            ProductsListViewModel listModel = new ProductsListViewModel()
            {
                Products = products.ToList(),
                Categories = new SelectList(categNames)             
            };
            return View(listModel);
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await unitOfWork.Products.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Create
        public async Task<ActionResult> Create()
        {
            IEnumerable<Category> categories = await unitOfWork.Categories.GetAll();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.Presence = new SelectList(new string[] { "В наличии", "Под заказ" });
            ViewBag.Material = new SelectList(new string[] { "Сосна, ель", "Ольха", "Ясень", "Дуб" });
            ViewBag.Subcategories = new SelectList(new string[] { "Необрезная", "Обрезная" });
            ViewBag.Sort = new SelectList(new string[] { "1,2", "3,4", "1", "2", "3" });
            return View();
        }

        // POST: Admin/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Category,Subcategory,Price,Material,Sort,IsTopSale,DaysToWork,IsHide,Discount,Size,Length,Presence,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {                       
                unitOfWork.Products.Create(product);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await unitOfWork.Products.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            
            List<Category> categories = (List<Category>)await unitOfWork.Categories.GetAll();            
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);            
            ViewBag.Material = new SelectList(new string[] { "Сосна, ель", "Ольха", "Ясень", "Дуб" }, product.Material);
            ViewBag.Sort = new SelectList(new string[] { "1,2", "3,4", "1", "2", "3" }, product.Sort);
            ViewBag.Subcategories = new SelectList(new string[] { "Необрезная", "Обрезная" }, product.Subcategory);
            ViewBag.Presence = new SelectList(new string[] { "В наличии", "Под заказ" }, product.Presence);
            return View(product);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Category,Subcategory,Price,Material,Sort,IsTopSale,DaysToWork,IsHide,Discount,Size,Length,Presence,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {                
                unitOfWork.Products.Update(product);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }

       
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await unitOfWork.Products.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            unitOfWork.Products.Delete(id);
            await unitOfWork.Save();
            return RedirectToAction("Index");
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
