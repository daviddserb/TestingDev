using DavidSerb.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DavidSerb.Web.Controllers
{
    [Route("site")]
    public class SiteController : Controller
    {
        public static AppDataContext dbContext = new AppDataContext();

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
    }
}