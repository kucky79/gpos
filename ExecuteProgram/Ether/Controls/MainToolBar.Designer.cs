using Bifrost.Win;
using System.Windows.Forms;

namespace Bifrost.Controls
{
    partial class MainToolBar
    {

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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainToolBar));
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblSaleDt = new DevExpress.XtraEditors.LabelControl();
            this.lblTime = new DevExpress.XtraEditors.LabelControl();
            this.btnClock = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnHome = new DevExpress.XtraEditors.SimpleButton();
            this.picLogo = new DevExpress.XtraEditors.PictureEdit();
            this.lblStoreName = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogout = new DevExpress.XtraEditors.SimpleButton();
            this.btnMin = new DevExpress.XtraEditors.SimpleButton();
            this.btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTop.Controls.Add(this.tableLayout);
            this.panelTop.Controls.Add(this.simpleButton1);
            this.panelTop.Controls.Add(this.lblStoreName);
            this.panelTop.Controls.Add(this.btnClock);
            this.panelTop.Controls.Add(this.lblTime);
            this.panelTop.Controls.Add(this.btnLogout);
            this.panelTop.Controls.Add(this.btnMin);
            this.panelTop.Controls.Add(this.btnRestore);
            this.panelTop.Controls.Add(this.btnExit);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.btnHome);
            this.panelTop.Controls.Add(this.picLogo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelTop.Size = new System.Drawing.Size(1049, 50);
            this.panelTop.TabIndex = 11;
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 10;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout.Controls.Add(this.labelControl1, 2, 0);
            this.tableLayout.Controls.Add(this.lblSaleDt, 3, 0);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayout.Location = new System.Drawing.Point(526, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 1;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(77, 50);
            this.tableLayout.TabIndex = 31;
            this.tableLayout.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(33, 44);
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "판매일";
            this.labelControl1.Visible = false;
            // 
            // lblSaleDt
            // 
            this.lblSaleDt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSaleDt.Location = new System.Drawing.Point(42, 3);
            this.lblSaleDt.Name = "lblSaleDt";
            this.lblSaleDt.Size = new System.Drawing.Size(1, 44);
            this.lblSaleDt.TabIndex = 34;
            this.lblSaleDt.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTime.Appearance.Options.UseFont = true;
            this.lblTime.Appearance.Options.UseTextOptions = true;
            this.lblTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTime.Location = new System.Drawing.Point(719, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(130, 50);
            this.lblTime.TabIndex = 35;
            this.lblTime.Text = "오후 06:10:15";
            // 
            // btnClock
            // 
            this.btnClock.AllowFocus = false;
            this.btnClock.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClock.Enabled = false;
            this.btnClock.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnClock.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnClock.ImageOptions.SvgImage")));
            this.btnClock.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnClock.Location = new System.Drawing.Point(689, 0);
            this.btnClock.Name = "btnClock";
            this.btnClock.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClock.Size = new System.Drawing.Size(30, 50);
            this.btnClock.TabIndex = 32;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Location = new System.Drawing.Point(131, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(389, 50);
            this.lblTitle.TabIndex = 30;
            // 
            // btnHome
            // 
            this.btnHome.AllowFocus = false;
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnHome.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnHome.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnHome.ImageOptions.SvgImage")));
            this.btnHome.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnHome.Location = new System.Drawing.Point(80, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnHome.Size = new System.Drawing.Size(51, 50);
            this.btnHome.TabIndex = 27;
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.picLogo.EditValue = global::Ether.Properties.Resources.bright_gpos_logo_header;
            this.picLogo.Location = new System.Drawing.Point(10, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Properties.AllowFocused = false;
            this.picLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Properties.Appearance.Options.UseBackColor = true;
            this.picLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picLogo.Properties.SvgImageSize = new System.Drawing.Size(60, 60);
            this.picLogo.Size = new System.Drawing.Size(70, 50);
            this.picLogo.TabIndex = 32;
            // 
            // lblStoreName
            // 
            this.lblStoreName.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F, System.Drawing.FontStyle.Bold);
            this.lblStoreName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(86)))), ((int)(((byte)(152)))));
            this.lblStoreName.Appearance.Options.UseFont = true;
            this.lblStoreName.Appearance.Options.UseForeColor = true;
            this.lblStoreName.Appearance.Options.UseTextOptions = true;
            this.lblStoreName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblStoreName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblStoreName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblStoreName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStoreName.Location = new System.Drawing.Point(633, 0);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Padding = new System.Windows.Forms.Padding(0, 11, 0, 0);
            this.lblStoreName.Size = new System.Drawing.Size(56, 39);
            this.lblStoreName.TabIndex = 37;
            this.lblStoreName.Text = "Store";
            // 
            // simpleButton1
            // 
            this.simpleButton1.AllowFocus = false;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Enabled = false;
            this.simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.simpleButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.simpleButton1.Location = new System.Drawing.Point(603, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButton1.Size = new System.Drawing.Size(30, 50);
            this.simpleButton1.TabIndex = 38;
            // 
            // btnLogout
            // 
            this.btnLogout.AllowFocus = false;
            this.btnLogout.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnLogout.Appearance.Options.UseFont = true;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLogout.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLogout.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnLogout.Location = new System.Drawing.Point(849, 0);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnLogout.Size = new System.Drawing.Size(50, 50);
            this.btnLogout.TabIndex = 36;
            this.btnLogout.Text = "로그\n아웃";
            this.btnLogout.ToolTip = "로그아웃";
            // 
            // btnMin
            // 
            this.btnMin.AllowFocus = false;
            this.btnMin.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnMin.Appearance.Options.UseFont = true;
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMin.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMin.ImageOptions.SvgImage = global::Ether.Properties.Resources.ChromeMinimize_16x;
            this.btnMin.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnMin.Location = new System.Drawing.Point(899, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnMin.Size = new System.Drawing.Size(50, 50);
            this.btnMin.TabIndex = 38;
            this.btnMin.ToolTip = "최소화";
            // 
            // btnRestore
            // 
            this.btnRestore.AllowFocus = false;
            this.btnRestore.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnRestore.Appearance.Options.UseFont = true;
            this.btnRestore.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRestore.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRestore.ImageOptions.SvgImage = global::Ether.Properties.Resources.ChromeRestore_16x;
            this.btnRestore.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnRestore.Location = new System.Drawing.Point(949, 0);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnRestore.Size = new System.Drawing.Size(50, 50);
            this.btnRestore.TabIndex = 37;
            this.btnRestore.ToolTip = "최대화";
            // 
            // btnExit
            // 
            this.btnExit.AllowFocus = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExit.ImageOptions.SvgImage = global::Ether.Properties.Resources.bright_close_icon_normal;
            this.btnExit.ImageOptions.SvgImageSize = new System.Drawing.Size(17, 17);
            this.btnExit.Location = new System.Drawing.Point(999, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnExit.Size = new System.Drawing.Size(50, 50);
            this.btnExit.TabIndex = 35;
            this.btnExit.ToolTip = "닫기";
            // 
            // MainToolBar
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MainToolBar";
            this.Size = new System.Drawing.Size(1049, 50);
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.SimpleButton btnHome;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private TableLayoutPanel tableLayout;
        private DevExpress.XtraEditors.LabelControl lblTime;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblSaleDt;
        private DevExpress.XtraEditors.SimpleButton btnClock;
        private DevExpress.XtraEditors.PictureEdit picLogo;
        private DevExpress.XtraEditors.LabelControl lblStoreName;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnLogout;
        private DevExpress.XtraEditors.SimpleButton btnMin;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        //private PictureBox picUserLogo;
    }
}
