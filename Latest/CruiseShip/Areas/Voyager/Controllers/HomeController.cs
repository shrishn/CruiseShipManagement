using System.Diagnostics;
using CruiseShip.Data.Repository;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CruiseShip.Areas.Voyager.Controllers
{
    [Area("Voyager")]
    [Authorize(Roles = "Voyager")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;        
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7061/api/Facilities";

        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
            _httpClient = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Facility()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var facility = JsonConvert.DeserializeObject<List<Facility>>(jsonData);
                return View(facility);
            }
            return View(new List<Facility>());
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
