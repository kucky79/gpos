namespace HiNet.Framework.Win.Controls
{
    partial class XTextBox 
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xLabel1 = new HiNet.Framework.Win.Controls.XLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // xLabel1
            // 
            this.xLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.xLabel1.Location = new System.Drawing.Point(0, 0);
            this.xLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.xLabel1.Resource = "";
            this.xLabel1.Size = new System.Drawing.Size(100, 21);
            this.xLabel1.TabIndex = 2;
            this.xLabel1.Text = "xLabel1";
            this.xLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(100, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(184, 21);
            this.textBox1.TabIndex = 3;
            // 
            // XTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.xLabel1);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.Name = "XTextBox";
            this.Size = new System.Drawing.Size(284, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public XLabel xLabel1;
        public System.Windows.Forms.TextBox textBox1;


    }
}
