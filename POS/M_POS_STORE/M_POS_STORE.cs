using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_STORE : POSFormBase
    {
        DataTable _dt = new DataTable();
        FreeFormBinding _freeForm = new FreeFormBinding();

        public M_POS_STORE()
        {
            InitializeComponent();
            InitializeForm();
            InitializeControl();
            InitializeEvent();

            OnView();
        }

        private void InitializeEvent()
        {
            btnPostCode.Click += BtnPostCode_Click;
        }

        private void BtnPostCode_Click(object sender, EventArgs e)
        {
            P_POS_POSTCODE P_POS_POSTCODE = new P_POS_POSTCODE();
            if (P_POS_POSTCODE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataRow drRow = (DataRow)P_POS_POSTCODE.ReturnData["ReturnData"];
                if (drRow != null)
                {
                    txtPostCode.Text = drRow["CD_POST"].ToString().Trim();
                    txtAddr1.Text = drRow["DC_ADD1"].ToString().Trim();
                }

                if (P_POS_POSTCODE != null)
                {
                    P_POS_POSTCODE.Dispose();
                }
            }
        }

        private void InitializeForm()
        {
            _dt = Search(new object[] { POSGlobal.StoreCode });

            _freeForm.SetBinding(_dt, panelMain);
            _freeForm.ClearAndNewRow();
        }

        private void InitializeControl()
        {
            //SetControl ctr = new SetControl();
            //ctr.SetCombobox(aLookUpEditL, CH.GetPOSCode("POS102", true));

            //gridMain.SetBinding(panelMain, gridView1, new object[] { txtItemCode });
        }

        public override void OnView()
        {
            _dt = Search(new object[] { POSGlobal.StoreCode });
            _freeForm.SetDataTable(_dt);
            //DataTable dt = Search(new object[] { POSGlobal.StoreCode, txtSearch.Text, aRadioButton1.EditValue });
            //gridMain.Binding(dt, true);


        }

        public override void OnSave()
        {
            
            DataTable dtChange = _freeForm.GetChanges();
            if (dtChange == null) return;
            bool result = Save(dtChange);

            if (result)
            {
                _freeForm.AcceptChanges();
                ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
            }
        }

    }
}
