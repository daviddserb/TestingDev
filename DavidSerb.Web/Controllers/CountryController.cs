using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DavidSerb.Web.Controllers
{
    public class CountryController : Controller
    {
        public static AppDbContext dbContext = new AppDbContext();

        [Route("")]
        public ActionResult Index()
        {
            List<Country> countries = dbContext.Countries
                .Include(country => country.Depot)
                .Include(country => country.Sites)
                .ToList();

            return View(countries);
        }

        /// <summary>
        /// Display form to create country
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // ??? TODO: ma obliga sa creez un default constructor in Country
        /// <summary>
        /// Get form's data after creating country
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(Country country)
        {
            if (!ModelState.IsValid) return View();

            dbContext.Countries.Add(country);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            Country selectedCountry = dbContext.Countries.FirstOrDefault(country => country.CountryId == id.ToString());
            if (selectedCountry == null) return HttpNotFound();

            return View(selectedCountry);
        }

        public ActionResult Edit(int id, Country editedCountry)
        {
            if (!ModelState.IsValid) return View();

            if (id.ToString() != editedCountry.CountryId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Country existingCountry = dbContext.Countries.Find(editedCountry.CountryId);
            if (existingCountry == null) return HttpNotFound();

            existingCountry.CountryName = editedCountry.CountryName;
            existingCountry.DepotId = editedCountry.DepotId;
            dbContext.SaveChanges();

            return Redirect("../Index");
        }

        public ActionResult Delete(string id)
        {
            Country selectedCountry = dbContext.Countries.FirstOrDefault(country => country.CountryId == id);
            if (selectedCountry == null) return HttpNotFound();

            dbContext.Remove(selectedCountry);
            dbContext.SaveChanges();

            return Redirect("../Index");
        }
    }
}