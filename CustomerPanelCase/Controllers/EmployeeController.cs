using Azure.Core;
using Business.Account.Command;
using Business.Account.Query;
using CustomerPanelCase.Models;
using DataAccess.Entities;
using DataAccess.JwtServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CustomerPanelCase.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenHelper _tokenHelper;
        private readonly IHttpContextAccessor _httpContext;

        public EmployeeController(ILogger<AccountController> logger, ITokenHelper tokenHelper, IHttpContextAccessor httpContext, IServiceProvider serviceProvider)
            : base(httpContext, serviceProvider)
        {
            _logger = logger;
            _tokenHelper = tokenHelper;
            _httpContext = httpContext;
        }
        public async Task<IActionResult> Management(GetManagementUserListQuery request)
        {

            return View(await MediatrSend(request));
        }
        [HttpPost]
        public async Task<JsonResult> AddOrUpdateManagement(Employee model)
        {
            //string jwtToken = GetJwtTokenFromSession();

            //if (!string.IsNullOrEmpty(jwtToken) && IsAuthorized(jwtToken))
            //{
                var status = await MediatrSend(new AddOrUpdateManagementCommand
                {
                    Id = model.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Phone = model.Phone,
                    Email = model.Email,
                    Password = model.Password
                });
                return Json(Ok("Yönetici ekleme iþlemi baþarýyla tamamlandý."));
            //}
            //else
            //{
            //    return Json(Unauthorized("Yetkisiz eriþim."));
            //}
        }

        [HttpPost]
        public async Task<JsonResult> ShowManagementModel(int Id)
        {
            var request = new GetManagementQuery
            {
                Id = Id
            };
            var model = await MediatrSend(request);
            return Json(model);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteEmployee(int Id)
        {
            var request = new DeleteEmployeeCommand
            {
                Id = Id
            };
            var model = await MediatrSend(request);
            return Json(model);
        }
        
        public async Task<IActionResult> Customer(GetCustomerUserListQuery request)
        {
            return View(await MediatrSend(request));
        }
        [HttpPost]
        public async Task<JsonResult> AddOrUpdateCustomer(Employee model)
        {
            //string jwtToken = GetJwtTokenFromSession();

            //if (!string.IsNullOrEmpty(jwtToken) && IsAuthorized(jwtToken))
            //{
                var status = await MediatrSend(new AddOrUpdateCustomerCommand
                {
                    Id = model.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Phone = model.Phone,
                    Email = model.Email,
                    Password = model.Password
                });
                return Json(Ok("Müþteri ekleme iþlemi baþarýyla tamamlandý."));
            //}
            //else
            //{
            //    return Json(Unauthorized("Yetkisiz eriþim."));
            //}
        }
        [HttpPost]
        public async Task<JsonResult> ShowCustomerModel(int Id)
        {
            var request = new GetCustomerQuery
            {
                Id = Id
            };
            var model = await MediatrSend(request);
            return Json(model);
        }
    }
}
