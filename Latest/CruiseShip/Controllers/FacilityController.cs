using CruiseShip.Data;
using CruiseShip.Models;
using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Controllers
{
    public class FacilityController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FacilityController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Facility> objFacilityList = _db.Facilities.ToList();
            return View(objFacilityList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Facility obj)
        {
            if (ModelState.IsValid)
            {

                _db.Facilities.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Facility Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Facility facilityFromDb = _db.Facilities.Find(id);
            if (facilityFromDb == null)
            {
                return NotFound();
            }
            return View(facilityFromDb);

        }
        [HttpPost]
        public IActionResult Edit(Facility obj)
        {
            if (ModelState.IsValid)
            {
                _db.Facilities.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Facility Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Facility? facilityFromDb = _db.Facilities.Where(f => f.Id == id).FirstOrDefault();

            if (facilityFromDb == null)
            {
                return NotFound();
            }
            return View(facilityFromDb); 
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Facility? obj = _db.Facilities.Where(f => f.Id == id).FirstOrDefault();
            if (obj == null)
            {
                return NotFound();
            }
            _db.Facilities.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Facility Deleted Successfully";
            return RedirectToAction("Index");

            
        }
    }
}
