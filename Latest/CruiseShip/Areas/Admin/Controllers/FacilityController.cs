using CruiseShip.Data;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Areas.Admin.Controllers
{
    public class FacilityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FacilityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Facility> objFacilityList = _unitOfWork.Facility.GetAll().ToList();
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

                _unitOfWork.Facility.Add(obj);
                _unitOfWork.Save();
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
            Facility facilityFromDb = _unitOfWork.Facility.Get(f => f.Id == id);
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
                _unitOfWork.Facility.Update(obj);
                _unitOfWork.Save();
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
            Facility? facilityFromDb = _unitOfWork.Facility.Get(f => f.Id == id);

            if (facilityFromDb == null)
            {
                return NotFound();
            }
            return View(facilityFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Facility? obj = _unitOfWork.Facility.Get(f => f.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Facility.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Facility Deleted Successfully";
            return RedirectToAction("Index");


        }
    }
}
