using System.Security.Claims;
using System.Text.RegularExpressions;
using CargoSimBackend.Services.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CargoSimBackend;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IRestClient _restClient;

    public UserController(IRestClient restClient)
    {
        this._restClient = restClient;
    }

    [HttpGet("login")]
    public IActionResult Login(string returnUrl = null)
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("StartSim", "Order"); 
        }
        return Ok(new { Message = "Please log in.", ReturnUrl = returnUrl });
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] UserCredentials userCredentials)
    {

        try
        {
            var token = await this._restClient.SignIn(userCredentials?.username, userCredentials?.password);
            if (!string.IsNullOrEmpty(token))
            {
                var claims = new List<Claim>
            {
                new Claim("Token", token)
            };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                this.HttpContext.User.AddIdentity(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                Ok(token);
            }
            return Unauthorized();

        }
        catch (Exception ex)
        {
            string message = Regex.Replace(ex.Message, @"[0-9]+", "****");
            return StatusCode(500, new { message = message });
        }

    }
    [HttpPost("logout")]
    public async Task<ActionResult<string>> Logout()
    {

        try
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        catch (Exception ex)
        {
            string message = Regex.Replace(ex.Message, @"[0-9]+", "****");
            return StatusCode(500, new { message = message });
        }

    }
}


public record UserCredentials(string username, string password);