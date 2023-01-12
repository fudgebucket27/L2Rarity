using L2Rarity.Server.Models;
using L2Rarity.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L2Rarity.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamestopController : ControllerBase
    {
        private IGamestopClient _gamestopClient;

        public GamestopController(IGamestopClient gamestopClient)
        {
            _gamestopClient = gamestopClient;
        }

        [HttpGet]
        [Route("listing")]
        public async Task<IActionResult> GetRankingsOverallAsync(string nftId)
        {
            var result = await _gamestopClient.GetNftListing(nftId);
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
