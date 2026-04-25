using DevExpress.XtraGrid.Views.Grid;
using NF.A2P;
using NF.A2P.Helper;
using NF.Framework.Common;
using NF.Framework.Win;
using NF.Framework.Win.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NF.A2P.Helper.aGridHelper;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList;
using System.Collections;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_MENU : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Code = string.Empty;
        

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string SearchCondition
        {
            set
            {
                //this.OnSearch();
                txtSearch.Text = value;

                if (txtSearch.Text.Length > 1)
                {
                    this.OnSearch();
                }
            }
        }

        public POPUP_MENU()
        {
            InitializeComponent();
            PopupTitle = "메뉴 도움창";
            InitEvent();
            InitTree();
        }

        public POPUP_MENU(string sNmenu)
        {
            InitializeComponent();
            PopupTitle = "메뉴 도움창";
            InitEvent();
            InitTree();
            txtSearch.Text = sNmenu;
        }

        public void InitTree()
        {
            //더미 데이터 바인딩 (일단 트리에 데이터를 바인딩 해야 먹는 속성이 있어서 그냥 먼저 더미 세팅)           

            try
            {
                
                dt = DBHelper.GetDataTable("AP_SYS_USERMENU_S3", new object[] { "!@#$", "!@#$", "!@#$" });                
                
                aTreeFirm.DataSource = dt;

                //키컬럼 설정
                aTreeFirm.KeyFieldName = "CD_MENU";
                //
                aTreeFirm.ParentFieldName = "CD_MENU_PARENT";
                aTreeFirm.ColumnVislble(new string[] { "CD_FIRM", "NO_LEVEL", "NO_SEQ", "NO_POS" });
                aTreeFirm.ColumnReadOnly(new string[] { "NM_MENU" });
                aTreeFirm.OptionsDragAndDrop.CanCloneNodesOnDrop = true;                
                aTreeFirm.OptionsDragAndDrop.DropNodesMode = DropNodesMode.Advanced;

               
                
                

                if (txtSearch.Text != "")
                    OnSearch();


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (dt != null)
                    dt.Dispose();
            }

        }        


        private void InitEvent()
        {            
            this.aTreeFirm.DoubleClick += new System.EventHandler(this.aTreeFirm_DoubleClick);
            aButton_Search.Click += AButton_Search_Click;
            txtSearch.KeyDown += TxtSearch_KeyDown;            

        }               
     


        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnSearch();
            }
        }

        private void AButton_Search_Click(object sender, EventArgs e)
        {
            OnSearch();
        }

        #region Buttons'  handlers
        protected override void OnSearch()
        {
            //더미 데이터 바인딩 (일단 트리에 데이터를 바인딩 해야 먹는 속성이 있어서 그냥 먼저 더미 세팅)
            dt = null;

            try
            {
                int nCnt = 0;
                string sSearch = txtSearch.Text + "%";                

                dt = DBHelper.GetDataTable("AP_SYS_USERMENU_S3", new object[] { Global.FirmCode, Global.UserID,  sSearch});
                if (dt.Rows.Count == 0)
                {
                    ShowMessageBoxA("The data not found. Please search again.", MessageType.Information);
                    return;
                }
                else
                {                    
                    aTreeFirm.DataSource = dt;
                }
                foreach (TreeListNode node in aTreeFirm.Nodes)
                {
                    if (node.Level < 3) { node.Expanded = true; }                    
                }
                
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    if (Convert.ToInt16(dt.Rows[i]["NO_LEVEL"].ToString()) > 2)
                        nCnt++;
                }
                if (nCnt == 0)
                    aTreeFirm.DataSource = null;

                base.OnSearch();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (dt != null)
                    dt.Dispose();
            }
            
        }

        protected override void OnOK()
        {
            if (aTreeFirm.AllNodesCount > 0)
            {
                
                
                object dn = aTreeFirm.GetDataRecordByNode(aTreeFirm.FocusedNode);
                DataRowView d = (DataRowView)dn;
                
                ReturnData.Add("ReturnData", (DataRow)d.Row);

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBoxA("데이터가 존재하지 않습니다.", MessageType.Information);
            }
        }

        protected override void OnCancel()
        {
            this.Close();
        }

        #endregion Buttons'  handlers

        private void _GridM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void aTreeFirm_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }
    }
}
