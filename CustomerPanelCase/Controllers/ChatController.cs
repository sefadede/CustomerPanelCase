using Azure.Core;
using Business.Account.Command;
using Business.Account.Query;
using Business.Chat.Query;
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
    public class ChatController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenHelper _tokenHelper;
        private readonly IHttpContextAccessor _httpContext;

        public ChatController(ILogger<AccountController> logger, ITokenHelper tokenHelper, IHttpContextAccessor httpContext, IServiceProvider serviceProvider)
            : base(httpContext, serviceProvider)
        {
            _logger = logger;
            _tokenHelper = tokenHelper;
            _httpContext = httpContext;
        }

        public async Task<IActionResult> Message(GetChatPageQuery request)
        {
            return View(await MediatrSend(request));
        }
    }
}
