using Microsoft.AspNetCore.Http;
using Piranha.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Piranha.AspNetCore
{
    public class StartPageMiddleware: MiddlewareBase
    {
        public StartPageMiddleware(RequestDelegate next): base(next)
        {

        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The current http context</param>
        /// <returns>An async task</returns>
        public override async Task Invoke(HttpContext context, Api api)
        {
            if (!IsHandled(context) && !context.Request.Path.Value.StartsWith("/manager/assets/"))
            {
                var url = context.Request.Path.HasValue ? context.Request.Path.Value : "";

                var response = StartPageRouter.Invoke(api, url);
                if (response != null)
                {
                    context.Request.Path = new PathString(response.Route);

                    if (context.Request.QueryString.HasValue)
                    {
                        context.Request.QueryString = new QueryString(context.Request.QueryString + "&" + response.QueryString);
                    }
                    else
                    {
                        context.Request.QueryString = new QueryString("?" + response.QueryString);
                    }
                }
            }

            await next.Invoke(context);
        }
    }
}
