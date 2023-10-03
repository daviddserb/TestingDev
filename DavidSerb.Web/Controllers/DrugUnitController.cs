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
    [Route("drug-unit")]
    public class DrugUnitController : Controller
    {
        public static AppDataContext dbContext = new AppDataContext();

        [Route("")]
        public ActionResult Index()
        {
            List<DrugUnit> drugUnits = dbContext.DrugUnits
                .Include(drugUnit => drugUnit.Depot)
                .Include(drugUnit => drugUnit.DrugType)
                .Include(drugUnit => drugUnit.Site)
                .ToList();

            return View(drugUnits);
        }

        [HttpGet, Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(DrugUnit drugUnit)
        {
            if (!ModelState.IsValid) return View();

            dbContext.DrugUnits.Add(drugUnit);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id}")]
        public ActionResult Edit(string id)
        {
            DrugUnit selectedDrugUnit = dbContext.DrugUnits.FirstOrDefault(drugUnit => drugUnit.DrugUnitId == id);
            if (selectedDrugUnit == null) return HttpNotFound();

            return View(selectedDrugUnit);
        }

        public ActionResult Edit(string id, DrugUnit editedDrugUnit)
        {
            if (!ModelState.IsValid) return View();

            if (id != editedDrugUnit.DrugUnitId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            DrugUnit existingDrugUnit = dbContext.DrugUnits.Find(editedDrugUnit.DrugUnitId);
            if (existingDrugUnit == null) return HttpNotFound();

            existingDrugUnit.PickNumber = editedDrugUnit.PickNumber;
            existingDrugUnit.DepotId = editedDrugUnit.DepotId;
            existingDrugUnit.DrugTypeId = editedDrugUnit.DrugTypeId;
            dbContext.SaveChanges();

            return Redirect("../Index");
        }

        public ActionResult Delete(string id)
        {
            DrugUnit selectedDrugUnit = dbContext.DrugUnits.FirstOrDefault(drugUnit => drugUnit.DrugUnitId == id);
            if (selectedDrugUnit == null) return HttpNotFound();

            dbContext.Remove(selectedDrugUnit);
            dbContext.SaveChanges();

            return Redirect("../Index");
        }
    }
}