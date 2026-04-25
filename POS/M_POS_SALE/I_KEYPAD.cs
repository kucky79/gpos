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
    partial class M_POS_SALE
    {
        //string _tmpResult = string.Empty; //키패드 숫자를 관리하기위한 변수

        //public string CustomerCode { get; set; } = string.Empty;
        public string PadUnitCode { get; set; } = string.Empty;
        public string PadUnitName { get; set; } = string.Empty;
        public string PadItemDescript { get; set; } = string.Empty;
        public decimal PadQty { get; set; } = 0; //묶음
        public decimal PadQtyUnit { get; set; } = 0; //수량
        public decimal PadItemPrice { get; set; } = 0; //단가

        public KeypadType PadType { get; set; } = KeypadType.Price;


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

            swFavorite.Click += SwFavorite_Click;
        }

        private void SwFavorite_Click(object sender, EventArgs e)
        {
            if (swFavorite.IsOn)
                DBHelper.ExecuteNonQuery("USP_POS_CUST_ITEM_FAVORITE_D", new object[] { POSGlobal.StoreCode, CustomerCode, _itemCode });
            else
                DBHelper.ExecuteNonQuery("USP_POS_CUST_ITEM_FAVORITE_I", new object[] { POSGlobal.StoreCode, CustomerCode, _itemCode, POSGlobal.UserID });
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
                PadItemPrice = A.GetDecimal(txtPadQty.EditValue);
                //PadItemPrice = A.GetDecimal(txtPadQty.EditValue) != 0 ? A.GetDecimal(txtPadQty.EditValue) : PadItemPrice;
            }
            PadType = KeypadType.Price;
            SetResultText();
        }

        private void btnPadCancel_Click(object sender, EventArgs e)
        {
            //navPay.SelectedPage = navPayContents;
            panelKeypadItem.Visible = false;
            IsKeyPadItemModify = false;

            if(ContentsType == ContentsMode.Favorite)
            {
                _dtItemFavorite = SearchItemFavorite(new object[] { POSGlobal.StoreCode, CustomerCode });
                SetContents();
            }
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
            gridViewItem.DeleteRow(gridViewItem.FocusedRowHandle);
            //navPay.SelectedPage = navPayContents;
            panelKeypadItem.Visible = false;
            IsKeyPadItemModify = false;

            SetGridFooter();
            CalcAmt(null);

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
                //btnPadPrice_Click(sender, e);
                //다시 품목 선택 페이지 보여주기 제일 먼저 하고 나머지 계산
                //navPay.SelectedPage = navPayContents;
                panelKeypadItem.Visible = false;

                //상품 수정일경우
                if (IsKeyPadItemModify)
                {
                    if(A.GetDecimal(txtPadQty.EditValue) != 0)
                        _itemPrice = A.GetDecimal(txtPadQty.EditValue);
                    else
                        _itemPrice = PadItemPrice;

                    DataRow[] dr = _dtItemAll.Select("CD_ITEM = '" + _itemCode + "'");
                    _vatType = A.GetString(dr[0]["FG_VAT"]);
                    _vatRate = A.GetDecimal(dr[0]["RT_VAT"]);
                    _Qt = A.GetDecimal(PadQty);
                    _QtUnit = A.GetDecimal(PadQtyUnit);
                    //_itemPrice = A.GetDecimal(PadItemPrice);
                    _itemDescript = A.GetString(PadItemDescript);
                    _unitCode = A.GetString(PadUnitCode);
                    _unitName = A.GetString(PadUnitName);

                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["CD_ITEM"], _itemCode);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["NM_ITEM"], _itemName);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["QT"], _Qt);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["QT_UNIT"], _QtUnit);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["UM"], _itemPrice);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["AM"], _Qt * _QtUnit * _itemPrice);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["DC_ITEM"], _itemDescript);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["CD_UNIT"], _unitCode);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["NM_UNIT"], _unitName);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["FG_VAT"], _vatType);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["AM_VAT"], _Qt * _QtUnit * _itemPrice * _vatRate / 100);
                    gridViewItem.SetRowCellValue(ClickRowHandle, gridViewItem.Columns["AM_NET"], (_Qt * _QtUnit * _itemPrice) - (_Qt * _QtUnit * _itemPrice * _vatRate / 100));

                    gridViewItem.UpdateCurrentRow();
                    gridViewItem.UpdateSummary();
                    txtPayAmt.EditValue = gridViewItem.Columns["AM"].SummaryItem.SummaryValue;
                    txtVatAmt.EditValue = gridViewItem.Columns["AM_VAT"].SummaryItem.SummaryValue;
                    //txtReceiveAmt.EditValue = A.GetDecimal(gridViewItem.Columns["AM"].SummaryItem.SummaryValue) + A.GetDecimal(gridViewItem.Columns["AM_VAT"].SummaryItem.SummaryValue);

                    CalcAmt(null);
                }
                else //신규일경우
                {

                    if (A.GetDecimal(txtPadQty.EditValue) != 0)
                        PadItemPrice = A.GetDecimal(txtPadQty.EditValue);

                    //수량과 묶음이 같이 관리되어서 0이면 무조건 1로 변환
                    PadQty = PadQty == 0 ? 1 : PadQty;
                    PadQtyUnit = PadQtyUnit == 0 ? 1 : PadQtyUnit; 

                    SetResultText();

                    DataRow[] dr = _dtItemAll.Select("CD_ITEM = '" + _itemCode + "'");

                    if (dr.Length > 0)
                    {
                        _vatType = A.GetString(dr[0]["FG_VAT"]);
                        _vatRate = A.GetDecimal(dr[0]["RT_VAT"]);
                        _Qt = A.GetDecimal(PadQty);
                        _QtUnit = A.GetDecimal(PadQtyUnit);
                        _itemPrice = A.GetDecimal(PadItemPrice);
                        _itemDescript = A.GetString(PadItemDescript);
                        _unitCode = A.GetString(PadUnitCode);
                        _unitName = A.GetString(PadUnitName);
                        _TmpLastFlag = "NEW";

                        gridViewItem.AddNewRow();
                        gridViewItem.BestFitColumns();
                        gridViewItem.UpdateCurrentRow();
                        gridViewItem.UpdateSummary();

                        SetGridFooter();
                        //전표유형 정상으로 바꿈
                        //20191211 왜 다시 정상으로 바꾸었을까? 바꿀 이유가 없는데.. 전표 유형 주석처리함
                        //SlipFlag = "A";
                        CalcAmt(null);
                    }
                }
                

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

            if (!IsKeyPadItemModify)
            {
                if (decimal.Parse(txtPadQty.Text) == 0)
                {
                    ShowMessageBoxA("수량을 먼저 입력하세요", Bifrost.Common.MessageType.Warning);
                    return;
                }
            }

            PadType = KeypadType.Unit;

            string OldUnitCode = PadUnitCode;
            PadQtyUnit = A.GetDecimal(_tmpResult);
            PadUnitName = ((DevExpress.XtraEditors.SimpleButton)sender).Text;
            PadUnitCode = A.GetString(((DevExpress.XtraEditors.SimpleButton)sender).Tag);

            //단가는 신규모드일때 혹은 단위가 바뀔때적용
            if (!IsKeyPadItemModify || OldUnitCode != PadUnitCode)
            {
                //거래처 최종 단가
                PadItemPrice = GetItemPrice(PadUnitCode);
                txtPadQty.EditValue = PadItemPrice;
            }
            SetResultText();
        }

        private void btnPadEA_Click(object sender, EventArgs e)
        {
            PadType = KeypadType.Bundle;
            PadQty = A.GetDecimal(_tmpResult);

            SetResultText();
        }

        private void GetItemUnit()
        {
            string FavoriteYN = DBHelper.ExecuteScalar("USP_POS_ITEM_FAVORITE_S", new object[] { POSGlobal.StoreCode, _itemCode, CustomerCode }) as string;
            DataTable dtUnit = DBHelper.GetDataTable("USP_POS_ITEM_UNIT_S", new object[] { POSGlobal.StoreCode, _itemCode });

            if(FavoriteYN == "Y")
                swFavorite.IsOn = true;
            else
                swFavorite.IsOn = false;

            //무조건 4개 채워야함
            for (int i = 0; i < 4; i++)
            {
                Control[] findCtl = Controls.Find("btnPadItemUnit" + (i + 1).ToString(), true);
                
                if (i < dtUnit.Rows.Count)
                {
                    findCtl[0].Text = dtUnit.Rows[i]["NM_UNIT"].ToString();
                    findCtl[0].Tag = dtUnit.Rows[i]["CD_UNIT"].ToString();
                    findCtl[0].Enabled = true;
                }
                else
                {
                    findCtl[0].Text = string.Empty;
                    findCtl[0].Tag = string.Empty;
                    findCtl[0].Enabled = false; ;
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
            return A.GetDecimal(DBHelper.ExecuteScalar("USP_GET_POS_ITEM_UNIT_PRICE_LAST", new object[] { POSGlobal.StoreCode, CustomerCode, _itemCode, unitCode }));
        }

        public enum Mode
        {
            New,
            Modify
        }

        public enum KeypadType
        {
            Unit,
            Bundle,
            Price
        }

    }
}
