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
    public class AddrBkIndustryAffliationController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private IPagedList<AddrBk_IndustryAffliation> model;

        //
        // GET: /AddrBkIndustryAffliation/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkOrganizationIndustryAffliation + " / " + orgName;

            if(isOrganization)
                model = db.AddrBk_IndustryAffliation.Include(a => a.Lookup_IndustrySector).Where(a => a.OrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_IndustrySector.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            else
                model = db.AddrBk_IndustryAffliation.Include(a => a.Lookup_IndustrySector).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_IndustrySector.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);

            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_Index", model);
        }

        //
        // GET: /AddrBkIndustryAffliation/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationIndustryAffliation + " / " + orgName;
            ViewBag.IndustrySector_LCID = new SelectList(db.Lookup_IndustrySector.Where(a => a.ActiveRec == true), "Id", "Title");

            var addrbk_industryaffliation = new AddrBk_IndustryAffliation();
            addrbk_industryaffliation.EffDt = DateTime.Now;
            return PartialView("_Create", addrbk_industryaffliation);
        }

        //
        // POST: /AddrBkIndustryAffliation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_IndustryAffliation addrbk_industryaffliation, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_industryaffliation.ContactBasis_LCID = Utils.GetLookUpBasisId(isOrganization);
                addrbk_industryaffliation.CreatedDt = addrbk_industryaffliation.LastUpdatedDt = DateTime.Now;
                addrbk_industryaffliation.ActiveRec = true;
                addrbk_industryaffliation.Id = Guid.NewGuid();

                if (isOrganization)
                    addrbk_industryaffliation.OrgID = organizationId;
                else
                    addrbk_industryaffliation.IndivID = organizationId;

                db.AddrBk_IndustryAffliation.Add(addrbk_industryaffliation);
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationIndustryAffliation + " / " + orgName;
            ViewBag.IndustrySector_LCID = new SelectList(db.Lookup_IndustrySector.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_industryaffliation.IndustrySector_LCID);
            return PartialView("_Create", addrbk_industryaffliation);
        }

        //
        // GET: /AddrBkIndustryAffliation/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_IndustryAffliation addrbk_industryaffliation = db.AddrBk_IndustryAffliation.Find(id);
            if (addrbk_industryaffliation == null)
            {
                return HttpNotFound();
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationIndustryAffliation + " / " + orgName + " / " + Utils.GetIndustrySectorFromId(addrbk_industryaffliation.IndustrySector_LCID);
            ViewBag.IndustrySector_LCID = new SelectList(db.Lookup_IndustrySector.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_industryaffliation.IndustrySector_LCID);
            return PartialView("_Edit", addrbk_industryaffliation);
        }

        //
        // POST: /AddrBkIndustryAffliation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_IndustryAffliation addrbk_industryaffliation, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_industryaffliation.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_industryaffliation).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_IndustryAffliation)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_industryaffliation.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationIndustryAffliation + " / " + orgName + " / " + Utils.GetIndustrySectorFromId(addrbk_industryaffliation.IndustrySector_LCID);
            ViewBag.IndustrySector_LCID = new SelectList(db.Lookup_IndustrySector.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_industryaffliation.IndustrySector_LCID);
            return PartialView("_Edit", addrbk_industryaffliation);
        }

        //
        // GET: /AddrBkIndustryAffliation/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_IndustryAffliation addrbk_industryaffliation = db.AddrBk_IndustryAffliation.Find(id);
            if (addrbk_industryaffliation == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_IndustryAffliation.Remove(addrbk_industryaffliation);
            db.SaveChanges();

            return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}