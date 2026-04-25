using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;

//using Bifrost.Adv.Controls;
using Bifrost.Win.Controls;

namespace Bifrost.Grid
{
    class SetControlBinding
    {
        #region 멤버필드

        /// <summary>
        /// DataMode가 변경되었을 때 발생하는 이벤트
        /// </summary>
        public event FreeBindingEventHandler _dataModeChanged;
        /// <summary>
        /// 바인딩된 각 컨트롤의 값을 사용자가 변경했을 때 발생하는 이벤트
        /// </summary>
        public event FreeBindingEventHandler _controlValueChanged;

        DataTable _dt = new DataTable();
        DataView _dv = new DataView();
        Dictionary<string, Control> _controls = null;
        GridView _view = null;

        DataModeEnum _dataMode = DataModeEnum.InsertAfterModify;
        private int _selectedRow = 0;
        private object[] _enableObjects;			// RowState 가 Added 일때는 Enable 시키는 컨트롤
        #endregion

        private void OnDataModeChanged(GridHelperArgs e)
        {
            if (_dt == null)
                return;

            _dataModeChanged?.Invoke(this, e);
        }

        private void OnControlValueChanged(object sender, GridHelperArgs e)
        {
            if (_dt == null)
                return;

            _controlValueChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// 그리드뷰 프리폼 바인딩에 사용
        /// </summary>
        /// <param name="aGridView"></param>
        /// <param name="container"></param>
        /// <param name="EnableControlsIfAdded"></param>
        public SetControlBinding(GridView aGridView, Control container, object[] EnableControlsIfAdded)
        {
            _view = aGridView;
            aGridView.DataSourceChanged += AGridView_DataSourceChanged;
            aGridView.FocusedRowChanged += AGridView_FocusedRowChanged;
            aGridView.RowCountChanged += AGridView_RowCountChanged;

            _controls = new Dictionary<string, Control>();

            if (container is TableLayoutPanel)
            {
                foreach (Control control in container.Controls)
                {
                    if (control is Panel || control is PanelControl || control is XtraScrollableControl)
                        InitControls(control);
                    else if (control is TabControl || control is XtraTabControl || control is XtraTabPage)// || control is aTabControl)
                        InitControls(control);
                    else if (control is TableLayoutPanel)
                        InitControls(control);
                    else if (control is RadioGroup || control is aRadioButton)
                        InitControls(control);
                }
            }
            else
            {
                InitControls(container);
            }

            aGridView.UpdateCurrentRow();
            if (aGridView.DataSource != null)
            {
                _dv = aGridView.DataSource as DataView;
                _dt = _dv.Table;
            }
            InitControlEvent();

            if (EnableControlsIfAdded != null)
                _enableObjects = EnableControlsIfAdded;
        }

        /// <summary>
        /// 트리 프리폼 바인딩에 사용
        /// </summary>
        /// <param name="aTree"></param>
        /// <param name="container"></param>
        /// <param name="EnableControlsIfAdded"></param>
        public SetControlBinding(aTree aTree, Control container, object[] EnableControlsIfAdded)
        {
            aTree.DataSourceChanged += ATree_DataSourceChanged;
            aTree.FocusedNodeChanged += ATree_FocusedNodeChanged;

            //_container = container;

            _controls = new Dictionary<string, Control>();

            if (container is TableLayoutPanel)
            {
                foreach (Control control in container.Controls)
                {
                    if (control is Panel || control is PanelControl || control is XtraScrollableControl)
                        InitControls(control);
                    else if (control is TabControl || control is XtraTabControl || control is XtraTabPage)// || control is aTabControl)
                        InitControls(control);
                    else if (control is TableLayoutPanel)
                        InitControls(control);
                    else if (control is RadioGroup || control is aRadioButton)
                        InitControls(control);
                }
            }
            else
            {
                InitControls(container);
            }
            InitControlEvent();


            if (EnableControlsIfAdded != null)
                _enableObjects = EnableControlsIfAdded;
        }

        #region Tree Event
        private void ATree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _dt = ((aTree)sender).DataSource as DataTable;

            //if (((aTree)sender).FocusedNode.Selected)
            {
                if (_dt.Rows.Count > 0)
                {
                    //컨트롤에서 datarow에 넣는 이벤트 제거
                    InitControlEventDelete();

                    _selectedRow = e.Node.Id;
                    SetValueToControl(_selectedRow);

                    //컨트롤에서 datarow에 넣는 이벤트 다시 추가
                    InitControlEvent();
                }
            }
        }

        private void ATree_DataSourceChanged(object sender, EventArgs e)
        {
            //초기화
            //Selected_Row = 0;

            //((GridView)sender).UpdateCurrentRow();
            //_dv = ((GridView)sender).DataSource as DataView;
            //_dt = _dv.Table;

            //if (((GridView)sender).GetSelectedRows().Length > 0)
            //{
            //    if (((GridView)sender).GetSelectedRows()[0] < 0)
            //    {
            //        Selected_Row = 0;
            //    }

            //    SetValueToControl(Selected_Row);
            //}
        }
        #endregion

        #region Grid Event
        private void AGridView_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            if (view.RowCount == 0) //행이 하나도 없을때 프리폼을 지워주기 위해서 
            {
                DataRow row = null;
                //컨트롤에서 datarow에 넣는 이벤트 제거
                InitControlEventDelete();
                //컨트롤에 value 넣어줌
                SetValueToControl(row);
                //컨트롤에서 datarow에 넣는 이벤트 다시 추가
                InitControlEvent();
            }
            else
            {
                //컨트롤에서 datarow에 넣는 이벤트 제거
                InitControlEventDelete();
                //컨트롤에 value 넣어줌
                SetValueToControl(view.GetFocusedDataSourceRowIndex());
                //컨트롤에서 datarow에 넣는 이벤트 다시 추가
                InitControlEvent();
            }
        }

        private void AGridView_DataSourceChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;

            //초기화
            _selectedRow = 0;
            InitControlEventDelete();

            _dv = view.DataSource as DataView;
            _dt = _dv.Table;

            //if (view.GetFocusedDataSourceRowIndex() < 0)
            //    Selected_Row = 0;
            //else
            //    Selected_Row = view.GetFocusedDataSourceRowIndex();

            SetValueToControl(view.GetFocusedDataSourceRowIndex());
            InitControlEvent();
        }

        private void AGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.GetFocusedDataSourceRowIndex() < 0) return;

            _selectedRow = view.GetFocusedDataSourceRowIndex();
            //컨트롤에서 datarow에 넣는 이벤트 제거
            InitControlEventDelete();
            SetValueToControl(_selectedRow);
            //컨트롤에서 datarow에 넣는 이벤트 다시 추가
            InitControlEvent();
            if (_dt.Rows.Count <= _selectedRow) return;

            DataRow dr = _dt.Rows[_selectedRow];
            if (dr.RowState == DataRowState.Added)
            {
                if (_enableObjects != null)
                {
                    #region Loop
                    foreach (Control ctr in _enableObjects)
                    {
                        switch (ctr.GetType().Name)
                        {
                            //case nameof(aCodeNText):
                            //    ((aCodeNText)ctr).ReadOnly = NF.Framework.Adv.Controls.aControlHelper.ControlEnum.ReadOnly.None;
                            //    break;
                            //case nameof(aCodeText):
                            //    ((aCodeText)ctr).ReadOnly = NF.Framework.Adv.Controls.aControlHelper.ControlEnum.ReadOnly.None;
                            //    break;
                            //case nameof(aCodeTextNonPopup):
                            //    ((aCodeTextNonPopup)ctr).ReadOnly = NF.Framework.Adv.Controls.aControlHelper.ControlEnum.ReadOnly.None;
                            //    break;
                            case nameof(aCheckBox):
                                ((aCheckBox)ctr).Properties.ReadOnly = false;
                                break;
                            case nameof(aDateEdit):
                                ((aDateEdit)ctr).Properties.ReadOnly = false;
                                break;
                            case nameof(aRadioButton):
                                ((aRadioButton)ctr).Properties.ReadOnly = false;
                                break;
                            case nameof(aLookUpEdit):
                                ((aLookUpEdit)ctr).Properties.ReadOnly = false;
                                break;
                            case nameof(aNumericText):
                                ((aNumericText)ctr).Properties.ReadOnly = false;
                                break;
                            case nameof(aMemoEdit):
                                ((aMemoEdit)ctr).Properties.ReadOnly = false;
                                break;
                            case nameof(aTextEdit):
                                ((aTextEdit)ctr).Properties.ReadOnly = false;
                                break;
                            case nameof(aMaskEdit):
                                ((aMaskEdit)ctr).Properties.ReadOnly = false;
                                break;
                            case nameof(aPeriodEdit):
                                ((aPeriodEdit)ctr).ReadOnly = false;
                                break;
                            case nameof(aDateMonth):
                                ((aDateMonth)ctr).ReadOnly = false;
                                break;
                            default:
                                ctr.Enabled = true;
                                break;
                        }
                    }
                    #endregion Loop
                }
            }
            else
            {
                if (_enableObjects != null)
                {
                    #region Loop
                    foreach (Control ctr in _enableObjects)
                    {
                        switch (ctr.GetType().Name)
                        {
                            //case nameof(aCodeNText):
                            //    ((aCodeNText)ctr).ReadOnly = NF.Framework.Adv.Controls.aControlHelper.ControlEnum.ReadOnly.TotalReadOnly;
                            //    break;
                            //case nameof(aCodeText):
                            //    ((aCodeText)ctr).ReadOnly = NF.Framework.Adv.Controls.aControlHelper.ControlEnum.ReadOnly.TotalReadOnly;
                            //    break;
                            //case nameof(aCodeTextNonPopup):
                            //    ((aCodeTextNonPopup)ctr).ReadOnly = NF.Framework.Adv.Controls.aControlHelper.ControlEnum.ReadOnly.TotalReadOnly;
                            //    break;
                            case nameof(aCheckBox):
                                ((aCheckBox)ctr).Properties.ReadOnly = true;
                                break;
                            case nameof(aDateEdit):
                                ((aDateEdit)ctr).Properties.ReadOnly = true;
                                break;
                            case nameof(aRadioButton):
                                ((aRadioButton)ctr).Properties.ReadOnly = true;
                                break;
                            case nameof(aLookUpEdit):
                                ((aLookUpEdit)ctr).Properties.ReadOnly = true;
                                break;
                            case nameof(aNumericText):
                                ((aNumericText)ctr).Properties.ReadOnly = true;
                                break;
                            case nameof(aMemoEdit):
                                ((aMemoEdit)ctr).Properties.ReadOnly = true;
                                break;
                            case nameof(aTextEdit):
                                ((aTextEdit)ctr).Properties.ReadOnly = true;
                                break;
                            case nameof(aMaskEdit):
                                ((aMaskEdit)ctr).Properties.ReadOnly = true;
                                break;
                            case nameof(aPeriodEdit):
                                ((aPeriodEdit)ctr).ReadOnly = true;
                                break;
                            case nameof(aDateMonth):
                                ((aDateMonth)ctr).ReadOnly = true;
                                break;
                            default:
                                ctr.Enabled = false;
                                break;
                        }
                    }
                    #endregion
                }
            }

        }
        #endregion

        private void InitControls(Control container)
        {
            foreach (Control ctr in container.Controls)
            {
                //20171129 SplitContainer 추가
                if (ctr is Panel || ctr is XtraScrollableControl || ctr is PanelControl || ctr is TabControl || ctr is TabPage || ctr is XtraTabControl || ctr is XtraTabPage || ctr is aTabControl || ctr is SplitContainer)
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

        public void InitControlEvent()
        {
            foreach (Control ctr in _controls.Values)
            {
                switch (ctr.GetType().Name)
                {
                    case "TextEdit":
                        ((TextEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        break;
                    case nameof(aTextEdit):
                        ((aTextEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        break;
                    case nameof(aMaskEdit):
                        ((aMaskEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        ((aMaskEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);

                        break;
                    case nameof(DateEdit):
                    case nameof(aDateEdit):
                        ((DateEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        ((DateEdit)ctr).DateTimeChanged += new EventHandler(Control_Validated);
                        ((DateEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);

                        break;
                    case nameof(aDateMonth):
                        ((aDateMonth)ctr).TextChanged += new EventHandler(Control_Validated);
                        ((aDateMonth)ctr).DateTimeChanged += new EventHandler(Control_Validated);
                        break;
                    case nameof(CheckEdit):
                        ((CheckEdit)ctr).CheckedChanged += new EventHandler(Control_Validated);
                        ((CheckEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(aCheckBox):
                        ((aCheckBox)ctr).CheckedChanged += new EventHandler(Control_Validated);
                        ((aCheckBox)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(MemoEdit):
                    case nameof(aMemoEdit):
                        ((MemoEdit)ctr).TextChanged += new EventHandler(Control_Validated);
                        break;
                    case nameof(LookUpEdit):
                        ((LookUpEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        break;
                    case nameof(aLookUpEdit):
                        ((aLookUpEdit)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        break;
                    //case nameof(aCodeNText):
                    //    ((aCodeNText)ctr).CodeChanged += new EventHandler(Control_CodeChanged);
                    //    ((aCodeNText)ctr).TextBoxChanged += new EventHandler(Control_CodeChanged);
                    //    break;
                    //case nameof(aCodeText):
                    //    ((aCodeText)ctr).CodeChanged += new EventHandler(Control_CodeChanged);
                    //    break;
                    //case nameof(aCodeTextNonPopup):
                    //    ((aCodeTextNonPopup)ctr).CodeChanged += new EventHandler(Control_CodeChanged);
                    //    break;
                    case nameof(aRadioButton):
                        ((aRadioButton)ctr).SelectedIndexChanged += new EventHandler(Control_Validated);
                        break;
                    case nameof(aNumericText):
                        ((aNumericText)ctr).EditValueChanged += new EventHandler(Control_Validated);
                        ((aNumericText)ctr).DecimalValueChanged += new EventHandler(Control_Validated);
                        break;
                    case nameof(aPeriodEdit):
                        break;
                }

            }
        }

        public void InitControlEventDelete()
        {
            foreach (Control ctr in _controls.Values)
            {
                switch (ctr.GetType().Name)
                {
                    case nameof(TextEdit):
                        ((TextEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(aTextEdit):
                        ((aTextEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(aMaskEdit):
                        ((aMaskEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        ((aMaskEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);

                        break;
                    case nameof(DateEdit):
                    case nameof(aDateEdit):
                        ((DateEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        ((DateEdit)ctr).DateTimeChanged -= new EventHandler(Control_Validated);
                        ((DateEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(aDateMonth):
                        ((aDateMonth)ctr).TextChanged -= new EventHandler(Control_Validated);
                        ((aDateMonth)ctr).DateTimeChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(CheckEdit):
                        ((CheckEdit)ctr).CheckedChanged -= new EventHandler(Control_Validated);
                        ((CheckEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);

                        break;
                    case nameof(aCheckBox):
                        ((aCheckBox)ctr).CheckedChanged -= new EventHandler(Control_Validated);
                        ((aCheckBox)ctr).EditValueChanged -= new EventHandler(Control_Validated);

                        break;
                    case nameof(MemoEdit):
                    case nameof(aMemoEdit):
                        ((MemoEdit)ctr).TextChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(LookUpEdit):
                        ((LookUpEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(aLookUpEdit):
                        ((aLookUpEdit)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        break;
                    //case nameof(aCodeNText):
                    //    ((aCodeNText)ctr).CodeChanged -= new EventHandler(Control_CodeChanged);
                    //    break;
                    //case nameof(aCodeText):
                    //    ((aCodeText)ctr).CodeChanged -= new EventHandler(Control_CodeChanged);
                    //    break;
                    //case nameof(aCodeTextNonPopup):
                    //    ((aCodeTextNonPopup)ctr).CodeChanged -= new EventHandler(Control_CodeChanged);
                    //    break;
                    case nameof(aRadioButton):
                        ((aRadioButton)ctr).SelectedIndexChanged -= new EventHandler(Control_Validated);
                        break;
                    case nameof(aNumericText):
                        ((aNumericText)ctr).EditValueChanged -= new EventHandler(Control_Validated);
                        ((aNumericText)ctr).DecimalValueChanged -= new EventHandler(Control_Validated);
                        break;
                }

            }
        }

        #region Control Event

        void Control_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (sender is aCodeNText)
                //{
                //    if (((aCodeNText)sender).CodeValue == string.Empty)
                //    {
                //        string[] Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = string.Empty;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = string.Empty;

                //        SetValueToDataRow(sender);
                //        OnControlValueChanged(sender, new GridHelperArgs(_dt.Rows[0], _dataMode));
                //    }
                //    else
                //    {
                //        string[] Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = ((aCodeNText)sender).CodeValue;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = ((aCodeNText)sender).CodeName;

                //        SetValueToDataRow(sender);
                //        OnControlValueChanged(sender, new GridHelperArgs(_dt.Rows[0], _dataMode));
                //    }
                //}

                //else if (sender is aCodeText)
                //{
                //    if (((aCodeText)sender).CodeValue == string.Empty)
                //    {
                //        string[] Args = ((aCodeText)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = string.Empty;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = string.Empty;

                //        SetValueToDataRow(sender);
                //        OnControlValueChanged(sender, new GridHelperArgs(_dt.Rows[0], _dataMode));
                //    }
                //    else
                //    {
                //        string[] Args = ((aCodeText)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = ((aCodeText)sender).CodeValue;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = ((aCodeText)sender).CodeName;
                //    }
                //}

                //else if (sender is aCodeTextNonPopup)
                //{
                //    if (((aCodeTextNonPopup)sender).CodeValue == string.Empty)
                //    {
                //        string[] Args = ((aCodeTextNonPopup)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = string.Empty;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = string.Empty;

                //        SetValueToDataRow(sender);
                //        OnControlValueChanged(sender, new GridHelperArgs(_dt.Rows[0], _dataMode));
                //    }
                //    else
                //    {
                //        string[] Args = ((aCodeTextNonPopup)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = ((aCodeTextNonPopup)sender).CodeValue;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = ((aCodeTextNonPopup)sender).CodeName;
                //    }
                //}

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
                //if (sender is aCodeNText)
                //{
                //    if (((aCodeNText)sender).CodeValue != string.Empty)
                //    {
                //        string[] Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = ((aCodeNText)sender).CodeValue;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = ((aCodeNText)sender).CodeName;

                //        OnControlValueChanged(sender, new GridHelperArgs(_dt.Rows[_selectedRow], _dataMode));
                //    }
                //}
                //if (sender is aCodeText)
                //{
                //    if (((aCodeText)sender).CodeValue != string.Empty)
                //    {
                //        string[] Args = ((aCodeText)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = ((aCodeText)sender).CodeValue;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = ((aCodeText)sender).CodeName;

                //        OnControlValueChanged(sender, new GridHelperArgs(_dt.Rows[_selectedRow], _dataMode));
                //    }
                //}

                //if (sender is aCodeTextNonPopup)
                //{
                //    if (((aCodeTextNonPopup)sender).CodeValue != string.Empty)
                //    {
                //        string[] Args = ((aCodeTextNonPopup)sender).Tag.ToString().Split(';');
                //        if (Args == null || Args.Length != 2)
                //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //        _dt.Rows[_selectedRow][Args[0].ToString()] = ((aCodeTextNonPopup)sender).CodeValue;
                //        _dt.Rows[_selectedRow][Args[1].ToString()] = ((aCodeTextNonPopup)sender).CodeName;

                //        OnControlValueChanged(sender, new GridHelperArgs(_dt.Rows[_selectedRow], _dataMode));
                //    }
                //}
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
                if (_dt == null) return;
                if (_dt.Rows.Count == 0) return;
                switch (((Control)sender).GetType().Name)
                {
                    case nameof(TextEdit):
                        if (!((TextEdit)sender).IsModified) return;
                        break;
                    case nameof(aTextEdit):
                        if (!((aTextEdit)sender).IsModified) return;
                        break;
                    case nameof(DateEdit):
                    case nameof(aDateEdit):
                        if (!((DateEdit)sender).IsModified) return;
                        break;
                    case nameof(aDateMonth):
                        if (!((aDateMonth)sender).IsModified) return;
                        break;
                    case nameof(CheckEdit):
                    case nameof(aCheckBox):
                        if (!((CheckEdit)sender).IsModified) return;
                        break;
                    case nameof(MemoEdit):
                    case nameof(aMemoEdit):
                        if (!((MemoEdit)sender).IsModified) return;
                        break;
                    case nameof(LookUpEdit):
                        if (!((LookUpEdit)sender).IsModified) return;
                        break;
                    case nameof(aLookUpEdit):
                        if (!((aLookUpEdit)sender).IsModified) return;
                        break;
                    case nameof(aRadioButton):
                        if (!((aRadioButton)sender).IsModified) return;
                        break;
                    case nameof(aNumericText):
                        if (!((aNumericText)sender).IsModified) return;
                        break;
                }

                SetValueToDataRow(sender);
                OnControlValueChanged(sender, new GridHelperArgs(_dt.Rows[_selectedRow], _dataMode));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Control Event

        /// <summary>
        /// Value값을 Datatable Row에 반영합니다.
        /// </summary>
        /// <param name="sender"></param>
        private void SetValueToDataRow(object sender)
        {
            if (_dt == null || _dt.Rows.Count == 0)
                return;

            DataRow row = _dt.Rows[_selectedRow];
            if (row == null) return;

            if (((Control)sender).Tag == null)
                throw new Exception(((Control)sender).Name + " 컨트롤 Tag 속성에 매핑할 DataTable 컬럼명을 지정해야 합니다.");

            string[] Args;

            switch (((Control)sender).GetType().Name)
            {
                case nameof(TextEdit):
                case nameof(aTextEdit):
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;

                case nameof(aMaskEdit):
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;
                case nameof(DateEdit):
                case nameof(aDateEdit):
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;
                case nameof(aDateMonth):
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;
                case nameof(CheckEdit):
                case nameof(aCheckBox):
                    bool bChecked = ((CheckEdit)sender).Checked;

                    Args = ((CheckEdit)sender).Tag.ToString().Split(';', ',');
                    if (Args == null || Args.Length != 3)
                        throw new Exception("CheckBox Tag 속성은 <데이터컬럼명;체크값,체크해제값> 형태로 지정되어야 합니다.");

                    if (bChecked)
                    {
                        _dt.Rows[_selectedRow][Args[0]] = Args[1];
                    }
                    else
                    {
                        _dt.Rows[_selectedRow][Args[0]] = Args[2];
                    }
                    break;
                case nameof(MemoEdit):
                case nameof(aMemoEdit):
                    row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                    break;
                case nameof(LookUpEdit):
                case nameof(aLookUpEdit):
                    row[((Control)sender).Tag.ToString()] = ((aLookUpEdit)sender).EditValue;
                    break;
                case nameof(aRadioButton):
                    row[((Control)sender).Tag.ToString()] = ((aRadioButton)sender).Properties.Items[((aRadioButton)sender).SelectedIndex].Tag;
                    break;
                case nameof(aNumericText):
                    row[((Control)sender).Tag.ToString()] = ((aNumericText)sender).EditValue;
                    break;

                //case nameof(aCodeText):
                //    Args = ((aCodeText)sender).Tag.ToString().Split(';');
                //    if (Args == null || Args.Length != 2)
                //        throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //    row[Args[0].ToString()] = ((aCodeText)sender).CodeValue;
                //    row[Args[1].ToString()] = ((aCodeText)sender).CodeName;
                //    break;

                //case nameof(aCodeNText):
                //    Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                //    if (Args == null || Args.Length != 2)
                //        throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //    row[Args[0].ToString()] = ((aCodeNText)sender).CodeValue;
                //    row[Args[1].ToString()] = ((aCodeNText)sender).CodeName;
                //    break;
                //case nameof(aCodeTextNonPopup):
                //    Args = ((aCodeNText)sender).Tag.ToString().Split(';');
                //    if (Args == null || Args.Length != 2)
                //        throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                //    row[Args[0].ToString()] = ((aCodeTextNonPopup)sender).CodeValue;
                //    row[Args[1].ToString()] = ((aCodeTextNonPopup)sender).CodeName;
                //    break;
            }
        }

        #region SetValueToControl
        /// <summary>
        /// DataTable의 Row 데이터를 컨트롤에 반영합니다.
        /// </summary>
        /// <param name="row_no"></param>
        private void SetValueToControl(DataRow row)
        {

            Control ctl = null;
            if (_controls == null)
                return;
            if (row != null && row.RowState == DataRowState.Deleted)
                return;
            foreach (string key in _controls.Keys)
            {

                ctl = _controls[key];
                switch (_controls[key].GetType().Name)
                {
                    case nameof(TextEdit):
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case nameof(aTextEdit):
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case nameof(aMaskEdit):
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case nameof(DateEdit):
                    case nameof(aDateEdit):
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case nameof(aDateMonth):
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case nameof(CheckEdit):
                        if (row == null)
                        {
                            ctl.GetType().GetProperty("Checked").SetValue(ctl, false);
                            //((CheckEdit)ctl).Checked = false;
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
                    case nameof(aCheckBox):
                        if (row == null)
                        {
                            ((aCheckBox)ctl).Checked = false;
                        }
                        else
                        {
                            string[] Args = key.ToString().Split(';', ',');
                            if (Args == null || Args.Length != 3)
                                throw new Exception("CheckEdit Tag는 <데이터컬럼명;체크값,체크해제값> 형태로 지정되어야 합니다.");

                            if (string.IsNullOrEmpty(Args[1]) || string.IsNullOrEmpty(Args[2]))
                                throw new Exception("CheckEdit Tag는 <데이터컬럼명;체크값,체크해제값> 형태로 지정되어야 합니다.");

                            if (row[Args[0]].ToString().ToUpper() == Args[1].ToUpper())
                                ((aCheckBox)ctl).Checked = true;
                            else
                                ((aCheckBox)ctl).Checked = false;

                            ((aCheckBox)ctl).IsFreeBinding = true;
                        }

                        break;
                    case nameof(MemoEdit):
                    case nameof(aMemoEdit):
                        ctl.Text = row == null ? string.Empty : row[key].ToString();
                        break;
                    case nameof(LookUpEdit):
                        ((LookUpEdit)ctl).EditValue = row == null ? string.Empty : row[key].ToString();
                        break;
                    case nameof(aLookUpEdit):
                        ((aLookUpEdit)ctl).EditValue = row == null ? string.Empty : row[key].ToString();
                        break;
                    //case nameof(aCodeNText):
                    //    if (row == null)
                    //    {
                    //        ((aCodeNText)ctl).CodeValue = string.Empty;
                    //        ((aCodeNText)ctl).CodeName = string.Empty;
                    //    }
                    //    else
                    //    {
                    //        string[] Args = key.ToString().Split(';');
                    //        if (Args == null || Args.Length != 2)
                    //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                    //        ((aCodeNText)ctl).CodeValue = row[Args[0]].ToString();
                    //        ((aCodeNText)ctl).CodeName = row[Args[1]].ToString();
                    //    }
                    //    break;
                    //case nameof(aCodeText):
                    //    if (row == null)
                    //    {
                    //        ((aCodeText)ctl).CodeValue = string.Empty;
                    //        ((aCodeText)ctl).CodeName = string.Empty;
                    //    } 
                    //    else
                    //    {
                    //        string[] Args = key.ToString().Split(';');
                    //        if (Args == null || Args.Length != 2)
                    //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                    //        ((aCodeText)ctl).CodeValue = row[Args[0]].ToString();
                    //        ((aCodeText)ctl).CodeName = row[Args[1]].ToString();
                    //    }
                    //    break;
                    //case nameof(aCodeTextNonPopup):
                    //    if (row == null)
                    //    {
                    //        ((aCodeTextNonPopup)ctl).CodeValue = string.Empty;
                    //        ((aCodeTextNonPopup)ctl).CodeName = string.Empty;
                    //    }
                    //    else
                    //    {
                    //        string[] Args = key.ToString().Split(';');
                    //        if (Args == null || Args.Length != 2)
                    //            throw new Exception("Tag 속성은 <CodeValue데이터컬럼명;CodeName데이터컬럼명> 형태로 지정되어야 합니다.");

                    //        ((aCodeTextNonPopup)ctl).CodeValue = row[Args[0]].ToString();
                    //        ((aCodeTextNonPopup)ctl).CodeName = row[Args[1]].ToString();
                    //    }
                    //    break;
                    case nameof(aRadioButton):
                        if (row != null)
                        {
                            ((RadioGroup)ctl).EditValue = row[key].ToString();
                        }
                        break;
                    case nameof(aNumericText):
                        ((aNumericText)ctl).EditValue = row == null ? string.Empty : row[key].ToString();
                        break;
                }

            }
        }

        private void SetValueToControl(int selectedRowNo)
        {
            //if (_dt.Rows.Count <= selectedRowNo)
            //    _dv.Table;

            if (_dt.Rows.Count == 0) return;
            if (_dt.Rows.Count <= selectedRowNo) return;
            if (selectedRowNo < 0) return;

            DataTable dtCahnged = _dt.Copy();
            dtCahnged.AcceptChanges();

            SetValueToControl(dtCahnged.Rows[selectedRowNo]);
        }

        private void SetValueToControl(string[] seletedNodeKey)
        {
            DataRow foundRow = _dt.Rows.Find(seletedNodeKey);
            SetValueToControl(foundRow);
        }

        private void SetValueToControl(string seletedNodeKey)
        {
            DataRow foundRow = _dt.Rows.Find(seletedNodeKey);
            SetValueToControl(foundRow);
        }

        public void SelectRow(int selectedRowNo)
        {
            SetValueToControl(selectedRowNo);
        }
        #endregion
    }
}
