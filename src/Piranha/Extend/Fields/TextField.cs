using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend.Fields
{
    [Field(Name = "Field",Shorthand = "Text")]
    public class TextField: SimpleField<string>
    {
        public static implicit operator TextField(string str)
        {
            return new TextField() { Value = str };
        }
    }
}
