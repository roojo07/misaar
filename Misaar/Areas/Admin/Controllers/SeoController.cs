using Misaar.Concrete;
using Misaar.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Misaar.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class SeoController : Controller
    {
        UnitOfWork unitOfWork;
        public SeoController()
        {
            unitOfWork = new UnitOfWork();
        }
        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.SeoTags.GetAll());
        }
       
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeoData tag = await unitOfWork.SeoTags.Get(id);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,Category,KeyWords, MetaDescription")] SeoData tag)
        {
            if (ModelState.IsValid)
            {                
                unitOfWork.SeoTags.Create(tag);
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
            SeoData tag = await unitOfWork.SeoTags.Get(id);
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
            var tag = await unitOfWork.SeoTags.Get(id);

            if (ModelState.IsValid)
            {
                unitOfWork.SeoTags.Update(tag);
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
            SeoData tag = await unitOfWork.SeoTags.Get(id);
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
            unitOfWork.SeoTags.Delete(id);
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