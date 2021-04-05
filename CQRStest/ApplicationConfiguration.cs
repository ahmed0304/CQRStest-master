using IdentityServer4.AccessTokenValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRStest
{
    public class ApplicationConfiguration
    {
        public IdentityServerAuthenticationOptions Authentication { get; set; }
    }
}
