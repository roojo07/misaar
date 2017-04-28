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
    public class PhonesController : Controller
    {
        UnitOfWork unitOfWork;
        public PhonesController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Phones
        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.Phones.GetAll());
        }

        // GET: Phones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = await unitOfWork.Phones.Get(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phones/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Number,Operator,HasViber,ContactId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                phone.ContactId = 1;
                unitOfWork.Phones.Create(phone);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = await unitOfWork.Phones.Get(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Number,Operator,HasViber,ContactId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                phone.ContactId = 1;
                unitOfWork.Phones.Update(phone);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = await unitOfWork.Phones.Get(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            unitOfWork.Phones.Delete(id);
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
