using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DavidSerb.Web.Controllers
{
    public class DrugUnitController : Controller
    {
        public static AppDbContext dbContext = new AppDbContext();

        [Route("")]
        public async Task<ActionResult> Index()
        {
            List<DrugUnit> drugUnits = await dbContext.DrugUnits
                .AsNoTracking()
                .Include(drugUnit => drugUnit.Depot)
                .Include(drugUnit => drugUnit.DrugType)
                .Include(drugUnit => drugUnit.Site)
                .ToListAsync();
            if (!drugUnits.Any()) return HttpNotFound();

            return View(drugUnits);
        }

        [HttpGet]
        public async Task<ActionResult> GroupedDrugUnits()
        {
            IList<DrugUnit> drugUnits = await dbContext.DrugUnits
                .Include(drugUnit => drugUnit.DrugType)
                .ToListAsync();

            Dictionary<string, List<DrugUnit>> drugUnitsDict = drugUnits.ToGroupedDrugUnits();

            GroupedDrugUnitsViewModel groupedDrugUnitsVM = new GroupedDrugUnitsViewModel { DrugUnitsDict = drugUnitsDict };

            return View(groupedDrugUnitsVM);
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

            await dbContext.DrugUnits.AddAsync(drugUnit);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id}")]
        public async Task<ActionResult> Edit(string id)
        {
            DrugUnit selectedDrugUnit = await dbContext.DrugUnits.FirstOrDefaultAsync(drugUnit => drugUnit.DrugUnitId == id);
            if (selectedDrugUnit == null) return HttpNotFound();

            return View(selectedDrugUnit);
        }

        public async Task<ActionResult> Edit(string id, DrugUnit editedDrugUnit)
        {
            if (!ModelState.IsValid) return View();

            if (id != editedDrugUnit.DrugUnitId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            DrugUnit existingDrugUnit = await dbContext.DrugUnits.FindAsync(editedDrugUnit.DrugUnitId);
            if (existingDrugUnit == null) return HttpNotFound();

            existingDrugUnit.PickNumber = editedDrugUnit.PickNumber;
            existingDrugUnit.DepotId = editedDrugUnit.DepotId;
            existingDrugUnit.DrugTypeId = editedDrugUnit.DrugTypeId;
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            DrugUnit selectedDrugUnit = await dbContext.DrugUnits.FirstOrDefaultAsync(drugUnit => drugUnit.DrugUnitId == id);
            if (selectedDrugUnit == null) return HttpNotFound();

            dbContext.Remove(selectedDrugUnit);
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }
    }
}