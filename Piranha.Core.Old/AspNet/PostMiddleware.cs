using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.AspNet
{
    public class PostMiddleware: MiddlewareBase
    {
        /// <summary>
        /// Creates a new middleware instance.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        public PostMiddleware(RequestDelegate next, IApi api): base(next, api)
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

                if (!string.IsNullOrWhiteSpace(url) && url.Length > 2)
                {
                    var segments = url.Substring(1).Split(new char[] { '/' });
                    var category = api.Categories.GetBySlug(segments[0]);
                    if (category != null)
                    {
                        var post = api.Posts.GetBySlug(category.Id, segments[1]);
                        if (post != null)
                        {
                            var route = post.Route;
                            if (segments.Length > 2)
                                route += "/" + segments.Subset(2).Implode("/");

                            context.Request.Path = new PathString(route);
                            if (context.Request.QueryString.HasValue)
                            {
                                context.Request.QueryString = new QueryString(context.Request.QueryString.Value + "&id=" + post.Id + "&piranha_handled=true");
                            }
                            else
                            {
                                context.Request.QueryString = new QueryString("?id=" + post.Id + "&piranha_handled=true");
                            }
                        }
                    }
                } 
            }

            await next.Invoke(context);
        }
    }
}
