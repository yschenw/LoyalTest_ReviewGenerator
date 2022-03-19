using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewGenerator.Models;

namespace ReviewGenerator.Controllers
{
    [Produces("application/json")]
    [Route("api/generate")]
    public class GenerateController : Controller
    {
        private readonly IReviewData reviewDataSet;

        public GenerateController(IReviewData reviewDataSet)
        {
            this.reviewDataSet = reviewDataSet;
        }


        public ReviewEntry Index()
        {
            return this.reviewDataSet.GenerateReview();
        }

    }
}
