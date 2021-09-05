using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend.Fields
{
    [Field(Name = "Markdown", Shorthand = "Markdown")]
    public class MarkdownField: SimpleField<string>
    {
        public static implicit operator MarkdownField(string str)
        {
            return new MarkdownField() { Value = str };
        }
    }
}
