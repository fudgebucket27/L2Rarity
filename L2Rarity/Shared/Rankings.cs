using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Rarity.Shared
{
    public class Rankings
    {
        public string? collectionid { get; set; }
        public string? name { get; set; }

        public string? collectionname { get; set; }

        public string? marketplaceurl { get; set; }

        public string? contractAddress { get; set; }
        public int total { get; set; }
        public List<Ranking> rankings { get; set; } = new List<Ranking>();

    }
    public class Ranking
    {
        public string? i { get; set; } //id
        public int r { get; set; } //rank
        public decimal s { get; set; } //score

        public string? im { get; set; } //image

        public string? ni { get; set; } //nft id
        public string? ti { get; set; } //token id

    }
}
