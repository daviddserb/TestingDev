using DavidSerb.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DavidSerb.Web.Controllers
{
    [Route("country")]
    public class CountryController : Controller
    {
        public static AppDataContext dbContext = new AppDataContext();

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
            await dbContext.SaveChangesAsync(); // ??? TODO: sa vad daca ajuta await => daca nu, scot Task<> din method's signature. (cred ca ajuta, altfel da eroare: A second operation started on this context before a previous operation completed. This is usually caused by different threads using the same instance of DbContext.)

            return Redirect("./Index");
        }

        // ??? TODO: daca schimb numele parametrului din id int countryId => nu mai merge.
        [HttpGet, Route("edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            Country selectedCountry = dbContext.Countries.FirstOrDefault(country => country.CountryId == id.ToString());
            if (selectedCountry == null) return HttpNotFound();

            return View(selectedCountry);
        }

        // ??? TODO: daca pun verbul HttpPut si Route => nu mai merge (pe tutoriale, la Edit, foloseaca verbul HttpPost, de ce?)
        //[HttpPut, Route("edit/{id:int}")]
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

        // ??? - TODO: de ce daca ii pun HttpDelete si Route nu mai imi intra in ruta asta...
        //[HttpDelete, Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            Country selectedCountry = dbContext.Countries.FirstOrDefault(country => country.CountryId == id.ToString());
            if (selectedCountry == null) return HttpNotFound();

            dbContext.Remove(selectedCountry);
            dbContext.SaveChanges();

            return Redirect("../Index");
        }
    }
}