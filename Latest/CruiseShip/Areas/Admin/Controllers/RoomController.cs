using CruiseShip.Data;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Areas.Admin.Controllers
{
    public class RoomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Room> objroomList = _unitOfWork.Room.GetAll().ToList();
            return View(objroomList);
        }

        public IActionResult Create()
        {
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
