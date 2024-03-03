using Business.Account.DTOs;
using DataAccess.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Business.Account.Query
{
    public class GetCustomerUserListQuery : IRequest<EmployeeListDTO>
    {
    }
    public class GetCustomerUserListQueryHandler : IRequestHandler<GetCustomerUserListQuery, EmployeeListDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetCustomerUserListQueryHandler(IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<EmployeeListDTO> Handle(GetCustomerUserListQuery request, CancellationToken cancellationToken)
        {
            EmployeeListDTO response = new EmployeeListDTO();
            var httpContext = _httpContextAccessor.HttpContext;
            var employee = _employeeRepository.GetAllByJobType((int)DataAccess.Enum.JobType.Customer);
            response.Employee = employee;

            response.Jwt = httpContext.Session.GetString("JwtToken");
            return response;
        }
    }
}
