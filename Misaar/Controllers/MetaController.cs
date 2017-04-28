using Misaar.Concrete;
using Misaar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misaar.Controllers
{
    public class MetaController : Controller
    {
        UnitOfWork unitOfWork;
        public MetaController()
        {
            unitOfWork = new UnitOfWork();
        }
        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.MetaTags.GetAll());
        }
       
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetaTag tag = await unitOfWork.MetaTags.Get(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Category,KeyWords, MetaDescription")] MetaTag tag)
        {
            if (ModelState.IsValid)
            {                
                unitOfWork.MetaTags.Create(tag);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(tag);
        }
      
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetaTag tag = await unitOfWork.MetaTags.Get(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = await unitOfWork.MetaTags.Get(id);

            if (ModelState.IsValid)
            {
                unitOfWork.MetaTags.Update(tag);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(tag);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetaTag tag = await unitOfWork.MetaTags.Get(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            unitOfWork.MetaTags.Delete(id);
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