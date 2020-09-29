using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using webapi.Controllers;
using webapi.Entities;
using webapi.Helpers;
using webapi.Infrastructure.Data;
using webapi.Infrastructure.Services.EmailService;
using webapi.Models.Auth;

namespace webapi.Services
{
    public interface IAuthenticationService
    {
        //account Authentication
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress);
        // AuthenticateResponse RefreshToken(string token, string ipAddress);
        Task RevokeToken();
        Task Register(RegisterRequest model, IUrlHelper url);
        Task<bool> VerifyEmail(string userId, string token);
        Task ForgotPassword(ForgotPasswordRequest model, IUrlHelper url);
        Task<bool> ResetPassword(ResetPasswordRequest model);
        
    }
    public class AuthenticationService : IAuthenticationService
    {
        private SignInManager<IdentityUser> signinManager;
        private UserManager<IdentityUser> userManager;
        private DataContext context;
        private IEmailSender emailSender;
        private readonly IMapper mapper;
        AppSettings appSettings;
        private IHttpContextAccessor httpContextAccessor;

        private ILogger logger;
        public AuthenticationService(DataContext context,
                              SignInManager<IdentityUser> signinManager,
                              UserManager<IdentityUser> userManager,
                              IEmailSender emailSender,
                              IOptions<AppSettings> appSettings,
                              IMapper mapper,
                              IHttpContextAccessor httpContextAccessor,
                              ILogger<AuthenticationService> logger)
        {
            this.signinManager = signinManager;
            this.userManager = userManager;
            this.context = context;
            this.emailSender = emailSender;
            this.appSettings = appSettings.Value;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null) throw new AppException("Cannot found " + model.UserName);
            if (user != null && !user.EmailConfirmed) throw new AppException("Please confirm your email before login.");

            var result = await signinManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (!result.Succeeded) throw new AppException("Password is incorrect");

            return await GenerateToken(user);
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, IUrlHelper Url)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) throw new AppException($@"{model.Email} does not exist");

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var decodeToken = HttpUtility.UrlEncode(token);
            //  var resetUrl = appSettings.Client_URL + $@"/#/account/reset-password?token={decodeToken}&email={model.Email}";
            var resetUrl = appSettings.Client_URL + $@"/#/account/reset-password?token={decodeToken}&email={model.Email}";
            var message = new Message(new string[] { model.Email }, "Reset password",
                $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>", null);

            await emailSender.SendEmailAsync(message);

        }

        public async Task Register(RegisterRequest model, IUrlHelper url)
        {
            var applicationUser = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await userManager.CreateAsync(applicationUser, model.Password);
            if (!result.Succeeded && result.Errors.Any()) throw new AppException(result.Errors.First().Description);
            result = await userManager.AddToRoleAsync(applicationUser, model.Role.ToString());

            if (!result.Succeeded && result.Errors.Any()) throw new AppException(result.Errors.First().Description);
            var account = await AddAccount(model, applicationUser.Id);
            string confirmationToken = userManager.GenerateEmailConfirmationTokenAsync(applicationUser).Result;

            string confirmationLink = url.Action("VerifyEmail", "Accounts",
              new
              {
                  userid = applicationUser.Id,
                  token = confirmationToken
              },
              httpContextAccessor.HttpContext.Request.Scheme);

            var message = new Message(new string[] { model.Email }, "Registration successful", "Before you can login, please confirm your email, by clicking " + confirmationLink, null);

            await emailSender.SendEmailAsync(message);
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) throw new AppException($@"{model.Email} does not exist");
            //var decodeToken = HttpUtility.UrlDecode(model.Token); //not need to decode
            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            return result.Succeeded;
        }

        public async Task RevokeToken()
        {
            await signinManager.SignOutAsync();
        }

        public async Task<bool> VerifyEmail(string userId, string token)
        {
            IdentityUser user = await userManager.FindByIdAsync(userId);
            IdentityResult result = await userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        private async Task<AuthenticateResponse> GenerateToken(IdentityUser user)
        {
            var role = await userManager.GetRolesAsync(user);
            var account = context.Accounts.FirstOrDefault(x => x.IdentityId == user.Id);
            IdentityOptions options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("accountId",account.AccountId.ToString()),
                        new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            var response = mapper.Map<AuthenticateResponse>(account);
            response.Role = role.FirstOrDefault();
            response.JwtToken = token;
            return response;
        }

        private async Task<Account> AddAccount(RegisterRequest model, string identityId)
        {
            var account = mapper.Map<Account>(model);
            account.IdentityId = identityId;
            context.Accounts.Add(account);
            await context.SaveChangesAsync();
            return account;
        }
    }


}