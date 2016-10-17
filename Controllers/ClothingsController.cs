using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpMyCloset.Models;

namespace HelpMyCloset.Controllers
{
    public class ClothingsController : Controller
    {
        private ClothesDBEntities db = new ClothesDBEntities();

        // GET: Clothings
        public ActionResult Index()
        {
            var clothings = from c in db.Clothings
                            select c;
            clothings = clothings.Include(c => c.ClothingType).Include(c => c.Season);
            return View(clothings.ToList());
        }
        // GET: Clothings/View/1

        public ActionResult View(int? id)
        {
            var clothings = from c in db.Clothings
                            select c;
            clothings = clothings.Include(c => c.ClothingType).Include(c => c.Season);
            if (id != null)
            {
                clothings = clothings.Where(c => c.ClothingType.TypeID == id);
            }
            return View(clothings.ToList());
        }
        // GET: Clothings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clothing clothing = db.Clothings.Find(id);
            if (clothing == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhotoURL = "/Content/Images/" + clothing.Photo;
            ViewBag.Season = db.Seasons.Find(clothing.SeasonID).SeasonName;
            ViewBag.ClothingType = db.ClothingTypes.Find(clothing.TypeID).TypeName;
            ViewBag.TypeID = clothing.TypeID;
            return View(clothing);
        }

        // GET: Clothings/Create
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.ClothingTypes, "TypeID", "TypeName");
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "SeasonName");
            return View();
        }

        // POST: Clothings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClothingID,Name,Photo,Color,SeasonID,Occassion,TypeID")] Clothing clothing)
        {
            if (ModelState.IsValid)
            {
                db.Clothings.Add(clothing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.ClothingTypes, "TypeID", "TypeName", clothing.TypeID);
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "SeasonName", clothing.SeasonID);
            return View(clothing);
        }

        // GET: Clothings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clothing clothing = db.Clothings.Find(id);
            if (clothing == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.ClothingTypes, "TypeID", "TypeName", clothing.TypeID);
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "SeasonName", clothing.SeasonID);
            return View(clothing);
        }

        // POST: Clothings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClothingID,Name,Photo,Color,SeasonID,Occassion,TypeID")] Clothing clothing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clothing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeID = new SelectList(db.ClothingTypes, "TypeID", "TypeName", clothing.TypeID);
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "SeasonName", clothing.SeasonID);
            return View(clothing);
        }

        // GET: Clothings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clothing clothing = db.Clothings.Find(id);
            if (clothing == null)
            {
                return HttpNotFound();
            }
            return View(clothing);
        }

        // POST: Clothings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clothing clothing = db.Clothings.Find(id);
            db.Clothings.Remove(clothing);
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
