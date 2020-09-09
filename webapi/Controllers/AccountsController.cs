using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using webapi.Helpers;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : BaseController
    {
        private IAccountService accountService;
        private readonly ILogger logger;
        AppSettings appSettings;
        public AccountsController(IAccountService accountService,
                                  IOptions<AppSettings> appSettings,
                                  ILogger<AccountsController> logger)
        {
            this.accountService = accountService;
            this.appSettings = appSettings.Value;
            this.logger = logger;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = await accountService.Authenticate(model, ipAddress());
                return Ok(response);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.ToString());
                throw;
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            await accountService.Register(model, this.Url);
            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }

        [HttpGet("VerifyEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string userid, string token)
        {
            var result = await accountService.VerifyEmail(userid, token);

            if (!result) return BadRequest();
            logger.LogError("appSettings.Client_URL is " + appSettings.Client_URL);
            return Redirect(appSettings.Client_URL + "/#/account/login");
            // return Redirect("http://klickon.canadacentral.cloudapp.azure.com" + "/#/account/login");
        }
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await accountService.ForgotPassword(model, this.Url);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            var result = await accountService.ResetPassword(model);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await accountService.RevokeToken();
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}