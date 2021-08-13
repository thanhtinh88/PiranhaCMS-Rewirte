using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.AspNet
{
    public class StartPageMiddleware: MiddlewareBase
    {
        /// <summary>
        /// Creates a new middleware instance.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        public StartPageMiddleware(RequestDelegate next, Api api): base(next, api)
        {

        }

        /// <summary>
		/// Invokes the middleware.
		/// </summary>
		/// <param name="context">The current http context</param>
		/// <returns>An async task</returns>
        public override async Task Invoke(HttpContext context)
        {
            if (!IsHandled(context))
            {
                var url = context.Request.Path.HasValue ? context.Request.Path.Value : "";
                if (string.IsNullOrWhiteSpace(url) || url == "/")
                {
                    var page = api.Pages.GetStartPage();

                    if (page != null)
                    {
                        context.Request.Path = new PathString(page.Route);
                        if (context.Request.QueryString.HasValue)
                            context.Request.QueryString = new QueryString(context.Request.QueryString.Value + "&id=" + page.Id + "&startpage=true&piranha_handled=true");
                        else
                            context.Request.QueryString = new QueryString("?id=" + page.Id + "&startpage=true&piranha_handled=true");
                    }
                } 
            }
            await next.Invoke(context);
        }
    }
}
