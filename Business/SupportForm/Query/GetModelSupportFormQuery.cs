using Business.Account.Query;
using DataAccess.Entities;
using DataAccess.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Business.SupportForm.Query
{
    public class GetModelSupportFormQuery : IRequest<DataAccess.Entities.SupportForm>
    {
        public int Id { get; set; }
    }
    public class GetModelSupportFormQueryHandler : IRequestHandler<GetModelSupportFormQuery, DataAccess.Entities.SupportForm>
    {
        private readonly ISupportFormRepository _supportFormRepository;
        public GetModelSupportFormQueryHandler(ISupportFormRepository supportFormRepository)
        {
            _supportFormRepository = supportFormRepository;
        }
        public async Task<DataAccess.Entities.SupportForm> Handle(GetModelSupportFormQuery request, CancellationToken cancellationToken)
        {
            return _supportFormRepository.Find(request.Id);
        }
    }
}
