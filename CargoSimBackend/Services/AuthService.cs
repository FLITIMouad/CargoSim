using CargoSimBackend.Services.Infrastructure;

namespace CargoSimBackend;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string getToken()
    {
        var test = this._httpContextAccessor.HttpContext.User;
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1vdWFkIiwibmJmIjoxNzIwNDY1NjQzLCJleHAiOjE3MjEwNzA0NDMsImlhdCI6MTcyMDQ2NTY0M30.bY3h-KgpEhKV-PIzUbqtZTp3PhJeNFHWzDwL44auxQs";
        //this._httpContextAccessor.HttpContext.User.FindFirst("Cookies")?.Value;
        return token;
    }
}