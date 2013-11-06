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
    public class AddrBkPhoneFaxController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private IPagedList<AddrBk_PhoneFax> model;

        //
        // GET: /AddrBkPhoneFax/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkOrganizationPhoneFax + " / " + orgName;

            if(isOrganization)
                model = db.AddrBk_PhoneFax.Include(a => a.Lookup_PhoneFaxType).Where(a => a.OrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_PhoneFaxType.Title.Contains(searchTerm) || a.PhoneFaxNum.Contains(searchTerm) || a.AreaCode.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            else
                model = db.AddrBk_PhoneFax.Include(a => a.Lookup_PhoneFaxType).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_PhoneFaxType.Title.Contains(searchTerm) || a.PhoneFaxNum.Contains(searchTerm) || a.AreaCode.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);

            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_Index", model);
        }

        //
        // GET: /AddrBkPhoneFax/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationPhoneFax + " / " + orgName;
            ViewBag.PhoneFaxType_LCID = new SelectList(db.Lookup_PhoneFaxType.Where(e => e.ActiveRec == true), "Id", "Title");

            var addrbk_phonefax = new AddrBk_PhoneFax();
            addrbk_phonefax.EffDt = DateTime.Now;
            return PartialView("_Create", addrbk_phonefax);
        }

        //
        // POST: /AddrBkPhoneFax/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_PhoneFax addrbk_phonefax, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_phonefax.ContactBasis_LCID = Utils.GetLookUpBasisId(isOrganization);
                addrbk_phonefax.CreatedDt = addrbk_phonefax.LastUpdatedDt = DateTime.Now;
                addrbk_phonefax.ActiveRec = true;
                addrbk_phonefax.Id = Guid.NewGuid();

                if (isOrganization)
                    addrbk_phonefax.OrgID = organizationId;
                else
                    addrbk_phonefax.IndivID = organizationId;

                db.AddrBk_PhoneFax.Add(addrbk_phonefax);
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
            ViewBag.MainTitle = Utils.AddrBkOrganizationPhoneFax + " / " + orgName;
            ViewBag.PhoneFaxType_LCID = new SelectList(db.Lookup_PhoneFaxType.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_phonefax.PhoneFaxType_LCID);
            return PartialView("_Create", addrbk_phonefax);

        }

        //
        // GET: /AddrBkPhoneFax/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_PhoneFax addrbk_phonefax = db.AddrBk_PhoneFax.Find(id);
            if (addrbk_phonefax == null)
            {
                return HttpNotFound();
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationPhoneFax + " / " + orgName + " / " + addrbk_phonefax.PhoneFaxNum;
            ViewBag.PhoneFaxType_LCID = new SelectList(db.Lookup_PhoneFaxType.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_phonefax.PhoneFaxType_LCID);

            return PartialView("_Edit", addrbk_phonefax);
        }

        //
        // POST: /AddrBkPhoneFax/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_PhoneFax addrbk_phonefax, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_phonefax.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_phonefax).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_PhoneFax)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_phonefax.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationPhoneFax + " / " + orgName + " / " + addrbk_phonefax.PhoneFaxNum;
            ViewBag.PhoneFaxType_LCID = new SelectList(db.Lookup_PhoneFaxType.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_phonefax.PhoneFaxType_LCID);

            return PartialView("_Edit", addrbk_phonefax);
        }

        //
        // GET: /AddrBkPhoneFax/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_PhoneFax addrbk_phonefax = db.AddrBk_PhoneFax.Find(id);
            if (addrbk_phonefax == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_PhoneFax.Remove(addrbk_phonefax);
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