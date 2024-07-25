using WebApiProject.Models;

namespace WebApiProject.Interface
{
    public interface IAuthService
    {
        public Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);
    }
}
