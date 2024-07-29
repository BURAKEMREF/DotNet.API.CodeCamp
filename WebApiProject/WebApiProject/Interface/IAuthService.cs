using Microsoft.AspNetCore.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Interface
{
    public interface IAuthService
    {
        public Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);
        public Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request);



    }
}
