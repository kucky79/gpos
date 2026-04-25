using Bifrost;
using Bifrost.CommonPopup;
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

namespace MAS
{
    public partial class M_MAS_EMP : BifrostFormBase
    {
        public M_MAS_EMP()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitEvent()
        {
            btnAddRow.Click += BtnAddRow_Click;
            btnDeleteRow.Click += BtnDeleteRow_Click;
            this.Load += M_MAS_STORE_MANAGE_Load;

            btnPost.Click += BtnPost_Click;
        }

        private void BtnPost_Click(object sender, EventArgs e)
        {
            P_POST P_POST = new P_POST();
            if (P_POST.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataRow drRow = (DataRow)P_POST.ReturnData["ReturnData"];
                if (drRow != null)
                {
                    txtPost.Text = drRow["CD_POST"].ToString().Trim();
                    txtAddr1.Text = drRow["DC_ADD1"].ToString().Trim();
                }

                if (P_POST != null)
                {
                    P_POST.Dispose();
                }
            }
        }

        private void BtnDeleteRow_Click(object sender, EventArgs e)
        {
            if (gridView.GetFocusedDataRow().RowState == DataRowState.Added)
                gridView.DeleteSelectedRows();
        }

        private void M_MAS_STORE_MANAGE_Load(object sender, EventArgs e)
        {
            OnView();
        }

        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            gridView.AddNewRow();
            gridView.UpdateCurrentRow();

        }

        private void InitControl()
        {
            //SetControl scr = new SetControl();
            //scr.SetCombobox(aLookUpEditArea, CH.GetCode("MAS100"));
            //scr.SetCombobox(aLookUpEditItem, CH.GetCode("MAS101"));

            //scr.SetCombobox(aLookUpEditSearchArea, CH.GetCode("MAS100", true));
            //scr.SetCombobox(aLookUpEditSearchBizItem, CH.GetCode("MAS101", true));

            aGridEmp.SetBinding(panelMain, gridView, new object[] { txtEmpCode });

        }

        public override void OnView()
        {
            aGridEmp.Binding(Search(new object[] { Global.FirmCode }), true);
        }

        public override void OnSave()
        {
            DataTable dtChange = aGridEmp.GetChanges();

            if(dtChange != null)
            {
                bool result = Save(dtChange);
                if(result)
                {
                    aGridEmp.AcceptChanges();
                    ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
                }
            }

        }
    }
}