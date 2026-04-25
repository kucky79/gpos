using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;

using CMAX.Framework.Common;
using CMAX.Framework.Data;
using CMAX.Framework.Win.Controls;
using CMAX.Framework.Win.Validation;
using CMAX.Framework.Win.Util;

namespace CMAX.Framework.Win
{
	public class InputBox : CMAX.Framework.Win.FormBase
	{
		private CMAX.Framework.Win.Controls.XHeaderPanel pnlTop;
		private PictureBox picTitleIcon;
		private Label lblTitle;
		protected System.Windows.Forms.ErrorProvider errorProviderText;
		protected TextBox txtText;

		private System.ComponentModel.IContainer components = null;
		protected Button btnCancel;
		protected Button btnOK;
		
		/// <summary>
		/// Delegate used to validate the object
		/// </summary>
		private InputBoxValidatingHandler _validator;

		public InputBox()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Public
		
		/// <summary>
		/// Displays a prompt in a dialog box, waits for the user to input text or click a button.
		/// </summary>
		/// <param name="prompt">String expression displayed as the message in the dialog box</param>
		/// <param name="title">String expression displayed in the title bar of the dialog box</param>
		/// <param name="defaultResponse">String expression displayed in the text box as the default response</param>
		/// <param name="validator">Delegate used to validate the text</param>
		/// <param name="xpos">Numeric expression that specifies the distance of the left edge of the dialog box from the left edge of the screen.</param>
		/// <param name="ypos">Numeric expression that specifies the distance of the upper edge of the dialog box from the top of the screen</param>
		/// <returns>An InputBoxResult object with the Text and the OK property set to true when OK was clicked.</returns>
		public static InputBoxResult Show(string prompt, string title, string defaultResponse, InputBoxValidatingHandler validator, int xpos, int ypos, bool passwordMode)
		{
			using (InputBox form = new InputBox())
			{
				form.lblTitle.Text = prompt;
				form.Text = title;
				form.txtText.Text = defaultResponse;
				if (passwordMode)
				{
					form.txtText.UseSystemPasswordChar = true;
				}
				if (xpos >= 0 && ypos >= 0)
				{
					form.StartPosition = FormStartPosition.Manual;
					form.Left = xpos;
					form.Top = ypos;
				}
				form.Validator = validator;

				DialogResult result = form.ShowDialog();

				InputBoxResult retval = new InputBoxResult();
				if (result == DialogResult.OK)
				{
					retval.Text = form.txtText.Text;
					retval.OK = true;
				}
				return retval;
			}
		}

		/// <summary>
		/// Displays a prompt in a dialog box, waits for the user to input text or click a button.
		/// </summary>
		/// <param name="prompt">String expression displayed as the message in the dialog box</param>
		/// <param name="title">String expression displayed in the title bar of the dialog box</param>
		/// <param name="defaultResponse">String expression displayed in the text box as the default response</param>
		/// <param name="validator">Delegate used to validate the text</param>
		/// <returns>An InputBoxResult object with the Text and the OK property set to true when OK was clicked.</returns>
		public static InputBoxResult Show(string prompt, string title, string defaultText, InputBoxValidatingHandler validator)
		{
			return Show(prompt, title, defaultText, validator, -1, -1, false);
		}

		#endregion

		#region Privates

		private void ApplyNewSubSystemType(Control c)
		{
			foreach (Control ctrl in c.Controls)
			{
				if (ctrl is IXControl)
				{
					((IXControl)ctrl).OnSubSystemTypeChanged(this.SubSystemType);
				}

				ApplyNewSubSystemType(ctrl);
			}
		}

		/// <summary>
		/// Change SubSystemType
		/// </summary>
		protected override void OnSubSystemTypeChanged()
		{
			this.BackColor = Settings.FormBackColor;
			ApplyNewSubSystemType(this);	
		}

		private void PopupBase_Load(object sender, EventArgs e)
		{
			txtText.Select();
		}

		/// <summary>
		/// Processing shortcut key
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessDialogKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Escape:
					btnCancel_Click(this, EventArgs.Empty);
					break;
				default:
					break;
			}

			return base.ProcessDialogKey(keyData);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.errorProviderText.Clear();
			this.Validator = null;
			this.Close();
		}

		/// <summary>
		/// Reset the ErrorProvider
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtText_TextChanged(object sender, System.EventArgs e)
		{
			errorProviderText.SetError(txtText, "");
		}

		/// <summary>
		/// Validate the Text using the Validator
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtText_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (Validator != null)
			{
				InputBoxValidatingArgs args = new InputBoxValidatingArgs();
				args.Text = txtText.Text;
				Validator(this, args);
				if (args.Cancel)
				{
					e.Cancel = true;
					errorProviderText.SetError(txtText, args.Message);
				}
			}
		}

		protected InputBoxValidatingHandler Validator
		{
			get
			{
				return (this._validator);
			}
			set
			{
				this._validator = value;
			}
		}

		#endregion
		
		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
			this.pnlTop = new CMAX.Framework.Win.Controls.XHeaderPanel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.picTitleIcon = new System.Windows.Forms.PictureBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.errorProviderText = new System.Windows.Forms.ErrorProvider(this.components);
			this.txtText = new System.Windows.Forms.TextBox();
			this.pnlTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picTitleIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProviderText)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlTop
			// 
			this.pnlTop.BorderColor = System.Drawing.Color.Empty;
			this.pnlTop.BorderWidth = new System.Windows.Forms.Padding(0);
			this.pnlTop.Controls.Add(this.lblTitle);
			this.pnlTop.Controls.Add(this.picTitleIcon);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(522, 34);
			this.pnlTop.TabIndex = 0;
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblTitle.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblTitle.Location = new System.Drawing.Point(55, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(467, 34);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Prompt Text";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picTitleIcon
			// 
			this.picTitleIcon.Dock = System.Windows.Forms.DockStyle.Left;
			this.picTitleIcon.Image = global::CMAX.Framework.Win.Properties.Resources.main_titleicon_max;
			this.picTitleIcon.Location = new System.Drawing.Point(0, 0);
			this.picTitleIcon.Name = "picTitleIcon";
			this.picTitleIcon.Size = new System.Drawing.Size(55, 34);
			this.picTitleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picTitleIcon.TabIndex = 0;
			this.picTitleIcon.TabStop = false;
			// 
			// btnCancel
			// 
			this.btnCancel.CausesValidation = false;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(425, 80);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(85, 28);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(334, 80);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(85, 28);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// errorProviderText
			// 
			this.errorProviderText.ContainerControl = this;
			this.errorProviderText.DataMember = "";
			// 
			// txtText
			// 
			this.txtText.Location = new System.Drawing.Point(12, 40);
			this.txtText.Name = "txtText";
			this.txtText.Size = new System.Drawing.Size(490, 21);
			this.txtText.TabIndex = 0;
			this.txtText.UseSystemPasswordChar = true;
			this.txtText.Validating += new System.ComponentModel.CancelEventHandler(this.txtText_Validating);
			this.txtText.TextChanged += new System.EventHandler(this.txtText_TextChanged);
			// 
			// InputBox
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(522, 120);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtText);
			this.Controls.Add(this.pnlTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputBox";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.SubSystemType = CMAX.Framework.Common.SubSystemType.CO;
			this.Text = "InputBox Title";
			this.Click += new System.EventHandler(this.PopupBase_Load);
			this.Load += new System.EventHandler(this.PopupBase_Load);
			this.pnlTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picTitleIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProviderText)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion	
	}


	/// <summary>
	/// Class used to store the result of an InputBox.Show message.
	/// </summary>
	public class InputBoxResult
	{
		public bool OK;
		public string Text;
	}

	/// <summary>
	/// EventArgs used to Validate an InputBox
	/// </summary>
	public class InputBoxValidatingArgs : EventArgs
	{
		public string Text;
		public string Message;
		public bool Cancel;
	}

	/// <summary>
	/// Delegate used to Validate an InputBox
	/// </summary>
	public delegate void InputBoxValidatingHandler(object sender, InputBoxValidatingArgs e);

}

