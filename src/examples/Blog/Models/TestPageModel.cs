using Piranha.Builder.Attribute;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Collections.Generic;

namespace Blog.Models
{
    public class TeaserItem
    {
        [Field]
        public StringField Title { get; set; }
        [Field]
        public HtmlField Body { get; set; }
    }

    public class TestPageModel: Page<StartPageModel>
    {
        [Region]
        public HtmlField Content { get; set; }

        [Region(Max = 5)]
        public IList<TeaserItem> Teasers { get; set; }

        public TestPageModel()
        {
            Teasers = new List<TeaserItem>();
        }
    }
}
