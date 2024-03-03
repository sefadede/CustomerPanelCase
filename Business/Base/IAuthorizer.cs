using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RannaCore.Models;

namespace Business.Base
{
    public interface IAuthorizer<T>
    {
        int Order { get; }
        Task<AuthorizeResult> AuthorizeAsync(T instance, CancellationToken cancellationToken, ClaimModel claimModel);

    }
}
