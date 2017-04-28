using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Misaar.Models;
using Misaar.Concrete;
using System.Data.Entity.Infrastructure;

namespace Misaar.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        UnitOfWork unitOfWork;
        public CategoriesController()
        {
            unitOfWork = new UnitOfWork();
        }
        // GET: Categories
        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.Categories.GetAll());
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await unitOfWork.Categories.Get(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.Measure = new SelectList(new string[] { "кв.м", "куб.м" });
            return View();
        }

        // POST: Categories/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,MenuPosition,Description,KeyWords,MetaDescription,Measure")] Category category, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    category.Files = new List<File> { avatar };
                }
                unitOfWork.Categories.Create(category);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await unitOfWork.Categories.Get(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Measure = new SelectList(new string[] { "кв.м", "куб.м" }, category.Measure);
            return View(category);
        }

        // POST: Categories/Edit/5
       
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost([Bind(Include = "Id,Name,MenuPosition,Description,KeyWords,MetaDescription,Measure")] int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await unitOfWork.Categories.Get(id);
            if (TryUpdateModel(category, "",
                new string[] { "Name", "MenuPosition", "Description", "KeyWords", "MetaDescription", "Measure" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (category.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            unitOfWork.Files.Delete(category.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        category.Files = new List<File> { avatar };
                    }
                    unitOfWork.Categories.Update(category);
                    await unitOfWork.Save();

                    Category categ = await unitOfWork.Categories.Get(id);
                    IEnumerable<Product> products = await unitOfWork.Products.GetAll();

                    Product[]productsArr = products.Where(p => p.CategoryId == id).ToArray();

                    for (int i = 0; i < productsArr.Length; i++)
                    {
                        productsArr[i].Category = categ;
                        unitOfWork.Products.Update(productsArr[i]);
                    }
                    await unitOfWork.Save();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Невозможно сохранить изменения.");
                }
                
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await unitOfWork.Categories.Get(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            unitOfWork.Categories.Delete(id);
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
