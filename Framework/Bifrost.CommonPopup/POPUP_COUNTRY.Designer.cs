namespace NF.A2P.CommonPopup
{
    partial class POPUP_COUNTRY
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_COUNTRY));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.pnlBound.Size = new System.Drawing.Size(342, 430);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 518);
            this.pnlBottom.Size = new System.Drawing.Size(382, 42);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.aLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 30);
            this.panel1.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.isUpper = true;
            this.txtSearch.Location = new System.Drawing.Point(47, 3);
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
            this.txtSearch.Size = new System.Drawing.Size(228, 24);
            this.txtSearch.TabIndex = 3;
            // 
            // aLabel1
            // 
            this.aLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(70)))));
            this.aLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.aLabel1.LabelType = NF.Framework.Win.Controls.LabelType.NONE;
            this.aLabel1.Location = new System.Drawing.Point(14, 6);
            this.aLabel1.Name = "aLabel1";
            this.aLabel1.Size = new System.Drawing.Size(20, 24);
            this.aLabel1.TabIndex = 2;
            this.aLabel1.Text = "검색";
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
            this.aGridM.Location = new System.Drawing.Point(5, 35);
            this.aGridM.MainView = this.gridView1;
            this.aGridM.Name = "aGridM";
            this.aGridM.SetBindingEvnet = true;
            this.aGridM.Size = new System.Drawing.Size(332, 390);
            this.aGridM.TabIndex = 4;
            this.aGridM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.aGridM;
            this.gridView1.Name = "gridView1";
            // 
            // POPUP_COUNTRY
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Name = "POPUP_COUNTRY";
            this.Text = "POPUP_COUNTRY";
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
    }
}