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
    public class DrugTypeController : Controller
    {
        public static AppDbContext dbContext = new AppDbContext();

        [Route("")]
        public ActionResult Index()
        {
            List<DrugType> drugTypes = dbContext.DrugTypes.ToList();

            return View(drugTypes);
        }

        [HttpGet, Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(DrugType drugType)
        {
            if (!ModelState.IsValid) return View();

            dbContext.DrugTypes.Add(drugType);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            DrugType selectedDrugType = dbContext.DrugTypes.FirstOrDefault(drugType => drugType.DrugTypeId == id.ToString());
            if (selectedDrugType == null) return HttpNotFound();

            return View(selectedDrugType);
        }

        public ActionResult Edit(int id, DrugType editedDrugType)
        {
            if (!ModelState.IsValid) return View();

            if (id.ToString() != editedDrugType.DrugTypeId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            DrugType existingDrugType = dbContext.DrugTypes.Find(editedDrugType.DrugTypeId);
            if (existingDrugType == null) return HttpNotFound();

            existingDrugType.DrugTypeName = editedDrugType.DrugTypeName;
            existingDrugType.WeightInPounds = editedDrugType.WeightInPounds;
            dbContext.SaveChanges();

            return Redirect("../Index");
        }

        public ActionResult Delete(int id)
        {
            DrugType selectedDrugType = dbContext.DrugTypes.FirstOrDefault(drugType => drugType.DrugTypeId == id.ToString());
            if (selectedDrugType == null) return HttpNotFound();

            dbContext.Remove(selectedDrugType);
            dbContext.SaveChanges();

            return Redirect("../Index");
        }

        public ActionResult DisplayWeight()
        {
            List<Depot> depots = dbContext.Depots
                .ToList();

            List<DrugUnit> drugUnits = dbContext.DrugUnits
                .Include(drugUnit => drugUnit.Depot)
                .Include(drugUnit => drugUnit.DrugType)
                .ToList();

            const decimal poundsToKg = 2.2m;
            List<DepotWeightViewModel> depotsWithWeights = depots
                .Select(depot => new DepotWeightViewModel
                {
                    DepotName = depot.DepotName,
                    TotalWeight = drugUnits
                        .Where(drugUnit => drugUnit.DepotId == depot.DepotId && drugUnit.DrugType != null)
                        .Sum(drugUnit => Math.Round((drugUnit.DrugType.WeightInPounds / poundsToKg), 3))
                })
                .ToList();

            return View(depotsWithWeights);
        }
    }
}