namespace POS
{
    partial class M_POS_INV_SEARCH
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpSearch = new Bifrost.Win.Controls.aDateEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.gridMain = new Bifrost.Grid.aGrid();
            this.gridViewItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridDetail = new Bifrost.Grid.aGrid();
            this.gridViewDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblItem = new DevExpress.XtraEditors.LabelControl();
            this.lblUnit4 = new DevExpress.XtraEditors.LabelControl();
            this.lblUnit3 = new DevExpress.XtraEditors.LabelControl();
            this.lblUnit2 = new DevExpress.XtraEditors.LabelControl();
            this.lblUnit1 = new DevExpress.XtraEditors.LabelControl();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.splitContainerControl1);
            this.pnlContainer.Controls.Add(this.groupControl1);
            this.pnlContainer.Controls.Add(this.panelControl1);
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContainer.Size = new System.Drawing.Size(1317, 854);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dtpSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1317, 45);
            this.panelControl1.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 28);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "조회일";
            // 
            // dtpSearch
            // 
            this.dtpSearch.DateFormat = "yyyy\\-MM\\-dd";
            this.dtpSearch.EditValue = "2019-12-01";
            this.dtpSearch.Enabled = false;
            this.dtpSearch.Location = new System.Drawing.Point(72, 7);
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
            this.dtpSearch.TabIndex = 5;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 45);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl3);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1317, 666);
            this.splitContainerControl1.SplitterPosition = 684;
            this.splitContainerControl1.TabIndex = 8;
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl3.Controls.Add(this.gridMain);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(684, 666);
            this.groupControl3.TabIndex = 10;
            this.groupControl3.Text = "상품별 재고";
            // 
            // gridMain
            // 
            this.gridMain.AddNewRowLastColumn = false;
            this.gridMain.disabledEditingColumns = null;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.isSaveLayout = false;
            this.gridMain.isUpper = false;
            this.gridMain.LayoutVersion = "";
            this.gridMain.Location = new System.Drawing.Point(2, 35);
            this.gridMain.MainView = this.gridViewItem;
            this.gridMain.Name = "gridMain";
            this.gridMain.SetBindingEvnet = true;
            this.gridMain.Size = new System.Drawing.Size(680, 629);
            this.gridMain.TabIndex = 11;
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
            this.gridColumn4,
            this.gridColumn3});
            this.gridViewItem.DetailHeight = 305;
            this.gridViewItem.GridControl = this.gridMain;
            this.gridViewItem.Name = "gridViewItem";
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
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "현재고";
            this.gridColumn4.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "QT_INV";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "주단위";
            this.gridColumn3.FieldName = "NM_UNIT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.gridDetail);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(628, 666);
            this.groupControl2.TabIndex = 31;
            this.groupControl2.Text = "상세";
            // 
            // gridDetail
            // 
            this.gridDetail.AddNewRowLastColumn = false;
            this.gridDetail.disabledEditingColumns = null;
            this.gridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetail.isSaveLayout = false;
            this.gridDetail.isUpper = false;
            this.gridDetail.LayoutVersion = "";
            this.gridDetail.Location = new System.Drawing.Point(2, 35);
            this.gridDetail.MainView = this.gridViewDetail;
            this.gridDetail.Name = "gridDetail";
            this.gridDetail.SetBindingEvnet = true;
            this.gridDetail.Size = new System.Drawing.Size(624, 629);
            this.gridDetail.TabIndex = 32;
            this.gridDetail.VerifyNotNull = null;
            this.gridDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetail});
            // 
            // gridViewDetail
            // 
            this.gridViewDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.gridViewDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewDetail.Appearance.Row.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewDetail.Appearance.Row.Options.UseFont = true;
            this.gridViewDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridViewDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridViewDetail.DetailHeight = 305;
            this.gridViewDetail.GridControl = this.gridDetail;
            this.gridViewDetail.Name = "gridViewDetail";
            this.gridViewDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewDetail.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "No";
            this.gridColumn1.FieldName = "NO_LINE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "상품명";
            this.gridColumn5.FieldName = "NM_ITEM";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Width = 158;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "현재고";
            this.gridColumn6.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "QT_INV";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "단위";
            this.gridColumn7.FieldName = "NM_UNIT";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.lblItem);
            this.groupControl1.Controls.Add(this.lblUnit4);
            this.groupControl1.Controls.Add(this.lblUnit3);
            this.groupControl1.Controls.Add(this.lblUnit2);
            this.groupControl1.Controls.Add(this.lblUnit1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 711);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1317, 143);
            this.groupControl1.TabIndex = 51;
            this.groupControl1.Text = "단위 환산 재고량";
            // 
            // lblItem
            // 
            this.lblItem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblItem.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblItem.Appearance.Options.UseFont = true;
            this.lblItem.Location = new System.Drawing.Point(60, 48);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(0, 28);
            this.lblItem.TabIndex = 52;
            // 
            // lblUnit4
            // 
            this.lblUnit4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUnit4.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblUnit4.Appearance.Options.UseFont = true;
            this.lblUnit4.Location = new System.Drawing.Point(1137, 103);
            this.lblUnit4.Name = "lblUnit4";
            this.lblUnit4.Size = new System.Drawing.Size(0, 28);
            this.lblUnit4.TabIndex = 56;
            // 
            // lblUnit3
            // 
            this.lblUnit3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUnit3.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblUnit3.Appearance.Options.UseFont = true;
            this.lblUnit3.Location = new System.Drawing.Point(778, 103);
            this.lblUnit3.Name = "lblUnit3";
            this.lblUnit3.Size = new System.Drawing.Size(0, 28);
            this.lblUnit3.TabIndex = 55;
            // 
            // lblUnit2
            // 
            this.lblUnit2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUnit2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblUnit2.Appearance.Options.UseFont = true;
            this.lblUnit2.Location = new System.Drawing.Point(419, 103);
            this.lblUnit2.Name = "lblUnit2";
            this.lblUnit2.Size = new System.Drawing.Size(0, 28);
            this.lblUnit2.TabIndex = 54;
            // 
            // lblUnit1
            // 
            this.lblUnit1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUnit1.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblUnit1.Appearance.Options.UseFont = true;
            this.lblUnit1.Location = new System.Drawing.Point(60, 103);
            this.lblUnit1.Name = "lblUnit1";
            this.lblUnit1.Size = new System.Drawing.Size(0, 28);
            this.lblUnit1.TabIndex = 53;
            // 
            // M_POS_INV_SEARCH
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 914);
            this.IsBottomVisible = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "M_POS_INV_SEARCH";
            this.SubTitle = "상품별판매조회";
            this.Text = "상품별판매조회";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Bifrost.Win.Controls.aDateEdit dtpSearch;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private Bifrost.Grid.aGrid gridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItem;
        private DevExpress.XtraGrid.Columns.GridColumn No;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private Bifrost.Grid.aGrid gridDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.LabelControl lblUnit4;
        private DevExpress.XtraEditors.LabelControl lblUnit3;
        private DevExpress.XtraEditors.LabelControl lblUnit2;
        private DevExpress.XtraEditors.LabelControl lblUnit1;
        private DevExpress.XtraEditors.LabelControl lblItem;
    }
}

