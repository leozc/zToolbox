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
    public class GetDeepSearch
    {
        private String ep = @"http://www.zillow.com/webservice/GetDeepSearchResults.htm?zws-id={0}&";
        public GetDeepSearchResult search(String address, String zwid)
        {
            ep = String.Format(ep, zwid);
            Address addrObj = new AddressParser(address).parseAddress();
            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] =
                "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                "(compatible; MSIE 6.0; Windows NT 5.1; " +
                ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                String url = String.Format("{0}address={1}&citystatezip={2}&rentzestimate=true", ep, HttpUtility.UrlEncode(addrObj.AddressLine), HttpUtility.UrlEncode(
                              addrObj.getCityStateZip()));
                String s = client.DownloadString(url);
                return new GetDeepSearchResult(s);

            }

        }

        public class GetDeepSearchResult
        {
            string m(String s)
            {
                return String.Format("${0:c}", s);
            }
            private XmlDocument doc;
            public GetDeepSearchResult(String s)
            {
                doc = new XmlDocument();
                doc.LoadXml(s);
            }

            public bool isValid()
            {
                return doc.SelectSingleNode("//message/code").InnerText == "0";

            }
            public String getMessage()
            {
                return doc.SelectSingleNode("//message/text").InnerText;

            }
            public String getHDP()
            {
                return doc.SelectSingleNode("//response/results/result/links/homedetails").InnerText;

            }
            public String getComparables()
            {
                return doc.SelectSingleNode("//response/results/result/links/comparables").InnerText;

            }
            public String getZpid()
            {
                return doc.SelectSingleNode("//response/results/result/zpid").InnerText;
            }
            public String getBedrooms()
            {
                return doc.SelectSingleNode("//response/results/result/bedrooms").InnerText;
            }
            public String getBathrooms()
            {
                return doc.SelectSingleNode("//response/results/result/bathrooms").InnerText;
            }
            public String getLastSoldDate()
            {
                return doc.SelectSingleNode("//response/results/result/lastSoldDate").InnerText;
            }
            public String getLastSoldPrice()
            {
                return m(doc.SelectSingleNode("//response/results/result/lastSoldPrice").InnerText);
            }
            public String getYearBuilt()
            {
                return doc.SelectSingleNode("//response/results/result/yearBuilt").InnerText;
            }
            public String getTaxAssessmentYear()
            {
                return doc.SelectSingleNode("//response/results/result/taxAssessmentYear").InnerText;
            }
            public String getUseCode()
            {
                return doc.SelectSingleNode("//response/results/result/useCode").InnerText;
            }
            public String getTaxAssessment()
            {
                return m(doc.SelectSingleNode("//response/results/result/taxAssessment").InnerText);
            }
            public String getLotSizeSqFt()
            {
                return doc.SelectSingleNode("//response/results/result/lotSizeSqFt").InnerText;
            }
            public String getFinishedSqFt()
            {
                return doc.SelectSingleNode("//response/results/result/finishedSqFt").InnerText;
            }
            public String getZestimate()
            {
                return m(doc.SelectSingleNode("//response/results/result/zestimate/amount").InnerText);
            }
            public String getZestimateRange()
            {
                return m(doc.SelectSingleNode("//response/results/result/zestimate/valuationRange/low").InnerText) + " - " + m(doc.SelectSingleNode("//response/results/result/zestimate/valuationRange/high").InnerText);
            }
            public String getRentZestimate()
            {
                return doc.SelectSingleNode("//response/results/result/rentzestimate/amount").InnerText;
            }
            public String getRentZestimateRange()
            {
                return m(doc.SelectSingleNode("//response/results/result/rentzestimate/valuationRange/low").InnerText) + " - " + m(doc.SelectSingleNode("//response/results/result/rentzestimate/valuationRange/high").InnerText);
            }

            public String getAddress()
            {
                var street = doc.SelectSingleNode("//response/results/result/address/street").InnerText;
                var zipcode = doc.SelectSingleNode("//response/results/result/address/zipcode").InnerText;
                var city = doc.SelectSingleNode("//response/results/result/address/city").InnerText;
                var state = doc.SelectSingleNode("//response/results/result/address/state").InnerText;

                return street + " " + city + " " + state + " " + zipcode;

            }
            public String getLat()
            {
                return doc.SelectSingleNode("//response/results/result/address/latitude").InnerText;
            }

            public String getLong()
            {
                return doc.SelectSingleNode("//response/results/result/address/longitude").InnerText;
            }


            public String GetGoogleMapStr()
            {
                var url = "http://maps.googleapis.com/maps/api/staticmap?center={0}&zoom=13&size=600x300&maptype=roadmap&markers=color:blue%7Clabel:S%7C{1},{2}&sensor=false";
                var addreeEncoded = HttpUtility.HtmlEncode(getAddress());
                var lat = getLat();
                var longtitude = getLong();
                return String.Format(url, addreeEncoded, lat, longtitude);


            }
        }

    }
}
