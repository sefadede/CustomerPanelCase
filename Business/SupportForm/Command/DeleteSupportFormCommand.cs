using Business.Account.Command;
using DataAccess.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Business.SupportForm.Command
{
    public class DeleteSupportFormCommand : IRequest<bool>
    {
        public int Id { get; set; }

    }
    public class DeleteSupportFormCommandHandler : IRequestHandler<DeleteSupportFormCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISupportFormRepository _supportFormRepository;
        public DeleteSupportFormCommandHandler(IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor, ISupportFormRepository supportFormRepository)
        {
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
            _supportFormRepository = supportFormRepository;
        }
        public async Task<bool> Handle(DeleteSupportFormCommand request, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.Find(request.Id);
            await _employeeRepository.ChangeStatus(request.Id, (int)DataAccess.Enum.EmployeeStatus.Deleted);

            var supportForm = _supportFormRepository.Find(request.Id);
            await _supportFormRepository.ChangeStatus(request.Id, (int)DataAccess.Enum.FormStatus.Deleted);

            return true;
        }
    }
}
