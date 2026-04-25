namespace POS
{
    partial class P_POS_TMP_ITEM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_POS_TMP_ITEM));
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.dtpSale = new Bifrost.Win.Controls.aDateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnDone = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridList = new Bifrost.Grid.aGrid();
            this.aNavButton1 = new Bifrost.Win.Controls.aControl.aNavButton();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSale.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.gridList);
            this.panelContainer.Controls.Add(this.aNavButton1);
            this.panelContainer.Controls.Add(this.panelBottom);
            this.panelContainer.Controls.Add(this.panelTop);
            this.panelContainer.Padding = new System.Windows.Forms.Padding(10);
            this.panelContainer.Size = new System.Drawing.Size(800, 754);
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTop.Controls.Add(this.dtpSale);
            this.panelTop.Controls.Add(this.labelControl2);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(10, 10);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(780, 45);
            this.panelTop.TabIndex = 5;
            // 
            // dtpSale
            // 
            this.dtpSale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpSale.DateFormat = "yyyy\\-MM\\-dd";
            this.dtpSale.EditValue = "2019-12-03";
            this.dtpSale.Location = new System.Drawing.Point(110, 6);
            this.dtpSale.Name = "dtpSale";
            this.dtpSale.Properties.AllowFocused = false;
            this.dtpSale.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtpSale.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F);
            this.dtpSale.Properties.Appearance.Options.UseFont = true;
            this.dtpSale.Properties.AppearanceCalendar.Header.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.dtpSale.Properties.AppearanceCalendar.Header.Options.UseFont = true;
            this.dtpSale.Properties.AppearanceDropDown.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.dtpSale.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpSale.Properties.AutoHeight = false;
            this.dtpSale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpSale.Properties.CalendarTimeProperties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.dtpSale.Properties.CalendarTimeProperties.Appearance.Options.UseFont = true;
            this.dtpSale.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpSale.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic;
            this.dtpSale.Properties.CellSize = new System.Drawing.Size(50, 50);
            this.dtpSale.Properties.DisplayFormat.FormatString = "yyyy\\-MM\\-dd";
            this.dtpSale.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpSale.Properties.EditFormat.FormatString = "yyyy\\-MM\\-dd";
            this.dtpSale.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpSale.Properties.Mask.EditMask = "yyyy\\-MM\\-dd";
            this.dtpSale.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtpSale.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtpSale.Properties.NullDate = new System.DateTime(((long)(0)));
            this.dtpSale.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtpSale.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtpSale.Size = new System.Drawing.Size(176, 32);
            this.dtpSale.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(3, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 37);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "판매일";
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelBottom.Controls.Add(this.btnPrint);
            this.panelBottom.Controls.Add(this.btnDone);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(10, 654);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(780, 90);
            this.panelBottom.TabIndex = 33;
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnPrint.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPrint.ImageOptions.SvgImage")));
            this.btnPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnPrint.Location = new System.Drawing.Point(0, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 78);
            this.btnPrint.TabIndex = 34;
            this.btnPrint.Text = "출력";
            // 
            // btnDone
            // 
            this.btnDone.AllowFocus = false;
            this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDone.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.btnDone.Appearance.Options.UseFont = true;
            this.btnDone.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnDone.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDone.ImageOptions.SvgImage")));
            this.btnDone.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnDone.Location = new System.Drawing.Point(534, 12);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(120, 78);
            this.btnDone.TabIndex = 35;
            this.btnDone.Text = "확인";
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnCancel.Location = new System.Drawing.Point(660, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 78);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "취소";
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.No,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn3});
            this.gridView1.DetailHeight = 305;
            this.gridView1.GridControl = this.gridList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // No
            // 
            this.No.Caption = "No";
            this.No.FieldName = "NO_LINE";
            this.No.Name = "No";
            this.No.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "상품코드";
            this.gridColumn1.FieldName = "CD_ITEM";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "상품명";
            this.gridColumn2.FieldName = "NM_ITEM";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "단위";
            this.gridColumn4.FieldName = "DC_ITEM";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "금액";
            this.gridColumn3.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "AM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // gridList
            // 
            this.gridList.AddNewRowLastColumn = false;
            this.gridList.disabledEditingColumns = null;
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.gridList.isSaveLayout = false;
            this.gridList.isUpper = false;
            this.gridList.LayoutVersion = "";
            this.gridList.Location = new System.Drawing.Point(10, 55);
            this.gridList.MainView = this.gridView1;
            this.gridList.Name = "gridList";
            this.gridList.SetBindingEvnet = true;
            this.gridList.Size = new System.Drawing.Size(730, 599);
            this.gridList.TabIndex = 9;
            this.gridList.VerifyNotNull = null;
            this.gridList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // aNavButton1
            // 
            this.aNavButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.aNavButton1.Location = new System.Drawing.Point(740, 55);
            this.aNavButton1.Name = "aNavButton1";
            this.aNavButton1.NavGrid = this.gridList;
            this.aNavButton1.Size = new System.Drawing.Size(50, 599);
            this.aNavButton1.TabIndex = 28;
            // 
            // P_POS_TMP_ITEM
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("P_POS_TMP_ITEM.IconOptions.Icon")));
            this.Name = "P_POS_TMP_ITEM";
            this.Text = "판매등록 임시저장 리스트";
            this.panelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSale.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.SimpleButton btnDone;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private Bifrost.Win.Controls.aDateEdit dtpSale;
        private Bifrost.Grid.aGrid gridList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn No;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private Bifrost.Win.Controls.aControl.aNavButton aNavButton1;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
    }
}