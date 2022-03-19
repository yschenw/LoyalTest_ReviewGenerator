using Newtonsoft.Json;

namespace ReviewGenerator.Models
{
    public class ReviewEntry
    {
        [JsonProperty("overall")]
        public decimal rating { get; set; }  = 0.0m;
        public bool verified { get; set; } = false;
        public string? reviewerName { get; set; }
        public string? reviewText { get; set; }
        public string? summary { get; set; }
        public long unixReviewTime { get; set; }
    }


}
