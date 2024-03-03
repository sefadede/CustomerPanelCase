using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Base
{
    public class AuthorizeResult
    {
        public bool HasPermission { get; set; }

        public static AuthorizeResult Fail()
        {
            return new AuthorizeResult { HasPermission = false };
        }

        public static AuthorizeResult Success()
        {
            return new AuthorizeResult { HasPermission = true };
        }
    }
}
