using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Rarity.Shared
{
    public class Collection
    {
        public string? CollectionId { get; set; }
        public string? CollectionDisplayName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        public string? CollectionLink
        {
            get
            {
                return $"/nft-collections/{CollectionId}";
            }
        }
    }
}
