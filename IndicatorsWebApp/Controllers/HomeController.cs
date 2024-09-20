using IndicatorsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;


namespace IndicatorsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string content = string.Empty;
            List<News> NewsData = new List<News>();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, "https://api.tradingeconomics.com/news/country/mexico,sweden/inflation%20rate?c=18fb9ce0200b4d9:qcip50r8120lx2z&d1=2021-02-02&d2=2022-03-03"))
                    {
                        request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                        var response = await httpClient.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            content = await response.Content.ReadAsStringAsync();

                            // Deserialize the JSON into a list of News Objects
                            NewsData = JsonConvert.DeserializeObject<List<News>>(content) ?? new List<News>(); // provide an empty List<News> if JsonConvert.DeserializeObject<List<News>>(content) returns null

                            NewsData = NewsData.Where(n => n.Id != 0 && n.Id.HasValue).ToList();
                        }
                        else
                        {
                            // Handle unsuccessful response
                            Console.WriteLine($"Error: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }

            // Pass the content to the view if needed
            //ViewBag.ApiResponse = content;
            return View(NewsData);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
