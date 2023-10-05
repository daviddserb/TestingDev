using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain.CorrelationService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DavidSerb.Web.Controllers
{
    public class DepotController : Controller
    {
        public static AppDbContext dbContext = new AppDbContext();
        public static DepotCorrelationService depotCorrelationService = new DepotCorrelationService(dbContext);

        [HttpGet, Route("")]
        public async Task<ActionResult> Index()
        {
            List<Depot> depots = await dbContext.Depots
                .AsNoTracking()
                .Include(Depot => Depot.Countries)
                .ToListAsync();
            if (!depots.Any()) return HttpNotFound();

            return View(depots);
        }

        [HttpGet]
        public ActionResult DepotUnits()
        {
            List<CorrelateData> depotUnits = depotCorrelationService.CorrelateData();
            return View(depotUnits);
        }

        [HttpGet, Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(Depot depot)
        {
            if (!ModelState.IsValid) return View();

            await dbContext.Depots.AddAsync(depot);
            await dbContext.SaveChangesAsync();

            return Redirect("./Index");
        }

        [HttpGet, Route("edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            Depot selectedDepot = dbContext.Depots.FirstOrDefault(depot => depot.DepotId == id.ToString());
            if (selectedDepot == null) return HttpNotFound();

            return View(selectedDepot);
        }

        public async Task<ActionResult> Edit(int id, Depot editedDepot)
        {
            if (!ModelState.IsValid) return View();

            if (id.ToString() != editedDepot.DepotId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Depot existingDepot = await dbContext.Depots.FindAsync(editedDepot.DepotId);
            if (existingDepot == null) return HttpNotFound();

            existingDepot.DepotName = editedDepot.DepotName;
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            Depot selectedDepot = await dbContext.Depots.FirstOrDefaultAsync(depot => depot.DepotId == id.ToString());
            if (selectedDepot == null) return HttpNotFound();

            dbContext.Remove(selectedDepot);
            await dbContext.SaveChangesAsync();

            return Redirect("../Index");
        }
    }
}