using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Misaar.Models;
using Misaar.Concrete;

namespace Misaar.Controllers
{
    public class AdminController : Controller
    {
        UnitOfWork unitOfWork;
        public AdminController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Admin
        public async Task<ActionResult> Index(string category)
        {
            IEnumerable<Product> products = await unitOfWork.Products.GetAll();
            if (!String.IsNullOrEmpty(category) && !category.Equals("Все"))
            {
                products = products.Where(p => p.Category == category);
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
            List<Category> categories = (List<Category>)await unitOfWork.Categories.GetAll();
            string[] categNames = new string[categories.Count()];
            
            for (int i = 0; i < categNames.Length; i++)
            {
                categNames[i] = categories[i].Name;
            }
           
            ViewBag.Categories = new SelectList(categNames);
            return View();
        }

        // POST: Admin/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Category,Subcategory,Price,Material,Sort,IsTopSale,DaysToWork,IsHide,Discount,Size,Length,Presence,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                Category categ = await unitOfWork.Categories.Get(product.Category);
                product.CategoryId = categ.Id;
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
            for(int i=0; i < categories.Count; i++)
            {
                if(categories[i].Name == product.Category)
                {
                    categories.Remove(categories[i]);
                }
            }
            string[] categNames = new string[categories.Count()+1];
            categNames[0] = product.Category;
            for (int i = 1; i < categNames.Length; i++)
            {
                categNames[i] = categories[i-1].Name;
            }
            ViewBag.Categories = new SelectList(categNames);

            string[] basicPresence = new string[] { "В наличии", "Под заказ" };
            List<string> basicListPres = new List<string>();
            basicListPres.AddRange(basicPresence);
            for (int i = 0; i < basicListPres.Count; i++)
            {
                if (basicListPres[i] == product.Presence)
                {
                    basicListPres.Remove(basicListPres[i]);
                }
            }
            basicPresence[0] = product.Presence;
            for (int i = 1; i < basicPresence.Length; i++)
            {
                basicPresence[i] = basicListPres[i - 1];
            }
            ViewBag.Presence = new SelectList(basicPresence);

            string[] basicMaterial = new string[] { "Сосна, ель", "Ольха", "Ясень", "Дуб" };
            List<string> basicListMater = new List<string>();
            basicListMater.AddRange(basicMaterial);
            for (int i = 0; i < basicListMater.Count; i++)
            {
                if (basicListMater[i] == product.Material)
                {
                    basicListMater.Remove(basicListMater[i]);
                }
            }
            basicMaterial[0] = product.Material;
            for (int i = 1; i < basicMaterial.Length; i++)
            {
                basicMaterial[i] = basicListMater[i - 1];
            }
            ViewBag.Material = new SelectList(basicMaterial);

            string[] basicSort = new string[] { "1,2", "3,4", "1", "2", "3"};
            List<string> basicListSort = new List<string>();
            basicListSort.AddRange(basicSort);
            for (int i = 0; i < basicListSort.Count; i++)
            {
                if (basicListSort[i] == product.Sort)
                {
                    basicListSort.Remove(basicListSort[i]);
                }
            }
            basicSort[0] = product.Sort;
            for (int i = 1; i < basicSort.Length; i++)
            {
                basicSort[i] = basicListSort[i - 1];
            }
            ViewBag.Sort = new SelectList(basicSort);

            return View(product);
        }

        // POST: Admin/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Category,Subcategory,Price,Material,Sort,IsTopSale,DaysToWork,IsHide,Discount,Size,Length,Presence,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                Category categ = await unitOfWork.Categories.Get(product.Category);
                product.CategoryId = categ.Id;
                unitOfWork.Products.Update(product);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Delete/5
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
