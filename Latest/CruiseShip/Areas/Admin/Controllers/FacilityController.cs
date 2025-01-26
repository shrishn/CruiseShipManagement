using CruiseShip.Data;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CruiseShip.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FacilityController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7061/api/Facilities";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
       
        public FacilityController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _httpClient = new HttpClient();
            

        }

        public IActionResult Index()
        {
            return View();
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
                var response = await _httpClient.GetAsync(_apiBaseUrl+"/"+id);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    facility = JsonConvert.DeserializeObject<Facility>(jsonData);
                    
                }
                else
                {
                    return NotFound();
                }
                //facility = result;
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
                    var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(obj), System.Text.Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_apiBaseUrl, jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Facility Created Successfully";
                    }
                    else
                    {
                        TempData["error"] = "Failed to create the facility.";
                        return View(obj);
                    }
                }
                else
                {
                    var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(obj), System.Text.Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync(_apiBaseUrl + "/" + obj.Id, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Facility Updated Successfully";
                    }
                    else
                    {
                        TempData["error"] = "Failed to update the facility.";
                        return View(obj);
                    }

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
        public async Task<IActionResult> GetAll()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var facility = JsonConvert.DeserializeObject<List<Facility>>(jsonData);
                return Json(new { data = facility });
            }

            TempData["ErrorMessage"] = "Error retrieving book list.";
            
           
            return Json(new { data = new List<Facility>() });
        }
        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    Facility? obj = _unitOfWork.Facility.Get(f => f.Id == id);
        //    if (obj == null)
        //    {
        //        return Json(new { success = false, message = "Error while deleting" });
        //    }
        //    string wwwRootPath = _webHostEnvironment.WebRootPath;
        //    string facilityPath = Path.Combine(wwwRootPath, @"images\facility");
        //    var ImagePath = Path.Combine(wwwRootPath, obj.ImageURL.TrimStart('\\'));
        //    if (System.IO.File.Exists(ImagePath))
        //        System.IO.File.Delete(ImagePath);
        //    _unitOfWork.Facility.Remove(obj);
        //    _unitOfWork.Save();
        //    return Json(new { success = true, message = "Delete Successful" });
        //}
        #endregion
    }
}
