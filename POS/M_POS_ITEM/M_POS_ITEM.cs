using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_ITEM : POSFormBase
    {
        DataRow drRow;

        public M_POS_ITEM()
        {
            InitializeComponent();
            InitializeControl();
            InitEvent();

        }

        private void InitEvent()
        {
            gridView1.InitNewRow += GridView1_InitNewRow;


            btnItemFlag.Click += BtnCode_Click;
            btnItemTypeL.Click += BtnCode_Click;
            btnItemTypeM.Click += BtnCode_Click;
            btnItemTypeS.Click += BtnCode_Click;
            btnVat.Click += BtnCode_Click;
            btnUnit1.Click += BtnCode_Click;
            btnUnit2.Click += BtnCode_Click;
            btnUnit3.Click += BtnCode_Click;
            btnUnit4.Click += BtnCode_Click;

            txtItemType.TextChanged += CodeTextChanged;
            txtItemTypeL.TextChanged += CodeTextChanged;
            txtItemTypeM.TextChanged += CodeTextChanged;
            txtItemTypeS.TextChanged += CodeTextChanged;
            txtVatType.TextChanged += CodeTextChanged;
            txtItemUnit1.TextChanged += CodeTextChanged;
            txtItemUnit2.TextChanged += CodeTextChanged;
            txtItemUnit3.TextChanged += CodeTextChanged;
            txtItemUnit4.TextChanged += CodeTextChanged;

            this.Shown += M_POS_ITEM_Shown;

            txtUnitQt1.Enter += TxtUnitQt_Enter;
            txtUnitQt2.Enter += TxtUnitQt_Enter;
            txtUnitQt3.Enter += TxtUnitQt_Enter;
            txtUnitQt4.Enter += TxtUnitQt_Enter;


            btnItemUnitDel1.Click += BtnItemUnitDel_Click;
            btnItemUnitDel2.Click += BtnItemUnitDel_Click;
            btnItemUnitDel3.Click += BtnItemUnitDel_Click;
            btnItemUnitDel4.Click += BtnItemUnitDel_Click;

            btnItemUnitDown1.Click += BtnItemUnitDown_Click;
            btnItemUnitDown2.Click += BtnItemUnitDown_Click;
            btnItemUnitDown3.Click += BtnItemUnitDown_Click;
            btnItemUnitDown4.Click += BtnItemUnitDown_Click;


            btnItemUnitUp1.Click += BtnItemUnitUp_Click;
            btnItemUnitUp2.Click += BtnItemUnitUp_Click;
            btnItemUnitUp3.Click += BtnItemUnitUp_Click;
            btnItemUnitUp4.Click += BtnItemUnitUp_Click;


        }

        private void BtnItemUnitDown_Click(object sender, System.EventArgs e)
        {
            string ctr = ((SimpleButton)sender).Name.Substring(((SimpleButton)sender).Name.Length - 1, 1);
            if (ctr == "4") return;

            ChangeControl(sender, UnitDirection.Down);
        }

        private void BtnItemUnitUp_Click(object sender, System.EventArgs e)
        {
            string ctr = ((SimpleButton)sender).Name.Substring(((SimpleButton)sender).Name.Length - 1, 1);
            if (ctr == "1") return;

            ChangeControl(sender, UnitDirection.Up);
        }

        private void ChangeControl(object sender, UnitDirection unitDirection)
        {
            int cntDirection = unitDirection == UnitDirection.Up ? -1 : 1;

            string tmp = ((SimpleButton)sender).Tag.ToString();
            string[] result = tmp.Split(new char[] { ';' });

            int NextCtrPosition = A.GetInt(result[1].Substring(result[1].Length - 1, 1)) + cntDirection;

            string NextCodeCtr = result[1].Substring(0, result[1].Length - 1) + NextCtrPosition.ToString();
            string NextNameCtr = result[0].Substring(0, result[0].Length - 1) + NextCtrPosition.ToString();

            //일단 임시저장
            string strNameTmp = panelMain.Controls[result[0]].Text;
            string strCodeTmp = panelMain.Controls[result[1]].Text;

            //아래거 위에 저장
            panelMain.Controls[result[1]].Text = panelMain.Controls[NextCodeCtr].Text;
            panelMain.Controls[result[0]].Text = panelMain.Controls[NextNameCtr].Text;

            //아래거에 원래거 저장
            panelMain.Controls[NextNameCtr].Text = strNameTmp;
            panelMain.Controls[NextCodeCtr].Text = strCodeTmp;
        }

        private enum UnitDirection
        { 
            Up,
            Down
        }

        private void BtnItemUnitDel_Click(object sender, System.EventArgs e)
        {
            string tmp = ((SimpleButton)sender).Tag.ToString();
            string[] result = tmp.Split(new char[] { ';' });

            panelMain.Controls[result[0]].Text = string.Empty; 
            panelMain.Controls[result[1]].Text = string.Empty;
        }

        private void TxtUnitQt_Enter(object sender, System.EventArgs e)
        {
            if (((aNumericText)sender).Text == string.Empty || ((aNumericText)sender).DecimalValue == 0)
            {
                ((aNumericText)sender).SelectAll();
            }
        }

        private void M_POS_ITEM_Shown(object sender, System.EventArgs e)
        {
            OnView();
        }

        private void CodeTextChanged(object sender, System.EventArgs e)
        {
            if(((TextEdit)sender).Text == string.Empty)
            {
                switch (((TextEdit)sender).Name)
                {
                    case nameof(txtItemType):
                        txtNMItemType.Text = string.Empty;
                        break;
                    case nameof(txtItemTypeL):
                        aTextEdit5.Text = string.Empty;
                        break;
                    case nameof(txtItemTypeM):
                        aTextEdit6.Text = string.Empty;
                        break;
                    case nameof(txtItemTypeS):
                        aTextEdit7.Text = string.Empty;
                        break;
                    case nameof(txtVatType):
                        aTextEdit8.Text = string.Empty;
                        break;
                    case nameof(txtItemUnit1):
                        txtItenUnitCode1.Text = string.Empty;
                        break;
                    case nameof(txtItemUnit2):
                        txtItenUnitCode2.Text = string.Empty;
                        break;
                    case nameof(txtItemUnit3):
                        txtItenUnitCode3.Text = string.Empty;
                        break;
                    case nameof(txtItemUnit4):
                        txtItenUnitCode4.Text = string.Empty;
                        break;

                    default:
                        break;
                }
            }
        }

        private void BtnCode_Click(object sender, System.EventArgs e)
        {
            string tmp = ((SimpleButton)sender).Tag.ToString();
            string[] result = tmp.Split(new char[] { ';' });

            var ItemUnit = new List<object>();
            ItemUnit.Add(txtItenUnitCode1);
            ItemUnit.Add(txtItenUnitCode2);
            ItemUnit.Add(txtItenUnitCode4);
            ItemUnit.Add(txtItenUnitCode3);

            for (int i = ItemUnit.Count - 1; i >= 0; i--)
            {
                if (((TextEdit)ItemUnit[i]).Name == result[1].ToString())
                {
                    ItemUnit.RemoveAt(i);
                }
            }

            P_POS_CODE P_POS_CODE = new P_POS_CODE();
            P_POS_CODE.Code = result[2];
            P_POS_CODE.AutoSearch = true;

            if (((SimpleButton)sender).Name == nameof(btnUnit1) || ((SimpleButton)sender).Name == nameof(btnUnit2) || ((SimpleButton)sender).Name == nameof(btnUnit3) || ((SimpleButton)sender).Name == nameof(btnUnit4))
            {
                string[] strItemUnit = new string[ItemUnit.Count];

                for (int i = 0; i < ItemUnit.Count; i++)
                {
                    strItemUnit[i] = ((TextEdit)ItemUnit[i]).Text;
                }

                P_POS_CODE.SelectedCode = strItemUnit;
            }

            if (P_POS_CODE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                drRow = (DataRow)P_POS_CODE.ReturnData["ReturnData"];
                if (drRow != null)
                {
                    panelMain.Controls[result[1]].Text = drRow["CODE"].ToString().Trim();
                    panelMain.Controls[result[0]].Text = drRow["NAME"].ToString().Trim();
                }

                if (P_POS_CODE != null)
                {
                    P_POS_CODE.Dispose();
                }
            }
        }

        decimal MaxItemCode = 0;
        decimal MaxSortNo = 0;
        DataTable dtItem;

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            DataTable _dt = gridMain.DataSource as DataTable;

            ++MaxItemCode;
            ++MaxSortNo;

            view.SetRowCellValue(e.RowHandle, view.Columns["YN_USE"], "Y");
            view.SetRowCellValue(e.RowHandle, view.Columns["ST_ROW"], "I");
            view.SetRowCellValue(e.RowHandle, view.Columns["FG_VAT"], "16"); //기본 면세(계산서)
            view.SetRowCellValue(e.RowHandle, view.Columns["NM_VAT"], "면세(계산서)"); //기본 면세(계산서)

            view.SetRowCellValue(e.RowHandle, view.Columns["CD_ITEM"], A.GetString(MaxItemCode).PadLeft(14, '0'));
            view.SetRowCellValue(e.RowHandle, view.Columns["NO_SORT"], MaxSortNo);
        }

        private void BtnCtrNew_Click(object sender, System.EventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.BestFitColumns();
            gridView1.UpdateCurrentRow();
            gridView1.UpdateSummary();
        }

        private void InitializeControl()
        {
            SetControl ctr = new SetControl();
            ctr.SetCombobox(aLookUpEditItemType, CH.GetPOSCode("POS102", true));
            //ctr.SetCombobox(aLookUpEditM, CH.GetPOSCode("POS103", true));
            //ctr.SetCombobox(aLookUpEditS, CH.GetPOSCode("POS104", true));
            //ctr.SetCombobox(aLookUpEditVat, CH.GetPOSCode("SYS013", true 

            //ctr.SetCombobox(aLookUpUnit1, CH.GetPOSCode("POS101", true));
            //ctr.SetCombobox(aLookUpUnit2, CH.GetPOSCode("POS101", true));
            //ctr.SetCombobox(aLookUpUnit3, CH.GetPOSCode("POS101", true));
            //ctr.SetCombobox(aLookUpUnit4, CH.GetPOSCode("POS101", true));


            gridMain.SetBinding(panelMain, gridView1, new object[] { txtItemCode });

            DataTable dtItem = DBHelper.GetDataTable("SELECT MAX(NO_SORT) AS NO_SORT, MAX(CD_ITEM) AS CD_ITEM FROM POS_ITEM WHERE CD_STORE = '" + POSGlobal.StoreCode + "'");
            if (dtItem.Rows.Count > 0)
            {
                MaxItemCode = A.GetDecimal(dtItem.Rows[0]["CD_ITEM"]);
                MaxSortNo = A.GetDecimal(dtItem.Rows[0]["NO_SORT"]);
            }

            gridMain.VerifyNotNull = new string[] { "CD_ITEM", "NM_ITEM" };

        }

        public override void OnView()
        {
            LoadData.StartLoading(this);
            DataTable dt = Search(new object[] { POSGlobal.StoreCode, txtSearch.Text, aRadioButtonUseYN.EditValue, aLookUpEditItemType.EditValue, aRadioButtonInventory.EditValue });
            gridMain.Binding(dt, true);
            LoadData.EndLoading();
        }

        public override void OnInsert()
        {
            gridView1.AddNewRow();
            gridView1.BestFitColumns();
            gridView1.UpdateCurrentRow();
            gridView1.UpdateSummary();
        }

        public override void OnSave()
        {
            DataTable dtChange = gridMain.GetChanges();
            if (dtChange == null)
            {
                ShowMessageBoxA("변경된 내용이 없습니다.", Bifrost.Common.MessageType.Warning);
                return;
            }
            bool result = Save(dtChange);

            if (result)
            {
                gridMain.AcceptChanges();
                ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
            }
        }

        public override void OnDelete()
        {
            if (A.GetString(gridView1.GetFocusedRowCellValue("ST_ROW")) == "I")
            {
                gridView1.DeleteSelectedRows();
            }
            else
            {
                ShowMessageBoxA("신규 추가한 건만 삭제할수 있습니다.", Bifrost.Common.MessageType.Error);
                return;
            }
        }
    }
}
