using L2Rarity.Shared;

namespace L2Rarity.Server.Models
{
    public interface ISqlService
    {
        Task<List<Listing1Hour>> GetListings(string _collectionId);
        Task<Collection> GetRandomCollection();
        Task<List<Collection>> GetCollections();
    }
}
