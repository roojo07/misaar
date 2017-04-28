using Misaar.Concrete;
using Misaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misaar.Controllers
{
    public class PictureController : Controller
    {
        UnitOfWork unitOfWork;
        public PictureController()
        {
            unitOfWork = new UnitOfWork();
        }
        public ActionResult Index(int id)
        {
            var fileToRetrieve = unitOfWork.Pictures.Get(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}