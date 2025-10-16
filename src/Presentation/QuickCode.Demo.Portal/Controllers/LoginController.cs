using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuickCode.Demo.Common.Nswag;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using QuickCode.Demo.Portal.Models;
using LoginRequest = QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts.LoginRequest;


using ForgotPasswordRequest = QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts.ForgotPasswordRequest;
using ResetPasswordRequest = QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts.ResetPasswordRequest;

namespace QuickCode.Demo.Portal.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IAuthenticationsClient authenticationsClient;
        private readonly IAspNetUsersClient usersClient;

        public LoginController(IAuthenticationsClient authenticationsClient,
            IAspNetUsersClient usersClient, ITableComboboxSettingsClient tableComboboxSettingsClient,
            IHttpContextAccessor httpContextAccessor, IMemoryCache cache) : base(tableComboboxSettingsClient,
            httpContextAccessor, cache)
        {
            this.authenticationsClient = authenticationsClient;
            this.usersClient = usersClient;
        }

        public  IActionResult Index(string returnUrl)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var model = GetModel<LoginData>();
            model.ReturnUrl = returnUrl;
            SetModelBinder(ref model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginData model)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelBinder(ref model);
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            try
            {
                var response = await authenticationsClient.ApiAuthLoginPostAsync(new LoginRequest()
                    { Email = model.Username, Password = model.Password });
                (authenticationsClient as ClientBase)!.SetBearerToken(response.AccessToken);
                (usersClient as ClientBase)!.SetBearerToken(response.AccessToken);
                var userData = await usersClient.AspNetUsersGetUserAsync(model.Username);
                HttpContextAccessor.HttpContext!.Session.Clear();

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, $"{userData.Id}"),
                    new Claim(ClaimTypes.Name, $"{userData.FirstName} {userData.LastName}"),
                    new Claim(ClaimTypes.Email, userData.Email),
                    new Claim(ClaimTypes.GroupSid, $"{userData.PermissionGroupName}"),
                    new Claim("QuickCodeApiToken", response.AccessToken),
                    new Claim("QuickCodeApiTokenExpiresIn", response.ExpiresIn.ToString()),
                    new Claim("RefreshToken", response.RefreshToken)

                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var userPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        AllowRefresh = true,
                        ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddMinutes(30)
                    });
                

                if (String.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(model.ReturnUrl);
                }
            }
            catch (QuickCodeSwaggerException ex)
            {
                if (ex.StatusCode == 401)
                {
                    model = new LoginData
                    {
                        ErrorMessage = "The email or password you entered is incorrect. Please check and try again."
                    };
                    return await Task.FromResult<IActionResult>(View(model));
                }
            }
            catch (Exception ex)
            {
                model = new LoginData
                {
                    ErrorMessage = $"Server Error !!!\nPlease try again later.\nError:{ex.Message}"
                };
                return await Task.FromResult<IActionResult>(View(model));
            }
            
            return await Task.FromResult<IActionResult>(View(model));
        }

        public async Task<IActionResult> Logout()
        {
            HttpContextAccessor.HttpContext!.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
          
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var model = new RegisterData();
            SetModelBinder(ref model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterData model)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelBinder(ref model);
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var customRegisterRequest = new CustomRegisterRequest
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                await authenticationsClient.ApiAuthRegisterPostAsync(customRegisterRequest);
                
                model.SuccessMessage = "Registration successful! Please check your email to confirm your account.";
                return View(model);
            }
            catch (QuickCodeSwaggerException ex)
            {
                if (ex.StatusCode == 400)
                {
                    model.ErrorMessage = "Registration failed. Please check your information and try again.";
                }
                else
                {
                    model.ErrorMessage = "An error occurred during registration. Please try again later.";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Server Error! Please try again later. Error: {ex.Message}";
                return View(model);
            }
        }

        public IActionResult ForgotPassword()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var model = new ForgotPasswordData();
            SetModelBinder(ref model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordData model)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelBinder(ref model);
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var forgotPasswordRequest = new ForgotPasswordRequest
                {
                    Email = model.Email
                };

                await authenticationsClient.ApiAuthForgotPasswordPostAsync(forgotPasswordRequest);
                
                model.SuccessMessage = "If an account with that email exists, a password reset link has been sent to your email address.";
                return View(model);
            }
            catch (QuickCodeSwaggerException)
            {
                model.ErrorMessage = "An error occurred while processing your request. Please try again later.";
                return View(model);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Server Error! Please try again later. Error: {ex.Message}";
                return View(model);
            }
        }

        public IActionResult ResetPassword(string email = null, string code = null)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var model = new ResetPasswordData();
            if (!string.IsNullOrEmpty(email))
            {
                model.Email = email;
            }
            if (!string.IsNullOrEmpty(code))
            {
                model.ResetCode = code;
            }
            
            SetModelBinder(ref model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordData model)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelBinder(ref model);
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var resetPasswordRequest = new ResetPasswordRequest
                {
                    Email = model.Email,
                    ResetCode = model.ResetCode,
                    NewPassword = model.NewPassword
                };

                await authenticationsClient.ApiAuthResetPasswordPostAsync(resetPasswordRequest);
                
                model.SuccessMessage = "Your password has been reset successfully. You can now login with your new password.";
                return View(model);
            }
            catch (QuickCodeSwaggerException ex)
            {
                if (ex.StatusCode == 400)
                {
                    model.ErrorMessage = "Invalid reset code or email. Please check your information and try again.";
                }
                else
                {
                    model.ErrorMessage = "An error occurred while resetting your password. Please try again later.";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Server Error! Please try again later. Error: {ex.Message}";
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return await Task.FromResult(View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
        }


    }
}