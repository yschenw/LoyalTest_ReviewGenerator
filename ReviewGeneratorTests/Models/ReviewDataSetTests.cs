using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewGenerator.Models.Tests
{
    [TestClass()]
    public class ReviewDataSetTests
    {
        ReviewDataSet reviewDataSet = null;
        public ReviewDataSetTests()
        {
            this.reviewDataSet = new ReviewDataSet();
            this.reviewDataSet.GetMarkovModel();
        }

        [TestMethod()]
        public void GenerateReviewTextTest()
        {
            string reviewStr = this.reviewDataSet.GenerateReviewText();
            //Assert.IsNotNull(reviewStr);
            if (reviewStr == null || !reviewStr.Equals(""))
                Assert.Fail();
        }

        [TestMethod()]
        public void GenerateReviewTest()
        {
            ReviewEntry entry = this.reviewDataSet.GenerateReview();
            Assert.IsNotNull(entry);
            //Assert.Fail();
        }
    }
}