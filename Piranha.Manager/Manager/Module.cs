using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Manager
{
    public class Module:Extend.IModule
    {
        public static IMapper Mapper { get; private set; }

        public void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.PageModelBase, Areas.Manager.Models.PageEditModel>()
                    .ForMember(m => m.PageType, o => o.Ignore())
                    .ForMember(m => m.Regions, o => o.Ignore());
                cfg.CreateMap<Areas.Manager.Models.PageEditModel, Models.PageModelBase>()
                    .ForMember(m => m.TypeId, o => o.Ignore())
                    .ForMember(m => m.Created, o => o.Ignore())
                    .ForMember(m => m.LastModified, o => o.Ignore());
            });

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }
    }
}
