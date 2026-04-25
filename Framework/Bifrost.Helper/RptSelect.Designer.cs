namespace Bifrost.Helper
{
    partial class RptSelect
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
            this.aGrid1 = new Bifrost.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.aGrid1);
            this.pnlBound.Padding = new System.Windows.Forms.Padding(0);
            this.pnlBound.Size = new System.Drawing.Size(458, 265);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 353);
            this.pnlBottom.Size = new System.Drawing.Size(498, 46);
            // 
            // aGrid1
            // 
            this.aGrid1.AddNewRowLastColumn = true;
            this.aGrid1.disabledEditingColumns = new string[0];
            this.aGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aGrid1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.aGrid1.isSaveLayout = false;
            this.aGrid1.isUpper = false;
            this.aGrid1.LayoutVersion = "";
            this.aGrid1.Location = new System.Drawing.Point(0, 0);
            this.aGrid1.MainView = this.gridView1;
            this.aGrid1.Name = "aGrid1";
            this.aGrid1.SetBindingEvnet = true;
            this.aGrid1.Size = new System.Drawing.Size(458, 265);
            this.aGrid1.TabIndex = 0;
            this.aGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.aGrid1;
            this.gridView1.Name = "gridView1";
            // 
            // RptSelect
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Name = "RptSelect";
            this.Text = "Report Choice";
            this.pnlBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Grid.aGrid aGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}