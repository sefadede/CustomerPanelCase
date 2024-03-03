using Azure.Core;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DataAccess.JwtServices
{
    public interface ITokenHelper
    {
        string GenerateJwtToken(Employee employee);
    }
}
