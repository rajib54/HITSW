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
    public class AddrBkPhysicalActivityController : Controller
    {
        private HITSWContext db = new HITSWContext();

        //
        // GET: /AddrBkPhysicalActivity/

        public ActionResult Index(Guid organizationId, String orgName, String searchTerm = null, int page = 1)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkPhysicalActivity + " / " + orgName;

            var model = db.AddrBk_PhysicalActivity.Include(a => a.Lookup_AddrBk).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrBk.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_Index", model);
        }

        //
        // GET: /AddrBkPhysicalActivity/Create

        public ActionResult Create(Guid organizationId, String orgName)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkPhysicalActivity + " / " + orgName;
            ViewBag.PhyActiv_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_PhysicalActivity), "Id", "Title");

            AddrBk_PhysicalActivity addrbk_physicalactivity = new AddrBk_PhysicalActivity();
            addrbk_physicalactivity.EffDt = DateTime.Now;
            return PartialView("_Create", addrbk_physicalactivity);
        }

        //
        // POST: /AddrBkPhysicalActivity/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_PhysicalActivity addrbk_physicalactivity, Guid organizationId, String orgName)
        {
            try
            {
                addrbk_physicalactivity.Id = Guid.NewGuid();
                addrbk_physicalactivity.CreatedDt = addrbk_physicalactivity.LastUpdatedDt = DateTime.Now;
                addrbk_physicalactivity.ActiveRec = true;
                addrbk_physicalactivity.IndivID = organizationId;

                db.AddrBk_PhysicalActivity.Add(addrbk_physicalactivity);
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkPhysicalActivity + " / " + orgName;
            ViewBag.PhyActiv_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_PhysicalActivity), "Id", "Title", addrbk_physicalactivity.PhyActiv_LCID);
            return PartialView("_Create", addrbk_physicalactivity);
        }

        //
        // GET: /AddrBkPhysicalActivity/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName)
        {
            AddrBk_PhysicalActivity addrbk_physicalactivity = db.AddrBk_PhysicalActivity.Find(id);
            if (addrbk_physicalactivity == null)
            {
                return HttpNotFound();
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkPhysicalActivity + " / " + orgName + " / " + Utils.GetPhysicalActivityFromId(addrbk_physicalactivity.PhyActiv_LCID);
            ViewBag.PhyActiv_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_PhysicalActivity), "Id", "Title", addrbk_physicalactivity.PhyActiv_LCID);
            return PartialView("_Edit", addrbk_physicalactivity);
        }

        //
        // POST: /AddrBkPhysicalActivity/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_PhysicalActivity addrbk_physicalactivity, Guid organizationId, String orgName)
        {
            try
            {
                addrbk_physicalactivity.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_physicalactivity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_PhysicalActivity)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_physicalactivity.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkPhysicalActivity + " / " + orgName + " / " + Utils.GetPhysicalActivityFromId(addrbk_physicalactivity.PhyActiv_LCID);
            ViewBag.PhyActiv_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_PhysicalActivity), "Id", "Title", addrbk_physicalactivity.PhyActiv_LCID);
            return PartialView("_Edit", addrbk_physicalactivity);
        }

        //
        // GET: /AddrBkPhysicalActivity/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName)
        {
            AddrBk_PhysicalActivity addrbk_physicalactivity = db.AddrBk_PhysicalActivity.Find(id);
            if (addrbk_physicalactivity == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_PhysicalActivity.Remove(addrbk_physicalactivity);
            db.SaveChanges();

            return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}