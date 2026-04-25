namespace Bifrost.Adv.Controls.PopUp
{
    partial class POPUP_BIZ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_BIZ));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.aGridM = new Bifrost.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.aPanel1 = new Bifrost.Win.Controls.aPanel();
            this.aButton_Search = new Bifrost.Win.Controls.aButton();
            this.txtSearch = new Bifrost.Win.Controls.aTextEdit();
            this.aLabel1 = new Bifrost.Win.Controls.aLabel();
            this.pnlBound.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).BeginInit();
            this.aPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.tableLayoutPanel1);
            this.pnlBound.Size = new System.Drawing.Size(342, 426);
            this.pnlBound.TabIndex = 6;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 514);
            this.pnlBottom.Size = new System.Drawing.Size(382, 46);
            this.pnlBottom.TabIndex = 32;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.aGridM, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.aPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(342, 426);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // aGridM
            // 
            this.aGridM.AddNewRowLastColumn = true;
            this.aGridM.disabledEditingColumns = new string[0];
            this.aGridM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aGridM.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.aGridM.isSaveLayout = false;
            this.aGridM.isUpper = false;
            this.aGridM.LayoutVersion = "";
            this.aGridM.Location = new System.Drawing.Point(3, 55);
            this.aGridM.MainView = this.gridView1;
            this.aGridM.Name = "aGridM";
            this.aGridM.SetBindingEvnet = true;
            this.aGridM.Size = new System.Drawing.Size(336, 368);
            this.aGridM.TabIndex = 13;
            this.aGridM.VerifyNotNull = null;
            this.aGridM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.aGridM;
            this.gridView1.IndicatorWidth = 29;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView1.OptionsView.RowAutoHeight = true;
            // 
            // aPanel1
            // 
            this.aPanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel1.Appearance.Options.UseBackColor = true;
            this.aPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.aPanel1.Controls.Add(this.aButton_Search);
            this.aPanel1.Controls.Add(this.txtSearch);
            this.aPanel1.Controls.Add(this.aLabel1);
            this.aPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.aPanel1.Location = new System.Drawing.Point(3, 3);
            this.aPanel1.Name = "aPanel1";
            this.aPanel1.SetPanelType = Bifrost.Win.Controls.aPanel.PanelType.NONE;
            this.aPanel1.Size = new System.Drawing.Size(336, 46);
            this.aPanel1.TabIndex = 8;
            // 
            // aButton_Search
            // 
            this.aButton_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.aButton_Search.BorderColor = System.Drawing.Color.Transparent;
            this.aButton_Search.ButtonDesignType = Bifrost.Win.Controls.ButtonDesignType.POP_SEARCH;
            this.aButton_Search.DisabledImage = ((System.Drawing.Image)(resources.GetObject("aButton_Search.DisabledImage")));
            this.aButton_Search.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.aButton_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.GroupKey = null;
            this.aButton_Search.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(139)))));
            this.aButton_Search.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aButton_Search.Location = new System.Drawing.Point(226, 0);
            this.aButton_Search.Name = "aButton_Search";
            this.aButton_Search.Selected = false;
            this.aButton_Search.SelectedColor = System.Drawing.Color.LightGray;
            this.aButton_Search.Size = new System.Drawing.Size(100, 24);
            this.aButton_Search.TabIndex = 12;
            this.aButton_Search.Text = "Retrieve";
            this.aButton_Search.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aButton_Search.UnSelectOtherButtons = false;
            this.aButton_Search.UseDefaultImages = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(51, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSearch.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSearch.Properties.Appearance.Options.UseBackColor = true;
            this.txtSearch.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSearch.Properties.AutoHeight = false;
            this.txtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.txtSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions1)});
            this.txtSearch.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.txtSearch.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtSearch.Size = new System.Drawing.Size(153, 24);
            this.txtSearch.TabIndex = 10;
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.Appearance.Options.UseForeColor = true;
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(0, 0);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(44, 24);
            this.aLabel1.TabIndex = 9;
            this.aLabel1.Text = "사업장";
            // 
            // POPUP_BIZ
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Name = "POPUP_BIZ";
            this.Text = "POPUP_BIZ";
            this.pnlBound.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).EndInit();
            this.aPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Bifrost.Grid.aGrid aGridM;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Win.Controls.aPanel aPanel1;
        private Win.Controls.aTextEdit txtSearch;
        private Win.Controls.aLabel aLabel1;
        private Win.Controls.aButton aButton_Search;
    }
}