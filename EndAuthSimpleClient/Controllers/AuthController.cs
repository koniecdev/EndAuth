using EndAuth.Shared.Dtos;
using EndAuth.Shared.Identities.Commands.Login;
using EndAuthSimpleClient.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EndAuthSimpleClient.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        // Send the login request to the authentication server
        var loginUserCommand = new LoginUserCommand("DefaultAdmin@default.com", "Default123$");
        using var httpClient = new HttpClient();
        var response = await httpClient.PostAsJsonAsync("https://localhost:7207/api/identities/login", loginUserCommand);

        if (response.IsSuccessStatusCode)
        {
            var authSuccessResponse = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();

            // Store the tokens or use them for subsequent API calls
            // Here, we're storing the access token in a secure cookie named "jwt_token"
            Response.Cookies.Append("jwt_token", authSuccessResponse?.AccessToken!);

            return RedirectToAction("Index", "Home");
        }

        // Handle the login failure
        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return View(loginUserCommand);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Clear the authentication cookies and sign out the user
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}