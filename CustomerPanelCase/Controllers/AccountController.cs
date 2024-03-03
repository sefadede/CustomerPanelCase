using Azure.Core;
using Business.Account.Command;
using Business.Account.DTOs;
using CustomerPanelCase.Models;
using DataAccess.JwtServices;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CustomerPanelCase.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenHelper _tokenHelper;
        private readonly IHttpContextAccessor _httpContext;

        public AccountController(ILogger<AccountController> logger, ITokenHelper tokenHelper, IHttpContextAccessor httpContext, IServiceProvider serviceProvider)
            : base(httpContext, serviceProvider)
        {
            _logger = logger;
            _tokenHelper = tokenHelper;
            _httpContext = httpContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginCheckQuery loginCheckQuery = new LoginCheckQuery { Email = model.Username, Password = model.Password };
                var loginResultObject = await MediatrSend(loginCheckQuery);
                var loginResult = (LoginDTO)loginResultObject;


                if (loginResult.Success)
                {
                    var returnUrl = loginCheckQuery.ReturnUrl;
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz kullanýcý adý veya þifre.");
                    return View(model);
                }
            }

            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login", "Account");
        }
    }
}
