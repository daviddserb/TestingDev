using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DavidSerb.Web.Controllers
{
    public class CountryController : Controller
    {
        public static AppDbContext dbContext = new AppDbContext();

        [HttpGet, Route("")]
        public async Task<ActionResult> Index()
        {
            List<Country> countries = await dbContext.Countries
                .AsNoTracking()
                .Include(country => country.Depot)
                .Include(country => country.Sites)
                .ToListAsync();
            if (!countries.Any()) return HttpNotFound();

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

        /// <summary>
        /// Get form's data after creating country
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(Country country)
        {
            if (!ModelState.IsValid) return View();

            await dbContext.Countries.AddAsync(country);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            Country selectedCountry = await dbContext.Countries.FirstOrDefaultAsync(country => country.CountryId == id.ToString());
            if (selectedCountry == null) return HttpNotFound();

            return View(selectedCountry);
        }

        public async Task<ActionResult> Edit(int id, Country editedCountry)
        {
            if (!ModelState.IsValid) return View();

            if (id.ToString() != editedCountry.CountryId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Country existingCountry = await dbContext.Countries.FindAsync(editedCountry.CountryId);
            if (existingCountry == null) return HttpNotFound();

            existingCountry.CountryName = editedCountry.CountryName;
            existingCountry.DepotId = editedCountry.DepotId;
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            Country selectedCountry = await dbContext.Countries.FirstOrDefaultAsync(country => country.CountryId == id);
            if (selectedCountry == null) return HttpNotFound();

            dbContext.Remove(selectedCountry);
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }
    }
}