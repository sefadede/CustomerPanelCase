using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RannaCore.Exceptions;
using RannaCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Base
{
    public class RequestAuthorizeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers;
        private readonly IHttpContextAccessor _httpContext;

        public RequestAuthorizeBehavior(IEnumerable<IAuthorizer<TRequest>> authorizers, IHttpContextAccessor httpContext)
        {
            if (authorizers == null)
            {
                throw new ArgumentNullException(nameof(authorizers), "Authorizers collection cannot be null.");
            }

            // Null olmayan elemanları al ve sıralı bir liste oluştur
            _authorizers = authorizers.Where(a => a != null).OrderBy(o => o.Order).ToList();

            _httpContext = httpContext;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var user = _httpContext.HttpContext.User;
            if (user == null)
                throw new ServiceNoPermissionException();

            //RouteData routeData = RoutingHttpContextExtensions.GetRouteData(_httpContext.HttpContext);
            ClaimModel claimModel = new ClaimModel();
            claimModel.EmployeeId = Convert.ToInt32(user.Claims.FirstOrDefault(_ => _.Type.Equals("Id"))?.Value);
            claimModel.JobId = Convert.ToInt32(user.Claims.FirstOrDefault(_ => _.Type.Equals("JobId"))?.Value);

            foreach (var authorizer in _authorizers)
            {
                var result = await authorizer.AuthorizeAsync(request, cancellationToken, claimModel);

                if (!result.HasPermission)
                    throw new ServiceNoPermissionException();
            }

            return await next();
        }
    }
}
