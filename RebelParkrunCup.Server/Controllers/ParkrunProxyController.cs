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
        public async Task<IActionResult> Get([FromQuery] string runnerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.parkrun.org.uk/parkrunner/{runnerId}/all/");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                
                // Load the HTML content into HtmlAgilityPack's HtmlDocument
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);

                // Select all the rows within the tbody of the results table
                var rows = htmlDocument.DocumentNode.SelectNodes("//table[@id='results']/tbody/tr");

                var results = new List<object>();

                // Loop through each row and extract the td values
                foreach (var row in rows)
                {
                    var tds = row.SelectNodes("td");
                    if (tds != null && tds.Count >= 7) // Ensure there are enough td elements
                    {
                        var eventName = tds[0].InnerText.Trim();
                        var runDate = tds[1].InnerText.Trim();
                        var time = tds[4].InnerText.Trim();

                        results.Add(new
                        {
                            Event = eventName,
                            RunDate = runDate,
                            Time = time,
                        });
                    }
                }

                // Return the results as JSON or process them as needed
                return new JsonResult(results); // Optionally, you can return this as a view or process the results differently
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }
    }
}