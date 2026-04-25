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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.xtraTabbedMdiManagerMain = new Jarvis.MainForm.MyTabbedMdiManager();
            this.mainToolBar = new Bifrost.Controls.MainToolBar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManagerMain)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabbedMdiManagerMain
            // 
            this.xtraTabbedMdiManagerMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabbedMdiManagerMain.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabbedMdiManagerMain.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;
            this.xtraTabbedMdiManagerMain.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this.xtraTabbedMdiManagerMain.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;
            this.xtraTabbedMdiManagerMain.MdiParent = this;
            // 
            // mainToolBar
            // 
            this.mainToolBar.BackColor = System.Drawing.Color.Transparent;
            this.mainToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainToolBar.IsMinMax = false;
            this.mainToolBar.Location = new System.Drawing.Point(0, 0);
            this.mainToolBar.MainTitle = null;
            this.mainToolBar.Margin = new System.Windows.Forms.Padding(0);
            this.mainToolBar.Name = "mainToolBar";
            this.mainToolBar.SaleDt = "2019-11-05";
            this.mainToolBar.Size = new System.Drawing.Size(1270, 50);
            this.mainToolBar.StoreName = null;
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
            // MainForm
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1270, 979);
            this.Controls.Add(this.mainToolBar);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("MainForm.IconOptions.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManagerMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bifrost.Controls.MainToolBar mainToolBar;
        private DevExpress.XtraBars.Bar bar2;
    }
}

