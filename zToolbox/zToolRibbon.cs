using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Word;
using zLib;
using System.Net;
using System.IO;

namespace zToolbox
{
    // highlight and insert the address as zillow link
    // highlight get zestimate
    //
    public partial class zToolRibbon
    {
        PullSearchResultForm stateForm;

        public PullSearchResultForm StateForm
        {
            get
            {
                if (stateForm == null)
                    stateForm = new PullSearchResultForm(null);
                return stateForm;
            }
            set { stateForm = value; }
        }


        private void zToolRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void zLinkB_Click(object sender, RibbonControlEventArgs e)
        {

            var inspector = Globals.ThisAddIn.Application.ActiveInspector();
            var m = ((MailItem)inspector.CurrentItem);
            if (m != null)
            {
                if (inspector.EditorType != OlEditorType.olEditorWord)
                {
                    MessageBox.Show("I only support Outlook 2010 with Word Editor " + inspector.EditorType);
                    return;
                }
                var d = (Document)inspector.WordEditor;

                String address = d.Application.Selection.Text; // or something else
                var form = StateForm;
                form.ShowDialog(address);
                if (!StateForm.Closed  && form.SearchResult != null)
                {
                    var r = form.SearchResult;
                    if (r.isValid())
                    {
                        // make sure response valid
                        Range range = d.Application.Selection.Range;
                        d.Application.Selection.Hyperlinks.Add(range, r.getHDP(), Type.Missing,
                                                               Type.Missing,
                                                               d.Application.Selection.Text.Trim().Length < 1 ? r.getHDP() : d.Application.Selection.Text);
                    }
                    else
                    {
                        MessageBox.Show(String.Format("'{0}' is not a valid for zillow reason: {1}", address,
                                                      r.getMessage()));
                    }
                }
            }
        }

        private void openinzillow_Click(object sender, RibbonControlEventArgs e)
        {
            var inspector = Globals.ThisAddIn.Application.ActiveInspector();
            var m = ((MailItem)inspector.CurrentItem);
            if (m != null)
            {
                if (inspector.EditorType != OlEditorType.olEditorWord)
                {
                    MessageBox.Show("I only support Outlook 2010 with Word Editor " + inspector.EditorType);
                    return;
                }

                this.StateForm.ShowDialog();
                if (!StateForm.Closed && StateForm.SearchResult != null)
                {
                    var r = StateForm.SearchResult;

                    //create a table
                    var d = (Document)inspector.WordEditor;
                    Range range = d.Application.Selection.Range;
                    var t = d.Application.Selection.Tables.Add(range, 8, 2);
                    t.ApplyStyleFirstColumn = true;

                    int nrRow = 1;
                    int nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "Property Address";
                    t.Cell(nrRow, nrColumn++).Range.Text = r.getAddress();
                    nrRow++;


                    nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "Dwelling Type";
                    t.Cell(nrRow, nrColumn++).Range.Text = r.getUseCode();
                    nrRow++;

                    nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "Year Built";
                    t.Cell(nrRow, nrColumn++).Range.Text = r.getYearBuilt();
                    nrRow++;

                    nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "Lot Size";
                    t.Cell(nrRow, nrColumn++).Range.Text = r.getLotSizeSqFt();
                    nrRow++;

                    nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "Finished SqFt";
                    t.Cell(nrRow, nrColumn++).Range.Text = r.getFinishedSqFt();
                    nrRow++;

                    nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "Bedrooms";
                    t.Cell(nrRow, nrColumn++).Range.Text = r.getBedrooms();
                    nrRow++;

                    nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "BathRoom";
                    t.Cell(nrRow, nrColumn++).Range.Text = r.getBathrooms();
                    nrRow++;

                    nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "Tax";
                    t.Cell(nrRow, nrColumn++).Range.Text = r.getTaxAssessment() + " in " + r.getTaxAssessmentYear();
                    nrRow++;



                    t.AutoFormat();
                }

                //t
            }


        }

        private void zEstimate_Click(object sender, RibbonControlEventArgs e)
        {
               var inspector = Globals.ThisAddIn.Application.ActiveInspector();
            var m = ((MailItem)inspector.CurrentItem);
            if (m != null)
            {
                if (inspector.EditorType != OlEditorType.olEditorWord)
                {
                    MessageBox.Show("I only support Outlook 2010 with Word Editor " + inspector.EditorType);
                    return;
                }
                this.StateForm.ShowDialog();
                if (!StateForm.Closed && StateForm.SearchResult != null)
                {
                    var r = StateForm.SearchResult;
                    var d = (Document)inspector.WordEditor;
                    Range range = d.Application.Selection.Range;
                    d.Application.Selection.Text = r.getZestimate() + " within the range of " + r.getZestimateRange();

                }
            }
        }

        private void gMap_Click(object sender, RibbonControlEventArgs e)
        {
            var inspector = Globals.ThisAddIn.Application.ActiveInspector();
            var m = ((MailItem)inspector.CurrentItem);
            if (m != null)
            {
                if (inspector.EditorType != OlEditorType.olEditorWord)
                {
                    MessageBox.Show("I only support Outlook 2010 with Word Editor " + inspector.EditorType);
                    return;
                }

                this.StateForm.ShowDialog();
                if (!StateForm.Closed && StateForm.SearchResult != null)
                {
                    var r = StateForm.SearchResult;

                    //create a table
                    var d = (Document)inspector.WordEditor;
                    Range range = d.Application.Selection.Range;
                    var url = new System.Uri(r.GetGoogleMapStr());
                    using (WebClient client = new WebClient())
                    {
                        client.Headers["User-Agent"] =
                            "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                            "(compatible; MSIE 6.0; Windows NT 5.1; " +
                            ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                        string randomFileName =Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".jpg");
                        client.DownloadFileCompleted += delegate(object s, System.ComponentModel.AsyncCompletedEventArgs ee)
                        {
                            if (ee.Error == null)
                                d.Application.Selection.InsertFile(randomFileName);
                           
                        };
                        client.DownloadFileAsync(url, randomFileName, "d");
                    }
                }
            }

        }



    }
}
                    
     