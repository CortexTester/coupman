using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using webapi.Models;
using webapi.Services.EmailService;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using webapi.Services.Utils;
using System.Net.Http;
using System.Net;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private SignInManager<IdentityUser> signinManager;
        private UserManager<IdentityUser> userManager;
        private IConfiguration configuration;

        private DataContext context;
        private IEmailSender emailSender;

        private IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationSettings appSettings;

        public UserController(SignInManager<IdentityUser> mgr,
        UserManager<IdentityUser> usermgr,
        IConfiguration config,
        DataContext ctx,
        IEmailSender emailSder,
        IHttpContextAccessor httpContext,
        ApplicationSettings applicationSettings)
        {
            signinManager = mgr;
            userManager = usermgr;
            configuration = config;
            context = ctx;
            emailSender = emailSder;
            httpContextAccessor = httpContext;
            appSettings = applicationSettings;
        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken] 
        //POST : /user/register
        public async Task<System.Object> PostUser(UserViewModel model)
        {
            var request = httpContextAccessor.HttpContext.Request;

            if (!ModelState.IsValid)
            {
                return BadRequest(
                new
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var applicationUser = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            try
            {
                var result = await userManager.CreateAsync(applicationUser, model.Password);
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(applicationUser, model.Role.ToString());

                    if (result.Succeeded)
                    {
                        AddUser(model, applicationUser.Id);
                        string confirmationToken = userManager.GenerateEmailConfirmationTokenAsync(applicationUser).Result;


                        string confirmationLink = Url.Action("confirmEmail", "User",
                          new
                          {
                              userid = applicationUser.Id,
                              usertoken = confirmationToken
                          },
                          Request.Scheme);

                        var message = new Message(new string[] { model.Email }, "Registration successful", "Before you can login, please confirm your email, by clicking " + confirmationLink, null);

                        await emailSender.SendEmailAsync(message);

                        return Ok(result);
                    }
                }

                return Problem();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        //GET : /user/ConfirmEmail
        public async Task<IActionResult> ConfirmEmail(string userid, string usertoken)
        {
            IdentityUser user = await userManager.FindByIdAsync(userid);
            IdentityResult result = await userManager.ConfirmEmailAsync(user, usertoken);
            if (result.Succeeded)
            {
                return Redirect(appSettings.Client_URL + "/#/login");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        // [ValidateAntiForgeryToken]
        //POST : /user/login
        public async Task<IActionResult> Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(obj.UserName);
                if (user != null)
                {
                    if (!userManager.IsEmailConfirmedAsync(user).Result)
                    {
                        return Unauthorized("Email not confirmed!");
                    }
                }


                var result = await signinManager.PasswordSignInAsync(obj.UserName, obj.Password, false, false);

                if (result.Succeeded)
                {
                    var token = GenerateToken(user);
                    return Ok(new { token });
                }

                return Unauthorized("Username and password do not match.");
            }

            return Unauthorized("Username and password do not match.");
        }

        //         [HttpPost]
        // [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var user = await _userManager.FindByNameAsync(model.Email);
        //         if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        //         {
        //             // Don't reveal that the user does not exist or is not confirmed
        //             return View("ForgotPasswordConfirmation");
        //         }

        //         // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
        //         // Send an email with this link
        //         var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //         var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
        //         await _emailSender.SendEmailAsync(model.Email, "Reset Password",
        //            $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
        //         return View("ForgotPasswordConfirmation");
        //     }

        //     // If we got this far, something failed, redisplay form
        //     return View(model);
        // }

        // [HttpPost]
        // [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        // {
        //     ViewData["ReturnUrl"] = returnUrl;
        //     if (ModelState.IsValid)
        //     {
        //         // Require the user to have a confirmed email before they can log on.
        //         var user = await _userManager.FindByNameAsync(model.Email);
        //         if (user != null)
        //         {
        //             if (!await _userManager.IsEmailConfirmedAsync(user))
        //             {
        //                 ModelState.AddModelError(string.Empty, "You must have a confirmed email to log in.");
        //                 return View(model);
        //             }
        //         }

        //         // This doesn't count login failures towards account lockout
        //         // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //         var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //         if (result.Succeeded)
        //         {
        //             _logger.LogInformation(1, "User logged in.");
        //             return RedirectToLocal(returnUrl);
        //         }
        //         if (result.RequiresTwoFactor)
        //         {
        //             return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //         }
        //         if (result.IsLockedOut)
        //         {
        //             _logger.LogWarning(2, "User account locked out.");
        //             return View("Lockout");
        //         }
        //         else
        //         {
        //             ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //             return View(model);
        //         }
        //     }


        private User AddUser(UserViewModel user, string identityId)
        {
            User newUser = new User();
            newUser.UserName = user.UserName;
            newUser.IdentityId = identityId;
            newUser.Role = user.Role;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Email = user.Email;
            newUser.Phone = user.Phone;
            newUser.PartyName = user.PartyName;
            newUser.Category = user.Category;
            newUser.Description = user.Description;

            context.Add(user);
            context.SaveChanges();
            return newUser;
        }

        private async Task<string> GenerateToken(IdentityUser user)
        {
            var role = await userManager.GetRolesAsync(user);
            IdentityOptions _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }

    public class UserViewModel : User
    {
        [Required]
        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}