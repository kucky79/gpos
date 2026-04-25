namespace POS
{
    partial class M_POS_INV_OPEN
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridMain = new Bifrost.Grid.aGrid();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelDetail = new DevExpress.XtraEditors.PanelControl();
            this.panelKeypadItem = new DevExpress.XtraEditors.PanelControl();
            this.separatorControlKeypad1 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControlKeypad2 = new DevExpress.XtraEditors.SeparatorControl();
            this.txtPadQty = new DevExpress.XtraEditors.TextEdit();
            this.txtItemDescrip = new DevExpress.XtraEditors.LabelControl();
            this.txtPadItem = new DevExpress.XtraEditors.LabelControl();
            this.btnPadPrice = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadDelItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadInit = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadMinus = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadEA = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadItemUnit4 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadItemUnit3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadItemUnit2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadItemUnit1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadPoint = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadBackSpace = new DevExpress.XtraEditors.SimpleButton();
            this.btnPadClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad000 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad00 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad0 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad6 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad5 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad4 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad9 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad8 = new DevExpress.XtraEditors.SimpleButton();
            this.btnPad7 = new DevExpress.XtraEditors.SimpleButton();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).BeginInit();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelKeypadItem)).BeginInit();
            this.panelKeypadItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControlKeypad1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControlKeypad2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPadQty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.splitContainerControl1);
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Size = new System.Drawing.Size(1270, 919);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.No,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn7,
            this.gridColumn4});
            this.gridView1.DetailHeight = 305;
            this.gridView1.GridControl = this.gridMain;
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
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "재고수량";
            this.gridColumn3.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "QT_OPEN";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "단위";
            this.gridColumn8.FieldName = "CD_UNIT";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "단위";
            this.gridColumn9.FieldName = "NM_UNIT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "단가";
            this.gridColumn7.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "UM_OPEN";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "금액";
            this.gridColumn4.DisplayFormat.FormatString = "##,##0.####";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "AM_OPEN";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
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
            this.gridMain.Size = new System.Drawing.Size(659, 919);
            this.gridMain.TabIndex = 6;
            this.gridMain.VerifyNotNull = null;
            this.gridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridMain);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelDetail);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1270, 919);
            this.splitContainerControl1.SplitterPosition = 606;
            this.splitContainerControl1.TabIndex = 4;
            // 
            // panelDetail
            // 
            this.panelDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelDetail.Controls.Add(this.panelKeypadItem);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(0, 0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(606, 919);
            this.panelDetail.TabIndex = 26;
            // 
            // panelKeypadItem
            // 
            this.panelKeypadItem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelKeypadItem.Controls.Add(this.separatorControlKeypad1);
            this.panelKeypadItem.Controls.Add(this.separatorControlKeypad2);
            this.panelKeypadItem.Controls.Add(this.txtPadQty);
            this.panelKeypadItem.Controls.Add(this.txtItemDescrip);
            this.panelKeypadItem.Controls.Add(this.txtPadItem);
            this.panelKeypadItem.Controls.Add(this.btnPadPrice);
            this.panelKeypadItem.Controls.Add(this.btnPadCancel);
            this.panelKeypadItem.Controls.Add(this.btnPadDelItem);
            this.panelKeypadItem.Controls.Add(this.btnPadInit);
            this.panelKeypadItem.Controls.Add(this.btnPadMinus);
            this.panelKeypadItem.Controls.Add(this.btnPadEA);
            this.panelKeypadItem.Controls.Add(this.btnPadConfirm);
            this.panelKeypadItem.Controls.Add(this.btnPadItemUnit4);
            this.panelKeypadItem.Controls.Add(this.btnPadItemUnit3);
            this.panelKeypadItem.Controls.Add(this.btnPadItemUnit2);
            this.panelKeypadItem.Controls.Add(this.btnPadItemUnit1);
            this.panelKeypadItem.Controls.Add(this.btnPadPoint);
            this.panelKeypadItem.Controls.Add(this.btnPadBackSpace);
            this.panelKeypadItem.Controls.Add(this.btnPadClear);
            this.panelKeypadItem.Controls.Add(this.btnPad000);
            this.panelKeypadItem.Controls.Add(this.btnPad00);
            this.panelKeypadItem.Controls.Add(this.btnPad0);
            this.panelKeypadItem.Controls.Add(this.btnPad3);
            this.panelKeypadItem.Controls.Add(this.btnPad2);
            this.panelKeypadItem.Controls.Add(this.btnPad1);
            this.panelKeypadItem.Controls.Add(this.btnPad6);
            this.panelKeypadItem.Controls.Add(this.btnPad5);
            this.panelKeypadItem.Controls.Add(this.btnPad4);
            this.panelKeypadItem.Controls.Add(this.btnPad9);
            this.panelKeypadItem.Controls.Add(this.btnPad8);
            this.panelKeypadItem.Controls.Add(this.btnPad7);
            this.panelKeypadItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKeypadItem.Location = new System.Drawing.Point(0, 0);
            this.panelKeypadItem.Name = "panelKeypadItem";
            this.panelKeypadItem.Size = new System.Drawing.Size(606, 919);
            this.panelKeypadItem.TabIndex = 27;
            // 
            // separatorControlKeypad1
            // 
            this.separatorControlKeypad1.AutoSizeMode = true;
            this.separatorControlKeypad1.BackColor = System.Drawing.Color.Transparent;
            this.separatorControlKeypad1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(84)))), ((int)(((byte)(151)))));
            this.separatorControlKeypad1.LineThickness = 2;
            this.separatorControlKeypad1.Location = new System.Drawing.Point(47, 124);
            this.separatorControlKeypad1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.separatorControlKeypad1.Margin = new System.Windows.Forms.Padding(0);
            this.separatorControlKeypad1.Name = "separatorControlKeypad1";
            this.separatorControlKeypad1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.separatorControlKeypad1.Size = new System.Drawing.Size(504, 4);
            this.separatorControlKeypad1.TabIndex = 29;
            // 
            // separatorControlKeypad2
            // 
            this.separatorControlKeypad2.AutoSizeMode = true;
            this.separatorControlKeypad2.BackColor = System.Drawing.Color.Transparent;
            this.separatorControlKeypad2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(84)))), ((int)(((byte)(151)))));
            this.separatorControlKeypad2.LineThickness = 2;
            this.separatorControlKeypad2.Location = new System.Drawing.Point(47, 270);
            this.separatorControlKeypad2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.separatorControlKeypad2.Margin = new System.Windows.Forms.Padding(0);
            this.separatorControlKeypad2.Name = "separatorControlKeypad2";
            this.separatorControlKeypad2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.separatorControlKeypad2.Size = new System.Drawing.Size(368, 4);
            this.separatorControlKeypad2.TabIndex = 34;
            // 
            // txtPadQty
            // 
            this.txtPadQty.EditValue = "0";
            this.txtPadQty.Enabled = false;
            this.txtPadQty.Location = new System.Drawing.Point(47, 222);
            this.txtPadQty.Name = "txtPadQty";
            this.txtPadQty.Properties.AllowFocused = false;
            this.txtPadQty.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPadQty.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(84)))), ((int)(((byte)(151)))));
            this.txtPadQty.Properties.Appearance.Options.UseFont = true;
            this.txtPadQty.Properties.Appearance.Options.UseForeColor = true;
            this.txtPadQty.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPadQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPadQty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtPadQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPadQty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPadQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPadQty.Properties.NullValuePrompt = "0";
            this.txtPadQty.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPadQty.Properties.ReadOnly = true;
            this.txtPadQty.Size = new System.Drawing.Size(368, 60);
            this.txtPadQty.TabIndex = 32;
            // 
            // txtItemDescrip
            // 
            this.txtItemDescrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemDescrip.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.txtItemDescrip.Appearance.Options.UseFont = true;
            this.txtItemDescrip.Appearance.Options.UseTextOptions = true;
            this.txtItemDescrip.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtItemDescrip.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.txtItemDescrip.Location = new System.Drawing.Point(45, 132);
            this.txtItemDescrip.Name = "txtItemDescrip";
            this.txtItemDescrip.Size = new System.Drawing.Size(521, 57);
            this.txtItemDescrip.TabIndex = 30;
            this.txtItemDescrip.Text = "단위 및 수량";
            // 
            // txtPadItem
            // 
            this.txtPadItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPadItem.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 35F);
            this.txtPadItem.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(84)))), ((int)(((byte)(151)))));
            this.txtPadItem.Appearance.Options.UseFont = true;
            this.txtPadItem.Appearance.Options.UseForeColor = true;
            this.txtPadItem.Appearance.Options.UseTextOptions = true;
            this.txtPadItem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPadItem.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.txtPadItem.Location = new System.Drawing.Point(45, 63);
            this.txtPadItem.Name = "txtPadItem";
            this.txtPadItem.Size = new System.Drawing.Size(521, 57);
            this.txtPadItem.TabIndex = 28;
            this.txtPadItem.Text = "상품명";
            // 
            // btnPadPrice
            // 
            this.btnPadPrice.AllowFocus = false;
            this.btnPadPrice.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadPrice.Appearance.Options.UseFont = true;
            this.btnPadPrice.Location = new System.Drawing.Point(143, 674);
            this.btnPadPrice.Name = "btnPadPrice";
            this.btnPadPrice.Size = new System.Drawing.Size(90, 90);
            this.btnPadPrice.TabIndex = 56;
            this.btnPadPrice.Text = "원";
            // 
            // btnPadCancel
            // 
            this.btnPadCancel.AllowFocus = false;
            this.btnPadCancel.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadCancel.Appearance.Options.UseFont = true;
            this.btnPadCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnPadCancel.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnPadCancel.Location = new System.Drawing.Point(431, 674);
            this.btnPadCancel.Name = "btnPadCancel";
            this.btnPadCancel.Size = new System.Drawing.Size(120, 90);
            this.btnPadCancel.TabIndex = 58;
            this.btnPadCancel.Text = "취소";
            // 
            // btnPadDelItem
            // 
            this.btnPadDelItem.AllowFocus = false;
            this.btnPadDelItem.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadDelItem.Appearance.Options.UseFont = true;
            this.btnPadDelItem.Location = new System.Drawing.Point(47, 772);
            this.btnPadDelItem.Name = "btnPadDelItem";
            this.btnPadDelItem.Size = new System.Drawing.Size(504, 90);
            this.btnPadDelItem.TabIndex = 59;
            this.btnPadDelItem.Text = "삭        제";
            // 
            // btnPadInit
            // 
            this.btnPadInit.AllowFocus = false;
            this.btnPadInit.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadInit.Appearance.Options.UseFont = true;
            this.btnPadInit.Location = new System.Drawing.Point(431, 195);
            this.btnPadInit.Name = "btnPadInit";
            this.btnPadInit.Size = new System.Drawing.Size(120, 90);
            this.btnPadInit.TabIndex = 31;
            this.btnPadInit.Text = "초기화";
            // 
            // btnPadMinus
            // 
            this.btnPadMinus.AllowFocus = false;
            this.btnPadMinus.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadMinus.Appearance.Options.UseFont = true;
            this.btnPadMinus.Location = new System.Drawing.Point(335, 387);
            this.btnPadMinus.Name = "btnPadMinus";
            this.btnPadMinus.Size = new System.Drawing.Size(90, 90);
            this.btnPadMinus.TabIndex = 43;
            this.btnPadMinus.Text = "─";
            // 
            // btnPadEA
            // 
            this.btnPadEA.AllowFocus = false;
            this.btnPadEA.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadEA.Appearance.Options.UseFont = true;
            this.btnPadEA.Location = new System.Drawing.Point(47, 673);
            this.btnPadEA.Name = "btnPadEA";
            this.btnPadEA.Size = new System.Drawing.Size(90, 90);
            this.btnPadEA.TabIndex = 55;
            this.btnPadEA.Text = "묶음";
            // 
            // btnPadConfirm
            // 
            this.btnPadConfirm.AllowFocus = false;
            this.btnPadConfirm.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadConfirm.Appearance.Options.UseFont = true;
            this.btnPadConfirm.Location = new System.Drawing.Point(239, 674);
            this.btnPadConfirm.Name = "btnPadConfirm";
            this.btnPadConfirm.Size = new System.Drawing.Size(186, 90);
            this.btnPadConfirm.TabIndex = 57;
            this.btnPadConfirm.Text = "확  인";
            // 
            // btnPadItemUnit4
            // 
            this.btnPadItemUnit4.AllowFocus = false;
            this.btnPadItemUnit4.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadItemUnit4.Appearance.Options.UseFont = true;
            this.btnPadItemUnit4.Enabled = false;
            this.btnPadItemUnit4.Location = new System.Drawing.Point(431, 578);
            this.btnPadItemUnit4.Name = "btnPadItemUnit4";
            this.btnPadItemUnit4.Size = new System.Drawing.Size(120, 90);
            this.btnPadItemUnit4.TabIndex = 54;
            // 
            // btnPadItemUnit3
            // 
            this.btnPadItemUnit3.AllowFocus = false;
            this.btnPadItemUnit3.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadItemUnit3.Appearance.Options.UseFont = true;
            this.btnPadItemUnit3.Enabled = false;
            this.btnPadItemUnit3.Location = new System.Drawing.Point(431, 482);
            this.btnPadItemUnit3.Name = "btnPadItemUnit3";
            this.btnPadItemUnit3.Size = new System.Drawing.Size(120, 90);
            this.btnPadItemUnit3.TabIndex = 49;
            // 
            // btnPadItemUnit2
            // 
            this.btnPadItemUnit2.AllowFocus = false;
            this.btnPadItemUnit2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadItemUnit2.Appearance.Options.UseFont = true;
            this.btnPadItemUnit2.Enabled = false;
            this.btnPadItemUnit2.Location = new System.Drawing.Point(431, 387);
            this.btnPadItemUnit2.Name = "btnPadItemUnit2";
            this.btnPadItemUnit2.Size = new System.Drawing.Size(120, 90);
            this.btnPadItemUnit2.TabIndex = 44;
            // 
            // btnPadItemUnit1
            // 
            this.btnPadItemUnit1.AllowFocus = false;
            this.btnPadItemUnit1.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadItemUnit1.Appearance.Options.UseFont = true;
            this.btnPadItemUnit1.Enabled = false;
            this.btnPadItemUnit1.Location = new System.Drawing.Point(431, 291);
            this.btnPadItemUnit1.Name = "btnPadItemUnit1";
            this.btnPadItemUnit1.Size = new System.Drawing.Size(120, 90);
            this.btnPadItemUnit1.TabIndex = 39;
            // 
            // btnPadPoint
            // 
            this.btnPadPoint.AllowFocus = false;
            this.btnPadPoint.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadPoint.Appearance.Options.UseFont = true;
            this.btnPadPoint.Location = new System.Drawing.Point(335, 578);
            this.btnPadPoint.Name = "btnPadPoint";
            this.btnPadPoint.Size = new System.Drawing.Size(90, 90);
            this.btnPadPoint.TabIndex = 53;
            this.btnPadPoint.Text = ".";
            // 
            // btnPadBackSpace
            // 
            this.btnPadBackSpace.AllowFocus = false;
            this.btnPadBackSpace.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadBackSpace.Appearance.Options.UseFont = true;
            this.btnPadBackSpace.Location = new System.Drawing.Point(335, 482);
            this.btnPadBackSpace.Name = "btnPadBackSpace";
            this.btnPadBackSpace.Size = new System.Drawing.Size(90, 90);
            this.btnPadBackSpace.TabIndex = 48;
            this.btnPadBackSpace.Text = "←";
            // 
            // btnPadClear
            // 
            this.btnPadClear.AllowFocus = false;
            this.btnPadClear.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPadClear.Appearance.Options.UseFont = true;
            this.btnPadClear.Location = new System.Drawing.Point(335, 291);
            this.btnPadClear.Name = "btnPadClear";
            this.btnPadClear.Size = new System.Drawing.Size(90, 90);
            this.btnPadClear.TabIndex = 38;
            this.btnPadClear.Text = "CLS";
            // 
            // btnPad000
            // 
            this.btnPad000.AllowFocus = false;
            this.btnPad000.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad000.Appearance.Options.UseFont = true;
            this.btnPad000.Location = new System.Drawing.Point(239, 578);
            this.btnPad000.Name = "btnPad000";
            this.btnPad000.Size = new System.Drawing.Size(90, 90);
            this.btnPad000.TabIndex = 52;
            this.btnPad000.Text = "000";
            // 
            // btnPad00
            // 
            this.btnPad00.AllowFocus = false;
            this.btnPad00.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad00.Appearance.Options.UseFont = true;
            this.btnPad00.Location = new System.Drawing.Point(143, 578);
            this.btnPad00.Name = "btnPad00";
            this.btnPad00.Size = new System.Drawing.Size(90, 90);
            this.btnPad00.TabIndex = 51;
            this.btnPad00.Text = "00";
            // 
            // btnPad0
            // 
            this.btnPad0.AllowFocus = false;
            this.btnPad0.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad0.Appearance.Options.UseFont = true;
            this.btnPad0.Location = new System.Drawing.Point(47, 577);
            this.btnPad0.Name = "btnPad0";
            this.btnPad0.Size = new System.Drawing.Size(90, 90);
            this.btnPad0.TabIndex = 50;
            this.btnPad0.Text = "0";
            // 
            // btnPad3
            // 
            this.btnPad3.AllowFocus = false;
            this.btnPad3.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad3.Appearance.Options.UseFont = true;
            this.btnPad3.Location = new System.Drawing.Point(239, 482);
            this.btnPad3.Name = "btnPad3";
            this.btnPad3.Size = new System.Drawing.Size(90, 90);
            this.btnPad3.TabIndex = 47;
            this.btnPad3.Text = "3";
            // 
            // btnPad2
            // 
            this.btnPad2.AllowFocus = false;
            this.btnPad2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad2.Appearance.Options.UseFont = true;
            this.btnPad2.Location = new System.Drawing.Point(143, 482);
            this.btnPad2.Name = "btnPad2";
            this.btnPad2.Size = new System.Drawing.Size(90, 90);
            this.btnPad2.TabIndex = 46;
            this.btnPad2.Text = "2";
            // 
            // btnPad1
            // 
            this.btnPad1.AllowFocus = false;
            this.btnPad1.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad1.Appearance.Options.UseFont = true;
            this.btnPad1.Location = new System.Drawing.Point(47, 481);
            this.btnPad1.Name = "btnPad1";
            this.btnPad1.Size = new System.Drawing.Size(90, 90);
            this.btnPad1.TabIndex = 45;
            this.btnPad1.Text = "1";
            // 
            // btnPad6
            // 
            this.btnPad6.AllowFocus = false;
            this.btnPad6.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad6.Appearance.Options.UseFont = true;
            this.btnPad6.Location = new System.Drawing.Point(239, 387);
            this.btnPad6.Name = "btnPad6";
            this.btnPad6.Size = new System.Drawing.Size(90, 90);
            this.btnPad6.TabIndex = 42;
            this.btnPad6.Text = "6";
            // 
            // btnPad5
            // 
            this.btnPad5.AllowFocus = false;
            this.btnPad5.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad5.Appearance.Options.UseFont = true;
            this.btnPad5.Location = new System.Drawing.Point(143, 387);
            this.btnPad5.Name = "btnPad5";
            this.btnPad5.Size = new System.Drawing.Size(90, 90);
            this.btnPad5.TabIndex = 41;
            this.btnPad5.Text = "5";
            // 
            // btnPad4
            // 
            this.btnPad4.AllowFocus = false;
            this.btnPad4.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad4.Appearance.Options.UseFont = true;
            this.btnPad4.Location = new System.Drawing.Point(47, 386);
            this.btnPad4.Name = "btnPad4";
            this.btnPad4.Size = new System.Drawing.Size(90, 90);
            this.btnPad4.TabIndex = 40;
            this.btnPad4.Text = "4";
            // 
            // btnPad9
            // 
            this.btnPad9.AllowFocus = false;
            this.btnPad9.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad9.Appearance.Options.UseFont = true;
            this.btnPad9.Location = new System.Drawing.Point(239, 291);
            this.btnPad9.Name = "btnPad9";
            this.btnPad9.Size = new System.Drawing.Size(90, 90);
            this.btnPad9.TabIndex = 37;
            this.btnPad9.Text = "9";
            // 
            // btnPad8
            // 
            this.btnPad8.AllowFocus = false;
            this.btnPad8.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad8.Appearance.Options.UseFont = true;
            this.btnPad8.Location = new System.Drawing.Point(143, 291);
            this.btnPad8.Name = "btnPad8";
            this.btnPad8.Size = new System.Drawing.Size(90, 90);
            this.btnPad8.TabIndex = 36;
            this.btnPad8.Text = "8";
            // 
            // btnPad7
            // 
            this.btnPad7.AllowFocus = false;
            this.btnPad7.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnPad7.Appearance.Options.UseFont = true;
            this.btnPad7.Location = new System.Drawing.Point(47, 290);
            this.btnPad7.Name = "btnPad7";
            this.btnPad7.Size = new System.Drawing.Size(90, 90);
            this.btnPad7.TabIndex = 35;
            this.btnPad7.Text = "7";
            // 
            // M_POS_INV_OPEN
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 979);
            this.IsBottomVisible = true;
            this.Name = "M_POS_INV_OPEN";
            this.SubTitle = "Form1";
            this.Text = "Form1";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).EndInit();
            this.panelDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelKeypadItem)).EndInit();
            this.panelKeypadItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControlKeypad1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControlKeypad2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPadQty.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn No;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private Bifrost.Grid.aGrid gridMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelDetail;
        private DevExpress.XtraEditors.PanelControl panelKeypadItem;
        private DevExpress.XtraEditors.SeparatorControl separatorControlKeypad1;
        private DevExpress.XtraEditors.SeparatorControl separatorControlKeypad2;
        private DevExpress.XtraEditors.TextEdit txtPadQty;
        private DevExpress.XtraEditors.LabelControl txtItemDescrip;
        private DevExpress.XtraEditors.LabelControl txtPadItem;
        private DevExpress.XtraEditors.SimpleButton btnPadPrice;
        private DevExpress.XtraEditors.SimpleButton btnPadCancel;
        private DevExpress.XtraEditors.SimpleButton btnPadDelItem;
        private DevExpress.XtraEditors.SimpleButton btnPadInit;
        private DevExpress.XtraEditors.SimpleButton btnPadMinus;
        private DevExpress.XtraEditors.SimpleButton btnPadEA;
        private DevExpress.XtraEditors.SimpleButton btnPadConfirm;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit4;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit3;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit2;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit1;
        private DevExpress.XtraEditors.SimpleButton btnPadPoint;
        private DevExpress.XtraEditors.SimpleButton btnPadBackSpace;
        private DevExpress.XtraEditors.SimpleButton btnPadClear;
        private DevExpress.XtraEditors.SimpleButton btnPad000;
        private DevExpress.XtraEditors.SimpleButton btnPad00;
        private DevExpress.XtraEditors.SimpleButton btnPad0;
        private DevExpress.XtraEditors.SimpleButton btnPad3;
        private DevExpress.XtraEditors.SimpleButton btnPad2;
        private DevExpress.XtraEditors.SimpleButton btnPad1;
        private DevExpress.XtraEditors.SimpleButton btnPad6;
        private DevExpress.XtraEditors.SimpleButton btnPad5;
        private DevExpress.XtraEditors.SimpleButton btnPad4;
        private DevExpress.XtraEditors.SimpleButton btnPad9;
        private DevExpress.XtraEditors.SimpleButton btnPad8;
        private DevExpress.XtraEditors.SimpleButton btnPad7;
    }
}

