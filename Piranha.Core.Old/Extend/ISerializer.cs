using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend
{
    /// <summary>
    /// Interface for extension serializers.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the object into a string.
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>The serialized object</returns>
        string Serialize(IExtension obj);

        /// <summary>
        /// Deserializes the given string into an object.
        /// </summary>
        /// <param name="str">The serialized data</param>
        /// <returns>The object</returns>
        IExtension Deserialize(string str);
    }
}
