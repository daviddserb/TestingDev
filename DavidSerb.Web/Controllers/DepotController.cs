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
    [Route("depot")]
    public class DepotController : Controller
    {
        public static AppDataContext dbContext = new AppDataContext();

        [Route("")]
        public ActionResult Index()
        {
            List<Depot> depots = dbContext.Depots
                .Include(Depot => Depot.Countries)
                .ToList();

            return View(depots);
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

            dbContext.Depots.Add(depot);
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

        public ActionResult Edit(int id, Depot editedDepot)
        {
            if (!ModelState.IsValid) return View();

            if (id.ToString() != editedDepot.DepotId) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Depot existingDepot = dbContext.Depots.Find(editedDepot.DepotId);
            if (existingDepot == null) return HttpNotFound();

            existingDepot.DepotName = editedDepot.DepotName;
            dbContext.SaveChanges();

            return Redirect("../Index");
        }

        public ActionResult Delete(int id)
        {
            Depot selectedDepot = dbContext.Depots.FirstOrDefault(depot => depot.DepotId == id.ToString());
            if (selectedDepot == null) return HttpNotFound();

            dbContext.Remove(selectedDepot);
            dbContext.SaveChanges();

            return Redirect("../Index");
        }
    }
}