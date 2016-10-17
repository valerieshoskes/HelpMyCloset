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
    public class ClothingTypesController : Controller
    {
        private ClothesDBEntities db = new ClothesDBEntities();

        // GET: ClothingTypes
        public ActionResult Index()
        {
            return View(db.ClothingTypes.ToList());
        }

        // GET: ClothingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClothingType clothingType = db.ClothingTypes.Find(id);
            if (clothingType == null)
            {
                return HttpNotFound();
            }
            return View(clothingType);
        }

        // GET: ClothingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClothingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeID,TypeName")] ClothingType clothingType)
        {
            if (ModelState.IsValid)
            {
                db.ClothingTypes.Add(clothingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clothingType);
        }

        // GET: ClothingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClothingType clothingType = db.ClothingTypes.Find(id);
            if (clothingType == null)
            {
                return HttpNotFound();
            }
            return View(clothingType);
        }

        // POST: ClothingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeID,TypeName")] ClothingType clothingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clothingType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clothingType);
        }

        // GET: ClothingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClothingType clothingType = db.ClothingTypes.Find(id);
            if (clothingType == null)
            {
                return HttpNotFound();
            }
            return View(clothingType);
        }

        // POST: ClothingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClothingType clothingType = db.ClothingTypes.Find(id);
            db.ClothingTypes.Remove(clothingType);
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
