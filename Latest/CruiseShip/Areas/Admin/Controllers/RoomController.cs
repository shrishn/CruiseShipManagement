using CruiseShip.Data;
using CruiseShip.Data.Repository;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CruiseShip.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        
        public RoomController(IUnitOfWork unitOfWork,ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
       
        public IActionResult Index()
        {
   
            List<Room> objroomList = _unitOfWork.Room.GetAll().ToList();
            return View(objroomList);
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

            IEnumerable<SelectListItem> AdminUsers = adminUsers.ToList();
            //ViewBag.AdminUsers = AdminUsers;
            ViewData["AdminUsers"] = AdminUsers;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Room obj)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Room.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Room Created Successfully";
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
            Room roomFromDb = _unitOfWork.Room.Get(f => f.Id == id);
            if (roomFromDb == null)
            {
                return NotFound();
            }
            return View(roomFromDb);

        }
        [HttpPost]
        public IActionResult Edit(Room obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Room.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Room Updated Successfully";
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
            Room? roomFromDb = _unitOfWork.Room.Get(f => f.Id == id);

            if (roomFromDb == null)
            {
                return NotFound();
            }
            return View(roomFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Room? obj = _unitOfWork.Room.Get(f => f.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Room.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Room Deleted Successfully";
            return RedirectToAction("Index");


        }
    }
}
