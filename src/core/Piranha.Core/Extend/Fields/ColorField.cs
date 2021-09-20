using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend.Fields
{
    [Field(Name ="Color", Shorthand ="Color")]
    public class ColorField: SimpleField<string>
    {
        public static implicit operator ColorField(string str)
        {
            return new ColorField() { Value = str };
        }
    }
}
