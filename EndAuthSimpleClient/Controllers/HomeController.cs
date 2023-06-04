using EndAuth.Shared.Dtos;
using EndAuth.Shared.Identities.Commands.Login;
using EndAuthSimpleClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace EndAuthSimpleClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }



        public async Task<IActionResult> OldLogin()
        {
            using HttpClient client = new();
            var response = await client.PostAsJsonAsync<LoginUserCommand>("https://localhost:7207/api/identities/login", new("DefaultAdmin@default.com", "Default123$"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();
                using HttpClient apiClient = new();
                apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result!.AccessToken);
                var apiresponse = await apiClient.GetAsync("https://localhost:7017/weatherforecast");
                if (apiresponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("hapi hapi hapi");
                }
                else
                {
                    Console.WriteLine(">:(");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}