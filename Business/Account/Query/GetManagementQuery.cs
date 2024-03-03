using MediatR;
using RannaCore.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DataAccess.Entities;
using DataAccess.Interface;

namespace Business.Account.Query
{
    public class GetManagementQuery : IRequest<Employee>
    {
        public int Id { get; set; }
    }
    public class GetManagementQueryHandler : IRequestHandler<GetManagementQuery, Employee>
    {
        private readonly IEmployeeRepository  _employeeRepository;
        public GetManagementQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> Handle(GetManagementQuery request, CancellationToken cancellationToken)
        {
            return _employeeRepository.Find(request.Id);
        }
    }
}
