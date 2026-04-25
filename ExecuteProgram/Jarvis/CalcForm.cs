using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ether
{
    public partial class CalcForm : MDIBase
    {

        string _tmpResult = string.Empty; //키패드 숫자를 관리하기위한 변수

        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;

        public decimal _1stQty = 0;

        public decimal _2ndQty = 0;

        public decimal _ItemPrice = 0;

        public string _1stStr = string.Empty;

        public string _2ndStr = string.Empty;

        

        public CalcForm()
        {
            InitializeComponent();
            InitEvent();
        }

        private void InitEvent()
        {
            #region Keypad NUM
            btn000.Click += BtnAlphabet_Click;
            btn00.Click += BtnAlphabet_Click;
            btn0.Click += BtnAlphabet_Click;
            btn1.Click += BtnAlphabet_Click;
            btn2.Click += BtnAlphabet_Click;
            btn3.Click += BtnAlphabet_Click;
            btn4.Click += BtnAlphabet_Click;
            btn5.Click += BtnAlphabet_Click;
            btn6.Click += BtnAlphabet_Click;
            btn7.Click += BtnAlphabet_Click;
            btn8.Click += BtnAlphabet_Click;
            btn9.Click += BtnAlphabet_Click;
            #endregion

            btnPoint.Click += BtnPoint_Click;
            btnClear.Click += BtnClear_Click;

            btnBackSpace.Click += BtnBackSpace_Click;
            btnClose.Click += BtnClose_Click;

            btnEA.Click += BtnEA_Click;
            btnItemUnit1.Click += BtnItemUnit_Click;
            btnItemUnit2.Click += BtnItemUnit_Click;
            btnItemUnit3.Click += BtnItemUnit_Click;
            btnItemUnit4.Click += BtnItemUnit_Click;
            btnConfirm.Click += BtnConfirm_Click;


            Load += CalcForm_Load;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {

                decimal resualAmt = 0;

                if (_1stQty != 0 && _2ndQty != 0)
                {
                    resualAmt = _1stQty * _2ndQty * A.GetDecimal(_tmpResult);
                    _ItemPrice = A.GetDecimal(_tmpResult);

                    SetResultText();
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetResultText()
        {
            if(_1stQty != 0 && _2ndQty == 0)
            {
                txtItemInfo.Text = _1stQty.ToString() + " * " + _ItemPrice.ToString() + "원";
            }
            else if(_1stQty != 0 && _2ndQty != 0)
            {
                txtItemInfo.Text = _1stQty.ToString() + " * " + _2ndQty.ToString() + _2ndStr + " * " + _ItemPrice.ToString() + "원";
            }
            else if(_1stQty == 0 && _2ndQty != 0)
            {
                txtItemInfo.Text = _2ndQty.ToString() + _2ndStr + " * " + _ItemPrice.ToString() + "원";
            }

            //초기화
            _tmpResult = string.Empty;
            txtQty.EditValue = 0;
        }

        private void BtnItemUnit_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtQty.Text) == 0)
            {
                ShowMessageBoxA("수량을 먼저 입력하세요", Bifrost.Common.MessageType.Warning);
            }
            else
            {
                _2ndQty = decimal.Parse(_tmpResult);
                _2ndStr = ((DevExpress.XtraEditors.SimpleButton)sender).Text;

                SetResultText();
            }
        }

        private void BtnEA_Click(object sender, EventArgs e)
        {
            _1stQty = decimal.Parse(_tmpResult);

       
            SetResultText();
        }

        private void CalcForm_Load(object sender, EventArgs e)
        {
            txtItem.Text = ItemName;
            GetItemUnit();
        }

        private void GetItemUnit()
        {
            DataTable dtUnit = DBHelper.GetDataTable("USP_POS_ITEM_UNIT_S", new object[] { "SE0001", ItemCode });

            for (int i = 0; i < dtUnit.Rows.Count; i++)
            {

                Control[] findCtl = Controls.Find("btnItemUnit" + (i + 1).ToString(), true);

                if (findCtl != null && findCtl.Length > 0)
                {
                    findCtl[0].Text = dtUnit.Rows[i]["NM_UNIT"].ToString();
                    findCtl[0].Tag = dtUnit.Rows[i]["CD_UNIT"].ToString();
                }
            }
        }

        #region 버튼이벤트 
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            if (A.GetDecimal(_tmpResult) != 0)
            {
                //마지막이 . 일땐 .까지 같이 지우기
                if (_tmpResult[_tmpResult.Length - 1].ToString() == ".")
                    _tmpResult = _tmpResult.Substring(0, _tmpResult.Length - 2);
                else
                    _tmpResult = _tmpResult.Substring(0, _tmpResult.Length - 1);

                txtQty.Text = _tmpResult;
            }
        }

        private void BtnPoint_Click(object sender, EventArgs e)
        {
            if (!A.GetDecimal(_tmpResult).ToString().Contains("."))
            {
                _tmpResult += ((DevExpress.XtraEditors.SimpleButton)sender).Text;
                txtQty.Text = _tmpResult;
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            _tmpResult = string.Empty;
            txtQty.EditValue = 0;

            _1stQty = 0;
            _2ndQty = 0;
            _1stStr = string.Empty;
            _2ndStr = string.Empty;
            _ItemPrice = 0;
            txtItemInfo.Text = string.Empty;
        }

        private void BtnAlphabet_Click(object sender, EventArgs e)
        {
            _tmpResult += ((DevExpress.XtraEditors.SimpleButton)sender).Text;
            txtQty.Text = _tmpResult;
        }
        #endregion
    }
}
