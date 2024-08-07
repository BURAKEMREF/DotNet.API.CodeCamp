using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts;
using WebApiProject.Entities;
using WebApiProject.Interface;
using WebApiProject.Models;
using WebApiProject.Services;
using WebApiProject.Context;

namespace WebApiProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    readonly IAuthService _authService;
    

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("register")]
    public async Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request)

    {
        var result = await _authService.RegisterUserAsync(request);
        


        return result;
    }



    [HttpPost("LoginUser")]
    [Authorize(Policy = "Admin")]
    [AllowAnonymous]

    public async Task<ActionResult<UserLoginResponse>> LoginUserAsync([FromBody] UserLoginRequest request)
    {
        var result = await _authService.LoginUserAsync(request);

        return result;
    }

    //[Authorize(Policy = "Admin")]
    //public IActionResult AdminOnly()
    //{
    //    return View();
    //}

    //[Authorize(Policy = "Manager", Roles = "Admin")]
    //public IActionResult ManagerOnly()
    //{
    //    return View();
    //}

    //private IActionResult View()
    //{
    //    throw new NotImplementedException();
    //}

    //[Authorize(Policy = "Member")]
    //public IActionResult MemberOnly()
    //{
    //    return View();
    //}

    //[Authorize(Policy = "ActiveUserOnly")]
    //public IActionResult ActiveUserOnly()
    //{
    //    return View();
    //}
}

