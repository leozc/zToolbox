using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using zLib;

namespace TestProject1
{
    [TestClass]
    public class TestGetCompDeepSearch
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            var ret = new GetDeepCompSearch().search(@"54829671");
            var comps = ret.GetComparables();
            Assert.AreEqual(11, comps.Count);
            foreach (var c in comps)
            {
                Debug.WriteLine("hdp " + c.Hdp);
                Debug.WriteLine("taxAssessmentYear " + c.TaxAssessmentYear);
                Debug.WriteLine("taxAssessment " + c.TaxAssessmentYear);
                Debug.WriteLine("yearBuilt " + c.YearBuilt);
                Debug.WriteLine("lotSizeSqFt " + c.LotsizeSqft);
                Debug.WriteLine("finishedSqFt " + c.FinishedSqft);
                Debug.WriteLine("bathrooms " + c.Bathrooms);
                Debug.WriteLine("bedrooms " + c.Bedrooms);
                Debug.WriteLine("lastSoldDate " + c.LastsoldDate);
                Debug.WriteLine("lastSoldPrice" + c.LastsoldDate);
                Debug.WriteLine("zestimate" + c.Zestimate);
                Debug.WriteLine("lat" + c.Lat);
                Debug.WriteLine("longg" + c.Longg);

            }

        }
    }
}
