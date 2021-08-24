using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Piranha.AspNet
{
    public class ArchiveMiddleware: MiddlewareBase
    {
        /// <summary>
        /// Creates a new middleware instance.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        public ArchiveMiddleware(RequestDelegate next, IApi api): base(next, api)
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

                if (!string.IsNullOrWhiteSpace(url) && url.Length > 1)
                {
                    var segments = url.Substring(1).Split(new char[] { '/' });

                    if (segments.Length >= 1)
                    {
                        var category = api.Categories.GetModelBySlug(segments[0]);

                        if (category != null)
                        {
                            var route = category.ArchiveRoute ?? "/archive";

                            int? page = null;
                            int? year = null;
                            int? month = null;
                            bool foundPage = false;

                            for (int i = 0; i < segments.Length; i++)
                            {
                                if (segments[i]=="page")
                                {
                                    foundPage = true;
                                    continue;
                                }

                                if (foundPage)
                                {
                                    try
                                    {
                                        page = Convert.ToInt32(segments[i]);
                                    }
                                    catch { }
                                    break;
                                }

                                if (!year.HasValue)
                                {
                                    try
                                    {
                                        year = Convert.ToInt32(segments[i]);
                                        if (year.Value > DateTime.Now.Year)
                                        {
                                            year = DateTime.Now.Year;
                                        }
                                    }
                                    catch { }
                                }
                                else
                                {
                                    try
                                    {
                                        month = Math.Max(Math.Min(Convert.ToInt32(segments[i]), 12), 1);
                                    }
                                    catch { }
                                }
                            }
                            context.Request.Path = new PathString(route);

                            var query =
                                "id=" + category.Id + "&" +
                                "year=" + year + "&" +
                                "month=" + month + "&" +
                                "page=" + page + "&piranha_handled=true";
                            if (context.Request.QueryString.HasValue)
                                context.Request.QueryString = new QueryString(context.Request.QueryString.Value + "&" + query);
                            else
                                context.Request.QueryString = new QueryString("?" + query);
                        }                        
                    }
                }
            }
            await next.Invoke(context);
        }
    }
}
