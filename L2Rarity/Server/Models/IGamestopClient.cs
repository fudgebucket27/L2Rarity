using L2Rarity.Shared;

namespace L2Rarity.Server.Models
{
    public interface IGamestopClient
    {
        Task<Dictionary<string, Value>> GetNftListing(string nftId);
    }
}
