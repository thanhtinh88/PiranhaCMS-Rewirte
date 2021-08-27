using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace Piranha
{
    /// <summary>
    /// The main application object.
    /// </summary>
    public sealed class App
    {
        #region Members
        /// <summary>
        /// The private singleton instance.
        /// </summary>
        private static readonly App instance = new App();

        /// <summary>
        /// The private initialization mutex.
        /// </summary>
        private object mutext = new object();

        /// <summary>
        /// The private application state.
        /// </summary>
        private bool isInitialized = false;

        private Extend.FieldInfoList fields;
        private List<Extend.IModule> modules;
        private IList<Models.PageType> pageTypes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current extension manager.
        /// </summary>
        public static Extend.FieldInfoList Fields
        {
            get => instance.fields;
        }

        /// <summary>
		/// Gets the current storage factory.
		/// </summary>
		public static IList<Extend.IModule> Modules
        {
            get { return instance.modules; }
        }

        public static IList<Models.PageType> PageTypes
        {
            get { return instance.pageTypes; }
        }

        /// <summary>
        /// Gets a mapper instance.
        /// </summary>
        public static BindingFlags PropertyBindings
        {
            get { return BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance; }
        }
        #endregion

        private App() 
        {
            fields = new Extend.FieldInfoList();
            modules = new List<Extend.IModule>();
        }


        /// <summary>
        /// Initializes the application.
        /// </summary>
        /// <param name="configurate">Action for configurating the application</param>
        public static void Init(IApi api, Extend.IModule[] modules = null)
        {            
            instance.Initialize(api, modules);
        }

        #region Private methods
        /// <summary>
        /// Initializes the application instance.
        /// </summary>
        private void Initialize(IApi api, Extend.IModule[] modules = null)
        {
            if (!isInitialized)
            {
                lock (mutext)
                {
                    if (!isInitialized)
                    {
                        // Compose field types
                        fields.Register<Extend.Fields.HtmlField>();
                        fields.Register<Extend.Fields.StringField>();
                        fields.Register<Extend.Fields.TextField>();

                        // Compose app config
                        if (File.Exists("piranha.json"))
                        {
                            using (var file = File.OpenRead("piranha.json"))
                            {
                                using (var reader = new StreamReader(file))
                                {
                                    var config = JsonConvert.DeserializeObject<AppConfig>(reader.ReadToEnd());
                                    config.Ensure();

                                    foreach (var type in config.PageTypes)
                                        api.PageTypes.Save(type);
                                }
                            }
                        }

                        // Get page types
                        pageTypes = api.PageTypes.Get();

                        // Compose $ initialize modules
                        foreach (var module in modules)
                        {
                            module.Init();
                            this.modules.Add(module);
                        }
                        isInitialized = true;
                    }
                }
            }
        }
        #endregion
    }
}
