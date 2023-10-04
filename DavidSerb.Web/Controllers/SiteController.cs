using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DavidSerb.Web.Controllers
{
    public class SiteController : Controller
    {
        public static AppDbContext dbContext = new AppDbContext();

        [Route("")]
        public ActionResult Index()
        {
            List<Site> sites = dbContext.Sites
                .Include(site => site.Country)
                .ToList();

            return View(sites);
        }

        [HttpGet, Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(Site site)
        {
            if (!ModelState.IsValid) return View();

            dbContext.Sites.Add(site);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id}")]
        public ActionResult Edit(string id)
        {
            Site selectedSite = dbContext.Sites.FirstOrDefault(site => site.SiteId == id);
            if (selectedSite == null) return HttpNotFound();

            return View(selectedSite);
        }

        public ActionResult Edit(string id, Site editedSite)
        {
            if (!ModelState.IsValid) return View();

            if (id != editedSite.SiteId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Site existingSite = dbContext.Sites.Find(editedSite.SiteId);
            if (existingSite == null) return HttpNotFound();

            existingSite.SiteName = editedSite.SiteName;
            existingSite.CountryCode = editedSite.CountryCode;
            dbContext.SaveChanges();

            return Redirect("../Index");
        }

        public ActionResult Delete(string id)
        {
            Site selectedSite = dbContext.Sites.FirstOrDefault(site => site.SiteId == id);
            if (selectedSite == null) return HttpNotFound();

            dbContext.Remove(selectedSite);
            dbContext.SaveChanges();

            return Redirect("../Index");
        }

        [HttpGet]
        public ActionResult RequestDrugs(string id)
        {
            Site selectedSite = dbContext.Sites
                .Include(site => site.Country)
                .FirstOrDefault(site => site.SiteId == id);
            if (selectedSite == null) throw new NotFoundException($"Site with id {id} not found.");

            List<DrugUnit> siteDrugUnits = dbContext.DrugUnits
                .Where(drugUnit => drugUnit.DepotId == selectedSite.Country.DepotId)
                .ToList();

            // ??? Maybe can be improved - instead of using ViewBag for SiteId, we can create a ViewModel with 2 props: List<DrugUnit> DrugUnits and string SiteId and return the obj of the VM.
            ViewBag.SiteId = id;

            return View(siteDrugUnits);
        }

        public ActionResult MoveDrugsToSite(string drugUnitId, string siteId)
        {
            DrugUnit selectedDrugUnit = dbContext.DrugUnits.FirstOrDefault(drugUnit => drugUnit.DrugUnitId == drugUnitId);
            if (selectedDrugUnit == null) throw new NotFoundException($"DrugUnit with id {drugUnitId} not found.");

            Site selectedSite = dbContext.Sites.FirstOrDefault(site => site.SiteId == siteId);
            if (selectedSite == null) throw new NotFoundException($"Site with id {siteId} not found.");

            selectedDrugUnit.DepotId = null;
            selectedDrugUnit.SiteId = selectedSite.SiteId;

            // Mark the entity (drugUnit) as changed => EFC knows to don't create a new one but to update the existing one
            dbContext.Entry(selectedDrugUnit).State = EntityState.Modified;
            dbContext.SaveChanges();

            return RedirectToAction("Index", "DrugUnit");
        }
    }
}