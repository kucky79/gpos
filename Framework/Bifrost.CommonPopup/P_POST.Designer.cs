namespace Bifrost.CommonPopup
{
    partial class P_POST
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_POST));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnMore = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearchAddr = new DevExpress.XtraEditors.SimpleButton();
            this.aLabel1 = new Bifrost.Win.Controls.aLabel();
            this.aTextEdit_Search = new Bifrost.Win.Controls.aTextEdit();
            this.gridMain = new Bifrost.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_Search.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(0, 407);
            this.pnlBottom.Size = new System.Drawing.Size(800, 43);
            this.pnlBottom.TabIndex = 29;
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.gridMain);
            this.pnlBound.Controls.Add(this.panelControl1);
            this.pnlBound.Size = new System.Drawing.Size(790, 351);
            this.pnlBound.TabIndex = 5;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnMore);
            this.panelControl1.Controls.Add(this.btnSearchAddr);
            this.panelControl1.Controls.Add(this.aLabel1);
            this.panelControl1.Controls.Add(this.aTextEdit_Search);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(790, 36);
            this.panelControl1.TabIndex = 6;
            // 
            // btnMore
            // 
            this.btnMore.Location = new System.Drawing.Point(613, 5);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(120, 24);
            this.btnMore.TabIndex = 11;
            this.btnMore.Text = "더보기";
            // 
            // btnSearchAddr
            // 
            this.btnSearchAddr.Location = new System.Drawing.Point(487, 5);
            this.btnSearchAddr.Name = "btnSearchAddr";
            this.btnSearchAddr.Size = new System.Drawing.Size(120, 24);
            this.btnSearchAddr.TabIndex = 10;
            this.btnSearchAddr.Text = "검색";
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.Appearance.Options.UseForeColor = true;
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(5, 5);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(50, 24);
            this.aLabel1.TabIndex = 7;
            this.aLabel1.Text = "검색";
            // 
            // aTextEdit_Search
            // 
            this.aTextEdit_Search.EditValue = "";
            this.aTextEdit_Search.Location = new System.Drawing.Point(61, 5);
            this.aTextEdit_Search.Name = "aTextEdit_Search";
            this.aTextEdit_Search.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.aTextEdit_Search.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aTextEdit_Search.Properties.Appearance.Options.UseBackColor = true;
            this.aTextEdit_Search.Properties.Appearance.Options.UseBorderColor = true;
            this.aTextEdit_Search.Properties.AutoHeight = false;
            this.aTextEdit_Search.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.aTextEdit_Search.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.aTextEdit_Search.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.aTextEdit_Search.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.aTextEdit_Search.Size = new System.Drawing.Size(420, 24);
            this.aTextEdit_Search.TabIndex = 8;
            // 
            // gridMain
            // 
            this.gridMain.AddNewRowLastColumn = true;
            this.gridMain.disabledEditingColumns = new string[0];
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.gridMain.isSaveLayout = false;
            this.gridMain.isUpper = true;
            this.gridMain.LayoutVersion = "";
            this.gridMain.Location = new System.Drawing.Point(0, 36);
            this.gridMain.MainView = this.gridView1;
            this.gridMain.Name = "gridMain";
            this.gridMain.SetBindingEvnet = true;
            this.gridMain.Size = new System.Drawing.Size(790, 315);
            this.gridMain.TabIndex = 12;
            this.gridMain.VerifyNotNull = null;
            this.gridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridMain;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "우편번호";
            this.gridColumn1.FieldName = "CD_POST";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "도로명주소";
            this.gridColumn2.FieldName = "DC_ADD1";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "지번주소";
            this.gridColumn3.FieldName = "DC_ADD2";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // P_POST
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "P_POST";
            this.Text = "P_POST";
            this.pnlBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_Search.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Grid.aGrid gridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SimpleButton btnMore;
        private DevExpress.XtraEditors.SimpleButton btnSearchAddr;
        private Win.Controls.aLabel aLabel1;
        private Win.Controls.aTextEdit aTextEdit_Search;
    }
}