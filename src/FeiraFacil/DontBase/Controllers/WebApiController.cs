using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DontBase.Controllers
{
    public abstract class WebApiController
    {
        protected const AuthorizationLevel Default = AuthorizationLevel.Anonymous;
        private readonly IServiceProvider _serviceProvider;

        protected T GetService<T>() => _serviceProvider.GetRequiredService<T>();

        protected WebApiController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}