using L2Rarity.Shared;

namespace L2Rarity.Server.Models
{
    public interface ICosmosDbService
    {
        Task<Rankings?> GetCollectionOverallRankingsAsync(string collectionId);
        Task<NftMetadata?> GetCollectionSingleRankingsAsync(string collectionId, string nftNumber);
        Task<Listings?> GetListingsAsync();
        Task<List<Filter>?> GetTraitFiltersAsync(string collectionId);
        Task<List<Ranking>?> GetItemsFilteredAsync(string collectionId, string traitQuery);
        Task<List<Filter>?> GetTraitFiltersFilteredAsync(string collectionId, string traitQuery);
        Task<CollectionStats?> GetCollectionStatsAsync(string collectionId);
    }
}
