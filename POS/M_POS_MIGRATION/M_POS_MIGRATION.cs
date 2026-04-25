using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniFile = Bifrost.Helper.IniFile;

namespace POS
{
    public partial class M_POS_MIGRATION : POSFormBase
    {
        //DataTable _dt = new DataTable();
        //FreeFormBinding _freeForm = new FreeFormBinding();

        public M_POS_MIGRATION()
        {
            InitializeComponent();
            InitializeForm();
            InitializeControl();
            InitializeEvent();

            OnView();
        }

        private void InitializeEvent()
        {
            btnTargetStore.Click += BtnTargetStore_Click;
            btnClear.Click += BtnClear_Click;

            btnConnectCheck.Click += BtnConnectCheck_Click;
            btnMagration.Click += BtnMagration_Click;
        }

        private void BtnMagration_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=" + txtTargetAddr.Text + ";initial Catalog=" + txtTargetCatalog.Text + ";uid=" + txtTargetID.Text + ";password=" + txtTargetPassword.Text + ";";

            try
            {
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = ConnectionString;
                conn.Open();

                #region 링크드서버 

                SqlCommand cmdLinkedServer = new SqlCommand("sp_addlinkedserver", conn);

                SqlParameter pServer = new SqlParameter("@server", SqlDbType.NVarChar, 128);
                SqlParameter pSvrProduct = new SqlParameter("@srvproduct", SqlDbType.NVarChar, 128);
                SqlParameter pProvider = new SqlParameter("@provider", SqlDbType.NVarChar, 4000);
                SqlParameter pDataSource = new SqlParameter("@datasrc", SqlDbType.NVarChar, 4000);
                SqlParameter pCatalog = new SqlParameter("@catalog", SqlDbType.NVarChar, 128);

                pServer.Value = "GPOS_MIG";
                pSvrProduct.Value = "MSSQL";
                pProvider.Value = "SQLOLEDB";
                pDataSource.Value = txtDestinationAddr.Text;
                pCatalog.Value = txtDestinationCatalog.Text;

                cmdLinkedServer.CommandType = CommandType.StoredProcedure;
                cmdLinkedServer.Parameters.Add(pServer);
                cmdLinkedServer.Parameters.Add(pSvrProduct);
                cmdLinkedServer.Parameters.Add(pProvider);
                cmdLinkedServer.Parameters.Add(pDataSource);
                cmdLinkedServer.Parameters.Add(pCatalog);

                cmdLinkedServer.ExecuteNonQuery();
                #endregion 링크드서버 

                #region 링크드서버 계정 
                SqlCommand cmdLogin = new SqlCommand("sp_addlinkedsrvlogin", conn);

                //링크드서버 계정
                SqlParameter pServerName = new SqlParameter("@rmtsrvname", SqlDbType.NVarChar, 128);
                SqlParameter pUseSelf = new SqlParameter("@useself", SqlDbType.VarChar, 8);
                SqlParameter pLogin = new SqlParameter("@locallogin", SqlDbType.NVarChar, 128);
                SqlParameter pUserID = new SqlParameter("@rmtuser", SqlDbType.NVarChar, 128);
                SqlParameter pPassword = new SqlParameter("@rmtpassword", SqlDbType.NVarChar, 128);

                pServerName.Value = "GPOS_MIG";
                pUseSelf.Value = "False";
                pLogin.Value = DBNull.Value;
                pUserID.Value = txtDestinationID.Text;
                pPassword.Value = txtDestinationPassword.Text;

                cmdLogin.CommandType = CommandType.StoredProcedure;
                cmdLogin.Parameters.Add(pServerName);
                cmdLogin.Parameters.Add(pUseSelf);
                cmdLogin.Parameters.Add(pLogin);
                cmdLogin.Parameters.Add(pUserID);
                cmdLogin.Parameters.Add(pPassword);

                cmdLogin.ExecuteNonQuery();

                #endregion 링크드서버 계정

                #region DB 마이그레이션
                SqlCommand cmdMigration = new SqlCommand("USP_MIGRATION", conn);

                //링크드서버 계정
                SqlParameter pTargetStoreCode = new SqlParameter("@CD_STORE_TARGET", SqlDbType.VarChar, 6);
                SqlParameter pDestinationStoreCode = new SqlParameter("@CD_STORE_DESTINATION", SqlDbType.NVarChar, 10);
                SqlParameter pMigrationCode = new SqlParameter("@CD_MIGRATION", SqlDbType.NVarChar, 100);

                pTargetStoreCode.Value = txtStoreCode.Text;
                pDestinationStoreCode.Value = txtTargetStoreCode.Text;
                pMigrationCode.Value = A.GetDummySlipNo;

                cmdMigration.CommandType = CommandType.StoredProcedure;
                cmdMigration.Parameters.Add(pTargetStoreCode);
                cmdMigration.Parameters.Add(pDestinationStoreCode);
                cmdMigration.Parameters.Add(pMigrationCode);

                cmdMigration.ExecuteNonQuery();

                #endregion DB 마이그레이션
            }
            catch (Exception ex)  
            {
                HandleWinException(ex);
            }
        }

        private void BtnConnectCheck_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();
            string StoreSearch = "SELECT * FROM MA_STORE";
            
            string ConnectionString = "Data Source=" + txtTargetAddr.Text + ";initial Catalog=" + txtTargetCatalog.Text + ";uid=" + txtTargetID.Text + ";password=" + txtTargetPassword.Text + ";";
            conn.ConnectionString = ConnectionString;
            try
            {
                conn.Open();       // conn을 열고 밑에 문자열을 출력한다.

                // SqlDataAdapter 초기화
                SqlDataAdapter adapter = new SqlDataAdapter(StoreSearch, conn);

                DataSet ds = new DataSet();
                // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtStoreCode.Text = A.GetString(ds.Tables[0].Rows[0]["STORE_CD"]);
                    txtStoreName.Text = A.GetString(ds.Tables[0].Rows[0]["STORE_NM"]);
                    txtCEOName.Text = A.GetString(ds.Tables[0].Rows[0]["SUB_NM"]);
                    txtBizCode.Text = A.GetString(ds.Tables[0].Rows[0]["BUSI_NO"]);

                    txtTargetAddr.Enabled = false;
                    txtTargetID.Enabled = false;
                    txtTargetPassword.Enabled = false;
                    txtTargetCatalog.Enabled = false;

                    string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
                    IniFile inifile = new IniFile();
                    //default 
                    inifile.IniWriteValue("DB", "TargetConnectionString", ConnectionString, Path);
                    TargetConnectionString = inifile.IniReadValue("DB", "TargetConnectionString", Path);
                }

                conn.Close();

                Console.WriteLine("데이터베이스 연결 성공...");
            }
            catch (Exception ex)    // 예외처리로서 연결할수 없으면 밑에 문자열을 출력한다.
            {
                Console.WriteLine("데이터베이스 연결 실패...");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();   // conn을 닫고 밑에 문자열을 출력한다.
                    Console.WriteLine("데이터베이스 연결 해제...");
                }
            }

        }



        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtTargetStoreCode.Text = string.Empty;
            txtTargetStoreName.Text = string.Empty;
        }

        private void BtnTargetStore_Click(object sender, EventArgs e)
        {
            P_POS_STORE P_POS_STORE = new P_POS_STORE();
            P_POS_STORE.AutoSearch = true;
            P_POS_STORE.SearchCondition = txtTargetStoreCode.Text;
            if(P_POS_STORE.ShowDialog() == DialogResult.OK)
            {
                DataRow drRow = drRow = (DataRow)P_POS_STORE.ReturnData["ReturnData"];
                if (drRow != null)
                {
                    txtTargetStoreCode.Text = drRow["CD_STORE"].ToString().Trim();
                    txtTargetStoreName.Text = drRow["NM_STORE"].ToString().Trim();
                }

                if (P_POS_STORE != null)
                {
                    P_POS_STORE.Dispose();
                }
            }

        }

        private void InitializeForm()
        {
            //_dt = Search(new object[] { A.GetDummyString });

            //_freeForm.SetBinding(_dt, panelMain);
            //_freeForm.ClearAndNewRow();
        }

        string TargetConnectionString;
        private void InitializeControl()
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            IniFile inifile = new IniFile();
            char[] delimiterChars = { ';', '=' };

            //마이그레이션 대상 DB

            //Target DB Server
            TargetConnectionString = inifile.IniReadValue("DB", "TargetConnectionString", Path);
            if (TargetConnectionString == string.Empty)
            {
                //default 
                inifile.IniWriteValue("DB", "TargetConnectionString", "Data Source=localhost; initial Catalog="+ txtSaleCatalog.Text + ";uid=YOUR_USER;password=YOUR_PASSWORD;", Path);
                TargetConnectionString = inifile.IniReadValue("DB", "TargetConnectionString", Path);
            }

            string[] TargetWords = TargetConnectionString.Split(delimiterChars);

            txtTargetAddr.Text = TargetWords[1].ToString();
            txtTargetID.Text = TargetWords[5].ToString();
            txtTargetPassword.Text = TargetWords[7].ToString();
            txtTargetCatalog.Text = TargetWords[3].ToString();


            //옮겨갈 DB
            string[] DestinationWords = inifile.IniReadValue("DB", "OldConnectionString", Path).Split(delimiterChars);

            txtDestinationAddr.Text = DestinationWords[1].ToString();
            txtDestinationID.Text = DestinationWords[5].ToString();
            txtDestinationPassword.Text = DestinationWords[7].ToString();
            txtDestinationCatalog.Text = DestinationWords[3].ToString();

            //SetControl ctr = new SetControl();
            //ctr.SetCombobox(aLookUpEditL, CH.GetPOSCode("POS102", true));

            //gridMain.SetBinding(panelMain, gridView1, new object[] { txtItemCode });
        }

        public override void OnView()
        {
            
            //DataTable dt = Search(new object[] { POSGlobal.StoreCode, txtSearch.Text, aRadioButton1.EditValue });
            //gridMain.Binding(dt, true);


        }

        public override void OnSave()
        {
            
            //DataTable dtChange = _freeForm.GetChanges();
            //if (dtChange == null) return;
            //bool result = Save(dtChange);

            //if (result)
            //{
            //    _freeForm.AcceptChanges();
            //    ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
            //}
        }

    }
}
