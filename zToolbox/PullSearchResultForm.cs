using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zLib;

namespace zToolbox
{
    public partial class PullSearchResultForm : Form
    {
        bool addressChanged = true;
        private GetDeepSearch.GetDeepSearchResult searchResult;
        private GetDeepCompSearch.GetDeepCompSearchResult compResult;
        public GetDeepSearch.GetDeepSearchResult SearchResult
        {
            get { return searchResult; }
            set { searchResult = value; }
        }

        public GetDeepCompSearch.GetDeepCompSearchResult CompResult
        {
            get { return compResult; }
        }

        private bool closed;

        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        public PullSearchResultForm(String address)
        {
            
            InitializeComponent();
            tbAddress.Text = address==null?"2607 Western Ave APT 1201 Seattle, WA 98121":address;
            tbAddress.TextChanged += new EventHandler(tbAddress_TextChanged);
            SearchResult = null;
        }
      
        public void SetNewAddress(String s)
        {
            tbAddress.Text = s;
        }
        public void tbAddress_TextChanged(object sender, EventArgs e)
        {
            addressChanged = true;
        }

        
        private void process_button_click(object sender, EventArgs e)
        {
            closed = false;
            bool valid = new AddressParser(tbAddress.Text).parseAddress().IsCompleteAddress();
            if (valid)
            {
                try
                {   if(SearchResult==null || addressChanged){
                        SearchResult = new GetDeepSearch().search(tbAddress.Text);
                        addressChanged = false;
                    }
                    this.Hide();
                           
                }                        
                catch (Exception ee)
                {
                    MessageBox.Show(String.Format("Something went wrong : '{0}'", tbAddress.Text), ee.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SearchResult = null;
                }
            }
            else
            {
                MessageBox.Show(String.Format("'{0}' is not a valid, please make sure you have city state and zip", tbAddress.Text), "Opps .. ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void getCompResult(bool searchResultChanged)
        {
            closed = false;
            String zpid= searchResult.getZpid();
            if (zpid !=null)
            {
                try
                {
                    if (SearchResult != null )
                    {
                        compResult = new GetDeepCompSearch().search(searchResult.getZpid());
                        addressChanged = false;

                    }
                    this.Hide();

                }
                catch (Exception ee)
                {
                    MessageBox.Show(String.Format("Something went wrong : '{0}'", tbAddress.Text), ee.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SearchResult = null;
                }
            }
            else
            {
                MessageBox.Show(String.Format("'{0}' is not a valid, please make sure you have city state and zip", tbAddress.Text), "Opps .. ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /**
         * show dialog and set address
         */
        public void PullPropertyInfo(String address){
            if (address.Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries).Count()>4) // at least 4 words
                this.tbAddress.Text = address;
            base.ShowDialog();
        }
        /**
         * show dialog 
        */
        public void PullPropertyInfo()
        {
            if (this.addressChanged || searchResult == null)
                base.ShowDialog();
        }
        public void PullComps()
        {
            bool searchResultChanged = false;
            if (addressChanged || searchResult == null){
                searchResultChanged = true;
                PullPropertyInfo();
            }
            //ensure search resule is valid
            if (searchResult != null)
                getCompResult(searchResultChanged);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            closed = true;
            this.Hide();
        }
    }
}
