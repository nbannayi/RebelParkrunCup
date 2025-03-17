using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;

namespace RebelParkrunCup.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkrunProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ParkrunProxyController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetRunnersResults([FromQuery] string[] runnerId)
        {
            if (runnerId == null || runnerId.Length == 0)
            {
                return BadRequest("No runner IDs provided.");
            }

            var tasks = runnerId.Select(GetRunnerResults);
            var results = await Task.WhenAll(tasks);

            return new JsonResult(results.Where(r => r != null)); // Filter out any null results from failed requests
        }

        private async Task<object?> GetRunnerResults(string runnerId)
        {
            runnerId = runnerId.Replace("A",""); // Remove the leading A, need the raw numeric code.
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.parkrun.org.uk/parkrunner/{runnerId}/all/");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);

                var rows = htmlDocument.DocumentNode.SelectNodes("//table[@id='results']/tbody/tr");
                if (rows == null) return null;

                var results = new List<object>();

                foreach (var row in rows)
                {
                    var tds = row.SelectNodes("td");
                    if (tds != null && tds.Count >= 7)
                    {
                        results.Add(new
                        {
                            RunnerId = $"A{runnerId}",
                            Event = tds[0].InnerText.Trim(),
                            RunDate = tds[1].InnerText.Trim(),
                            Time = tds[4].InnerText.Trim(),
                        });
                    }
                }

                return new { RunnerId = runnerId, Results = results };
            }
            catch (HttpRequestException)
            {
                return null; // Handle failures gracefully
            }
        }
    }
}