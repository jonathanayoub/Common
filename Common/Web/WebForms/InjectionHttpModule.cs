using CommonServiceLocator;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Common.Web.WebForms
{
    //see https://blogs.msdn.microsoft.com/webdev/2016/10/19/modern-asp-net-web-forms-development-dependency-injection/
    public class InjectionHttpModule : IHttpModule
    {
        private static readonly IServiceLocator Container;

        static InjectionHttpModule()
        {
            Container = ServiceLocator.Current;
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += Context_PreRequestHandlerExecute;
        }

        private void Context_PreRequestHandlerExecute(object sender, EventArgs e)
        {

            var page = HttpContext.Current.CurrentHandler as Page;

            if (page == null) return;

            // Get the code-behind class that we may have written
            var pageType = page.GetType().BaseType;

            // Determine if there is a constructor to inject, and grab it
            var ctor = (from c in pageType.GetConstructors()
                        where c.GetParameters().Length > 0
                        select c).FirstOrDefault();

            if (ctor != null)
            {

                // Resolve the parameters for the constructor
                var args = (from parm in ctor.GetParameters()
                            select Container.GetInstance(parm.ParameterType))
                            .ToArray();

                // Execute the constructor method with the arguments resolved 
                ctor.Invoke(page, args);

            }

            // TODO: module does not currently support property injection. implement property injection if needed
        }

        public void Dispose() { }
    }
}
