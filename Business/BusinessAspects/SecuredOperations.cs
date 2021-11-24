using System;
using System.Net.Http;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects
{
    public class SecuredOperations : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperations(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        
        protected override void OnBefore(IInvocation invocation)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
                foreach (var role in _roles)
                {
                    if (roleClaims.Contains(role) )
                    {
                        return;
                    }
                }
            }

            throw new ValidationException(Messages.AuthorizationDenied);
           
        }
    }
}