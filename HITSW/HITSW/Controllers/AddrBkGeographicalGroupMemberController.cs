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
    public class AddrBkGeographicalGroupMemberController : Controller
    {
        private HITSWContext db = new HITSWContext();

        //
        // GET: /AddrBkGeographicalGroupMember/

        public ActionResult Index(Guid organizationId, String orgName, String searchTerm = null, int page = 1)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrbkGeographicalGroupMember + " / " + orgName;

            var model = db.AddrBk_GeographicalGroupMember.Include(a => a.AddrBk_Address).Include(a => a.AddrBk_GeographicalGroup).Include(a => a.Lookup_Continent).Include(a => a.Lookup_Country).Include(a => a.Lookup_StateProvince).Include(a => a.AddrBk_StateOrProvinceLocality).Where(a => a.GeographicalGroup_LCID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_Continent.Title.Contains(searchTerm) || a.Lookup_Country.Title.Contains(searchTerm) || a.Lookup_StateProvince.Title.Contains(searchTerm) || a.AddrBk_Address.Title.Contains(searchTerm) || a.AddrBk_StateOrProvinceLocality.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_IndexCountry", model);
        }

        //
        // GET: /AddrBkGeographicalGroupMember/Create

        public ActionResult Create(Guid organizationId, String orgName)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkGeographicalGroupMember + " / " + orgName;

            ViewBag.AddrID = new SelectList(db.AddrBk_Address, "Id", "Title");
            ViewBag.GeographicalGroup_LCID = new SelectList(db.AddrBk_GeographicalGroup, "Id", "Title");
            ViewBag.Continent_LCID = new SelectList(db.Lookup_Continent, "Id", "Title");
            ViewBag.Country_LCID = new SelectList(db.Lookup_Country.Where(a=>a.ActiveRec == true), "Id", "Title");
            ViewBag.StateOrProv_LCID = new SelectList(db.Lookup_StateProvince, "Id", "Title");
            ViewBag.StateOrProvLocalityID = new SelectList(db.AddrBk_StateOrProvinceLocality, "Id", "Title");

            AddrBk_GeographicalGroupMember model = new AddrBk_GeographicalGroupMember();
            model.EffDt = DateTime.Now;

            return PartialView("_CreateCountry", model);
        }

        //
        // POST: /AddrBkGeographicalGroupMember/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddrBk_GeographicalGroupMember addrbk_geographicalgroupmember, Guid organizationId, String orgName)
        {
            try
            {
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

            ViewBag.AddrID = new SelectList(db.AddrBk_Address, "Id", "Title", addrbk_geographicalgroupmember.AddrID);
            ViewBag.GeographicalGroup_LCID = new SelectList(db.AddrBk_GeographicalGroup, "Id", "Title", addrbk_geographicalgroupmember.GeographicalGroup_LCID);
            ViewBag.Continent_LCID = new SelectList(db.Lookup_Continent, "Id", "Title", addrbk_geographicalgroupmember.Continent_LCID);
            ViewBag.Country_LCID = new SelectList(db.Lookup_Country.Where(a=>a.ActiveRec == true), "Id", "Title", addrbk_geographicalgroupmember.Country_LCID);
            ViewBag.StateOrProv_LCID = new SelectList(db.Lookup_StateProvince, "Id", "Title", addrbk_geographicalgroupmember.StateOrProv_LCID);
            ViewBag.StateOrProvLocalityID = new SelectList(db.AddrBk_StateOrProvinceLocality, "Id", "Title", addrbk_geographicalgroupmember.StateOrProvLocalityID);

            return PartialView("_CreateCountry", addrbk_geographicalgroupmember);
        }

        //
        // GET: /AddrBkGeographicalGroupMember/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName)
        {
            AddrBk_GeographicalGroupMember addrbk_geographicalgroupmember = db.AddrBk_GeographicalGroupMember.Find(id);
            if (addrbk_geographicalgroupmember == null)
            {
                return HttpNotFound();
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrbkGeographicalGroupMember + " / " + orgName;

            ViewBag.AddrID = new SelectList(db.AddrBk_Address, "Id", "Title", addrbk_geographicalgroupmember.AddrID);
            ViewBag.GeographicalGroup_LCID = new SelectList(db.AddrBk_GeographicalGroup, "Id", "Title", addrbk_geographicalgroupmember.GeographicalGroup_LCID);
            ViewBag.Continent_LCID = new SelectList(db.Lookup_Continent, "Id", "Title", addrbk_geographicalgroupmember.Continent_LCID);
            ViewBag.Country_LCID = new SelectList(db.Lookup_Country.Where(e=>e.ActiveRec == true), "Id", "Title", addrbk_geographicalgroupmember.Country_LCID);
            ViewBag.StateOrProv_LCID = new SelectList(db.Lookup_StateProvince, "Id", "Title", addrbk_geographicalgroupmember.StateOrProv_LCID);
            ViewBag.StateOrProvLocalityID = new SelectList(db.AddrBk_StateOrProvinceLocality, "Id", "Title", addrbk_geographicalgroupmember.StateOrProvLocalityID);

            return PartialView("_EditCountry", addrbk_geographicalgroupmember);
        }

        //
        // POST: /AddrBkGeographicalGroupMember/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddrBk_GeographicalGroupMember addrbk_geographicalgroupmember, Guid organizationId, String orgName)
        {
            try
            {
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

            ViewBag.AddrID = new SelectList(db.AddrBk_Address, "Id", "Title", addrbk_geographicalgroupmember.AddrID);
            ViewBag.GeographicalGroup_LCID = new SelectList(db.AddrBk_GeographicalGroup, "Id", "Title", addrbk_geographicalgroupmember.GeographicalGroup_LCID);
            ViewBag.Continent_LCID = new SelectList(db.Lookup_Continent, "Id", "Title", addrbk_geographicalgroupmember.Continent_LCID);
            ViewBag.Country_LCID = new SelectList(db.Lookup_Country.Where(e=>e.ActiveRec == true), "Id", "Title", addrbk_geographicalgroupmember.Country_LCID);
            ViewBag.StateOrProv_LCID = new SelectList(db.Lookup_StateProvince, "Id", "Title", addrbk_geographicalgroupmember.StateOrProv_LCID);
            ViewBag.StateOrProvLocalityID = new SelectList(db.AddrBk_StateOrProvinceLocality, "Id", "Title", addrbk_geographicalgroupmember.StateOrProvLocalityID);
            return PartialView("_EditCountry", addrbk_geographicalgroupmember);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}