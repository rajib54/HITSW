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
    public class AddrBkIdentificationController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private String statusFilter = "AddrBk_Identification.IndentVerifStatus_LCID";
        private IPagedList<AddrBk_Identification> model;

        //
        // GET: /AddrBkIdentification/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkIdentification + " / " + orgName;

            if(isOrganization)
                model = db.AddrBk_Identification.Include(a => a.AddrBk_OrganizationUnit).Include(a => a.Lookup_IdentificationType).Include(a => a.Lookup_VerificationType).Include(a => a.Lookup_Status).Where(a => a.OrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.AddrBk_OrganizationUnit.Name.Contains(searchTerm) || a.Lookup_IdentificationType.Title.Contains(searchTerm) || a.Lookup_VerificationType.Title.Contains(searchTerm) || a.Lookup_Status.Title.Contains(searchTerm) || a.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            else
                model = db.AddrBk_Identification.Include(a => a.AddrBk_OrganizationUnit).Include(a => a.Lookup_IdentificationType).Include(a => a.Lookup_VerificationType).Include(a => a.Lookup_Status).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.AddrBk_OrganizationUnit.Name.Contains(searchTerm) || a.Lookup_IdentificationType.Title.Contains(searchTerm) || a.Lookup_VerificationType.Title.Contains(searchTerm) || a.Lookup_Status.Title.Contains(searchTerm) || a.Title.Contains(searchTerm) || a.LegalFirstN.Contains(searchTerm) || a.LegalMiddleN.Contains(searchTerm) || a.LegalLastN.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);

            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;

            if(isOrganization)
                return PartialView("_IndexOrg", model);
            else
                return PartialView("_IndexIndv", model);
        }

        //
        // GET: /AddrBkIdentification/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            var contactType = Utils.GetLookUpBasisId(isOrganization);
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(true);

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkIdentification + " / " + orgName;
            ViewBag.IdentificationIssuerID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId && a.ContactBasis_LCID == contactBasisId), "Id", "Name");
            ViewBag.IdentType_LCID = new SelectList(db.Lookup_IdentificationType.Where(a => a.ActiveRec == true && a.ContactType_LCID == contactType), "Id", "Title");
            ViewBag.VerificationType_LCID = new SelectList(db.Lookup_VerificationType.Where(a => a.ActiveRec == true), "Id", "Title");
            ViewBag.IndentVerifStatus_LCID = new SelectList(db.Lookup_Status.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter), "Id", "Title");

            AddrBk_Identification addrbk_identification = new AddrBk_Identification();
            addrbk_identification.EffDt = DateTime.Now;

            if (isOrganization)
                return PartialView("_CreateOrg", addrbk_identification);
            else
                return PartialView("_CreateIndv", addrbk_identification);
        }

        //
        // POST: /AddrBkIdentification/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_Identification addrbk_identification, Guid organizationId, String orgName, bool isOrganization = true)
        {
            var contactType = Utils.GetLookUpBasisId(isOrganization);
            try
            {
                if(addrbk_identification.IdentificationIssuerID == Guid.Empty || addrbk_identification.IdentificationIssuerID == null)
                    throw new Exception();

                addrbk_identification.Id = Guid.NewGuid();
                addrbk_identification.ContactBasis_LCID = contactType;
                addrbk_identification.CreatedDt = addrbk_identification.LastUpdatedDt = DateTime.Now;
                addrbk_identification.ActiveRec = true;

                if (isOrganization)
                    addrbk_identification.OrgID = organizationId;
                else
                    addrbk_identification.IndivID = organizationId;

                db.AddrBk_Identification.Add(addrbk_identification);
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            var contactBasisId = Utils.GetOrganizationLookUpBasisId(true);

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkIdentification + " / " + orgName;
            ViewBag.IdentificationIssuerID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId && a.ContactBasis_LCID == contactBasisId), "Id", "Name", addrbk_identification.IdentificationIssuerID);
            ViewBag.IdentType_LCID = new SelectList(db.Lookup_IdentificationType.Where(a => a.ActiveRec == true && a.ContactType_LCID == contactType), "Id", "Title", addrbk_identification.IdentType_LCID);
            ViewBag.VerificationType_LCID = new SelectList(db.Lookup_VerificationType.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_identification.VerificationType_LCID);
            ViewBag.IndentVerifStatus_LCID = new SelectList(db.Lookup_Status.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter), "Id", "Title", addrbk_identification.IndentVerifStatus_LCID);

            if (isOrganization)
                return PartialView("_CreateOrg", addrbk_identification);
            else
                return PartialView("_CreateIndv", addrbk_identification);
        }

        //
        // GET: /AddrBkIdentification/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            var contactType = Utils.GetLookUpBasisId(isOrganization);
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(true);

            AddrBk_Identification addrbk_identification = db.AddrBk_Identification.Find(id);
            if (addrbk_identification == null)
            {
                return HttpNotFound();
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkIdentification + " / " + orgName;
            ViewBag.IdentificationIssuerID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId && a.ContactBasis_LCID == contactBasisId), "Id", "Name", addrbk_identification.IdentificationIssuerID);
            ViewBag.IdentType_LCID = new SelectList(db.Lookup_IdentificationType.Where(a => a.ActiveRec == true && a.ContactType_LCID == contactType), "Id", "Title", addrbk_identification.IdentType_LCID);
            ViewBag.VerificationType_LCID = new SelectList(db.Lookup_VerificationType.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_identification.VerificationType_LCID);
            ViewBag.IndentVerifStatus_LCID = new SelectList(db.Lookup_Status.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter), "Id", "Title", addrbk_identification.IndentVerifStatus_LCID);

            if (isOrganization)
                return PartialView("_EditOrg", addrbk_identification);
            else
                return PartialView("_EditIndv", addrbk_identification);
        }

        //
        // POST: /AddrBkIdentification/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_Identification addrbk_identification, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                if (addrbk_identification.IdentificationIssuerID == Guid.Empty || addrbk_identification.IdentificationIssuerID == null)
                    throw new Exception();

                addrbk_identification.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_identification).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_Identification)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_identification.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            var contactType = Utils.GetLookUpBasisId(isOrganization);
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(true);

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkIdentification + " / " + orgName;
            ViewBag.IdentificationIssuerID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId && a.ContactBasis_LCID == contactBasisId), "Id", "Name", addrbk_identification.IdentificationIssuerID);
            ViewBag.IdentType_LCID = new SelectList(db.Lookup_IdentificationType.Where(a => a.ActiveRec == true && a.ContactType_LCID == contactType), "Id", "Title", addrbk_identification.IdentType_LCID);
            ViewBag.VerificationType_LCID = new SelectList(db.Lookup_VerificationType.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_identification.VerificationType_LCID);
            ViewBag.IndentVerifStatus_LCID = new SelectList(db.Lookup_Status.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter), "Id", "Title", addrbk_identification.IndentVerifStatus_LCID);

            if (isOrganization)
                return PartialView("_EditOrg", addrbk_identification);
            else
                return PartialView("_EditIndv", addrbk_identification);
        }

        //
        // GET: /AddrBkIdentification/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_Identification addrbk_identification = db.AddrBk_Identification.Find(id);
            if (addrbk_identification == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_Identification.Remove(addrbk_identification);
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