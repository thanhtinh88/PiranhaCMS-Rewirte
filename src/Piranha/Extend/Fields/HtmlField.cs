using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend.Fields
{
    [Field(Name = "Html", Shorthand = "Html")]
    public class HtmlField: SimpleField<string>
    {
        public static implicit operator HtmlField(string str)
        {
            return new HtmlField() { Value = str };
        }
    }
}
