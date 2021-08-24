using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    public sealed class FieldInfo
    {
        public string Name { get; set; }
        public string Shorthand { get; set; }
        public string CLRType { get; set; }
        public Type Type { get; set; }
    }
}
