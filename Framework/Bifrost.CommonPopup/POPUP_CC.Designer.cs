namespace NF.A2P.CommonPopup
{
    partial class POPUP_CC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_CC));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
            this.aButton_Search = new NF.Framework.Win.Controls.aButton();
            this.txtSearch = new NF.Framework.Win.Controls.aTextEdit();
            this.aLabel1 = new NF.Framework.Win.Controls.aLabel();
            this.aGridM = new NF.A2P.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlBound.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.aGridM);
            this.pnlBound.Controls.Add(this.panel1);
            this.pnlBound.Size = new System.Drawing.Size(420, 398);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 486);
            this.pnlBottom.Size = new System.Drawing.Size(460, 46);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.aButton_Search);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.aLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 34);
            this.panel1.TabIndex = 0;
            // 
            // aButton_Search
            // 
            this.aButton_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.aButton_Search.BorderColor = System.Drawing.Color.Transparent;
            this.aButton_Search.ButtonDesignType = NF.Framework.Win.Controls.ButtonDesignType.POP_SEARCH;
            this.aButton_Search.DisabledImage = ((System.Drawing.Image)(resources.GetObject("aButton_Search.DisabledImage")));
            this.aButton_Search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.aButton_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.GroupKey = null;
            this.aButton_Search.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(171)))), ((int)(((byte)(139)))));
            this.aButton_Search.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aButton_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aButton_Search.Location = new System.Drawing.Point(291, 5);
            this.aButton_Search.Name = "aButton_Search";
            this.aButton_Search.Selected = false;
            this.aButton_Search.SelectedColor = System.Drawing.Color.LightGray;
            this.aButton_Search.Size = new System.Drawing.Size(100, 24);
            this.aButton_Search.TabIndex = 9;
            this.aButton_Search.Text = "Search";
            this.aButton_Search.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aButton_Search.UnSelectOtherButtons = false;
            this.aButton_Search.UseDefaultImages = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.isUpper = true;
            this.txtSearch.Location = new System.Drawing.Point(47, 4);
            this.txtSearch.Modified = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSearch.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSearch.Properties.Appearance.Options.UseBackColor = true;
            this.txtSearch.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSearch.Properties.AutoHeight = false;
            this.txtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("txtSearch.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, true)});
            this.txtSearch.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.txtSearch.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtSearch.Size = new System.Drawing.Size(238, 24);
            this.txtSearch.TabIndex = 1;
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(11, 5);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(27, 24);
            this.aLabel1.TabIndex = 0;
            this.aLabel1.Text = "aLabel";
            // 
            // aGridM
            // 
            this.aGridM.AddNewRowLastColumn = true;
            this.aGridM.disabledEditingColumns = new string[0];
            this.aGridM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aGridM.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.aGridM.isSaveLayout = false;
            this.aGridM.isUpper = false;
            this.aGridM.LayoutVersion = "";
            this.aGridM.Location = new System.Drawing.Point(5, 39);
            this.aGridM.MainView = this.gridView1;
            this.aGridM.Name = "aGridM";
            this.aGridM.SetBindingEvnet = true;
            this.aGridM.Size = new System.Drawing.Size(410, 354);
            this.aGridM.TabIndex = 1;
            this.aGridM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.aGridM;
            this.gridView1.Name = "gridView1";
            // 
            // POPUP_CC
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 533);
            this.Name = "POPUP_CC";
            this.Text = "Form1";
            this.pnlBound.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private NF.Framework.Win.Controls.aTextEdit txtSearch;
        private NF.Framework.Win.Controls.aLabel aLabel1;
        private A2P.Grid.aGrid aGridM;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Framework.Win.Controls.aButton aButton_Search;
    }
}

