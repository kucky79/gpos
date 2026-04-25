using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bifrost.Win
{
    public partial class POSPopupBase : FormBase
    {
        private bool _AutoSearch = false;
        private bool _isSizeable = false;


        #region Properties
        /// <summary>
        /// Title in form and SubTitle
        /// </summary>
        [DefaultValue(typeof(string), ""), Description("Set Title")]
        private string _popupTitle = string.Empty;
        public string PopupTitle
        {
            get { return _popupTitle; }
            set
            {
                _popupTitle = value;
                this.Text = value + " - Popup";
                lblTitle.Text = value;
            }
        }

        public bool AutoSearch
        {
            get { return _AutoSearch; }
            set
            {
                _AutoSearch = value;
                if (_AutoSearch)
                {
                    this.OnSearch();
                }
            }
        }

      
        public bool isSizeable
        {
            get { return _isSizeable; }
            set
            {
                _isSizeable = value;
                if (value)
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                else
                    this.FormBorderStyle = FormBorderStyle.None;
            }
        }

        private bool _isTopVisible = true;
        public bool IsTopVisible
        {
            get { return _isTopVisible; }
            set
            {
                _isTopVisible = value;
                pnlTop.Visible = value;
            }
        }

        private bool _TaskbarVisible = false;

        [DefaultValue(typeof(bool), "false"), Description("Set Taskbar Visible.")]
        public bool TaskbarVisible
        {
            get
            {
                ShowInTaskbar = _TaskbarVisible;
                return _TaskbarVisible;
            }
            set
            {
                _TaskbarVisible = value;
                ShowInTaskbar = _TaskbarVisible;
            }
        }

        #endregion Properties

        
        public POSPopupBase()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            InitEvent();

            //작업표시줄에서 없애기
            this.ShowInTaskbar = false;

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
        }

        private void InitEvent()
        {
            this.btnClose.Click += BtnClose_Click;

            //상단영역 이동 가능
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseMove);
        }

        #region Public Overriding

        #endregion

        #region Privates

        /// <summary>
        /// Processing shortcut key
        /// </summary>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F5:
                    btnSearch_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F12:
                    btnSave_Click(this, EventArgs.Empty);
                    return true;
                case Keys.Escape:
                    btnCancel_Click(this, EventArgs.Empty);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            OnSave();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            OnOK();

            ///
            /// return
            /// 
            //this.DialogResult = DialogResult.OK;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            OnCancel();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSave();

            ///
            /// return
            /// 
            //this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel();
        }

        #endregion

        #region Must Overrides

        /// <summary>
        /// When clicking on Search button.
        /// </summary>
        protected virtual void OnSearch() { }
        /// <summary>
        /// When clicking on OK button, set data to return to caller form
        /// this.ReturnData = dataset;
        /// </summary>
        protected virtual void OnOK() { }
        /// <summary>
        /// When clicking on Cancel button
        /// </summary>
        protected virtual void OnCancel()
        {
            if (this.Modal)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
		/// When clicking on Save button
		/// </summary>
		protected virtual void OnSave() { }

        /// <summary>
        /// Get the data return on Enter
        /// </summary>
        public Hashtable ReturnData { get; set; } = new Hashtable();

        #endregion


        private Point mousePoint;

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X), this.Top - (mousePoint.Y - e.Y));
            }
        }
    }
}
