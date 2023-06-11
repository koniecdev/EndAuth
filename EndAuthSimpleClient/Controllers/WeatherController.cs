using EndAuthSimpleClient.Attributes;
using EndAuthSimpleClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EndAuthSimpleClient.Controllers;

public class WeatherController : Controller
{
    [JwtAuthentication]
    [HttpGet]
    public async Task<IActionResult> GetWeatherForecast()
    {
        // Retrieve the JWT token from the cookie
        var jwtToken = Request.Cookies["jwt_token"];

        // Create a new HttpClient with the JWT token in the Authorization header
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        // Send a GET request to the Main API's weather forecast endpoint
        var response = await httpClient.GetAsync("https://localhost:7017/weatherforecast");

        if (response.IsSuccessStatusCode)
        {
            // Process the successful response from the Main API
            var weatherForecasts = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
            Console.WriteLine("Success");
            return LocalRedirect("/");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // Handle unauthorized access
            Console.WriteLine("Unauthorized");
            return RedirectToAction("Login", "Auth");
        }
        else
        {
            // Handle other error cases
            return View("Error");
        }
    }
}