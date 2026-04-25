using Bifrost.Common;
using Bifrost.Win.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Win
{
    public partial class MsgDetailDialog : FormBase
    {

        private MessageType _MsgType = MessageType.Information;
        private object _MsgObject = null;
        private object _MsgObjectDetail = null;

        private bool _UseMsgCode = true;
        private int _MsgShowType = 2;
        private bool isClosedByButtonClick = false;

        public MsgDetailDialog()
        {
            InitializeComponent();
            FormInitialize();
            InitEvent();
        }

        public MsgDetailDialog(MessageType msgType, object msgObject)
        {
            InitializeComponent();

            _MsgType = msgType;
            _MsgObject = msgObject;

            FormInitialize();
            InitEvent();
        }
       
        public MsgDetailDialog(string msgText, MessageType msgType)
        {
            InitializeComponent();

            _MsgType = msgType;
            _MsgObject = msgText;
            _UseMsgCode = false;

            FormInitialize();
            InitEvent();
        }

        public MsgDetailDialog(string msgText, string msgDetailText, MessageType msgType)
        {
            InitializeComponent();

            _MsgType = msgType;
            _MsgObject = msgText;
            _MsgObjectDetail = msgDetailText;
            _UseMsgCode = false;

            FormInitialize();
            InitEvent();
        }

        private void InitEvent()
        {
            btnOKYes.Click += btnOKYes_Click;
            btnYesNo.Click += btnYesNo_Click;
            btnNoCancel.Click += btnNoCancel_Click;
            btnClose.Click += BtnClose_Click;
            llblShowDetails.Click += LlblShowDetails_Click;
            lblTitle.MouseDown += Form_MouseDown;
            lblTitle.MouseMove += Form_MouseMove;
        }

        private void LlblShowDetails_Click(object sender, EventArgs e)
        {
            //ExceptionInfoDialog eid = new ExceptionInfoDialog();
            //eid.ShowException((Exception)_MsgObject);
            //eid.ShowDialog(this);

            if (txtDetail.Visible)
            {
                txtDetail.Visible = false;
                this.Height = 225;
                llblShowDetails.Text = "Show Details...";
            }
            else
            {
                txtDetail.Visible = true;
                this.Height = 450;
                llblShowDetails.Text = "Close Details...";
            }
        }

        private void FormInitialize()
        {
            string msgCaption = string.Empty;
            string msgText = string.Empty;
            string msgDetailText = string.Empty;
            Color subjectColor = new Color();

            if (_MsgObject is Exception)
                msgText = UIException.ParseException((Exception)_MsgObject, out _MsgShowType);
            else
                msgText = _UseMsgCode ? ResReader.GetString((string)_MsgObject) : (string)_MsgObject;

            txtDetail.EditValue = _MsgObjectDetail;

            switch (_MsgType)
            {
                case MessageType.Question:
                    btnOKYes.Visible = false;

                    btnYesNo.Text = "&Yes";
                    btnNoCancel.Text = "&No";

                    this.AcceptButton = btnYesNo;
                    this.CancelButton = btnNoCancel;

                    btnYesNo.Location = new System.Drawing.Point(112, 368);
                    btnNoCancel.Location = new System.Drawing.Point(228, 368);

                    msgCaption = ResReader.GetString("Confirmation");
                    subjectColor = Color.FromArgb(211, 89, 65); 
                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_question_icon;
                    break;

                case MessageType.YesNoCancel:
                    btnOKYes.Text = "&Yes";
                    btnYesNo.Text = "&No";
                    btnNoCancel.Text = "&Cancel";

                    this.AcceptButton = btnOKYes;
                    this.CancelButton = btnNoCancel;

                    btnOKYes.Location = new System.Drawing.Point(54, 368);
                    btnYesNo.Location = new System.Drawing.Point(170, 368);
                    btnNoCancel.Location = new System.Drawing.Point(286, 368);

                    msgCaption = ResReader.GetString("Confirmation");
                    subjectColor = Color.FromArgb(211, 89, 65);
                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_question_icon;
                    break;

                case MessageType.Error:
                    btnYesNo.Visible = btnNoCancel.Visible = false;
                    btnOKYes.Text = "&OK";

                    this.AcceptButton = btnOKYes;
                    this.CancelButton = null;

                    btnOKYes.Location = new System.Drawing.Point(170, 368);
                    msgCaption = ResReader.GetString("Error");
                    subjectColor = Color.FromArgb(26, 170, 143);
                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_error_icon;
                    break;

                case MessageType.Warning:
                    btnYesNo.Visible = btnNoCancel.Visible = false;
                    btnOKYes.Text = "&OK";
                    this.AcceptButton = btnOKYes;
                    this.CancelButton = null;

                    btnOKYes.Location = new System.Drawing.Point(170, 368);

                    msgCaption = ResReader.GetString("Warning");
                    subjectColor = Color.FromArgb(245, 160, 75);

                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_notice_icon;
                    break;

                default:
                    btnYesNo.Visible = btnNoCancel.Visible = false;
                    btnOKYes.Text = "&OK";
                    this.AcceptButton = btnOKYes;
                    this.CancelButton = null;

                    btnOKYes.Location = new System.Drawing.Point(170, 368);

                    msgCaption = ResReader.GetString("Information");
                    subjectColor = Color.FromArgb(20, 150, 198);

                    MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_information_icon;
                    break;
            }

            this.lblMessage.Text = msgText.Replace("\\r\\n", Environment.NewLine);
            this.lblTitle.ForeColor = subjectColor;
            this.lblTitle.Text = msgCaption;
        }

        private DialogResult GetDialogResult(Button btn)
        {
            switch (btn.Tag)
            {
                case "&OK":
                    return DialogResult.OK;
                case "&Yes":
                    return DialogResult.Yes;
                case "&No":
                    return DialogResult.No;
                case "&Cancel":
                    return DialogResult.Cancel;
                default:
                    return DialogResult.Cancel;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnYesNo_Click(object sender, EventArgs e)
        {
            isClosedByButtonClick = true;
            this.DialogResult = GetDialogResult((Button)sender);
        }

        private void btnNoCancel_Click(object sender, EventArgs e)
        {
            isClosedByButtonClick = true;
            this.DialogResult = GetDialogResult((Button)sender);
        }

        private void btnOKYes_Click(object sender, EventArgs e)
        {
            isClosedByButtonClick = true;
            this.DialogResult = GetDialogResult((Button)sender);
        }

        private void MsgDetailDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isClosedByButtonClick)
            {
                this.DialogResult = (_MsgType == MessageType.Question) ?
                    DialogResult.No : (_MsgType == MessageType.YesNoCancel ? DialogResult.Cancel : DialogResult.OK);
            }
        }

        private Point mousePoint;
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X), this.Top - (mousePoint.Y - e.Y));
            }
        }

    }
}
