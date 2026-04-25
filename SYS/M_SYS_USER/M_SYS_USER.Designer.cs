namespace SYS
{
    partial class M_SYS_USER
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M_SYS_USER));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            this.aGridMain = new Bifrost.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.button_reset = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.aTextEdit1 = new Bifrost.Win.Controls.aTextEdit();
            this.aLabel2 = new Bifrost.Win.Controls.aLabel();
            this.aLabel1 = new Bifrost.Win.Controls.aLabel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.aLookUpEditUseYN = new Bifrost.Win.Controls.aLookUpEdit();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aGridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.button_reset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEditUseYN.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.aGridMain);
            this.pnlContainer.Controls.Add(this.panelControl1);
            this.pnlContainer.Location = new System.Drawing.Point(1, 86);
            this.pnlContainer.Size = new System.Drawing.Size(1063, 609);
            this.pnlContainer.TabIndex = 15;
            // 
            // aGridMain
            // 
            this.aGridMain.AddNewRowLastColumn = true;
            this.aGridMain.disabledEditingColumns = new string[0];
            this.aGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aGridMain.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.aGridMain.isSaveLayout = false;
            this.aGridMain.isUpper = false;
            this.aGridMain.LayoutVersion = "";
            this.aGridMain.Location = new System.Drawing.Point(0, 50);
            this.aGridMain.MainView = this.gridView1;
            this.aGridMain.Name = "aGridMain";
            this.aGridMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.button_reset});
            this.aGridMain.SetBindingEvnet = true;
            this.aGridMain.Size = new System.Drawing.Size(1063, 559);
            this.aGridMain.TabIndex = 25;
            this.aGridMain.VerifyNotNull = null;
            this.aGridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn12,
            this.gridColumn3,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn11,
            this.gridColumn13});
            this.gridView1.GridControl = this.aGridMain;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "CD_USER";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 97;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "암호";
            this.gridColumn2.FieldName = "NO_PWD";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 88;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "초기화";
            this.gridColumn12.ColumnEdit = this.button_reset;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            this.gridColumn12.Width = 54;
            // 
            // button_reset
            // 
            this.button_reset.AutoHeight = false;
            this.button_reset.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Undo, "RESET", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.button_reset.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.button_reset.Name = "button_reset";
            this.button_reset.ReadOnly = true;
            this.button_reset.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "사용자명";
            this.gridColumn3.FieldName = "NM_USER";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 124;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "사원";
            this.gridColumn7.FieldName = "NM_EMP";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 124;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "사용유무";
            this.gridColumn8.FieldName = "YN_USE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 59;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "CD_FIRM";
            this.gridColumn11.FieldName = "CD_FIRM";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "CD_EMP";
            this.gridColumn13.FieldName = "CD_EMP";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // aTextEdit1
            // 
            this.aTextEdit1.isUpper = true;
            this.aTextEdit1.Location = new System.Drawing.Point(300, 13);
            this.aTextEdit1.Name = "aTextEdit1";
            this.aTextEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.aTextEdit1.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aTextEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.aTextEdit1.Properties.Appearance.Options.UseBorderColor = true;
            this.aTextEdit1.Properties.AutoHeight = false;
            this.aTextEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions4.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions4.Image")));
            this.aTextEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.aTextEdit1.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.aTextEdit1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.aTextEdit1.Size = new System.Drawing.Size(204, 24);
            this.aTextEdit1.TabIndex = 18;
            // 
            // aLabel2
            // 
            this.aLabel2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel2.Appearance.Options.UseForeColor = true;
            this.aLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel2.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.aLabel2.Location = new System.Drawing.Point(269, 16);
            this.aLabel2.Name = "aLabel2";
            this.aLabel2.Size = new System.Drawing.Size(53, 19);
            this.aLabel2.TabIndex = 23;
            this.aLabel2.Text = "검색";
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.Appearance.Options.UseForeColor = true;
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(22, 16);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(47, 19);
            this.aLabel1.TabIndex = 22;
            this.aLabel1.Text = "사용유무";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.aLookUpEditUseYN);
            this.panelControl1.Controls.Add(this.aTextEdit1);
            this.panelControl1.Controls.Add(this.aLabel1);
            this.panelControl1.Controls.Add(this.aLabel2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1063, 50);
            this.panelControl1.TabIndex = 17;
            // 
            // aLookUpEditUseYN
            // 
            this.aLookUpEditUseYN.Location = new System.Drawing.Point(75, 14);
            this.aLookUpEditUseYN.Name = "aLookUpEditUseYN";
            this.aLookUpEditUseYN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.aLookUpEditUseYN.Properties.NullText = "";
            this.aLookUpEditUseYN.Size = new System.Drawing.Size(100, 24);
            this.aLookUpEditUseYN.TabIndex = 20;
            // 
            // M_SYS_USER
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 706);
            this.Name = "M_SYS_USER";
            this.SubTitle = "M_SYS_USER";
            this.Text = "M_SYS_USER";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aGridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.button_reset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEditUseYN.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bifrost.Grid.aGrid aGridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit button_reset;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private Bifrost.Win.Controls.aTextEdit aTextEdit1;
        private Bifrost.Win.Controls.aLabel aLabel2;
        private Bifrost.Win.Controls.aLabel aLabel1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Bifrost.Win.Controls.aLookUpEdit aLookUpEditUseYN;
    }
}