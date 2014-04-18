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

namespace HITSW.Controllers
{
    public class LookupContactTypeController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private IPagedList<Lookup_ContactType> model;

        //
        // GET: /LookupContactType/

        public ActionResult Index(String tblcolsal, String searchTerm = null, int page = 1)
        {
            ViewBag.tblcolsal = tblcolsal;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.DataTableOrganizationType;

            if(tblcolsal.ToLower().Equals("outype"))
                model = db.Lookup_ContactType.Include(e => e.Lookup_ContactBasis).Where(e => e.TblColSel == Utils.AB_OrganizationUnitType && (searchTerm == null || e.Title.Contains(searchTerm) || e.Lookup_ContactBasis.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);

            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_Index", model);
        }

        //
        // GET: /LookupContactType/Create

        public ActionResult Create()
        {
            ViewBag.ContactBasis_LCID = new SelectList(db.Lookup_ContactBasis, "Id", "Title");
            return View();
        }

        //
        // POST: /LookupContactType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lookup_ContactType lookup_contacttype)
        {
            if (ModelState.IsValid)
            {
                lookup_contacttype.Id = Guid.NewGuid();
                db.Lookup_ContactType.Add(lookup_contacttype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactBasis_LCID = new SelectList(db.Lookup_ContactBasis, "Id", "Title", lookup_contacttype.ContactBasis_LCID);
            return View(lookup_contacttype);
        }

        //
        // GET: /LookupContactType/Edit/5

        public ActionResult Edit(Guid id)
        {
            Lookup_ContactType lookup_contacttype = db.Lookup_ContactType.Find(id);
            if (lookup_contacttype == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactBasis_LCID = new SelectList(db.Lookup_ContactBasis, "Id", "Title", lookup_contacttype.ContactBasis_LCID);
            return View(lookup_contacttype);
        }

        //
        // POST: /LookupContactType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lookup_ContactType lookup_contacttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookup_contacttype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactBasis_LCID = new SelectList(db.Lookup_ContactBasis, "Id", "Title", lookup_contacttype.ContactBasis_LCID);
            return View(lookup_contacttype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}