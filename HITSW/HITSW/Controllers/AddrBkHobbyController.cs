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
    public class AddrBkHobbyController : Controller
    {
        private HITSWContext db = new HITSWContext();
        private String statusFilter = "Addrbk_Hobby.Hobby_LCID";

        //
        // GET: /AddrBkHobby/

        public ActionResult Index(Guid organizationId, String orgName, String searchTerm = null, int page = 1)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.searchTerm = searchTerm;
            ViewBag.MainTitle = Utils.AddrBkHobby + " / " + orgName;

            var model = db.Addrbk_Hobby.Include(a => a.Lookup_AddrBk).Where(a => a.IndivID == organizationId && a.ActiveRec == true && (searchTerm == null || a.Lookup_AddrBk.Title.Contains(searchTerm))).OrderByDescending(a => a.LastUpdatedDt).ToPagedList(page, Utils.pageSize);
            ViewBag.resultCount = model.Count;
            if (ViewBag.resultCount == 0)
                ViewBag.NoRecordFoundMsg = Utils.norecordfoundMsg;
            return PartialView("_Index", model);

        }

        //
        // GET: /AddrBkHobby/Create

        public ActionResult Create(Guid organizationId, String orgName)
        {
            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkHobby + " / " + orgName;
            ViewBag.Hobby_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter), "Id", "Title");

            Addrbk_Hobby addrbk_hobby = new Addrbk_Hobby();
            addrbk_hobby.EffDt = DateTime.Now;
            return PartialView("_Create", addrbk_hobby);
        }

        //
        // POST: /AddrBkHobby/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Addrbk_Hobby addrbk_hobby, Guid organizationId, String orgName)
        {
            try
            {
                addrbk_hobby.Id = Guid.NewGuid();
                addrbk_hobby.CreatedDt = addrbk_hobby.LastUpdatedDt = DateTime.Now;
                addrbk_hobby.ActiveRec = true;
                addrbk_hobby.IndivID = organizationId;

                db.Addrbk_Hobby.Add(addrbk_hobby);
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkHobby + " / " + orgName;
            ViewBag.Hobby_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter), "Id", "Title", addrbk_hobby.Hobby_LCID);
            return PartialView("_Create", addrbk_hobby);
        }

        //
        // GET: /AddrBkHobby/Edit/5

        public ActionResult Edit(Guid id, Guid organizationId, String orgName)
        {
            Addrbk_Hobby addrbk_hobby = db.Addrbk_Hobby.Find(id);
            if (addrbk_hobby == null)
            {
                return HttpNotFound();
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkHobby + " / " + orgName + " / " + Utils.GetHobbyFromId(addrbk_hobby.Hobby_LCID);
            ViewBag.Hobby_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter), "Id", "Title", addrbk_hobby.Hobby_LCID);
            return PartialView("_Edit", addrbk_hobby);
        }

        //
        // POST: /AddrBkHobby/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Addrbk_Hobby addrbk_hobby, Guid organizationId, String orgName)
        {
            try
            {
                addrbk_hobby.LastUpdatedDt = DateTime.Now;
                db.Entry(addrbk_hobby).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { organizationId = organizationId, orgName = orgName });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (Addrbk_Hobby)entry.GetDatabaseValues().ToObject();
                ModelState.AddModelError(string.Empty, Utils.concurrencyMsg);
                addrbk_hobby.Concurrency = databaseValues.Concurrency;
            }
            catch
            {
                ModelState.AddModelError(String.Empty, Utils.errorMsg);
            }

            ViewBag.orgName = orgName;
            ViewBag.organizationId = organizationId;
            ViewBag.MainTitle = Utils.AddrBkHobby + " / " + orgName + " / " + Utils.GetHobbyFromId(addrbk_hobby.Hobby_LCID);
            ViewBag.Hobby_LCID = new SelectList(db.Lookup_AddrBk.Where(a => a.ActiveRec == true && a.TblColSel == statusFilter), "Id", "Title", addrbk_hobby.Hobby_LCID);
            return PartialView("_Edit", addrbk_hobby);
        }

        //
        // GET: /AddrBkHobby/Delete/5

        public ActionResult Delete(Guid id, Guid organizationId, String orgName)
        {
            Addrbk_Hobby addrbk_hobby = db.Addrbk_Hobby.Find(id);
            if (addrbk_hobby == null)
            {
                return HttpNotFound();
            }
            db.Addrbk_Hobby.Remove(addrbk_hobby);
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