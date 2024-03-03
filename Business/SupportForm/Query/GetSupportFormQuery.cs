using Business.Account.Query;
using DataAccess.Entities;
using DataAccess.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Business.SupportForm.DTOs;
using Microsoft.AspNetCore.Http;
using DataAccess.Entities.Custom;

namespace Business.SupportForm.Query
{
    public class GetSupportFormQuery : IRequest<SupportFormDTO>
    {
    }
    public class GetSupportFormQueryHandler : IRequestHandler<GetSupportFormQuery, SupportFormDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISupportFormRepository _supportFormRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetSupportFormQueryHandler(IEmployeeRepository employeeRepository, ISupportFormRepository supportFormRepository,IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _supportFormRepository = supportFormRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<SupportFormDTO> Handle(GetSupportFormQuery request, CancellationToken cancellationToken)
        {
            SupportFormDTO response = new SupportFormDTO();
            var httpContext = _httpContextAccessor.HttpContext;
            var employeeId = httpContext.Session.GetInt32("EmployeeId").Value;
            response.CanEdit = false;
            var employee = _employeeRepository.Find(employeeId);
            bool allList = false;
            if (employee.JobId == (int)DataAccess.Enum.JobType.Management)
            {
                allList = true;
                response.CanEdit = true;
            }
            var supportFormList = _supportFormRepository.GetAllByEmployeeId(employeeId, allList);
            foreach ( var supportForm in supportFormList )
            {
                supportForm.FromStatusText = Enum.GetName(typeof(DataAccess.Enum.FormStatus), supportForm.FormStatusId);
            }
            response.SupportFormRs = supportFormList;
            return response;
        }
    }
}
