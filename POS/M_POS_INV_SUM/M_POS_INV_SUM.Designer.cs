namespace POS
{
    partial class M_POS_INV_SUM
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblMessage = new DevExpress.XtraEditors.LabelControl();
            this.btnInvSum = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpSearch = new Bifrost.Win.Controls.aDateEdit();
            this.gridMain = new Bifrost.Grid.aGrid();
            this.gridViewItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.gridMain);
            this.pnlContainer.Controls.Add(this.panelControl1);
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Size = new System.Drawing.Size(1027, 584);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblMessage);
            this.panelControl1.Controls.Add(this.btnInvSum);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dtpSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1027, 58);
            this.panelControl1.TabIndex = 4;
            // 
            // lblMessage
            // 
            this.lblMessage.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 13F);
            this.lblMessage.Appearance.Options.UseFont = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 23);
            this.lblMessage.TabIndex = 9;
            // 
            // btnInvSum
            // 
            this.btnInvSum.AllowFocus = false;
            this.btnInvSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInvSum.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnInvSum.Appearance.Options.UseFont = true;
            this.btnInvSum.Location = new System.Drawing.Point(871, 6);
            this.btnInvSum.Name = "btnInvSum";
            this.btnInvSum.Size = new System.Drawing.Size(144, 44);
            this.btnInvSum.TabIndex = 5;
            this.btnInvSum.Text = "재고 마감";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(659, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 28);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "마감일";
            this.labelControl2.Visible = false;
            // 
            // dtpSearch
            // 
            this.dtpSearch.DateFormat = "yyyy\\-MM\\-dd";
            this.dtpSearch.EditValue = "2019-12-01";
            this.dtpSearch.Enabled = false;
            this.dtpSearch.Location = new System.Drawing.Point(722, 15);
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
            this.dtpSearch.Properties.DisplayFormat.FormatString = "yyyy\\-MM\\-dd";
            this.dtpSearch.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpSearch.Properties.EditFormat.FormatString = "yyyy\\-MM\\-dd";
            this.dtpSearch.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpSearch.Properties.Mask.EditMask = "yyyy\\-MM\\-dd";
            this.dtpSearch.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtpSearch.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtpSearch.Properties.NullDate = new System.DateTime(((long)(0)));
            this.dtpSearch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtpSearch.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtpSearch.Size = new System.Drawing.Size(141, 30);
            this.dtpSearch.TabIndex = 6;
            this.dtpSearch.Visible = false;
            // 
            // gridMain
            // 
            this.gridMain.AddNewRowLastColumn = false;
            this.gridMain.disabledEditingColumns = null;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.isSaveLayout = false;
            this.gridMain.isUpper = false;
            this.gridMain.LayoutVersion = "";
            this.gridMain.Location = new System.Drawing.Point(0, 58);
            this.gridMain.MainView = this.gridViewItem;
            this.gridMain.Name = "gridMain";
            this.gridMain.SetBindingEvnet = true;
            this.gridMain.Size = new System.Drawing.Size(1027, 526);
            this.gridMain.TabIndex = 10;
            this.gridMain.VerifyNotNull = null;
            this.gridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItem});
            // 
            // gridViewItem
            // 
            this.gridViewItem.Appearance.FooterPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewItem.Appearance.FooterPanel.Options.UseFont = true;
            this.gridViewItem.Appearance.HeaderPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewItem.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewItem.Appearance.Row.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewItem.Appearance.Row.Options.UseFont = true;
            this.gridViewItem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridViewItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.No,
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn3});
            this.gridViewItem.DetailHeight = 305;
            this.gridViewItem.GridControl = this.gridMain;
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
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "상품명";
            this.gridColumn2.FieldName = "NM_ITEM";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 158;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "기초재고량";
            this.gridColumn6.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "QT_OPEN";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "입고량";
            this.gridColumn5.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "QT_RCP";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "출고량";
            this.gridColumn1.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "QT_ISU";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "현재고";
            this.gridColumn4.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "QT_CLS";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "단위";
            this.gridColumn3.FieldName = "NM_UNIT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 5;
            // 
            // M_POS_INV_SUM
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 644);
            this.IsBottomVisible = true;
            this.Name = "M_POS_INV_SUM";
            this.SubTitle = "상품별판매조회";
            this.Text = "상품별판매조회";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Bifrost.Win.Controls.aDateEdit dtpSearch;
        private Bifrost.Grid.aGrid gridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItem;
        private DevExpress.XtraGrid.Columns.GridColumn No;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SimpleButton btnInvSum;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.LabelControl lblMessage;
    }
}

