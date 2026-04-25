using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bifrost.Common;
using Bifrost.Data;
using Bifrost.Win;

namespace Bifrost.Win
{
    public partial class CloseConfirmDialog : Form
    {
        public Form OwnerForm = null;
        public bool IsLogoutClosing = false;

        private bool bIsLoading = false;

        public CloseConfirmDialog()
        {
            InitializeComponent();

            btnYes.Click += btnYes_Click;
            btnNo.Click += btnNo_Click;
            btnClose.Click += BtnClose_Click;

            panelMain.MouseDown += Form1_MouseDown;
            panelMain.MouseMove += Form1_MouseMove;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private Point mousePoint;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X), this.Top - (mousePoint.Y - e.Y));
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bIsLoading) return;
            if (listBoxForm.SelectedIndex == -1) return;

            DataChangedForm df = (DataChangedForm)this.listBoxForm.SelectedItem;
            ((MDIBase)OwnerForm).ActivateTabForm(df.FormKey);
        }

        private void CloseForm_Load(object sender, EventArgs e)
        {
            ResManager resReader = new ResManager();

            ///
            /// load list of 
            /// 
            bool bHasChanged = false;
            bool bShowList = false;

            if (OwnerForm.GetType().IsSubclassOf(typeof(MDIBase)))
            {
                MDIBase baseForm = (MDIBase)OwnerForm;

                if (baseForm.ChildFormNeedPromptCount > 0)
                {
                    DataChangedForm[] changedForms = new DataChangedForm[baseForm.ChildFormNeedPromptCount];

                    int j = 0;
                    for (int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        if (!(Application.OpenForms[i] is FormBase) || Application.OpenForms[i].IsMdiContainer) continue;

                        FormBase fb = (FormBase)Application.OpenForms[i];
                        if (fb.PromptOnClose)
                        {
                            changedForms[j] = new DataChangedForm(fb.Text, fb.MenuData.MenuKey);
                            j++;
                        }
                    }

                    ///
                    /// Bind list of forms
                    /// 
                    bIsLoading = true;
                    listBoxForm.SelectedIndex = -1;
                    listBoxForm.DataSource = changedForms;
                    listBoxForm.DisplayMember = "FormText";
                    listBoxForm.ValueMember = "FormKey";

                    ///
                    /// Set SelectedIndex to current active form if it's in the list
                    ///
                    if(listBoxForm.SelectedValue != null)
                        listBoxForm.SelectedValue = ((MDIBase)OwnerForm).ActiveMdiForm.MenuData.MenuKey;
                    bIsLoading = false;

                    ///
                    /// Show listbox
                    /// 
                    bShowList = listBoxForm.Items.Count != 0;
                    bHasChanged = listBoxForm.Items.Count != 0;
                }

            }
            else if (OwnerForm.GetType().IsSubclassOf(typeof(FormBase)))
            {
                bHasChanged = true;
                bShowList = false;
            }

            if (bShowList)
            {
                panelMain.Visible = false;
               // this.Height -= 80;
                //panel1.Visible = false;
                //panel1.Height = 0;
            }

            lblTitle.Text = IsLogoutClosing ? "로그아웃" : " 프로그램 종료";

            if (bHasChanged)
            {
                lblMessage.Text = resReader.GetString(IsLogoutClosing ? "진행중인 작업이 있습니다.\r\n로그아웃 하시겠습니까?" : "진행중인 작업이 있습니다.\r\n종료하시겠습니까?").Replace("\\r\\n", Environment.NewLine);
            }
            else
            {
                lblMessage.Text = resReader.GetString(IsLogoutClosing ? "로그아웃 하시겠습니까?" : "종료하시겠습니까?").Replace("\\r\\n", Environment.NewLine);
            }

            listBoxForm.Focus();
        }
    }

    struct DataChangedForm
    {
        private string _formText;
        private string _formKey;

        public DataChangedForm(string formText, string formKey)
        {
            this._formText = formText;
            this._formKey = formKey;
        }

        public string FormText { get { return _formText; } }
        public string FormKey { get { return _formKey; } }
    }
}
