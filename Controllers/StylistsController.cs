using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        private readonly HairSalonContext _db;
        public StylistsController(HairSalonContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Stylist> model = _db.Stylists.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Stylist stylist)
        {
            _db.Stylists.Add(stylist);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Stylist thisStylist = _db.Stylists.Include(stylist => stylist.Clients).FirstOrDefault(stylists => stylists.StylistId == id);
            return View(thisStylist);
        }

        public ActionResult Edit(int id)
        {
            Stylist thisStylist = _db.Stylists.Include(stylist => stylist.Clients).FirstOrDefault(stylists => stylists.StylistId == id);
            ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name", "Specialty");
            return View(thisStylist);
        }

        [HttpPost]
        public ActionResult Edit(Stylist stylist)
        {
            _db.Entry(stylist).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Stylist thisStylist = _db.Stylists.FirstOrDefault(Stylist => Stylist.StylistId == id);
            return View(thisStylist);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Stylist thisStylist = _db.Stylists.FirstOrDefault(Stylist => Stylist.StylistId == id);
            _db.Stylists.Remove(thisStylist);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}