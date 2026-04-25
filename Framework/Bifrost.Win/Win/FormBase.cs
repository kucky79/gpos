using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using Bifrost;
using Bifrost.Data;
using Bifrost.Grid;
using Bifrost.Common;
using Bifrost.Win.Data;
using Bifrost.Win.Controls;

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;

namespace Bifrost.Win
{
    public partial class FormBase : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Privates

        protected virtual void OnCustomFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this.MdiForm != null)
                {
                    if (this.PromptOnClose)
                        this.MdiForm.ChildFormNeedPromptCount--;
                    //this.MdiForm.OnUpdateStatus(string.Empty);
                    this.MdiForm.RemoveChildForm(this);
                }
            }
            catch { }
        }

        #endregion Privates

        #region Form Validation

        private Hashtable tableValidators = new Hashtable();
        private bool bControlValidated = true;
        private bool bCustomValidated = true;

        protected void StartValidate()
        {
            bControlValidated = true;
            bCustomValidated = true;

            FormValidatingEventArgs e = new FormValidatingEventArgs(true);
            RaiseFormValidatingEvent(e);
            bCustomValidated = e.IsValid;

            this.Validate();
            this.ValidateChildren();
        }

        #region FormValidating Event

        public event FormValidatingEventHandler FormValidating;
        protected void RaiseFormValidatingEvent(FormValidatingEventArgs e)
        {
            FormValidating?.Invoke(e);
        }

        #endregion

        #region IsValidated

        /// <summary>
        /// Validation status
        /// </summary>
        [Description("Validation status")]
        public bool IsValidated
        {
            get
            {
                return bControlValidated && bCustomValidated && IsTablesValidated();
            }
        }

        #endregion

        #region RegisterColumnValidation - DataTable / DataColumn

        /// <summary>
        /// Add a validation rule
        /// </summary>
        /// <param name="tableName">DataTable</param>
        /// <param name="columnName"></param>
        /// <param name="valueToValidate">If the object has this value, this means error </param>
        protected void RegisterColumnValidation(DataTable dataTable, string columnName, object valueToValidate)
        {
            string groupKey = GenerateValidationGroupKey(dataTable);
            RegisterValidation(groupKey, columnName, valueToValidate, "Error");
        }

        /// <summary>
        /// Add a validation rule
        /// </summary>
        /// <param name="tableName">DataTable</param>
        /// <param name="columnName"></param>
        /// <param name="valueToValidate">If the object has this value, this means error </param>
        /// <param name="errorMessage">Message to display when mouse is on the exclamation icon</param>
        protected void RegisterColumnValidation(DataTable dataTable, string columnName, object valueToValidate, string errorMessage)
        {
            string groupKey = GenerateValidationGroupKey(dataTable);
            RegisterValidation(groupKey, columnName, valueToValidate, errorMessage);
        }

        /// <summary>
        /// Handle cell change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTableCellValueChanging(object sender, DataColumnChangeEventArgs e)
        {
            ValidateCell((DataTable)sender, e.Column.ColumnName, e.Row, e.ProposedValue);
            UpdateTableValidationStatus((DataTable)sender, !e.Row.Table.HasErrors);
        }

        /// <summary>
        /// Handle row change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTableRowChanging(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Delete)
                e.Row.ClearErrors();
            else
                if (ValidateRow((DataTable)sender, e.Row)) e.Row.ClearErrors();

            UpdateTableValidationStatus((DataTable)sender, !e.Row.Table.HasErrors);
        }

        /// <summary>
        /// On Deleting a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTableRowDeleted(object sender, DataRowChangeEventArgs e)
        {
            e.Row.ClearErrors();
            UpdateTableValidationStatus((DataTable)sender, !e.Row.Table.HasErrors);
        }

        #endregion RegisterColumnValidation - DataTable / DataColumn

        #endregion

        #region Privates Validation

        /// <summary>
        /// Generate unique key for each dataTable-type validation group
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private string GenerateValidationGroupKey(DataTable dataTable)
        {
            string groupKey = string.Concat(dataTable.TableName, "_", dataTable.Columns.Count.ToString());
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                groupKey += "_";
                groupKey += dataTable.Columns[i].ColumnName;
            }

            return groupKey;
        }

        /// <summary>
        /// General function to register validation
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="subKey"></param>
        /// <param name="valueToValidate"></param>
        /// <param name="errorMessage"></param>
        private void RegisterValidation(string groupKey, string subKey, object valueToValidate, string errorMessage)
        {
            Hashtable columns;
            if (!tableValidators.ContainsKey(groupKey))
            {
                columns = new Hashtable();
                columns.Add("IsValid", true);
                tableValidators.Add(groupKey, columns);
            }
            else
            {
                columns = (Hashtable)tableValidators[groupKey];
            }

            if (columns == null)
            {
                columns = new Hashtable();
                columns.Add("IsValid", true);
                tableValidators[groupKey] = columns;
            }

            object[] values = new object[] { valueToValidate, errorMessage };
            if (columns.ContainsKey(subKey))
                columns[subKey] = values;
            else
                columns.Add(subKey, values);

            ///
            /// First registration, it is valid
            /// 
            columns["IsValid"] = true;
        }

        #region IsTablesValidated

        /// <summary>
        /// Get all validation count
        /// </summary>
        /// <returns></returns>
        private bool IsTablesValidated()
        {
            bool isValid = true;
            IDictionaryEnumerator en = (IDictionaryEnumerator)tableValidators.GetEnumerator();
            while (en.MoveNext())
            {
                string groupKey = Convert.ToString(en.Key);
                Hashtable columns = (Hashtable)en.Value;
                if (columns == null) continue;
                isValid = isValid && (bool)columns["IsValid"];
            }

            return isValid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="isValid"></param>
        private void UpdateTableValidationStatus(DataTable dataTable, bool isValid)
        {
            string groupKey = GenerateValidationGroupKey(dataTable);
            Hashtable columns = (Hashtable)tableValidators[groupKey];
            if (columns == null) return;
            columns["IsValid"] = isValid;
            tableValidators[groupKey] = columns;
        }

        #endregion

        #region ValidateItem

        private bool ValidateItem(DataTable dataTable, string columnName, object proposedValue, ref string errorMessage)
        {
            string groupKey = GenerateValidationGroupKey(dataTable);
            return ValidateItem(groupKey, columnName, proposedValue, ref errorMessage);
        }

        private bool ValidateItem(string groupKey, string subKey, object proposedValue)
        {
            string errorMessage = string.Empty;
            return ValidateItem(groupKey, subKey, proposedValue, ref errorMessage);
        }

        private bool ValidateItem(string groupKey, string subKey, object proposedValue, ref string errorMessage)
        {
            Hashtable columns = (Hashtable)tableValidators[groupKey];
            if (columns == null)
            {
                return true;
            }

            if (!columns.ContainsKey(subKey)) return true;

            object[] values = (object[])columns[subKey];
            if (errorMessage != null) errorMessage = Convert.ToString(values[1]);
            return ValidateItem(proposedValue, values[0]);
        }

        private bool ValidateItem(object proposedValue, object valueToValidate)
        {
            string sValueToValidate = Convert.ToString(valueToValidate);
            string sProposedValue = Convert.ToString(proposedValue);
            return !sProposedValue.Equals(sValueToValidate);
        }

        #endregion ValidateItem

        #region ValidateCell & ValidateRow

        /// <summary>
        /// Validate value of a column in datatable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <param name="dataRow"></param>
        /// <param name="proposedValue"></param>
        /// <returns></returns>
        private bool ValidateCell(DataTable dataTable, string columnName, DataRow dataRow, object proposedValue)
        {

            string groupKey = GenerateValidationGroupKey(dataTable);
            Hashtable columns = (Hashtable)tableValidators[groupKey];
            if (columns == null)
            {
                return true;
            }

            if (!columns.ContainsKey(columnName)) return true;

            if (!dataTable.Columns.Contains(columnName)) return true;

            // clear error			
            dataRow.SetColumnError(dataRow.Table.Columns[columnName], "");

            bool bIsValidated = true;
            object[] values = (object[])columns[columnName];
            if (!ValidateItem(proposedValue, values[0]))
            {
                dataRow.SetColumnError(dataRow.Table.Columns[columnName], Convert.ToString(values[1]));
                bIsValidated = false;
            }


            //else
            //{
            //    dataRow.ClearErrors();
            //}

            return bIsValidated;
        }

        /// <summary>
        /// Validate a row in datatable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private bool ValidateRow(DataTable dataTable, DataRow dataRow)
        {
            if (dataRow.RowState == DataRowState.Deleted) return true;

            string groupKey = GenerateValidationGroupKey(dataTable);

            Hashtable columns = (Hashtable)tableValidators[groupKey];
            if (columns == null)
            {
                return true;
            }

            IDictionaryEnumerator ide = (IDictionaryEnumerator)columns.GetEnumerator();

            bool bIsValidated = true;
            while (ide.MoveNext())
            {
                string columnName = ide.Key.ToString();
                if (columnName == "IsValid") continue;
                if (!dataTable.Columns.Contains(columnName)) continue;

                object[] values = (object[])ide.Value;
                dataRow.SetColumnError(dataRow.Table.Columns[columnName], "");

                if (!ValidateItem(dataRow[columnName], values[0]))
                {
                    string errorMessage = Convert.ToString(values[1]);
                    dataRow.SetColumnError(dataRow.Table.Columns[columnName], errorMessage);
                    bIsValidated = false;
                }
            }

            return bIsValidated;
        }

        #endregion ValidateCell & ValidateRow

        #endregion

        #region Protected, Public
        public string MenuKey { get; set; } = string.Empty;

        public virtual bool isFloating { get; set; } = false;

        public TabOrderManager.TabScheme AutoTabOrder { get; set; } = TabOrderManager.TabScheme.AcrossFirst;

        public bool ShouldSerializeAutoTabOrder()
        {
            return AutoTabOrder != TabOrderManager.TabScheme.AcrossFirst;
        }

        private FormBaseCollection _TabForms;

        /// <summary>
        /// List of child forms
        /// </summary>
        [Description("This property is obsolute. Dont use it lately")]
        public FormBaseCollection TabForms
        {
            get
            {
                if (_TabForms == null) _TabForms = new FormBaseCollection();
                return _TabForms;
            }
            set { _TabForms = value; }
        }

		/// <summary>
		/// 
		/// </summary>
		//[Browsable(false)]
		//public virtual GlobalSettings GlobalSettings
		//{
		//	get
		//	{
		//		GlobalSettings gs = null;
		//		if (this.IsMdiContainer)
		//		{
		//			gs = ((MDIBase)this).Global.GlobalSettings;
		//		}
		//		else
		//		{
		//			if (this.MdiForm != null)
		//				gs = this.MdiForm.Global.GlobalSettings;
		//		}
		//		if (gs == null)
		//			gs = new GlobalSettings();
		//		return gs;
		//	}
		//}
		
		private bool _PromptOnClose = false;

		/// <summary>
		/// true: Show confirmation on closing, false: no confirmation dialog
		/// </summary>
        [Browsable(false)]
        public bool PromptOnClose
        {
            get { return _PromptOnClose; }
            set
            {

                if (this.IsMdiContainer)
                {
                    _PromptOnClose = value;
                    return;
                }

                if (value && !_PromptOnClose)
                {
                    if(MdiForm != null)
                        MdiForm.ChildFormNeedPromptCount++;
                }
                else if (!value && _PromptOnClose)
                {
                    if (MdiForm != null)
                        MdiForm.ChildFormNeedPromptCount--;
                }

                _PromptOnClose = value;
            }
        }


        private MenuData _MenuData;

        [Browsable(false)]
        public MenuData MenuData
        {
            get { return _MenuData; }
            set { _MenuData = value; OnMenuDataChanged(); }
        }

        [Browsable(false)]
        public MDIBase MdiForm
        {
            get
            {
                try
                {
                    return (MDIBase)Application.OpenForms[0];
                }
                catch
                {
                    return null;
                }

            }
        }


		private SubSystemType _SubSystemType = SubSystemType.MAS;

		/// <summary>
		/// SubSystem
		/// </summary>
		public SubSystemType SubSystemType
		{
			get { return _SubSystemType; }
			set
			{
				_SubSystemType = value;

				///
				/// Design init
				/// 
				OnSubSystemTypeChanged();

				///
				/// Redraw
				/// 
				Invalidate();

			}
		}

		/// <summary>
		/// Apply style if changing the subsystem type
		/// </summary>
		protected virtual void OnSubSystemTypeChanged()
		{	
		}
		
         /// <summary>
        /// Contain settings about style for all controls on Form
        /// </summary>
        [Browsable(false)]
        public virtual FormSettings formSettings
        {
            get
            {
                FormSettings fs = null;
                if (MdiForm == null)
                {
                    fs = new FormSettings(this.SubSystemType);
                }
                else
                {
                    fs = (FormSettings)MdiForm.Global.SharedData[this.SubSystemType.ToString() + "Settings"];

                    if (fs == null)
                    {
                        fs = new FormSettings(this.SubSystemType);
                        MdiForm.Global.SharedData[this.SubSystemType.ToString() + "Settings"] = fs;
                    }
                }

                return fs;
            }
        }

        protected ResManager ResReader;

        protected virtual void OnMenuDataChanged()
        {
        }

		/// <summary>
		/// Display message with provided code when error cannot be analyzed
		/// </summary>
		/// <param name="ex"></param>
        protected virtual void HandleWinException(UIException ex)
        {
            string StoreCode, UserID;
            if (Global.FirmCode == null || Global.FirmCode == string.Empty)
            {
                StoreCode = POSGlobal.StoreCode;
                UserID = POSGlobal.UserID;
            }
            else
            {
                StoreCode = Global.FirmCode;
                UserID = Global.UserID;
            }

            DBHelper.ExecuteNonQuery("USP_SYS_ERROR_LOG_I", new object[] { StoreCode, ex.ToString(), UserID });
            new MsgDialog(MessageType.Error, ex).ShowDialog(this.MdiForm);
        }

		/// <summary>
		/// Display default message when error cannot be analyzed
		/// </summary>
		/// <param name="ex"></param>
        protected virtual void HandleWinException(Exception ex)
        {
            string StoreCode, UserID;
            if (Global.FirmCode == null || Global.FirmCode == string.Empty)
            {
                StoreCode = POSGlobal.StoreCode;
                UserID = POSGlobal.UserID;
            }
            else
            {
                StoreCode = Global.FirmCode;
                UserID = Global.UserID;
            }

            DBHelper.ExecuteNonQuery("USP_SYS_ERROR_LOG_I", new object[] { StoreCode, ex.ToString(), UserID });
            new MsgDialog(MessageType.Error, new UIException(ex)).ShowDialog(this.MdiForm);
        }

        /// <summary>
        /// Return Webservice format
        /// </summary>
        /// <param name="mainUrl"></param>
        /// <param name="partName"></param>
        protected string GetWebServiceUrl(string mainUrl, string partName)
        {
            return string.Concat(mainUrl.Substring(0, mainUrl.LastIndexOf(".")), "_", partName, mainUrl.Substring(mainUrl.LastIndexOf(".")));
        }

        /// <summary>
        /// Show messagebox by msgCode (resource id)
        /// </summary>
        /// <param name="msgCode">MessageCode</param>
        /// <param name="msgType"></param>
        public virtual DialogResult ShowMessageBox(string msgCode, MessageType msgType)
        {
            return new MsgDialog(msgType, msgCode).ShowDialog(this);
        }

        /// <summary>
        /// Show messagebox by text
        /// </summary>
        /// <param name="msgCode">MessageText</param>
        /// <param name="msgType"></param>
        public virtual DialogResult ShowMessageBoxA(string messageText, MessageType msgType)
        {
            //메시지는 전부 MAS 로 저장
            //messageText = ResReader.GetString(Global.Language, "MAS", messageText);
            return new MsgDialog(messageText, msgType).ShowDialog(this);
        }

        /// <summary>
        /// Show messagebox by text
        /// </summary>
        /// <param name="msgCode">MessageText</param>
        /// <param name="msgType"></param>
        public virtual DialogResult ShowMessageBoxDetail(string messageText, string messageDetailText, MessageType msgType)
        {
            //메시지는 전부 MAS 로 저장
            //messageText = ResReader.GetString(Global.Language, "MAS", messageText);
            //디테일 메시지는 저장않함
            return new MsgDetailDialog(messageText, messageDetailText, msgType).ShowDialog(this);
        }

        /// <summary>
        /// Show a confirmation dialog (Y/N) when user closes the current form
        /// </summary>
        /// <param name="dataChanged"></param>
        /// <returns></returns>
        public virtual bool ShowConfirmationBox(bool dataChanged, CloseReason reason)
        {
            if (!dataChanged) return false;
            PromptOnClose = dataChanged;

            if (reason == CloseReason.UserClosing)
            {
                CloseConfirmDialog cfd = new CloseConfirmDialog();
                cfd.OwnerForm = this;
                return cfd.ShowDialog(this) == System.Windows.Forms.DialogResult.No;
            }
            else
            {
                return false;
            }
        }


        public FormBase()
        {
            try
            {
                InitializeComponent();

                this.ResReader = new Bifrost.Common.ResManager();
                this.Shown += A2PFormBase_Shown;
                
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        #endregion

        private static string GetCurrentSubSystemType(Control control)
        {
            Form f = control.FindForm();
            if (f == null) return "MAS";

            try
            {
                return "MAS";// XPropertyConverter.EnumToString(typeof(SubSystemType), UIHelper.GetControlPropertyValue(f, f.GetType().BaseType, "SubSystemType"));

            }
            catch (Exception)
            {
                return "MAS";
            }
        }

        SystemConfig _configDD;
        SystemConfig _configUpper;
        SystemConfig _configDateFormat;


        private void GetDDApply()
        {
            _configDD = new SystemConfig();
            _configDD = ConfigUtil.GetConfig("SYS0050");
        }
        private string GetSystemType(string SystemType)
        {
            //MAS0123 영문대문자 입력 조회 여부
            //SYS0123 영문대문자 입력 조회 여부
            //HRS0123 영문대문자 입력 조회 여부
            //GRW0123 영문대문자 입력 조회 여부
            _configUpper = new SystemConfig();

            switch (SystemType)
            {
                case "MAS":
                    _configUpper = ConfigUtil.GetConfig("MAS0123");
                    break;
                case "SYS":
                    _configUpper = ConfigUtil.GetConfig("SYS0123");
                    break;
                case "HRS":
                    _configUpper = ConfigUtil.GetConfig("HRS0123");
                    break;
                case "GRW":
                    _configUpper = ConfigUtil.GetConfig("GRW0123");
                    break;
                case "PRD":
                    _configUpper = ConfigUtil.GetConfig("PRD0123");
                    break;
                case "SAL":
                    _configUpper = ConfigUtil.GetConfig("SAL0123");
                    break;
                default:
                    break;
            }

            return _configUpper.ConfigValue;
        }

        private string GetDateFormat()
        {
            _configDateFormat = ConfigUtil.GetConfig("MAS0124");
            return _configDateFormat.ConfigValue;
        }
        private void A2PFormBase_Shown(object sender, EventArgs e)
        {
            try
            {
                if (AutoTabOrder != TabOrderManager.TabScheme.None)
                {
                    //탭오더 자동 세팅
                    TabOrderManager tom = new TabOrderManager(this);
                    tom.SetTabOrder(AutoTabOrder);
                }
                Control[] controls = GetAllControlsUsingQueue(this);
                    
                char[] delimiterChars = { '.' };
                string[] words;
                string _Module;

                if (MenuData == null)
                {
                    _Module = "MAS";
                }
                else
                {
                    words = MenuData.MenuClass.Split(delimiterChars);
                    _Module = words[0];
                }


                //GetSystemType(_Module);
                //날짜 포맷 세팅
                //GetDateFormat();
                //다국어 모듈 세팅
                //GetDDApply();
                ResReader.Module = _Module;

                //foreach (Control control in controls)
                //{

                //    if (control is aTextEdit)
                //    {
                //        try
                //        {
                //            if (_configUpper.ConfigValue == "N")
                //                ((aTextEdit)control).isUpper = false;
                //            else
                //                ((aTextEdit)control).isUpper = true;
                //        }
                //        catch
                //        {
                //            ((aTextEdit)control).isUpper = true;
                //        }
                //    }

                //    if (control is aMemoEdit)
                //    {
                //        try
                //        {
                //            if (_configUpper.ConfigValue == "N")
                //                ((aMemoEdit)control).isUpper = false;
                //            else
                //                ((aMemoEdit)control).isUpper = true;
                //        }
                //        catch
                //        {
                //            ((aMemoEdit)control).isUpper = true;
                //        }
                //    }

                //    if (control is aGrid)
                //    {
                //        try
                //        {
                //            //대문자 적용
                //            if (_configUpper.ConfigValue == "N")
                //                ((aGrid)control).isUpper = false;
                //            else
                //                ((aGrid)control).isUpper = true;

                //            ////다국어 적용
                //            //if (_configDD.ConfigValue == "Y")
                //            //{
                //            //    foreach (DevExpress.XtraGrid.Columns.GridColumn col in ((ColumnView)((GridControl)control).Views[0]).Columns)
                //            //    {
                //            //        col.Caption = ResReader.GetString(Global.Language, _Module, col.Caption);
                //            //    }
                //            //}
                //        }
                //        catch
                //        {
                //            ((aGrid)control).isUpper = true;
                //        }
                //    }

                //    if (control is aDateEdit)
                //    {
                //        if (_configDateFormat.ConfigValue != string.Empty)
                //        {
                //            ((aDateEdit)control).DateFormat = _configDateFormat.ConfigValue;
                //        }
                //    }

                //    if (control is aPeriodEdit)
                //    {
                //        if (_configDateFormat.ConfigValue != string.Empty)
                //        {
                //            ((aPeriodEdit)control).DateFormat = _configDateFormat.ConfigValue;
                //        }
                //    }

                //    #region Set DD
                //    if (_configDD.ConfigValue == "Y")
                //    {
                //        //20180626 : control is Label 윈도우 라벨은 제외 이거 쓰면 업데이트 떄부터 MAS 리소스 파일을 잡고 있어서 재생성 되기전부터 잡고 있음
                //        if (control is aLabel  || control is DevExpress.XtraEditors.LabelControl || control is aButton || control is aButtonSizeFree || control is aCheckBox)
                //        {
                //            control.Text = ResReader.GetString(Global.Language, _Module, control.Text);
                //        }

                //        if (control is aTabControl)
                //        {
                //            for (int i = 0; i < ((aTabControl)control).TabPages.Count; i++)
                //            {
                //                ((aTabControl)control).TabPages[i].Text = ResReader.GetString(Global.Language, _Module, ((aTabControl)control).TabPages[i].Text);
                //            }
                //        }

                //        if (control is aTabPanel)
                //        {
                //            for (int i = 0; i < ((aTabPanel)control).Pages.Count; i++)
                //            {
                //                ((aTabPanel)control).Pages[i].Caption = ResReader.GetString(Global.Language, _Module, ((aTabPanel)control).Pages[i].Caption);
                //            }
                //        }

                //        if (control is aGrid)
                //        {
                //            foreach (DevExpress.XtraGrid.Columns.GridColumn col in ((ColumnView)((aGrid)control).Views[0]).Columns)
                //            {
                //                col.Caption = ResReader.GetString(Global.Language, _Module, col.Caption);
                //            }
                //        }

                //        if (control is aRadioButton)
                //        {
                //            foreach(DevExpress.XtraEditors.Controls.RadioGroupItem rg in ((aRadioButton)control).Properties.Items)
                //            {
                //                rg.Description = ResReader.GetString(Global.Language, _Module, rg.Description);
                //            }
                //        }
                //    }
                //    #endregion
                //}

                //폰트 설정
                //DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new System.Drawing.Font("D2Coding ligature", 15);
            }
            catch
            {

            }
        }

        private static Control[] GetAllControlsUsingQueue(Control containerControl)
        {
            List<Control> allControls = new List<Control>();

            Queue<Control.ControlCollection> queue = new Queue<Control.ControlCollection>();
            queue.Enqueue(containerControl.Controls);

            while (queue.Count > 0)
            {
                Control.ControlCollection controls = (Control.ControlCollection)queue.Dequeue();
                if (controls == null || controls.Count == 0) continue;

                foreach (Control control in controls)
                {
                    allControls.Add(control);
                    queue.Enqueue(control.Controls);
                }
            }

            return allControls.ToArray();
        }

        static Control[] GetAllControlsUsingRecursive(Control containerControl)
        {
            List<Control> allControls = new List<Control>();

            foreach (Control control in containerControl.Controls)
            {
                //자식 컨트롤을 컬렉션에 추가한다
                allControls.Add(control);
                //만일 자식 컨트롤이 또 다른 자식 컨트롤을 가지고 있다면…
                if (control.Controls.Count > 0)
                {
                    //자신을 재귀적으로 호출한다
                    allControls.AddRange(GetAllControlsUsingRecursive(control));
                }
            }
            //모든 컨트롤을 반환한다
            return allControls.ToArray();
        }

        private void FormBase_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            // If the input language is Japanese.
            // set the initial IMEMode to Katakana.
            if (e.InputLanguage.Culture.TwoLetterISOLanguageName.Equals("ko"))
            {
                this.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            }
            else
            {
                this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    this.MdiForm.OnHelpClick();
                    return true;
                default:
                    return base.ProcessDialogKey(keyData);
            }
        }
	}
	
}