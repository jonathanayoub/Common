using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Common.Web
{
    /// <summary>
    /// To be used to replace the default MVC dependency resolver when using Windsor with MVC in your global.asax.cs
    /// </summary>
    /// <example>
    /// <code>
    /// var dependencyResolver = new MvcWindsorDependencyResolver(container);
    /// DependencyResolver.SetResolver(dependencyResolver);
    /// </code>    
    /// </example>
    public sealed class MvcWindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public MvcWindsorDependencyResolver(IWindsorContainer container)
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

        public void Dispose()
        {

        }

    }
}
