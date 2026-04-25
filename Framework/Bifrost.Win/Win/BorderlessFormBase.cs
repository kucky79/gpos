using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NF.Framework;
using NF.Framework.Common;
using NF.A2P.Data;
using NF.Framework.Win.Data;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;
using NF.A2P;
using NF.Framework.Win.Controls;
using NF.A2P.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars.Ribbon;

namespace NF.Framework.Win
{
	public partial class BorderlessFormBase : RibbonForm
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
                    this.MdiForm.RemoveChildForm(this);
                }
            }
            catch { }
        }

		#endregion Privates

        #region Form Validation
        
        private const string INFRA_VALIDATION_KEY = "Infragistics";
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
            if (FormValidating != null)
            {
                FormValidating(e);
            }
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

        #region RegisterControlValidation - UltraDateTimeEditor

        /// <summary>
        /// Register validation rule to a infragistics control
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="valueToValite"></param>
        protected void RegisterControlValidation(Infragistics.Win.UltraWinEditors.UltraDateTimeEditor dateTimeEditor, object valueToValidate)
        {
            RegisterControlValidation(dateTimeEditor, valueToValidate, "Error");
        }

        /// <summary>
        /// Register validation rule to a infragistics control
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="valueToValite"></param>
        protected void RegisterControlValidation(Infragistics.Win.UltraWinEditors.UltraDateTimeEditor dateTimeEditor, object valueToValidate, string errorMessage)
        {
            dateTimeEditor.Validating += new CancelEventHandler(OnUltraDateTimeEditorValidating);
            RegisterValidation(INFRA_VALIDATION_KEY, dateTimeEditor.Name, valueToValidate, errorMessage);
        }

        /// <summary>
        /// Function to validate a UltraDateTimeEditor control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnUltraDateTimeEditorValidating(object sender, CancelEventArgs e)
        {
            Infragistics.Win.UltraWinEditors.UltraDateTimeEditor dateTimeEditor = (Infragistics.Win.UltraWinEditors.UltraDateTimeEditor)sender;
            if (dateTimeEditor.Enabled && !ValidateItem(INFRA_VALIDATION_KEY, dateTimeEditor.Name, dateTimeEditor.Value))
            {
                dateTimeEditor.Appearance.BackColor = formSettings.ErrorBackColor;
                bControlValidated = false;
            }
            else
            {
                dateTimeEditor.Appearance.BackColor = Color.Empty;
            }
        }

        #endregion RegisterControlValidation - UltraDateTimeEditor

        #region RegisterControlValidation - UltraNumericEditor

        /// <summary>
        /// Register validation rule to a infragistics control
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="valueToValite"></param>
        protected void RegisterControlValidation(Infragistics.Win.UltraWinEditors.UltraNumericEditor numericEditor, object valueToValidate)
        {
            RegisterControlValidation(numericEditor, valueToValidate, "Error");
        }

        /// <summary>
        /// Register validation rule to a infragistics control
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="valueToValite"></param>
        protected void RegisterControlValidation(Infragistics.Win.UltraWinEditors.UltraNumericEditor numericEditor, object valueToValidate, string errorMessage)
        {
            numericEditor.Validating += new CancelEventHandler(OnUltraNumericEditorValidating);
            RegisterValidation(INFRA_VALIDATION_KEY, numericEditor.Name, valueToValidate, errorMessage);
        }

        /// <summary>
        /// Function to validate a UltraDateTimeEditor control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnUltraNumericEditorValidating(object sender, CancelEventArgs e)
        {
            Infragistics.Win.UltraWinEditors.UltraNumericEditor numericEditor = (Infragistics.Win.UltraWinEditors.UltraNumericEditor)sender;
            if (numericEditor.Enabled && !ValidateItem(INFRA_VALIDATION_KEY, numericEditor.Name, numericEditor.Value))
            {
                numericEditor.Appearance.BackColor = formSettings.ErrorBackColor;
                bControlValidated = false;
            }
            else
            {
                numericEditor.Appearance.BackColor = Color.Empty;
            }
        }

        #endregion RegisterControlValidation - UltraNumericEditor

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
                    MdiForm.ChildFormNeedPromptCount++;
                else if (!value && _PromptOnClose)
                    MdiForm.ChildFormNeedPromptCount--;

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


		private SubSystemType _SubSystemType = SubSystemType.FRAMEWORK;

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
            new MsgDialog(MessageType.Error, ex).ShowDialog(this.MdiForm);
        }

		/// <summary>
		/// Display default message when error cannot be analyzed
		/// </summary>
		/// <param name="ex"></param>
        protected virtual void HandleWinException(Exception ex)
        {
            new MsgDialog(MessageType.Error, new UIException("M00004", ex)).ShowDialog(this.MdiForm);
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
			

		public BorderlessFormBase()
		{
			InitializeComponent();

            this.ResReader = new NF.Framework.Common.ResManager();
            //this.Shown += A2PFormBase_Shown;
        }

        #endregion

        private void A2PFormBase_Shown(object sender, EventArgs e)
        {
            try
            {
                Control[] controls = GetAllControlsUsingRecursive(this);

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
                
                ResReader.Module = _Module;

                foreach (Control control in controls)
                {
                    if (control is aLabel || control is Label || control is aButton || control is aButtonSizeFree)
                    {
                        control.Text = ResReader.GetString(Global.Language, _Module, control.Text);
                    }

                    if (control is aTabControl)
                    {
                        for (int i = 0; i < ((aTabControl)control).TabPages.Count; i++)
                        {
                            ((aTabControl)control).TabPages[i].Text = ResReader.GetString(Global.Language, _Module, ((aTabControl)control).TabPages[i].Text);
                        }
                    }

                    if (control is aGrid)
                    {
                        foreach (DevExpress.XtraGrid.Columns.GridColumn col in ((ColumnView)((aGrid)control).Views[0]).Columns)
                        {
                            col.Caption = ResReader.GetString(Global.Language, _Module, col.Caption);
                        }
                    }
                }
            }
            catch
            {

            }
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