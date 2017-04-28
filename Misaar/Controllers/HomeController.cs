using Misaar.Concrete;
using Misaar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Misaar.Controllers
{
    //[OutputCache(Duration = 180)]    
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork;
        public HomeController()
        {
            unitOfWork = new UnitOfWork();
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Product> products = await unitOfWork.Products.GetAll();           
            IEnumerable<Article> articles = await unitOfWork.Articles.GetAll("Главная");
            IEnumerable<Category> categories = await unitOfWork.Categories.GetAll();
            IEnumerable<SeoData> tags = await unitOfWork.SeoTags.GetAll();
            tags = tags.Where(t => t.Category == "Главная");
            string keyWords="";
            string metaDesc="";
            if(tags != null)
            {
                foreach(var tag in tags)
                {
                    keyWords += tag.KeyWords;
                    metaDesc += tag.MetaDescription;
                }
            }
            AllFilesModel myModel = new AllFilesModel
            {                
                Articles = articles,
                Products = products,
                Categories = categories
            };
            ViewBag.Title = "Пиломатериалы в Беларуси";
            ViewBag.KeyWords = keyWords;
            ViewBag.MetaDescription = metaDesc;
            return View(myModel);
        }

        public async Task<ActionResult> About()
        {
            IEnumerable<Article> articles = await unitOfWork.Articles.GetAll("О нас");
            IEnumerable<SeoData> tags = await unitOfWork.SeoTags.GetAll();
            tags = tags.Where(t => t.Category == "О нас");
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
            AllFilesModel myModel = new AllFilesModel
            {
                Articles = articles
            };
            ViewBag.Title = "О нас";
            ViewBag.KeyWords = keyWords;
            ViewBag.MetaDescription = metaDesc;
            return View(myModel);
        }

        public ActionResult Contact()
        {
            IEnumerable<Phone> phones = unitOfWork.Phones.GetAllSync();
            IEnumerable<Contact> contacts = unitOfWork.Contacts.GetAllSync();
            ContactViewModel model = new ContactViewModel
            {
                Phones = phones,
                Contacts = contacts
            };
            ViewBag.Title = "Контакты";
            return View(model);
        }

        public async Task<ActionResult> Delivery()
        {
            IEnumerable<Article> articles = await unitOfWork.Articles.GetAll("Доставка");
            IEnumerable<SeoData> tags = await unitOfWork.SeoTags.GetAll();
            tags = tags.Where(t => t.Category == "Доставка");
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
            AllFilesModel myModel = new AllFilesModel
            {
                Articles = articles
            };
            ViewBag.Title = "Доставка";
            ViewBag.KeyWords = keyWords;
            ViewBag.MetaDescription = metaDesc;
            return View(myModel);
        }

       
        [HttpGet]
        public ActionResult Feedback()
        {
            FeedbackForm temp = new FeedbackForm();
            temp.Message = "";
            ViewBag.Title = "Напишите нам";
            return View(temp);            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Feedback(FeedbackForm model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Имя: {0}</p><p>Email: {1}</p><p>Номер телефона: {2}</p><p>Сообщение:</p><p>{3}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("andreybashak@rambler.ru"));  
                message.From = new MailAddress("roojo123@gmail.com"); 
                message.Subject = "Вам написали на сайте";
                message.Body = string.Format(body, model.Name, model.Email, model.Phone, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "roojo123@gmail.com", 
                        Password = "myPass1" 
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

       
        [HttpPost]        
        public async Task<ActionResult> SendPhone(string phone)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>На сайте МИСААР просят перезвонить.</p><p>Номер телефона: {0}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("andreybashak@rambler.ru"));
                message.From = new MailAddress("roojo123@gmail.com");
                message.Subject = "Вам написали на сайте";
                message.Body = string.Format(body, phone);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "roojo123@gmail.com",
                        Password = "myPass1"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("SentPhone");
                }
            }
            return View(phone);
        }

        public ActionResult Sent()
        {
            ViewBag.Title = "Сообщение отправлено";
            return View();
        }

        public ActionResult SentPhone()
        {
            ViewBag.Title = "Номер телефона отправлен";
            return View();
        }

        public async Task<ActionResult> Cooperation()
        {
            IEnumerable<Article> articles = await unitOfWork.Articles.GetAll("Сотрудничество");
            IEnumerable<SeoData> tags = await unitOfWork.SeoTags.GetAll();
            tags = tags.Where(t => t.Category == "Доставка");
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
            AllFilesModel myModel = new AllFilesModel
            {
                Articles = articles
            };
            ViewBag.Title = "Сотрудничество";
            ViewBag.KeyWords = keyWords;
            ViewBag.MetaDescription = metaDesc;
            return View(myModel);
        }

       public ActionResult Menu()
        {
            IEnumerable<Category> categories = unitOfWork.Categories.GetAllSync();
            categories = categories.OrderBy(c => c.MenuPosition);
            AllFilesModel myModel = new AllFilesModel
            {
                Categories = categories
            };
            return View("_PartialMenu", myModel);
        }

        public ActionResult MenuMobile()
        {
            IEnumerable<Category> categories = unitOfWork.Categories.GetAllSync();
            categories = categories.OrderBy(c => c.MenuPosition);
            AllFilesModel myModel = new AllFilesModel
            {
                Categories = categories
            };
            return View("_PartialMenuMobile", myModel);
        }

        public ActionResult ShowPhones()
        {
            IEnumerable<Phone> phones = unitOfWork.Phones.GetAllSync();
            IEnumerable<Contact> contacts = unitOfWork.Contacts.GetAllSync();
            ContactViewModel model = new ContactViewModel
            {
                Phones = phones,
                Contacts = contacts                
            };

            return View("_PartialContacts", model);
        }

        public ActionResult Footer()
        {
            IEnumerable<Phone> phones = unitOfWork.Phones.GetAllSync();
            IEnumerable<Contact> contacts = unitOfWork.Contacts.GetAllSync();
            ContactViewModel model = new ContactViewModel
            {
                Phones = phones,
                Contacts = contacts
            };

            return View("_PartialFooter", model);
        }

        public ActionResult FooterMobile()
        {
            IEnumerable<Phone> phones = unitOfWork.Phones.GetAllSync();
            IEnumerable<Contact> contacts = unitOfWork.Contacts.GetAllSync();
            ContactViewModel model = new ContactViewModel
            {
                Phones = phones,
                Contacts = contacts
            };

            return View("_PartialFooterMobile", model);
        }
    }
}