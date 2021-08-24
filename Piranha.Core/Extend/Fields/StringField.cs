using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend.Fields
{
    [Field( Name = "string", Shorthand = "string")]
    public class StringField: SimpleField<string>
    {
        public static implicit operator StringField(string str)
        {
            return new StringField() { Value = str };
        }
    }
}
