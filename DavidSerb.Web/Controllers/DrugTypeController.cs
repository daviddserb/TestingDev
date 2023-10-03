using DavidSerb.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DavidSerb.Web.Controllers
{
    [Route("drug-type")]
    public class DrugTypeController : Controller
    {
        public static AppDataContext dbContext = new AppDataContext();

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
    }
}