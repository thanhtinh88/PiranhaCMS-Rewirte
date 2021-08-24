using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Core
{
    /// <summary>
    /// The startup options for the Piranha middleware.
    /// </summary>
    [Flags]
    public enum Handle
    {
        All = 1,
        Archives = 2,
        Media = 4,
        Pages = 8,
        Posts = 16,
        StartPage = 32
    }
}
