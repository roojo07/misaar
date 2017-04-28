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
    public class ContactsController : Controller
    {
        UnitOfWork unitOfWork;
        public ContactsController()
        {
            unitOfWork = new UnitOfWork();
        }
        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.Contacts.GetAll());
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await unitOfWork.Contacts.Get(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Country,City,Address,PostIndex,Email,StorageAddr1,StorageAddr2,StorageAddr3")] Contact contact)
        {
            contact.Phones = new List<Phone>();
            if (ModelState.IsValid)
            {               
                unitOfWork.Contacts.Create(contact);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await unitOfWork.Contacts.Get(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Country,City,Address,PostIndex,Email,StorageAddr1,StorageAddr2,StorageAddr3")] Contact contact)
        {
            if (ModelState.IsValid)
            {                
                unitOfWork.Contacts.Update(contact);
                await unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await unitOfWork.Contacts.Get(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {            
            unitOfWork.Contacts.Delete(id);
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
