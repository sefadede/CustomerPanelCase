using Business.Account.Command;
using Business.Account.DTOs;
using Business.Account.Query;
using Business.SupportForm.Command;
using Business.SupportForm.Query;
using CustomerPanelCase.Models;
using DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CustomerPanelCase.Controllers
{
    public class SupportFormController : BaseController
    {
        public SupportFormController(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
       : base(httpContextAccessor, serviceProvider)
        {
        }

        public async Task<IActionResult> Index(GetSupportFormQuery request)
        {
            return View(await MediatrSend(request));
        }
        [HttpPost]
        public async Task<JsonResult> AddOrUpdateSupportForm(SupportForm model)
        {
            await MediatrSend(new AddOrUpdateSupportFormCommand
            {
                Id = model.Id,
                Subject = model.Subject,
                Message = model.Message,
                CustomerEmployeeId = model.CustomerEmployeeId,
            });
            return Json(Ok("Destek formu başarıyla tamamlandı."));

        }
        [HttpPost]
        public async Task<JsonResult> ShowSupportFormModel(int Id)
        {
            var request = new GetModelSupportFormQuery
            {
                Id = Id
            };
            var model = await MediatrSend(request);
            return Json(model);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteSupportForm(int Id)
        {
            var request = new DeleteSupportFormCommand
            {
                Id = Id
            };
            var model = await MediatrSend(request);
            return Json(model);
        }
    }
}
