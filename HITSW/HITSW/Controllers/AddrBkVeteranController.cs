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
    public class AddrBkVeteranController : Controller
    {
        private HITSWContext db = new HITSWContext();

        //
        // GET: /AddrBkVeteran/

        public ActionResult Index(Guid organizationId, String orgName, String searchTerm = null, int page = 1)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkVeteran + " / " + orgName;

            var model = db.AddrBk_Veteran.Include(a => a.Lookup_AddrBk).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrBk.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_Index", model);

        }

        //
        // GET: /AddrBkVeteran/Create

        public ActionResult Create(Guid organizationId, String orgName)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkVeteran + " / " + orgName;
            ViewBag.Veteran_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_Veteran), "Id", "Title");

            AddrBk_Veteran addrbk_veteran = new AddrBk_Veteran();
            addrbk_veteran.EffDt = DateTime.Now;
            return PartialView("_Create", addrbk_veteran);
        }

        //
        // POST: /AddrBkVeteran/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_Veteran addrbk_veteran, Guid organizationId, String orgName)
        {
            try
            {
                addrbk_veteran.Id = Guid.NewGuid();
                addrbk_veteran.CreatedDt = addrbk_veteran.LastUpdatedDt = DateTime.Now;
                addrbk_veteran.ActiveRec = true;
                addrbk_veteran.IndivID = organizationId;

                db.AddrBk_Veteran.Add(addrbk_veteran);
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkVeteran + " / " + orgName;
            ViewBag.Veteran_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_Veteran), "Id", "Title", addrbk_veteran.Veteran_LCID);
            return PartialView("_Create", addrbk_veteran);
        }

        //
        // GET: /AddrBkVeteran/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName)
        {
            AddrBk_Veteran addrbk_veteran = db.AddrBk_Veteran.Find(id);
            if (addrbk_veteran == null)
            {
                return HttpNotFound();
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkVeteran + " / " + orgName + " / " + Utils.GetVeteranFromId(addrbk_veteran.Veteran_LCID);
            ViewBag.Veteran_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_Veteran), "Id", "Title", addrbk_veteran.Veteran_LCID);
            return PartialView("_Edit", addrbk_veteran);
        }

        //
        // POST: /AddrBkVeteran/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_Veteran addrbk_veteran, Guid organizationId, String orgName)
        {
            try
            {
                addrbk_veteran.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_veteran).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_Veteran)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_veteran.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkVeteran + " / " + orgName + " / " + Utils.GetVeteranFromId(addrbk_veteran.Veteran_LCID);
            ViewBag.Veteran_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_Veteran), "Id", "Title", addrbk_veteran.Veteran_LCID);
            return PartialView("_Edit", addrbk_veteran);
        }

        //
        // GET: /AddrBkVeteran/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName)
        {
            AddrBk_Veteran addrbk_veteran = db.AddrBk_Veteran.Find(id);
            if (addrbk_veteran == null)
            {
                return HttpNotFound();
            }

            db.AddrBk_Veteran.Remove(addrbk_veteran);
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