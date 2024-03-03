using Business.Account.Command;
using Business.Account.DTOs;
using DataAccess.Entities;
using DataAccess.Interface;
using DataAccess.JwtServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using RannaCore.Models;
using RannaCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;

namespace Business.Account.Query
{
    public class GetManagementUserListQuery : IRequest<EmployeeListDTO>
    {
    }
    public class GetManagementUserListQueryHandler : IRequestHandler<GetManagementUserListQuery, EmployeeListDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetManagementUserListQueryHandler(IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<EmployeeListDTO> Handle(GetManagementUserListQuery request, CancellationToken cancellationToken)
        {
            EmployeeListDTO response = new EmployeeListDTO();
            var httpContext = _httpContextAccessor.HttpContext;
            var employee = _employeeRepository.GetAllByJobType((int)DataAccess.Enum.JobType.Management);
            response.Employee = employee;

            response.Jwt = httpContext.Session.GetString("JwtToken");
            return response;
        }
    }
}
