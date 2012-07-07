using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using zLib;

namespace TestProject1
{
    [TestClass]
    public class TestGetDeepSearch
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            // to do, I need to fix this unit test which pass in the XML instead of a service(service result can change)
           var ret = new GetDeepSearch().search(@"18338 28 st remond 98052 wa","X1-ZWz1brb7wpucqz_2doog");
           String p = ret.getHDP();
           Assert.AreEqual(p, @"http://www.zillow.com/homedetails/18338-NE-28th-St-Redmond-WA-98052/54829671_zpid/");

           Assert.AreEqual(ret.getZpid(), @"54829671");
           Assert.AreEqual(ret.getUseCode(), @"SingleFamily");

           Assert.AreEqual(ret.getTaxAssessmentYear(), @"2011");
           Assert.AreEqual(ret.getTaxAssessment(), @"$851000.0");
           Assert.AreEqual(ret.getYearBuilt(), @"2001");
           Assert.AreEqual(ret.getLotSizeSqFt(), @"10541");
           Assert.AreEqual(ret.getFinishedSqFt(), @"4880");
           Assert.AreEqual(ret.getBathrooms(), @"3.0");
           Assert.AreEqual(ret.getBedrooms(), @"4");
           Assert.AreEqual(ret.getLastSoldDate(), @"09/10/2009");
           Assert.AreEqual(ret.getLastSoldPrice(), @"$600000");

           Assert.AreEqual(ret.getZestimate(), @"$918400");
           Assert.AreEqual(ret.getZestimateRange(), @"762272 - 1010240");
           Assert.AreEqual(ret.getRentZestimate(), @"$4025");
           Assert.AreEqual(ret.getRentZestimateRange(), @"1811 - 4991");

           Assert.AreEqual(ret.GetGoogleMapStr(), "");
        }
    }
}
