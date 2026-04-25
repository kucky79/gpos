using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Bifrost.Data;

namespace Bifrost.Win
{
    public partial class POSMDIBase : FormBase
    {
        /// <summary>
        /// Indicate if child forms need prompt dialog
        /// This property is set in FormBase.PromptOnClose
        /// </summary>
        public int ChildFormNeedPromptCount = 0;

        /// <summary>
        /// Current Active Form
        /// </summary>
        public FormBase ActiveMdiForm { get; set; }

        /// <summary>
        /// This is the flag indicate if the MDIForm is closed completely
        /// </summary>
        protected bool CloseCommited = false;

        /// <summary>
        /// True if this closing System.Windows.Forms is made by the Logout button's click event
        /// </summary>
        protected bool IsLogoutClosing = false;

        /// <summary>
        /// 
        /// </summary>
        public static CommonCodeHelperBase CommonCode;

        public POSGlobalData Global;

        /// <summary>
        /// Only MDI Form will have this property writable
        /// </summary>
        //[Browsable(false)]
        //// public Settings Settings
        //new public Settings Settings
        //{
        //    get
        //    {
        //        return Global.Settings;
        //    }
        //    set
        //    {
        //        Global.Settings = value;
        //    }
        //}

        #region CreateChildForm, CreateChildPopup..

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildForm(MenuData menuData)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildForm(string formNamespace)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <param name="argument"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildForm(string formNamespace, object[] args)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <param name="argument"></param>
        /// <param name="menuKey"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildForm(string formNamespace, object[] args, string menuKey)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildForm(string formAssLocation, string formNamespace)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildPopup(MenuData menuData)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildPopup(string formNamespace)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildPopup(string formAssLocation, string formNamespace)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildForm(MenuData menuData, bool autoShow)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildForm(string formNamespace, bool autoShow)
        {
            return null;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public virtual FormBase CreateChildForm(string formAssLocation, string formNamespace, bool autoShow)
        {
            return null;
        }

        public virtual FormBase CreateChildForm(string formAssLocation, string formNamespace, string MenuCode)
        {
            return null;
        }

        public void RemoveChildForm(FormBase form)
        {
        }

        /// <summary>
        /// Need to implement in MDI Main form
        /// </summary>
        /// <param name="menuKey"></param>
        /// <returns></returns>
        //public virtual MdiTab FindTabByKey(string menuKey)
        //{
        //    return null;
        //}

        /// <summary>
        /// Activate a form by menukey
        /// </summary>
        /// <param name="menuKey"></param>
        /// <returns></returns>
        public virtual bool ActivateTabForm(string menuKey)
        {
            return false;
        }

        #endregion CreateChildForm, CreateChildPopup..

        //public virtual void OnLogoutClick()
        //{
        //    //throw new Exception("The method or operation is not implemented.");
        //}

        public virtual void OnComLogoClick()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual int GetOpenMenuCount()
        {
            return 0;
        }


        public virtual void OnComLogoClick(bool ViewMain)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void SetBackColor(string _Biz)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnHelpClick()
        {
            //throw new Exception("The method or operation is not implemented.");
        }


        public virtual void OnCloseTabAll()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnCloseTab()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnSearchClick(string menuName)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnMyMenuClick(bool showMenu)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnFoundMenuClick()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnMenuClick(bool showMenu)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnMenuPopupClick()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnHomeClick()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnUserClick()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnReConnect()
        {
            
        }

        public virtual void OnReConnect(string connectionString)
        {

        }

        public virtual void OnInsert()
        {
            //A2PFormBase child = (A2PFormBase)this.ActiveMdiChild;
            //if(child!=null)
            //    child.OnInsert();
        }

        public virtual void OnView()
        {
            if (ActiveMdiChild != null && ActiveMdiChild.CompanyName == "EML")
                return;

            //A2PFormBase child = (A2PFormBase)this.ActiveMdiChild;
            //if (child != null)
            //    child.OnView();rjs wjrflq 
        }
        public virtual void OnDelete()
        {
            if (ActiveMdiChild != null && ActiveMdiChild.CompanyName == "EML")
                return;

            //A2PFormBase child = (A2PFormBase)this.ActiveMdiChild;
            //if (child != null)
            //    child.OnDelete();
        }
        public virtual void OnSave()
        {

            if (ActiveMdiChild != null && ActiveMdiChild.CompanyName == "EML")
                return;

            //A2PFormBase child = (A2PFormBase)this.ActiveMdiChild;
            //if (child != null)
            //    child.OnSave();
        }


        public virtual void CloseForm()
        {
        }

        public virtual void OnUserDeptClick()
        {
        }

        public virtual void FormClose()
        {
        }

        public virtual void FormMin()
        {
        }

        public virtual void FormFloat()
        {
        }

        public virtual void FormLogOut()
        {
        }

        public virtual void FormDock(Form _FloatingForm)
        {
        }

        public virtual void ToolBar_MouseDown(MouseEventArgs e)
        {
        }

        public virtual void ToolBar_MouseMove(MouseEventArgs e)
        {
        }

        public virtual void ToolBar_MouseUp(MouseEventArgs e)
        {
        }

        public virtual void FormSizeChange()
        {
        }

        public virtual void OnUpdateStatus(string statusText) { }
        public virtual void OnUpdateProgress(int percentage, bool showPercentage) { }
        [Description("This method is obsolete")]
        public virtual void OnStartProcessing(bool blnStart) { }
        public virtual void OnStartProcessing(string processingMethod, bool blnStart) { }
        public virtual void OnStartProcessing(Form activeForm, string processingMethod, bool blnStart) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuKey"></param>
        /// <returns></returns>
        public FormBase FindFormByKey(string menuKey)
        {
            return (FormBase)Application.OpenForms[menuKey];
        }

        public POSMDIBase()
        {
            try
            {
                Global = new POSGlobalData();
                InitializeComponent();
                //PromptOnClose = true;
            }
            catch(Exception ex)
            {
                HandleWinException(ex);
            }
        }
        private void MDIBase_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!PromptOnClose) return;

            CloseConfirmDialog cfd = new CloseConfirmDialog();
            cfd.OwnerForm = this;
            cfd.IsLogoutClosing = IsLogoutClosing;

            bool bCancel = cfd.ShowDialog(this) != System.Windows.Forms.DialogResult.Yes;
            //IsLogoutClosing
            if (IsLogoutClosing)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = bCancel;
            }

            // commit closing
            CloseCommited = !bCancel;

            // Reset flag
            IsLogoutClosing = false;
        }

    }

}