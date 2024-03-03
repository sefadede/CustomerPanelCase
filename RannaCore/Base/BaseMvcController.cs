using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace RannaCore.Base
{
    public class BaseMvcController : Controller
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        public BaseMvcController(IHttpContextAccessor httpContextAccessor,IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = serviceProvider.GetService<IMediator>();
        }
        public async Task<object> MediatrSend(object input)
        {
            var result = await _mediator.Send(input, CancellationToken.None);
            return result;
        }
        protected string GetJwtTokenFromSession()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
        }
        protected bool IsAuthorized(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("yourSecretKey"); 
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out var validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
