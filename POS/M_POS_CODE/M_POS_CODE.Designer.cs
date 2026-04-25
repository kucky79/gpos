namespace POS
{
    partial class M_POS_CODE
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
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.aLookUpEditModule = new Bifrost.Win.Controls.aLookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridMain = new Bifrost.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridDtetail = new Bifrost.Grid.aGrid();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnD1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEditModule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDtetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.splitContainerControl1);
            this.pnlContainer.Controls.Add(this.panelTop);
            this.pnlContainer.Size = new System.Drawing.Size(1270, 855);
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTop.Controls.Add(this.aLookUpEditModule);
            this.panelTop.Controls.Add(this.labelControl1);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.labelControl3);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1270, 62);
            this.panelTop.TabIndex = 4;
            // 
            // aLookUpEditModule
            // 
            this.aLookUpEditModule.Location = new System.Drawing.Point(657, 14);
            this.aLookUpEditModule.Name = "aLookUpEditModule";
            this.aLookUpEditModule.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.aLookUpEditModule.Properties.Appearance.Options.UseFont = true;
            this.aLookUpEditModule.Properties.AppearanceDropDown.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.aLookUpEditModule.Properties.AppearanceDropDown.Options.UseFont = true;
            this.aLookUpEditModule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.aLookUpEditModule.Properties.NullText = "";
            this.aLookUpEditModule.Size = new System.Drawing.Size(149, 34);
            this.aLookUpEditModule.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(605, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(38, 28);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "모듈";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(113, 14);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F, System.Drawing.FontStyle.Bold);
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Size = new System.Drawing.Size(460, 34);
            this.txtSearch.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(30, 17);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 28);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "검색조건";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 62);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridMain);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridDtetail);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1270, 793);
            this.splitContainerControl1.SplitterPosition = 529;
            this.splitContainerControl1.TabIndex = 11;
            // 
            // gridMain
            // 
            this.gridMain.AddNewRowLastColumn = false;
            this.gridMain.disabledEditingColumns = null;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.isSaveLayout = false;
            this.gridMain.isUpper = false;
            this.gridMain.LayoutVersion = "";
            this.gridMain.Location = new System.Drawing.Point(0, 0);
            this.gridMain.MainView = this.gridView1;
            this.gridMain.Name = "gridMain";
            this.gridMain.SetBindingEvnet = true;
            this.gridMain.Size = new System.Drawing.Size(529, 793);
            this.gridMain.TabIndex = 13;
            this.gridMain.VerifyNotNull = null;
            this.gridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.GridControl = this.gridMain;
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "모듈";
            this.gridColumn1.FieldName = "CD_MODULE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 87;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "상위코드";
            this.gridColumn2.FieldName = "CD_CLAS";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 101;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "상위코드명";
            this.gridColumn3.FieldName = "NM_CLAS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 155;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "시스템 구분";
            this.gridColumn4.FieldName = "YN_SYSTEM";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 155;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "사용구분";
            this.gridColumn5.FieldName = "YN_USE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 81;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "비고";
            this.gridColumn6.FieldName = "DC_RMK";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 235;
            // 
            // gridDtetail
            // 
            this.gridDtetail.AddNewRowLastColumn = false;
            this.gridDtetail.disabledEditingColumns = null;
            this.gridDtetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDtetail.isSaveLayout = false;
            this.gridDtetail.isUpper = false;
            this.gridDtetail.LayoutVersion = "";
            this.gridDtetail.Location = new System.Drawing.Point(0, 0);
            this.gridDtetail.MainView = this.gridView2;
            this.gridDtetail.Name = "gridDtetail";
            this.gridDtetail.SetBindingEvnet = true;
            this.gridDtetail.Size = new System.Drawing.Size(731, 793);
            this.gridDtetail.TabIndex = 33;
            this.gridDtetail.VerifyNotNull = null;
            this.gridDtetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnD1,
            this.gridColumnD2,
            this.gridColumnD3,
            this.gridColumnD4,
            this.gridColumnD5,
            this.gridColumnD6,
            this.gridColumnD7,
            this.gridColumnD8,
            this.gridColumnD10});
            this.gridView2.GridControl = this.gridDtetail;
            this.gridView2.Name = "gridView2";
            // 
            // gridColumnD1
            // 
            this.gridColumnD1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD1.Caption = "상위코드";
            this.gridColumnD1.FieldName = "CD_CLAS";
            this.gridColumnD1.Name = "gridColumnD1";
            this.gridColumnD1.Width = 100;
            // 
            // gridColumnD2
            // 
            this.gridColumnD2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD2.Caption = "세부코드";
            this.gridColumnD2.FieldName = "CD_FLAG";
            this.gridColumnD2.Name = "gridColumnD2";
            this.gridColumnD2.Visible = true;
            this.gridColumnD2.VisibleIndex = 0;
            this.gridColumnD2.Width = 100;
            // 
            // gridColumnD3
            // 
            this.gridColumnD3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD3.Caption = "세부코드명";
            this.gridColumnD3.FieldName = "NM_FLAG";
            this.gridColumnD3.Name = "gridColumnD3";
            this.gridColumnD3.Visible = true;
            this.gridColumnD3.VisibleIndex = 1;
            this.gridColumnD3.Width = 100;
            // 
            // gridColumnD4
            // 
            this.gridColumnD4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD4.Caption = "시스템구분";
            this.gridColumnD4.FieldName = "YN_SYSTEM";
            this.gridColumnD4.Name = "gridColumnD4";
            this.gridColumnD4.Visible = true;
            this.gridColumnD4.VisibleIndex = 2;
            this.gridColumnD4.Width = 100;
            // 
            // gridColumnD5
            // 
            this.gridColumnD5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD5.Caption = "사용구분";
            this.gridColumnD5.FieldName = "YN_USE";
            this.gridColumnD5.Name = "gridColumnD5";
            this.gridColumnD5.Visible = true;
            this.gridColumnD5.VisibleIndex = 3;
            this.gridColumnD5.Width = 100;
            // 
            // gridColumnD6
            // 
            this.gridColumnD6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD6.Caption = "비고";
            this.gridColumnD6.FieldName = "DC_RMK";
            this.gridColumnD6.Name = "gridColumnD6";
            this.gridColumnD6.Visible = true;
            this.gridColumnD6.VisibleIndex = 5;
            this.gridColumnD6.Width = 100;
            // 
            // gridColumnD7
            // 
            this.gridColumnD7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD7.Caption = "상위참조코드";
            this.gridColumnD7.FieldName = "CD_CLAS_REF";
            this.gridColumnD7.Name = "gridColumnD7";
            this.gridColumnD7.Visible = true;
            this.gridColumnD7.VisibleIndex = 6;
            this.gridColumnD7.Width = 100;
            // 
            // gridColumnD8
            // 
            this.gridColumnD8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD8.Caption = "세부참조코드";
            this.gridColumnD8.FieldName = "CD_FLAG_REF";
            this.gridColumnD8.Name = "gridColumnD8";
            this.gridColumnD8.Visible = true;
            this.gridColumnD8.VisibleIndex = 7;
            this.gridColumnD8.Width = 100;
            // 
            // gridColumnD10
            // 
            this.gridColumnD10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD10.Caption = "순서";
            this.gridColumnD10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnD10.FieldName = "NO_SEQ";
            this.gridColumnD10.Name = "gridColumnD10";
            this.gridColumnD10.Visible = true;
            this.gridColumnD10.VisibleIndex = 4;
            this.gridColumnD10.Width = 40;
            // 
            // gridColumnD9
            // 
            this.gridColumnD9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD9.Caption = "Ref. Remark";
            this.gridColumnD9.FieldName = "DC_REF1";
            this.gridColumnD9.Name = "gridColumnD9";
            this.gridColumnD9.Visible = true;
            this.gridColumnD9.VisibleIndex = 8;
            this.gridColumnD9.Width = 100;
            // 
            // M_POS_CODE
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 895);
            this.Name = "M_POS_CODE";
            this.SubTitle = "Form1";
            this.Text = "Form1";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEditModule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDtetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Bifrost.Grid.aGrid gridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        
        private Bifrost.Grid.aGrid gridDtetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;

        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD10;
        private Bifrost.Win.Controls.aLookUpEdit aLookUpEditModule;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}

