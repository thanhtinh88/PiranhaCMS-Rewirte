using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Manager
{
    public static class Menu
    {
        #region Inner classes
        public class MenuItem
        {
            #region Properties
            /// <summary>
            /// Gets/sets the internal id.
            /// </summary>
            public string InternalId { get; set; }

            /// <summary>
            /// Gets/sets the display name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets/sets the optional css class.
            /// </summary>
            public string Css { get; set; }

            /// <summary>
            /// Gets/sets the manager controller.
            /// </summary>
            public string Controller { get; set; }

            /// <summary>
            /// Gets/sets the default action to invoke.
            /// </summary>
            public string Action { get; set; }

            /// <summary>
            /// Gets/sets the available items.
            /// </summary>
            public IList<MenuItem> Items { get; set; }
            #endregion

            public MenuItem()
            {
                Items = new List<MenuItem>();
            }
        }
        #endregion

        public static IList<MenuItem> Items = new List<MenuItem>()
        {
            new MenuItem()
            {
                InternalId = "Content", Name="Content", Css="glypicon glypicon-pencil", Items = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        InternalId = "Blocks", Name = "Blocks", Controller = "Block", Action = "List"
                    },
                    new MenuItem()
                    {
                        InternalId = "Categories", Name = "Categories", Controller = "Category", Action = "List"
                    },
                    new MenuItem()
                    {
                        InternalId = "Pages", Name = "Pages", Controller = "Page", Action = "List"
                    },
                    new MenuItem()
                    {
                        InternalId = "Posts", Name = "Posts", Controller = "Post", Action = "List"
                    }
                }
            },
            new MenuItem()
            {
                InternalId = "Settings", Name = "Settings", Css="glyphicon glyphicon-wrench", Items = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        InternalId = "PageTypes", Name = "Page Types", Controller = "PageType", Action = "List"
                    }
                }
            },
            new MenuItem()
            {
                InternalId = "System", Name = "System", Css="glyphicon glyphicon-cog", Items = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        InternalId = "Config", Name = "Config", Controller = "ConfigMgr", Action = "List"
                    }
                }
            }
        };
    }
}
