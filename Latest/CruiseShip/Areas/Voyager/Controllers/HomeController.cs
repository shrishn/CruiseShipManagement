//using System.Diagnostics;
//using CruiseShip.Data.Repository;
//using CruiseShip.Data.Repository.IRepository;
//using CruiseShip.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace CruiseShip.Areas.Voyager.Controllers
//{
//    [Area("Voyager")]
//    [Authorize(Roles = "Voyager")]
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly IUnitOfWork _unitOfWork;

//        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//            _logger = logger;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult Facility()
//        {
//            IEnumerable<Facility> faciltiesList = _unitOfWork.Facility.GetAll(includeProperties: "CreatedByUser");
//            return View(faciltiesList);
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
