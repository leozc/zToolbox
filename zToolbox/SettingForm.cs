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
    /**
     * This class acts as the Setting model and the UI.
     */
    public partial class SettingForm : Form
    {


        public SettingForm()
        {

            InitializeComponent();
            this.CenterToScreen();
        }

        private void process_button_click(object sender, EventArgs e)
        {
            try
            {
                Application.UserAppDataRegistry.SetValue(
                            "zwid", tbzwid.Text.Trim());

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.zillow.com/howto/api/APIOverview.htm");
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Get the connection string from the registry.
                if (Application.UserAppDataRegistry.GetValue("zwid") != null)
                {
                    Object zwid =
                      Application.UserAppDataRegistry.GetValue(
                      "zwid");
                    tbzwid.Text = zwid.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string GetZWID()
        {
            if (Application.UserAppDataRegistry.GetValue("zwid") != null)
            {
                Object zwid =
                  Application.UserAppDataRegistry.GetValue(
                  "zwid");
                return zwid.ToString();
            }
            else
                return null;
        }
    }
}
