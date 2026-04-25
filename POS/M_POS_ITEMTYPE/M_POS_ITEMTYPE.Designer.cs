namespace POS
{
    partial class M_POS_ITEMTYPE
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
            this.gridColumnD9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridMain = new Bifrost.Grid.aGrid();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnD6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnD8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnD10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.gridMain);
            this.pnlContainer.Size = new System.Drawing.Size(1270, 795);
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
            // gridMain
            // 
            this.gridMain.AddNewRowLastColumn = false;
            this.gridMain.disabledEditingColumns = null;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gridMain.isSaveLayout = false;
            this.gridMain.isUpper = false;
            this.gridMain.LayoutVersion = "";
            this.gridMain.Location = new System.Drawing.Point(0, 0);
            this.gridMain.MainView = this.gridViewMain;
            this.gridMain.Name = "gridMain";
            this.gridMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridMain.SetBindingEvnet = true;
            this.gridMain.Size = new System.Drawing.Size(1270, 795);
            this.gridMain.TabIndex = 4;
            this.gridMain.VerifyNotNull = null;
            this.gridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            // 
            // gridViewMain
            // 
            this.gridViewMain.Appearance.HeaderPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewMain.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewMain.Appearance.Row.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridViewMain.Appearance.Row.Options.UseFont = true;
            this.gridViewMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridViewMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumnD1,
            this.gridColumnD2,
            this.gridColumnD3,
            this.gridColumnD4,
            this.gridColumnD5,
            this.gridColumnD6,
            this.gridColumnD7,
            this.gridColumnD8,
            this.gridColumnD10});
            this.gridViewMain.GridControl = this.gridMain;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "ST_ROW";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumnD1
            // 
            this.gridColumnD1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD1.Caption = "Classification";
            this.gridColumnD1.FieldName = "CD_CLAS";
            this.gridColumnD1.Name = "gridColumnD1";
            this.gridColumnD1.Width = 100;
            // 
            // gridColumnD2
            // 
            this.gridColumnD2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD2.Caption = "대분류코드";
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
            this.gridColumnD3.Caption = "대분류명";
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
            this.gridColumnD4.Caption = "System Y/N";
            this.gridColumnD4.FieldName = "YN_SYSTEM";
            this.gridColumnD4.Name = "gridColumnD4";
            this.gridColumnD4.Width = 100;
            // 
            // gridColumnD5
            // 
            this.gridColumnD5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD5.Caption = "사용여부";
            this.gridColumnD5.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumnD5.FieldName = "YN_USE";
            this.gridColumnD5.Name = "gridColumnD5";
            this.gridColumnD5.Visible = true;
            this.gridColumnD5.VisibleIndex = 2;
            this.gridColumnD5.Width = 100;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = "Y";
            this.repositoryItemCheckEdit1.ValueGrayed = "N";
            this.repositoryItemCheckEdit1.ValueUnchecked = "N";
            // 
            // gridColumnD6
            // 
            this.gridColumnD6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD6.Caption = "Remark";
            this.gridColumnD6.FieldName = "DC_RMK";
            this.gridColumnD6.Name = "gridColumnD6";
            this.gridColumnD6.Width = 100;
            // 
            // gridColumnD7
            // 
            this.gridColumnD7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD7.Caption = "대분류 고정";
            this.gridColumnD7.ColumnEdit = this.repositoryItemCheckEdit2;
            this.gridColumnD7.FieldName = "CD_CLAS_REF";
            this.gridColumnD7.Name = "gridColumnD7";
            this.gridColumnD7.Visible = true;
            this.gridColumnD7.VisibleIndex = 3;
            this.gridColumnD7.Width = 100;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = "Y";
            this.repositoryItemCheckEdit2.ValueGrayed = "N";
            this.repositoryItemCheckEdit2.ValueUnchecked = "N";
            // 
            // gridColumnD8
            // 
            this.gridColumnD8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD8.Caption = "Ref. Sub Code";
            this.gridColumnD8.FieldName = "CD_FLAG_REF";
            this.gridColumnD8.Name = "gridColumnD8";
            this.gridColumnD8.Width = 100;
            // 
            // gridColumnD10
            // 
            this.gridColumnD10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnD10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnD10.Caption = "SEQ";
            this.gridColumnD10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnD10.FieldName = "NO_SEQ";
            this.gridColumnD10.Name = "gridColumnD10";
            this.gridColumnD10.Width = 40;
            // 
            // M_POS_ITEMTYPE
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 895);
            this.IsBottomVisible = true;
            this.Name = "M_POS_ITEMTYPE";
            this.SubTitle = "Form1";
            this.Text = "Form1";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD9;
        private Bifrost.Grid.aGrid gridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnD10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
    }
}

