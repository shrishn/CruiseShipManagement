using CruiseShip.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CruiseShip.Areas.Voyager.Controllers
{
    [Area("Voyager")]
    [Authorize(Roles = "Voyager")]

    public class BillsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBills = "https://localhost:7061/api/Bills";
        private UserManager<IdentityUser> _userManager;
        public BillsController(UserManager<IdentityUser> usermanager)
        {
            _httpClient = new HttpClient();
            _userManager = usermanager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User) as UserProfile;
            var userid = user.Id;

            var response = await _httpClient.GetAsync(_apiBills + "/user/" + userid);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var bills = JsonConvert.DeserializeObject<List<Bill>>(jsonData);
                return View(bills);
            }
            return View(new List<Bill>() );
            
        }
    }
}
