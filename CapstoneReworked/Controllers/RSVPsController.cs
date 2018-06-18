using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneReworked.Models;

namespace CapstoneReworked.Controllers
{
   
    public class RSVPsController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        //RSVPs/RSVPMaybe
        public ActionResult RSVPMaybe()
        {
            return View();
        }

        //RSVPs/RSVPNo
        public ActionResult RSVPNo()
        {
            return View();
        }


        public ActionResult RSVP()
        {
            ViewBag.Message = "RSVP";

            return View();
        }

        //RSVPs/RSVPYes
        public ActionResult RSVPYes()
        {
            ViewBag.Message = "RSVP";

            return View();
        }
        


        // GET: RSVPs
        public ActionResult Index()
        {
            return View(db.RSVPs.ToList());
        }

        // GET: RSVPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            return View(rSVP);
        }

        // GET: RSVPs/Create
        public ActionResult Create(string id)
        {

            ViewBag.FoodMenuCategories = db.FoodCategories.ToDictionary(x => x, x => new MultiSelectList(x.FoodMenus, "id", "name"));
            ViewBag.AllFoodMenus = new MultiSelectList(db.FoodMenus, "id", "name");
            var invitation = new RSVPViewModel();
            invitation.Name = id;
            return View(invitation);
        }

        // POST: RSVPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RSVPViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rSVP = model.ToRSVP(db);
                db.RSVPs.Add(rSVP);
                db.SaveChanges();
                String myFood = "";
                if (model.Attending == "yes")
                {
                    foreach (int id in model.FoodMenuIDs)
                    {
                        var food = db.FoodMenus.Find(id);
                        myFood += food.name + " ";
                    }
                    string msgBody =
                    $@"Hello, 
                    {model.Name} registered for the reunion
                    Food Items: {myFood}
                    Total Guests: {model.GuestNumber}
                    Allergies: {model.Allergies}
                    Comments: {model.Comments}"
                   ;
                    string subject = "RSVP";
                    EmailSender.Send("mullermk521@gmail.com", subject, msgBody);
                    return RedirectToAction("RSVPYes", "RSVPs");

                }
                else if (model.Attending == "no")
                {
                    string msgBody =
                    $@"Hello, 
                    {model.Name} isn't attending the reunion"
                   ;
                    string subject = "RSVP";
                    EmailSender.Send("mullermk521@gmail.com", subject, msgBody);
                    return RedirectToAction("RSVPNo", "RSVPs");
                }
                else
                {
                    string msgBody =
                    $@"Hello, 
                    {model.Name} might be attending the reunion.
                    Let's follow up in a week.";
                    string subject = "RSVP";
                    EmailSender.Send("mullermk521@gmail.com", subject, msgBody);
                    return RedirectToAction("RSVPMaybe", "RSVPs");
                }
            }

            
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodMenuCategories = db.FoodCategories.ToDictionary(x => x, x => new MultiSelectList(x.FoodMenus, "id", "name"));
            ViewBag.AllFoodMenus = new MultiSelectList(db.FoodMenus, "id", "name");
            return View(model);

           
        }

        // GET: RSVPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            return View(rSVP);
        }

        // POST: RSVPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Attending,GuestNumber,FoodItems,Allergies,Comments")] RSVP rSVP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rSVP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rSVP);
        }

        // GET: RSVPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            return View(rSVP);
        }

        // POST: RSVPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RSVP rSVP = db.RSVPs.Find(id);
            db.RSVPs.Remove(rSVP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
