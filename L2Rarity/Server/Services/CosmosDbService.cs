using L2Rarity.Server.Models;
using L2Rarity.Shared;
using Microsoft.Azure.Cosmos;
using MudBlazor.Charts;
using System.Diagnostics;

namespace L2Rarity.Server.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;
        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task<Rankings?> GetCollectionOverallRankingsAsync(string collectionId)
        {
            try
            {
                Rankings? rankings = null;
                var query = new QueryDefinition(query: "SELECT * FROM data c WHERE c.name = @name and c.collectionid = @collectionid")
                    .WithParameter("@name", "ranks")
                    .WithParameter("@collectionid", collectionId);
                using (FeedIterator<Rankings> iterator = _container.GetItemQueryIterator<Rankings>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<Rankings> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            rankings = item;
                        }
                    }
                }
                return rankings;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<CollectionStats?> GetCollectionStatsAsync(string collectionId)
        {
            try
            {
                CollectionStats? stats = null;
                var query = new QueryDefinition(query: "SELECT * FROM data c WHERE c.name = @name and c.collectionid = @collectionid")
                    .WithParameter("@name", "stats")
                    .WithParameter("@collectionid", collectionId);
                using (FeedIterator<CollectionStats> iterator = _container.GetItemQueryIterator<CollectionStats>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<CollectionStats> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            stats = item;
                        }
                    }
                }
                return stats;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<NftMetadata?> GetCollectionSingleRankingsAsync(string collectionId, string nftNumber)
        {
            try
            {
                NftMetadata? nftMetadata = null;
                // Can write raw SQL, but the iteration is a little annoying. 
                int nftNumberAsInt;
                bool isNumeric = int.TryParse(nftNumber, out nftNumberAsInt);
                var query = new QueryDefinition(query: "SELECT * FROM data c WHERE c.nftNumber = @nftNumber and c.collectionid = @collectionid");
                if (isNumeric == true)
                {
                    query.WithParameter("@nftNumber", nftNumberAsInt);
                    query.WithParameter("@collectionid", collectionId);
                }
                else
                {
                    query.WithParameter("@nftNumber", nftNumber);
                    query.WithParameter("@collectionid", collectionId);
                }
                using (FeedIterator<NftMetadata> iterator = _container.GetItemQueryIterator<NftMetadata>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<NftMetadata> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            nftMetadata = item;
                        }
                    }
                }
                return nftMetadata;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Listings?> GetListingsAsync()
        {
            try
            {
                Listings? listings = null;
                var query = new QueryDefinition(query: "SELECT * FROM data c WHERE c.name = @name and c.collectionid = @collectionid")
                    .WithParameter("@name", "listings")
                    .WithParameter("@collectionid", "listings");
                using (FeedIterator<Listings> iterator = _container.GetItemQueryIterator<Listings>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<Listings> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            listings = item;
                        }
                    }
                }
                return listings;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Filter>?> GetTraitFiltersAsync(string collectionId)
        {
            List<Filter>? filters = new List<Filter>();
            try
            {
                var query = new QueryDefinition(query: "SELECT t.trait_type, t['value'], count(1) as total FROM c Join t IN c.attributes where c.collectionid = @collectionId group by t.trait_type, t['value']")
                .WithParameter("@collectionId", collectionId);
                using (FeedIterator<TraitFilter> iterator = _container.GetItemQueryIterator<TraitFilter>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<TraitFilter> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            if (!filters.Where(x => x.FilterName == item.trait_type).Any())
                            {
                                Filter filter = new Filter();
                                filter.FilterName = item.trait_type;
                                filter.TraitFilters = new List<TraitFilter>();
                                filter.TraitFilters.Add(item);
                                filters.Add(filter);
                            }
                            else
                            {
                                filters.Where(x => x.FilterName == item.trait_type).FirstOrDefault()!.TraitFilters.Add(item);
                            }
                        }
                    }
                }

                filters = filters.OrderBy(x => x.FilterName).ToList();
                filters = filters.OrderBy(x => x.FilterName).ToList();
                filters.ForEach(o => o.TraitFilters = o.TraitFilters.OrderBy(d => d.value).ToList());

                return filters;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<List<Ranking>?> GetItemsFilteredAsync(string collectionId, string traitQuery)
        {
            try
            {
                List<Ranking> rankings = new List<Ranking>();

                var query = new QueryDefinition(query: $"SELECT * FROM c WHERE c.collectionid = @collectionId {traitQuery}")
                .WithParameter("@collectionId", collectionId);

                using (FeedIterator<NftMetadata> iterator = _container.GetItemQueryIterator<NftMetadata>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<NftMetadata> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            rankings.Add(new Ranking()
                            {
                                i = item.nftNumber!.ToString(),
                                r = item.Rank,
                                s = item.Score,
                                im = item.image,
                                ni = item.nftId,
                                ti = item.tokenId                              
                            });
                        }
                    }
                }
                return rankings;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<List<Filter>?> GetTraitFiltersFilteredAsync(string collectionId, string traitQuery)
        {
            List<Filter> filters = new List<Filter>();
            try
            {
                var query = new QueryDefinition(query: $"SELECT t.trait_type, t['value'], count(1) as total FROM c Join t IN c.attributes where c.collectionid = @collectionId {traitQuery} group by t.trait_type, t['value']")
                .WithParameter("@collectionId", collectionId);
                using (FeedIterator<TraitFilter> iterator = _container.GetItemQueryIterator<TraitFilter>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        FeedResponse<TraitFilter> response = await iterator.ReadNextAsync();
                        foreach (var item in response)
                        {
                            if (!filters.Where(x => x.FilterName == item.trait_type).Any())
                            {
                                Filter filter = new Filter();
                                filter.FilterName = item.trait_type;
                                filter.TraitFilters = new List<TraitFilter>();
                                filter.TraitFilters.Add(item);
                                filters.Add(filter);
                            }
                            else
                            {
                                filters.Where(x => x.FilterName == item.trait_type).FirstOrDefault()!.TraitFilters.Add(item);
                            }
                        }
                    }
                }
                filters = filters.OrderBy(x => x.FilterName).ToList();
                filters.ForEach(o => o.TraitFilters = o.TraitFilters.OrderBy(d => d.value).ToList());

                return filters;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
