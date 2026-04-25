using Bifrost;

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Bifrost.Adv.Controls.PopUp;
using Bifrost.Adv.Controls.aControlHelper;
using System.Drawing;
using DevExpress.XtraEditors;
//using Bifrost.Helper;

namespace Bifrost.Adv.Controls
{
    public partial class aCodeNText : ControlBase
    {
        private const int FIXED_HEIGHT = 24;

        DataTable dt = new DataTable();
        DataRow drRow;

        private bool _IsValidating = false;
        //protected PopUpHelper.PopUpID _PopUpID;

        private PopUpReturn _PopUpReturn = new PopUpReturn();
        private PopUpParam _PopUpParams = new PopUpParam();

        private string _CodeValue = "";
        private string _CodeName = "";
        public string _CustName = "";//거래처코드추가
        private bool _isResultChk = false;
        private bool _isPopUpOpen = false;

        private Color _BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));

        public aCodeNText()
        {
            InitializeComponent();
            InitEvent();

            this.AutoSize = false;
            this._CodeBox.AutoSize = false;
            this._TextBox.AutoSize = false;


            //20170627 디폴트 영문, 대문자 입력 일단 주석

            //_CodeBox.ImeMode = ImeMode.Off;
            //_TextBox.ImeMode = ImeMode.Off;

            this._Button.Image = Images.btn_search_default;
            this._Button.HoverImage = Images.btn_search_HOver;
            this._Button.DisabledImage = Images.btn_search_disable;
            this._Button.ImageAlign = ContentAlignment.MiddleCenter;
            this._Button.HoverColor = Color.White;
            this._Button.BorderColor = _BorderColor;

        }

        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        #region Event
        [Browsable(true)]
        public event EventHandler CodeChanged;

        [Browsable(true)]
        public event EventHandler TextBoxChanged;

        public event aControlEventHandler AfterCodeValueChanged;
        public event aControlEventHandler BeforeCodeValueChanged;

        private void InitEvent()
        {
            _Button.Click += _Button_Click;
            _CodeBox.KeyDown += _CodeBox_KeyDown;
            _CodeBox.Leave += _CodeBox_Leave;

            _CodeBox.DoubleClick += _CodeBox_DoubleClick;
            _TextBox.KeyUp += _TextBox_KeyUp;

            _CodeBox.GotFocus += Control_GotFocus;
            _TextBox.GotFocus += Control_GotFocus;
            BeforeCodeChange();
        }

        private void Control_GotFocus(object sender, EventArgs e)
        {

            ((TextEdit)sender).ImeMode = ImeMode.Alpha;
            ((TextEdit)sender).Properties.CharacterCasing = CharacterCasing.Upper;
        }

        private void _CodeBox_Leave(object sender, EventArgs e)
        {
            SetEvent();
        }

        private void _TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            CodeName = _TextBox.Text;
            this.TextBoxChanged?.Invoke(this, null);
        }

        private void _CodeBox_DoubleClick(object sender, EventArgs e)
        {
            if (this.ReadOnly == ControlEnum.ReadOnly.None || this.ReadOnly == ControlEnum.ReadOnly.TextBoxReadOnly || this.ReadOnly == ControlEnum.ReadOnly.TotalNotReadOnly)
            {
                CallPopUp();
            }
        }

        private void _CodeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetEvent();
            }
        }

        private void _CodeBox_LostFocus(object sender, EventArgs e)
        {
            SetEvent();
        }

        private void _Button_Click(object sender, EventArgs e)
        {
            CallPopUp();
        }


        private void SetEvent()
        {
            if (_CodeBox.Text == string.Empty)
            {
                Clear();
            }
            else
            {
                if (!_isResultChk || _CodeBox.Text != _CodeValue)
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
        #endregion

        #region Property
        [Browsable(false)]
        public string CodeValue
        {
            get { return _CodeBox.Text; }
            set
            {
                _CodeBox.Text = value;
                _CodeValue = value;
                if (_CodeBox.Text == string.Empty)
                    _isResultChk = false;
                else
                    _isResultChk = true;
            }
        }

        [Browsable(false)]
        public string CodeName
        {
            get { return _TextBox.Text; }
            set
            {
                _TextBox.Text = value;
                _CodeName = value;
                if (_CodeBox.Text == string.Empty)
                    _isResultChk = false;
                else
                    _isResultChk = true;
            }
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
            CodeName = codename;

            _CodeName = codename;
            _TextBox.Text = codename;

            this.CodeChanged?.Invoke(this, null);

        }

        public string GetCodeValue()
        {
            return _CodeBox.Text;
        }

        public string GetCodeName()
        {
            return _TextBox.Text;
        }

        [Browsable(true), Description("ReadOnly를 설정합니다.")]
        public ControlEnum.ReadOnly ReadOnly
        {
            get
            {
                return base._ReadOnly;
            }
            set
            {
                base._ReadOnly = value;

                if (base._ReadOnly == aControlHelper.ControlEnum.ReadOnly.TotalNotReadOnly)
                {
                    _CodeBox.BackColor = _ItemBackColor;
                    _TextBox.BackColor = _ItemBackColor;

                    _Button.Enabled = true;
                    _CodeBox.ReadOnly = false;
                    _TextBox.ReadOnly = false;
                    _TextBox.TabStop = true;
                }
                else if (base._ReadOnly == aControlHelper.ControlEnum.ReadOnly.TotalReadOnly)
                {
                    _CodeBox.BackColor = System.Drawing.SystemColors.Control;
                    _TextBox.BackColor = System.Drawing.SystemColors.Control;

                    _Button.Enabled = false;
                    _CodeBox.ReadOnly = true;
                    _TextBox.ReadOnly = true;
                    _TextBox.TabStop = false;
                }
                else if (base._ReadOnly == aControlHelper.ControlEnum.ReadOnly.TextBoxReadOnly)
                {
                    _CodeBox.BackColor = _ItemBackColor;
                    _TextBox.BackColor = System.Drawing.SystemColors.Control;
                    _Button.Enabled = true;
                    _CodeBox.ReadOnly = false;
                    _TextBox.ReadOnly = true;
                    _TextBox.TabStop = false;
                }
                else if (base._ReadOnly == aControlHelper.ControlEnum.ReadOnly.ButtonReadOnly)
                {
                    _CodeBox.BackColor = _ItemBackColor;
                    _TextBox.BackColor = _ItemBackColor;
                    _Button.Enabled = false;
                    _CodeBox.ReadOnly = true;
                    _TextBox.ReadOnly = false;
                    _TextBox.TabStop = false;
                }
                else if (base._ReadOnly == aControlHelper.ControlEnum.ReadOnly.None)
                {
                    base._ReadOnly = aControlHelper.ControlEnum.ReadOnly.TextBoxReadOnly;

                    _CodeBox.BackColor = _ItemBackColor;
                    _TextBox.BackColor = System.Drawing.SystemColors.Control;
                    _Button.Enabled = true;
                    _CodeBox.ReadOnly = false;
                    _TextBox.ReadOnly = true;
                    _TextBox.TabStop = false;

                }
            }
        }
        #endregion

        #region Public Method
        private void CallPopUp()
        {
            string strSearch = string.Empty;
            if (!_isResultChk)
            {
                strSearch = _CodeBox.Text;
            }

            if (!_isPopUpOpen)
            {
                switch (this.PopUpID)
                {
                    //#region 코드도움창
                    //case PopUpHelper.PopUpID.POPUP_CODE:
                    //    POPUP_CODE POPUP_CODE = new POPUP_CODE(_Code, _CodeBox.Text);
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
                    //        if (!_isResultChk)
                    //        {
                    //            Clear();
                    //        }
                    //    }
                    //    break;
                    //#endregion

                    //#region Country 도움창
                    //case PopUpHelper.PopUpID.POPUP_COUNTRY:
                    //    POPUP_COUNTRY POPUP_COUNTRY = new POPUP_COUNTRY(_Code, _CodeBox.Text);
                    //    POPUP_COUNTRY.AutoSearch = true;
                    //    if (!POPUP_COUNTRY.Created)
                    //        if (POPUP_COUNTRY.ShowDialog() == DialogResult.OK)
                    //        {
                    //            drRow = (DataRow)POPUP_COUNTRY.ReturnData["ReturnData"];
                    //            if (drRow != null)
                    //            {
                    //                SetData("CD_COUNTRY", "NM_COUNTRY");
                    //            }
                    //            if (POPUP_COUNTRY != null)
                    //            {
                    //                POPUP_COUNTRY.Dispose();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (!_isResultChk)
                    //            {
                    //                Clear();
                    //            }
                    //        }
                    //    break;
                    //#endregion

                    //#region Location도움창
                    //case PopUpHelper.PopUpID.POPUP_LOCATION:
                    //    POPUP_LOCATION POPUP_LOCATION = new POPUP_LOCATION(_Code, _CodeBox.Text, UserPopUpParam);

                    //    if (!POPUP_LOCATION.Created)
                    //        if (POPUP_LOCATION.ShowDialog() == DialogResult.OK)
                    //        {
                    //            drRow = (DataRow)POPUP_LOCATION.ReturnData["ReturnData"];
                    //            if (drRow != null)
                    //            {
                    //                SetData("CD_LOC", "NM_LOC");
                    //            }
                    //            if (POPUP_LOCATION != null)
                    //            {
                    //                POPUP_LOCATION.Dispose();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (!_isResultChk)
                    //            {
                    //                Clear();
                    //            }
                    //        }
                    //    break;
                    //#endregion

                    //#region 거래처도움창
                    //case PopUpHelper.PopUpID.POPUP_PARTNER:
                    //    POPUP_PARTNER POPUP_PARTNER = new POPUP_PARTNER(strSearch);
                    //    //POPUP_PARTNER.AutoSearch = true;
                    //    if (!POPUP_PARTNER.Created)
                    //        if (POPUP_PARTNER.ShowDialog() == DialogResult.OK)
                    //        {
                    //            drRow = (DataRow)POPUP_PARTNER.ReturnData["ReturnData"];
                    //            if (drRow != null)
                    //            {
                    //                SetData("CD_PARTNER", "NM_PARTNER_ENG");
                    //            }
                    //            if (POPUP_PARTNER != null)
                    //            {
                    //                POPUP_PARTNER.Dispose();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (!_isResultChk)
                    //            {
                    //                Clear();
                    //            }
                    //        }
                    //    break;
                    //#endregion

                    //#region  GL CODE 도움창
                    //case PopUpHelper.PopUpID.POPUP_GL:
                    //    POPUP_GL POPUP_GL = new POPUP_GL(_CodeBox.Text);
                    //    POPUP_GL.AutoSearch = true;
                    //    if (!POPUP_GL.Created)
                    //        if (POPUP_GL.ShowDialog() == DialogResult.OK)
                    //        {
                    //            drRow = (DataRow)POPUP_GL.ReturnData["ReturnData"];
                    //            if (drRow != null)
                    //            {
                    //                SetData("CD_GL", "DC_RMK");
                    //            }
                    //            if (POPUP_GL != null)
                    //            {
                    //                POPUP_GL.Dispose();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (!_isResultChk)
                    //            {
                    //                Clear();
                    //            }
                    //        }
                    //    break;
                    //#endregion

                    //#region BANK 도움창
                    //case PopUpHelper.PopUpID.POPUP_BANK:
                    //    POPUP_BANK POPUP_BANK = new POPUP_BANK(_CodeBox.Text);
                    //    POPUP_BANK.AutoSearch = true;
                    //    if (!POPUP_BANK.Created)
                    //        if (POPUP_BANK.ShowDialog() == DialogResult.OK)
                    //        {
                    //            drRow = (DataRow)POPUP_BANK.ReturnData["ReturnData"];
                    //            if (drRow != null)
                    //            {
                    //                SetData("CD_BANK", "NM_BANK");
                    //            }
                    //            if (POPUP_BANK != null)
                    //            {
                    //                POPUP_BANK.Dispose();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (!_isResultChk)
                    //            {
                    //                Clear();
                    //            }
                    //        }
                    //    break;
                    //#endregion

                    //#region PICKUP ORDER 도움창
                    //case PopUpHelper.PopUpID.POPUP_PICKUP_ORDER:
                    //    POPUP_PICKUP_ORDER POPUP_PICKUP_ORDER = new POPUP_PICKUP_ORDER(_CodeBox.Text);
                    //    POPUP_PICKUP_ORDER.AutoSearch = true;
                    //    if (!POPUP_PICKUP_ORDER.Created)

                    //        if (POPUP_PICKUP_ORDER.ShowDialog() == DialogResult.OK)
                    //        {
                    //            drRow = (DataRow)POPUP_PICKUP_ORDER.ReturnData["ReturnData"];
                    //            if (drRow != null)
                    //            {
                    //                SetData("NO_PICKUP", "NO_PICKUP");
                    //            }
                    //            if (POPUP_PICKUP_ORDER != null)
                    //            {
                    //                POPUP_PICKUP_ORDER.Dispose();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (!_isResultChk)
                    //            {
                    //                Clear();
                    //            }
                    //        }
                    //    break;
                    //#endregion

                    //#region  오피스 도움창
                    //case PopUpHelper.PopUpID.POPUP_BIZ:
                    //    POPUP_BIZ POPUP_BIZ = new POPUP_BIZ(_CodeBox.Text);
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
                    //            Clear();

                    //        }
                    //    }

                    //    break;
                    //#endregion

                    //#region  부서 도움창
                    //case PopUpHelper.PopUpID.POPUP_DEPT:
                    //    POPUP_DEPT POPUP_DEPT = new POPUP_DEPT(_CodeBox.Text);
                    //    POPUP_DEPT.AutoSearch = true;

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
                    //            Clear();

                    //        }
                    //    }

                    //    break;
                    //#endregion

                    //#region 품목 도움창
                    //case PopUpHelper.PopUpID.POPUP_ITEM:
                    //    POPUP_ITEM_S POPUP_ITEM_S = new POPUP_ITEM_S();
                    //    //임시로 Code 속성 사용
                    //    POPUP_ITEM_S.AcctType = _Code;
                    //    POPUP_ITEM_S.ItemName = _TextBox.Text;

                    //    //거래처 조회 추가
                    //    if (_CustName != string.Empty)
                    //        POPUP_ITEM_S.PartnerCode = _CustName;

                    //    if (POPUP_ITEM_S.ItemName != string.Empty || POPUP_ITEM_S.PartnerCode != string.Empty)
                    //        POPUP_ITEM_S.AutoSearch = true;
                    //    else
                    //        POPUP_ITEM_S.AutoSearch = false;
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
                    //    #endregion

                }
                AfterCodechange();
            }
        }

        private void CallCodeVale()
        {
            string BlankYN = "N"; //디폴트 
            string CondTP = "C"; //팝업도움창에 쓸건지 아닌지

            switch (this.PopUpID)
            {
                //#region 공통 Code 도움창
                //case PopUpHelper.PopUpID.POPUP_CODE:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_CODE_S", new object[] { Global.FirmCode, _Code, BlankYN, _CodeBox.Text, CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        CodeValue = dt.Rows[0]["CODE"].ToString().Trim();
                //        CodeName = dt.Rows[0]["NAME"].ToString().Trim();

                //        drRow = dt.Rows[0];
                //        _isResultChk = true;

                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region Country 도움창
                //case PopUpHelper.PopUpID.POPUP_COUNTRY:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_COUNTRY_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        CodeValue = dt.Rows[0]["CD_COUNTRY"].ToString().Trim();
                //        CodeName = dt.Rows[0]["NM_COUNTRY"].ToString().Trim();

                //        drRow = dt.Rows[0];
                //        _isResultChk = true;

                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region Location도움창
                //case PopUpHelper.PopUpID.POPUP_LOCATION:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_LOCATION_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP, CH.SplitString(UserPopUpParam, ";")[0] });

                //    if (dt.Rows.Count == 1)
                //    {
                //        CodeValue = dt.Rows[0]["CD_LOC"].ToString().Trim();
                //        CodeName = dt.Rows[0]["NM_LOC"].ToString().Trim();

                //        drRow = dt.Rows[0];
                //        _isResultChk = true;

                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region 거래처도움창
                //case PopUpHelper.PopUpID.POPUP_PARTNER:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_PARTNER_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        CodeValue = dt.Rows[0]["CD_PARTNER"].ToString().Trim();
                //        CodeName = dt.Rows[0]["NM_PARTNER_ENG"].ToString().Trim();

                //        drRow = dt.Rows[0];
                //        _isResultChk = true;

                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region GL CODE 도움창
                //case PopUpHelper.PopUpID.POPUP_GL:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_GL_S", new object[] { Global.FirmCode, _CodeBox.Text, "", "", "", CondTP });

                //    if (dt.Rows.Count == 1)
                //    {

                //        CodeValue = dt.Rows[0]["CD_GL"].ToString().Trim();
                //        CodeName = dt.Rows[0]["DC_RMK"].ToString().Trim();

                //        drRow = dt.Rows[0];
                //        _isResultChk = true;

                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region BANK 도움창
                //case PopUpHelper.PopUpID.POPUP_BANK:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_BANK_S", new object[] { Global.FirmCode, _CodeBox.Text, "", "", CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        CodeValue = dt.Rows[0]["CD_BANK"].ToString().Trim();
                //        CodeName = dt.Rows[0]["NM_BANK"].ToString().Trim();

                //        drRow = dt.Rows[0];
                //        _isResultChk = true;

                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region PICKUP ORDER 도움창
                //case PopUpHelper.PopUpID.POPUP_PICKUP_ORDER:

                //    dt = DBHelper.GetDataTable("AP_H_INT_PICKUP_S", new object[] { Global.FirmCode, Global.BizCode, _CodeBox.Text, "", "", "", decimal.Zero, decimal.Zero, decimal.Zero, decimal.Zero, CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        CodeValue = dt.Rows[0]["NO_PICKUP"].ToString().Trim();
                //        CodeName = dt.Rows[0]["NO_PICKUP"].ToString().Trim();

                //        drRow = dt.Rows[0];
                //        _CodeBox.Text = CodeName;
                //        _CodeBox.SelectAll();
                //        _isResultChk = true;
                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region 품목 도움창
                //case PopUpHelper.PopUpID.POPUP_ITEM:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_ITEM_S", new object[] { Global.FirmCode, _CodeBox.Text, "", "", "", "", "", "", "", CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        drRow = dt.Rows[0];
                //        SetData("CD_ITEM", "NM_ITEM");
                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region CUST 도움창
                //case PopUpHelper.PopUpID.POPUP_CUST:

                //    dt = DBHelper.GetDataTable("AP_H_MAS_CUST_S", new object[] { Global.FirmCode, "", _CodeBox.Text, CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        drRow = dt.Rows[0];
                //        SetData("CD_CUST", "NM_CUST");
                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region EQUIP 도움창
                //case PopUpHelper.PopUpID.POPUP_EQUIP:

                //    dt = DBHelper.GetDataTable("AP_H_PRD_EQUIP_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        drRow = dt.Rows[0];
                //        SetData("CD_EQUIP", "NM_EQUIP");
                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //#endregion

                //#region OP 도움창
                //case PopUpHelper.PopUpID.POPUP_OP:

                //    dt = DBHelper.GetDataTable("AP_H_PRD_OPERATION_S", new object[] { Global.FirmCode, _CodeBox.Text, CondTP });

                //    if (dt.Rows.Count == 1)
                //    {
                //        drRow = dt.Rows[0];
                //        SetData("CD_OP", "NM_OP");
                //    }
                //    else
                //    {
                //        CallPopUp();
                //    }
                //    break;
                //    #endregion
            }
            AfterCodechange();
        }

        private void SetData(string code, string name)
        {
            CodeValue = drRow[code].ToString().Trim();
            CodeName = drRow[name].ToString().Trim();

            _CodeValue = drRow[code].ToString().Trim();
            _CodeName = drRow[name].ToString().Trim();

            _isResultChk = true;
            _isPopUpOpen = false;

            AfterCodechange();
        }


        public new void Focus()
        {
            _CodeBox.Select();
            _CodeBox.Focus();
        }

        public new void Select()
        {
            _CodeBox.Select();
            _CodeBox.Focus();
        }

        public void Clear()
        {
            _CodeValue = string.Empty;
            CodeValue = string.Empty;
            _CodeName = string.Empty;
            CodeName = string.Empty;

            _CodeBox.Text = string.Empty;
            _TextBox.Text = string.Empty;

            _isResultChk = false;

            #region Event
            aControlEventArgs args = new aControlEventArgs(_PopUpReturn, _PopUpParams, this);
            _PopUpReturn.CodeName = _CodeName;
            _PopUpReturn.CodeValue = _CodeValue;
            _PopUpReturn.PopUpID = PopUpID;
            _PopUpReturn.Row = null;

            this.CodeChanged?.Invoke(this, null);
            this.AfterCodeValueChanged?.Invoke(this, args);
            #endregion
        }

        public void SetCodeNameNValue(string codevalue, string codename)
        {
            _CodeValue = codevalue;
            _CodeName = codename;
            _CodeBox.Text = codevalue;
            _TextBox.Text = codename;

            this.CodeChanged?.Invoke(this, null);
        }

        #endregion

        #region control 길이
        protected override void OnLayout(LayoutEventArgs e)
        {
            this.AutoSize = false;

            this._CodeBox.AutoSize = false;
            this._TextBox.AutoSize = false;


            int X = this.Width;
            int Y = FIXED_HEIGHT;// this.Height;

            if (X < 250)
            {
                double size = X * 0.4;

                if (_CodeBox != null)
                {
                    _CodeBox.Location = new System.Drawing.Point(0, 0);
                    _CodeBox.Width = Convert.ToInt32(size);
                }

                if (_Button != null)
                {
                    _Button.Location = new System.Drawing.Point(_CodeBox.Width - 1, 0);
                }

                if (_TextBox != null)
                {
                    _TextBox.Location = new System.Drawing.Point(_CodeBox.Width + _Button.Width - 2, 0);
                    _TextBox.Width = X - (_CodeBox.Width + _Button.Width - 1);
                }
            }
            else
            {
                if (_CodeBox != null)
                {
                    _CodeBox.Location = new System.Drawing.Point(0, 0);
                    _CodeBox.Width = 100;
                }

                if (_Button != null)
                {
                    _Button.Location = new System.Drawing.Point(99, 0);
                }

                if (_TextBox != null)
                {
                    _TextBox.Location = new System.Drawing.Point(122, 0);
                    _TextBox.Width = X - (_CodeBox.Width + _Button.Width - 1);
                }
            }
            base.OnLayout(e);
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Pen penBorder = new Pen(Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(248)))), ((int)(((byte)(251))))));
            //Rectangle rectBorder = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //e.Graphics.DrawRectangle(penBorder, rectBorder);

            //Rectangle textRec = new Rectangle(e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //TextRenderer.DrawText(e.Graphics, Text, this._CodeBox.Font, textRec, this._CodeBox.ForeColor, this._CodeBox.BackColor, TextFormatFlags.Default);



            this._CodeBox.Properties.AutoHeight = false;
            this._CodeBox.Properties.Appearance.BorderColor = _BorderColor;
            this._CodeBox.Properties.Appearance.Options.UseBorderColor = true;
            this._CodeBox.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            this._TextBox.Properties.AutoHeight = false;
            this._TextBox.Properties.Appearance.BorderColor = _BorderColor;
            this._TextBox.Properties.Appearance.Options.UseBorderColor = true;
            this._TextBox.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            this._CodeBox.Height = FIXED_HEIGHT;
            this._TextBox.Height = FIXED_HEIGHT;

            if (this.Size.Height != FIXED_HEIGHT)
                this.Size = new Size(this.Size.Width, FIXED_HEIGHT);

        }
    }
}
