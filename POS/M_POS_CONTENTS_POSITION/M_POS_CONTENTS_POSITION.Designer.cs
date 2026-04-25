namespace POS
{
    partial class M_POS_CONTENTS_POSITION
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M_POS_CONTENTS_POSITION));
            this.splitContainerMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridMain = new Bifrost.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRight = new DevExpress.XtraEditors.SimpleButton();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnLeft = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.btnInit = new DevExpress.XtraEditors.SimpleButton();
            this.rboCustmerType = new Bifrost.Win.Controls.aRadioButton();
            this.btnResetCode = new DevExpress.XtraEditors.SimpleButton();
            this.btnResetAtoZ = new DevExpress.XtraEditors.SimpleButton();
            this.btnChangeSort = new DevExpress.XtraEditors.SimpleButton();
            this.rboContents = new Bifrost.Win.Controls.aRadioButton();
            this.panelContents = new DevExpress.XtraEditors.PanelControl();
            this.flowLayoutPanelContents = new System.Windows.Forms.FlowLayoutPanel();
            this.progressBarContents = new DevExpress.XtraEditors.ProgressBarControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnTop = new DevExpress.XtraEditors.SimpleButton();
            this.btnPre = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rboCustmerType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rboContents.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContents)).BeginInit();
            this.panelContents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarContents.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.splitContainerMain);
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Size = new System.Drawing.Size(1073, 710);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Panel1.Controls.Add(this.gridMain);
            this.splitContainerMain.Panel1.Controls.Add(this.panelControl);
            this.splitContainerMain.Panel1.Controls.Add(this.panelTop);
            this.splitContainerMain.Panel1.Text = "Panel1";
            this.splitContainerMain.Panel2.Controls.Add(this.panelContents);
            this.splitContainerMain.Panel2.Text = "Panel2";
            this.splitContainerMain.Size = new System.Drawing.Size(1073, 710);
            this.splitContainerMain.SplitterPosition = 529;
            this.splitContainerMain.TabIndex = 4;
            // 
            // gridMain
            // 
            this.gridMain.AddNewRowLastColumn = false;
            this.gridMain.disabledEditingColumns = null;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.isSaveLayout = false;
            this.gridMain.isUpper = false;
            this.gridMain.LayoutVersion = "";
            this.gridMain.Location = new System.Drawing.Point(0, 176);
            this.gridMain.MainView = this.gridView1;
            this.gridMain.Name = "gridMain";
            this.gridMain.SetBindingEvnet = true;
            this.gridMain.Size = new System.Drawing.Size(529, 468);
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
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.No,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
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
            this.gridColumn1.Caption = "상품코드";
            this.gridColumn1.FieldName = "CD_ITEM";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "상품명";
            this.gridColumn2.FieldName = "NM_ITEM";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "순서";
            this.gridColumn3.FieldName = "NO_SORT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // panelControl
            // 
            this.panelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl.Controls.Add(this.layoutControl1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 644);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(529, 66);
            this.panelControl.TabIndex = 30;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnRight);
            this.layoutControl1.Controls.Add(this.btnDown);
            this.layoutControl1.Controls.Add(this.btnUp);
            this.layoutControl1.Controls.Add(this.btnLeft);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2007, -666, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(529, 66);
            this.layoutControl1.TabIndex = 31;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnRight
            // 
            this.btnRight.AllowFocus = false;
            this.btnRight.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.btnRight.Appearance.Options.UseFont = true;
            this.btnRight.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRight.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_to_left_icon_normal;
            this.btnRight.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnRight.Location = new System.Drawing.Point(393, 12);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(124, 42);
            this.btnRight.StyleController = this.layoutControl1;
            this.btnRight.TabIndex = 37;
            // 
            // btnDown
            // 
            this.btnDown.AllowFocus = false;
            this.btnDown.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.btnDown.Appearance.Options.UseFont = true;
            this.btnDown.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDown.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_to_top_icon_normal;
            this.btnDown.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnDown.Location = new System.Drawing.Point(266, 12);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(123, 42);
            this.btnDown.StyleController = this.layoutControl1;
            this.btnDown.TabIndex = 36;
            // 
            // btnUp
            // 
            this.btnUp.AllowFocus = false;
            this.btnUp.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.btnUp.Appearance.Options.UseFont = true;
            this.btnUp.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnUp.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_to_bottom_icon_normal;
            this.btnUp.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnUp.Location = new System.Drawing.Point(139, 12);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(123, 42);
            this.btnUp.StyleController = this.layoutControl1;
            this.btnUp.TabIndex = 35;
            // 
            // btnLeft
            // 
            this.btnLeft.AllowFocus = false;
            this.btnLeft.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.btnLeft.Appearance.Options.UseFont = true;
            this.btnLeft.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLeft.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_to_right_icon_normal;
            this.btnLeft.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnLeft.Location = new System.Drawing.Point(12, 12);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(123, 42);
            this.btnLeft.StyleController = this.layoutControl1;
            this.btnLeft.TabIndex = 34;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(529, 66);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnLeft;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(37, 35);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(127, 46);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnUp;
            this.layoutControlItem2.Location = new System.Drawing.Point(127, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(37, 35);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(127, 46);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnDown;
            this.layoutControlItem3.Location = new System.Drawing.Point(254, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(37, 35);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(127, 46);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnRight;
            this.layoutControlItem4.Location = new System.Drawing.Point(381, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(37, 35);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(128, 46);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTop.Controls.Add(this.btnInit);
            this.panelTop.Controls.Add(this.rboCustmerType);
            this.panelTop.Controls.Add(this.btnResetCode);
            this.panelTop.Controls.Add(this.btnResetAtoZ);
            this.panelTop.Controls.Add(this.btnChangeSort);
            this.panelTop.Controls.Add(this.rboContents);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(529, 176);
            this.panelTop.TabIndex = 6;
            // 
            // btnInit
            // 
            this.btnInit.AllowFocus = false;
            this.btnInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInit.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.btnInit.Appearance.Options.UseFont = true;
            this.btnInit.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_reset_icon_normal;
            this.btnInit.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnInit.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnInit.Location = new System.Drawing.Point(385, 110);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(130, 50);
            this.btnInit.TabIndex = 12;
            this.btnInit.Text = "순서 초기화";
            // 
            // rboCustmerType
            // 
            this.rboCustmerType.EditValue = "S";
            this.rboCustmerType.Location = new System.Drawing.Point(15, 54);
            this.rboCustmerType.Name = "rboCustmerType";
            this.rboCustmerType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rboCustmerType.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.rboCustmerType.Properties.Appearance.Options.UseBackColor = true;
            this.rboCustmerType.Properties.Appearance.Options.UseFont = true;
            this.rboCustmerType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rboCustmerType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("S", "판매처"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("P", "구매처")});
            this.rboCustmerType.SelectedValue = "";
            this.rboCustmerType.Size = new System.Drawing.Size(190, 35);
            this.rboCustmerType.TabIndex = 8;
            this.rboCustmerType.Visible = false;
            // 
            // btnResetCode
            // 
            this.btnResetCode.AllowFocus = false;
            this.btnResetCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetCode.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.btnResetCode.Appearance.Options.UseFont = true;
            this.btnResetCode.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnResetCode.ImageOptions.SvgImage")));
            this.btnResetCode.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnResetCode.Location = new System.Drawing.Point(249, 54);
            this.btnResetCode.Name = "btnResetCode";
            this.btnResetCode.Size = new System.Drawing.Size(130, 50);
            this.btnResetCode.TabIndex = 9;
            this.btnResetCode.Tag = "CODE";
            this.btnResetCode.Text = "코드순\n초기화";
            // 
            // btnResetAtoZ
            // 
            this.btnResetAtoZ.AllowFocus = false;
            this.btnResetAtoZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetAtoZ.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.btnResetAtoZ.Appearance.Options.UseFont = true;
            this.btnResetAtoZ.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnResetAtoZ.ImageOptions.SvgImage")));
            this.btnResetAtoZ.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnResetAtoZ.Location = new System.Drawing.Point(249, 110);
            this.btnResetAtoZ.Name = "btnResetAtoZ";
            this.btnResetAtoZ.Size = new System.Drawing.Size(130, 50);
            this.btnResetAtoZ.TabIndex = 11;
            this.btnResetAtoZ.Tag = "NAME";
            this.btnResetAtoZ.Text = "이름순\n초기화";
            // 
            // btnChangeSort
            // 
            this.btnChangeSort.AllowFocus = false;
            this.btnChangeSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeSort.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.btnChangeSort.Appearance.Options.UseFont = true;
            this.btnChangeSort.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnChangeSort.ImageOptions.SvgImage")));
            this.btnChangeSort.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnChangeSort.Location = new System.Drawing.Point(385, 54);
            this.btnChangeSort.Name = "btnChangeSort";
            this.btnChangeSort.Size = new System.Drawing.Size(130, 50);
            this.btnChangeSort.TabIndex = 10;
            this.btnChangeSort.Text = "순서변경";
            // 
            // rboContents
            // 
            this.rboContents.EditValue = "ITEMTYPE";
            this.rboContents.Location = new System.Drawing.Point(15, 13);
            this.rboContents.Name = "rboContents";
            this.rboContents.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rboContents.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.rboContents.Properties.Appearance.Options.UseBackColor = true;
            this.rboContents.Properties.Appearance.Options.UseFont = true;
            this.rboContents.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rboContents.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CUSTTYPE", "거래처분류"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CUST", "거래처"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("ITEMTYPE", "상품분류"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("ITEM", "상품")});
            this.rboContents.SelectedValue = "";
            this.rboContents.Size = new System.Drawing.Size(503, 35);
            this.rboContents.TabIndex = 7;
            // 
            // panelContents
            // 
            this.panelContents.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelContents.Controls.Add(this.flowLayoutPanelContents);
            this.panelContents.Controls.Add(this.progressBarContents);
            this.panelContents.Controls.Add(this.panelControl1);
            this.panelContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContents.Location = new System.Drawing.Point(0, 0);
            this.panelContents.Name = "panelContents";
            this.panelContents.Size = new System.Drawing.Size(539, 710);
            this.panelContents.TabIndex = 41;
            // 
            // flowLayoutPanelContents
            // 
            this.flowLayoutPanelContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelContents.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelContents.Name = "flowLayoutPanelContents";
            this.flowLayoutPanelContents.Size = new System.Drawing.Size(539, 623);
            this.flowLayoutPanelContents.TabIndex = 42;
            // 
            // progressBarContents
            // 
            this.progressBarContents.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarContents.Location = new System.Drawing.Point(0, 623);
            this.progressBarContents.Name = "progressBarContents";
            this.progressBarContents.Size = new System.Drawing.Size(539, 6);
            this.progressBarContents.TabIndex = 43;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnTop);
            this.panelControl1.Controls.Add(this.btnPre);
            this.panelControl1.Controls.Add(this.btnNext);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 629);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(539, 81);
            this.panelControl1.TabIndex = 44;
            // 
            // btnTop
            // 
            this.btnTop.AllowFocus = false;
            this.btnTop.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnTop.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            this.btnTop.Appearance.Options.UseFont = true;
            this.btnTop.Appearance.Options.UseForeColor = true;
            this.btnTop.Appearance.Options.UseTextOptions = true;
            this.btnTop.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnTop.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_up_icon_normal;
            this.btnTop.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnTop.Location = new System.Drawing.Point(5, 5);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(144, 70);
            this.btnTop.TabIndex = 45;
            // 
            // btnPre
            // 
            this.btnPre.AllowFocus = false;
            this.btnPre.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnPre.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            this.btnPre.Appearance.Options.UseFont = true;
            this.btnPre.Appearance.Options.UseForeColor = true;
            this.btnPre.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnPre.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_back_icon_normal;
            this.btnPre.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnPre.Location = new System.Drawing.Point(155, 5);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(144, 70);
            this.btnPre.TabIndex = 46;
            // 
            // btnNext
            // 
            this.btnNext.AllowFocus = false;
            this.btnNext.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnNext.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            this.btnNext.Appearance.Options.UseFont = true;
            this.btnNext.Appearance.Options.UseForeColor = true;
            this.btnNext.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnNext.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_next_icon_normal;
            this.btnNext.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnNext.Location = new System.Drawing.Point(305, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(144, 70);
            this.btnNext.TabIndex = 47;
            // 
            // M_POS_CONTENTS_POSITION
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 770);
            this.IsBottomVisible = true;
            this.Name = "M_POS_CONTENTS_POSITION";
            this.SubTitle = "Form1";
            this.Text = "Form1";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rboCustmerType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rboContents.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContents)).EndInit();
            this.panelContents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarContents.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SplitContainerControl splitContainerMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn No;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.PanelControl panelContents;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelContents;
        private DevExpress.XtraEditors.ProgressBarControl progressBarContents;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.SimpleButton btnLeft;
        private DevExpress.XtraEditors.SimpleButton btnRight;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.PanelControl panelTop;
        private Bifrost.Win.Controls.aRadioButton rboContents;
        private DevExpress.XtraEditors.SimpleButton btnChangeSort;
        private Bifrost.Grid.aGrid gridMain;
        private DevExpress.XtraEditors.SimpleButton btnResetAtoZ;
        private DevExpress.XtraEditors.SimpleButton btnResetCode;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnTop;
        private DevExpress.XtraEditors.SimpleButton btnPre;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private Bifrost.Win.Controls.aRadioButton rboCustmerType;
        private DevExpress.XtraEditors.SimpleButton btnInit;
    }
}

