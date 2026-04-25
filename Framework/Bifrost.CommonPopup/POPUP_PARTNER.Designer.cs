namespace NF.A2P.CommonPopup
{
    partial class POPUP_PARTNER
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_PARTNER));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.txtSearch = new NF.Framework.Win.Controls.aTextEdit();
            this._GridM = new NF.A2P.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_CdPartner = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_NmPartner = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_DcAddressBL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_TelNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_TpPartner = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_AccountID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.aPanel1 = new NF.Framework.Win.Controls.aPanel();
            this.aLookUpEdit_PartnerType = new NF.Framework.Win.Controls.aLookUpEdit();
            this.aLabel2 = new NF.Framework.Win.Controls.aLabel();
            this.aLabel1 = new NF.Framework.Win.Controls.aLabel();
            this.pnlBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._GridM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).BeginInit();
            this.aPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEdit_PartnerType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.tableLayoutPanel1);
            this.pnlBound.Size = new System.Drawing.Size(803, 424);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 512);
            this.pnlBottom.Size = new System.Drawing.Size(843, 37);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(87, 9);
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
            this.txtSearch.Size = new System.Drawing.Size(355, 24);
            this.txtSearch.TabIndex = 1;
            // 
            // _GridM
            // 
            this._GridM.AddNewRowLastColumn = true;
            this._GridM.disabledEditingColumns = new string[0];
            this._GridM.Dock = System.Windows.Forms.DockStyle.Fill;
            this._GridM.ImeMode = System.Windows.Forms.ImeMode.Off;
            this._GridM.isSaveLayout = false;
            this._GridM.isUpper = false;
            this._GridM.LayoutVersion = "";
            this._GridM.Location = new System.Drawing.Point(3, 48);
            this._GridM.MainView = this.gridView1;
            this._GridM.Name = "_GridM";
            this._GridM.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this._GridM.SetBindingEvnet = true;
            this._GridM.Size = new System.Drawing.Size(797, 373);
            this._GridM.TabIndex = 2;
            this._GridM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_CdPartner,
            this.gridColumn_NmPartner,
            this.gridColumn_DcAddressBL,
            this.gridColumn_TelNo,
            this.gridColumn_TpPartner,
            this.gridColumn_AccountID});
            this.gridView1.GridControl = this._GridM;
            this.gridView1.IndicatorWidth = 29;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn_CdPartner
            // 
            this.gridColumn_CdPartner.Name = "gridColumn_CdPartner";
            // 
            // gridColumn_NmPartner
            // 
            this.gridColumn_NmPartner.Name = "gridColumn_NmPartner";
            // 
            // gridColumn_DcAddressBL
            // 
            this.gridColumn_DcAddressBL.Name = "gridColumn_DcAddressBL";
            // 
            // gridColumn_TelNo
            // 
            this.gridColumn_TelNo.Name = "gridColumn_TelNo";
            // 
            // gridColumn_TpPartner
            // 
            this.gridColumn_TpPartner.FieldName = "TP_PARTNER";
            this.gridColumn_TpPartner.Name = "gridColumn_TpPartner";
            // 
            // gridColumn_AccountID
            // 
            this.gridColumn_AccountID.Name = "gridColumn_AccountID";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._GridM, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.aPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(803, 424);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // aPanel1
            // 
            this.aPanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel1.Appearance.Options.UseBackColor = true;
            this.aPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.aPanel1.Controls.Add(this.aLookUpEdit_PartnerType);
            this.aPanel1.Controls.Add(this.aLabel2);
            this.aPanel1.Controls.Add(this.aLabel1);
            this.aPanel1.Controls.Add(this.txtSearch);
            this.aPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.aPanel1.Location = new System.Drawing.Point(3, 3);
            this.aPanel1.Name = "aPanel1";
            this.aPanel1.SetPanelType = NF.Framework.Win.Controls.aPanel.PanelType.NONE;
            this.aPanel1.Size = new System.Drawing.Size(797, 39);
            this.aPanel1.TabIndex = 0;
            // 
            // aLookUpEdit_PartnerType
            // 
            this.aLookUpEdit_PartnerType.Location = new System.Drawing.Point(531, 9);
            this.aLookUpEdit_PartnerType.Name = "aLookUpEdit_PartnerType";
            this.aLookUpEdit_PartnerType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.aLookUpEdit_PartnerType.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aLookUpEdit_PartnerType.Properties.Appearance.Options.UseBackColor = true;
            this.aLookUpEdit_PartnerType.Properties.Appearance.Options.UseBorderColor = true;
            this.aLookUpEdit_PartnerType.Properties.AutoHeight = false;
            this.aLookUpEdit_PartnerType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            serializableAppearanceObject1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            serializableAppearanceObject1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            serializableAppearanceObject1.Options.UseBackColor = true;
            this.aLookUpEdit_PartnerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1)});
            this.aLookUpEdit_PartnerType.Properties.NullText = "";
            this.aLookUpEdit_PartnerType.Properties.PopupFormMinSize = new System.Drawing.Size(50, 50);
            this.aLookUpEdit_PartnerType.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize;
            this.aLookUpEdit_PartnerType.Properties.ShowHeader = false;
            this.aLookUpEdit_PartnerType.Properties.ShowLines = false;
            this.aLookUpEdit_PartnerType.Properties.ShowNullValuePromptWhenFocused = true;
            this.aLookUpEdit_PartnerType.Size = new System.Drawing.Size(147, 24);
            this.aLookUpEdit_PartnerType.TabIndex = 326;
            this.aLookUpEdit_PartnerType.Tag = "TP_PARTNER";
            // 
            // aLabel2
            // 
            this.aLabel2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.aLabel2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel2.Appearance.Options.UseFont = true;
            this.aLabel2.Appearance.Options.UseForeColor = true;
            this.aLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel2.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel2.Location = new System.Drawing.Point(448, 9);
            this.aLabel2.Name = "aLabel2";
            this.aLabel2.Size = new System.Drawing.Size(77, 24);
            this.aLabel2.TabIndex = 2;
            this.aLabel2.Text = "Partner Type";
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.Appearance.Options.UseFont = true;
            this.aLabel1.Appearance.Options.UseForeColor = true;
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(16, 9);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(65, 24);
            this.aLabel1.TabIndex = 0;
            this.aLabel1.Text = "Eng. Name";
            // 
            // POPUP_PARTNER
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 550);
            this.Name = "POPUP_PARTNER";
            this.Text = "POPUP_PARTNER";
            this.pnlBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._GridM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).EndInit();
            this.aPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aLookUpEdit_PartnerType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private NF.Framework.Win.Controls.aTextEdit txtSearch;
        private A2P.Grid.aGrid _GridM;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_CdPartner;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_NmPartner;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_DcAddressBL;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_TelNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_AccountID;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NF.Framework.Win.Controls.aPanel aPanel1;
        private NF.Framework.Win.Controls.aLabel aLabel1;
        private NF.Framework.Win.Controls.aLabel aLabel2;
        private NF.Framework.Win.Controls.aLookUpEdit aLookUpEdit_PartnerType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_TpPartner;
    }
}

