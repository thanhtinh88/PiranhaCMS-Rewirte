using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.AspNet
{
    public class StartPageMiddleware: MiddlewareBase
    {
        public StartPageMiddleware(RequestDelegate next): base(next)
        {

        }

        public override async Task Invoke(HttpContext context)
        {
            var url = context.Request.Path.HasValue ? context.Request.Path.Value : "";
            if (string.IsNullOrWhiteSpace(url) || url == "/")
            {
                var page = api.Pages.GetStartPage();

                if (page != null)
                {
                    context.Request.Path = new PathString(page.Route);
                    if (context.Request.QueryString.HasValue)
                        context.Request.QueryString = new QueryString(context.Request.QueryString.Value + "&id=" + page.Id);
                    else
                        context.Request.QueryString = new QueryString("?id=" + page.Id);
                }
            }
            await next.Invoke(context);
        }
    }
}
