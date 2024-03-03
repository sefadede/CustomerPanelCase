using Business.Account.Query;
using CustomerPanelCase.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RannaCore.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPanelCase.Controllers
{
    public class BaseController : BaseMvcController
    {
        public BaseController(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
       : base(httpContextAccessor, serviceProvider)
        {
        }
    }
}
