using WebApiProject.Interface;
using WebApiProject.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;
using WebApiProject.Contracts;
using System.IdentityModel.Tokens.Jwt;
using WebApiProject.Entities;
using WebApiProject.Services;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Newtonsoft.Json.Linq;
using System.Net;
using System;




namespace WebApiProject.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenService _tokenService;
        private readonly WebContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;

        public AuthService(ITokenService tokenService, WebContext context)
        {
            this._tokenService = tokenService;
            this._context = context;    
        }

        public async Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request)
        {
            UserRegisterResponse response = new();
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == request.Username);
            if (user != null)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "Kullanıcı zaten kayıtlı!";

                return response;
            }

            var passwordHash = ComputeSha256Hash(request.Password);
            var newUser = new User
            {
                UserName = request.Username,
                Password = passwordHash,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            response.StatusCode =(int) HttpStatusCode.OK;
            response.Message = "Kayıt başarlı";

            return response;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new();

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "User is not found!";

                return response;
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == request.Username);
            if (user == null)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "User is not found!";

                return response;
            }

            string sha256Password = ComputeSha256Hash(request.Password);
            if (!sha256Password.Equals(user.Password, StringComparison.InvariantCultureIgnoreCase))
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Message = "Password is invalid";

                return response;
            }

            GenerateTokenRequest generateTokenRequest = new GenerateTokenRequest()
            {
                Username = request.Username
            };
            var token = _tokenService.GenerateToken(generateTokenRequest);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = "Login is successfull!";
            response.AuthToken = token.Result.Token;

            return response;
        }
       public string ComputeSha256Hash(string rawData)
        {
            using (var sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }


        }
    }
}
