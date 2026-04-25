using Bifrost;
using Bifrost.Helper;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    partial class M_POS_INV_OPEN
    {
        //string _tmpResult = string.Empty; //키패드 숫자를 관리하기위한 변수

        //public string CustomerCode { get; set; } = string.Empty;
        public string PadUnitCode { get; set; } = string.Empty;
        public string PadUnitName { get; set; } = string.Empty;
        public string PadItemDescript { get; set; } = string.Empty;
        public decimal PadQty { get; set; } = 0; //묶음
        public decimal PadQtyUnit { get; set; } = 0; //수량
        public decimal PadItemPrice { get; set; } = 0; //단가

        private void InitKeypadEvent()
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

                txtPadQty.Text = _tmpResult;
            }
        }

        private void btnPadPoint_Click(object sender, EventArgs e)
        {
            if (!A.GetDecimal(_tmpResult).ToString().Contains("."))
            {
                _tmpResult += ((DevExpress.XtraEditors.SimpleButton)sender).Text;
                txtPadQty.Text = _tmpResult;
            }
        }

        private void btnPadClear_Click(object sender, EventArgs e)
        {
            _tmpResult = string.Empty;
            txtPadQty.EditValue = 0;

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
            txtPadQty.Text = _tmpResult;
        }

        private void btnPadInit_Click(object sender, EventArgs e)
        {
            _tmpResult = string.Empty;
            txtPadQty.EditValue = 0;

            PadQty = 0;
            PadQtyUnit = 0;
            PadItemPrice = 0;

            PadUnitCode = string.Empty;
            PadUnitName = string.Empty;
            PadItemDescript = string.Empty;

            txtItemDescrip.Text = string.Empty;
        }

        private void btnPadPrice_Click(object sender, EventArgs e)
        {
            //if(PadItemPrice != 0 && A.GetDecimal(txtPadQty.EditValue) == 0)
            //{
            //    PadItemPrice = PadItemPrice;
            //}
            //else
            //{
            //    PadItemPrice = A.GetDecimal(txtPadQty.EditValue);
            //}
            if (IsKeyPadItemModify)
            {
                if (((SimpleButton)sender).Name == nameof(btnPadPrice))
                {
                    PadItemPrice = A.GetDecimal(txtPadQty.EditValue);// == 0 ? PadItemPrice : A.GetDecimal(txtPadQty.EditValue);
                }
                else
                {
                    PadItemPrice = A.GetDecimal(txtPadQty.EditValue) == 0 ? PadItemPrice : A.GetDecimal(txtPadQty.EditValue);
                }
            }
            else
            {
                PadItemPrice = A.GetDecimal(txtPadQty.EditValue) != 0 ? A.GetDecimal(txtPadQty.EditValue) : PadItemPrice;
            }
            SetResultText();
        }

        private void btnPadCancel_Click(object sender, EventArgs e)
        {
            //navPay.SelectedPage = navPayContents;
            panelContents.Visible = true;
            IsKeyPadItemModify = false;

        }

        private void btnPadMinus_Click(object sender, EventArgs e)
        {
            if (A.GetDecimal(txtPadQty.EditValue) == 0)
            {
                txtPadQty.Text = "-";
                _tmpResult = "-";
            }
            else if (A.GetDecimal(txtPadQty.EditValue) > 0)
            {
                txtPadQty.EditValue = A.GetDecimal(txtPadQty.EditValue) * (-1);
                _tmpResult = txtPadQty.Text;
            }
        }

        private void btnPadDelItem_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
            //navPay.SelectedPage = navPayContents;
            panelContents.Visible = true;
            IsKeyPadItemModify = false;

        }

        private void btnPadConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (PadUnitName == string.Empty)
                {
                    ShowMessageBoxA("단위는 필수 입니다.", Bifrost.Common.MessageType.Warning);
                    return;
                }

                //강제 원 클릭
                btnPadPrice_Click(sender, e);
                //다시 품목 선택 페이지 보여주기 제일 먼저 하고 나머지 계산
                //navPay.SelectedPage = navPayContents;
                panelContents.Visible = true;

                //상품 수정일경우
                if (IsKeyPadItemModify)
                {
                    DataRow[] dr = _dtItemAll.Select("CD_ITEM = '" + _itemCode + "'");
                    _vatType = A.GetString(dr[0]["FG_VAT"]);
                    _vatRate = A.GetDecimal(dr[0]["RT_VAT"]);
                    _Qt = A.GetDecimal(PadQty);
                    _QtUnit = A.GetDecimal(PadQtyUnit);
                    _itemPrice = A.GetDecimal(PadItemPrice);
                    _itemDescript = A.GetString(PadItemDescript);
                    _unitCode = A.GetString(PadUnitCode);
                    _unitName = A.GetString(PadUnitName);

                    gridView1.SetRowCellValue(ClickRowHandle, gridView1.Columns["CD_ITEM"], _itemCode);
                    gridView1.SetRowCellValue(ClickRowHandle, gridView1.Columns["NM_ITEM"], _itemName);
                    gridView1.SetRowCellValue(ClickRowHandle, gridView1.Columns["QT_OPEN"], _QtUnit);
                    gridView1.SetRowCellValue(ClickRowHandle, gridView1.Columns["UM_OPEN"], _itemPrice);
                    gridView1.SetRowCellValue(ClickRowHandle, gridView1.Columns["AM_OPEN"], _QtUnit * _itemPrice);
                    gridView1.SetRowCellValue(ClickRowHandle, gridView1.Columns["CD_UNIT"], _unitCode);
                    gridView1.SetRowCellValue(ClickRowHandle, gridView1.Columns["NM_UNIT"], _unitName);

                    gridView1.UpdateCurrentRow();
                    gridView1.UpdateSummary();
                }
                else //신규일경우
                {
                    decimal resultPrice = A.GetDecimal(_tmpResult);
                    if (resultPrice == 0) //단가수정 없는경우
                    {
                        PadItemPrice = PadItemPrice;
                    }
                    else
                    {
                        PadItemPrice = resultPrice;
                    }
                    

                    //수량과 묶음이 같이 관리되어서 0이면 무조건 1로 변환
                    PadQty = PadQty == 0 ? 1 : PadQty;
                    PadQtyUnit = PadQtyUnit == 0 ? 1 : PadQtyUnit; 

                    SetResultText();

                    DataRow[] dr = _dtItem.Select("CD_ITEM = '" + _itemCode + "'");

                    _vatType = A.GetString(dr[0]["FG_VAT"]);
                    _vatRate = A.GetDecimal(dr[0]["RT_VAT"]);
                    _Qt = A.GetDecimal(PadQty);
                    _QtUnit = A.GetDecimal(PadQtyUnit);
                    _itemPrice = A.GetDecimal(PadItemPrice);
                    _itemDescript = A.GetString(PadItemDescript);
                    _unitCode = A.GetString(PadUnitCode);
                    _unitName = A.GetString(PadUnitName);

                    gridView1.AddNewRow();
                    gridView1.BestFitColumns();
                    gridView1.UpdateCurrentRow();
                    gridView1.UpdateSummary();

                }

                btnPadInit_Click(null, null);

                IsKeyPadItemModify = false;
                
                if(!IsItemTypeFix)
                    InitItemTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnPadItemUnit_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtPadQty.Text) == 0)
            {
                ShowMessageBoxA("수량을 먼저 입력하세요", Bifrost.Common.MessageType.Warning);
            }
            else
            {
                PadQtyUnit = decimal.Parse(_tmpResult);
                PadUnitName = ((DevExpress.XtraEditors.SimpleButton)sender).Text;
                PadUnitCode = A.GetString(((DevExpress.XtraEditors.SimpleButton)sender).Tag);

                //거래처 최종 단가
                PadItemPrice = GetItemPrice(PadUnitCode);
                SetResultText();
            }
        }

        private void btnPadEA_Click(object sender, EventArgs e)
        {
            PadQty = decimal.Parse(_tmpResult);

            SetResultText();
        }

        private void GetItemUnit()
        {
            DataTable dtUnit = DBHelper.GetDataTable("USP_POS_ITEM_UNIT_S", new object[] { POSGlobal.StoreCode, _itemCode });

            for (int i = 0; i < dtUnit.Rows.Count; i++)
            {
                Control[] findCtl = Controls.Find("btnPadItemUnit" + (i + 1).ToString(), true);
                
                if (findCtl != null && findCtl.Length > 0)
                {
                    findCtl[0].Text = dtUnit.Rows[i]["NM_UNIT"].ToString();
                    findCtl[0].Tag = dtUnit.Rows[i]["CD_UNIT"].ToString();
                    findCtl[0].Enabled = true;
                }
            }
        }

        private void SetResultText()
        {
            txtPadItem.Text = _itemName;

            string tmpStr = string.Empty;

            string strQty = A.GetString(PadQty.ToString("##,##0.####"));
            string strItemPrice = A.GetString(PadItemPrice.ToString("##,##0.####"));
            string strQtyUnit = A.GetString(PadQtyUnit.ToString("##,##0.####"));


            if (PadQty == 0 && PadQtyUnit == 0)
            {
                txtItemDescrip.Text = string.Empty;
                PadItemDescript = string.Empty;
            }
            else if (PadQty != 0 && PadQtyUnit == 0)
            {
                txtItemDescrip.Text = strQty + " 단가 " + strItemPrice + "원";
                PadItemDescript = strQty;
            }
            else if (PadQty != 0 && PadQtyUnit != 0)
            {
                tmpStr = strQty == "1" ? strQtyUnit + PadUnitName : strQtyUnit + PadUnitName + " * " + strQty;
                txtItemDescrip.Text = tmpStr + " 단가 " + strItemPrice + "원";
                PadItemDescript = tmpStr;
            }
            else if (PadQty == 0 && PadQtyUnit != 0)
            {
                txtItemDescrip.Text = strQtyUnit + PadUnitName + " 단가 " + strItemPrice + "원";
                PadItemDescript = strQtyUnit + PadUnitName;
            }

            //초기화
            _tmpResult = string.Empty;
            txtPadQty.EditValue = 0;
        }

        private decimal GetItemPrice(string unitCode)
        {
            return A.GetDecimal(DBHelper.ExecuteScalar("USP_GET_POS_ITEM_UNIT_PRICE_LAST", new object[] { POSGlobal.StoreCode, string.Empty, _itemCode, unitCode }));
        }

        public enum Mode
        {
            New,
            Modify
        }

    }
}
