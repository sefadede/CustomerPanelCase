using Business.Account.Command;
using DataAccess.Entities;
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
    public class AddOrUpdateSupportFormCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int CustomerEmployeeId { get; set; }

    }
    public class AddOrUpdateSupportFormCommandHandler : IRequestHandler<AddOrUpdateSupportFormCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISupportFormRepository _supportFormRepository;
        public AddOrUpdateSupportFormCommandHandler(IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor, ISupportFormRepository supportFormRepository)
        {
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
            _supportFormRepository = supportFormRepository;
        }
        public async Task<bool> Handle(AddOrUpdateSupportFormCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != 0)
            {
                var supportForm = _supportFormRepository.Find(request.Id);
                supportForm.Subject = request.Subject;
                supportForm.Message = request.Message;
                supportForm.Date = DateTime.Now;
                supportForm.FormStatusId = (int)DataAccess.Enum.FormStatus.OperationIsDone;
                supportForm.CustomerEmployeeId = request.CustomerEmployeeId;
                _supportFormRepository.Update(supportForm);
                return true;
            }
            else
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var employeeId = httpContext.Session.GetInt32("EmployeeId").Value;
                _supportFormRepository.Insert(new DataAccess.Entities.SupportForm
                {
                    EmployeeId = employeeId,
                    CustomerEmployeeId = request.CustomerEmployeeId,
                    Subject = request.Subject,
                    Message = request.Message,
                    Date = DateTime.Now,
                    FormStatusId = (int)DataAccess.Enum.FormStatus.NoActionTaken
                });
                return true;
            }
        }
    }
}
