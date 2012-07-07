using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
namespace zLib
{
   
    public class AddressParser
    {
        public static Dictionary<string, string> states = stateAbbreviationExpand();
        public String address;
        public AddressParser(String address)
        {
            this.address = address.Replace(","," ");
        }
        public Address parseAddress()
        {
            char[] delimiters = new char[] { ' ' };
            var addressArray = address.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (addressArray.Length < 4) // i cannot accept address less then 4 words len
                return new Address();//return a dummy address

            var a = new Address();
            
            var addressLine = new List<String>();
            int reverseCounter = 0;
            foreach (string s in addressArray.Reverse())
            {
                if (reverseCounter <4 && a.Zip == null && Regex.IsMatch(s, @"\d\d\d\d\d")) // a zip code must within last 3 words in the string
                {
                    a.Zip = s;
                }
                else if (a.State == null && a.City==null && states.Keys.Contains(s, StringComparer.InvariantCultureIgnoreCase))
                {
                    a.State = s;
                }
                else if ((a.Zip != null || a.State != null) && a.City == null)
                {
                    a.City = s;
                }
                else {
                    addressLine.Add(s);
                }
                reverseCounter++;
            }
            a.AddressLine = String.Join(" ", addressLine.ToArray().Reverse());
            return a;
        }

        public static Dictionary<string, string> stateAbbreviationExpand()
        {
            Dictionary<string, string> states = new Dictionary<string, string>();

            states.Add("AL", "Alabama");
            states.Add("AK", "Alaska");
            states.Add("AZ", "Arizona");
            states.Add("AR", "Arkansas");
            states.Add("CA", "California");
            states.Add("CO", "Colorado");
            states.Add("CT", "Connecticut");
            states.Add("DE", "Delaware");
            states.Add("DC", "District of Columbia");
            states.Add("FL", "Florida");
            states.Add("GA", "Georgia");
            states.Add("HI", "Hawaii");
            states.Add("ID", "Idaho");
            states.Add("IL", "Illinois");
            states.Add("IN", "Indiana");
            states.Add("IA", "Iowa");
            states.Add("KS", "Kansas");
            states.Add("KY", "Kentucky");
            states.Add("LA", "Louisiana");
            states.Add("ME", "Maine");
            states.Add("MD", "Maryland");
            states.Add("MA", "Massachusetts");
            states.Add("MI", "Michigan");
            states.Add("MN", "Minnesota");
            states.Add("MS", "Mississippi");
            states.Add("MO", "Missouri");
            states.Add("MT", "Montana");
            states.Add("NE", "Nebraska");
            states.Add("NV", "Nevada");
            states.Add("NH", "New Hampshire");
            states.Add("NJ", "New Jersey");
            states.Add("NM", "New Mexico");
            states.Add("NY", "New York");
            states.Add("NC", "North Carolina");
            states.Add("ND", "North Dakota");
            states.Add("OH", "Ohio");
            states.Add("OK", "Oklahoma");
            states.Add("OR", "Oregon");
            states.Add("PA", "Pennsylvania");
            states.Add("RI", "Rhode Island");
            states.Add("SC", "South Carolina");
            states.Add("SD", "South Dakota");
            states.Add("TN", "Tennessee");
            states.Add("TX", "Texas");
            states.Add("UT", "Utah");
            states.Add("VT", "Vermont");
            states.Add("VA", "Virginia");
            states.Add("WA", "Washington");
            states.Add("WV", "West Virginia");
            states.Add("WI", "Wisconsin");
            states.Add("WY", "Wyoming");
            return states;
        }


    }

    public class Address
    {
        private String addressLine;
        private String state;
        private String zip;
        private String city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string AddressLine
        {
            get { return addressLine; }
            set { addressLine = value; }
        }
        

        public override String ToString()
        {
            return String.Join(" ", new string[] {addressLine, City, state, zip});
        }
        public String getCityStateZip()
        {
            return City == null ? "" : City + " " + 
                   State == null ? "" : State + " " + 
                   Zip == null ? "" : Zip + " ";
        }
        public bool IsCompleteAddress()
        {
            return City != null && State != null && Zip != null && AddressLine!=null;
        }
    }

 
}
