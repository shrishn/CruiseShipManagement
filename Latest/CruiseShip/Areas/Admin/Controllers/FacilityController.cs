using CruiseShip.Data;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CruiseShip.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class FacilityController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        public FacilityController(IUnitOfWork unitOfWork,ApplicationDbContext db,IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            List<Facility> objFacilityList = _unitOfWork.Facility.GetAll(includeProperties: "CreatedByUser").ToList();
            
            return View(objFacilityList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Facility facility = new Facility();

            // Check if id is null or zero
            if (id == null || id == 0)
            {
                // Return view with new facility
                return View(facility);
            }
            else
            {
                // Retrieve the existing facility from the database
                facility = _unitOfWork.Facility.Get(f => f.Id == id);

                // Return the view with the existing facility
                return View(facility);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Facility obj, IFormFile? file)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Ensure CreatedBy is set during the POST request
                obj.CreatedBy = user.Id; // Assign the current user's ID
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string facilityPath = Path.Combine(wwwRootPath, @"images\facility");
                    if (!string.IsNullOrEmpty(obj.ImageURL))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);

                    }
                    using (var fileStream = new FileStream(Path.Combine(facilityPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.ImageURL = @"\images\facility\" + filename;
                }
                if (obj.Id == 0)
                {
                    _unitOfWork.Facility.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Facility Created Successfully";

                }
                else
                {
                    _unitOfWork.Facility.Update(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Facility Updated Successfully";

                }

                return RedirectToAction("Index");
            }
            else
            {
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
