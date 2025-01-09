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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FacilityController(IUnitOfWork unitOfWork,ApplicationDbContext db,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _webHostEnvironment = webHostEnvironment;   
        }

        public IActionResult Index()
        {
            List<Facility> objFacilityList = _unitOfWork.Facility.GetAll(includeProperties: "CreatedByUser").ToList();
            
            return View(objFacilityList);
        }

        public IActionResult Upsert(int? id)
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
            if (id == null || id==0)
            {
                return View(facilityVM);
            }
            else
            {
                facilityVM.Facility = _unitOfWork.Facility.Get(f => f.Id == id);
                
                return View(facilityVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(FacilityVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string facilityPath = Path.Combine(wwwRootPath, @"images\facility");
                    if (!string.IsNullOrEmpty(obj.Facility.ImageURL))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Facility.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);

                    }
                    using (var fileStream = new FileStream(Path.Combine(facilityPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Facility.ImageURL = @"\images\facility\" + filename;
                }
                if (obj.Facility.Id == 0)
                {
                    _unitOfWork.Facility.Add(obj.Facility);
                    _unitOfWork.Save();
                    TempData["success"] = "Facility Created Successfully";

                }
                else
                {
                    _unitOfWork.Facility.Update(obj.Facility);
                    _unitOfWork.Save();
                    TempData["success"] = "Facility Updated Successfully";

                }

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
        }

                

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Facility> objFacilityList = _unitOfWork.Facility.GetAll(includeProperties: "CreatedByUser").ToList();
            return Json(new { data = objFacilityList });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Facility? obj = _unitOfWork.Facility.Get(f => f.Id == id);
            if (obj == null)
            {
                return Json(new {success=false,message="Error while deleting"});
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string facilityPath = Path.Combine(wwwRootPath, @"images\facility");
            var ImagePath = Path.Combine(wwwRootPath, obj.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(ImagePath))
                System.IO.File.Delete(ImagePath);
            _unitOfWork.Facility.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
            }
        #endregion
    }
}
