using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace zLib
{
    /**
     * A service caller for deep comp interface
     */
    public class GetDeepCompSearch
    {
        private String ep = @"http://www.zillow.com/webservice/GetDeepComps.htm?zws-id={0}&count=10&";
        public GetDeepCompSearchResult search(String zpid,String zwid)
        {

            ep = String.Format(ep, zwid);
            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] =
                "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                "(compatible; MSIE 6.0; Windows NT 5.1; " +
                ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                String url = String.Format("{0}zpid={1}", ep, zpid);
                String s = client.DownloadString(url);
                return new GetDeepCompSearchResult(s);
            }

        }

        public class GetDeepCompSearchResult
        {
            string m(String s )
            {
                return String.Format("${0:c}", s);
            }
            private XmlDocument doc;
            public GetDeepCompSearchResult(String s)
            {
                doc = new XmlDocument();
                doc.LoadXml(s);
            }

            public bool IsValid()
            {
                return unknownOrValue(doc.SelectSingleNode("//message/code"))=="0";

            }
            public String  GetMessage()
            {
                return unknownOrValue(doc.SelectSingleNode("//message/text"));

            }

            private IList<Comp> comp_results;
            public IList<Comp> Comp_results
            {
                get
                {
                    if (comp_results == null)
                        comp_results = GetComparables();
                    return comp_results;
                }
            } 
            public IList<Comp> GetComparables()
            {
                var nodes = doc.SelectNodes("//response/properties/comparables/comp");
                var results = new List<Comp>();
                var priciple = doc.SelectSingleNode("//response/properties/principal");
                results.Add(createCompEntry(priciple, "SUBJECT"));
                if (nodes != null)
                {
                    int i = 1;
                    foreach (XmlNode n in nodes)
                    {

                        var c = createCompEntry(n, "comp_"+i++);
                        results.Add(c);
                    }
                }

                
                return results;
                
            }
            private String unknownOrValue(XmlNode n)
            {
                return n == null ? "Unknown" : n.InnerText;
            }
            private  Comp createCompEntry(XmlNode n, String id)
            {
                var hdp = unknownOrValue(n.SelectSingleNode("links/homedetails"));
                var zestimate = m(unknownOrValue(n.SelectSingleNode("zestimate/amount")));
                
                var lastSoldDate = unknownOrValue(n.SelectSingleNode("lastSoldDate"));

                var lastSoldPrice = m( unknownOrValue(n.SelectSingleNode("lastSoldPrice")));
                var bedrooms = unknownOrValue(n.SelectSingleNode("bedrooms"));
                var bathrooms = unknownOrValue(n.SelectSingleNode("bathrooms"));
                var finishedSqFt = unknownOrValue(n.SelectSingleNode("finishedSqFt"));
                var lotSizeSqFt = unknownOrValue(n.SelectSingleNode("lotSizeSqFt"));
                var yearBuilt = unknownOrValue(n.SelectSingleNode("yearBuilt"));
                var taxAssessment = m(unknownOrValue(n.SelectSingleNode("taxAssessment")));
                var taxAssessmentYear = unknownOrValue(n.SelectSingleNode("taxAssessmentYear"));
                var lat = unknownOrValue(n.SelectSingleNode("address/latitude"));
                var logg = unknownOrValue(n.SelectSingleNode("address/longitude"));
                var address = unknownOrValue(n.SelectSingleNode("address/street")) +", "+ unknownOrValue(n.SelectSingleNode("address/city"));

                Comp c = new Comp(address,hdp, zestimate, lastSoldPrice, lastSoldDate, bedrooms, bathrooms, finishedSqFt, lotSizeSqFt,
                                  yearBuilt, taxAssessmentYear, taxAssessment, lat, logg);
                c.Id = id;
                return c;
            }

            public class Comp
            {
                private String taxAssessmentYear;
                private String taxAccessment;
                private String yearBuilt;
                private String lotsizeSqft;
                private String finishedSqft;
                private String bathrooms;
                private String bedrooms;
                private String lastsoldDate;
                private String lastsoldPricel;
                private string zestimate;
                private string hdp;
                private string lat;
                private string longg;
                private string id;
                private String address; 
                public Comp(string address, string hdp, string zestimate, string lastsoldPricel, string lastsoldDate, string bedrooms, string bathrooms, string finishedSqft, string lotsizeSqft, string yearBuilt, string taxAssessmentYear, string taxAccessment,string lat,string longg)
                {
                    this.address = address;
                    this.hdp = hdp;
                    this.zestimate = zestimate;
                    this.lastsoldPricel = lastsoldPricel;
                    this.lastsoldDate = lastsoldDate;
                    this.bedrooms = bedrooms;
                    this.bathrooms = bathrooms;
                    this.finishedSqft = finishedSqft;
                    this.lotsizeSqft = lotsizeSqft;
                    this.yearBuilt = yearBuilt;
                    this.taxAssessmentYear = taxAssessmentYear;
                    this.taxAccessment = taxAccessment;
                    this.lat = lat;
                    this.longg = longg;
                }


                public String Address
                {
                    get { return address; }
                    
                }
                public string Id
                {
                    get { return id; }
                    set { id = value; }
                }

                public string Lat
                {
                    get { return lat; }
                }

                public string Longg
                {
                    get { return longg; }
                }

                public string YearBuilt
                {
                    get { return yearBuilt; }
                }

                public string LotsizeSqft
                {
                    get { return lotsizeSqft; }
                }

                public string TaxAssessmentYear
                {
                    get { return taxAssessmentYear; }
                }

                public string TaxAccessment
                {
                    get { return taxAccessment; }
                }

                public string FinishedSqft
                {
                    get { return finishedSqft; }
                }

                public string Bathrooms
                {
                    get { return bathrooms; }
                }

                public string Bedrooms
                {
                    get { return bedrooms; }
                }

                public string LastsoldDate
                {
                    get { return lastsoldDate; }
                }

                public string LastsoldPricel
                {
                    get { return lastsoldPricel; }
                }

                public string Zestimate
                {
                    get { return zestimate; }
                }

                public string Hdp
                {
                    get { return hdp; }
                }
            }

            private enum Color : uint { green=0, black=1, brown=2, purple=3, yellow=4, blue=5, gray=6, orange=7, red=8, white=9, pink=10};

            public String GetGoogleMapStr()
            {
                String mark = "markers=color:{0}%7Clabel:{1}%7C{2},{3}";
                var markList = new List<String>();
                int coloridx = 0;
                foreach (var c in this.Comp_results)
                {
                    var m = string.Format(mark, ((Color) coloridx).ToString(), c.Id.Last(), c.Lat, c.Longg);
                    markList.Add(m);
                    coloridx++;
                }
                
                var urlTemplate = "http://maps.googleapis.com/maps/api/staticmap?center={0}&zoom=13&size=600x300&maptype=roadmap&{0}&sensor=false";
                var url = String.Format(urlTemplate, String.Join("&", markList.ToArray()));
                return url;

            }
        }

    }
}
