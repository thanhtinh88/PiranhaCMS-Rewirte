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
        private IList<Extend.BlockType> blockTypes;
        private IList<Extend.PageType> pageTypes;
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

        /// <summary>
        /// Gets the currently registered block types.
        /// </summary>
        public static IList<Extend.BlockType> BlockTypes
        {
            get { return instance.blockTypes; }
        }

        /// <summary>
        /// Gets the currently registered page types.
        /// </summary>
        public static IList<Extend.PageType> PageTypes
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
            blockTypes = new List<Extend.BlockType>();
            pageTypes = new List<Extend.PageType>();
        }


        /// <summary>
        /// Initializes the application.
        /// </summary>
        /// <param name="configurate">Action for configurating the application</param>
        public static void Init(IApi api, params Extend.IModule[] modules)
        {            
            instance.Initialize(api, modules);
        }

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
                        
                        // Get page types
                        pageTypes = api.PageTypes.Get();
                        blockTypes = api.BlockTypes.Get();

                        InitializeModules(modules);

                        isInitialized = true;
                    }
                }
            }
        }

        private void InitializeModules(Extend.IModule[] modules)
        {
            // Add modules if present
            if (modules!= null)
            {
                foreach (var module in modules)
                {
                    this.modules.Add(module);
                }
            }

            // Initialize all modules
            foreach (var module in this.modules)
            {
                module.Init();
            }
        }
    }
}
