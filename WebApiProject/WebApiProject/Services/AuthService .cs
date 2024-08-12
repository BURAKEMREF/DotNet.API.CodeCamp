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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using WebApiProject.Constants;




namespace WebApiProject.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenService _tokenService;
        private readonly WebContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, WebContext context, IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
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
            // Kullanıcı türüne göre belirli bir kod döndür
            if (request.UserType == 2)
            {
                response.StatusCode = 2;
                response.Message = "Kayıt başarılı: Customer";
            }
            else if (request.UserType == 3)
            {
                response.StatusCode = 3;
                response.Message = "Kayıt başarılı: Vendor";
            }
            else if (request.UserType == 1)
            {
                response.StatusCode = 1;
                response.Message = "Kayıt başarısız:Yönetici sadece Sultanı Bahtiyarı Emre cezeriye verebilir.";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Kayıt başarılı";
            }
            var newUser = new User
            {
                UserName = request.Username,
                Password = passwordHash,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
                UserType = request.UserType,
                IsActive = true,
            };
            response.Username = request.Username;
            response.Email = request.Email;
            response.UserType = request.UserType;
            response.Password = request.Password;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = "Kayıt başarlı";
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new();

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Username or password is missing!";
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
            // Kullanıcının aktiflik durumu kontrolü
            // Kullanıcının UserType'ını kontrol et
            if (user.UserType == 1)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Login is successful for Admin!";
            }
            else if (user.UserType == 2)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Login is successful for Customer!";
            }
            else if (user.UserType == 3)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Login is successful for Vendor!";
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Invalid UserType!";
                return response;
            }
            if ((bool)!user.IsActive)
            {
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                response.Message = "User is not active!";
                return response;
            }

            // Kullanıcı rolünü al

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
