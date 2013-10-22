using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HITSW.Models;
using HITSW.Class;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace HITSW.Controllers
{
    public class AddrBkGeographicalGroupController : Controller
    {
        private HITSWContext db = new HITSWContext();

        //
        // GET: /AddrBkGeographicalGroup/

        public ActionResult Index(String searchTerm = null, int page = 1)
        {
            var addrbk_geographicalgroup = db.AddrBk_GeographicalGroup.Include(a => a.Lookup_GeographicalBasis).Where(a=>a.ActiveRec == true && (searchTerm == null || a.Lookup_GeographicalBasis.Title.Contains(searchTerm) || a.Title.Contains(searchTerm) || a.Description.Contains(searchTerm))).OrderByDescending(a=>a.LastUpdatedDt).ToPagedList(page,Utils.pageSize);
            ViewBag.searchTerm = searchTerm;
            ViewBag.resultCount = addrbk_geographicalgroup.Count;
            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrbkGeographicalGroup;

            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;

            if (Request.IsAjaxRequest())
                return PartialView("_Group", addrbk_geographicalgroup);
            return View(addrbk_geographicalgroup);
        }

        //
        // GET: /AddrBkGeographicalGroup/Details/5

        public ActionResult Details(Guid id)
        {
            AddrBk_GeographicalGroup addrbk_geographicalgroup = db.AddrBk_GeographicalGroup.Find(id);
            if (addrbk_geographicalgroup == null)
            {
                return HttpNotFound();
            }
            return View(addrbk_geographicalgroup);
        }

        //
        // GET: /AddrBkGeographicalGroup/Create

        public ActionResult Create()
        {
            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrbkGeographicalGroup;
            ViewBag.GeoBasis_LCID = new SelectList(db.Lookup_GeographicalBasis.Where(a=>a.ActiveRec == true), "Id", "Title");

            var addrbk_geographicalgroup = new AddrBk_GeographicalGroup();
            addrbk_geographicalgroup.EffDt = DateTime.Now;

            return View(addrbk_geographicalgroup);
        }

        //
        // POST: /AddrBkGeographicalGroup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_GeographicalGroup addrbk_geographicalgroup)
        {
            if (ModelState.IsValid)
            {
                addrbk_geographicalgroup.Id = Guid.NewGuid();
                addrbk_geographicalgroup.CreatedDt = addrbk_geographicalgroup.LastUpdatedDt = DateTime.Now;
                addrbk_geographicalgroup.ActiveRec = true;
                db.AddrBk_GeographicalGroup.Add(addrbk_geographicalgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrbkGeographicalGroup;
            ViewBag.GeoBasis_LCID = new SelectList(db.Lookup_GeographicalBasis.Where(a=>a.ActiveRec == true), "Id", "Title", addrbk_geographicalgroup.GeoBasis_LCID);
            return View(addrbk_geographicalgroup);
        }

        //
        // GET: /AddrBkGeographicalGroup/Edit/5

        public ActionResult Edit(Guid id)
        {
            AddrBk_GeographicalGroup addrbk_geographicalgroup = db.AddrBk_GeographicalGroup.Find(id);
            if (addrbk_geographicalgroup == null)
            {
                return HttpNotFound();
            }

            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrbkGeographicalGroup + " / " + addrbk_geographicalgroup.Title;
            ViewBag.GeoBasis_LCID = new SelectList(db.Lookup_GeographicalBasis.Where(a=>a.ActiveRec == true), "Id", "Title", addrbk_geographicalgroup.GeoBasis_LCID);
            return View(addrbk_geographicalgroup);
        }

        //
        // POST: /AddrBkGeographicalGroup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_GeographicalGroup addrbk_geographicalgroup)
        {
            try
            {
                addrbk_geographicalgroup.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_geographicalgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_GeographicalGroup)entry.GetDatabaseValues().ToObject();
                var clientValues = (AddrBk_GeographicalGroup)entry.Entity;
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_geographicalgroup.Concurrency = databaseValues.Concurrency;
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, Utils.errorMsg);

            }

            ViewBag.GeoBasis_LCID = new SelectList(db.Lookup_GeographicalBasis.Where(a=>a.ActiveRec == true), "Id", "Title", addrbk_geographicalgroup.GeoBasis_LCID);
            return View(addrbk_geographicalgroup);
        }

        //
        // GET: /AddrBkGeographicalGroup/Delete/5

        public ActionResult Delete(Guid id)
        {
            AddrBk_GeographicalGroup addrbk_geographicalgroup = db.AddrBk_GeographicalGroup.Find(id);
            try
            {
                if (addrbk_geographicalgroup == null)
                {
                    return HttpNotFound();
                }
                db.AddrBk_GeographicalGroup.Remove(addrbk_geographicalgroup);
                db.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, Utils.errorMsg);

            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}