#region Using directives

using System;
using System.Windows.Forms;

using Bifrost.Common;
using Bifrost.Data;
using Bifrost.Win.Data;
using System.Drawing;
using System.Collections.Generic;
using DevExpress.XtraEditors;

#endregion

namespace Bifrost.Win
{
    public partial class MsgDialog : FormBase
    {
        #region
        private Point mousePoint;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(Left - (mousePoint.X - e.X), Top - (mousePoint.Y - e.Y));
            }
        }

        #endregion
        private MessageType _MsgType = MessageType.Information;
        private object _MsgObject = null;
        private bool _UseMsgCode = true;
        private int _MsgShowType = 2;
        private bool isClosedByButtonClick = false;


        private void InitEvent()
        {
            btnOKYes.Click += BtnOKYes_Click;
            btnYesNo.Click += BtnYesNo_Click;
            btnNoCancel.Click += BtnNoCancel_Click;
            btnClose.Click += BtnClose_Click;
            llblShowDetails.Click += LlblShowDetails_Click;
            lblTitle.MouseDown += Form1_MouseDown;
            lblTitle.MouseMove += Form1_MouseMove;
            KeyDown += MsgDialog_KeyDown;
        }

        private void LlblShowDetails_Click(object sender, EventArgs e)
        {
            ExceptionInfoDialog eid = new ExceptionInfoDialog();
            if (_MsgObject is UIException)
            {
                Exception ex = ((UIException)_MsgObject).InnerException;
                eid.ShowException(ex == null ? (Exception)_MsgObject : ex, _MsgShowType);
            }
            else
            {
                eid.ShowException((Exception)_MsgObject, _MsgShowType);
            }

            eid.ShowDialog(this);

            //Exception ex = (Exception)_MsgObject;
            //ex.GetBaseException
        }

        private void MsgDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnYesNo_Click(object sender, EventArgs e)
        {
            isClosedByButtonClick = true;
            DialogResult = GetDialogResult((SimpleButton)sender);
        }

        private void BtnNoCancel_Click(object sender, EventArgs e)
        {
            isClosedByButtonClick = true;
            DialogResult = GetDialogResult((SimpleButton)sender);
        }

        private void BtnOKYes_Click(object sender, EventArgs e)
        {
            isClosedByButtonClick = true;
            DialogResult = GetDialogResult((SimpleButton)sender);
        }


        private void FormInitialize()
        {
            InitEvent();

            string msgCaption = string.Empty;
            string msgText = string.Empty;
            Color subjectColor = new Color();


            if (_MsgObject is Exception)
                msgText = UIException.ParseException((Exception)_MsgObject, out _MsgShowType);
            else
                msgText = _UseMsgCode ? ResReader.GetString((string)_MsgObject) : (string)_MsgObject;

            switch (_MsgType)
            {
                case MessageType.Question:
                    btnOKYes.Visible = false;

                    btnYesNo.Text = "&예";
                    btnNoCancel.Text = "&아니오";

                    AcceptButton = btnYesNo;
                    CancelButton = btnNoCancel;

                    btnYesNo.Location = new System.Drawing.Point(112, 368);
                    btnNoCancel.Location = new System.Drawing.Point(228, 368);

                    llblShowDetails.Visible = false;
                    msgCaption = ResReader.GetString("Confirmation");
                    subjectColor = Color.FromArgb(211, 89, 65);

                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_question_icon;
                    btnNoCancel.Focus();
                    break;

                case MessageType.YesNoCancel:
                    btnOKYes.Text = "&예";
                    btnYesNo.Text = "&아니오";
                    btnNoCancel.Text = "&취소";

                    AcceptButton = btnOKYes;
                    CancelButton = btnNoCancel;

                    btnOKYes.Location = new System.Drawing.Point(54, 368);
                    btnYesNo.Location = new System.Drawing.Point(170, 368);
                    btnNoCancel.Location = new System.Drawing.Point(286, 368);

                    llblShowDetails.Visible = false;
                    msgCaption = ResReader.GetString("Confirmation");
                    subjectColor = Color.FromArgb(211, 89, 65);

                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_question_icon;
                    break;

                case MessageType.Error:
                    btnYesNo.Visible = btnNoCancel.Visible = false;
                    btnOKYes.Text = "&확인";

                    AcceptButton = btnOKYes;
                    CancelButton = null;

                    btnOKYes.Location = new System.Drawing.Point(170, 368);

                    llblShowDetails.Visible = _MsgObject is Exception;
                    msgCaption = ResReader.GetString("Error");
                    subjectColor = Color.FromArgb(26, 170, 143);

                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_error_icon;
                    break;

                case MessageType.Warning:
                    btnYesNo.Visible = btnNoCancel.Visible = false;
                    btnOKYes.Text = "&확인";
                    AcceptButton = btnOKYes;
                    CancelButton = null;

                    btnOKYes.Location = new System.Drawing.Point(170, 368);

                    llblShowDetails.Visible = _MsgObject is Exception;
                    msgCaption = ResReader.GetString("Warning");
                    subjectColor = Color.FromArgb(245, 160, 75);

                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_notice_icon;
                    break;

                default:
                    btnYesNo.Visible = btnNoCancel.Visible = false;
                    btnOKYes.Text = "&확인";
                    AcceptButton = btnOKYes;
                    CancelButton = null;

                    btnOKYes.Location = new System.Drawing.Point(170, 368);

                    llblShowDetails.Visible = false;
                    msgCaption = ResReader.GetString("Information");
                    subjectColor = Color.FromArgb(20, 150, 198);

                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_information_icon;
                    break;
            }

            lblMessage.Text = msgText.Replace("\\r\\n", Environment.NewLine).Replace("'", "′");
            this.lblTitle.ForeColor = subjectColor;

            Text = msgCaption;
            lblTitle.Text = msgCaption;
        }

        private DialogResult GetDialogResult(SimpleButton btn)
        {
            switch (btn.Text)
            {
                case "&확인":
                    return DialogResult.OK;
                case "&예":
                    return DialogResult.Yes;
                case "&아니오":
                    return DialogResult.No;
                case "&취소":
                    return DialogResult.Cancel;
                default:
                    return DialogResult.Cancel;
            }
        }

        public MsgDialog(MessageType msgType, object msgObject)
        {
            InitializeComponent();

            _MsgType = msgType;
            _MsgObject = msgObject;

            FormInitialize();
        }
        public MsgDialog(string language, MessageType msgType, object msgObject)
        {
            InitializeComponent();

            _MsgType = msgType;
            _MsgObject = msgObject;

            FormInitialize();
        }
        public MsgDialog(string msgText, MessageType msgType)
        {
            InitializeComponent();
             
            _MsgType = msgType;
            _MsgObject = msgText.Replace("'", "′");
            _UseMsgCode = false;

            FormInitialize();
        }

        void MsgDialog_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (!isClosedByButtonClick)
            {
                DialogResult = (_MsgType == MessageType.Question) ? DialogResult.No : (_MsgType == MessageType.YesNoCancel ? DialogResult.Cancel : DialogResult.OK);
            }
        }


        private void llblShowDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ExceptionInfoDialog eid = new ExceptionInfoDialog();
            eid.ShowException((Exception)_MsgObject);
            eid.ShowDialog(this);
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;//= GetDialogResult((Button)sender);
                    //BtnNoCancel_Click(this, EventArgs.Empty);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

    }
}