namespace NF.A2P.CommonPopup
{
    partial class POPUP_LOCATION
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_LOCATION));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.timer1 = new System.Windows.Forms.Timer();
            this.txtSearch = new NF.Framework.Win.Controls.aTextEdit();
            this.aGridM = new NF.A2P.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_CD_LOC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_NM_LOC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_CD_COUNTRY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.aPanel1 = new NF.Framework.Win.Controls.aPanel();
            this.aLookUpEdit_AirSea = new NF.Framework.Win.Controls.aLookUpEdit();
            this.aLabel2 = new NF.Framework.Win.Controls.aLabel();
            this.aLabel1 = new NF.Framework.Win.Controls.aLabel();
            this.pnlBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).BeginInit();
            this.aPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEdit_AirSea.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.tableLayoutPanel1);
            this.pnlBound.Size = new System.Drawing.Size(432, 466);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 554);
            this.pnlBottom.Size = new System.Drawing.Size(472, 46);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(69, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSearch.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSearch.Properties.Appearance.Options.UseBackColor = true;
            this.txtSearch.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSearch.Properties.AutoHeight = false;
            this.txtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.txtSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions2)});
            this.txtSearch.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.txtSearch.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtSearch.Size = new System.Drawing.Size(158, 24);
            this.txtSearch.TabIndex = 3;
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
            this.aGridM.Location = new System.Drawing.Point(3, 48);
            this.aGridM.MainView = this.gridView1;
            this.aGridM.Name = "aGridM";
            this.aGridM.SetBindingEvnet = true;
            this.aGridM.Size = new System.Drawing.Size(426, 415);
            this.aGridM.TabIndex = 2;
            this.aGridM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_CD_LOC,
            this.gridColumn_NM_LOC,
            this.gridColumn_CD_COUNTRY});
            this.gridView1.GridControl = this.aGridM;
            this.gridView1.IndicatorWidth = 29;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // gridColumn_CD_LOC
            // 
            this.gridColumn_CD_LOC.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_CD_LOC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_CD_LOC.Caption = "Location Code";
            this.gridColumn_CD_LOC.FieldName = "CD_LOC";
            this.gridColumn_CD_LOC.Name = "gridColumn_CD_LOC";
            this.gridColumn_CD_LOC.Visible = true;
            this.gridColumn_CD_LOC.VisibleIndex = 0;
            this.gridColumn_CD_LOC.Width = 100;
            // 
            // gridColumn_NM_LOC
            // 
            this.gridColumn_NM_LOC.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_NM_LOC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_NM_LOC.Caption = "Location Name";
            this.gridColumn_NM_LOC.FieldName = "NM_LOC";
            this.gridColumn_NM_LOC.Name = "gridColumn_NM_LOC";
            this.gridColumn_NM_LOC.Visible = true;
            this.gridColumn_NM_LOC.VisibleIndex = 1;
            this.gridColumn_NM_LOC.Width = 200;
            // 
            // gridColumn_CD_COUNTRY
            // 
            this.gridColumn_CD_COUNTRY.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_CD_COUNTRY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_CD_COUNTRY.Caption = "Country";
            this.gridColumn_CD_COUNTRY.FieldName = "CD_COUNTRY";
            this.gridColumn_CD_COUNTRY.Name = "gridColumn_CD_COUNTRY";
            this.gridColumn_CD_COUNTRY.Visible = true;
            this.gridColumn_CD_COUNTRY.VisibleIndex = 2;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 466);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // aPanel1
            // 
            this.aPanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel1.Appearance.Options.UseBackColor = true;
            this.aPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.aPanel1.Controls.Add(this.aLookUpEdit_AirSea);
            this.aPanel1.Controls.Add(this.aLabel2);
            this.aPanel1.Controls.Add(this.aLabel1);
            this.aPanel1.Controls.Add(this.txtSearch);
            this.aPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aPanel1.Location = new System.Drawing.Point(3, 3);
            this.aPanel1.Name = "aPanel1";
            this.aPanel1.SetPanelType = NF.Framework.Win.Controls.aPanel.PanelType.NONE;
            this.aPanel1.Size = new System.Drawing.Size(426, 39);
            this.aPanel1.TabIndex = 0;
            // 
            // aLookUpEdit_AirSea
            // 
            this.aLookUpEdit_AirSea.Location = new System.Drawing.Point(321, 8);
            this.aLookUpEdit_AirSea.Name = "aLookUpEdit_AirSea";
            this.aLookUpEdit_AirSea.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.aLookUpEdit_AirSea.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aLookUpEdit_AirSea.Properties.Appearance.Options.UseBackColor = true;
            this.aLookUpEdit_AirSea.Properties.Appearance.Options.UseBorderColor = true;
            this.aLookUpEdit_AirSea.Properties.AutoHeight = false;
            this.aLookUpEdit_AirSea.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            serializableAppearanceObject1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            serializableAppearanceObject1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            serializableAppearanceObject1.Options.UseBackColor = true;
            this.aLookUpEdit_AirSea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1)});
            this.aLookUpEdit_AirSea.Properties.DropDownRows = 15;
            this.aLookUpEdit_AirSea.Properties.NullText = "";
            this.aLookUpEdit_AirSea.Properties.PopupFormMinSize = new System.Drawing.Size(50, 50);
            this.aLookUpEdit_AirSea.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize;
            this.aLookUpEdit_AirSea.Properties.ReadOnly = true;
            this.aLookUpEdit_AirSea.Properties.ShowFooter = false;
            this.aLookUpEdit_AirSea.Properties.ShowHeader = false;
            this.aLookUpEdit_AirSea.Properties.ShowLines = false;
            this.aLookUpEdit_AirSea.Properties.ShowNullValuePromptWhenFocused = true;
            this.aLookUpEdit_AirSea.Properties.UseDropDownRowsAsMaxCount = true;
            this.aLookUpEdit_AirSea.Size = new System.Drawing.Size(73, 24);
            this.aLookUpEdit_AirSea.TabIndex = 0;
            // 
            // aLabel2
            // 
            this.aLabel2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.aLabel2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel2.Appearance.Options.UseFont = true;
            this.aLabel2.Appearance.Options.UseForeColor = true;
            this.aLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel2.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel2.Location = new System.Drawing.Point(258, 8);
            this.aLabel2.Name = "aLabel2";
            this.aLabel2.Size = new System.Drawing.Size(57, 24);
            this.aLabel2.TabIndex = 4;
            this.aLabel2.Text = "Air / Sea";
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.Appearance.Options.UseFont = true;
            this.aLabel1.Appearance.Options.UseForeColor = true;
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(18, 8);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(45, 24);
            this.aLabel1.TabIndex = 0;
            this.aLabel1.Text = "Search";
            // 
            // POPUP_LOCATION
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 601);
            this.Name = "POPUP_LOCATION";
            this.Text = "POPUP_LOCATION";
            this.pnlBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).EndInit();
            this.aPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEdit_AirSea.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private NF.Framework.Win.Controls.aTextEdit txtSearch;
        private A2P.Grid.aGrid aGridM;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NF.Framework.Win.Controls.aPanel aPanel1;
        private NF.Framework.Win.Controls.aLookUpEdit aLookUpEdit_AirSea;
        private NF.Framework.Win.Controls.aLabel aLabel2;
        private NF.Framework.Win.Controls.aLabel aLabel1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_CD_LOC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_NM_LOC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_CD_COUNTRY;
    }
}