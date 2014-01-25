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
        private String orgFilter = "AddrBk_Relation.RelnToPrimaryExtOrg_LCID";
        private String deptFilter = "AddrBk_Relation.RelnToExtToIntOrg_LCID";
        private String contactTypeFilter = "AddrBk_OrganizationUnit.OUType_LCID";
        private IPagedList<AddrBk_Relation> model;

        //
        // GET: /AddrBkRelation/

        public ActionResult Index(Guid organizationId, String orgName, bool isOrganization = true, String searchTerm = null, int page = 1)
        {
            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;

            if(isOrganization)
                model = db.AddrBk_Relation.Include(a => a.AddrBk_OrganizationUnit2).Include(a => a.Lookup_AddrBk1).Where(a => a.PrimaryExtOrgID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrBk.Title.Contains(searchTerm) || a.AddrBk_OrganizationUnit2.Name.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            else
                model = db.AddrBk_Relation.Include(a => a.AddrBk_Person1).Include(a => a.Lookup_GenderRelationship).Where(a => a.PrimaryIndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_GenderRelationship.Title.Contains(searchTerm) || a.AddrBk_Person1.FName.Contains(searchTerm) || a.AddrBk_Person1.LName.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);

            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;

            if(isOrganization)
                return PartialView("_IndexOrg", model);
            else
                return PartialView("_IndexIndv", model);
        }

        //
        // GET: /AddrBkRelation/Create

        public ActionResult Create(Guid organizationId, String orgName, bool isOrganization = true)
        {
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(true);

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;
            ViewBag.RelatedExtOrgID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId && a.ContactBasis_LCID == contactBasisId), "Id", "Name");
            ViewBag.RelatedIndivID = new SelectList(db.AddrBk_Person.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "FName");
            ViewBag.RelnToPrimaryIndiv_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == indivFilter), "Id", "Title");
            ViewBag.RelnToPrimaryExtOrg_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == orgFilter), "Id", "Title");

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
                if (isOrganization)
                {
                    if (addrbk_relation.RelatedExtOrgID == null || addrbk_relation.RelatedExtOrgID == Guid.Empty || addrbk_relation.RelnToPrimaryExtOrg_LCID == null || addrbk_relation.RelnToPrimaryExtOrg_LCID == Guid.Empty)
                        throw new Exception();
                }
                else
                {
                    if (addrbk_relation.RelatedIndivID == null || addrbk_relation.RelatedIndivID == Guid.Empty || addrbk_relation.RelnToPrimaryIndiv_LCID == null || addrbk_relation.RelnToPrimaryIndiv_LCID == Guid.Empty)
                        throw new Exception();
                }

                addrbk_relation.Id = Guid.NewGuid();
                if (isOrganization)
                    addrbk_relation.ContactBasis_LCID = Utils.GetOrganizationContactBasisId(organizationId);
                else
                    addrbk_relation.ContactBasis_LCID = Utils.GetLookUpBasisId(false);
                addrbk_relation.CreatedDt = addrbk_relation.LastUpdatedDt = DateTime.Now;
                addrbk_relation.ActiveRec = true;

                if (isOrganization)
                    addrbk_relation.PrimaryExtOrgID = organizationId;
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

            var contactBasisId = Utils.GetOrganizationLookUpBasisId(true);

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;
            ViewBag.RelatedExtOrgID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId && a.ContactBasis_LCID == contactBasisId), "Id", "Name", addrbk_relation.RelatedExtOrgID);
            ViewBag.RelatedIndivID = new SelectList(db.AddrBk_Person.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "FName", addrbk_relation.RelatedIndivID);
            ViewBag.RelnToPrimaryIndiv_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == indivFilter), "Id", "Title", addrbk_relation.RelnToPrimaryIndiv_LCID);
            ViewBag.RelnToPrimaryExtOrg_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == orgFilter), "Id", "Title", addrbk_relation.RelnToPrimaryExtOrg_LCID);

            if (isOrganization)
                return PartialView("_CreateOrg", addrbk_relation);
            else
                return PartialView("_CreateIndv", addrbk_relation);
        }

        //
        // GET: /AddrBkRelation/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName, bool isOrganization = true)
        {
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(true);
            AddrBk_Relation addrbk_relation = db.AddrBk_Relation.Find(id);
            if (addrbk_relation == null)
            {
                return HttpNotFound();
            }

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;
            ViewBag.RelatedExtOrgID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId && a.ContactBasis_LCID == contactBasisId), "Id", "Name", addrbk_relation.RelatedExtOrgID);
            ViewBag.RelatedIndivID = new SelectList(db.AddrBk_Person.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "FName", addrbk_relation.RelatedIndivID);
            ViewBag.RelnToPrimaryIndiv_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == indivFilter), "Id", "Title", addrbk_relation.RelnToPrimaryIndiv_LCID);
            ViewBag.RelnToPrimaryExtOrg_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == orgFilter), "Id", "Title", addrbk_relation.RelnToPrimaryExtOrg_LCID);

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
                if (isOrganization)
                {
                    if (addrbk_relation.RelatedExtOrgID == null || addrbk_relation.RelatedExtOrgID == Guid.Empty || addrbk_relation.RelnToPrimaryExtOrg_LCID == null || addrbk_relation.RelnToPrimaryExtOrg_LCID == Guid.Empty)
                        throw new Exception();
                }
                else
                {
                    if (addrbk_relation.RelatedIndivID == null || addrbk_relation.RelatedIndivID == Guid.Empty || addrbk_relation.RelnToPrimaryIndiv_LCID == null || addrbk_relation.RelnToPrimaryIndiv_LCID == Guid.Empty)
                        throw new Exception();
                }

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

            var contactBasisId = Utils.GetOrganizationLookUpBasisId(true);

            ViewBag.isOrganization = Convert.ToString(isOrganization);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkRelation + " / " + orgName;
            ViewBag.RelatedExtOrgID = new SelectList(db.AddrBk_OrganizationUnit.Where(a => a.ActiveRec == true && a.Id != organizationId && a.ContactBasis_LCID == contactBasisId), "Id", "Name", addrbk_relation.RelatedExtOrgID);
            ViewBag.RelatedIndivID = new SelectList(db.AddrBk_Person.Where(a => a.ActiveRec == true && a.Id != organizationId), "Id", "FName", addrbk_relation.RelatedIndivID);
            ViewBag.RelnToPrimaryIndiv_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == indivFilter), "Id", "Title", addrbk_relation.RelnToPrimaryIndiv_LCID);
            ViewBag.RelnToPrimaryExtOrg_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == orgFilter), "Id", "Title", addrbk_relation.RelnToPrimaryExtOrg_LCID);

            if (isOrganization)
                return PartialView("_EditOrg", addrbk_relation);
            else
                return PartialView("_EditIndv", addrbk_relation);
        }


        //
        // GET: /AddrBkRelation/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName, bool isOrganization = true, bool isDepartment = false)
        {
            AddrBk_Relation addrbk_relation = db.AddrBk_Relation.Find(id);
            AddrBk_OrganizationUnit addrbk_organization = db.AddrBk_OrganizationUnit.Find(addrbk_relation.RelatedIntOrgID1);
            if (addrbk_relation == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_Relation.Remove(addrbk_relation);
            db.SaveChanges();

            if (isDepartment)
            {
                db.AddrBk_OrganizationUnit.Remove(addrbk_organization);
                db.SaveChanges();
                return RedirectToAction("IndexDept", new { organizationId = organizationId, orgName = orgName });
            }
            else return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName, isOrganization = isOrganization });
        }

        public ActionResult IndexDept(Guid organizationId, String orgName, String searchTerm = null, int page = 1)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrbkDepartment + " / " + orgName;

            model = db.AddrBk_Relation.Include(a => a.AddrBk_OrganizationUnit3.Lookup_ContactType).Include(a => a.Lookup_AddrBk).Where(a => a.PrimaryExtOrgID1 == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrBk.Title.Contains(searchTerm) || a.AddrBk_OrganizationUnit3.Name.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);

            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;

            return PartialView("_IndexDept", model);
        }

        // GET: /AddrBkRelation/CreateDept

        public ActionResult CreateDept(Guid organizationId, String orgName)
        {
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(false);

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkDepartment + " / " + orgName;

            AddrBk_Relation addrbk_relation = new AddrBk_Relation();
            addrbk_relation.EffDt = DateTime.Now;

            AddrBk_Department addrbk_department = new AddrBk_Department();
            AddrBk_OrganizationUnit addrbk_organizationunit = new AddrBk_OrganizationUnit();

            addrbk_department.Addrbk_OrganizationUnit = addrbk_organizationunit;
            addrbk_department.Addrbk_Relation = addrbk_relation;
            addrbk_department.Lookup_AddrBks = db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == deptFilter).ToList();
            addrbk_department.Lookup_ContactTypes = db.Lookup_ContactType.Where(a => a.ActiveRec == true && a.TblColSel == contactTypeFilter && a.ContactBasis_LCID == contactBasisId).ToList();


           return PartialView("_CreateDept", addrbk_department);
        }

        //
        // POST: /AddrBkRelation/CreateDept
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDept(AddrBk_Department addrbk_department, Guid organizationId, String orgName)
        {
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(false);

            try
            {
                if (addrbk_department.Addrbk_Relation.RelnToExtToIntOrg_LCID == null || addrbk_department.Addrbk_Relation.RelnToExtToIntOrg_LCID == Guid.Empty)
                    throw new Exception();

                addrbk_department.Addrbk_OrganizationUnit.EffDt = addrbk_department.Addrbk_OrganizationUnit.LastUpdatedDt = addrbk_department.Addrbk_OrganizationUnit.CreatedDt = DateTime.Now;
                addrbk_department.Addrbk_OrganizationUnit.Id = Guid.NewGuid();
                addrbk_department.Addrbk_OrganizationUnit.ActiveRec = true;
                addrbk_department.Addrbk_OrganizationUnit.ContactBasis_LCID = contactBasisId;
                addrbk_department.Addrbk_OrganizationUnit.IsProspect = false;
                
                db.AddrBk_OrganizationUnit.Add(addrbk_department.Addrbk_OrganizationUnit);
                db.SaveChanges();
                
                addrbk_department.Addrbk_Relation.Id = Guid.NewGuid();
                addrbk_department.Addrbk_Relation.ContactBasis_LCID = contactBasisId;
                addrbk_department.Addrbk_Relation.CreatedDt = addrbk_department.Addrbk_Relation.LastUpdatedDt = DateTime.Now;
                addrbk_department.Addrbk_Relation.ActiveRec = true;
                addrbk_department.Addrbk_Relation.PrimaryExtOrgID1 = organizationId;
                addrbk_department.Addrbk_Relation.RelatedIntOrgID1 = addrbk_department.Addrbk_OrganizationUnit.Id;

                db.AddrBk_Relation.Add(addrbk_department.Addrbk_Relation);
                db.SaveChanges();

                return RedirectToAction("IndexDept", new { organizationId = organizationId, orgName = orgName });
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkDepartment + " / " + orgName;

            addrbk_department.Lookup_AddrBks = db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == deptFilter).ToList();
            addrbk_department.Lookup_ContactTypes = db.Lookup_ContactType.Where(a => a.ActiveRec == true && a.TblColSel == contactTypeFilter && a.ContactBasis_LCID == contactBasisId).ToList();


            return PartialView("_CreateDept", addrbk_department);
        }

        // GET: /AddrBkRelation/EditDept

        public ActionResult EditDept(Guid id, Guid organizationId, String orgName)
        {
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(false);

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkDepartment + " / " + orgName;

            AddrBk_Relation addrbk_relation = db.AddrBk_Relation.Find(id);
            AddrBk_OrganizationUnit addrbk_organizationunit = db.AddrBk_OrganizationUnit.Find(addrbk_relation.RelatedIntOrgID1);
            AddrBk_Department addrbk_department = new AddrBk_Department();
            

            addrbk_department.Addrbk_OrganizationUnit = addrbk_organizationunit;
            addrbk_department.Addrbk_Relation = addrbk_relation;
            addrbk_department.Lookup_AddrBks = db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == deptFilter).ToList();
            addrbk_department.Lookup_ContactTypes = db.Lookup_ContactType.Where(a => a.ActiveRec == true && a.TblColSel == contactTypeFilter && a.ContactBasis_LCID == contactBasisId).ToList();


            return PartialView("_EditDept", addrbk_department);
        }

        //
        // POST: /AddrBkRelation/EditDept

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDept(AddrBk_Department addrbk_department, Guid organizationId, String orgName)
        {
            var contactBasisId = Utils.GetOrganizationLookUpBasisId(false);

            try
            {
                if (addrbk_department.Addrbk_Relation.RelnToExtToIntOrg_LCID == null || addrbk_department.Addrbk_Relation.RelnToExtToIntOrg_LCID == Guid.Empty)
                    throw new Exception();

                AddrBk_OrganizationUnit addrbk_organization_unit = addrbk_department.Addrbk_OrganizationUnit;
                AddrBk_Relation addrbk_relation = addrbk_department.Addrbk_Relation;
                
                addrbk_organization_unit.LastUpdatedDt = addrbk_relation.LastUpdatedDt = DateTime.Now;
                
                db.Entry(addrbk_organization_unit).State = EntityState.Modified;
                db.SaveChanges();

                db.Entry(addrbk_relation).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("IndexDept", new { organizationId = organizationId, orgName = orgName });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_Department)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_department.Addrbk_OrganizationUnit.Concurrency = databaseValues.Addrbk_OrganizationUnit.Concurrency;
                addrbk_department.Addrbk_Relation.Concurrency = databaseValues.Addrbk_Relation.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkDepartment + " / " + orgName;

            addrbk_department.Lookup_AddrBks = db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == deptFilter).ToList();
            addrbk_department.Lookup_ContactTypes = db.Lookup_ContactType.Where(a => a.ActiveRec == true && a.TblColSel == contactTypeFilter && a.ContactBasis_LCID == contactBasisId).ToList();


            return PartialView("_EditDept", addrbk_department);
        }
        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}