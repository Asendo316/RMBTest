using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using RMBTestRestAPI.Contracts;
using RMBTestRestAPI.Contracts.v1.Requests.auth;
using RMBTestRestAPI.Contracts.v1.Response.abs;
using RMBTestRestAPI.Contracts.v1.Response.auth;
using RMBTestRestAPI.Model;
using RMBTestRestAPI.Services.Interfaces.v1.auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Controllers.v1.auth
{
    public class AuthenticationController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(IIdentityService identityService, UserManager<IdentityUser> userManager)
        {
            _identityService = identityService;
            _userManager = userManager;
        }


        /// <summary>
        /// Creates New Account For User
        /// </summary>
        [HttpPost(ApiRoutes.Authentication.RegisterUser)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await _identityService.RegisterUserAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                ProfileId = authResponse.ProfileId
            });
        }



        /// <summary>
        /// Login For User
        /// </summary>
        [HttpPost(ApiRoutes.Authentication.LoginUser)]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginUserAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                ProfileId = authResponse.ProfileId
            });
        }



        /// <summary>
        /// Refresh JWT Token
        /// </summary>
        [HttpPost(ApiRoutes.Authentication.RefreshToken)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        [HttpPost(ApiRoutes.Authentication.ResetPassword)]
        public async Task<IActionResult> SendPasswordResetLink([FromBody] ResetPasswordRequest request)
        {
            if (request.Email != null)
            {
                IdentityUser user = _userManager.FindByEmailAsync(request.Email).Result;

                if (user == null)
                {
                    return NotFound(new GenericResponse
                    {
                        Status = "Failed",
                        Message = "Invalid User"
                    });
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);



                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var resetLink = baseUrl + "/" + ApiRoutes.Authentication.ResetPasswordUser + "?token=" + token;

                var emailConfirmation = await sendEmail("noreply@lesscard.com", "Password Reset", request.Email, resetLink, 2);

                if (emailConfirmation)
                {
                    return Ok(new GenericResponse
                    {
                        Status = "Success",
                        Message = "Password reset link has ben sent to your email"
                    });
                }
                else
                {
                    return NotFound(new GenericResponse
                    {
                        Status = "Failed",
                        Message = "An Error Occured Generating Password Reset Link"
                    });
                }
            }
            else
            {
                return NotFound(new GenericResponse
                {
                    Status = "Failed",
                    Message = "Email cannot be Empty"
                });
            }
        }


        [HttpGet(ApiRoutes.Authentication.ResetPasswordUser)]
        public IActionResult OnGet([FromQuery] string token)
        {
            if (token == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                return View();
            }
        }

        [HttpGet(ApiRoutes.Authentication.ConfrimEmail)]
        public IActionResult ConfirmEmail([FromQuery] string token)
        {
            if (token == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                //var result = await _userManager. (user, token);

                return View();
            }
        }

        [HttpPost(ApiRoutes.Authentication.ResetPasswordUser)]
        public async Task<IActionResult> ChangePasswordAsync([FromQuery] string token, [FromForm]ResetPasswordViewModel resetPasswordViewModel)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

            IdentityResult result = await _userManager.ResetPasswordAsync
                      (user, token.Replace(" ", "+"), resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return Ok(new GenericResponse
                {
                    Status = "Success",
                    Message = "Password Changed"
                });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return NotFound(new GenericResponse
                {
                    Status = "Failed",
                    Message = "Error Changing Password"
                });
            }
        }


        private async Task<bool> sendEmail(string sender, string subject, string recipient, string url, int mailType)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
            sender);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            recipient);
            message.To.Add(to);

            message.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();

            switch (mailType)
            {
                case 1: //Email Confirmation
                    /*
                    bodyBuilder.HtmlBody = string.Format(@"<p>Welcome to LessCard,<br>
                                                           <p>Please click link below to verify your email<br>");*/
                    bodyBuilder.TextBody = "Welcome to LessCard, \n" +
                        "Please click link below to verify your email \n" +
                        url;
                    break;
                case 2: //Password reset
                    bodyBuilder.TextBody = "Forgot your Password ? \n" +
                        "Please click link below to reset your password \n" +
                        url;
                    break;
                default: //test
                    bodyBuilder.TextBody = "Hello World";
                    break;
            }

            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                SmtpClient client = new SmtpClient();
                //client.Connect("smtp-relay.sendinblue.com", 587, false);
                //client.Authenticate("ibikunle18@gmail.com", "tx3aw72EZVc1kmBM");

                client.Connect("mail.thruthevase.com", 465, true);
                client.Authenticate("ibikunle@thruthevase.com", "prosperspulloutgame");

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
