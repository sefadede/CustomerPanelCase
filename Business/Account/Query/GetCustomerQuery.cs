using DataAccess.Entities;
using DataAccess.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Business.Account.Query
{
    public class GetCustomerQuery : IRequest<Employee>
    {
        public int Id { get; set; }
    }
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetCustomerQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return _employeeRepository.Find(request.Id);
        }
    }
}
