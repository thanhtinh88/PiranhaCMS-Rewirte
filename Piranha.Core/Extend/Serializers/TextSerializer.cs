using Piranha.Core.Extend.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend.Serializers
{
    public class TextSerializer:ISerializer
    {
        public string Serialize(IExtension obj)
        {
            return ((TextRegion)obj).Value;
        }

        public IExtension Deserialize(string str)
        {
            return new TextRegion()
            {
                Value = str
            };
        }
    }
}
