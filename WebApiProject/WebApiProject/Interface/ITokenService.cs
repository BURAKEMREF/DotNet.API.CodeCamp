using WebApiProject.Models;

namespace WebApiProject.Interface
{
    public interface ITokenService
    {
        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request);
    }
}
