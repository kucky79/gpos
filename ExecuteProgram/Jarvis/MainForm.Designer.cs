namespace Jarvis
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.xtraTabbedMdiManagerMain = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.mainToolBar = new Bifrost.Controls.MainToolBar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.accordionControlMenu = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManagerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControlMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabbedMdiManagerMain
            // 
            this.xtraTabbedMdiManagerMain.MdiParent = this;
            // 
            // mainToolBar
            // 
            this.mainToolBar.BackColor = System.Drawing.Color.Transparent;
            this.mainToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainToolBar.Location = new System.Drawing.Point(0, 0);
            this.mainToolBar.MainTitle = null;
            this.mainToolBar.Margin = new System.Windows.Forms.Padding(0);
            this.mainToolBar.Name = "mainToolBar";
            this.mainToolBar.SaleDt = "2019-11-05";
            this.mainToolBar.Size = new System.Drawing.Size(1270, 50);
            this.mainToolBar.TabIndex = 0;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // accordionControlMenu
            // 
            this.accordionControlMenu.AnimationType = DevExpress.XtraBars.Navigation.AnimationType.None;
            this.accordionControlMenu.Appearance.Group.Hovered.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.accordionControlMenu.Appearance.Group.Hovered.Options.UseFont = true;
            this.accordionControlMenu.Appearance.Group.Normal.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.accordionControlMenu.Appearance.Group.Normal.Options.UseFont = true;
            this.accordionControlMenu.Appearance.Group.Pressed.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.accordionControlMenu.Appearance.Group.Pressed.Options.UseFont = true;
            this.accordionControlMenu.Appearance.Item.Hovered.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.accordionControlMenu.Appearance.Item.Hovered.Options.UseFont = true;
            this.accordionControlMenu.Appearance.Item.Normal.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.accordionControlMenu.Appearance.Item.Normal.Options.UseFont = true;
            this.accordionControlMenu.Appearance.Item.Pressed.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.accordionControlMenu.Appearance.Item.Pressed.Options.UseFont = true;
            this.accordionControlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControlMenu.Location = new System.Drawing.Point(0, 50);
            this.accordionControlMenu.Name = "accordionControlMenu";
            this.accordionControlMenu.OptionsHamburgerMenu.DisplayMode = DevExpress.XtraBars.Navigation.AccordionControlDisplayMode.Overlay;
            this.accordionControlMenu.Size = new System.Drawing.Size(250, 744);
            this.accordionControlMenu.TabIndex = 16;
            this.accordionControlMenu.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // MainForm
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1270, 794);
            this.Controls.Add(this.accordionControlMenu);
            this.Controls.Add(this.mainToolBar);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("MainForm.IconOptions.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManagerMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControlMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bifrost.Controls.MainToolBar mainToolBar;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControlMenu;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
    }
}

