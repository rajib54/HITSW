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
    public class AddrBkRelationController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private String indivFilter = "AddrBk_Relation.RelnToPrimaryIndiv_LCID";
        private String orgFilter = "AddrBk_Relation.RelnToPrimaryOrg_LCID";

        //
        // GET: /AddrBkRelation/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;

            if (isOrganization)
            {
                var model = db.AddrBk_Relation.Include(a => a.AddrBk_OrganizationUnit1).Include(a => a.Lookup_AddrBk).Where(a => a.PrimaryOrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrBk.Title.Contains(searchTerm) || a.AddrBk_OrganizationUnit1.Name.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
                ViewBag.resultCount = model.Count;
                if (ViewBag.resultCount == 0)
                    ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
                return PartialView("_IndexOrg", model);
            }
            else
            {
                var model = db.AddrBk_Relation.Include(a => a.AddrBk_Person1).Include(a => a.Lookup_GenderRelationship).Where(a => a.PrimaryIndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_GenderRelationship.Title.Contains(searchTerm) || a.AddrBk_Person1.FName.Contains(searchTerm) || a.AddrBk_Person1.LName.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
                ViewBag.resultCount = model.Count;
                if (ViewBag.resultCount == 0)
                    ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
                return PartialView("_IndexIndv", model);
            }
        }

        //
        // GET: /AddrBkRelation/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;
            ViewBag.RelatedOrgID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "Name");
            ViewBag.RelatedIndivID = new SelectList(db.AddrBk_Person.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "FName");
            ViewBag.RelnToPrimaryIndiv_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == indivFilter), "Id", "Title");
            ViewBag.RelnToPrimaryOrg_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == orgFilter), "Id", "Title");

            AddrBk_Relation addrbk_relation = new AddrBk_Relation();
            addrbk_relation.EffDt = DateTime.Now;

            if (isOrganization)
                return PartialView("_CreateOrg", addrbk_relation);
            else
                return PartialView("_CreateIndv", addrbk_relation);
        }

        //
        // POST: /AddrBkRelation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_Relation addrbk_relation, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_relation.Id = Guid.NewGuid();
                addrbk_relation.ContactBasis_LCID = Utils.GetLookUpBasisId(isOrganization);
                addrbk_relation.CreatedDt = addrbk_relation.LastUpdatedDt = DateTime.Now;
                addrbk_relation.ActiveRec = true;

                if (isOrganization)
                    addrbk_relation.PrimaryOrgID = organizationId;
                else
                    addrbk_relation.PrimaryIndivID = organizationId;

                db.AddrBk_Relation.Add(addrbk_relation);
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
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;
            ViewBag.RelatedOrgID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "Name", addrbk_relation.RelatedOrgID);
            ViewBag.RelatedIndivID = new SelectList(db.AddrBk_Person.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "FName", addrbk_relation.RelatedIndivID);
            ViewBag.RelnToPrimaryIndiv_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == indivFilter), "Id", "Title", addrbk_relation.RelnToPrimaryIndiv_LCID);
            ViewBag.RelnToPrimaryOrg_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == orgFilter), "Id", "Title", addrbk_relation.RelnToPrimaryOrg_LCID);

            if (isOrganization)
                return PartialView("_CreateOrg", addrbk_relation);
            else
                return PartialView("_CreateIndv", addrbk_relation);
        }

        //
        // GET: /AddrBkRelation/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_Relation addrbk_relation = db.AddrBk_Relation.Find(id);
            if (addrbk_relation == null)
            {
                return HttpNotFound();
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;
            ViewBag.RelatedOrgID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "Name", addrbk_relation.RelatedOrgID);
            ViewBag.RelatedIndivID = new SelectList(db.AddrBk_Person.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "FName", addrbk_relation.RelatedIndivID);
            ViewBag.RelnToPrimaryIndiv_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == indivFilter), "Id", "Title", addrbk_relation.RelnToPrimaryIndiv_LCID);
            ViewBag.RelnToPrimaryOrg_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == orgFilter), "Id", "Title", addrbk_relation.RelnToPrimaryOrg_LCID);

            if (isOrganization)
                return PartialView("_EditOrg", addrbk_relation);
            else
                return PartialView("_EditIndv", addrbk_relation);
        }

        //
        // POST: /AddrBkRelation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_Relation addrbk_relation, Guid organizationId, String orgName, bool isOrganization = true)
        {
            try
            {
                addrbk_relation.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_relation).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_Relation)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_relation.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;
            ViewBag.RelatedOrgID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "Name", addrbk_relation.RelatedOrgID);
            ViewBag.RelatedIndivID = new SelectList(db.AddrBk_Person.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "FName", addrbk_relation.RelatedIndivID);
            ViewBag.RelnToPrimaryIndiv_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == indivFilter), "Id", "Title", addrbk_relation.RelnToPrimaryIndiv_LCID);
            ViewBag.RelnToPrimaryOrg_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == orgFilter), "Id", "Title", addrbk_relation.RelnToPrimaryOrg_LCID);

            if (isOrganization)
                return PartialView("_EditOrg", addrbk_relation);
            else
                return PartialView("_EditIndv", addrbk_relation);
        }


        //
        // GET: /AddrBkRelation/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            AddrBk_Relation addrbk_relation = db.AddrBk_Relation.Find(id);
            if (addrbk_relation == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_Relation.Remove(addrbk_relation);
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