using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF
{
    public class Module: Extend.IModule
    {
        /// <summary>
        /// Gets the mapper.
        /// </summary>
        public static IMapper Mapper { get; set; }

        /// <summary>
        /// Gets the registered serializers.
        /// </summary>
        public static Serializers.SerializerManager Serializer { get; private set; }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Data.Category, Models.Category>();
                cfg.CreateMap<Data.Category, Models.CategoryModel>();

                cfg.CreateMap<Models.Category, Data.Category>()
                    .ForMember(m => m.Id, o => o.Ignore())
                    .ForMember(m => m.ArchiveTitle, o => o.Ignore())
                    .ForMember(m => m.ArchiveKeywords, o => o.Ignore())
                    .ForMember(m => m.ArchiveDescription, o => o.Ignore())
                    .ForMember(m => m.ArchiveRoute, o => o.Ignore())
                    .ForMember(m => m.Created, o => o.Ignore())
                    .ForMember(m => m.LastModified, o => o.Ignore());
                cfg.CreateMap<Models.CategoryModel, Data.Category>()
                    .ForMember(m => m.Id, o => o.Ignore())
                    .ForMember(m => m.Created, o => o.Ignore())
                    .ForMember(m => m.LastModified, o => o.Ignore());

                cfg.CreateMap<Data.Page, Models.PageModelBase>();
                cfg.CreateMap<Models.PageModelBase, Data.Page>()
                    .ForMember(m => m.Id, o => o.Ignore())
                    .ForMember(m => m.Created, o => o.Ignore())
                    .ForMember(m => m.LastModified, o => o.Ignore())
                    .ForMember(m => m.PageType, o => o.Ignore())
                    .ForMember(m => m.Fields, o => o.Ignore());

                cfg.CreateMap<Data.Post, Models.PostModel>()
                    .ForMember(m => m.Permalink, o => o.MapFrom(p => p.Category.Slug + "/" + p.Slug))
                    .ForMember(m => m.Category, o => o.Ignore())
                    .ForMember(m => m.Tags, o => o.Ignore());
            });

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();

            Serializer = new Serializers.SerializerManager();
            Serializer.Register<Extend.Fields.HtmlField>(new Serializers.StringSerializer<Extend.Fields.HtmlField>());
            Serializer.Register<Extend.Fields.MarkdownField>(new Serializers.StringSerializer<Extend.Fields.MarkdownField>());
            Serializer.Register<Extend.Fields.StringField>(new Serializers.StringSerializer<Extend.Fields.StringField>());
            Serializer.Register<Extend.Fields.TextField>(new Serializers.StringSerializer<Extend.Fields.TextField>());
        }
    }
}
