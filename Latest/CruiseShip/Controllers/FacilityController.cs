using CruiseShip.Data;
using CruiseShip.Models;
using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Controllers
{
    public class FacilityController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FacilityController(ApplicationDbContext db) { 
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
                return RedirectToAction("Index");
            }
            return View();
            
        }
    }
}
