using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Web
{
    public interface IRouteResponse
    {
        /// <summary>
        /// Gets/sets the route.
        /// </summary>
        string Route { get; set; }

        /// <summary>
        /// Gets/sets the optional query string.
        /// </summary>
        string QueryString { get; set; }
    }
}
