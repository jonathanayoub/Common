using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Common.Web.Api
{
    /// <summary>
    /// To be used to replace the default web api dependency resolver when using Windsor with web api in your global.asax.cs
    /// </summary>
    /// <example>
    /// <code>
    /// var dependencyResolver = new WebApiWindsorDependencyResolver(container);
    /// DependencyResolver.SetResolver(dependencyResolver);
    /// </code>    
    /// </example>
    public sealed class WebApiWindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WebApiWindsorDependencyResolver(IWindsorContainer container)
        {
            Guard.NotNull(() => container, container);
            _container = container;
        }
        public object GetService(Type t)
        {
            return _container.Kernel.HasComponent(t) ? _container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return _container.ResolveAll(t).Cast<object>().ToArray();
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public void Dispose()
        {

        }

    }
}
