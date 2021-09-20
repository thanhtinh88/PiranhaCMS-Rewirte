using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend.Fields
{
    [Field(Name ="Date", Shorthand ="Date")]
    public class DateField: SimpleField<DateTime>
    {
        public static implicit operator DateField(DateTime date)
        {
            return new DateField() { Value = date };
        }
    }
}
