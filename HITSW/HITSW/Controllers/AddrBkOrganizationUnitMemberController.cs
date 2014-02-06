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
    public class AddrBkOrganizationUnitMemberController : Controller
    {
        private HITSWContext db = new HITSWContext();

        //
        // GET: /AddrBkOrganizationUnitMember/

        public ActionResult Index(Guid organizationId, String orgName, String searchTerm = null, int page = 1)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkOrganizationMemeber + " / " + orgName;

            var model = db.AddrBk_OrganizationalUnitMember.Include(a => a.Lookup_ContactType).Include(a => a.AddrBk_Person).Where(a => a.OU_ID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_ContactType.Title.Contains(searchTerm) || a.MemTitle.Contains(searchTerm) || a.AddrBk_Person.FName.Contains(searchTerm) || a.AddrBk_Person.LName.Contains(searchTerm) || a.AddrBk_Person.MName.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_Index", model);
        }

        //
        // GET: /AddrBkOrganizationUnitMember/Create

        public ActionResult Create(Guid organizationId, String orgName)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationMemeber + " / " + orgName;

            AddrBk_OrganizationalUnitMember addrbk_organizationalunitmember = new AddrBk_OrganizationalUnitMember();
            addrbk_organizationalunitmember.EffDt = DateTime.Now;

            AddrBk_Person addrbk_person = new AddrBk_Person();
            AddrBk_OrganizationMember addrbk_organizationmember = new AddrBk_OrganizationMember();

            addrbk_organizationmember.OrganizationUnitMember = addrbk_organizationalunitmember;
            addrbk_organizationmember.Person = addrbk_person;

            addrbk_organizationmember.LookUp_MemberTypes = db.Lookup_ContactType.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberType).ToList();
            addrbk_organizationmember.Lookup_Salutations = db.Lookup_GenderRelationship.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberSalutation).ToList();
            addrbk_organizationmember.Lookup_Suffixes = db.Lookup_AddrBk.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberSuffix).ToList();
            addrbk_organizationmember.Lookup_Genders = db.Lookup_Gender.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_MaritialStatuses = db.Lookup_MaritalStatus.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Races = db.Lookup_Race.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Ethnicities = db.Lookup_Ethnicity.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_ReligiousBackgrounds = db.Lookup_AddrBk.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberReligiousBackground).ToList();
            addrbk_organizationmember.Lookup_EducationLevels = db.Lookup_EducationalLevel.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Occupations = db.Lookup_Occupation.Where(e => e.ActiveRec == true).ToList();

            return PartialView("_Create", addrbk_organizationmember);
        }

        //
        // POST: /AddrBkOrganizationUnitMember/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_OrganizationMember addrbk_organizationmember, Guid organizationId, String orgName)
        {
            try
            {
                addrbk_organizationmember.Person.Id = Guid.NewGuid();
                addrbk_organizationmember.Person.CreatedDt = addrbk_organizationmember.Person.LastUpdatedDt = addrbk_organizationmember.Person.EffDt = DateTime.Now;
                addrbk_organizationmember.Person.ActiveRec = true;
                addrbk_organizationmember.Person.IsProspect = false;

                db.AddrBk_Person.Add(addrbk_organizationmember.Person);
                db.SaveChanges();

                addrbk_organizationmember.OrganizationUnitMember.MemberID = addrbk_organizationmember.Person.Id;
                addrbk_organizationmember.OrganizationUnitMember.CreatedDt = addrbk_organizationmember.OrganizationUnitMember.LastUpdatedDt = DateTime.Now;
                addrbk_organizationmember.OrganizationUnitMember.ActiveRec = true;
                addrbk_organizationmember.OrganizationUnitMember.OU_ID = organizationId;
                addrbk_organizationmember.OrganizationUnitMember.Id = Guid.NewGuid();

                db.AddrBk_OrganizationalUnitMember.Add(addrbk_organizationmember.OrganizationUnitMember);
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationMemeber + " / " + orgName;

            addrbk_organizationmember.LookUp_MemberTypes = db.Lookup_ContactType.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberType).ToList();
            addrbk_organizationmember.Lookup_Salutations = db.Lookup_GenderRelationship.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberSalutation).ToList();
            addrbk_organizationmember.Lookup_Suffixes = db.Lookup_AddrBk.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberSuffix).ToList();
            addrbk_organizationmember.Lookup_Genders = db.Lookup_Gender.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_MaritialStatuses = db.Lookup_MaritalStatus.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Races = db.Lookup_Race.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Ethnicities = db.Lookup_Ethnicity.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_ReligiousBackgrounds = db.Lookup_AddrBk.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberReligiousBackground).ToList();
            addrbk_organizationmember.Lookup_EducationLevels = db.Lookup_EducationalLevel.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Occupations = db.Lookup_Occupation.Where(e => e.ActiveRec == true).ToList();

            return PartialView("_Create", addrbk_organizationmember);
        }


        //
        // GET: /AddrBkOrganizationUnitMember/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName)
        {
            AddrBk_OrganizationalUnitMember addrbk_organizationalunitmember = db.AddrBk_OrganizationalUnitMember.Find(id);
            if (addrbk_organizationalunitmember == null)
            {
                return HttpNotFound();
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;

            AddrBk_Person addrbk_person = db.AddrBk_Person.Find(addrbk_organizationalunitmember.MemberID);
            AddrBk_OrganizationMember addrbk_organizationmember = new AddrBk_OrganizationMember();

            ViewBag.MainTitle = Utils.AddrBkOrganizationMemeber + " / " + orgName + " / " + addrbk_person.FName + " " + addrbk_person.LName;

            addrbk_organizationmember.OrganizationUnitMember = addrbk_organizationalunitmember;
            addrbk_organizationmember.Person = addrbk_person;

            addrbk_organizationmember.LookUp_MemberTypes = db.Lookup_ContactType.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberType).ToList();
            addrbk_organizationmember.Lookup_Salutations = db.Lookup_GenderRelationship.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberSalutation).ToList();
            addrbk_organizationmember.Lookup_Suffixes = db.Lookup_AddrBk.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberSuffix).ToList();
            addrbk_organizationmember.Lookup_Genders = db.Lookup_Gender.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_MaritialStatuses = db.Lookup_MaritalStatus.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Races = db.Lookup_Race.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Ethnicities = db.Lookup_Ethnicity.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_ReligiousBackgrounds = db.Lookup_AddrBk.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberReligiousBackground).ToList();
            addrbk_organizationmember.Lookup_EducationLevels = db.Lookup_EducationalLevel.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Occupations = db.Lookup_Occupation.Where(e => e.ActiveRec == true).ToList();

            return PartialView("_Edit", addrbk_organizationmember);
        }

        //
        // POST: /AddrBkOrganizationUnitMember/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_OrganizationMember addrbk_organizationmember, Guid organizationId, String orgName)
        {
            try
            {
                AddrBk_OrganizationalUnitMember addrbk_organizationunitmember = addrbk_organizationmember.OrganizationUnitMember;
                AddrBk_Person addrbk_person = addrbk_organizationmember.Person;

                addrbk_organizationunitmember.LastUpdatedDt = addrbk_person.LastUpdatedDt = DateTime.Now;

                db.Entry(addrbk_person).State = EntityState.Modified;
                db.SaveChanges();

                db.Entry(addrbk_organizationunitmember).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_OrganizationMember)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_organizationmember.OrganizationUnitMember.Concurrency = databaseValues.OrganizationUnitMember.Concurrency;
                addrbk_organizationmember.Person.Concurrency = databaseValues.Person.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkOrganizationMemeber + " / " + orgName + " / " + addrbk_organizationmember.Person.FName + " " + addrbk_organizationmember.Person.LName;

            addrbk_organizationmember.LookUp_MemberTypes = db.Lookup_ContactType.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberType).ToList();
            addrbk_organizationmember.Lookup_Salutations = db.Lookup_GenderRelationship.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberSalutation).ToList();
            addrbk_organizationmember.Lookup_Suffixes = db.Lookup_AddrBk.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberSuffix).ToList();
            addrbk_organizationmember.Lookup_Genders = db.Lookup_Gender.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_MaritialStatuses = db.Lookup_MaritalStatus.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Races = db.Lookup_Race.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Ethnicities = db.Lookup_Ethnicity.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_ReligiousBackgrounds = db.Lookup_AddrBk.Where(e => e.ActiveRec == true && e.TblColSel == Utils.AB_OrganizationUnitMemberReligiousBackground).ToList();
            addrbk_organizationmember.Lookup_EducationLevels = db.Lookup_EducationalLevel.Where(e => e.ActiveRec == true).ToList();
            addrbk_organizationmember.Lookup_Occupations = db.Lookup_Occupation.Where(e => e.ActiveRec == true).ToList();

            return PartialView("_Edit", addrbk_organizationmember);
        }

        //
        // GET: /AddrBkOrganizationUnitMember/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName)
        {
            AddrBk_OrganizationalUnitMember addrbk_organizationalunitmember = db.AddrBk_OrganizationalUnitMember.Find(id);
            if (addrbk_organizationalunitmember == null)
            {
                return HttpNotFound();
            }

            db.AddrBk_OrganizationalUnitMember.Remove(addrbk_organizationalunitmember);
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