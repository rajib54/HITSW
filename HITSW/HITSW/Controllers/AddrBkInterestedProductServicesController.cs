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
    public class AddrBkInterestedProductServicesController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private IPagedList<AddrBk_InterestedProductSrvcs> model;

        //
        // GET: /AddrBkInterestedProductServices/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkOrganizationInterestedProductServices + " / " + orgName;

            if(isOrganization)
                model = db.AddrBk_InterestedProductSrvcs.Include(a => a.Lookup_AddrBk).Where(a => a.OrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrBk.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            else
                model = db.AddrBk_InterestedProductSrvcs.Include(a => a.Lookup_AddrBk).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrBk.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);

            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_Index", model);
        }

        //
        // GET: /AddrBkInterestedProductServices/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationInterestedProductServices + " / " + orgName;
            ViewBag.InterestedProdSrvc_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_InterestedProductsServices), "Id", "Title");

            var addrbk_interested_productservices = new AddrBk_InterestedProductSrvcs();
            addrbk_interested_productservices.EffDt = DateTime.Now;
            return PartialView("_Create", addrbk_interested_productservices);
        }

        //
        // POST: /AddrBkInterestedProductServices/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_InterestedProductSrvcs addrbk_interested_productservices, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_interested_productservices.ContactBasis_LCID = Utils.GetLookUpBasisId(isOrganization);
                addrbk_interested_productservices.CreatedDt = addrbk_interested_productservices.LastUpdatedDt = DateTime.Now;
                addrbk_interested_productservices.ActiveRec = true;
                addrbk_interested_productservices.Id = Guid.NewGuid();

                if (isOrganization)
                    addrbk_interested_productservices.OrgID = organizationId;
                else
                    addrbk_interested_productservices.IndivID = organizationId;

                db.AddrBk_InterestedProductSrvcs.Add(addrbk_interested_productservices);
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
            ViewBag.MainTitle = Utils.AddrBkOrganizationInterestedProductServices + " / " + orgName;
            ViewBag.InterestedProdSrvc_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_InterestedProductsServices), "Id", "Title", addrbk_interested_productservices.InterestedProdSrvc_LCID);
            return PartialView("_Create", addrbk_interested_productservices);
        }

        //
        // GET: /AddrBkInterestedProductServices/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_InterestedProductSrvcs addrbk_interested_productservices = db.AddrBk_InterestedProductSrvcs.Find(id);
            if (addrbk_interested_productservices == null)
            {
                return HttpNotFound();
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationInterestedProductServices + " / " + orgName + " / " + Utils.GetInterestedProductServicesTitleFromId(addrbk_interested_productservices.InterestedProdSrvc_LCID);
            ViewBag.InterestedProdSrvc_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_InterestedProductsServices), "Id", "Title", addrbk_interested_productservices.InterestedProdSrvc_LCID);
            return PartialView("_Edit", addrbk_interested_productservices);
        }

        //
        // POST: /AddrBkInterestedProductServices/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_InterestedProductSrvcs addrbk_interested_productservices, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_interested_productservices.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_interested_productservices).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_InterestedProductSrvcs)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_interested_productservices.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationInterestedProductServices + " / " + orgName + " / " + Utils.GetInterestedProductServicesTitleFromId(addrbk_interested_productservices.InterestedProdSrvc_LCID);
            ViewBag.InterestedProdSrvc_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == Utils.AB_InterestedProductsServices), "Id", "Title", addrbk_interested_productservices.InterestedProdSrvc_LCID);
            return PartialView("_Edit", addrbk_interested_productservices);
        }

        //
        // GET: /AddrBkInterestedProductServices/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_InterestedProductSrvcs addrbk_interested_productservices = db.AddrBk_InterestedProductSrvcs.Find(id);
            if (addrbk_interested_productservices == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_InterestedProductSrvcs.Remove(addrbk_interested_productservices);
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