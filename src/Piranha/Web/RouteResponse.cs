using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Web
{
    public class RouteResponse: IRouteResponse
    {
        /// <summary>
        /// Gets/sets the route.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets/sets the optional query string.
        /// </summary>
        public string QueryString { get; set; }
    }
}
