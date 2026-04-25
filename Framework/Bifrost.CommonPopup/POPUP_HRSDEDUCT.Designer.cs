namespace NF.A2P.CommonPopup
{
    partial class POPUP_HRSDEDUCT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_HRSDEDUCT));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new NF.Framework.Win.Controls.aTextEdit();
            this.aLabel1 = new NF.Framework.Win.Controls.aLabel();
            this.aGridM = new NF.A2P.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.aButton_Search = new NF.Framework.Win.Controls.aButton();
            this.pnlBound.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.aGridM);
            this.pnlBound.Controls.Add(this.panel1);
            this.pnlBound.Size = new System.Drawing.Size(342, 426);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 514);
            this.pnlBottom.Size = new System.Drawing.Size(382, 46);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.aButton_Search);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.aLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 43);
            this.panel1.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(47, 7);
            this.txtSearch.Modified = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSearch.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSearch.Properties.Appearance.Options.UseBackColor = true;
            this.txtSearch.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSearch.Properties.AutoHeight = false;
            this.txtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSearch.Size = new System.Drawing.Size(176, 24);
            this.txtSearch.TabIndex = 3;
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(3, 6);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(38, 24);
            this.aLabel1.TabIndex = 2;
            this.aLabel1.Text = "검색";
            // 
            // aGridM
            // 
            this.aGridM.AddNewRowLastColumn = true;
            this.aGridM.disabledEditingColumns = new string[0];
            this.aGridM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aGridM.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.aGridM.Location = new System.Drawing.Point(5, 48);
            this.aGridM.LookAndFeel.SkinName = "Office 2013";
            this.aGridM.LookAndFeel.UseDefaultLookAndFeel = false;
            this.aGridM.MainView = this.gridView1;
            this.aGridM.Name = "aGridM";
            this.aGridM.SetBindingEvnet = true;
            this.aGridM.Size = new System.Drawing.Size(332, 373);
            this.aGridM.TabIndex = 4;
            this.aGridM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.aGridM;
            this.gridView1.Name = "gridView1";
            // 
            // aButton_Search
            // 
            this.aButton_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.aButton_Search.BorderColor = System.Drawing.Color.Transparent;
            this.aButton_Search.ButtonDesignType = NF.Framework.Win.Controls.ButtonDesignType.POP_SEARCH;
            this.aButton_Search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.aButton_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.GroupKey = null;
            this.aButton_Search.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(139)))));
            this.aButton_Search.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aButton_Search.Location = new System.Drawing.Point(229, 7);
            this.aButton_Search.Name = "aButton_Search";
            this.aButton_Search.Selected = false;
            this.aButton_Search.SelectedColor = System.Drawing.Color.LightGray;
            this.aButton_Search.Size = new System.Drawing.Size(100, 24);
            this.aButton_Search.TabIndex = 8;
            this.aButton_Search.Text = "SEARCH";
            this.aButton_Search.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aButton_Search.UnSelectOtherButtons = false;
            this.aButton_Search.UseDefaultImages = true;
            // 
            // POPUP_HRSDEDUCT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Name = "POPUP_HRSDEDUCT";
            this.Text = "POPUP_HRSDEDUCT";
            this.pnlBound.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private NF.Framework.Win.Controls.aTextEdit txtSearch;
        private NF.Framework.Win.Controls.aLabel aLabel1;
        private A2P.Grid.aGrid aGridM;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Framework.Win.Controls.aButton aButton_Search;
    }
}