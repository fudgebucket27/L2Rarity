using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Rarity.Shared
{
    public class TraitFilter
    {
        public string? trait_type { get; set; }
        public string? value { get; set; } //value can be either string or int
        public int total { get; set; }
    }

    public class Filter
    {
        public string? FilterName { get; set; }
        public List<TraitFilter> TraitFilters { get; set; } = new List<TraitFilter>();
    }
}
