using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.AspNet
{
    public class PostMiddleware: MiddlewareBase
    {
        /// <summary>
        /// Creates a new middleware instance.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        public PostMiddleware(RequestDelegate next): base(next)
        {

        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The current http context</param>
        /// <returns>An async task</returns>
        public override async Task Invoke(HttpContext context, IApi api)
        {
            if (!IsHandled(context) && !context.Request.Path.Value.StartsWith("/manager/assets"))
            {
                var url = context.Request.Path.HasValue ? context.Request.Path.Value : "";

                if (!string.IsNullOrWhiteSpace(url) && url.Length > 1)
                {
                    var segments = url.Substring(1).Split(new char[] { '/' });

                    if (segments.Length >= 2)
                    {
                        var post = api.Posts.GetBySlug(segments[0], segments[1]);
                        if (post != null)
                        {
                            var route = post.Route ?? "/post";
                            if (segments.Length > 2)
                                route += "/" + segments.Subset(2).Implode("/");

                            context.Request.Path = new PathString(route);
                            if (context.Request.QueryString.HasValue)
                                context.Request.QueryString = new QueryString(context.Request.QueryString.Value + "&id=" + post.Id + "&piranha_handled=true");
                            else
                                context.Request.QueryString = new QueryString("?id=" + post.Id + "&piranha_handled=true");
                        }
                    }
                } 
            }

            await next.Invoke(context);
        }
    }
}
