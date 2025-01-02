using CruiseShip.Data;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using CruiseShip.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CruiseShip.Areas.Admin.Controllers
{
    public class FacilityController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        public FacilityController(IUnitOfWork unitOfWork,ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public IActionResult Index()
        {
            List<Facility> objFacilityList = _unitOfWork.Facility.GetAll().ToList();
            
            return View(objFacilityList);
        }

        public IActionResult Create()
        {
            var adminRoleId = _db.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).FirstOrDefault();
            var adminUsers = from user in _db.Users
                             join userRole in _db.UserRoles on user.Id equals userRole.UserId
                             where userRole.RoleId == adminRoleId
                             select new SelectListItem
                             {
                                 Value = user.Id.ToString(),
                                 Text = user.UserName
                             };
            FacilityVM facilityVM = new()
            {
                Facility = new Facility(),
                AdminUsers = adminUsers.ToList()

            };
            return View(facilityVM);
        }
        [HttpPost]
        public IActionResult Create(FacilityVM obj)
        {
            
            if (ModelState.IsValid)
            {

                _unitOfWork.Facility.Add(obj.Facility);
                _unitOfWork.Save();
                TempData["success"] = "Facility Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                var adminRoleId = _db.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).FirstOrDefault();
                var adminUsers = from user in _db.Users
                                 join userRole in _db.UserRoles on user.Id equals userRole.UserId
                                 where userRole.RoleId == adminRoleId
                                 select new SelectListItem
                                 {
                                     Value = user.Id.ToString(),
                                     Text = user.UserName
                                 };
                obj.AdminUsers = adminUsers.ToList();
                return View(obj);
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
