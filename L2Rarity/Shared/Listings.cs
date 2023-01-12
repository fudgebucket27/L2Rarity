using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Rarity.Shared
{
    public class Listing
    {
        public string? name { get; set; }
        public string? id { get; set; }
    }

    public class Listings
    {
        public string? collectionid { get; set; }
        public string? name { get; set; }
        public List<Listing> listings { get; set; } = new List<Listing>();
    }
}
