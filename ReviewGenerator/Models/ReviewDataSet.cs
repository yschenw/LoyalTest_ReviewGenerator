using MarkovSharp.TokenisationStrategies;
using System.Text.Json;

namespace ReviewGenerator.Models
{
    public interface IReviewData {
        public Task GetMarkovModel();
        public string GenerateReviewText();

        public ReviewEntry GenerateReview();
    }

    public class ReviewDataSet : IReviewData
    {
        public StringMarkov? markovModel;
        //public const int sampleSize = 100000;
        public const int sampleSize = 10000;
        public static string fileName = "Office_Products_5.json";
        public bool modelReady = false;

        public ReviewDataSet()
        {
        }

        public async Task GetMarkovModel()
        {
            Console.WriteLine("- Get Markov model from dataset ... ");
            Console.WriteLine("... preparing dataset for Markov model ...");
            using (StreamReader sr = new StreamReader(fileName))
            {
                string? linebuffer;
                List<string> listReviews = new List<string>();
                while ((linebuffer = sr.ReadLine()) != null)
                {
                    ReviewEntry tmpReviewEntry = JsonSerializer.Deserialize<ReviewEntry>(linebuffer);
                    if (tmpReviewEntry != null && tmpReviewEntry.reviewText != null)
                        listReviews.Add(tmpReviewEntry.reviewText);
                    if (listReviews.Count > sampleSize) break;
                }

                Random rng = new Random();
                int n = listReviews.Count;
                while (n > 1)
                {
                    n--;
                    int i = rng.Next(n + 1);
                    string tmpReviewEntry = listReviews[i];
                    listReviews[i] = listReviews[n];
                    listReviews[n] = tmpReviewEntry;
                }
                Console.WriteLine("... complete preparing dataset for Markov model!");

                this.markovModel = new StringMarkov(2);
                Console.WriteLine("... training Markov model from the dataset ...");
                await Task.Run(() => this.markovModel.Learn(listReviews.Take<string>(sampleSize)));
                Console.WriteLine("... complete training Markov model from the dataset");

                Console.WriteLine("- Succeed to get Markov model from dataset!");
                this.modelReady = true;
            }
        }

        public string GenerateReviewText()
        {
            Console.WriteLine("- Generating review from trained Markov model ... ");
            string str = this.markovModel!.Walk().First();
            Console.WriteLine("- Complete Generating review from trained Markov model!");
            return str;
        }

        public ReviewEntry GenerateReview()
        {
            if (this.modelReady == false)
            {
                Console.WriteLine("- Markov model is not ready yet!!!");
                return null;
            }
            Console.WriteLine("- Generating review entry ... ");
            ReviewEntry entry = new ReviewEntry();
            Random rng = new Random();
            entry.rating = rng.Next(1, 5);
            entry.reviewText = this.GenerateReviewText();
            Console.WriteLine("- Complete generating review entry!");
            return entry;
        }

    }
}
