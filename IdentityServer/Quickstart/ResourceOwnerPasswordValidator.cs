using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using IdentityServerHost.Quickstart.UI;

namespace IdentityServer.Quickstart
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly TestUserStore _users;

        public ResourceOwnerPasswordValidator(TestUserStore users = null)
        {
            _users = users ?? new TestUserStore(TestUsers.Users);
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_users.ValidateCredentials(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(_users.FindByUsername(context.UserName).SubjectId, "password", null, "local", null);
                return Task.FromResult(context.Result);
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
            return Task.FromResult(context.Result);
        }
    }
}
