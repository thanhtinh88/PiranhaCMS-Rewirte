using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.AspNet
{
    public static class FieldExtensionscs
    {
        public static HtmlString Html(this Piranha.Extend.Fields.HtmlField field)
        {
            return new HtmlString(field.Value);
        }
    }
}
