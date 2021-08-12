using Piranha.Core.Extend.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend.Serializers
{
    public class HtmlSerializer:ISerializer
    {
        public string Serialize(IExtension obj)
        {
            return ((HtmlRegion)obj).Value;
        }

        public IExtension Deserialize(string str)
        {
            return new HtmlRegion()
            {
                Value = str
            };
        }
    }
}
