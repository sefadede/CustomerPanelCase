using Business.Account.DTOs;
using Business.Account.Query;
using DataAccess.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Business.Chat.DTOs;
using DataAccess.Entities;

namespace Business.Chat.Query
{
    public class GetChatPageQuery : IRequest<ChatDTO>
    {
    }
    public class GetChatPageQueryHandler : IRequestHandler<GetChatPageQuery, ChatDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetChatPageQueryHandler(IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ChatDTO> Handle(GetChatPageQuery request, CancellationToken cancellationToken)
        {
            ChatDTO response = new ChatDTO();
            response.EmployeeList = _employeeRepository.GetAllByStatusId((int)DataAccess.Enum.EmployeeStatus.Approved);
            var httpContext = _httpContextAccessor.HttpContext;
            var xx = httpContext.Session.GetString("EmployeeId");
            var employeeId = httpContext.Session.GetInt32("EmployeeId");
            response.EmployeeId = employeeId.Value;
            return response;
        }
    }
}
