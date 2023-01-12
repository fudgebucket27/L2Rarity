using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Rarity.Shared
{
    public class NftMetadata
    {
        public string? collectionid { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

        public string? animation_url { get; set; }
        public string? image { get; set; }
        public string? nftNumber { get; set; }

        public int Rank { get; set; }

        public string? Tier { get; set; }
        public decimal Score { get; set; }

        public string? id { get; set; }

        public string? nftId { get; set; }
        public string? tokenId { get; set; }

        public string? nftData { get; set; }

        public string? marketplaceUrl { get; set; }

        public List<Trait>? attributes { get; set; }


    }

    public class Trait
    {
        public string? trait_type { get; set; }
        public string? value { get; set; } //value can be either string or int

        public int count { get; set; }

        public decimal score { get; set; }

        public string ReturnPercentange(int collectionTotal)
        {
            decimal decCount = count;
            decimal decCollectionTotal = collectionTotal;
            decimal percentageDecimal = (decCount / decCollectionTotal) * 100;
            return percentageDecimal.ToString("0.00");
        }
    }   
}
