using AutoMapper;
using HeyRed.MarkdownSharp;
using Piranha.Extend;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Piranha
{
    public sealed class App
    {
        #region Members
        private static readonly App instance = new App();
        private readonly object mutex = new object();
        private bool isInitialized = false;
        private AppFieldList fields;
        private AppModuleList modules;
        private IMapper mapper;
        private Markdown markdown;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the currently registered field types.
        /// </summary>
        public static AppFieldList Fields { get => instance.fields; }

        /// <summary>
        /// Gets the currently registred modules.
        /// </summary>
        public static AppModuleList Modules { get => instance.modules; }

        /// <summary>
        /// Gets the application object mapper.
        /// </summary>
        public static IMapper Mapper { get => instance.mapper; }

        /// <summary>
        /// Gets the markdown converter.
        /// </summary>
        public static Markdown Markdown { get => instance.markdown; }

        /// <summary>
        /// Gets the binding flags for retrieving a region from a strongly typed model.
        /// </summary>
        public static BindingFlags PropertyBindings
        {
            get => BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;
        }
        #endregion

        public App()
        {
            fields = new AppFieldList();
            modules = new AppModuleList();
        }

        /// <summary>
        /// Initializes the application object.
        /// </summary>
        /// <param name="api">The current api</param>
        public static void Init(Api api)
        {
            instance.Initialize(api);
        }

        #region Private methods
        /// <summary>
        /// Initializes the application object.
        /// </summary>
        /// <param name="api">The current api</param>
        private void Initialize(Api api)
        {
            if (!isInitialized)
            {
                lock (mutex)
                {
                    if (!isInitialized)
                    {
                        // Configure object mapper
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<Data.Page, Models.PageBase>()
                                .ForMember(p => p.TypeId, o => o.MapFrom(m => m.PageTypeId));
                            cfg.CreateMap<Models.PageBase, Data.Page>()
                                .ForMember(p => p.PageTypeId, o => o.MapFrom(m => m.TypeId))
                                .ForMember(p => p.Fields, o => o.Ignore())
                                .ForMember(p => p.Created, o => o.Ignore())
                                .ForMember(p => p.LastModified, o => o.Ignore());
                        });
                        mapperConfig.AssertConfigurationIsValid();
                        mapper = mapperConfig.CreateMapper();

                        // Compose field types
                        fields.Register<Extend.Fields.HtmlField>();
                        fields.Register<Extend.Fields.MarkdownField>();
                        fields.Register<Extend.Fields.TextField>();

                        // Create markdown converter
                        markdown = new Markdown();

                        // Initialize all modules
                        foreach (var module in this.modules)
                        {
                            module.Instance.Init();
                        }
                        isInitialized = true;
                    }
                }
            }
        }
        #endregion
    }
}
