using Business.Account.DTOs;
using Business.Account.Query;
using DataAccess.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DataAccess.Entities;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using RannaCore.Utilities;

namespace Business.Account.Command
{
    public class AddOrUpdateManagementCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public class AddorUpdateManagementCommandHandler : IRequestHandler<AddOrUpdateManagementCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddorUpdateManagementCommandHandler(IEmployeeRepository employeeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<bool> Handle(AddOrUpdateManagementCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != 0)
            {
                var employee = _employeeRepository.Find(request.Id);
                employee.Name = request.Name;
                employee.Surname = request.Surname;
                employee.Phone = request.Phone;
                employee.Email = request.Email;
                employee.Gender = request.Gender;
                employee.Password = request.Password;
                employee.BirthDate = request.BirthDate;
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
                        Gender = request.Gender,
                        BirthDate = request.BirthDate,
                        Password = CryptoService.CreateMD5(request.Password),
                        JobId = (int)DataAccess.Enum.JobType.Management,
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
