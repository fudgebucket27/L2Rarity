using L2Rarity.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace L2Rarity.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private ISqlService _sqlClient;

        public ListingsController(ISqlService sqlClient)
        {
            _sqlClient = sqlClient;
        }

        [HttpGet]
        [Route("listing")]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetListingsOverallAsync(string collectionId)
        {
            var result = await _sqlClient.GetListings(collectionId);
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
        [Route("collections")]
        public async Task<IActionResult> GetCollectionsAsync()
        {
            var result = await _sqlClient.GetCollections();
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
        [Route("randomCollection")]
        public async Task<IActionResult> GetRandomCollectionAsync()
        {
            var result = await _sqlClient.GetRandomCollection();
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
