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
            var User =  _context.Users.Where( x => x.UserName == request.Username).FirstOrDefault();

            if (User != null)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "Kullanıcı zaten kayıtlı!";
                return response;
            }

            GenerateTokenRequest generateTokenRequest = new GenerateTokenRequest();
            generateTokenRequest.Username = request.Username;

            var token = _tokenService.GenerateToken(generateTokenRequest);

            var passwordHash = ComputeSha256Hash(token.Result.Token);

            var user = new User
            {
                UserName = request.Username,
                Password = passwordHash,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.StatusCode =(int) HttpStatusCode.OK;
            response.Message = "Kayıt başarlı";
            return response;

        }


        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            
            GenerateTokenRequest generateTokenRequest = new GenerateTokenRequest();
            UserLoginResponse response = new();
            var User = await _context.Users.Where(u => u.UserName == request.Username).ToArrayAsync();

            if (User == null)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "User not found!";
                return response;
                
            }
            var user = new User
            {
                UserName = request.Username,
                Password = request.Password,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
            };
            generateTokenRequest.Username = request.Username;

            var token = _tokenService.GenerateToken(generateTokenRequest);

            var passwordHash = ComputeSha256Hash(token.Result.Token);

            if (user.Password == request.Password)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message =" Login to the system successful";
                return response;
                
            }
            response.StatusCode = (int)HttpStatusCode.OK;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return response;




            /*  if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
              {
                  throw new ArgumentNullException(nameof(request));
              }

              if (request.Username == "emre" && request.Password == "1")
              {
                  var generatedTokenInformation = await tokenService.GenerateToken(new GenerateTokenRequest { Username = request.Username });
                  response.Username = request.Username;
                  response.AuthenticateResult = true;
                  response.AuthToken = generatedTokenInformation.Token;
                  response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;
              }

              return response;
            */

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
