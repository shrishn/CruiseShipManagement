using CruiseShip.Data;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Controllers
{
    public class FacilityController : Controller
    {
        private readonly IFacilityRepository _facilityRepo;
        public FacilityController(IFacilityRepository facilityRepo)
        {
            _facilityRepo = facilityRepo;
        }

        public IActionResult Index()
        {
            List<Facility> objFacilityList = _facilityRepo.GetAll().ToList();
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

                _facilityRepo.Add(obj);
                _facilityRepo.Save();
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
            Facility facilityFromDb = _facilityRepo.Get(f=>f.Id==id);
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
                _facilityRepo.Update(obj);
                _facilityRepo.Save();
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
            Facility? facilityFromDb = _facilityRepo.Get(f => f.Id == id);

            if (facilityFromDb == null)
            {
                return NotFound();
            }
            return View(facilityFromDb); 
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Facility? obj = _facilityRepo.Get(f => f.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _facilityRepo.Remove(obj);
            _facilityRepo.Save ();
            TempData["success"] = "Facility Deleted Successfully";
            return RedirectToAction("Index");

            
        }
    }
}
