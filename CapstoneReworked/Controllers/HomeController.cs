using CapstoneReworked.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneReworked.Controllers

{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult Event()
        {
            ViewBag.Message = "Event Timeline";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us";

            return View();
        }


        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                string toAddress = "mullermk521@gmail.com";
                string subject = $"Reunion Message from {model.FromName}";
                string body = $@"Contact Name: {model.FromName}
                Contact Email: {model.FromEmail}
                Message Body:
                --------------------
                {model.Body}";
                EmailSender.Send(toAddress, subject, body);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}