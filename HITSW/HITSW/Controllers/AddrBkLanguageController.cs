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
    public class AddrBkLanguageController : Controller
    {
        private HITSWContext db = new HITSWContext();

        //
        // GET: /AddrBkLanguage/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkOrganizationLanguage + " / " + orgName;

            if (isOrganization)
            {
                var model = db.AddrBk_Language.Include(a => a.Lookup_Language).Where(a => a.OrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_Language.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
                ViewBag.resultCount = model.Count;
                if (ViewBag.resultCount == 0)
                    ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
                return PartialView("_Index", model);
            }
            else
            {
                var model = db.AddrBk_Language.Include(a => a.Lookup_Language).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_Language.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
                ViewBag.resultCount = model.Count;
                if (ViewBag.resultCount == 0)
                    ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
                return PartialView("_Index", model);
            }
        }

        //
        // GET: /AddrBkLanguage/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationLanguage + " / " + orgName;
            ViewBag.Language_LCID = new SelectList(db.Lookup_Language.Where(e => e.ActiveRec == true), "Id", "Title");

            var addrbk_language = new AddrBk_Language();
            addrbk_language.EffDt = DateTime.Now;
            return PartialView("_Create", addrbk_language);
        }

        //
        // POST: /AddrBkLanguage/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_Language addrbk_language, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_language.ContactBasis_LCID = Utils.GetLookUpBasisId(isOrganization);
                addrbk_language.CreatedDt = addrbk_language.LastUpdatedDt = DateTime.Now;
                addrbk_language.ActiveRec = true;
                addrbk_language.Id = Guid.NewGuid();

                if (isOrganization)
                    addrbk_language.OrgID = organizationId;
                else
                    addrbk_language.IndivID = organizationId;

                db.AddrBk_Language.Add(addrbk_language);
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
            ViewBag.MainTitle = Utils.AddrBkOrganizationLanguage + " / " + orgName;
            ViewBag.Language_LCID = new SelectList(db.Lookup_Language.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_language.Language_LCID);
            return PartialView("_Create", addrbk_language);
        }

        //
        // GET: /AddrBkLanguage/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_Language addrbk_language = db.AddrBk_Language.Find(id);
            if (addrbk_language == null)
            {
                return HttpNotFound();
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationLanguage + " / " + orgName + " / " + Utils.GetLanguageFromId(addrbk_language.Language_LCID);
            ViewBag.Language_LCID = new SelectList(db.Lookup_Language.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_language.Language_LCID);
            return PartialView("_Edit", addrbk_language);
        }

        //
        // POST: /AddrBkLanguage/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_Language addrbk_language, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_language.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_language).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_Language)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_language.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationLanguage + " / " + orgName + " / " + Utils.GetLanguageFromId(addrbk_language.Language_LCID);
            ViewBag.Language_LCID = new SelectList(db.Lookup_Language.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_language.Language_LCID);
            return PartialView("_Edit", addrbk_language);
        }

        //
        // GET: /AddrBkLanguage/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_Language addrbk_language = db.AddrBk_Language.Find(id);
            if (addrbk_language == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_Language.Remove(addrbk_language);
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