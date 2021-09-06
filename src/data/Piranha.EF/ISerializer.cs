using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF
{
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the given object.
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>The serialized value</returns>
        string Serialize(object obj);

        /// <summary>
        /// Deserializes the given string.
        /// </summary>
        /// <param name="str">The serialized value</param>
        /// <returns>The object</returns>
        object Deserialize(string str);
    }
}
