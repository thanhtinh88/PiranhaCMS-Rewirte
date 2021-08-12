using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend
{
    public class Extension: IExtension
    {
        public virtual object GetValue()
        {
            return this;
        }

        public static implicit operator string(Extension e)
        {
            return App.ExtensionManager.Serialize(e);
        }
    }
}
