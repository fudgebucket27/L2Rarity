using L2Rarity.Server.Models;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace L2Rarity.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public class RarityController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        private IAppCache cache = new CachingService();

        public RarityController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }

        [HttpGet]
        [Route("rankings")]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetRankingsOverallAsync(string collectionId)
        {
            var result = await cache.GetOrAddAsync($"{collectionId}-listings", async () => await _cosmosDbService.GetCollectionOverallRankingsAsync(collectionId), DateTimeOffset.UtcNow.AddHours(1));
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }


        [HttpGet]
        [Route("stats")]
        public async Task<IActionResult> GetCollectionStatsAsync(string collectionId)
        {
            var result = await _cosmosDbService.GetCollectionStatsAsync(collectionId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }


        [HttpGet]
        [Route("rankingsSingle")]
        public async Task<IActionResult> GetRankingsSingleAsync(string collectionId, string nftNumber)
        {
            var result = await _cosmosDbService.GetCollectionSingleRankingsAsync(collectionId, nftNumber);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("listings")]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetListingsAsync()
        {
            var result = await _cosmosDbService.GetListingsAsync();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("traitFilters")]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetTraitFiltersAsync(string collectionId)
        {
            var result = await _cosmosDbService.GetTraitFiltersAsync(collectionId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("traitFiltersFiltered")]
        public async Task<IActionResult> GetTraitFiltersFilteredAsync(string collectionId, string traitQuery)
        {
            var result = await _cosmosDbService.GetTraitFiltersFilteredAsync(collectionId,traitQuery);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("rankingsFilteredByTraits")]
        public async Task<IActionResult> GetRankingsFilteredByTraitsAsync(string collectionId, string traitQuery)
        {
            var result = await _cosmosDbService.GetItemsFilteredAsync(collectionId, traitQuery);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
