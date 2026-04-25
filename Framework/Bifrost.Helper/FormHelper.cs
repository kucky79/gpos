using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTab;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

using Bifrost.Win.Controls;
using Bifrost.Adv.Controls;

namespace Bifrost.Helper
{
    public class FreeFormBinding
    {
        /// <summary>
        /// DataMode가 변경되었을 때 발생하는 이벤트
        /// </summary>
        public event FreeFormBindingEventHandler DataModeChanged;
        /// <summary>
        /// 바인딩된 각 컨트롤의 값을 사용자가 변경했을 때 발생하는 이벤트
        /// </summary>
        public event FreeFormBindingEventHandler ControlValueChanged;

        DataTable _dt = null;
        Dictionary<string, Control> _controls = null;
        FormDataModeEnum _DataMode = FormDataModeEnum.InsertAfterModify;

        private void OnDataModeChanged(FormHelperArg e)
        {
            if (_dt == null)
                return;

            DataModeChanged?.Invoke(this, e);
        }

        private void OnControlValueChanged(object sender, FormHelperArg e)
        {
            if (_dt == null)
                return;

            ControlValueChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// </summary>
        /// <param name="dt">데이터테이블. </param>
        /// <param name="container">Panel, TabControl, TableLayout 등 컨트롤들을 담고 있는 컨테이너</param>
        public void SetBinding(DataTable dt, Control container)
        {
            if (dt == null)
                throw new Exception("FreeBinding 생성자 : DataTable이 null 이면 안됩니다.");

            if (dt.Columns == null)
                throw new Exception("FreeBinding 생성자 : DataTable의 컬럼정보를 가져오지 못했습니다.");


            _dt = dt;
            _controls = new Dictionary<string, Control>();

            if (container is TableLayoutPanel)
            {
                foreach (Control ctr in container.Controls)
                {
                    if (ctr is Panel || ctr is PanelControl || ctr is XtraScrollableControl)
                        InitControls(ctr);
                    else if (ctr is TabControl || ctr is XtraTabControl || ctr is XtraTabPage || ctr is aTabControl)
                        InitControls(ctr);
                    else if (ctr is TableLayoutPanel)
                        InitControls(ctr);
                    else if (ctr is RadioGroup || ctr is aRadioButton)
                        InitControls(ctr);
                }
            }
            else
            {
                InitControls(container);
            }

            InitControlEvent();
        }

        private void InitControls(Control container)
        {
            foreach (Control ctr in container.Controls)
            {
                if (ctr is Panel || ctr is XtraScrollableControl || ctr is PanelControl || ctr is TabControl || ctr is TabPage || ctr is TabPane || ctr is XtraTabControl || ctr is XtraTabPage || ctr is aTabControl || ctr is GroupBox)
                {
                    InitControls(ctr);
                }
                else if (ctr.Tag != null && ctr.Tag.ToString() != "")
                {
                    if (ctr is Label || ctr is Button || ctr is TableLayoutPanel)
                        continue;

                    if (_controls.ContainsKey(ctr.Tag.ToString()))
                        throw new Exception(ctr.Name + " 컨트롤에 설정된 Tag 는 이미 다른 컨트롤에 설정된 값입니다.\n바인딩할 컨트롤들의 Tag는 중복될 수 없습니다.");

                    _controls.Add(ctr.Tag.ToString(), ctr);
                    if (ctr is aRadioButton)
                    {
                        for (int i = 0; i < ((aRadioButton)ctr).Properties.Items.Count; i++)
                        {
                            ((aRadioButton)ctr).Properties.Items[i].Value = ((aRadioButton)ctr).Properties.Items[i].Tag;
                        }
                    }
                }
            }
        }

        private void InitControlEvent()
        {
            foreach (Control ctr in _controls.Values)
            {
                switch (ctr.GetType().Name)
                {
                    case "TextEdit":
                        ((TextEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        break;
                    case "aTextEdit":
                        ((aTextEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        ((aTextEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);

                        break;
                    case "aMaskEdit":
                        ((aMaskEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        ((aMaskEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);

                        break;
                    case "DateEdit":
                    case "aDateEdit":
                        ((DateEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        ((DateEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        ((DateEdit)ctr).DateTimeChanged += new EventHandler(Control_Validated);
                        break;
                    case "aDateMonth":
                        ((aDateMonth)ctr).TextChanged += new EventHandler(Control_Validated);
                        ((aDateMonth)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        ((aDateMonth)ctr).DateTimeChanged += new EventHandler(Control_Validated);
                        break;
                    case "CheckEdit":
                    case "aCheckBox":
                        ((CheckEdit)ctr).CheckedChanged += new EventHandler(Control_Validated);
                        ((CheckEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        break;
                    case "MemoEdit":
                    case "aMemoEdit":
                        ((MemoEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        ((MemoEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        break;
                    case "LookUpEdit":
                        ((LookUpEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        break;
                    case "aLookUpEdit":
                        ((aLookUpEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        break;
                    case "aCodeNText":
                        ((aCodeNText)ctr).CodeChanged += new EventHandler(Control_CodeChanged);
                        ((aCodeNText)ctr).TextBoxChanged += new EventHandler(Control_CodeChanged);
                        break;
                    case "aCodeText":
                        ((aCodeText)ctr).CodeChanged += new EventHandler(Control_CodeChanged);
                        ((aCodeText)ctr).TextChanged += new EventHandler(Control_CodeChanged);
                        break;
                    case "aCodeTextNonPopup":
                        ((aCodeTextNonPopup)ctr).CodeChanged += new EventHandler(Control_CodeChanged);
                        break;
                    case "aRadioButton":
                        ((aRadioButton)ctr).SelectedIndexChanged += new EventHandler(Control_Validated);
                        ((aRadioButton)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        break;
                    case "aNumericText":
                        ((aNumericText)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        ((aNumericText)ctr).DecimalValueChanged += new EventHandler(Control_Validated);

                        break;
                }

            }
        }

        private void InitControlEventDelete()
        {
            foreach (Control ctr in _controls.Values)
            {
                switch (ctr.GetType().Name)
                {
                    case "TextEdit":
                        ((TextEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        break;
                    case "aTextEdit":
                        ((aTextEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        ((aTextEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case "aMaskEdit":
                        ((aMaskEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        ((aMaskEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case "DateEdit":
                    case "aDateEdit":
                        ((DateEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        ((DateEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        ((DateEdit)ctr).DateTimeChanged -= new EventHandler(Control_Validated);
                        break;
                    case "aDateMonth":
                        ((aDateMonth)ctr).TextChanged -= new EventHandler(Control_Validated);
                        ((aDateMonth)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        ((aDateMonth)ctr).DateTimeChanged -= new EventHandler(Control_Validated);
                        break;
                    case "CheckEdit":
                    case "aCheckBox":
                        ((CheckEdit)ctr).CheckedChanged -= new EventHandler(Control_Validated);
                        ((CheckEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case "MemoEdit":
                    case "aMemoEdit":
                        ((MemoEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        ((MemoEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case "LookUpEdit":
                    case "aLookUpEdit":
                        ((aLookUpEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case "aCodeNText":
                        ((aCodeNText)ctr).CodeChanged -= new EventHandler(Control_CodeChanged);
                        ((aCodeNText)ctr).TextBoxChanged -= new EventHandler(Control_CodeChanged);
                        ((aCodeNText)ctr).TextChanged -= new EventHandler(Control_CodeChanged);
                        break;
                    case "aCodeText":
                        ((aCodeText)ctr).CodeChanged -= new EventHandler(Control_CodeChanged);
                        ((aCodeText)ctr).TextChanged -= new EventHandler(Control_CodeChanged);
                        break;
                    case "aCodeTextNonPopup":
                        ((aCodeTextNonPopup)ctr).CodeChanged -= new EventHandler(Control_CodeChanged);
                        break;
                    case "aRadioButton":
                        ((aRadioButton)ctr).SelectedIndexChanged -= new EventHandler(Control_Validated);
                        ((aRadioButton)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case "aNumericText":
                        ((aNumericText)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        ((aNumericText)ctr).DecimalValueChanged -= new EventHandler(Control_Validated);
                        break;
                }

            }
        }

        void Control_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is aCodeNText)
                {
                    if (((aCodeNText)sender).CodeValue == string.Empty)
                    {
                        string[] Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = string.Empty;
                        _dt.Rows[0][Args[1].ToString()] = string.Empty;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                    else
                    {
                        string[] Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = ((aCodeNText)sender).CodeValue;
                        _dt.Rows[0][Args[1].ToString()] = ((aCodeNText)sender).CodeName;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                }

                else if (sender is aCodeText)
                {
                    if (((aCodeText)sender).CodeValue == string.Empty)
                    {
                        string[] Args = ((aCodeText)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = string.Empty;
                        _dt.Rows[0][Args[1].ToString()] = string.Empty;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                    else
                    {
                        string[] Args = ((aCodeText)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = ((aCodeText)sender).CodeValue;
                        _dt.Rows[0][Args[1].ToString()] = ((aCodeText)sender).CodeName;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                }

                else if (sender is aCodeTextNonPopup)
                {
                    if (((aCodeTextNonPopup)sender).CodeValue == string.Empty)
                    {
                        string[] Args = ((aCodeTextNonPopup)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = string.Empty;
                        _dt.Rows[0][Args[1].ToString()] = string.Empty;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                    else
                    {
                        string[] Args = ((aCodeTextNonPopup)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = ((aCodeTextNonPopup)sender).CodeValue;
                        _dt.Rows[0][Args[1].ToString()] = ((aCodeTextNonPopup)sender).CodeName;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void Control_QueryAfter(object sender, EventArgs e)
        {
            try
            {
                if (sender is aCodeNText)
                {
                    if (((aCodeNText)sender).CodeValue != string.Empty)
                    {
                        string[] Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = ((aCodeNText)sender).CodeValue;
                        _dt.Rows[0][Args[1].ToString()] = ((aCodeNText)sender).CodeName;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                }
                if (sender is aCodeText)
                {
                    if (((aCodeText)sender).CodeValue != string.Empty)
                    {
                        string[] Args = ((aCodeText)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = ((aCodeText)sender).CodeValue;
                        _dt.Rows[0][Args[1].ToString()] = ((aCodeText)sender).CodeName;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                }

                if (sender is aCodeTextNonPopup)
                {
                    if (((aCodeTextNonPopup)sender).CodeValue != string.Empty)
                    {
                        string[] Args = ((aCodeTextNonPopup)sender).Tag.ToString().Split(';');
                        if (Args == null || Args.Length != 2)
                            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                        _dt.Rows[0][Args[0].ToString()] = ((aCodeTextNonPopup)sender).CodeValue;
                        _dt.Rows[0][Args[1].ToString()] = ((aCodeTextNonPopup)sender).CodeName;

                        SetValueToDataRow(sender);
                        OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).GetType().Name)
                {
                    case "TextEdit":
                        if (!((TextEdit)sender).IsModified) return;
                        break;
                    case "aTextEdit":
                        if (!((aTextEdit)sender).IsModified) return;
                        break;
                    case "aMaskEdit":
                        if (!((aMaskEdit)sender).IsModified) return;
                        break;
                    case "DateEdit":
                    case "aDateEdit":
                        if (!((DateEdit)sender).IsModified) return;
                        break;
                    case "aDateMonth":
                        if (!((aDateMonth)sender).IsModified) return;
                        break;
                    case "CheckEdit":
                    case "aCheckBox":
                        if (!((CheckEdit)sender).IsModified) return;
                        break;
                    case "MemoEdit":
                    case "aMemoEdit":
                        if (!((MemoEdit)sender).IsModified) return;
                        break;
                    case "LookUpEdit":
                        if (!((LookUpEdit)sender).IsModified) return;
                        break;
                    case "aLookUpEdit":
                        if (!((aLookUpEdit)sender).IsModified) return;
                        break;
                    case "aRadioButton":
                        if (!((aRadioButton)sender).IsModified) return;
                        break;
                    case "aNumericText":
                        if (!((aNumericText)sender).IsModified) return;
                        break;
                }

                SetValueToDataRow(sender);
                OnControlValueChanged(sender, new FormHelperArg(_dt.Rows[0], _DataMode));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetValueToDataRow(object sender)
        {
            DataRow row = _dt.Rows[0];

            if (row == null) return;

            if (((Control)sender).Tag == null)
                throw new Exception(((Control)sender).Name + " 컨트롤 Tag 속성에 매핑할 DataTable 컬럼명을 지정해야 합니다.");

            string[] Args;

            switch (((Control)sender).GetType().Name)
            {
                case "TextEdit":
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;
                case "aTextEdit":
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;
                case "aMaskEdit":
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;
                case "DateEdit":
                case "aDateEdit":
                    row[((Control)sender).Tag.ToString()] = ((aDateEdit)sender).Text;
                    break;
                case "aDateMonth":
                    row[((Control)sender).Tag.ToString()] = ((aDateMonth)sender).Text;
                    break;
                case "CheckEdit":
                case "aCheckBox":
                    bool bChecked = ((CheckEdit)sender).Checked;

                    Args = ((CheckEdit)sender).Tag.ToString().Split(';', ',');
                    if (Args == null || Args.Length != 3)
                        throw new Exception("CheckBox Tag 속성은 <데이터컬럼명;체크값,체크해제값> 형태로 지정되어야 합니다.");

                    if (bChecked)
                        row[Args[0]] = Args[1];
                    else
                        row[Args[0]] = Args[2];
                    break;
                case "MemoEdit":
                case "aMemoEdit":
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;
                case "LookUpEdit":
                    row[((Control)sender).Tag.ToString()] = ((LookUpEdit)sender).EditValue;
                    break;
                case "aLookUpEdit":
                    row[((Control)sender).Tag.ToString()] = ((aLookUpEdit)sender).EditValue;
                    break;
                case "aRadioButton":
                    row[((Control)sender).Tag.ToString()] = ((aRadioButton)sender).Properties.Items[((aRadioButton)sender).SelectedIndex].Tag;
                    break;
                case "aNumericText":
                    row[((Control)sender).Tag.ToString()] = ((aNumericText)sender).EditValue;
                    break;

                case "aCodeText":
                    Args = ((aCodeText)sender).Tag.ToString().Split(';');
                    if (Args == null || Args.Length != 2)
                        throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                    row[Args[0].ToString()] = ((aCodeText)sender).CodeValue;
                    row[Args[1].ToString()] = ((aCodeText)sender).CodeName;

                    break;

                case "aCodeNText":
                    Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                    if (Args == null || Args.Length != 2)
                        throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                    row[Args[0].ToString()] = ((aCodeNText)sender).CodeValue;
                    row[Args[1].ToString()] = ((aCodeNText)sender).CodeName;

                    break;

                case "aCodeTextNonPopup":
                    Args = ((aCodeTextNonPopup)sender).Tag.ToString().Split(';');
                    if (Args == null || Args.Length != 2)
                        throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                    row[Args[0].ToString()] = ((aCodeTextNonPopup)sender).CodeValue;
                    row[Args[1].ToString()] = ((aCodeTextNonPopup)sender).CodeName;

                    break;
            }

        }

        /// <summary>
        /// 변경된 내용을 DataTable로 리턴한다.
        /// </summary>
        /// <returns></returns>
        public DataTable GetChanges()
        {
            if (_dt == null)
                return null;

            return _dt.GetChanges();
        }

        /// <summary>
        /// 조회한 후 DataTable을 셋팅할 때 사용
        /// </summary>
        /// <param name="dt"></param>
        public void SetDataTable(DataTable dt)
        {
            if (dt == null)
                throw new Exception("FreeBinding.DataTable 속성 : DataTable 은 null 일 수 없습니다.");

            if (dt.Columns == null)
                throw new Exception("FreeBinding.DataTable 속성 : DataTable 에 컬럼정보가 없습니다.");

            if (dt.Rows.Count == 0)
                throw new Exception("FreeBinding.DataTable 속성 : DataTable 행수가 0 입니다. 행수는 항상 1 이어야 합니다.");

            if (dt.Rows.Count > 1)
                throw new Exception("FreeBinding.DataTable 속성 : DataTable 행수가 1 보다 큽니다. 행수는 항상 1 이어야 합니다.");

            _dt = dt;
            InitControlEventDelete();
            SetValueToControl();
            _DataMode = FormDataModeEnum.SearchAfterModify;
            OnDataModeChanged(new FormHelperArg(_dt.Rows[0], _DataMode));
            InitControlEvent();
        }

        private void SetValueToControl()
        {
            DataRow row = _dt.Rows[0];
            Control ctl = null;

            foreach (string key in _controls.Keys)
            {
                ctl = _controls[key];
                switch (_controls[key].GetType().Name)
                {
                    case "TextEdit":
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;

                    case "aTextEdit":
                        ((aTextEdit)ctl).Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case "aMaskEdit":
                        ((aMaskEdit)ctl).Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case "aDateEdit":
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case "aDateMonth":
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case "aCheckBox":
                        if (row == null)
                        {
                            ((CheckEdit)ctl).Checked = false;
                        }
                        else
                        {
                            string[] Args = key.ToString().Split(';', ',');
                            if (Args == null || Args.Length != 3)
                                throw new Exception("CheckEdit Tag는 <데이터컬럼명;체크값,체크해제값> 형태로 지정되어야 합니다.");

                            if (string.IsNullOrEmpty(Args[1]) || string.IsNullOrEmpty(Args[2]))
                                throw new Exception("CheckEdit Tag는 <데이터컬럼명;체크값,체크해제값> 형태로 지정되어야 합니다.");

                            if (row[Args[0]].ToString().ToUpper() == Args[1].ToUpper())
                                ((CheckEdit)ctl).Checked = true;
                            else
                                ((CheckEdit)ctl).Checked = false;
                        }
                        break;
                    case "aMemoEdit":
                        ((aMemoEdit)ctl).Text = row == null ? string.Empty : row[key].ToString();
                        break;

                    case "aLookUpEdit":
                        ((aLookUpEdit)ctl).EditValue = row == null ? string.Empty : row[key].ToString();
                        break;
                    case "aCodeNText":
                        if (row == null)
                        {
                            ((aCodeNText)ctl).CodeValue = string.Empty;
                            ((aCodeNText)ctl).CodeName = string.Empty;

                            //((aCodeNText)ctl).SetCodeNameNValue(string.Empty, string.Empty);
                        }
                        else
                        {
                            string[] Args = key.ToString().Split(';');
                            if (Args == null || Args.Length != 2)
                                throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                            ((aCodeNText)ctl).CodeValue = row[Args[0]].ToString();
                            ((aCodeNText)ctl).CodeName = row[Args[1]].ToString();

                            //((aCodeNText)ctl).SetCodeNameNValue(row[Args[0]].ToString(), row[Args[1]].ToString());
                        }
                        break;
                    case "aCodeText":
                        if (row == null)
                        {
                            ((aCodeText)ctl).CodeValue = string.Empty;
                            ((aCodeText)ctl).CodeName = string.Empty;
                            //((aCodeText)ctl).SetCodeNameNValue(string.Empty, string.Empty);
                        }
                        else
                        {
                            string[] Args = key.ToString().Split(';');
                            if (Args == null || Args.Length != 2)
                                throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                            ((aCodeText)ctl).CodeValue = row[Args[0]].ToString();
                            ((aCodeText)ctl).CodeName = row[Args[1]].ToString();

                            //((aCodeText)ctl).SetCodeNameNValue(row[Args[0]].ToString(), row[Args[1]].ToString());
                        }
                        break;
                    case "aCodeTextNonPopup":
                        if (row == null)
                        {
                            ((aCodeTextNonPopup)ctl).CodeValue = string.Empty;
                            ((aCodeTextNonPopup)ctl).CodeName = string.Empty;
                            //((aCodeText)ctl).SetCodeNameNValue(string.Empty, string.Empty);
                        }
                        else
                        {
                            string[] Args = key.ToString().Split(';');
                            if (Args == null || Args.Length != 2)
                                throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                            ((aCodeTextNonPopup)ctl).CodeValue = row[Args[0]].ToString();
                            ((aCodeTextNonPopup)ctl).CodeName = row[Args[1]].ToString();

                            //((aCodeText)ctl).SetCodeNameNValue(row[Args[0]].ToString(), row[Args[1]].ToString());
                        }
                        break;
                    case "aRadioButton":
                        if (row != null)
                        {
                            ((aRadioButton)ctl).EditValue = row[key].ToString();
                        }
                        break;
                    case "aNumericText":
                        ((aNumericText)ctl).EditValue = row == null ? string.Empty : row[key].ToString();
                        break;
                }

            }
        }

        /// <summary>
        /// 기존 행이 있으면 삭제하고 새로운 행을 추가한다.
        /// </summary>
        /// <returns></returns>
        public void ClearAndNewRow()
        {
            if (_dt == null)
                throw new Exception("ClearAndNewRow() Error : DataTable is null");

            _dt.Rows.Clear();
            _dt.Rows.Add(_dt.NewRow());

            SetValueToControl();

            _DataMode = FormDataModeEnum.InsertAfterModify;

            OnDataModeChanged(new FormHelperArg(_dt.Rows[0], _DataMode));
        }

        /// <summary>
        /// 변경사항을 저장. DataMode가 "조회후수정" 으로 변경됨.
        /// </summary>
        public void AcceptChanges()
        {
            if (_dt == null)
                return;

            _dt.AcceptChanges();
            _DataMode = FormDataModeEnum.SearchAfterModify;

            OnDataModeChanged(new FormHelperArg(_dt.Rows[0], _DataMode));
        }


        /// <summary>
        /// 바인딩된 컨트롤들에 대하여 Enable 속성을 처리한다.
        /// </summary>
        /// <param name="enable"></param>
        public void SetControlEnabled(bool enable)
        {
            foreach (Control ctl in _controls.Values)
            {
                ctl.Enabled = enable;
            }
        }

        /// <summary>
        /// 현재 DataRow를 가져온다. 항상 0번째 인덱스의 DataRow를 리턴한다.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataRow CurrentRow
        {
            get
            {
                if (_dt == null)
                    return null;

                if (_dt.Rows == null)
                    return null;

                if (_dt.Rows.Count == 0)
                    return null;

                return _dt.Rows[0];
            }
        }

        /// <summary>
        /// 현재 작업모드를 알고 싶을 때
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FormDataModeEnum DataMode
        {
            get { return _DataMode; }
            set
            {
                _DataMode = value;
                OnDataModeChanged(new FormHelperArg(_dt.Rows[0], _DataMode));
            }
        }
    }

    public static class LoadData
    {
        public static void StartLoading(Form targetForm)
        {
            StartLoading(targetForm, "조회중", "Loading....");
        }

        public static void StartLoading(Form targetForm, string caption, string description)
        {
            SplashScreenManager.ShowForm(targetForm, typeof(Bifrost.Win.LoadingForm), true, true, true);
            SplashScreenManager.Default.SetWaitFormCaption(caption);
            SplashScreenManager.Default.SetWaitFormDescription(description);
        }

        public static void StartLoading(UserControl targetControl, string caption, string description)
        {

            SplashScreenManager.ShowForm(targetControl, typeof(Bifrost.Win.LoadingForm), true, true);
            SplashScreenManager.Default.SetWaitFormCaption(caption);
            SplashScreenManager.Default.SetWaitFormDescription(description);
        }

        public static void EndLoading()
        {
            if (SplashScreenManager.Default != null)
            {
                SplashScreenManager.CloseForm();
            }
        }
    }
}
