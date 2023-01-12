using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Rarity.Shared
{
    public class LoopringSaleInfo
    {
        public string? minPricePerNftInWei { get; set; }
        public string? amountForSale { get; set; }
    }

    public class Value
    {
        public string? minPricePerNft { get; set; }
        public string? amountForSale { get; set; }
        public LoopringSaleInfo? loopringSaleInfo { get; set; }
        public int likeCount { get; set; }
    }
    public class GameStopNftListing
    {
        public string? @class { get; set; }
        public Value? value { get; set; }
    }
}
