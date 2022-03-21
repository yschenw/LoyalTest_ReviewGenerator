using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ReviewGenerator.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ReviewGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReviewData reviewDataSet;

        public HomeController(IReviewData reviewDataSet, ILogger<HomeController> logger)
        {
            _logger = logger;
            this.reviewDataSet = reviewDataSet;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public async Task<IActionResult> Index(string id)
        {
            Task<ReviewEntry> reviewEntryTask = null;
            await Task.Run(() => reviewEntryTask = this.GetReview());
            ReviewEntry reviewEntry = reviewEntryTask.Result;
            return View(reviewEntry);
        }

        private async Task<ReviewEntry> GetReview()
        {
            ReviewEntry reviewEntry = null;
            var serviceProvider = this.HttpContext.RequestServices;
            var gcontroller = (GenerateController)serviceProvider.GetRequiredService<GenerateController>();
            reviewEntry = gcontroller.Index();
            return reviewEntry;

            // another way:
            string apiUrl = this.Url.ActionLink("Index", "Generate");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    reviewEntry = JsonSerializer.Deserialize<ReviewEntry>(data);
                }
            }
            return reviewEntry;
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