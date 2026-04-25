namespace POS
{
    partial class M_POS_SO_SEARCH01
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M_POS_SO_SEARCH01));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.aLookUpEditCust = new Bifrost.Win.Controls.aLookUpEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnCust = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpSearch = new Bifrost.Win.Controls.aDateEdit();
            this.gridViewItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridMain = new Bifrost.Grid.aGrid();
            this.aNavButton1 = new Bifrost.Win.Controls.aControl.aNavButton();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEditCust.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.gridMain);
            this.pnlContainer.Controls.Add(this.aNavButton1);
            this.pnlContainer.Controls.Add(this.panelControl1);
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Size = new System.Drawing.Size(1027, 584);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.aLookUpEditCust);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnCust);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dtpSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1027, 85);
            this.panelControl1.TabIndex = 4;
            // 
            // aLookUpEditCust
            // 
            this.aLookUpEditCust.Location = new System.Drawing.Point(343, 17);
            this.aLookUpEditCust.Name = "aLookUpEditCust";
            this.aLookUpEditCust.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.aLookUpEditCust.Properties.Appearance.Options.UseFont = true;
            this.aLookUpEditCust.Properties.AutoHeight = false;
            this.aLookUpEditCust.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.aLookUpEditCust.Properties.NullText = "";
            this.aLookUpEditCust.Size = new System.Drawing.Size(148, 50);
            this.aLookUpEditCust.TabIndex = 7;
            this.aLookUpEditCust.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.AllowFocus = false;
            this.btnClear.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Location = new System.Drawing.Point(603, 17);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 50);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "초기화";
            this.btnClear.Visible = false;
            // 
            // btnCust
            // 
            this.btnCust.AllowFocus = false;
            this.btnCust.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnCust.Appearance.Options.UseFont = true;
            this.btnCust.Location = new System.Drawing.Point(497, 17);
            this.btnCust.Name = "btnCust";
            this.btnCust.Size = new System.Drawing.Size(100, 50);
            this.btnCust.TabIndex = 9;
            this.btnCust.Text = "찾기";
            this.btnCust.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(283, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 28);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "거래처";
            this.labelControl1.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(14, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 28);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "조회월";
            // 
            // dtpSearch
            // 
            this.dtpSearch.DateFormat = "yyyy\\-MM";
            this.dtpSearch.EditValue = "2019-12";
            this.dtpSearch.Location = new System.Drawing.Point(77, 17);
            this.dtpSearch.Name = "dtpSearch";
            this.dtpSearch.Properties.AllowFocused = false;
            this.dtpSearch.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtpSearch.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.dtpSearch.Properties.Appearance.Options.UseFont = true;
            this.dtpSearch.Properties.AutoHeight = false;
            this.dtpSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dtpSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpSearch.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpSearch.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent;
            this.dtpSearch.Properties.CellSize = new System.Drawing.Size(50, 50);
            this.dtpSearch.Properties.DisplayFormat.FormatString = "yyyy\\-MM";
            this.dtpSearch.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpSearch.Properties.EditFormat.FormatString = "yyyy\\-MM";
            this.dtpSearch.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpSearch.Properties.Mask.EditMask = "yyyy\\-MM";
            this.dtpSearch.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtpSearch.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtpSearch.Properties.NullDate = new System.DateTime(((long)(0)));
            this.dtpSearch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtpSearch.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dtpSearch.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dtpSearch.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtpSearch.Size = new System.Drawing.Size(170, 50);
            this.dtpSearch.TabIndex = 5;
            // 
            // gridViewItem
            // 
            this.gridViewItem.Appearance.FooterPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewItem.Appearance.FooterPanel.Options.UseFont = true;
            this.gridViewItem.Appearance.HeaderPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewItem.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewItem.Appearance.Row.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewItem.Appearance.Row.Options.UseFont = true;
            this.gridViewItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.No,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn7,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn8});
            this.gridViewItem.DetailHeight = 305;
            this.gridViewItem.GridControl = this.gridMain;
            this.gridViewItem.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AM_TOT", this.gridColumn7, "(총외상금 합계 : {0:##,##0})")});
            this.gridViewItem.Name = "gridViewItem";
            this.gridViewItem.OptionsView.ShowFooter = true;
            this.gridViewItem.OptionsView.ShowGroupPanel = false;
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
            this.gridColumn1.Caption = "거래처코드";
            this.gridColumn1.FieldName = "CD_CUST";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "거래처";
            this.gridColumn2.FieldName = "NM_CUST";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 292;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "매출액";
            this.gridColumn7.DisplayFormat.FormatString = "##,##0";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "AM_TOT";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 221;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "신용카드";
            this.gridColumn3.DisplayFormat.FormatString = "##,##0";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "AM_CREDIT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 155;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "(건수)";
            this.gridColumn4.DisplayFormat.FormatString = "##,###";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "QT_CREDIT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 88;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "매출액 - 카드금액";
            this.gridColumn5.DisplayFormat.FormatString = "##,###";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "AM_CREDIT_DIFF";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 214;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "부가세상품금액";
            this.gridColumn6.DisplayFormat.FormatString = "##,###";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "AM_VAT_ITEM";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 202;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "매출액 - 부가세상품금액";
            this.gridColumn8.DisplayFormat.FormatString = "##,###";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "AM_VAT_DIFF";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 231;
            // 
            // gridMain
            // 
            this.gridMain.AddNewRowLastColumn = false;
            this.gridMain.disabledEditingColumns = null;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.isSaveLayout = false;
            this.gridMain.isUpper = false;
            this.gridMain.LayoutVersion = "";
            this.gridMain.Location = new System.Drawing.Point(0, 85);
            this.gridMain.MainView = this.gridViewItem;
            this.gridMain.Name = "gridMain";
            this.gridMain.SetBindingEvnet = true;
            this.gridMain.Size = new System.Drawing.Size(977, 499);
            this.gridMain.TabIndex = 13;
            this.gridMain.VerifyNotNull = null;
            this.gridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItem});
            // 
            // aNavButton1
            // 
            this.aNavButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.aNavButton1.Location = new System.Drawing.Point(977, 85);
            this.aNavButton1.Name = "aNavButton1";
            this.aNavButton1.NavGrid = this.gridMain;
            this.aNavButton1.Size = new System.Drawing.Size(50, 499);
            this.aNavButton1.TabIndex = 32;
            // 
            // M_POS_SO_SEARCH01
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 644);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("M_POS_SO_SEARCH01.IconOptions.Icon")));
            this.IsBottomVisible = true;
            this.Name = "M_POS_SO_SEARCH01";
            this.SubTitle = "월별매출현황";
            this.Text = "월별매출현황";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEditCust.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItem;
        private DevExpress.XtraGrid.Columns.GridColumn No;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private Bifrost.Grid.aGrid gridMain;
        private Bifrost.Win.Controls.aControl.aNavButton aNavButton1;
        private Bifrost.Win.Controls.aLookUpEdit aLookUpEditCust;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnCust;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Bifrost.Win.Controls.aDateEdit dtpSearch;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}

