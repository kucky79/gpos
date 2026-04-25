namespace NF.A2P.CommonPopup
{
    partial class POPUP_MENU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_MENU));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            this.xPanel1 = new NF.Framework.Win.Controls.XPanel();
            this.aButton_Search = new NF.Framework.Win.Controls.aButton();
            this.txtSearch = new NF.Framework.Win.Controls.aTextEdit();
            this.aLabel1 = new NF.Framework.Win.Controls.aLabel();
            this.aTreeFirm = new NF.A2P.Grid.aTree();
            this.pnlBound.SuspendLayout();
            this.xPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTreeFirm)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.aTreeFirm);
            this.pnlBound.Controls.Add(this.xPanel1);
            // 
            // xPanel1
            // 
            this.xPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.xPanel1.BorderWidth = new System.Windows.Forms.Padding(0);
            this.xPanel1.Controls.Add(this.aButton_Search);
            this.xPanel1.Controls.Add(this.txtSearch);
            this.xPanel1.Controls.Add(this.aLabel1);
            this.xPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xPanel1.Location = new System.Drawing.Point(0, 0);
            this.xPanel1.Name = "xPanel1";
            this.xPanel1.PanelStyle = NF.Framework.Win.Controls.XPanel.PanelStyles.Nomal;
            this.xPanel1.Size = new System.Drawing.Size(378, 56);
            this.xPanel1.TabIndex = 1;
            // 
            // aButton_Search
            // 
            this.aButton_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.aButton_Search.BorderColor = System.Drawing.Color.Transparent;
            this.aButton_Search.ButtonDesignType = NF.Framework.Win.Controls.ButtonDesignType.POP_SEARCH;
            this.aButton_Search.DisabledImage = ((System.Drawing.Image)(resources.GetObject("aButton_Search.DisabledImage")));
            this.aButton_Search.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.aButton_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.GroupKey = null;
            this.aButton_Search.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(139)))));
            this.aButton_Search.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aButton_Search.Location = new System.Drawing.Point(262, 11);
            this.aButton_Search.Name = "aButton_Search";
            this.aButton_Search.Selected = false;
            this.aButton_Search.SelectedColor = System.Drawing.Color.LightGray;
            this.aButton_Search.Size = new System.Drawing.Size(100, 24);
            this.aButton_Search.TabIndex = 8;
            this.aButton_Search.Text = "Search";
            this.aButton_Search.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aButton_Search.UnSelectOtherButtons = false;
            this.aButton_Search.UseDefaultImages = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.isUpper = true;
            this.txtSearch.Location = new System.Drawing.Point(65, 12);
            this.txtSearch.Modified = false;
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
            this.txtSearch.Size = new System.Drawing.Size(176, 24);
            this.txtSearch.TabIndex = 7;
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.Appearance.Options.UseFont = true;
            this.aLabel1.Appearance.Options.UseForeColor = true;
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(18, 11);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(31, 24);
            this.aLabel1.TabIndex = 6;
            this.aLabel1.Text = "메뉴";
            // 
            // aTreeFirm
            // 
            this.aTreeFirm.Appearance.HorzLine.BackColor = System.Drawing.Color.Red;
            this.aTreeFirm.Appearance.HorzLine.Options.UseBackColor = true;
            this.aTreeFirm.Appearance.VertLine.BackColor = System.Drawing.Color.Red;
            this.aTreeFirm.Appearance.VertLine.Options.UseBackColor = true;
            this.aTreeFirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aTreeFirm.Location = new System.Drawing.Point(0, 56);
            this.aTreeFirm.Name = "aTreeFirm";
            this.aTreeFirm.OptionsBehavior.Editable = false;
            this.aTreeFirm.OptionsBehavior.ReadOnly = true;
            this.aTreeFirm.OptionsDragAndDrop.AcceptOuterNodes = true;
            this.aTreeFirm.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.aTreeFirm.OptionsNavigation.AutoFocusNewNode = true;
            this.aTreeFirm.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.aTreeFirm.OptionsSelection.KeepSelectedOnClick = false;
            this.aTreeFirm.OptionsSelection.MultiSelect = true;
            this.aTreeFirm.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            this.aTreeFirm.OptionsView.ShowColumns = false;
            this.aTreeFirm.OptionsView.ShowHorzLines = false;
            this.aTreeFirm.OptionsView.ShowIndicator = false;
            this.aTreeFirm.OptionsView.ShowVertLines = false;
            this.aTreeFirm.Size = new System.Drawing.Size(378, 442);
            this.aTreeFirm.TabIndex = 5;
            // 
            // POPUP_MENU
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 633);
            this.Name = "POPUP_MENU";
            this.Text = "POPUP_MENU";
            this.pnlBound.ResumeLayout(false);
            this.xPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTreeFirm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.Win.Controls.XPanel xPanel1;
        private Framework.Win.Controls.aButton aButton_Search;
        private Framework.Win.Controls.aTextEdit txtSearch;
        private Framework.Win.Controls.aLabel aLabel1;
        private Grid.aTree aTreeFirm;
    }
}