﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Web
{
    public class StartPageRouter
    {
        /// <summary>
        /// Invokes the router.
        /// </summary>
        /// <param name="api">The current api</param>
        /// <param name="url">The requested url</param>
        /// <returns>The piranha response, null if no matching page was found</returns>
        public static IRouteResponse Invoke(Api api, string url)
        {
            if (string.IsNullOrWhiteSpace(url) || url == "/")
            {
                var page = api.Pages.GetStartPage();

                if (page != null)
                {
                    return new RouteResponse()
                    {
                        Route = page.Route ?? "/page",
                        QueryString = "id=" + page.Id + "&startpage=true&piranha_handled=true"
                    };
                }
            }
            return null;
        }
    }
}
