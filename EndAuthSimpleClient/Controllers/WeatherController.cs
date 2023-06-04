using EndAuth.Shared.Dtos;
using EndAuth.Shared.Identities.Commands.Login;
using EndAuthSimpleClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace EndAuthSimpleClient.Controllers;

public class WeatherController : Controller
{
    //[Authorize]
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
            return View(weatherForecasts);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // Handle unauthorized access
            return RedirectToAction("Login", "Auth");
        }
        else
        {
            // Handle other error cases
            return View("Error");
        }
    }
}