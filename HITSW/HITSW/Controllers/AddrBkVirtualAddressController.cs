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
    public class AddrBkVirtualAddressController : Controller
    {
        private HITSWContext db = new HITSWContext();

        //
        // GET: /AddrBkVirtualAddress/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkOrganizationVirtualAddress + " / " + orgName;

            if (isOrganization)
            {
                var model = db.AddrBk_VirtualAddress.Include(a => a.Lookup_AddrType).Where(a => a.OrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrType.Title.Contains(searchTerm) || a.Name.Contains(searchTerm) || a.VirtualAddrDomain.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
                ViewBag.resultCount = model.Count;
                if (ViewBag.resultCount == 0)
                    ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
                return PartialView("_Index", model);
            }
            else
            {
                var model = db.AddrBk_VirtualAddress.Include(a => a.Lookup_AddrType).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrType.Title.Contains(searchTerm) || a.Name.Contains(searchTerm) || a.VirtualAddrDomain.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
                ViewBag.resultCount = model.Count;
                if (ViewBag.resultCount == 0)
                    ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
                return PartialView("_Index", model);
            }
        }

        //
        // GET: /AddrBkVirtualAddress/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationVirtualAddress + " / " + orgName;
            ViewBag.VirtualAddrType_LCID = new SelectList(db.Lookup_AddrType.Where(e => e.ActiveRec == true), "Id", "Title");

            var addrbk_virtualaddress = new AddrBk_VirtualAddress();
            addrbk_virtualaddress.EffDt = DateTime.Now;
            return PartialView("_Create", addrbk_virtualaddress);
        }

        //
        // POST: /AddrBkVirtualAddress/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_VirtualAddress addrbk_virtualaddress, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_virtualaddress.ContactBasis_LCID = Utils.GetLookUpBasisId(isOrganization);
                addrbk_virtualaddress.CreatedDt = addrbk_virtualaddress.LastUpdatedDt = DateTime.Now;
                addrbk_virtualaddress.ActiveRec = true;
                addrbk_virtualaddress.Id = Guid.NewGuid();

                if (isOrganization)
                    addrbk_virtualaddress.OrgID = organizationId;
                else
                    addrbk_virtualaddress.IndivID = organizationId;

                db.AddrBk_VirtualAddress.Add(addrbk_virtualaddress);
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
            ViewBag.MainTitle = Utils.AddrBkOrganizationVirtualAddress + " / " + orgName;
            ViewBag.VirtualAddrType_LCID = new SelectList(db.Lookup_AddrType.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_virtualaddress.VirtualAddrType_LCID);
            return PartialView("_Create", addrbk_virtualaddress);
        }

        //
        // GET: /AddrBkVirtualAddress/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_VirtualAddress addrbk_virtualaddress = db.AddrBk_VirtualAddress.Find(id);
            if (addrbk_virtualaddress == null)
            {
                return HttpNotFound();
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationVirtualAddress + " / " + orgName + " / " + Utils.GetVirtualAddressTypeFromId(addrbk_virtualaddress.VirtualAddrType_LCID);
            ViewBag.VirtualAddrType_LCID = new SelectList(db.Lookup_AddrType.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_virtualaddress.VirtualAddrType_LCID);

            return PartialView("_Edit", addrbk_virtualaddress);
        }

        //
        // POST: /AddrBkVirtualAddress/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_VirtualAddress addrbk_virtualaddress, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_virtualaddress.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_virtualaddress).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_VirtualAddress)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_virtualaddress.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationVirtualAddress + " / " + orgName + " / " + Utils.GetVirtualAddressTypeFromId(addrbk_virtualaddress.VirtualAddrType_LCID);
            ViewBag.VirtualAddrType_LCID = new SelectList(db.Lookup_AddrType.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_virtualaddress.VirtualAddrType_LCID);

            return PartialView("_Edit", addrbk_virtualaddress);
        }

        //
        // GET: /AddrBkVirtualAddress/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_VirtualAddress addrbk_virtualaddress = db.AddrBk_VirtualAddress.Find(id);
            if (addrbk_virtualaddress == null)
            {
                return HttpNotFound();
            }

            db.AddrBk_VirtualAddress.Remove(addrbk_virtualaddress);
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