using FutbolowaJaskinia.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Data
{
    public class ApiAccess
    {
        private readonly string highlightsUrl = @"https://www.scorebat.com/video-api/v3/";
        private readonly string tablesUrl = @"https://api.football-data.org/v2/competitions/";
        private readonly ILogger<ApiAccess> _logger;
        private readonly IConfiguration _config;

        public ApiAccess(ILogger<ApiAccess> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<List<HighlightsModel>> GetHighlightsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage mess = await client.GetAsync(highlightsUrl))
                {
                    using (HttpContent content = mess.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        _logger.LogInformation(data);
                        var obj = JObject.Parse(data)["response"];
                        var output = JsonConvert.DeserializeObject<List<HighlightsModel>>(obj.ToString());

                        return output;
                    }
                }
                return null;
            }

        }

        public async Task<StandingsModel> GetStandingsAsync(string league)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Auth-Token", _config.GetSection("ApiKey").Value);
                    using (HttpResponseMessage resp = await client.GetAsync($"{tablesUrl}{league}/standings"))
                    {
                        using (HttpContent content = resp.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            var obj = JObject.Parse(data);

                            var mapped = JsonConvert.DeserializeObject<StandingsModel>(obj.ToString());

                            return (mapped);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Occured: {date}", DateTime.Now);
            }
            return null;
        }
    }
}
