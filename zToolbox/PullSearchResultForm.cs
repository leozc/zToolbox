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

        public GetDeepSearch.GetDeepSearchResult SearchResult
        {
            get { return searchResult; }
            set { searchResult = value; }
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
      
        void tbAddress_TextChanged(object sender, EventArgs e)
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
                    this.Close();
                
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
        public void ShowDialog(String address){
            this.tbAddress.Text = address;
            base.ShowDialog();
        }
        /**
         * show dialog 
         */
        public void ShowDialog(){
            base.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            closed = true;
            this.Close();
        }
    }
}
