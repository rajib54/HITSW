﻿using System;
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
    public class AddrBkPersonController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private String suffixFilter = "AddrBk_Person.Suffix_LCID";
        private String religiousBackgroundFilter = "AddrBk_Person.ReligiousBkgd_LCID";
        private String salutationFilter = "AddrBk_Person.Salutation_LCID";

        //
        // GET: /AddrBkPerson/

        public ActionResult Index(String searchTerm = null, int page = 1)
        {
            var model = db.AddrBk_Person.Include(a => a.Lookup_Ethnicity).Include(a => a.Lookup_Gender).Include(a => a.Lookup_EducationalLevel).Include(a => a.Lookup_MaritalStatus).Include(a => a.Lookup_Occupation).Include(a => a.Lookup_Race).Include(a => a.Lookup_AddrBk).Include(a => a.Lookup_GenderRelationship).Include(a => a.Lookup_AddrBk1)
                .Where(a => a.IsProspect == false && a.ActiveRec == true && (searchTerm == null || a.Lookup_Ethnicity.Title.Contains(searchTerm) || a.Lookup_Gender.Title.Contains(searchTerm) || a.Lookup_EducationalLevel.Title.Contains(searchTerm) || a.Lookup_MaritalStatus.Title.Contains(searchTerm) || a.Lookup_Race.Title.Contains(searchTerm) || a.Lookup_AddrBk.Title.Contains(searchTerm) || a.Lookup_Occupation.Title.Contains(searchTerm)
                    || a.FName.Contains(searchTerm) || a.MName.Contains(searchTerm) || a.LName.Contains(searchTerm) || a.JobTitle.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);

            ViewBag.searchTerm = searchTerm;
            ViewBag.resultCount = model.Count;
            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkPerson;

            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;

            if (Request.IsAjaxRequest())
                return PartialView("_Person", model);
            return View(model);
        }

        //
        // GET: /AddrBkPerson/Details/5

        public ActionResult Details(Guid id)
        {
            AddrBk_Person addrbk_person = db.AddrBk_Person.Find(id);
            if (addrbk_person == null)
            {
                return HttpNotFound();
            }
            return View(addrbk_person);
        }

        //
        // GET: /AddrBkPerson/Create

        
        public ActionResult Create()
        {
            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkPerson;
            ViewBag.Ethnicity_LCID = new SelectList(db.Lookup_Ethnicity.Where(a => a.ActiveRec == true), "Id", "Title");
            ViewBag.Gender_LCID = new SelectList(db.Lookup_Gender.Where(a => a.ActiveRec == true), "Id", "Title");
            ViewBag.HighestEducLvl_LCID = new SelectList(db.Lookup_EducationalLevel.Where(a => a.ActiveRec == true), "Id", "Title");
            ViewBag.MaritalStatus_LCID = new SelectList(db.Lookup_MaritalStatus.Where(a => a.ActiveRec == true), "Id", "Title");
            ViewBag.Occupation_LCID = new SelectList(db.Lookup_Occupation.Where(a => a.ActiveRec == true), "Id", "Title");
            ViewBag.Race_LCID = new SelectList(db.Lookup_Race.Where(a => a.ActiveRec == true), "Id", "Title");
            ViewBag.ReligiousBkgd_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == religiousBackgroundFilter), "Id", "Title");
            ViewBag.Salutation_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == salutationFilter), "Id", "Title");
            ViewBag.SuffixLCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == suffixFilter), "Id", "Title");

            AddrBk_Person addrbk_person = new AddrBk_Person();
            addrbk_person.EffDt = DateTime.Now;
            return View(addrbk_person);
        }

        //
        // POST: /AddrBkPerson/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_Person addrbk_person)
        {
            try
            {
                addrbk_person.Id = Guid.NewGuid();
                addrbk_person.CreatedDt = addrbk_person.LastUpdatedDt = DateTime.Now;
                addrbk_person.ActiveRec = true;
                addrbk_person.IsProspect = false;
                db.AddrBk_Person.Add(addrbk_person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, Utils.errorMsg);

            }

            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkPerson;
            ViewBag.Ethnicity_LCID = new SelectList(db.Lookup_Ethnicity.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Ethnicity_LCID);
            ViewBag.Gender_LCID = new SelectList(db.Lookup_Gender.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Gender_LCID);
            ViewBag.HighestEducLvl_LCID = new SelectList(db.Lookup_EducationalLevel.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.HighestEducLvl_LCID);
            ViewBag.MaritalStatus_LCID = new SelectList(db.Lookup_MaritalStatus.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.MaritalStatus_LCID);
            ViewBag.Occupation_LCID = new SelectList(db.Lookup_Occupation.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Occupation_LCID);
            ViewBag.Race_LCID = new SelectList(db.Lookup_Race.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Race_LCID);
            ViewBag.ReligiousBkgd_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == religiousBackgroundFilter), "Id", "Title", addrbk_person.ReligiousBkgd_LCID);
            ViewBag.Salutation_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == salutationFilter), "Id", "Title", addrbk_person.Salutation_LCID);
            ViewBag.SuffixLCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == suffixFilter), "Id", "Title", addrbk_person.Suffix_LCID);
            return View(addrbk_person);
        }

        //
        // GET: /AddrBkPerson/Edit/5

        public ActionResult Edit(Guid id)
        {
            AddrBk_Person addrbk_person = db.AddrBk_Person.Find(id);
            if (addrbk_person == null)
            {
                return HttpNotFound();
            }

            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkPerson;
            ViewBag.Ethnicity_LCID = new SelectList(db.Lookup_Ethnicity.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Ethnicity_LCID);
            ViewBag.Gender_LCID = new SelectList(db.Lookup_Gender.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Gender_LCID);
            ViewBag.HighestEducLvl_LCID = new SelectList(db.Lookup_EducationalLevel.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.HighestEducLvl_LCID);
            ViewBag.MaritalStatus_LCID = new SelectList(db.Lookup_MaritalStatus.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.MaritalStatus_LCID);
            ViewBag.Occupation_LCID = new SelectList(db.Lookup_Occupation.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Occupation_LCID);
            ViewBag.Race_LCID = new SelectList(db.Lookup_Race.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Race_LCID);
            ViewBag.ReligiousBkgd_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == religiousBackgroundFilter), "Id", "Title", addrbk_person.ReligiousBkgd_LCID);
            ViewBag.Salutation_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == salutationFilter), "Id", "Title", addrbk_person.Salutation_LCID);
            ViewBag.SuffixLCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == suffixFilter), "Id", "Title", addrbk_person.Suffix_LCID);
            return View(addrbk_person);
        }

        //
        // POST: /AddrBkPerson/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_Person addrbk_person)
        {
            try
            {
                addrbk_person.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_Person)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_person.Concurrency = databaseValues.Concurrency;
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, Utils.errorMsg);

            }

            ViewBag.MainTitle = Utils.AddressBook + " / " + Utils.AddrBkPerson;
            ViewBag.Ethnicity_LCID = new SelectList(db.Lookup_Ethnicity.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Ethnicity_LCID);
            ViewBag.Gender_LCID = new SelectList(db.Lookup_Gender.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Gender_LCID);
            ViewBag.HighestEducLvl_LCID = new SelectList(db.Lookup_EducationalLevel.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.HighestEducLvl_LCID);
            ViewBag.MaritalStatus_LCID = new SelectList(db.Lookup_MaritalStatus.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.MaritalStatus_LCID);
            ViewBag.Occupation_LCID = new SelectList(db.Lookup_Occupation.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Occupation_LCID);
            ViewBag.Race_LCID = new SelectList(db.Lookup_Race.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_person.Race_LCID);
            ViewBag.ReligiousBkgd_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == religiousBackgroundFilter), "Id", "Title", addrbk_person.ReligiousBkgd_LCID);
            ViewBag.Salutation_LCID = new SelectList(db.Lookup_GenderRelationship.Where(a => a.ActiveRec == true && a.TblColSel == salutationFilter), "Id", "Title", addrbk_person.Salutation_LCID);
            ViewBag.SuffixLCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == suffixFilter), "Id", "Title", addrbk_person.Suffix_LCID);
            return View(addrbk_person);
        }

        //
        // GET: /AddrBkPerson/Delete/5

        public ActionResult Delete(Guid id)
        {
            AddrBk_Person addrbk_person = db.AddrBk_Person.Find(id);
            if (addrbk_person == null)
            {
                return HttpNotFound();
            }

            db.AddrBk_Person.Remove(addrbk_person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}