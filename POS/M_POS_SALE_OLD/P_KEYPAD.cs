using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace POS
{
    public partial class P_KEYPAD : POSPopupBase
    {

        string _tmpResult = string.Empty; //키패드 숫자를 관리하기위한 변수

        public string KeypadItemCode { get; set; } = string.Empty;
        public string KeypadItemName { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public string ItemDescript { get; set; } = string.Empty;
        public decimal Qty { get; set; } = 0; //묶음
        public decimal QtyUnit { get; set; } = 0; //수량
        public decimal ItemPrice { get; set; } = 0; //단가
        public bool isDel { get; set; } = false;

        public Mode KeyMode { get; set; } = Mode.New;


        public P_KEYPAD()
        {
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);

            InitializeComponent();
            InitEvent();

            PopupTitle = "상품도움창";
        }

        private void InitEvent()
        {
            #region Keypad NUM
            btnPad000.Click += btnPadAlphabet_Click;
            btnPad00.Click += btnPadAlphabet_Click;
            btnPad0.Click += btnPadAlphabet_Click;
            btnPad1.Click += btnPadAlphabet_Click;
            btnPad2.Click += btnPadAlphabet_Click;
            btnPad3.Click += btnPadAlphabet_Click;
            btnPad4.Click += btnPadAlphabet_Click;
            btnPad5.Click += btnPadAlphabet_Click;
            btnPad6.Click += btnPadAlphabet_Click;
            btnPad7.Click += btnPadAlphabet_Click;
            btnPad8.Click += btnPadAlphabet_Click;
            btnPad9.Click += btnPadAlphabet_Click;
            #endregion

            btnPadPoint.Click += btnPadPoint_Click;
            btnPadClear.Click += btnPadClear_Click;
            btnPadMinus.Click += btnPadMinus_Click;
            btnPadBackSpace.Click += btnPadBackSpace_Click;
            btnPadClose.Click += btnPadClose_Click;
            btnPadDelItem.Click += btnPadDelItem_Click;
            btnPadPrice.Click += btnPadPrice_Click;
            btnPadInit.Click += btnPadInit_Click;

            btnPadEA.Click += btnPadEA_Click;
            btnPadItemUnit1.Click += btnPadItemUnit_Click;
            btnPadItemUnit2.Click += btnPadItemUnit_Click;
            btnPadItemUnit3.Click += btnPadItemUnit_Click;
            btnPadItemUnit4.Click += btnPadItemUnit_Click;
            btnPadConfirm.Click += btnPadConfirm_Click;
            btnPadCancel.Click += btnPadCancel_Click;

            Load += P_KEYPAD_Load;
        }

        private void btnPadClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnPadBackSpace_Click(object sender, EventArgs e)
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

        private void btnPadPoint_Click(object sender, EventArgs e)
        {
            if (!A.GetDecimal(_tmpResult).ToString().Contains("."))
            {
                _tmpResult += ((DevExpress.XtraEditors.SimpleButton)sender).Text;
                txtQty.Text = _tmpResult;
            }
        }

        private void btnPadClear_Click(object sender, EventArgs e)
        {
            _tmpResult = string.Empty;
            txtQty.EditValue = 0;

            //Qty = 0;
            //QtyUnit = 0;
            //ItemPrice = 0;

            //UnitCode = string.Empty;
            //UnitName = string.Empty;
            //ItemDescript = string.Empty;

            //txtItemDescrip.Text = string.Empty;
        }

        private void btnPadAlphabet_Click(object sender, EventArgs e)
        {
            _tmpResult += ((DevExpress.XtraEditors.SimpleButton)sender).Text;
            txtQty.Text = _tmpResult;
        }

        private void btnPadInit_Click(object sender, EventArgs e)
        {
            _tmpResult = string.Empty;
            txtQty.EditValue = 0;

            Qty = 0;
            QtyUnit = 0;
            ItemPrice = 0;

            UnitCode = string.Empty;
            UnitName = string.Empty;
            ItemDescript = string.Empty;

            txtItemDescrip.Text = string.Empty;
        }

        private void btnPadPrice_Click(object sender, EventArgs e)
        {
            ItemPrice = A.GetDecimal(txtQty.EditValue);
            SetResultText();
        }

        private void btnPadCancel_Click(object sender, EventArgs e)
        {
            //this.Hide();

            this.DialogResult = DialogResult.Cancel;
        }

        private void btnPadMinus_Click(object sender, EventArgs e)
        {
            if (A.GetDecimal(txtQty.EditValue) == 0)
            {
                txtQty.Text = "-";
                _tmpResult = "-";
            }
            else if (A.GetDecimal(txtQty.EditValue) > 0)
            {
                txtQty.EditValue = A.GetDecimal(txtQty.EditValue) * (-1);
                _tmpResult = txtQty.Text;
            }
        }

        private void btnPadDelItem_Click(object sender, EventArgs e)
        {
            isDel = true;
            this.DialogResult = DialogResult.OK;
        }

        private void btnPadConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //decimal resultAmt = 0;
                decimal resultPrice = A.GetDecimal(_tmpResult);

                //if(KeyMode == Mode.New)
                //{
                //    ItemPrice = resultPrice;
                //}
                //else if (KeyMode == Mode.Modify)
                {
                    if(resultPrice == 0) //단가수정 없는경우
                    {
                        ItemPrice = ItemPrice;
                    }
                    else
                    {
                        ItemPrice = resultPrice;
                    }
                }

                //if (ItemPrice == 0)
                //{
                //    ShowMessageBoxA("단가는 0원일 수 없습니다.", Bifrost.Common.MessageType.Warning);
                //    return;
                //}

                if(UnitName == string.Empty)
                {
                    ShowMessageBoxA("단위는 필수 입니다.", Bifrost.Common.MessageType.Warning);
                    return;
                }

                //수량과 묶음이 같이 관리되어서 0이면 무조건 1로 변환
                Qty = Qty == 0 ? 1 : Qty;
                QtyUnit = QtyUnit == 0 ? 1 : QtyUnit;

                //resultAmt = Qty * QtyUnit * A.GetDecimal(_tmpResult);

                SetResultText();

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnPadItemUnit_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtQty.Text) == 0)
            {
                ShowMessageBoxA("수량을 먼저 입력하세요", Bifrost.Common.MessageType.Warning);
            }
            else
            {
                QtyUnit = decimal.Parse(_tmpResult);
                UnitName = ((DevExpress.XtraEditors.SimpleButton)sender).Text;
                UnitCode = A.GetString(((DevExpress.XtraEditors.SimpleButton)sender).Tag);

                //거래처 최종 단가
                ItemPrice = GetItemPrice(UnitCode);
                SetResultText();
            }
        }

        private void btnPadEA_Click(object sender, EventArgs e)
        {
            Qty = decimal.Parse(_tmpResult);
       
            SetResultText();
        }

        private void P_KEYPAD_Load(object sender, EventArgs e)
        {
            
            btnPadDelItem.Visible = false; //기본은 삭제 버튼 안보임

            txtItem.Text = KeypadItemName;
            txtItemDescrip.Text = ItemDescript;
            SetResultText();
            //txtQty.Text = A.GetString(ItemPrice);
            GetItemUnit();

            if(KeyMode == Mode.Modify)
            {
                btnPadDelItem.Visible = true;
            }
            else
            {
                txtItem.Size = new System.Drawing.Size(298, 39);
            }
        }

        private void GetItemUnit()
        {
            DataTable dtUnit = DBHelper.GetDataTable("USP_POS_ITEM_UNIT_S", new object[] { POSGlobal.StoreCode, KeypadItemCode });

            if(dtUnit.Rows.Count > 0)
            {
                switch (dtUnit.Rows.Count)
                {
                    case 1:
                        btnPadItemUnit1.Text = dtUnit.Rows[0]["NM_UNIT"].ToString();
                        btnPadItemUnit1.Tag = dtUnit.Rows[0]["CD_UNIT"].ToString();
                        btnPadItemUnit1.Enabled = true;
                        break;
                    case 2:
                        btnPadItemUnit1.Text = dtUnit.Rows[0]["NM_UNIT"].ToString();
                        btnPadItemUnit1.Tag = dtUnit.Rows[0]["CD_UNIT"].ToString();
                        btnPadItemUnit1.Enabled = true;
                        btnPadItemUnit2.Text = dtUnit.Rows[1]["NM_UNIT"].ToString();
                        btnPadItemUnit2.Tag = dtUnit.Rows[1]["CD_UNIT"].ToString();
                        btnPadItemUnit2.Enabled = true;
                        break;
                    case 3:
                        btnPadItemUnit1.Text = dtUnit.Rows[0]["NM_UNIT"].ToString();
                        btnPadItemUnit1.Tag = dtUnit.Rows[0]["CD_UNIT"].ToString();
                        btnPadItemUnit1.Enabled = true;
                        btnPadItemUnit2.Text = dtUnit.Rows[1]["NM_UNIT"].ToString();
                        btnPadItemUnit2.Tag = dtUnit.Rows[1]["CD_UNIT"].ToString();
                        btnPadItemUnit2.Enabled = true;
                        btnPadItemUnit3.Text = dtUnit.Rows[2]["NM_UNIT"].ToString();
                        btnPadItemUnit3.Tag = dtUnit.Rows[2]["CD_UNIT"].ToString();
                        btnPadItemUnit3.Enabled = true;
                        break;
                    case 4:
                        btnPadItemUnit1.Text = dtUnit.Rows[0]["NM_UNIT"].ToString();
                        btnPadItemUnit1.Tag = dtUnit.Rows[0]["CD_UNIT"].ToString();
                        btnPadItemUnit1.Enabled = true;
                        btnPadItemUnit2.Text = dtUnit.Rows[1]["NM_UNIT"].ToString();
                        btnPadItemUnit2.Tag = dtUnit.Rows[1]["CD_UNIT"].ToString();
                        btnPadItemUnit2.Enabled = true;
                        btnPadItemUnit3.Text = dtUnit.Rows[2]["NM_UNIT"].ToString();
                        btnPadItemUnit3.Tag = dtUnit.Rows[2]["CD_UNIT"].ToString();
                        btnPadItemUnit3.Enabled = true;
                        btnPadItemUnit4.Text = dtUnit.Rows[3]["NM_UNIT"].ToString();
                        btnPadItemUnit4.Tag = dtUnit.Rows[3]["CD_UNIT"].ToString();
                        btnPadItemUnit4.Enabled = true;

                        break;
                }
            }


            //for (int i = 0; i < dtUnit.Rows.Count; i++)
            //{

            //    Control[] findCtl = this.Controls.Find("btnPadItemUnit" + (i + 1).ToString(), true);

            //    if (findCtl != null && findCtl.Length > 0)
            //    {
            //        findCtl[0].Text = dtUnit.Rows[i]["NM_UNIT"].ToString();
            //        findCtl[0].Tag = dtUnit.Rows[i]["CD_UNIT"].ToString();
            //        findCtl[0].Enabled = true;
            //    }
            //}
        }

        private void SetResultText()
        {
            string tmpStr = string.Empty;

            string strQty = A.GetString(Qty.ToString("##,##0.####"));
            string strItemPrice = A.GetString(ItemPrice.ToString("##,##0.####"));
            string strQtyUnit = A.GetString(QtyUnit.ToString("##,##0.####"));


            if (Qty != 0 && QtyUnit == 0)
            {
                txtItemDescrip.Text = strQty + " 단가 " + strItemPrice + "원";
                ItemDescript = strQty;
            }
            else if (Qty != 0 && QtyUnit != 0)
            {
                tmpStr = strQty == "1" ? strQtyUnit + UnitName : strQtyUnit + UnitName + " * " + strQty;
                txtItemDescrip.Text = tmpStr + " 단가 " + strItemPrice + "원";
                ItemDescript = tmpStr;
            }
            else if (Qty == 0 && QtyUnit != 0)
            {
                txtItemDescrip.Text = strQtyUnit + UnitName + " 단가 " + strItemPrice + "원";
                ItemDescript = strQtyUnit + UnitName;
            }

            //초기화
            _tmpResult = string.Empty;
            txtQty.EditValue = 0;
        }

        private decimal GetItemPrice(string unitCode)
        {
            return A.GetDecimal(DBHelper.ExecuteScalar("USP_GET_POS_ITEM_UNIT_PRICE_LAST", new object[] { POSGlobal.StoreCode, CustomerCode, KeypadItemCode, unitCode }));
        }

        public enum Mode
        {
            New,
            Modify
        }

    }
}
