using AutoMapper;
using Piranha.Data;
using Piranha.Data.Data;
using Piranha.Core.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core
{
    /// <summary>
    /// The main Piranha application object.
    /// </summary>
    public sealed class App
    {
        #region Members
        /// <summary>
        /// The private singleton instance.
        /// </summary>
        private static readonly App instance = new App();

        /// <summary>
        /// The private application state.
        /// </summary>
        private bool isInitialized = false;

        /// <summary>
        /// The private initialization mutex.
        /// </summary>
        private object mutext = new object();

        /// <summary>
        /// The private extension manager
        /// </summary>
        private Extend.ExtensionManager extensionManager;

        /// <summary>
        /// The private auto mapper configuration.
        /// </summary>
        private MapperConfiguration mapper;
        #endregion

        private App() { }

        #region Static properties
        /// <summary>
        /// Gets the current extension manager.
        /// </summary>
        public static ExtensionManager ExtensionManager
        {
            get => instance.extensionManager;
        }

        /// <summary>
        /// Gets a mapper instance.
        /// </summary>
        public static IMapper Mapper
        {
            get => instance.mapper.CreateMapper();
        }
        #endregion

        /// <summary>
        /// Initializes the application.
        /// </summary>
        /// <param name="configurate">Action for configurating the application</param>
        public static void Init(Action<AppConfig> configurate = null)
        {
            var config = new AppConfig();

            if (configurate != null)
                configurate(config);
            instance.Initialize(config);
        }

        #region Private methods
        /// <summary>
        /// Initializes the application instance.
        /// </summary>
        private void Initialize(AppConfig config)
        {
            if (!isInitialized)
            {
                lock (mutext)
                {
                    if (!isInitialized)
                    {
                        // Create & compose the extension manager
                        extensionManager = new ExtensionManager().Compose();

                        // Setup auto mapper
                        mapper = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<Page, Models.PageModel>()
                                .ForMember(m => m.Permalink, o => o.Ignore())
                                .ForMember(m => m.IsStartPage, o => o.Ignore())
                                .ForMember(m => m.Regions, o => o.Ignore());
                            cfg.CreateMap<Page, Models.SiteMapModel>()
                                .ForMember(m => m.Permalink, o => o.Ignore())
                                .ForMember(m => m.Level, o => o.Ignore())
                                .ForMember(m => m.Children, o => o.Ignore());
                            cfg.CreateMap<Post, Models.PostListModel>()
                                .ForMember(m => m.Permalink, o => o.Ignore());
                            cfg.CreateMap<Post, Models.PostModel>()
                                .ForMember(m => m.Permalink, o => o.Ignore())
                                .ForMember(m => m.Regions, o => o.Ignore());
                        });
                        mapper.AssertConfigurationIsValid();
                        
                        isInitialized = true;
                    }
                }
            }
        }
        #endregion
    }
}
