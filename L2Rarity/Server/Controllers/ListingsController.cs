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
    }
}
