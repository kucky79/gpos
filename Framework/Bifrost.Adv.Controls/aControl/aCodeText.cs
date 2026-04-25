using Bifrost;

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Bifrost.Adv.Controls.PopUp;
using Bifrost.Adv.Controls.aControlHelper;
using System.Drawing;

namespace Bifrost.Adv.Controls
{
    public partial class aCodeText : ControlBase
    {
        DataTable dt = new DataTable();
        DataRow drRow;
        private const int FIXED_HEIGHT = 24;


        private bool _IsValidating = false;
        //private bool _UseUpperCase = true;
        private bool _isResultChk = false;
        private bool _isPopUpOpen = false;

        //protected PopUpHelper.PopUpID _PopUpID;

        private PopUpReturn _PopUpReturn = new PopUpReturn();
        private PopUpParam _PopUpParams = new PopUpParam();

        private string _CodeValue = string.Empty;
        private string _CodeName = string.Empty;
        private string _OldCodeValue = string.Empty;
        private string _OldCodeName = string.Empty;

        private Color _BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));

        [Browsable(false)]
        public string CodeValue
        {
            get { return _CodeValue; }
            set
            {
                if (IsCodeValueToUpper)
                {
                    if (value != null)
                    {
                        _CodeBox.TextChanged -= _CodeBox_TextChanged;

                        if (_UseUpperCase)
                        {
                            _CodeValue = value.ToUpper();
                            _OldCodeValue = value.ToUpper();

                            _CodeBox.Text = _CodeValue;
                        }
                        else
                        {
                            _CodeValue = value;
                            _OldCodeValue = value;

                            _CodeBox.Text = _CodeValue;
                        }
                        _CodeBox.TextChanged += _CodeBox_TextChanged;

                        return;
                    }
                }
                _CodeValue = value;
                _OldCodeValue = value;


                if (_CodeBox.Text == string.Empty)
                    _isResultChk = false;
                else
                    _isResultChk = true;
            }
        }

        [Browsable(false)]
        public string CodeName
        {
            get { return _CodeName; }
            set
            {
                _CodeBox.TextChanged -= _CodeBox_TextChanged;

                _CodeBox.Text = value;
                _CodeName = value;
                _OldCodeName = value;
                _CodeboxText = _CodeBox.Text;
                _CodeBox.TextChanged += _CodeBox_TextChanged;


                if (_CodeBox.Focused)
                {
                    _CodeBox.Text = CodeValue;
                    _CodeBox.SelectAll();
                }

                if (_CodeBox.Text == string.Empty)
                    _isResultChk = false;
                else
                    _isResultChk = true;

                //포커스가 있으면 codevlaue를 보여줌
                if (_CodeBox.Focused)
                {
                    _CodeBox.Text = CodeValue;
                }
            }
        }

        [Category("AIMS"), Browsable(true), Description("ReadOnly를 설정합니다.")]
        public ControlEnum.ReadOnly ReadOnly
        {
            get
            {
                return base._ReadOnly;
            }
            set
            {
                base._ReadOnly = value;

                if (base._ReadOnly == ControlEnum.ReadOnly.TotalNotReadOnly)
                {
                    _CodeBox.BackColor = _ItemBackColor;

                    _Button.Enabled = true;
                    _CodeBox.ReadOnly = false;
                }
                else if (base._ReadOnly == ControlEnum.ReadOnly.TotalReadOnly)
                {
                    _CodeBox.BackColor = System.Drawing.SystemColors.Control;

                    _Button.Enabled = false;
                    _CodeBox.ReadOnly = true;
                }
                else if (base._ReadOnly == ControlEnum.ReadOnly.TextBoxReadOnly)
                {
                    _CodeBox.BackColor = _ItemBackColor;
                    _Button.Enabled = true;
                    _CodeBox.ReadOnly = false;
                }
                else if (base._ReadOnly == ControlEnum.ReadOnly.ButtonReadOnly)
                {
                    _CodeBox.BackColor = _ItemBackColor;
                    _Button.Enabled = false;
                    _CodeBox.ReadOnly = false;
                }
                else if (base._ReadOnly == ControlEnum.ReadOnly.None)
                {
                    _CodeBox.BackColor = _ItemBackColor;
                    _Button.Enabled = true;
                    _CodeBox.ReadOnly = false;
                }
            }
        }

        public bool ShouldSerializeReadOnly() { return ReadOnly != ControlEnum.ReadOnly.None; }


        [Browsable(true)]
        public event EventHandler CodeChanged;

        public event aControlEventHandler AfterCodeValueChanged;
        public event aControlEventHandler BeforeCodeValueChanged;

        public aCodeText()
        {
            InitializeComponent();
            InitEvent();
            this.AutoSize = false;
            this._CodeBox.AutoSize = false;

            this._Button.Image = Images.btn_search_default;
            this._Button.HoverImage = Images.btn_search_HOver;
            this._Button.DisabledImage = Images.btn_search_disable;
            this._Button.ImageAlign = ContentAlignment.MiddleCenter;
            this._Button.HoverColor = Color.White;
            this._Button.BorderColor = _BorderColor;
        }

        private void InitEvent()
        {
            _Button.Click += _Button_Click;
            _CodeBox.Leave += _CodeBox_Leave;
            _CodeBox.Enter += _CodeBox_Enter;
            _CodeBox.KeyDown += _CodeBox_KeyDown;
            _CodeBox.TextChanged += _CodeBox_TextChanged;
            _CodeBox.DoubleClick += _CodeBox_DoubleClick;
            _CodeBox.Click += _CodeBox_Click;

            _CodeBox.GotFocus += Control_GotFocus;
            //_CodeBox.LostFocus += _CodeBox_LostFocus;

            BeforeCodeChange();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _CodeBox.Text = CodeValue;

            _CodeBox.SelectAll();
            this._CodeBox.Focus();

            base.OnGotFocus(e);
        }

        private string _CodeboxText = string.Empty;
        private void Control_GotFocus(object sender, EventArgs e)
        {
            _CodeboxText = _CodeBox.Text;
            _CodeBox.Text = CodeValue;
            //_CodeBox.ImeMode = ImeMode.Off;
            //_CodeBox.CharacterCasing = CharacterCasing.Upper;
        }

        private void _CodeBox_Enter(object sender, EventArgs e)
        {
            _CodeboxText = _CodeBox.Text;
            _CodeBox.Text = CodeValue;
        }

        private void _CodeBox_Leave(object sender, EventArgs e)
        {
            if (_OldCodeName != _CodeboxText)//_CodeBox.Text)
            {
                _isResultChk = false;
                

            }
            //if(!_isPopUpOpen)
            SetEvent();

            _CodeBox.Text = CodeName;
        }
        private void _CodeBox_LostFocus(object sender, EventArgs e)
        {
            if (_OldCodeName != _CodeboxText)//_CodeBox.Text)
            {
                _isResultChk = false;
                
            }
            //if(!_isPopUpOpen)
            SetEvent();

            _CodeBox.Text = CodeName;
        }



        private void _CodeBox_Click(object sender, EventArgs e)
        {
            _CodeBox.SelectAll();
        }

        private void _CodeBox_DoubleClick(object sender, EventArgs e)
        {
            CallPopUp();
        }

        #region Event모음

        private void _CodeBox_TextChanged(object sender, EventArgs e)
        {
            if (_CodeBox.Text == string.Empty)
            {
                Clear();

                aControlEventArgs args = new aControlEventArgs(_PopUpReturn, _PopUpParams, this);
                _PopUpReturn.CodeName = string.Empty;
                _PopUpReturn.CodeValue = string.Empty;
                _PopUpReturn.PopUpID = PopUpID;
                //지웠을때 이벤트 태우려고
                this.CodeChanged?.Invoke(this, null);
                this.AfterCodeValueChanged?.Invoke(this, args);
            }

            if (_CodeBox.Focused)
            {
                _CodeBox.Text = CodeValue;
            }

        }
        private void _CodeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                _CodeValue = _CodeBox.Text;

                if (_OldCodeValue != _CodeValue)
                {
                    _isResultChk = false;
                    

                }
                SetEvent();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                CodeName = _OldCodeName;
                CodeValue = _OldCodeValue;
                _CodeBox.Text = CodeValue;

                _isPopUpOpen = false;

            }
        }



        private void _Button_Click(object sender, EventArgs e)
        {
            CallPopUp();
        }
        #endregion

        private void CallPopUp()
        {
            if (_isPopUpOpen)
                return;

            string strSearch = string.Empty;
            if (!_isResultChk)
            {
                strSearch = _CodeBox.Text;
            }

            #region 도움창
            switch (this.PopUpID)
            {
                //#region  코드도움창
                //case PopUpHelper.PopUpID.POPUP_CODE:
                //    POPUP_CODE POPUP_CODE = new POPUP_CODE(_Code, _CodeBox.Text);
                //    _isPopUpOpen = true;

                //    POPUP_CODE.AutoSearch = true;
                //    if (POPUP_CODE.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_CODE.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CODE", "NAME");
                //        }
                //        if (POPUP_CODE != null)
                //        {
                //            POPUP_CODE.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region Country 도움창
                //case PopUpHelper.PopUpID.POPUP_COUNTRY:
                //    POPUP_COUNTRY POPUP_COUNTRY = new POPUP_COUNTRY(_Code, _CodeBox.Text);
                //    _isPopUpOpen = true;

                //    POPUP_COUNTRY.AutoSearch = true;
                //    if (POPUP_COUNTRY.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_COUNTRY.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_COUNTRY", "NM_COUNTRY");
                //        }
                //        if (POPUP_COUNTRY != null)
                //        {
                //            POPUP_COUNTRY.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region Location도움창
                //case PopUpHelper.PopUpID.POPUP_LOCATION:
                //    POPUP_LOCATION POPUP_LOCATION = new POPUP_LOCATION(_Code, _CodeBox.Text, UserPopUpParam);
                //    _isPopUpOpen = true;

                //    if (POPUP_LOCATION.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_LOCATION.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_LOC", "NM_LOC");
                //        }
                //        if (POPUP_LOCATION != null)
                //        {
                //            POPUP_LOCATION.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region Partner 도움창
                //case PopUpHelper.PopUpID.POPUP_PARTNER:
                //    POPUP_PARTNER POPUP_PARTNER = new POPUP_PARTNER(strSearch);
                //    _isPopUpOpen = true;
                //    //POPUP_PARTNER.AutoSearch = true;
                //    if (POPUP_PARTNER.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_PARTNER.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_PARTNER", "NM_PARTNER_ENG");
                //        }
                //        if (POPUP_PARTNER != null)
                //        {
                //            POPUP_PARTNER.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;
                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }
                //    break;
                //#endregion

                //#region  GL CODE 도움창
                //case PopUpHelper.PopUpID.POPUP_GL:
                //    POPUP_GL POPUP_GL = new POPUP_GL(_CodeBox.Text);
                //    _isPopUpOpen = true;

                //    if (POPUP_GL.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_GL.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_GL", "DC_RMK");
                //        }
                //        if (POPUP_GL != null)
                //        {
                //            POPUP_GL.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region  BANK 도움창
                //case PopUpHelper.PopUpID.POPUP_BANK:
                //    POPUP_BANK POPUP_BANK = new POPUP_BANK(_CodeBox.Text);
                //    _isPopUpOpen = true;

                //    if (POPUP_BANK.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_BANK.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_BANK", "NM_BANK");
                //        }
                //        if (POPUP_BANK != null)
                //        {
                //            POPUP_BANK.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region PICKUP ORDER 도움창
                //case PopUpHelper.PopUpID.POPUP_PICKUP_ORDER:
                //    POPUP_PICKUP_ORDER POPUP_PICKUP_ORDER = new POPUP_PICKUP_ORDER(_CodeBox.Text);
                //    _isPopUpOpen = true;

                //    if (POPUP_PICKUP_ORDER.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_PICKUP_ORDER.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("NO_PICKUP", "NO_PICKUP");
                //        }
                //        if (POPUP_PICKUP_ORDER != null)
                //        {
                //            POPUP_PICKUP_ORDER.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region  EMPLOYEE 도움창
                //case PopUpHelper.PopUpID.POPUP_EMPLOYEE:
                //    POPUP_EMPLOYEE POPUP_EMPLOYEE = new POPUP_EMPLOYEE(_CodeBox.Text);
                //    _isPopUpOpen = true;
                //    POPUP_EMPLOYEE.AutoSearch = true;

                //    if (POPUP_EMPLOYEE.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_EMPLOYEE.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_EMP", "NM_EMP");
                //        }
                //        if (POPUP_EMPLOYEE != null)
                //        {
                //            POPUP_EMPLOYEE.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region  오피스 도움창
                //case PopUpHelper.PopUpID.POPUP_BIZ:
                //    POPUP_BIZ POPUP_BIZ = new POPUP_BIZ(_CodeBox.Text);
                //    _isPopUpOpen = true;
                //    POPUP_BIZ.AutoSearch = true;
                //    if (POPUP_BIZ.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_BIZ.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_BIZ", "NM_BIZ");
                //        }
                //        if (POPUP_BIZ != null)
                //        {
                //            POPUP_BIZ.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region  부서 도움창
                //case PopUpHelper.PopUpID.POPUP_DEPT:
                //    POPUP_DEPT POPUP_DEPT = new POPUP_DEPT(_CodeBox.Text);
                //    _isPopUpOpen = true;
                //    if (POPUP_DEPT.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_DEPT.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_DEPT", "NM_DEPT");
                //        }
                //        if (POPUP_DEPT != null)
                //        {
                //            POPUP_DEPT.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region  발령 도움창
                //case PopUpHelper.PopUpID.POPUP_APNTCODE:
                //    POPUP_APNTCODE POPUP_APNTCODE = new POPUP_APNTCODE(_CodeBox.Text);
                //    _isPopUpOpen = true;
                //    if (POPUP_APNTCODE.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_APNTCODE.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_APNT", "NM_APNT");
                //        }
                //        if (POPUP_APNTCODE != null)
                //        {
                //            POPUP_APNTCODE.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region  교육과정 도움창
                //case PopUpHelper.PopUpID.POPUP_EDUCODE:
                //    POPUP_EDUCODE POPUP_EDUCODE = new POPUP_EDUCODE(_CodeBox.Text);
                //    _isPopUpOpen = true;
                //    if (POPUP_EDUCODE.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_EDUCODE.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_EDU", "NM_EDU");
                //        }
                //        if (POPUP_EDUCODE != null)
                //        {
                //            POPUP_EDUCODE.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }
                //    break;
                //#endregion

                //#region  강사 도움창
                //case PopUpHelper.PopUpID.POPUP_HRSLCTR:
                //    POPUP_HRSLCTR POPUP_HRSLCTR = new POPUP_HRSLCTR(_CodeBox.Text);
                //    _isPopUpOpen = true;
                //    if (POPUP_HRSLCTR.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_HRSLCTR.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_LCTR", "NM_LCTR");
                //        }
                //        if (POPUP_HRSLCTR != null)
                //        {
                //            POPUP_HRSLCTR.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;

                //#endregion

                //#region  근태 도움창
                //case PopUpHelper.PopUpID.POPUP_DGCODE:
                //    POPUP_DGCODE POPUP_DGCODE = new POPUP_DGCODE(_CodeBox.Text);
                //    _isPopUpOpen = true;
                //    if (POPUP_DGCODE.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_DGCODE.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_DGCODE", "NM_DG");
                //        }
                //        if (POPUP_DGCODE != null)
                //        {
                //            POPUP_DGCODE.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;

                //#endregion

                //#region  양식 도움창
                //case PopUpHelper.PopUpID.POPUP_PFORM:
                //    POPUP_PFORM POPUP_PFORM = new POPUP_PFORM(_Code, _CodeBox.Text);
                //    _isPopUpOpen = true;
                //    if (POPUP_PFORM.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_PFORM.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_FORM", "NM_FORM");
                //        }
                //        if (POPUP_PFORM != null)
                //        {
                //            POPUP_PFORM.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;

                //#endregion

                //#region  CC 도움창
                //case PopUpHelper.PopUpID.POPUP_CC:
                //    POPUP_CC POPUP_CC = new POPUP_CC(_CodeBox.Text);
                //    _isPopUpOpen = true;
                //    if (POPUP_CC.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_CC.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_CC", "NM_CC");
                //        }
                //        if (POPUP_CC != null)
                //        {
                //            POPUP_CC.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region  지출결의 도움창
                //case PopUpHelper.PopUpID.POPUP_VCR:
                //    POPUP_VCR POPUP_VCR = new POPUP_VCR();
                //    _isPopUpOpen = true;
                //    if (POPUP_VCR.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_VCR.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("NO_APP", "NO_APP");
                //        }
                //        if (POPUP_VCR != null)
                //        {
                //            POPUP_VCR.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                            

                //        }
                //    }

                //    break;
                //#endregion

                //#region 품목 도움창
                //case PopUpHelper.PopUpID.POPUP_ITEM:
                //    POPUP_ITEM_S POPUP_ITEM_S = new POPUP_ITEM_S();
                //    POPUP_ITEM_S.ItemName = _CodeBox.Text;
                //    if(POPUP_ITEM_S.ItemName != string.Empty)
                //        POPUP_ITEM_S.AutoSearch = true;
                //    _isPopUpOpen = true;
                //    if (POPUP_ITEM_S.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_ITEM_S.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_ITEM", "NM_ITEM");
                //        }
                //        if (POPUP_ITEM_S != null)
                //        {
                //            POPUP_ITEM_S.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                //        }
                //    }

                //    break;
                //#endregion

                //#region ENR 도움창
                //case PopUpHelper.PopUpID.POPUP_ENR:
                //    POPUP_ENR POPUP_ENR = new POPUP_ENR(_Code);
                //    _isPopUpOpen = true;
                //    if (POPUP_ENR.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_ENR.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_ENR", "NM_ENR");
                //        }
                //        if (POPUP_ENR != null)
                //        {
                //            POPUP_ENR.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                //        }
                //    }

                //    break;
                //#endregion

                //#region CUST 도움창
                //case PopUpHelper.PopUpID.POPUP_CUST:
                //    POPUP_CUST POPUP_CUST = new POPUP_CUST(strSearch);
                //    _isPopUpOpen = true;
                //    //POPUP_PARTNER.AutoSearch = true;
                //    if (POPUP_CUST.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_CUST.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_CUST", "NM_CUST");
                //        }
                //        if (POPUP_CUST != null)
                //        {
                //            POPUP_CUST.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;
                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                //        }
                //    }
                //    break;
                //#endregion

                //#region AGENT 도움창
                //case PopUpHelper.PopUpID.POPUP_AGENT:
                //    POPUP_AGENT POPUP_AGENT = new POPUP_AGENT(strSearch);
                //    _isPopUpOpen = true;
                //    //POPUP_PARTNER.AutoSearch = true;
                //    if (POPUP_AGENT.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_AGENT.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_AGENT", "NM_AGENT");
                //        }
                //        if (POPUP_AGENT != null)
                //        {
                //            POPUP_AGENT.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;
                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                //        }
                //    }
                //    break;
                //#endregion

                //#region EQUIP 도움창
                //case PopUpHelper.PopUpID.POPUP_EQUIP:
                //    POPUP_EQUIP POPUP_EQUIP = new POPUP_EQUIP(strSearch);
                //    _isPopUpOpen = true;
                //    //POPUP_PARTNER.AutoSearch = true;
                //    if (POPUP_EQUIP.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_EQUIP.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_EQUIP", "NM_EQUIP");
                //        }
                //        if (POPUP_EQUIP != null)
                //        {
                //            POPUP_EQUIP.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;
                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;


                //        }
                //    }
                //    break;
                //#endregion

                //#region OP 도움창
                //case PopUpHelper.PopUpID.POPUP_OP:
                //    POPUP_OPERATION POPUP_OPERATION = new POPUP_OPERATION(strSearch);
                //    _isPopUpOpen = true;
                //    //POPUP_PARTNER.AutoSearch = true;
                //    if (POPUP_OPERATION.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_OPERATION.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_OP", "NM_OP");
                //        }
                //        if (POPUP_OPERATION != null)
                //        {
                //            POPUP_OPERATION.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;
                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;


                //        }
                //    }
                //    break;
                //#endregion

                //#region 외주처 도움창
                //case PopUpHelper.PopUpID.POPUP_CUST_SCT:
                //    POPUP_CUST POPUP_CUST_SCT = new POPUP_CUST(strSearch);
                //    POPUP_CUST_SCT.sctYN = "Y";
                //    _isPopUpOpen = true;
                //    //POPUP_PARTNER.AutoSearch = true;
                //    if (POPUP_CUST_SCT.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_CUST_SCT.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_CUST", "NM_CUST");
                //        }
                //        if (POPUP_CUST_SCT != null)
                //        {
                //            POPUP_CUST_SCT.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;
                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;


                //        }
                //    }
                //    break;
                //#endregion

                //#region 품목군 도움창
                //case PopUpHelper.PopUpID.POPUP_ITEMGRP:
                //    POPUP_ITEMGRP_S POPUP_ITEMGRP_S = new POPUP_ITEMGRP_S();
                //    POPUP_ITEMGRP_S.ItemName = _CodeBox.Text;
                //    if (POPUP_ITEMGRP_S.ItemName != string.Empty)
                //        POPUP_ITEMGRP_S.AutoSearch = true;
                //    _isPopUpOpen = true;
                //    if (POPUP_ITEMGRP_S.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_ITEMGRP_S.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_GROUP", "NM_GROUP");
                //        }
                //        if (POPUP_ITEMGRP_S != null)
                //        {
                //            POPUP_ITEMGRP_S.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                //        }
                //    }

                //    break;
                //#endregion

                //#region 창고 도움창
                //case PopUpHelper.PopUpID.POPUP_WH:
                //    POPUP_WH POPUP_WH = new POPUP_WH();
                //    POPUP_WH.ItemName = _CodeBox.Text;
                //    if (POPUP_WH.ItemName != string.Empty)
                //        POPUP_WH.AutoSearch = true;
                //    _isPopUpOpen = true;
                //    if (POPUP_WH.ShowDialog() == DialogResult.OK)
                //    {
                //        drRow = (DataRow)POPUP_WH.ReturnData["ReturnData"];
                //        if (drRow != null)
                //        {
                //            SetData("CD_WH", "NM_WH");
                //        }
                //        if (POPUP_WH != null)
                //        {
                //            POPUP_WH.Dispose();
                //        }
                //    }
                //    else
                //    {
                //        _isPopUpOpen = false;

                //        if (!_isResultChk)
                //        {
                //            _CodeBox.Text = string.Empty;
                //            _isResultChk = false;
                //        }
                //    }

                //    break;


                //    #endregion
            }
            #endregion


        }

        private void SetData(string code, string name)
        {
            CodeValue = drRow[code].ToString().Trim();
            CodeName = drRow[name].ToString().Trim();

            _CodeValue = drRow[code].ToString().Trim();
            _CodeName = drRow[name].ToString().Trim();

            _OldCodeValue = drRow[code].ToString().Trim();
            _OldCodeName = drRow[name].ToString().Trim();

            _CodeBox.Text = CodeValue;
            //Select();
            _isResultChk = true;
            

            _isPopUpOpen = false;

            AfterCodechange();
            this.CodeChanged?.Invoke(this, null);
        }


        private void CallNullValue()
        {
            string BlankYN = "N"; //디폴트 
            string CondTP = "C"; //팝업도움창에 쓸건지 아닌지

            switch (this.PopUpID)
            {
                //#region MAS_Code 도움창
                //case PopUpHelper.PopUpID.POPUP_CODE:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_CODE_S", new object[] { Global.FirmCode, _Code, BlankYN, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region  Country 도움창
                //case PopUpHelper.PopUpID.POPUP_COUNTRY:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_COUNTRY_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });
                //    break;
                //#endregion

                //#region  Location도움창
                //case PopUpHelper.PopUpID.POPUP_LOCATION:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_LOCATION_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP, CH.SplitString(UserPopUpParam, ";")[0] });

                //    break;
                //#endregion

                //#region  거래처도움창
                //case PopUpHelper.PopUpID.POPUP_PARTNER:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_PARTNER_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region GL CODE 도움창
                //case PopUpHelper.PopUpID.POPUP_GL:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_GL_S", new object[] { Global.FirmCode, _CodeBox.Text, "", "", "", CondTP });

                //    break;
                //#endregion

                //#region BANK 도움창
                //case PopUpHelper.PopUpID.POPUP_BANK:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_BANK_S", new object[] { Global.FirmCode, _CodeBox.Text, "", "", CondTP });
                //    break;
                //#endregion

                //#region PICKUP ORDER 도움창
                //case PopUpHelper.PopUpID.POPUP_PICKUP_ORDER:

                //    dt = DBHelper.GetDataTable("AP_H_INT_PICKUP_S", new object[] { Global.FirmCode, Global.BizCode, _CodeBox.Text, "", "", "", 0, 0, 0, 0 });

                //    break;
                //#endregion

                //#region EMPLOYEE 도움창
                //case PopUpHelper.PopUpID.POPUP_EMPLOYEE:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_EMPLOYEE_S", new object[] { Global.FirmCode, "", _CodeBox.Text, "", "", CondTP });

                //    break;
                //#endregion

                //#region Office 도움창
                //case PopUpHelper.PopUpID.POPUP_BIZ:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_BIZ_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region 부서 도움창
                //case PopUpHelper.PopUpID.POPUP_DEPT:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_DEPT_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region 발령 도움창
                //case PopUpHelper.PopUpID.POPUP_APNTCODE:

                //    dt = DBHelper.GetDataTable("AP_H_HRS_APNTCODE_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region  교육과정 도움창
                ////case PopUpHelper.PopUpID.POPUP_EDUCODE:

                //case PopUpHelper.PopUpID.POPUP_EDUCODE:

                //    dt = DBHelper.GetDataTable("AP_H_HRS_EDUCODE_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });


                //    break;
                //#endregion

                //#region  강사 도움창

                //case PopUpHelper.PopUpID.POPUP_HRSLCTR:

                //    dt = DBHelper.GetDataTable("AP_H_HRS_LCTRODE_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });


                //    break;
                //#endregion

                //#region  근태 도움창

                //case PopUpHelper.PopUpID.POPUP_DGCODE:

                //    dt = DBHelper.GetDataTable("AP_H_HRS_DGCODE_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region  양식 도움창

                //case PopUpHelper.PopUpID.POPUP_PFORM:

                //    dt = DBHelper.GetDataTable("AP_H_HRS_PFORM_S", new object[] { Global.FirmCode, _Code, _CodeBox.Text });

                //    break;

                //#endregion

                //#region CC 도움창
                //case PopUpHelper.PopUpID.POPUP_CC:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_CC_S", new object[] { Global.FirmCode, _CodeBox.Text });

                //    break;
                //#endregion

                //#region 지출결의 도움창
                //case PopUpHelper.PopUpID.POPUP_VCR:

                //    dt = DBHelper.GetDataTable("AP_GRW_VCR_POP_S", new object[] { Global.FirmCode, _CodeBox.Text, Global.EmpCode });

                //    break;
                //#endregion

                //#region 품목 도움창
                //case PopUpHelper.PopUpID.POPUP_ITEM:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_ITEM_S", new object[] { Global.FirmCode, _CodeBox.Text, "", "", "", "", "", "", "", CondTP });
                //    break;
                //#endregion

                //#region 소득자
                //case PopUpHelper.PopUpID.POPUP_ENR:

                //    dt = DBHelper.GetDataTable("AP_H_HRS_ENR_S", new object[] { Global.FirmCode, _Code,_CodeBox.Text, CondTP });
                //    break;
                //#endregion

                //#region  거래처도움창
                //case PopUpHelper.PopUpID.POPUP_CUST:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_CUST_S", new object[] { Global.FirmCode, string.Empty, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region  EQUIP도움창
                //case PopUpHelper.PopUpID.POPUP_EQUIP:

                //    dt = DBHelper.GetDataTable("AP_H_PRD_EQUIP_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region   OP 도움창
                //case PopUpHelper.PopUpID.POPUP_OP:

                //    dt = DBHelper.GetDataTable("AP_H_PRD_OPERATION_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    break;
                //#endregion

                //#region  외주처
                //case PopUpHelper.PopUpID.POPUP_CUST_SCT:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_CUST_S", new object[] { Global.FirmCode, string.Empty, _CodeBox.Text, CondTP, "9" });

                //    break;
                //#endregion
/*
                #region  품목군
                case PopUpHelper.PopUpID.POPUP_ITEMGRP:

                    dt = DBHelper.GetDataTable("AP_H_MAS_CUST_S", new object[] { Global.FirmCode, string.Empty });

                    break;
                #endregion

                #region  창고
                case PopUpHelper.PopUpID.POPUP_WH:

                    dt = DBHelper.GetDataTable("AP_H_MAS_WH_S", new object[] { Global.FirmCode, string.Empty, _CodeBox.Text, CondTP });

                    break;
                    #endregion
                */
            }




        }
        //if (_isResultChk)
        //    AfterCodechange();
    

        private void CallCodeVale()
        {
            string BlankYN = "N"; //디폴트 
            string CondTP = "C"; //팝업도움창에 쓸건지 아닌지
            SetData("CODE", "NAME");
        }

        private void SetEvent()
        {
            if (_CodeBox.Text == string.Empty)
            {
                //컨트롤에 값이 없을경우만 지워줌
                if (CodeValue != string.Empty)
                {
                    Clear();
                }
            }
            else
            {
                if (_isResultChk)
                    return;
                if (_OldCodeValue != _CodeBox.Text)
                    CallCodeVale();
            }
        }
        private void AfterCodechange()
        {
            if (!_isResultChk)
            {
                Clear();
                return;
            }

            // Result Event
            if (this.AfterCodeValueChanged != null)
            {
                aControlEventArgs args = new aControlEventArgs(_PopUpReturn, _PopUpParams, this);
                _PopUpReturn.CodeName = CodeName;
                _PopUpReturn.CodeValue = CodeValue;
                _PopUpReturn.PopUpID = PopUpID;
                _PopUpReturn.Row = drRow;

                this.AfterCodeValueChanged(this, args);
            }

            this.CodeChanged?.Invoke(this, null);

            // Focus();

            if (_CodeBox.Focused)
                _CodeBox.Text = CodeValue;
        }

        private void BeforeCodeChange()
        {
            // Result Event
            if (this.BeforeCodeValueChanged != null)
            {
                aControlEventArgs args = new aControlEventArgs(null, base._PopUpParam, this);
                UserPopUpParam = _PopUpParams.UserPopUpParam;
            }
        }

        #region Public Method

        public new void Focus()
        {
            _CodeBox.Focus();
            _CodeBox.Select();

        }

        public new void Select()
        {
            _CodeBox.Focus();
            _CodeBox.Select();

        }

        public void Clear()
        {
            string temp = _CodeValue;

            _CodeValue = string.Empty;
            CodeValue = string.Empty;
            _CodeName = string.Empty;
            CodeName = string.Empty;
            _CodeBox.Text = string.Empty;

            _isResultChk = false;
            

            #region Event
            aControlEventArgs args = new aControlEventArgs(_PopUpReturn, _PopUpParams, this);
            _PopUpReturn.CodeName = _CodeName;
            _PopUpReturn.CodeValue = _CodeValue;
            _PopUpReturn.PopUpID = PopUpID;

            CallNullValue();
            DataTable dt_Temp = new DataTable();
            dt_Temp = dt.Clone();
            DataRow _drRow_blank = dt_Temp.NewRow();
            _PopUpReturn.Row = _drRow_blank;
            ////지웠을때 이벤트 태우려고
            //this.CodeChanged?.Invoke(this, null);
            //this.AfterCodeValueChanged?.Invoke(this, args);
            #endregion
        }

        public void SetCode(string code)
        {
            string temp = _Code;

            _Code = code;

            if (temp != _Code)
            {
                this.CodeChanged?.Invoke(this, null);
            }
        }

        #endregion

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            int X = this.Width;
            int Y = this.Height;

            this.Height = 24;

            if (_CodeBox != null)
            {
                _CodeBox.Location = new System.Drawing.Point(0, 0);
                _CodeBox.Width = X - _Button.Width + 1;
            }

            if (_Button != null)
            {
                _Button.Location = new System.Drawing.Point(X - _Button.Width, 0);
                _Button.BackColor = Color.White;
            }
        }

        public string GetCodeValue()
        {
            return CodeValue;
        }

        public string GetCodeName()
        {
            return CodeName;
        }

        public void SetCodeValue(string codevalue)
        {

            _isResultChk = true;

            _CodeValue = codevalue;
            CodeValue = codevalue;
            _CodeBox.Text = codevalue;

            this.CodeChanged?.Invoke(this, null);

        }

        public void SetCodeName(string codename)
        {

            _isResultChk = true;

            _CodeName = codename;
            CodeName = codename;
            _CodeBox.Text = CodeName;

            this.CodeChanged?.Invoke(this, null);

        }

        public void SetCodeNameNValue(string codevalue, string codename)
        {

            _isResultChk = true;

            CodeValue = codevalue;
            CodeName = codename;
            _CodeBox.Text = CodeName;

            _CodeValue = codevalue;
            _CodeName = codename;

            this.CodeChanged?.Invoke(this, null);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Pen penBorder = new Pen(Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(248)))), ((int)(((byte)(251))))));
            //Rectangle rectBorder = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //e.Graphics.DrawRectangle(penBorder, rectBorder);

            //Rectangle textRec = new Rectangle(e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //TextRenderer.DrawText(e.Graphics, Text, this._CodeBox.Font, textRec, this._CodeBox.ForeColor, this._CodeBox.BackColor, TextFormatFlags.Default);
            this._CodeBox.Properties.AutoHeight = false;

            this._CodeBox.Properties.Appearance.BorderColor = _BorderColor;// System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this._CodeBox.Properties.Appearance.Options.UseBorderColor = true;
            this._CodeBox.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            this._CodeBox.Height = FIXED_HEIGHT;

            if (this.Size.Height != FIXED_HEIGHT)
                this.Size = new Size(this.Size.Width, FIXED_HEIGHT);
        }

    }
}
