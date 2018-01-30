using Castle.Windsor;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Common.Web.Api
{
    /// <summary>
    /// To be used when Windsor should be used to resolve web api controllers with DI in global.asax.cs
    /// </summary>
    /// <example>
    /// <code>
    /// var config = GlobalConfiguration.Configuration;
    /// config.Services.Replace(
    ///     typeof(IHttpControllerActivator),
    ///     new WindsorCompositionRoot(container));
    /// </code>
    /// </example>
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer container;

        public WindsorCompositionRoot(IWindsorContainer container)
        {
            this.container = container;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                (IHttpController)this.container.Resolve(controllerType);

            request.RegisterForDispose(
                new Release(
                    () => this.container.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                this.release();
            }
        }
    }
}