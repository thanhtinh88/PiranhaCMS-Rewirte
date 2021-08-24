using Microsoft.AspNetCore.Html;

namespace Piranha.Core.Extend.Regions
{
    [Extension(Types = ExtensionType.Region)]
    public class HtmlRegion: SimpleRegion<string>
    {
        public override object GetValue()
        {
            return new HtmlString(Value);
        }
    }
}
