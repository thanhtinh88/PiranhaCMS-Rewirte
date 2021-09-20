using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Piranha.Manager
{
    public class Module:Extend.IModule
    {
        #region Properties
        /// <summary>
        /// Gets the mapper.
        /// </summary>
        public static IMapper Mapper { get; private set; }

        /// <summary>
        /// The assembly.
        /// </summary>
        internal static Assembly Assembly;

        /// <summary>
        /// Last modification date of the assembly.
        /// </summary>
        internal static DateTime LastModified;
        #endregion

        public void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.PageBase, Piranha.Areas.Manager.Models.PageEditModel>()
                    .ForMember(m => m.PageType, o => o.Ignore())
                    .ForMember(m => m.Regions, o => o.Ignore());
                cfg.CreateMap<Piranha.Areas.Manager.Models.PageEditModel, Models.PageBase>()
                    .ForMember(m => m.TypeId, o => o.Ignore())
                    .ForMember(m => m.Created, o => o.Ignore())
                    .ForMember(m => m.LastModified, o => o.Ignore());
            });

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();

            Assembly = this.GetType().GetTypeInfo().Assembly;
            LastModified = new FileInfo(Assembly.Location).LastWriteTime;
        }
    }
}
