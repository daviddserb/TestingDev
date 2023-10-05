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
        public async Task<ActionResult> Index()
        {
            List<DrugType> drugTypes = await dbContext.DrugTypes
                .AsNoTracking()
                .ToListAsync();
            if (!drugTypes.Any()) return HttpNotFound();

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

            await dbContext.DrugTypes.AddAsync(drugType);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            DrugType selectedDrugType = await dbContext.DrugTypes.FirstOrDefaultAsync(drugType => drugType.DrugTypeId == id.ToString());
            if (selectedDrugType == null) return HttpNotFound();

            return View(selectedDrugType);
        }

        public async Task<ActionResult> Edit(int id, DrugType editedDrugType)
        {
            if (!ModelState.IsValid) return View();

            if (id.ToString() != editedDrugType.DrugTypeId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            DrugType existingDrugType = await dbContext.DrugTypes.FindAsync(editedDrugType.DrugTypeId);
            if (existingDrugType == null) return HttpNotFound();

            existingDrugType.DrugTypeName = editedDrugType.DrugTypeName;
            existingDrugType.WeightInPounds = editedDrugType.WeightInPounds;
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            DrugType selectedDrugType = await dbContext.DrugTypes.FirstOrDefaultAsync(drugType => drugType.DrugTypeId == id.ToString());
            if (selectedDrugType == null) return HttpNotFound();

            dbContext.Remove(selectedDrugType);
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }

        public async Task<ActionResult> DisplayWeight()
        {
            List<Depot> depots = await dbContext.Depots
                .ToListAsync();

            List<DrugUnit> drugUnits = await dbContext.DrugUnits
                .Include(drugUnit => drugUnit.Depot)
                .Include(drugUnit => drugUnit.DrugType)
                .ToListAsync();

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