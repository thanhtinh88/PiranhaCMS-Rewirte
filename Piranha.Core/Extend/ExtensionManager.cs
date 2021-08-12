using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Piranha.Core.Extend
{
    /// <summary>
    /// The extension manager is responsible for all imported types
    /// as well as for serializing & deserializing extension data.
    /// </summary>
    public sealed class ExtensionManager
    {
        #region Members
        /// <summary>
        /// The private list of extensions.
        /// </summary>
        private IList<ExtensionInfo> extensions;

        /// <summary>
        /// The private collection of serializers.
        /// </summary>
        private IDictionary<Type, ISerializer> serializers;
        #endregion

        #region Properties
        /// <summary>
        /// The currently registered extensions.
        /// </summary>
        public IEnumerable<ExtensionInfo> Extensions
        {
            get { return extensions; }
        }

        /// <summary>
        /// The currently registered serializers.
        /// </summary>
        public IEnumerable<ISerializer> Serializers
        {
            get { return serializers.Values; }
        }
        #endregion

        internal ExtensionManager()
        {
            extensions = new List<ExtensionInfo>();
            serializers = new Dictionary<Type, ISerializer>();
        }

        /// <summary>
        /// Registers the extension of the specified type.
        /// </summary>
        /// <typeparam name="T">The extensions type</typeparam>
        public void RegisterExtension<T>()
        {
            if (typeof(IExtension).IsAssignableFrom(typeof(T)))
            {
                var attr = typeof(T).GetTypeInfo().GetCustomAttribute<ExtensionAttribute>();

                if (attr != null)
                {
                    extensions.Add(new ExtensionInfo()
                    {
                        CLRType = typeof(T),
                        Types = attr.Types
                    });
                }
            }
        }

        /// <summary>
        ///  Removes the extension of the specified type.
        /// </summary>
        /// <typeparam name="T">The extension type</typeparam>
        /// <returns>If the extension was successfully removed</returns>
        public bool RemoveExtension<T>()
        {
            var ext = extensions.SingleOrDefault(e => e.CLRType == typeof(T));
            if (ext != null)
                return extensions.Remove(ext);
            return false;
        }

        /// <summary>
        /// Registers a serializer for the specified type.
        /// </summary>
        /// <typeparam name="T">The extension type</typeparam>
        /// <param name="serializer">The serializer</param>
        public void RegisterSerializer<T>(ISerializer serializer)
        {
            serializers.Add(typeof(T), serializer);
        }

        /// <summary>
        /// Removes the serializer registered for the specified type.
        /// </summary>
        /// <typeparam name="T">The extension type</typeparam>
        /// <returns>If the serializer was successfully removed</returns>
        public bool RemoveSerializer<T>()
        {
            return serializers.Remove(typeof(T));
        }

        /// <summary>
        /// Deserializes the current extension.
        /// </summary>
        /// <param name="CLRType">The CLR type FullName</param>
        /// <param name="value">The serialized data</param>
        /// <returns>The deserialized extension</returns>
        public IExtension Deserialize(string CLRType, string value)
        {
            var extension = Extensions.SingleOrDefault(e => e.CLRType.FullName == CLRType);

            if (extension != null)
            {
                if (serializers.ContainsKey(extension.CLRType))
                    return serializers[extension.CLRType].Deserialize(value);
                else
                    return (IExtension)JsonConvert.DeserializeObject(value, extension.CLRType);
            }

            return null;
        }

        /// <summary>
        /// Serializes the given extensions.
        /// </summary>
        /// <param name="obj">The extension</param>
        /// <returns>The serialized data</returns>
        public string Serialize(IExtension obj)
        {
            var type = obj.GetType();

            if (serializers.ContainsKey(type))
                return serializers[type].Serialize(obj);
            else
                return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Composes the manager.
        /// </summary>
        internal ExtensionManager Compose()
        {
            // Add default extensions
            RegisterExtension<Regions.HtmlRegion>();
            RegisterExtension<Regions.TextRegion>();

            // Add default serializers
            RegisterSerializer<Regions.HtmlRegion>(new Serializers.HtmlSerializer());
            RegisterSerializer<Regions.TextRegion>(new Serializers.TextSerializer());

            // Check if application is subscribing to composition
            if (Hooks.Extend.OnCompose != null)
                Hooks.Extend.OnCompose(this);
            return this;
        }
    }
}
