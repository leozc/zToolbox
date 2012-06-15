namespace zToolbox
{
    partial class zToolRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public zToolRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.zLinkB = this.Factory.CreateRibbonButton();
            this.zEstimate = this.Factory.CreateRibbonButton();
            this.factsheet = this.Factory.CreateRibbonButton();
            this.getCompTable = this.Factory.CreateRibbonButton();
            this.gMap = this.Factory.CreateRibbonButton();
            this.button1 = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "zToolbox";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.zLinkB);
            this.group1.Items.Add(this.zEstimate);
            this.group1.Items.Add(this.factsheet);
            this.group1.Items.Add(this.getCompTable);
            this.group1.Items.Add(this.gMap);
            this.group1.Items.Add(this.button1);
            this.group1.Label = "common";
            this.group1.Name = "group1";
            // 
            // zLinkB
            // 
            this.zLinkB.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.zLinkB.Image = global::zToolbox.Properties.Resources._4feca402f9cb8bcf5bf2015cb2b7be9f_1_;
            this.zLinkB.Label = "zLink";
            this.zLinkB.Name = "zLinkB";
            this.zLinkB.ShowImage = true;
            this.zLinkB.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.zLinkB_Click);
            // 
            // zEstimate
            // 
            this.zEstimate.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.zEstimate.Image = global::zToolbox.Properties.Resources._4feca402f9cb8bcf5bf2015cb2b7be9f_1_;
            this.zEstimate.Label = "zEstimate";
            this.zEstimate.Name = "zEstimate";
            this.zEstimate.ShowImage = true;
            this.zEstimate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.zEstimate_Click);
            // 
            // factsheet
            // 
            this.factsheet.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.factsheet.Image = global::zToolbox.Properties.Resources._4feca402f9cb8bcf5bf2015cb2b7be9f_1_;
            this.factsheet.Label = "zFacts";
            this.factsheet.Name = "factsheet";
            this.factsheet.ShowImage = true;
            this.factsheet.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.openinzillow_Click);
            // 
            // getCompTable
            // 
            this.getCompTable.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.getCompTable.Image = global::zToolbox.Properties.Resources._4feca402f9cb8bcf5bf2015cb2b7be9f_1_;
            this.getCompTable.Label = "Comps";
            this.getCompTable.Name = "getCompTable";
            this.getCompTable.ShowImage = true;
            this.getCompTable.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.getCompTable_Click);
            // 
            // gMap
            // 
            this.gMap.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.gMap.Image = global::zToolbox.Properties.Resources.GOOG;
            this.gMap.Label = "Property Map";
            this.gMap.Name = "gMap";
            this.gMap.ShowImage = true;
            this.gMap.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.gMap_Click);
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Image = global::zToolbox.Properties.Resources.GOOG;
            this.button1.Label = "Comp Map";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // zToolRibbon
            // 
            this.Name = "zToolRibbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Compose, Microsoft.Outlook.Post.Compose, Microsoft.Outlook" +
    ".Sharing.Compose";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.zToolRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton zLinkB;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton zEstimate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton factsheet;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton gMap;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton getCompTable;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
    }

    partial class ThisRibbonCollection
    {
        internal zToolRibbon zToolRibbon
        {
            get { return this.GetRibbon<zToolRibbon>(); }
        }
    }
}
