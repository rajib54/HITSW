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
    public class AddrBkGeographicalGroupMemberController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private String countryBasis = "Country";
        private String stateBasis = "State/Province";
        private String continentBasis = "Continent";
        private String cityBasis = "City/Town";

        //
        // GET: /AddrBkGeographicalGroupMember/

        public ActionResult Index(Guid organizationId, String orgName, String searchTerm = null, int page = 1)
        {
            String basis = Utils.GetGeoBasisTitle(organizationId);
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrbkGeographicalGroupMember + " / " + orgName;
            ViewBag.basis = basis;

            var model = db.AddrBk_GeographicalGroupMember.Include(a => a.AddrBk_Address).Include(a => a.AddrBk_GeographicalGroup).Include(a => a.Lookup_Continent).Include(a => a.Lookup_Country).Include(a => a.Lookup_StateProvince).Include(a => a.AddrBk_StateOrProvinceLocality).Where(a => a.GeographicalGroup_LCID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_Continent.Title.Contains(searchTerm) || a.Lookup_Country.Title.Contains(searchTerm) || a.Lookup_StateProvince.Title.Contains(searchTerm) || a.AddrBk_Address.Title.Contains(searchTerm) || a.AddrBk_StateOrProvinceLocality.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;

            return PartialView("_Index", model);
        }

        //
        // GET: /AddrBkGeographicalGroupMember/Create

        public ActionResult Create(Guid organizationId, String orgName, String basis)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkGeographicalGroupMember + " / " + orgName;
            ViewBag.State = "";
            ViewBag.StateProvince = "";
            ViewBag.basis = basis;

            ViewBag.AddrID = new SelectList(db.AddrBk_Address, "Id", "Title");
            ViewBag.GeographicalGroup_LCID = new SelectList(db.AddrBk_GeographicalGroup, "Id", "Title");

            if (basis.Equals(continentBasis)) ViewBag.Continent_LCID = new SelectList(db.Lookup_Continent.Where(e=>e.ActiveRec == true), "Id", "Title");

            if (basis.Equals(countryBasis)) ViewBag.Country_LCID = new SelectList(db.Lookup_Country.Where(a => a.ActiveRec == true), "Id", "Title");
            else ViewBag.Country = db.Lookup_Country.Where(a => a.ActiveRec == true).ToList();

            AddrBk_GeographicalGroupMember model = new AddrBk_GeographicalGroupMember();
            model.EffDt = DateTime.Now;

            if (basis.Equals(countryBasis)) return PartialView("_CreateCountry", model);
            else if(basis.Equals(stateBasis))  return PartialView("_CreateState", model);
            else if(basis.Equals(continentBasis)) return PartialView("_CreateContinent", model);
            else return PartialView("_CreateCity", model);
        }

        //
        // POST: /AddrBkGeographicalGroupMember/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_GeographicalGroupMember addrbk_geographicalgroupmember, Guid organizationId, String orgName, String basis)
        {
            try
            {
                if (basis.Equals(countryBasis))
                {
                    if (addrbk_geographicalgroupmember.Country_LCID == null || addrbk_geographicalgroupmember.Country_LCID == Guid.Empty)
                        throw new Exception();
                }

                else if (basis.Equals(stateBasis))
                {
                    if (addrbk_geographicalgroupmember.Country_LCID == null || addrbk_geographicalgroupmember.Country_LCID == Guid.Empty || addrbk_geographicalgroupmember.StateOrProv_LCID == null || addrbk_geographicalgroupmember.StateOrProv_LCID == Guid.Empty)
                        throw new Exception();
                }
                else if (basis.Equals(continentBasis))
                {
                    if (addrbk_geographicalgroupmember.Continent_LCID == null || addrbk_geographicalgroupmember.Continent_LCID == Guid.Empty)
                        throw new Exception();
                }
                else if (basis.Equals(cityBasis))
                {
                    if (addrbk_geographicalgroupmember.Country_LCID == null || addrbk_geographicalgroupmember.Country_LCID == Guid.Empty || addrbk_geographicalgroupmember.StateOrProv_LCID == null || addrbk_geographicalgroupmember.StateOrProv_LCID == Guid.Empty || addrbk_geographicalgroupmember.StateOrProvLocalityID == null || addrbk_geographicalgroupmember.StateOrProvLocalityID == Guid.Empty)
                        throw new Exception();
                }

                addrbk_geographicalgroupmember.Id = Guid.NewGuid();
                addrbk_geographicalgroupmember.CreatedDt = addrbk_geographicalgroupmember.LastUpdatedDt = DateTime.Now;
                addrbk_geographicalgroupmember.ActiveRec = true;
                addrbk_geographicalgroupmember.GeographicalGroup_LCID = organizationId;


                db.AddrBk_GeographicalGroupMember.Add(addrbk_geographicalgroupmember);
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkGeographicalGroupMember + " / " + orgName;
            ViewBag.basis = basis;

            ViewBag.AddrID = new SelectList(db.AddrBk_Address, "Id", "Title", addrbk_geographicalgroupmember.AddrID);
            ViewBag.GeographicalGroup_LCID = new SelectList(db.AddrBk_GeographicalGroup, "Id", "Title", addrbk_geographicalgroupmember.GeographicalGroup_LCID);

            if (basis.Equals(continentBasis)) ViewBag.Continent_LCID = new SelectList(db.Lookup_Continent.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_geographicalgroupmember.Continent_LCID);

            if (basis.Equals(countryBasis)) ViewBag.Country_LCID = new SelectList(db.Lookup_Country.Where(a => a.ActiveRec == true), "Id", "Title");
            else ViewBag.Country = db.Lookup_Country.Where(a => a.ActiveRec == true).ToList();

            ViewBag.State = "";
            if (addrbk_geographicalgroupmember.Country_LCID != null)
                ViewBag.State = db.Lookup_StateProvince.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_geographicalgroupmember.Country_LCID).ToList();

            ViewBag.StateProvince = "";
            if (addrbk_geographicalgroupmember.StateOrProv_LCID != null)
            {
                Guid basisId = Utils.GetGeoBasisId(organizationId);
                if(addrbk_geographicalgroupmember.Country_LCID != null && addrbk_geographicalgroupmember.StateOrProv_LCID != null)
                    ViewBag.StateProvince = db.AddrBk_StateOrProvinceLocality.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_geographicalgroupmember.Country_LCID && a.GeoBasis_LCID == basisId && a.StateOrProv_LCID == addrbk_geographicalgroupmember.StateOrProv_LCID).ToList();
            }

            if (basis.Equals(countryBasis)) return PartialView("_CreateCountry", addrbk_geographicalgroupmember);
            else if (basis.Equals(stateBasis)) return PartialView("_CreateState", addrbk_geographicalgroupmember);
            else if (basis.Equals(continentBasis)) return PartialView("_CreateContinent", addrbk_geographicalgroupmember);
            else return PartialView("_CreateCity", addrbk_geographicalgroupmember);
        }

        //
        // GET: /AddrBkGeographicalGroupMember/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName,String basis)
        {
            AddrBk_GeographicalGroupMember addrbk_geographicalgroupmember = db.AddrBk_GeographicalGroupMember.Find(id);
            if (addrbk_geographicalgroupmember == null)
            {
                return HttpNotFound();
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkGeographicalGroupMember + " / " + orgName;
            ViewBag.basis = basis;

            ViewBag.AddrID = new SelectList(db.AddrBk_Address, "Id", "Title", addrbk_geographicalgroupmember.AddrID);
            ViewBag.GeographicalGroup_LCID = new SelectList(db.AddrBk_GeographicalGroup, "Id", "Title", addrbk_geographicalgroupmember.GeographicalGroup_LCID);

            if (basis.Equals(continentBasis)) ViewBag.Continent_LCID = new SelectList(db.Lookup_Continent.Where(e => e.ActiveRec == true), "Id", "Title", addrbk_geographicalgroupmember.Continent_LCID);

            if (basis.Equals(countryBasis)) ViewBag.Country_LCID = new SelectList(db.Lookup_Country.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_geographicalgroupmember.Country_LCID);
            else ViewBag.Country = db.Lookup_Country.Where(a => a.ActiveRec == true).ToList();

            ViewBag.State = db.Lookup_StateProvince.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_geographicalgroupmember.Country_LCID).ToList();
            
            if (addrbk_geographicalgroupmember.StateOrProv_LCID != null)
            {
                Guid basisId = Utils.GetGeoBasisId(organizationId);
                if (addrbk_geographicalgroupmember.Country_LCID != null && addrbk_geographicalgroupmember.StateOrProv_LCID != null)
                    ViewBag.StateProvince = db.AddrBk_StateOrProvinceLocality.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_geographicalgroupmember.Country_LCID && a.GeoBasis_LCID == basisId && a.StateOrProv_LCID == addrbk_geographicalgroupmember.StateOrProv_LCID).ToList();
            }

            if (basis.Equals(countryBasis)) return PartialView("_EditCountry", addrbk_geographicalgroupmember);
            else if (basis.Equals(stateBasis)) return PartialView("_EditState", addrbk_geographicalgroupmember);
            else if (basis.Equals(continentBasis)) return PartialView("_EditContinent", addrbk_geographicalgroupmember);
            else return PartialView("_EditCity", addrbk_geographicalgroupmember);
        }

        //
        // POST: /AddrBkGeographicalGroupMember/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_GeographicalGroupMember addrbk_geographicalgroupmember, Guid organizationId, String orgName, String basis)
        {
            try
            {
                if (basis.Equals(countryBasis))
                {
                    if (addrbk_geographicalgroupmember.Country_LCID == null || addrbk_geographicalgroupmember.Country_LCID == Guid.Empty)
                        throw new Exception();
                }
                else if (basis.Equals(stateBasis))
                {
                    if (addrbk_geographicalgroupmember.Country_LCID == null || addrbk_geographicalgroupmember.Country_LCID == Guid.Empty || addrbk_geographicalgroupmember.StateOrProv_LCID == null || addrbk_geographicalgroupmember.StateOrProv_LCID == Guid.Empty)
                        throw new Exception();
                }
                else if (basis.Equals(continentBasis))
                {
                    if (addrbk_geographicalgroupmember.Continent_LCID == null || addrbk_geographicalgroupmember.Continent_LCID == Guid.Empty)
                        throw new Exception();
                }
                else if (basis.Equals(cityBasis))
                {
                    if (addrbk_geographicalgroupmember.Country_LCID == null || addrbk_geographicalgroupmember.Country_LCID == Guid.Empty || addrbk_geographicalgroupmember.StateOrProv_LCID == null || addrbk_geographicalgroupmember.StateOrProv_LCID == Guid.Empty || addrbk_geographicalgroupmember.StateOrProvLocalityID == null || addrbk_geographicalgroupmember.StateOrProvLocalityID == Guid.Empty)
                        throw new Exception();
                }

                addrbk_geographicalgroupmember.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_geographicalgroupmember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (AddrBk_GeographicalGroupMember)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_geographicalgroupmember.Concurrency = databaseValues.Concurrency;
                addrbk_geographicalgroupmember.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkGeographicalGroupMember + " / " + orgName;
            ViewBag.basis = basis;

            ViewBag.AddrID = new SelectList(db.AddrBk_Address, "Id", "Title", addrbk_geographicalgroupmember.AddrID);
            ViewBag.GeographicalGroup_LCID = new SelectList(db.AddrBk_GeographicalGroup, "Id", "Title", addrbk_geographicalgroupmember.GeographicalGroup_LCID);
            ViewBag.Continent_LCID = new SelectList(db.Lookup_Continent.Where(e=>e.ActiveRec == true), "Id", "Title", addrbk_geographicalgroupmember.Continent_LCID);

            if (basis.Equals(countryBasis)) ViewBag.Country_LCID = new SelectList(db.Lookup_Country.Where(a => a.ActiveRec == true), "Id", "Title", addrbk_geographicalgroupmember.Country_LCID);
            else ViewBag.Country = db.Lookup_Country.Where(a => a.ActiveRec == true).ToList();

            ViewBag.State = db.Lookup_StateProvince.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_geographicalgroupmember.Country_LCID).ToList();

            if (addrbk_geographicalgroupmember.StateOrProv_LCID != null)
            {
                Guid basisId = Utils.GetGeoBasisId(organizationId);
                if (addrbk_geographicalgroupmember.Country_LCID != null && addrbk_geographicalgroupmember.StateOrProv_LCID != null)
                    ViewBag.StateProvince = db.AddrBk_StateOrProvinceLocality.Where(a => a.ActiveRec == true && a.Cntry_LCID == addrbk_geographicalgroupmember.Country_LCID && a.GeoBasis_LCID == basisId && a.StateOrProv_LCID == addrbk_geographicalgroupmember.StateOrProv_LCID).ToList();
            }

            if (basis.Equals(countryBasis)) return PartialView("_EditCountry", addrbk_geographicalgroupmember);
            else if (basis.Equals(stateBasis)) return PartialView("_EditState", addrbk_geographicalgroupmember);
            else if (basis.Equals(continentBasis)) return PartialView("_EditContinent", addrbk_geographicalgroupmember);
            else return PartialView("_EditCity", addrbk_geographicalgroupmember);
        }

        //
        // GET: /AddrBkGeographicalGroupMember/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName)
        {
            AddrBk_GeographicalGroupMember addrbk_geographicalgroupmember = db.AddrBk_GeographicalGroupMember.Find(id);
            if (addrbk_geographicalgroupmember == null)
            {
                return HttpNotFound();
            }
            db.AddrBk_GeographicalGroupMember.Remove(addrbk_geographicalgroupmember);
            db.SaveChanges();

            return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadStateProvinceLocality(Guid countryId,Guid stateprovinceId,Guid organizationId)
        {
            Guid basisId = Utils.GetGeoBasisId(organizationId);
            var stateprovincelocalityList = db.AddrBk_StateOrProvinceLocality.Where(a => a.ActiveRec == true && a.Cntry_LCID == countryId && a.GeoBasis_LCID == basisId && a.StateOrProv_LCID == stateprovinceId).ToList();
            var data = stateprovincelocalityList.Select(m => new SelectListItem()
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