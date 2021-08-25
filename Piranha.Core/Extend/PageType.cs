using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Extend
{
    public sealed class PageType: ContentType
    {
        #region Properties
        /// <summary>
        /// Gets/sets the optional route.
        /// </summary>
        public string Route { get; set; }
        #endregion

        internal PageType() : base() { }

        /// <summary>
        /// Validates that the page type is correctly defined.
        /// </summary>
        public void Ensure()
        {
            if (Regions.Select(r => r.Id).Distinct().Count() != Regions.Count)
                throw new Exception($"Region Id not unique for page type {Id}");

            foreach (var region in Regions)
            {
                if (region.Fields.Select(f => f.Id).Distinct().Count() != region.Fields.Count)
                    throw new Exception($"Field Id not unique for page type {Id}");
                foreach (var field in region.Fields)
                {
                    field.Id = field.Id ?? "Default";
                    field.Title = field.Title ?? field.Id;
                }
            }
        }
    }
}
