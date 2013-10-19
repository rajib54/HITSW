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
    public class AddrBkContactAddressController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private String statusFilter = "AddrBk_Address.AddrVerifStatus_LCID";

        //
        // GET: /AddrBkContactAddress/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkContactAddress + " / " + orgName;

            if (isOrganization)
            {
                var model = db.AddrBk_ContactAddr.Include(a => a.AddrBk_Address.Lookup_AddrType).Include(a => a.AddrBk_Address.Lookup_Country).Include(a => a.AddrBk_Address.Lookup_StateProvince).Include(a => a.AddrBk_Address.Lookup_Status).Where(a => a.OrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.AddrBk_Address.Lookup_AddrType.Title.Contains(searchTerm) || a.AddrBk_Address.Title.Contains(searchTerm) || a.AddrBk_Address.CityOrTown.Contains(searchTerm) || a.AddrBk_Address.PostalCode.Contains(searchTerm) || a.AddrBk_Address.Lookup_Country.Title.Contains(searchTerm) || a.AddrBk_Address.Lookup_StateProvince.Title.Contains(searchTerm) || a.AddrBk_Address.Lookup_Status.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
                ViewBag.resultCount = model.Count;
                if (ViewBag.resultCount == 0)
                    ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
                return PartialView("_Index", model);
            }
            else
            {
                var model = db.AddrBk_ContactAddr.Include(a => a.AddrBk_Address.Lookup_AddrType).Include(a => a.AddrBk_Address.Lookup_Country).Include(a => a.AddrBk_Address.Lookup_StateProvince).Include(a => a.AddrBk_Address.Lookup_Status).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.AddrBk_Address.Lookup_AddrType.Title.Contains(searchTerm) || a.AddrBk_Address.Title.Contains(searchTerm) || a.AddrBk_Address.CityOrTown.Contains(searchTerm) || a.AddrBk_Address.PostalCode.Contains(searchTerm) || a.AddrBk_Address.Lookup_Country.Title.Contains(searchTerm) || a.AddrBk_Address.Lookup_StateProvince.Title.Contains(searchTerm) || a.AddrBk_Address.Lookup_Status.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
                ViewBag.resultCount = model.Count;
                if (ViewBag.resultCount == 0)
                    ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
                return PartialView("_Index", model);
            }
        }

        //
        // GET: /AddrBkContactAddress/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkContactAddress + " / " + orgName;
            ViewBag.State = "";

            AddrBk_ContactAddr addrbk_contactaddr = new AddrBk_ContactAddr();
            addrbk_contactaddr.EffDt = DateTime.Now;

            AddrBk_Address addrbk_address = new AddrBk_Address();
            AddrBk_ContactAddress addrbk_contactaddress = new AddrBk_ContactAddress();

            addrbk_contactaddress.AddrBk_Address = addrbk_address;
            addrbk_contactaddress.AddrBk_ContactAddr = addrbk_contactaddr;
            addrbk_contactaddress.Lookup_AddrTypes = db.Lookup_AddrType.Where(a => a.ActiveRec == true).ToList();
            addrbk_contactaddress.Lookup_Countries = db.Lookup_Country.Where(a => a.ActiveRec == true).ToList();
            addrbk_contactaddress.Lookup_VerificationStatuses = db.Lookup_Status.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter).ToList();

            return PartialView("_Create", addrbk_contactaddress);
        }

        //
        // POST: /AddrBkContactAddress/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_ContactAddress addrbk_contactaddress, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_contactaddress.AddrBk_Address.Id = Guid.NewGuid();
                addrbk_contactaddress.AddrBk_Address.CreatedDt = addrbk_contactaddress.AddrBk_Address.LastUpdatedDt = DateTime.Now;
                addrbk_contactaddress.AddrBk_Address.ActiveRec = true;
                addrbk_contactaddress.AddrBk_Address.EffDt = addrbk_contactaddress.AddrBk_ContactAddr.EffDt;

                db.AddrBk_Address.Add(addrbk_contactaddress.AddrBk_Address);
                db.SaveChanges();

                addrbk_contactaddress.AddrBk_ContactAddr.Id = Guid.NewGuid();
                addrbk_contactaddress.AddrBk_ContactAddr.CreatedDt = addrbk_contactaddress.AddrBk_ContactAddr.LastUpdatedDt = DateTime.Now;
                addrbk_contactaddress.AddrBk_ContactAddr.ActiveRec = true;
                addrbk_contactaddress.AddrBk_ContactAddr.AddrID = addrbk_contactaddress.AddrBk_Address.Id;
                addrbk_contactaddress.AddrBk_ContactAddr.ContactBasis_LCID = Utils.GetLookUpBasisId(isOrganization);

                if (isOrganization)
                    addrbk_contactaddress.AddrBk_ContactAddr.OrgID = organizationId;
                else
                    addrbk_contactaddress.AddrBk_ContactAddr.IndivID = organizationId;

                db.AddrBk_ContactAddr.Add(addrbk_contactaddress.AddrBk_ContactAddr);
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
            ViewBag.MainTitle = Utils.AddrBkContactAddress + " / " + orgName;

            addrbk_contactaddress.Lookup_AddrTypes = db.Lookup_AddrType.Where(a => a.ActiveRec == true).ToList();
            addrbk_contactaddress.Lookup_Countries = db.Lookup_Country.Where(a => a.ActiveRec == true).ToList();
            addrbk_contactaddress.Lookup_VerificationStatuses = db.Lookup_Status.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter).ToList();

            ViewBag.State = "";
            if (addrbk_contactaddress.AddrBk_Address.Cntry_LCID != null)
                ViewBag.State = db.Lookup_StateProvince.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_contactaddress.AddrBk_Address.Cntry_LCID).ToList();

            return PartialView("_Create", addrbk_contactaddress);
        }

        //
        // GET: /AddrBkContactAddress/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_ContactAddr addrbk_contactaddr = db.AddrBk_ContactAddr.Find(id);
            if (addrbk_contactaddr == null)
            {
                return HttpNotFound();
            }

            AddrBk_Address addrbk_address = db.AddrBk_Address.Find(addrbk_contactaddr.AddrID);
            AddrBk_ContactAddress addrbk_contactaddress = new AddrBk_ContactAddress();

            addrbk_contactaddress.AddrBk_Address = addrbk_address;
            addrbk_contactaddress.AddrBk_ContactAddr = addrbk_contactaddr;

            ViewBag.State = db.Lookup_StateProvince.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_contactaddress.AddrBk_Address.Cntry_LCID).ToList();
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkContactAddress + " / " + orgName + " / " + addrbk_contactaddress.AddrBk_Address.Title;

            addrbk_contactaddress.Lookup_AddrTypes = db.Lookup_AddrType.Where(a => a.ActiveRec == true).ToList();
            addrbk_contactaddress.Lookup_Countries = db.Lookup_Country.Where(a => a.ActiveRec == true).ToList();
            addrbk_contactaddress.Lookup_VerificationStatuses = db.Lookup_Status.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter).ToList();

            return PartialView("_Edit", addrbk_contactaddress);
        }

        //
        // POST: /AddrBkContactAddress/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_ContactAddress addrbk_contactaddress, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                AddrBk_Address addrbk_address = addrbk_contactaddress.AddrBk_Address;
                AddrBk_ContactAddr addrbk_contactaddr = addrbk_contactaddress.AddrBk_ContactAddr;

                addrbk_contactaddress.AddrBk_Address.LastUpdatedDt = addrbk_contactaddress.AddrBk_ContactAddr.LastUpdatedDt = DateTime.Now;

                db.Entry(addrbk_address).State = EntityState.Modified;
                db.SaveChanges();

                db.Entry(addrbk_contactaddr).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_ContactAddress)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_contactaddress.AddrBk_ContactAddr.Concurrency = databaseValues.AddrBk_ContactAddr.Concurrency;
                addrbk_contactaddress.AddrBk_Address.Concurrency = databaseValues.AddrBk_Address.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkContactAddress + " / " + orgName + " / " + addrbk_contactaddress.AddrBk_Address.Title;

            ViewBag.State = "";
            if (addrbk_contactaddress.AddrBk_Address.Cntry_LCID != null)
                ViewBag.State = db.Lookup_StateProvince.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_contactaddress.AddrBk_Address.Cntry_LCID).ToList();

            addrbk_contactaddress.Lookup_AddrTypes = db.Lookup_AddrType.Where(a => a.ActiveRec == true).ToList();
            addrbk_contactaddress.Lookup_Countries = db.Lookup_Country.Where(a => a.ActiveRec == true).ToList();
            addrbk_contactaddress.Lookup_VerificationStatuses = db.Lookup_Status.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter).ToList();

            return PartialView("_Edit", addrbk_contactaddress);
        }

        //
        // GET: /AddrBkContactAddress/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_ContactAddr addrbk_contactaddr = db.AddrBk_ContactAddr.Find(id);
            if (addrbk_contactaddr == null)
            {
                return HttpNotFound();
            }

            db.AddrBk_ContactAddr.Remove(addrbk_contactaddr);
            db.SaveChanges();

            return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadStateProvince(Guid countryId)
        {
            var stateprovinceList = db.Lookup_StateProvince.Where(a => a.ActiveRec == true && a.Cntry_LCID == countryId).ToList();
            var data = stateprovinceList.Select(m => new SelectListItem()
            {
                Text = m.Title,
                Value = m.Id.ToString(),
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}