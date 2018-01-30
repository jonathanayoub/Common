﻿using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Common.Web.Api
{
    /// <summary>
    ///     Action filter to check the model state before the controller action is invoked.
    /// </summary>
    /// <remarks>
    ///     From http://www.asp.net/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
    /// </remarks>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }

        public override bool AllowMultiple
        {
            get { return false; }
        }
    }
}
