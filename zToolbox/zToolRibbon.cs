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
                form.PullPropertyInfo(address);
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

                this.StateForm.PullPropertyInfo();
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
                this.StateForm.PullPropertyInfo();
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

                this.StateForm.PullPropertyInfo();
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
                        client.DownloadFileCompleted +=
                            delegate(object s, System.ComponentModel.AsyncCompletedEventArgs ee)
                                {
                                    if (ee.Error == null)
                                    {   m.Display(false);
                                        //d.Application.Selection.InsertFile(randomFileName);
                                        var image = d.Shapes.AddPicture(randomFileName, false,  true, url.ToString(),d.Application.Selection.Range);
                                        var f = image.ConvertToInlineShape();
                                        d.Application.Selection.Hyperlinks.Add(f,url.ToString());
                                    }
                                };
                        
                        client.DownloadFileAsync(url, randomFileName, "d");
                    }
                }
            }

        }

        private void getCompTable_Click(object sender, RibbonControlEventArgs e)
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

                this.StateForm.PullComps();
                if (!StateForm.Closed && StateForm.CompResult != null)
                {
                    var r = StateForm.CompResult;
                     
                    //create a table
                    var d = (Document)inspector.WordEditor;
                     
                    Range range = d.Application.Selection.Range;

                    var t = d.Application.Selection.Tables.Add(range, 11, 11);
                    t.ApplyStyleHeadingRows = true;

                    int nrRow = 1;
                    int nrColumn = 1;
                    t.Cell(nrRow, nrColumn++).Range.Text = "Property Id";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Address";
                    t.Cell(nrRow, nrColumn++).Range.Text = "YearBuilt;";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Lot Size";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Livable Size";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Bedrooms";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Bathrooms";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Last Sold Date";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Last Sold Price";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Zestimate";
                    t.Cell(nrRow, nrColumn++).Range.Text = "Zillow HDP";
                    nrRow++;


                    foreach (var c in r.GetComparables())
                    {
                        nrColumn = 1;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.Id;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.Address;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.YearBuilt;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.LotsizeSqft;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.FinishedSqft;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.Bedrooms;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.Bathrooms;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.LastsoldDate;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.LastsoldPricel;
                        t.Cell(nrRow, nrColumn++).Range.Text = c.Zestimate;
                        t.Cell(nrRow, nrColumn).Range.Hyperlinks.Add(t.Cell(nrRow, nrColumn).Range, c.Hdp,
                                                                     TextToDisplay: "Chech Out Zillow");
                        nrRow++;
                    }
                   


                    t.AutoFormat();

                }
            }
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
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

                this.StateForm.PullComps();
                if (!StateForm.Closed && StateForm.CompResult != null)
                {
                    var r = StateForm.CompResult;

                    //create a table
                    var d = (Document)inspector.WordEditor;
                    var url = new System.Uri(r.GetGoogleMapStr());
                    using (WebClient client = new WebClient())
                    {
                        client.Headers["User-Agent"] =
                            "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                            "(compatible; MSIE 6.0; Windows NT 5.1; " +
                            ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                        string randomFileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".jpg");
                        client.DownloadFileCompleted +=
                            delegate(object s, System.ComponentModel.AsyncCompletedEventArgs ee)
                            {
                                if (ee.Error == null)
                                {
                                    m.Display(false);
                                    //d.Application.Selection.InsertFile(randomFileName);
                                    var image = d.Shapes.AddPicture(randomFileName, false, true, url.ToString(), d.Application.Selection.Range);
                                    var f = image.ConvertToInlineShape();
                                    d.Application.Selection.Hyperlinks.Add(f, url.ToString());
                                }
                            };

                        client.DownloadFileAsync(url, randomFileName, "d");
                    }
                }
            }
        }



    }
}
                    
     