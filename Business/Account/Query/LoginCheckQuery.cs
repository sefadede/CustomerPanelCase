using Business.Account.DTOs;
using DataAccess.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DataAccess.Entities;
using DataAccess.Repository;
using RannaCore.Utilities;
using DataAccess;
using RannaCore.Models;
using DataAccess.JwtServices;
using Microsoft.AspNetCore.Http;

namespace Business.Account.Command
{
    public class LoginCheckQuery : IRequest<LoginDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public ClaimModel ClaimModel { get; set; }
    }
    public class GetLoginQueryHandler : IRequestHandler<LoginCheckQuery, LoginDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetLoginQueryHandler(IEmployeeRepository employeeRepository, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LoginDTO> Handle(LoginCheckQuery request, CancellationToken cancellationToken)
        {
            LoginDTO response = new LoginDTO() { Success = false };
            Employee employee = await _employeeRepository.CheckLogin(request.Email);
            if (employee != null)
            {
                response.Success = CryptoService.CompareMD5(request.Password, employee.Password);
                if (response.Success)
                {
                    var jwtToken = _tokenHelper.GenerateJwtToken(employee);
                    var httpContext = _httpContextAccessor.HttpContext;
                    httpContext.Session.SetString("JwtToken", jwtToken);
                    httpContext.Session.SetInt32("EmployeeId", employee.Id);
                    httpContext.Session.SetInt32("JobId", employee.JobId);
                }
            }
            return response;
        }
    }
}
