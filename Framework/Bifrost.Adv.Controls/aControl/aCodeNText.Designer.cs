using DevExpress.XtraEditors;
using System.Drawing;

namespace Bifrost.Adv.Controls
{
    partial class aCodeNText
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this._CodeBox = new TextEdit();
            this._Button = new ButtonEx();
            this._TextBox = new TextEdit();
            this.SuspendLayout();
            // 
            // _CodeBox
            // 
            this._CodeBox.Location = new System.Drawing.Point(0, 0);
            this._CodeBox.Name = "_CodeBox";
            this._CodeBox.Size = new System.Drawing.Size(100, 24);
            this._CodeBox.TabIndex = 0;
            
            
            // 
            // _Button
            // 
            this._Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._Button.Image = Images.btn_search;
            this._Button.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._Button.BackColor = System.Drawing.Color.White;

            this._Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._Button.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D5DEE1");// System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));

            this._Button.FlatAppearance.BorderSize = 1;
            this._Button.Location = new System.Drawing.Point(101, 0);
            this._Button.Name = "_Button";
            this._Button.Size = new System.Drawing.Size(24, 24);
            this._Button.TabIndex = 1;
            this._Button.TabStop = false;
            this._Button.UseVisualStyleBackColor = false;
            
            // 
            // _TextBox
            // 
            this._TextBox.BackColor = System.Drawing.SystemColors.Control;
            this._TextBox.Location = new System.Drawing.Point(130, 0);
            this._TextBox.Name = "_TextBox";
            this._TextBox.ReadOnly = true;
            this._TextBox.Size = new System.Drawing.Size(123, 24);
            this._TextBox.TabIndex = 2;
            this._TextBox.TabStop = false;
            // 
            // aCodeNText
            // 
            this.Controls.Add(this._CodeBox);
            this.Controls.Add(this._Button);
            this.Controls.Add(this._TextBox);
            this.Size = new System.Drawing.Size(252, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

            this.TabStop = false;

        }

        #endregion

        public ButtonEx _Button;
        public TextEdit _CodeBox;
        public TextEdit _TextBox;
    }
}
