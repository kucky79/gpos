namespace NF.A2P.CommonPopup
{
    partial class POPUP_DEPT
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_DEPT));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            this.panel1 = new System.Windows.Forms.Panel();
            this.aButton_Search = new NF.Framework.Win.Controls.aButton();
            this.txtSearch = new NF.Framework.Win.Controls.aTextEdit();
            this.aLabel1 = new NF.Framework.Win.Controls.aLabel();
            this.aGridM = new NF.A2P.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.aLabel2 = new NF.Framework.Win.Controls.aLabel();
            this.aDateEdit_Rfdate = new NF.Framework.Win.Controls.aDateEdit();
            this.pnlBound.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aDateEdit_Rfdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aDateEdit_Rfdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.aGridM);
            this.pnlBound.Controls.Add(this.panel1);
            this.pnlBound.Size = new System.Drawing.Size(352, 426);
            this.pnlBound.TabIndex = 6;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 514);
            this.pnlBottom.Size = new System.Drawing.Size(392, 46);
            this.pnlBottom.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.aLabel2);
            this.panel1.Controls.Add(this.aDateEdit_Rfdate);
            this.panel1.Controls.Add(this.aButton_Search);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.aLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 59);
            this.panel1.TabIndex = 7;
            // 
            // aButton_Search
            // 
            this.aButton_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.aButton_Search.BorderColor = System.Drawing.Color.Transparent;
            this.aButton_Search.ButtonDesignType = NF.Framework.Win.Controls.ButtonDesignType.POP_SEARCH;
            this.aButton_Search.DisabledImage = ((System.Drawing.Image)(resources.GetObject("aButton_Search.DisabledImage")));
            this.aButton_Search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.aButton_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.GroupKey = null;
            this.aButton_Search.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(139)))));
            this.aButton_Search.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aButton_Search.Location = new System.Drawing.Point(247, 32);
            this.aButton_Search.Name = "aButton_Search";
            this.aButton_Search.Selected = false;
            this.aButton_Search.SelectedColor = System.Drawing.Color.LightGray;
            this.aButton_Search.Size = new System.Drawing.Size(100, 24);
            this.aButton_Search.TabIndex = 10;
            this.aButton_Search.Text = "Search";
            this.aButton_Search.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aButton_Search.UnSelectOtherButtons = false;
            this.aButton_Search.UseDefaultImages = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(44, 31);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSearch.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSearch.Properties.Appearance.Options.UseBackColor = true;
            this.txtSearch.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSearch.Properties.AutoHeight = false;
            this.txtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions3.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions3.Image")));
            this.txtSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions3)});
            this.txtSearch.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.txtSearch.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtSearch.Size = new System.Drawing.Size(200, 24);
            this.txtSearch.TabIndex = 8;
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.Appearance.Options.UseFont = true;
            this.aLabel1.Appearance.Options.UseForeColor = true;
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(11, 32);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(27, 24);
            this.aLabel1.TabIndex = 11;
            this.aLabel1.Text = "Dept.";
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
            this.aGridM.Location = new System.Drawing.Point(0, 59);
            this.aGridM.MainView = this.gridView1;
            this.aGridM.Name = "aGridM";
            this.aGridM.SetBindingEvnet = true;
            this.aGridM.Size = new System.Drawing.Size(352, 367);
            this.aGridM.TabIndex = 12;
            this.aGridM.VerifyNotNull = null;
            this.aGridM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.aGridM;
            this.gridView1.Name = "gridView1";
            // 
            // aLabel2
            // 
            this.aLabel2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel2.Appearance.Options.UseForeColor = true;
            this.aLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel2.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel2.Location = new System.Drawing.Point(10, 2);
            this.aLabel2.Name = "aLabel2";
            this.aLabel2.Size = new System.Drawing.Size(90, 24);
            this.aLabel2.TabIndex = 15;
            this.aLabel2.Text = "Reference Date.";
            // 
            // aDateEdit_Rfdate
            // 
            this.aDateEdit_Rfdate.DateFormat = "MM\\/dd\\/yyyy";
            this.aDateEdit_Rfdate.EditValue = "";
            this.aDateEdit_Rfdate.Location = new System.Drawing.Point(106, 3);
            this.aDateEdit_Rfdate.Name = "aDateEdit_Rfdate";
            this.aDateEdit_Rfdate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.aDateEdit_Rfdate.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aDateEdit_Rfdate.Properties.Appearance.Options.UseBackColor = true;
            this.aDateEdit_Rfdate.Properties.Appearance.Options.UseBorderColor = true;
            this.aDateEdit_Rfdate.Properties.AutoHeight = false;
            this.aDateEdit_Rfdate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            serializableAppearanceObject1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            serializableAppearanceObject1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            serializableAppearanceObject1.Options.UseBackColor = true;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.aDateEdit_Rfdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions2)});
            this.aDateEdit_Rfdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.aDateEdit_Rfdate.Properties.DisplayFormat.FormatString = "MM\\/dd\\/yyyy";
            this.aDateEdit_Rfdate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.aDateEdit_Rfdate.Properties.EditFormat.FormatString = "MM\\/dd\\/yyyy";
            this.aDateEdit_Rfdate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.aDateEdit_Rfdate.Properties.Mask.EditMask = "MM\\/dd\\/yyyy";
            this.aDateEdit_Rfdate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.aDateEdit_Rfdate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.aDateEdit_Rfdate.Properties.NullDate = new System.DateTime(((long)(0)));
            this.aDateEdit_Rfdate.Size = new System.Drawing.Size(138, 24);
            this.aDateEdit_Rfdate.TabIndex = 14;
            // 
            // POPUP_DEPT
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 561);
            this.Name = "POPUP_DEPT";
            this.Text = "POPUP_DEPT";
            this.pnlBound.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aDateEdit_Rfdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aDateEdit_Rfdate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private NF.Framework.Win.Controls.aTextEdit txtSearch;
        private NF.Framework.Win.Controls.aLabel aLabel1;
        private A2P.Grid.aGrid aGridM;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Framework.Win.Controls.aButton aButton_Search;
        private Framework.Win.Controls.aLabel aLabel2;
        private Framework.Win.Controls.aDateEdit aDateEdit_Rfdate;
    }
}