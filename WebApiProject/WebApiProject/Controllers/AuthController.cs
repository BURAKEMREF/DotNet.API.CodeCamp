using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts;
using WebApiProject.Entities;
using WebApiProject.Interface;
using WebApiProject.Models;
using WebApiProject.Services;


namespace WebApiProject.Controllers;



[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request)

    {
        var result = await authService.RegisterUserAsync(request);
        return result;
    }



    [HttpPost("LoginUser")]
    [AllowAnonymous]
    public async Task<ActionResult<UserLoginResponse>> LoginUserAsync([FromBody] UserLoginRequest request)
    {
        var result = await authService.LoginUserAsync(request);

        return result;
    }
}

