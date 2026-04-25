namespace Bifrost.Adv.Controls.PopUp
{
    partial class POPUP_ITEM_S
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_ITEM_S));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            this.aPanel1 = new Bifrost.Win.Controls.aPanel();
            this.btnSearch = new Bifrost.Win.Controls.aButton();
            this.aTextEdit_Cust = new Bifrost.Win.Controls.aTextEdit();
            this.aLabel3 = new Bifrost.Win.Controls.aLabel();
            this.aTextEdit_ItemName = new Bifrost.Win.Controls.aTextEdit();
            this.aTextEdit_ItemCode = new Bifrost.Win.Controls.aTextEdit();
            this.aLabel2 = new Bifrost.Win.Controls.aLabel();
            this.aLabel1 = new Bifrost.Win.Controls.aLabel();
            this.aGrid1 = new Bifrost.Grid.aGrid();
            this.gvItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcItem1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItem2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItem3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItem4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItem5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).BeginInit();
            this.aPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_Cust.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_ItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_ItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.aGrid1);
            this.pnlBound.Controls.Add(this.aPanel1);
            this.pnlBound.Size = new System.Drawing.Size(758, 515);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 603);
            this.pnlBottom.Size = new System.Drawing.Size(798, 46);
            // 
            // aPanel1
            // 
            this.aPanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(253)))));
            this.aPanel1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(253)))));
            this.aPanel1.Appearance.Options.UseBackColor = true;
            this.aPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.aPanel1.Controls.Add(this.btnSearch);
            this.aPanel1.Controls.Add(this.aTextEdit_Cust);
            this.aPanel1.Controls.Add(this.aLabel3);
            this.aPanel1.Controls.Add(this.aTextEdit_ItemName);
            this.aPanel1.Controls.Add(this.aTextEdit_ItemCode);
            this.aPanel1.Controls.Add(this.aLabel2);
            this.aPanel1.Controls.Add(this.aLabel1);
            this.aPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.aPanel1.Location = new System.Drawing.Point(0, 0);
            this.aPanel1.Name = "aPanel1";
            this.aPanel1.SetPanelType = Bifrost.Win.Controls.aPanel.PanelType.MAINFORM;
            this.aPanel1.Size = new System.Drawing.Size(758, 61);
            this.aPanel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnSearch.BorderColor = System.Drawing.Color.Transparent;
            this.btnSearch.ButtonDesignType = Bifrost.Win.Controls.ButtonDesignType.POP_SEARCH;
            this.btnSearch.DisabledImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DisabledImage")));
            this.btnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSearch.GroupKey = null;
            this.btnSearch.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(139)))));
            this.btnSearch.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(658, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Selected = false;
            this.btnSearch.SelectedColor = System.Drawing.Color.LightGray;
            this.btnSearch.Size = new System.Drawing.Size(100, 24);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSearch.UnSelectOtherButtons = false;
            this.btnSearch.UseDefaultImages = true;
            // 
            // aTextEdit_Cust
            // 
            this.aTextEdit_Cust.isUpper = true;
            this.aTextEdit_Cust.Location = new System.Drawing.Point(419, 3);
            this.aTextEdit_Cust.Modified = true;
            this.aTextEdit_Cust.Name = "aTextEdit_Cust";
            this.aTextEdit_Cust.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.aTextEdit_Cust.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aTextEdit_Cust.Properties.Appearance.Options.UseBackColor = true;
            this.aTextEdit_Cust.Properties.Appearance.Options.UseBorderColor = true;
            this.aTextEdit_Cust.Properties.AutoHeight = false;
            this.aTextEdit_Cust.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.aTextEdit_Cust.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions1)});
            this.aTextEdit_Cust.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.aTextEdit_Cust.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.aTextEdit_Cust.Size = new System.Drawing.Size(200, 24);
            this.aTextEdit_Cust.TabIndex = 2;
            // 
            // aLabel3
            // 
            this.aLabel3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel3.Appearance.Options.UseFont = true;
            this.aLabel3.Appearance.Options.UseForeColor = true;
            this.aLabel3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel3.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.aLabel3.Location = new System.Drawing.Point(343, 3);
            this.aLabel3.Name = "aLabel3";
            this.aLabel3.Size = new System.Drawing.Size(70, 24);
            this.aLabel3.TabIndex = 4;
            this.aLabel3.Text = "거래처";
            // 
            // aTextEdit_ItemName
            // 
            this.aTextEdit_ItemName.isUpper = true;
            this.aTextEdit_ItemName.Location = new System.Drawing.Point(79, 33);
            this.aTextEdit_ItemName.Modified = true;
            this.aTextEdit_ItemName.Name = "aTextEdit_ItemName";
            this.aTextEdit_ItemName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.aTextEdit_ItemName.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aTextEdit_ItemName.Properties.Appearance.Options.UseBackColor = true;
            this.aTextEdit_ItemName.Properties.Appearance.Options.UseBorderColor = true;
            this.aTextEdit_ItemName.Properties.AutoHeight = false;
            this.aTextEdit_ItemName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.aTextEdit_ItemName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions2)});
            this.aTextEdit_ItemName.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.aTextEdit_ItemName.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.aTextEdit_ItemName.Size = new System.Drawing.Size(200, 24);
            this.aTextEdit_ItemName.TabIndex = 1;
            // 
            // aTextEdit_ItemCode
            // 
            this.aTextEdit_ItemCode.isUpper = true;
            this.aTextEdit_ItemCode.Location = new System.Drawing.Point(79, 3);
            this.aTextEdit_ItemCode.Modified = true;
            this.aTextEdit_ItemCode.Name = "aTextEdit_ItemCode";
            this.aTextEdit_ItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.aTextEdit_ItemCode.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aTextEdit_ItemCode.Properties.Appearance.Options.UseBackColor = true;
            this.aTextEdit_ItemCode.Properties.Appearance.Options.UseBorderColor = true;
            this.aTextEdit_ItemCode.Properties.AutoHeight = false;
            this.aTextEdit_ItemCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.aTextEdit_ItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions3)});
            this.aTextEdit_ItemCode.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.aTextEdit_ItemCode.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.aTextEdit_ItemCode.Size = new System.Drawing.Size(200, 24);
            this.aTextEdit_ItemCode.TabIndex = 0;
            // 
            // aLabel2
            // 
            this.aLabel2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel2.Appearance.Options.UseFont = true;
            this.aLabel2.Appearance.Options.UseForeColor = true;
            this.aLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel2.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.aLabel2.Location = new System.Drawing.Point(3, 33);
            this.aLabel2.Name = "aLabel2";
            this.aLabel2.Size = new System.Drawing.Size(70, 24);
            this.aLabel2.TabIndex = 1;
            this.aLabel2.Text = "품명";
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.Appearance.Options.UseFont = true;
            this.aLabel1.Appearance.Options.UseForeColor = true;
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(3, 3);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(70, 24);
            this.aLabel1.TabIndex = 0;
            this.aLabel1.Text = "품목코드";
            // 
            // aGrid1
            // 
            this.aGrid1.AddNewRowLastColumn = true;
            this.aGrid1.disabledEditingColumns = new string[0];
            this.aGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aGrid1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.aGrid1.isSaveLayout = false;
            this.aGrid1.isUpper = false;
            this.aGrid1.LayoutVersion = "";
            this.aGrid1.Location = new System.Drawing.Point(0, 61);
            this.aGrid1.MainView = this.gvItem;
            this.aGrid1.Name = "aGrid1";
            this.aGrid1.SetBindingEvnet = true;
            this.aGrid1.Size = new System.Drawing.Size(758, 454);
            this.aGrid1.TabIndex = 0;
            this.aGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItem});
            // 
            // gvItem
            // 
            this.gvItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcItem1,
            this.gcItem2,
            this.gcItem3,
            this.gcItem4,
            this.gcItem5});
            this.gvItem.GridControl = this.aGrid1;
            this.gvItem.Name = "gvItem";
            // 
            // gcItem1
            // 
            this.gcItem1.AppearanceHeader.Options.UseTextOptions = true;
            this.gcItem1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcItem1.Caption = "품목코드";
            this.gcItem1.FieldName = "CD_ITEM";
            this.gcItem1.Name = "gcItem1";
            this.gcItem1.OptionsColumn.AllowEdit = false;
            this.gcItem1.Visible = true;
            this.gcItem1.VisibleIndex = 0;
            // 
            // gcItem2
            // 
            this.gcItem2.AppearanceHeader.Options.UseTextOptions = true;
            this.gcItem2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcItem2.Caption = "품명";
            this.gcItem2.FieldName = "NM_ITEM";
            this.gcItem2.Name = "gcItem2";
            this.gcItem2.OptionsColumn.AllowEdit = false;
            this.gcItem2.Visible = true;
            this.gcItem2.VisibleIndex = 1;
            // 
            // gcItem3
            // 
            this.gcItem3.AppearanceHeader.Options.UseTextOptions = true;
            this.gcItem3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcItem3.Caption = "거래처코드";
            this.gcItem3.FieldName = "CD_PARTNER";
            this.gcItem3.Name = "gcItem3";
            this.gcItem3.OptionsColumn.AllowEdit = false;
            // 
            // gcItem4
            // 
            this.gcItem4.AppearanceHeader.Options.UseTextOptions = true;
            this.gcItem4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcItem4.Caption = "거래처명";
            this.gcItem4.FieldName = "NM_PARTNER";
            this.gcItem4.Name = "gcItem4";
            this.gcItem4.OptionsColumn.AllowEdit = false;
            this.gcItem4.Visible = true;
            this.gcItem4.VisibleIndex = 2;
            // 
            // gcItem5
            // 
            this.gcItem5.AppearanceHeader.Options.UseTextOptions = true;
            this.gcItem5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcItem5.Caption = "제품구분";
            this.gcItem5.FieldName = "TP_ITEM";
            this.gcItem5.Name = "gcItem5";
            this.gcItem5.OptionsColumn.AllowEdit = false;
            this.gcItem5.Visible = true;
            this.gcItem5.VisibleIndex = 3;
            // 
            // POPUP_ITEM_S
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Name = "POPUP_ITEM_S";
            this.Text = "POPUP_ITEM";
            this.pnlBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).EndInit();
            this.aPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_Cust.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_ItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_ItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bifrost.Win.Controls.aPanel aPanel1;
        private Bifrost.Grid.aGrid aGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItem;
        private Bifrost.Win.Controls.aTextEdit aTextEdit_Cust;
        private Bifrost.Win.Controls.aLabel aLabel3;
        private Bifrost.Win.Controls.aTextEdit aTextEdit_ItemName;
        private Bifrost.Win.Controls.aTextEdit aTextEdit_ItemCode;
        private Bifrost.Win.Controls.aLabel aLabel2;
        private Bifrost.Win.Controls.aLabel aLabel1;
        private Bifrost.Win.Controls.aButton btnSearch;
        private DevExpress.XtraGrid.Columns.GridColumn gcItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gcItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gcItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gcItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gcItem5;
    }
}