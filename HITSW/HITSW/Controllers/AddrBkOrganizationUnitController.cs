using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HITSW.Models;
using PagedList;
using HITSW.Class;
using System.Data.Entity.Infrastructure;

namespace HITSW.Controllers
{
    public class AddrBkOrganizationUnitController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private String filter = "AddrBk_OrganizationUnit.OUType_LCID";

        //
        // GET: /AddrBkOrganizationUnit/

        public ActionResult Index(String searchTerm = null, int page = 1)
        {
            var model = db.AddrBk_OrganizationUnit.Include(a => a.Lookup_ContactType).Where(a => a.IsProspect == false && a.ActiveRec == true && (searchTerm == null || a.Name.Contains(searchTerm) || a.Lookup_ContactType.Title.Contains(searchTerm) || a.OUDesc.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            ViewBag.searchTerm = searchTerm;
            ViewBag.resultCount = model.Count;
            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkOrganizationUnit;

            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;

            if (Request.IsAjaxRequest())
                return PartialView("_Organization", model);
            return View(model);
        }

        //
        // GET: /Organization/Details/5

        public ActionResult Details(Guid id)
        {
            AddrBk_OrganizationUnit addrbk_organizationunit = db.AddrBk_OrganizationUnit.Find(id);
            if (addrbk_organizationunit == null)
            {
                return HttpNotFound();
            }
            return View(addrbk_organizationunit);
        }

        //
        // GET: /Organization/Create

        public ActionResult Create()
        {
            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkOrganizationUnit;
            ViewBag.OUType_LCID = new SelectList(db.Lookup_ContactType.Where(e => e.ActiveRec == true && e.TblColSel == filter), "Id", "Title");

            var addrbk_organizationunit = new AddrBk_OrganizationUnit();
            addrbk_organizationunit.EffDt = DateTime.Now;
            return View(addrbk_organizationunit);
        }

        //
        // POST: /Organization/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_OrganizationUnit addrbk_organizationunit)
        {
            try
            {
                addrbk_organizationunit.Id = Guid.NewGuid();
                addrbk_organizationunit.CreatedDt = addrbk_organizationunit.LastUpdatedDt = DateTime.Now;
                addrbk_organizationunit.ActiveRec = true;
                addrbk_organizationunit.IsProspect = false;
                db.AddrBk_OrganizationUnit.Add(addrbk_organizationunit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, Utils.errorMsg);

            }

            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkOrganizationUnit;
            ViewBag.OUType_LCID = new SelectList(db.Lookup_ContactType.Where(e => e.ActiveRec == true && e.TblColSel == filter), "Id", "Title", addrbk_organizationunit.OUType_LCID);
            return View(addrbk_organizationunit);
        }

        //
        // GET: /Organization/Edit/5

        public ActionResult Edit(Guid id)
        {
            AddrBk_OrganizationUnit addrbk_organizationunit = db.AddrBk_OrganizationUnit.Find(id);
            if (addrbk_organizationunit == null)
            {
                return HttpNotFound();
            }
            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkOrganizationUnit + " / " + addrbk_organizationunit.Name;
            ViewBag.OUType_LCID = new SelectList(db.Lookup_ContactType.Where(e => e.ActiveRec == true && e.TblColSel == filter), "Id", "Title", addrbk_organizationunit.OUType_LCID);
            return View(addrbk_organizationunit);
        }

        //
        // POST: /Organization/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_OrganizationUnit addrbk_organizationunit)
        {
            try
            {
                addrbk_organizationunit.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_organizationunit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_OrganizationUnit)entry.GetDatabaseValues().ToObject();
                var clientValues = (AddrBk_OrganizationUnit)entry.Entity;
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_organizationunit.Concurrency = databaseValues.Concurrency;
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, Utils.errorMsg);

            }

            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkOrganizationUnit + " / " + addrbk_organizationunit.Name;
            ViewBag.OUType_LCID = new SelectList(db.Lookup_ContactType.Where(e => e.ActiveRec == true && e.TblColSel == filter), "Id", "Title", addrbk_organizationunit.OUType_LCID);
            return View(addrbk_organizationunit);
        }

        //
        // GET: /Organization/Delete/5

        public ActionResult Delete(Guid id)
        {
            AddrBk_OrganizationUnit addrbk_organizationunit = db.AddrBk_OrganizationUnit.Find(id);
            try
            {
                if (addrbk_organizationunit == null)
                {
                    return HttpNotFound();
                }

                List<AddrBk_Relation> relations = db.AddrBk_Relation.Where(e => e.RelatedOrgID == id).ToList();
                for (int i = 0; i < relations.Count; i++)
                {
                    AddrBk_Relation addrbk_relation = relations[i];
                    db.AddrBk_Relation.Remove(addrbk_relation);
                    db.SaveChanges();
                }

                db.AddrBk_OrganizationUnit.Remove(addrbk_organizationunit);
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