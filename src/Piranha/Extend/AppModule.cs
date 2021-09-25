using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    public sealed class AppModule: AppDataItem
    {
        /// <summary>
        /// Gets/sets the module instance.
        /// </summary>
        public IModule Instance { get; set; }
    }
}
