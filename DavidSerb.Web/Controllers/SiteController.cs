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
        public async Task<ActionResult> Index()
        {
            List<Site> sites = await dbContext.Sites
                .AsNoTracking()
                .Include(site => site.Country)
                .ToListAsync();
            if (!sites.Any()) return HttpNotFound();

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

            await dbContext.Sites.AddAsync(site);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id}")]
        public async Task<ActionResult> Edit(string id)
        {
            Site selectedSite = await dbContext.Sites.FirstOrDefaultAsync(site => site.SiteId == id);
            if (selectedSite == null) return HttpNotFound();

            return View(selectedSite);
        }

        public async Task<ActionResult> Edit(string id, Site editedSite)
        {
            if (!ModelState.IsValid) return View();

            if (id != editedSite.SiteId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Site existingSite = await dbContext.Sites.FindAsync(editedSite.SiteId);
            if (existingSite == null) return HttpNotFound();

            existingSite.SiteName = editedSite.SiteName;
            existingSite.CountryCode = editedSite.CountryCode;
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            Site selectedSite = await dbContext.Sites.FirstOrDefaultAsync(site => site.SiteId == id);
            if (selectedSite == null) return HttpNotFound();

            dbContext.Remove(selectedSite);
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }

        [HttpGet]
        public async Task<ActionResult> RequestDrugs(string id)
        {
            Site selectedSite = await dbContext.Sites
                .Include(site => site.Country)
                .FirstOrDefaultAsync(site => site.SiteId == id);
            if (selectedSite == null) throw new NotFoundException($"Site with id {id} not found.");

            // BEFORE:
            //List<DrugUnit> siteDrugUnits1 = await dbContext.DrugUnits
            //    .Where(drugUnit => drugUnit.DepotId == selectedSite.Country.DepotId)
            //    .ToListAsync();
            // AFTER (EFC - interpolated string to parameterized SQL. Prevent SQL injection attacks.):
            List<DrugUnit> siteDrugUnits = await dbContext.DrugUnits
                .FromSqlInterpolated($"SELECT * FROM DrugUnits WHERE DepotId = {selectedSite.Country.DepotId}")
                .ToListAsync();

            // ??? Maybe can be improved - instead of using ViewBag for SiteId, we can create a ViewModel with 2 props: List<DrugUnit> DrugUnits and string SiteId and return the obj of the VM.
            ViewBag.SiteId = id;

            return View(siteDrugUnits);
        }

        public async Task<ActionResult> MoveDrugsToSite(string drugUnitId, string siteId)
        {
            DrugUnit selectedDrugUnit = await dbContext.DrugUnits.FirstOrDefaultAsync(drugUnit => drugUnit.DrugUnitId == drugUnitId);
            if (selectedDrugUnit == null) throw new NotFoundException($"DrugUnit with id {drugUnitId} not found.");

            Site selectedSite = await dbContext.Sites.FirstOrDefaultAsync(site => site.SiteId == siteId);
            if (selectedSite == null) throw new NotFoundException($"Site with id {siteId} not found.");

            selectedDrugUnit.DepotId = null;
            selectedDrugUnit.SiteId = selectedSite.SiteId;

            // Mark the entity (drugUnit) as changed => EFC knows to don't create a new one but to update the existing one
            dbContext.Entry(selectedDrugUnit).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "DrugUnit");
        }
    }
}