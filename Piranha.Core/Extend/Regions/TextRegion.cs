using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend.Regions
{
    [Extension(Types = ExtensionType.Region)]
    public class TextRegion: SimpleRegion<string>
    {
    }
}
