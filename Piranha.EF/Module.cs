using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF
{
    public class Module: Extend.IModule
    {
        public static IMapper Mapper { get; set; }
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
                    .ForMember(m => m.Type, o => o.Ignore())
                    .ForMember(m => m.Fields, o => o.Ignore());

                cfg.CreateMap<Data.Post, Models.PostModel>()
                    .ForMember(m => m.Permalink, o => o.MapFrom(p => p.Category.Slug + "/" + p.Slug))
                    .ForMember(m => m.Category, o => o.Ignore())
                    .ForMember(m => m.Tags, o => o.Ignore());
            });

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }
    }
}
