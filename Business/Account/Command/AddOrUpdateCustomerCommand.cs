using DataAccess.Entities;
using DataAccess.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RannaCore.Utilities;

namespace Business.Account.Command
{
    public class AddOrUpdateCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AddOrUpdateCustomerCommandHandler : IRequestHandler<AddOrUpdateCustomerCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddOrUpdateCustomerCommandHandler(IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<bool> Handle(AddOrUpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != 0)
            {
                var employee = _employeeRepository.Find(request.Id);
                employee.Name = request.Name;
                employee.Surname = request.Surname;
                employee.Phone = request.Phone;
                employee.Email = request.Email;
                employee.Password = request.Password;
                _employeeRepository.Update(employee);
                return true;
            }
            else
            {
                var employee = _employeeRepository.FindEmployeeByEmail(request.Email);
                if (employee == null)
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    var employeeId = httpContext.Session.GetInt32("EmployeeId");
                    _employeeRepository.Insert(new Employee
                    {
                        Name = request.Name,
                        Surname = request.Surname,
                        Phone = request.Phone,
                        Email = request.Email,
                        Password= CryptoService.CreateMD5(request.Password),
                        JobId = (int)DataAccess.Enum.JobType.Customer,
                        StatusId = (int)DataAccess.Enum.EmployeeStatus.Approved,
                        MasterEmployeeId = Convert.ToInt32(employeeId)
                    });
                    return true;
                }
                else return false;
            }

        }
    }
}
