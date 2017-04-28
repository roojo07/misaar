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
using System.Data.Entity.Infrastructure;

namespace Misaar.Controllers
{
    public class ArticlesController : Controller
    {
        UnitOfWork unitOfWork;
        public ArticlesController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Articles
        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.Articles.GetAll());
        }

        // GET: Articles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await unitOfWork.Articles.Get(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Body,Category,Align")] Article article, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new Picture
                    {
                        PictureName = System.IO.Path.GetFileName(upload.FileName),
                        PictureType = PictureType.Main,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    article.Pictures = new List<Picture> { avatar };
                }
                unitOfWork.Articles.Create(article);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await unitOfWork.Articles.Get(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

       
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = await unitOfWork.Articles.Get(id);
            if (TryUpdateModel(article, "",
                new string[] { "Name", "Body", "Category", "Align" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (article.Pictures.Any(f => f.PictureType == PictureType.Main))
                        {
                            unitOfWork.Pictures.Delete(article.Pictures.First(f => f.PictureType == PictureType.Main));
                        }
                        var avatar = new Picture
                        {
                            PictureName = System.IO.Path.GetFileName(upload.FileName),
                            PictureType = PictureType.Main,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        article.Pictures = new List<Picture> { avatar };
                    }
                    unitOfWork.Articles.Update(article);
                    await unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Невозможно сохранить изменения.");
                }

            }
            return View(article);
        }
    

        // GET: Articles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await unitOfWork.Articles.Get(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            unitOfWork.Articles.Delete(id);
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
