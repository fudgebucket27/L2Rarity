using L2Rarity.Server.Models;
using L2Rarity.Shared;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.WebRequestMethods;

namespace L2Rarity.Server.Services
{

    public class GamestopService : IGamestopClient, IDisposable
    {
        const string _baseUrl = "https://api.nft.gamestop.com";
        readonly RestClient _client;

        public GamestopService()
        {
            _client = new RestClient(_baseUrl);
            _client.AddDefaultHeader("User-Agent", "Mozilla/5.0 Gecko/20100101 Firefox/106.0");
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Dictionary<string, Value>> GetNftListing(string nftId)
        {
            var request = new RestRequest("nft-svc-marketplace/getNftDetailRecord");
            request.AddParameter("nftId", nftId);
            try
            {
                var response = await _client.GetAsync(request);
                var data = JsonConvert.DeserializeObject<Dictionary<string, Value>>(response.Content!);
                return data;
            }
            catch (HttpRequestException httpException)
            {
                Console.WriteLine($"Error getting nft data from gamestop: {httpException.Message}");
                return null!;
            }
            catch (Newtonsoft.Json.JsonReaderException jSex)
            {
                Console.WriteLine($"Error deserialising json: {jSex.Message}");
                return null!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting nft data from gamestop: {ex.Message}");
                return null!;
            }
        }
    }
}
