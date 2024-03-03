using DataAccess.Entities;
using DataAccess.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DataAccess.Repository;

namespace Business.Account.Command
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }

    }
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.Find(request.Id);
            await _employeeRepository.ChangeStatus(request.Id, (int)DataAccess.Enum.EmployeeStatus.Deleted);
            return true;
        }
    }
}
