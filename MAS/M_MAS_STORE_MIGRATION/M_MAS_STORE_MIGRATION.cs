using Bifrost;
using Bifrost.Grid;
using Bifrost.Helper;
using Bifrost.Win;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAS
{
    public partial class M_MAS_STORE_MIGRATION : BifrostFormBase
    {
        public M_MAS_STORE_MIGRATION()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitEvent()
        {
            this.Load += M_MAS_STORE_MANAGE_Load;


            btnGetDataSale.Click += BtnGetDataSale_Click;
            btnGetDataPurchase.Click += BtnGetDataPurchase_Click;

            btnMigrationMaster.Click += BtnMigration_Click;
            btnSaleItemPrice.Click += BtnMigrationItemPrice_Click;
            btnPurchaseItemPrice.Click += BtnPurchaseItemPrice_Click;

            gridViewMain.FocusedRowChanged += GridViewMain_FocusedRowChanged;

            btnOldSaleConnect.Click += BtnOldSaleConnect_Click;
            btnOldPurchaseConnect.Click += BtnOldPurchaseConnect_Click;

            btnOldMigration.Click += BtnOldMigration_Click;

            btnSaleBackUp.Click += BtnSaleBackUp_Click;
            btnPurchaseBackUp.Click += BtnPurchaseBackUp_Click;


            btnOldSaleFileFind.Click += BtnOldSaleFileFind_Click;
            btnOldPurchaseFileFind.Click += BtnOldPurchaseFileFind_Click;


            btnOldSaleRestroe.Click += BtnOldSaleRestroe_Click;
            btnOldPurchaseRestroe.Click += BtnOldPurchaseRestroe_Click;

            btnCustCheck.Click += BtnCustCheck_Click;
        }

        private void BtnOldPurchaseRestroe_Click(object sender, EventArgs e)
        {
            if (txtOldPurchasePath.Text == string.Empty)
            {
                ShowMessageBoxA("선택된 파일이 존재하지 않습니다.\n백업 파일을 선택해주세요.", Bifrost.Common.MessageType.Warning);
                return;
            }

            SqlConnection conn = new SqlConnection();
            StringBuilder query = new StringBuilder();

            query.Append(@"IF EXISTS(SELECT 1 FROM sys.databases WHERE name='OLD_MARKET_P')" + "\n");
            query.Append(@"BEGIN" + "\n");
            query.Append(@"                DROP DATABASE OLD_MARKET_P" + "\n");
            query.Append(@"END" + "\n");
            query.Append(@"RESTORE FILELISTONLY" + "\n");
            query.Append(@"FROM DISK = '" + txtOldPurchasePath.Text + "'" + "\n");

            query.Append(@"RESTORE DATABASE OLD_MARKET_P" + "\n");
            query.Append(@"FROM DISK = '" + txtOldPurchasePath.Text + "'" + "\n");
            query.Append(@"WITH MOVE 'Old_Market_Data' TO 'C:\Database\GPos_DB_P\DATA\Old_Market_Data.MDF'," + "\n");
            query.Append(@"MOVE 'Old_Market_Log' TO 'C:\Database\GPos_DB_P\Old_Market_Log.ldf'" + "\n");

            string ConnectionStringPurchase = "Data Source=" + txtOldPurchaseIP.Text +  ";uid=" + txtOldPurchaseID.Text + ";password=" + txtOldPurchasePW.Text + ";";
            conn.ConnectionString = ConnectionStringPurchase;
            try
            {
                LoadData.StartLoading(this, "Progressing....", "복원을 진행중입니다.");

                conn.Open();


                SqlCommand command = new SqlCommand();
                command.CommandTimeout = 0;
                command.CommandText = query.ToString();
                command.Connection = conn;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                adapter.Fill(ds);

                conn.Close();

                //txtOldPurchasePath.Text = ds.Tables[0].Rows[0]["PATH"].ToString();

                LoadData.EndLoading();
                ShowMessageBoxA("판매 DB 복원이 완료되었습니다.", Bifrost.Common.MessageType.Information);
            }
            catch (Exception ex)
            {
                LoadData.EndLoading();
                HandleWinException(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void BtnOldSaleRestroe_Click(object sender, EventArgs e)
        {
            if(txtOldSalePath.Text == string.Empty)
            {
                ShowMessageBoxA("선택된 파일이 존재하지 않습니다.\n백업 파일을 선택해주세요.", Bifrost.Common.MessageType.Warning);
                return;
            }

            SqlConnection conn = new SqlConnection();
            StringBuilder query = new StringBuilder();

            query.Append(@"IF EXISTS(SELECT 1 FROM sys.databases WHERE name='OLD_MARKET_S')" + "\n");
            query.Append(@"BEGIN" + "\n");
            query.Append(@"                DROP DATABASE OLD_MARKET_S" + "\n");
            query.Append(@"END" + "\n");
            query.Append(@"RESTORE FILELISTONLY" + "\n");
            query.Append(@"FROM DISK = '" + txtOldSalePath.Text + "'" + "\n");

            query.Append(@"RESTORE DATABASE OLD_MARKET_S" + "\n");
            query.Append(@"FROM DISK = '" + txtOldSalePath.Text + "'" + "\n");
            query.Append(@"WITH MOVE 'Old_Market_Data' TO 'C:\Database\GPos_DB_S\DATA\Old_Market_Data.MDF'," + "\n");
            query.Append(@"MOVE 'Old_Market_Log' TO 'C:\Database\GPos_DB_S\Old_Market_Log.ldf'" + "\n");

            string ConnectionStringSale = "Data Source=" + txtOldSaleIP.Text + ";uid=" + txtOldSaleID.Text + ";password=" + txtOldSalePW.Text + ";";
            conn.ConnectionString = ConnectionStringSale;
            try
            {
                LoadData.StartLoading(this, "Progressing....", "복원을 진행중입니다.");

                conn.Open();


                SqlCommand command = new SqlCommand();
                command.CommandTimeout = 0;
                command.CommandText = query.ToString();
                command.Connection = conn;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                adapter.Fill(ds);

                conn.Close();

                //txtOldSalePath.Text = ds.Tables[0].Rows[0]["PATH"].ToString();

                LoadData.EndLoading();
                ShowMessageBoxA("판매 DB 복원이 완료되었습니다.", Bifrost.Common.MessageType.Information);
            }
            catch (Exception ex)
            {
                LoadData.EndLoading();
                HandleWinException(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        private void BtnOldPurchaseFileFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog(); 
            openFileDlg.DefaultExt = "db"; 
            openFileDlg.Filter = "Sql Bakup File(*.bak)|*.bak"; 
            openFileDlg.ShowDialog(); 
            if (openFileDlg.FileName.Length > 0) 
            { 
                foreach (string filename in openFileDlg.FileNames) 
                { 
                    this.txtOldPurchasePath.Text = filename; 
                } 
            }

        }

        private void BtnOldSaleFileFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.DefaultExt = "db";
            openFileDlg.Filter = "Sql Bakup File(*.bak)|*.bak";
            openFileDlg.ShowDialog();
            if (openFileDlg.FileName.Length > 0)
            {
                foreach (string filename in openFileDlg.FileNames)
                {
                    this.txtOldSalePath.Text = filename;
                }
            }
        }

        private void BtnCustCheck_Click(object sender, EventArgs e)
        {
            SetCustomerCode();
        }

        private void BtnPurchaseBackUp_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();

            StringBuilder query = new StringBuilder();

            query.Append("DECLARE @Path nvarchar(200)" + "\n");
            query.Append("DECLARE @DBName nvarchar(100)" + "\n");
            query.Append("DECLARE @FileName nvarchar(100)" + "\n");

            query.Append("DECLARE @TXFileName nvarchar(100)" + "\n");
            query.Append("DECLARE @FullFileName nvarchar(200)" + "\n");
            query.Append("DECLARE @TXFullFileName nvarchar(200)" + "\n");
            query.Append("DECLARE @StoreName NVarchar(200)" + "\n");

            query.Append("SET @StoreName = '_" + txtStoreCode.Text + "_'" + "\n");
            query.Append("SET @Path = 'C:\\DATABASE'" + "\n");
            query.Append("SET @DBName = '" + txtPurchaseCatalog.Text + "'" + "\n");

            query.Append("SET @Path = @Path + N'\\'" + "\n");
            query.Append("SET @FileName = CONVERT(NVARCHAR(8), GETDATE(), 112) + @StoreName + @DBName" + "\n");
            query.Append("SET @FullFileName = @Path + N'\' + @FileName + N'.bak'" + "\n");
            query.Append("BACKUP DATABASE @DBName TO DISK = @FullFileName WITH NOFORMAT, NOINIT, NAME = @FileName, SKIP, REWIND, NOUNLOAD, STATS = 10" + "\n");
            query.Append("SELECT @FullFileName AS PATH" + "\n");



            string ConnectionStringPurchase = "Data Source=" + txtPurchaseIP.Text + ";initial Catalog=" + txtPurchaseCatalog.Text + ";uid=" + txtPurchaseID.Text + ";password=" + txtPurchasePassword.Text + ";";
            conn.ConnectionString = ConnectionStringPurchase;
            try
            {
                if (ShowMessageBoxA("백업은 몇분의 시간이 소요됩니다.\n진행하시겠습니까?", Bifrost.Common.MessageType.Question) == DialogResult.Yes)
                {
                    LoadData.StartLoading(this, "Progressing....", "백업을 진행중입니다.");

                    conn.Open();


                    SqlCommand command = new SqlCommand();
                    command.CommandTimeout = 0;
                    command.CommandText = query.ToString();
                    command.Connection = conn;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet ds = new DataSet();
                    // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                    adapter.Fill(ds);

                    conn.Close();

                    txtOldPurchasePath.Text = ds.Tables[0].Rows[0]["PATH"].ToString();

                    LoadData.EndLoading();
                    ShowMessageBoxA("구매 DB 백업이 완료되었습니다.", Bifrost.Common.MessageType.Information);
                }
            }
            catch (Exception ex)
            {
                LoadData.EndLoading();
                HandleWinException(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void BtnSaleBackUp_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();

            StringBuilder query = new StringBuilder();

            query.Append("DECLARE @Path nvarchar(200)" + "\n");
            query.Append("DECLARE @DBName nvarchar(100)" + "\n");
            query.Append("DECLARE @FileName nvarchar(100)" + "\n");

            query.Append("DECLARE @TXFileName nvarchar(100)" + "\n");
            query.Append("DECLARE @FullFileName nvarchar(200)" + "\n");
            query.Append("DECLARE @TXFullFileName nvarchar(200)" + "\n");
            query.Append("DECLARE @StoreName NVarchar(200)" + "\n");

            query.Append("SET @StoreName = '_"+ txtStoreCode.Text + "_'" + "\n");
            query.Append("SET @Path = 'C:\\DATABASE'" + "\n");
            query.Append("SET @DBName = '"+ txtSaleCatalog.Text + "'" + "\n");

            query.Append("SET @Path = @Path + N'\\'" + "\n");
            query.Append("SET @FileName = CONVERT(NVARCHAR(8), GETDATE(), 112) + @StoreName + @DBName" + "\n");
            query.Append("SET @FullFileName = @Path + N'\' + @FileName + N'.bak'" + "\n");
            query.Append("BACKUP DATABASE @DBName TO DISK = @FullFileName WITH NOFORMAT, NOINIT, NAME = @FileName, SKIP, REWIND, NOUNLOAD, STATS = 10" + "\n");
            query.Append("SELECT @FullFileName AS PATH" + "\n");


            string ConnectionStringSale = "Data Source=" + txtSaleIP.Text + ";initial Catalog=" + txtSaleCatalog.Text + ";uid=" + txtSaleID.Text + ";password=" + txtSalePassword.Text + ";";
            conn.ConnectionString = ConnectionStringSale;
            try
            {
                if (ShowMessageBoxA("백업은 몇분의 시간이 소요됩니다.\n진행하시겠습니까?", Bifrost.Common.MessageType.Question) == DialogResult.Yes)
                {
                    LoadData.StartLoading(this, "Progressing....", "백업을 진행중입니다.");

                    conn.Open();


                    SqlCommand command = new SqlCommand();
                    command.CommandTimeout = 0;
                    command.CommandText = query.ToString();
                    command.Connection = conn;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet ds = new DataSet();
                    // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                    adapter.Fill(ds);

                    conn.Close();

                    txtOldSalePath.Text = ds.Tables[0].Rows[0]["PATH"].ToString();

                    LoadData.EndLoading();
                    ShowMessageBoxA("판매 DB 백업이 완료되었습니다.", Bifrost.Common.MessageType.Information);
                }
            }
            catch (Exception ex)
            {
                LoadData.EndLoading();
                HandleWinException(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void BtnOldMigration_Click(object sender, EventArgs e)
        {
            if (IsOldSaleConnect && IsOldPurchaseConnect)
            {
                if (ShowMessageBoxA(txtOldStoreCode.Text + "로 마이그레이선 하시겠습니까?", Bifrost.Common.MessageType.Question) == DialogResult.Yes)
                {


                    int SlipCount = A.GetInt(DBHelperOld.ExecuteScalar("SELECT COUNT(RCPT_NO) AS CNT FROM TR_SALE_H WHERE STORE_CD = '" + txtOldStoreCode.Text + "'"));
                    if (SlipCount > 0)
                    {
                        ShowMessageBoxA("이미 거래가 존재하여 마이그래이션을 할 수 없습니다.\n다른 매장코드를 선택하거나 신규생성 하세요.", Bifrost.Common.MessageType.Warning);
                        return;
                    }
                    else
                    {

                        DBHelperOld.ExecuteNonQuery("USP_MIGRATION_SALE", new object[] { txtOldStoreCode.Text });
                    }
                }
            }
            else
            {
                ShowMessageBoxA("구버전 판매/구매 디비 접속을 확인해주세요.", Bifrost.Common.MessageType.Warning);
                return;
            }    

        }

        bool IsOldSaleConnect { get; set; } = false;
        bool IsOldPurchaseConnect { get; set; } = false;

        private void BtnOldPurchaseConnect_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * FROM MA_STORE\n");

            string ConnectionStringSale = "Data Source=" + txtOldPurchaseIP.Text + ";initial Catalog=" + txtOldPurchaseCatalog.Text + ";uid=" + txtOldPurchaseID.Text + ";password=" + txtOldPurchasePW.Text + ";";
            conn.ConnectionString = ConnectionStringSale;
            try
            {
                conn.Open();       // conn을 열고 밑에 문자열을 출력한다.

                // SqlDataAdapter 초기화
                SqlDataAdapter adapter = new SqlDataAdapter(query.ToString(), conn);

                DataSet ds = new DataSet();
                // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowMessageBoxA("데이터 베이스 접속 성공", Bifrost.Common.MessageType.Information);
                    IsOldSaleConnect = true;
                }

                conn.Close();

                Console.WriteLine("데이터베이스 연결 성공...");
            }
            catch (Exception ex) 
            {
                HandleWinException(ex);
                Console.WriteLine("데이터베이스 연결 실패...");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    Console.WriteLine("데이터베이스 연결 해제...");
                }
            }
        }

        private void BtnOldSaleConnect_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * FROM MA_STORE\n");
            string ConnectionStringSale = "Data Source=" + txtOldSaleIP.Text + ";initial Catalog=" + txtOldSaleCatalog.Text + ";uid=" + txtOldSaleID.Text + ";password=" + txtOldSalePW.Text + ";";


            conn.ConnectionString = ConnectionStringSale;
            try
            {
                conn.Open();       // conn을 열고 밑에 문자열을 출력한다.

                // SqlDataAdapter 초기화
                SqlDataAdapter adapter = new SqlDataAdapter(query.ToString(), conn);

                DataSet ds = new DataSet();
                // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowMessageBoxA("데이터 베이스 접속 성공", Bifrost.Common.MessageType.Information);
                    IsOldPurchaseConnect = true;
                }

                conn.Close();

                Console.WriteLine("데이터베이스 연결 성공...");
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
                Console.WriteLine("데이터베이스 연결 실패...");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    Console.WriteLine("데이터베이스 연결 해제...");
                }
            }
        }

        private void BtnPurchaseItemPrice_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();

            StringBuilder query = new StringBuilder();

            query.Append("DECLARE @CD_STORE NVARCHAR(10)" + "\n");
            query.Append("SET @CD_STORE  = '" + gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "'" + "\n");
            query.Append("" + "\n");
            query.Append("if exists(select * from information_schema.tables where table_name='TMP_PURCHASE_D') " + "\n");
            query.Append("BEGIN  " + "\n");
            query.Append("     DROP TABLE TMP_PURCHASE_D    " + "\n");
            query.Append("END            " + "\n"); 
            query.Append("" + "\n");
            query.Append("CREATE TABLE TMP_PURCHASE_D" + "\n");
            query.Append("(" + "\n");
            query.Append("    CST_CD  varchar(4) NOT NULL," + "\n");
            query.Append("    MNU_CD  varchar(15) NOT NULL," + "\n");
            query.Append("    UNIT_NM varchar(50) NOT NULL," + "\n");
            query.Append("    SALE_DT varchar(8) NOT NULL," + "\n");
            query.Append("    RCPT_NO varchar(15) NOT NULL," + "\n");
            query.Append("    CONSTRAINT PK_TMP_PURCHASE_D" + "\n");
            query.Append("    PRIMARY KEY CLUSTERED" + "\n");
            query.Append("    (" + "\n");
            query.Append("        CST_CD ASC," + "\n");
            query.Append("        MNU_CD ASC," + "\n");
            query.Append("        UNIT_NM ASC," + "\n");
            query.Append("        SALE_DT ASC," + "\n");
            query.Append("        RCPT_NO ASC" + "\n");
            query.Append("    )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" + "\n");
            query.Append(")" + "\n");
            query.Append("ON[PRIMARY]" + "\n");
            query.Append("--판매데이터 입력" + "\n");
            query.Append("INSERT INTO   TMP_PURCHASE_D" + "\n");
            query.Append("SELECT   CST_CD," + "\n");
            query.Append("         MNU_CD," + "\n");
            query.Append("         UNIT_NM," + "\n");
            query.Append("         MAX(SALE_DT) AS SALE_DT," + "\n");
            query.Append("         MAX(RCPT_NO) AS RCPT_NO" + "\n");
            query.Append("FROM     "+ txtPurchaseCatalog.Text + ".dbo.TR_SALE_D" + "\n");
            query.Append("GROUP BY CST_CD," + "\n");
            query.Append("         MNU_CD," + "\n");
            query.Append("         UNIT_NM" + "\n");
            query.Append("ORDER BY CST_CD," + "\n");
            query.Append("         MNU_CD" + "\n");
            query.Append("" + "\n");
            query.Append("--단가 데이터 입력" + "\n");
            query.Append("SELECT DISTINCT" + "\n");
            query.Append("                @CD_STORE AS CD_STORE," + "\n");
            query.Append("                TM.CST_CD AS CD_CUST," + "\n");
            query.Append("                TM.MNU_CD AS CD_ITEM," + "\n");
            query.Append("                TM.RCPT_NO," + "\n");
            query.Append("                'P'      AS FG_SLIP," + "\n");
            query.Append("                CASE WHEN D.UNIT_NM = M.UNIT_NM THEN UNIT_CD WHEN D.UNIT_NM = M.USEUNIT2_NM THEN USEUNIT2_CD WHEN D.UNIT_NM = M.USEUNIT3_NM THEN USEUNIT3_CD WHEN D.UNIT_NM = M.USEUNIT4_NM THEN USEUNIT4_CD END AS CD_UNIT," + "\n");
            query.Append("                D.APP_AMT                                                                                                                                                                                        AS UM" + "\n");
            query.Append("INTO            #TEMP2" + "\n");
            query.Append("FROM            "+ txtPurchaseCatalog.Text + ".dbo.TR_SALE_D D" + "\n");
            query.Append("LEFT OUTER JOIN "+ txtPurchaseCatalog.Text + ".dbo.MA_MENU M ON D.STORE_CD = M.STORE_CD AND             D.DP_CD = M.DP_CD AND             D.PC_CD = M.PC_CD AND             D.MNU_CD = M.MNU_CD AND             D.CST_CD = M.CST_CD" + "\n");
            query.Append("INNER JOIN      TMP_PURCHASE_D TM              ON D.CST_CD   = TM.CST_CD AND             D.MNU_CD = TM.MNU_CD AND             D.UNIT_NM = TM.UNIT_NM AND             D.SALE_DT = TM.SALE_DT AND             D.RCPT_NO = TM.RCPT_NO" + "\n");
            query.Append("WHERE           TM.MNU_CD                      IS NOT NULL" + "\n");
            query.Append("ORDER BY        TM.CST_CD," + "\n");
            query.Append("                TM.MNU_CD" + "\n");
            query.Append("" + "\n");
            query.Append("" + "\n");
            query.Append("--단가 조회" + "\n");
            query.Append("SELECT   CD_STORE ," + "\n");
            query.Append("         CD_CUST ," + "\n");
            query.Append("         CD_ITEM ," + "\n");
            query.Append("         FG_SLIP ," + "\n");
            query.Append("         CD_UNIT ," + "\n");
            query.Append("         MAX(UM) AS UM" + "\n");
            query.Append("FROM     #TEMP2" + "\n");
            query.Append("WHERE    CD_UNIT IS NOT NULL" + "\n");
            query.Append("GROUP BY CD_STORE ," + "\n");
            query.Append("         CD_CUST ," + "\n");
            query.Append("         CD_ITEM ," + "\n");
            query.Append("         FG_SLIP ," + "\n");
            query.Append("         CD_UNIT" + "\n");

            string ConnectionStringSale = "Data Source=" + txtPurchaseIP.Text + ";initial Catalog=" + txtPurchaseCatalog.Text + ";uid=" + txtPurchaseID.Text + ";password=" + txtPurchasePassword.Text + ";";
            conn.ConnectionString = ConnectionStringSale;
            try
            {
                if (ShowMessageBoxA("단가 가져오기는 몇분의 시간이 소요됩니다.\n진행하시겠습니까?", Bifrost.Common.MessageType.Question) == DialogResult.Yes)
                {
                    LoadData.StartLoading(this, "Progressing....", "데이터 가져오기 진행중입니다.");

                    conn.Open();


                    SqlCommand command = new SqlCommand();
                    command.CommandTimeout = 0;
                    command.CommandText = query.ToString();
                    command.Connection = conn;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet ds = new DataSet();
                    // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        aGridItemPricePurchase.Binding(ds.Tables[0], true);
                    }

                    conn.Close();

                    LoadData.EndLoading();
                    ShowMessageBoxA("단가 가져오기가 완료되었습니다.", Bifrost.Common.MessageType.Information);
                }
            }
            catch (Exception ex)
            {
                LoadData.EndLoading();
                HandleWinException(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void BtnMigrationItemPrice_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();

            StringBuilder query = new StringBuilder();

            query.Append("DECLARE @CD_STORE NVARCHAR(10)" + "\n");
            query.Append("SET @CD_STORE  = '"+ gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "'" + "\n");
            query.Append("" + "\n");
            query.Append("if exists(select * from information_schema.tables where table_name='TMP_SALE_D') " + "\n");
            query.Append("BEGIN  " + "\n");
            query.Append("     DROP TABLE TMP_SALE_D    " + "\n");
            query.Append("END            " + "\n"); 
            query.Append("" + "\n");

            query.Append("CREATE TABLE TMP_SALE_D(" + "\n");
            query.Append("    CST_CD varchar(4) NOT NULL," + "\n");
            query.Append("    MNU_CD varchar(15) NOT NULL," + "\n");
            query.Append("    UNIT_NM varchar(50) NOT NULL," + "\n");
            query.Append("    SALE_DT varchar(8) NOT NULL," + "\n");
            query.Append("    RCPT_NO varchar(15) NOT NULL," + "\n");
            query.Append(" CONSTRAINT PK_TMP_SALE_D PRIMARY KEY CLUSTERED" + "\n");
            query.Append("(" + "\n");
            query.Append("    CST_CD ASC," + "\n");
            query.Append("    MNU_CD ASC," + "\n");
            query.Append("    UNIT_NM ASC," + "\n");
            query.Append("    SALE_DT ASC," + "\n");
            query.Append("    RCPT_NO ASC" + "\n");
            query.Append(")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" + "\n");
            query.Append(") ON[PRIMARY]" + "\n");

            query.Append("--판매데이터 입력" + "\n");
            query.Append("INSERT INTO   TMP_SALE_D" + "\n");
            query.Append("SELECT   CST_CD," + "\n");
            query.Append("         MNU_CD," + "\n");
            query.Append("         UNIT_NM," + "\n");
            query.Append("         MAX(SALE_DT) AS SALE_DT," + "\n");
            query.Append("         MAX(RCPT_NO) AS RCPT_NO" + "\n");
            query.Append("FROM     "+ txtSaleCatalog.Text + ".dbo.TR_SALE_D" + "\n");
            query.Append("GROUP BY CST_CD," + "\n");
            query.Append("         MNU_CD," + "\n");
            query.Append("         UNIT_NM" + "\n");
            query.Append("ORDER BY CST_CD," + "\n");
            query.Append("         MNU_CD" + "\n");
            query.Append("" + "\n");
            query.Append("--단가 데이터 입력" + "\n");
            query.Append("SELECT DISTINCT" + "\n");
            query.Append("                @CD_STORE AS CD_STORE," + "\n");
            query.Append("                TM.CST_CD AS CD_CUST," + "\n");
            query.Append("                TM.MNU_CD AS CD_ITEM," + "\n");
            query.Append("                TM.RCPT_NO," + "\n");
            query.Append("                'S'      AS FG_SLIP," + "\n");
            query.Append("                CASE WHEN D.UNIT_NM = M.UNIT_NM THEN UNIT_CD WHEN D.UNIT_NM = M.USEUNIT2_NM THEN USEUNIT2_CD WHEN D.UNIT_NM = M.USEUNIT3_NM THEN USEUNIT3_CD WHEN D.UNIT_NM = M.USEUNIT4_NM THEN USEUNIT4_CD END AS CD_UNIT," + "\n");
            query.Append("                D.APP_AMT                                                                                                                                                                                        AS UM" + "\n");
            query.Append("INTO            #TEMP2" + "\n");
            query.Append("FROM            "+ txtSaleCatalog.Text + ".dbo.TR_SALE_D D" + "\n");
            query.Append("LEFT OUTER JOIN "+ txtSaleCatalog.Text + ".dbo.MA_MENU M ON D.STORE_CD = M.STORE_CD AND             D.DP_CD = M.DP_CD AND             D.PC_CD = M.PC_CD AND             D.MNU_CD = M.MNU_CD AND             D.CST_CD = M.CST_CD" + "\n");
            query.Append("INNER JOIN      TMP_SALE_D TM              ON D.CST_CD   = TM.CST_CD AND             D.MNU_CD = TM.MNU_CD AND             D.UNIT_NM = TM.UNIT_NM AND             D.SALE_DT = TM.SALE_DT AND             D.RCPT_NO = TM.RCPT_NO" + "\n");
            query.Append("WHERE           TM.MNU_CD                      IS NOT NULL" + "\n");
            query.Append("ORDER BY        TM.CST_CD," + "\n");
            query.Append("                TM.MNU_CD" + "\n");
            query.Append("" + "\n");
            query.Append("" + "\n");
            query.Append("--단가 조회" + "\n");
            query.Append("SELECT   CD_STORE ," + "\n");
            query.Append("         CD_CUST ," + "\n");
            query.Append("         CD_ITEM ," + "\n");
            query.Append("         FG_SLIP ," + "\n");
            query.Append("         CD_UNIT ," + "\n");
            query.Append("         MAX(UM) AS UM"  + "\n");
            query.Append("FROM     #TEMP2" + "\n");
            query.Append("WHERE    CD_UNIT IS NOT NULL" + "\n");
            query.Append("GROUP BY CD_STORE ," + "\n");
            query.Append("         CD_CUST ," + "\n");
            query.Append("         CD_ITEM ," + "\n");
            query.Append("         FG_SLIP ," + "\n");
            query.Append("         CD_UNIT" + "\n");

            string ConnectionStringSale = "Data Source=" + txtSaleIP.Text + ";initial Catalog=" + txtSaleCatalog.Text + ";uid=" + txtSaleID.Text + ";password=" + txtSalePassword.Text + ";Connection Timeout=0";
            conn.ConnectionString = ConnectionStringSale;
            try
            {
                if (ShowMessageBoxA("단가 가져오기는 몇분의 시간이 소요됩니다.\n진행하시겠습니까?", Bifrost.Common.MessageType.Question) == DialogResult.Yes)
                {
                    LoadData.StartLoading(this, "Progressing....", "데이터 가져오기 진행중입니다.");

                    conn.Open();


                    SqlCommand command = new SqlCommand();
                    command.CommandTimeout = 0;
                    command.CommandText = query.ToString();
                    command.Connection = conn;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet ds = new DataSet();
                    // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        aGridItemPriceSale.Binding(ds.Tables[0], true);
                    }

                    conn.Close();

                    LoadData.EndLoading();
                    ShowMessageBoxA("단가 가져오기가 완료되었습니다.", Bifrost.Common.MessageType.Information);
                }
            }
            catch (Exception ex) 
            {
                LoadData.EndLoading();
                HandleWinException(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }


        }

        private void GridViewMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtStoreCode.EditValue = gridViewMain.GetFocusedRowCellValue("CD_STORE");
            txtStoreName.EditValue = gridViewMain.GetFocusedRowCellValue("NM_STORE");

            txtOldStoreCode.EditValue = gridViewMain.GetFocusedRowCellValue("CD_STORE");
            txtOldStoreName.EditValue = gridViewMain.GetFocusedRowCellValue("NM_STORE");
        }

        private void BtnMigration_Click(object sender, EventArgs e)
        {
            //if (gridView1.RowCount == 0)
            //{
            //    ShowMessageBoxA("먼저 데이터를 가져오세요", Bifrost.Common.MessageType.Warning);
            //    return;
            //}

            //SetCustomerCod();
            OnSave();
        }

        private DataTable xtraGridToDatatable(aGrid grid)
        {
            DataView dv = grid.MainView.DataSource as DataView;

            DataTable dt = null; 

            if (dv != null) 
                dt = dv.ToTable();

            ChangeState(ref dt);
            return dt;
        }

        private void ChangeState(ref DataTable dt)
        {
            if(dt == null) return;
            dt.AcceptChanges();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i].SetAdded();
            }
        }


        private void BtnGetDataSale_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection();

            StringBuilder query = new StringBuilder();
           
            query.Append("SELECT" + "\n");
            query.Append("'" + gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "' AS CD_STORE\n");
            query.Append(",STORE_NM AS NM_STORE" + "\n");
            query.Append(",SUB_NM AS NM_CEO" + "\n");
            query.Append(",BUSI_NO AS NO_BIZ" + "\n");
            query.Append(",BUS_COND AS DC_BIZ1" + "\n");
            query.Append(",BUS_ITEM AS DC_BIZ2" + "\n");
            query.Append(",null AS NO_POST" + "\n");
            query.Append(",ADDR AS DC_ADDR1" + "\n");
            query.Append(",null AS DC_ADDR2" + "\n");
            query.Append(",TEL AS NO_TEL1" + "\n");
            query.Append(",null AS NO_TEL2" + "\n");
            query.Append(",FAX AS NO_FAX1" + "\n");
            query.Append(",null AS NO_FAX2" + "\n");
            query.Append(",null AS DC_HOMPAGE" + "\n");
            query.Append(",MSG_1 AS NO_ACCOUNT1" + "\n");
            query.Append(",MSG_2 AS NO_ACCOUNT2" + "\n");
            query.Append(",'Y' AS YN_USE" + "\n");
            query.Append(",'' AS CD_FIRM" + "\n");
            query.Append(",STORE_CD AS DC_RMK" + "\n");
            query.Append("FROM "+ txtSaleCatalog.Text + ".dbo.MA_STORE" + "\n");
            query.Append("");
            query.Append("-------------------------------- - 거래처" + "\n");
            query.Append("SELECT" + "\n");
            query.Append("'" + gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "' AS CD_STORE\n");
            query.Append(",CUST_CD AS CD_CUST" + "\n");
            query.Append(",CUST_NM AS NM_CUST" + "\n");
            query.Append(",SUB_NM AS NM_CEO" + "\n");
            query.Append(",BUSI_NO AS NO_BIZ" + "\n");
            query.Append(",'2' AS FG_CUST" + "\n");
            query.Append(",null AS TP_CUST_L" + "\n");
            query.Append(",null AS TP_CUST_M" + "\n");
            query.Append(",null AS TP_CUST_S" + "\n");
            query.Append(",TEL AS NO_TEL1" + "\n");    
            query.Append(",SUB_HP AS NO_TEL2" + "\n");
            query.Append(",EMP_HP AS NO_HP" + "\n");
            query.Append(",TELFX AS NO_FAX1" + "\n");
            query.Append(",null AS NO_FAX2" + "\n");
            query.Append(",CAR_NO AS NO_CAR" + "\n");
            query.Append(",TOT_AMT AS AM_NONPAID" + "\n");
            query.Append(",0 AS AM_NONPAID_PO" + "\n");
            query.Append(",DISP_PNT AS NO_SORT" + "\n");
            query.Append(",USE_FLG AS YN_USE" + "\n");
            query.Append(",BIGO AS DC_RMK" + "\n");
            query.Append("FROM "+ txtSaleCatalog.Text + ".dbo.MA_CUSTOM" + "\n");
            query.Append("" + "\n");
            query.Append("------------------------ - 품목" + "\n");
            query.Append("SELECT" + "\n");
            query.Append("'" + gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "' AS CD_STORE\n");
            query.Append(", MNU_CD AS CD_ITEM" + "\n");
            query.Append(", MNU_NM AS NM_ITEM" + "\n");
            query.Append(", DISP_NM AS DC_ITEM" + "\n");
            query.Append(",null AS FG_ITEM" + "\n");
            query.Append(",'16' AS FG_VAT" + "\n");
            query.Append(",0 AS UM_COST_PO" + "\n");
            query.Append(",0 AS UM_COST_SA" + "\n");
            query.Append(", GMNU_CD AS TP_ITEM_L" + "\n");
            query.Append(", SUBSTRING(MMNU_CD, 4, 3) AS TP_ITEM_M" + "\n");
            query.Append(", SUBSTRING(SMNU_CD, 7, 3) AS TP_ITEM_S" + "\n");
            query.Append(", USE_FLG AS YN_USE" + "\n");
            query.Append(",'N' AS YN_INV" + "\n");
            query.Append(", DISP_PNT AS NO_SORT" + "\n");
            query.Append(",null as DC_RMK" + "\n");
            query.Append("FROM "+ txtSaleCatalog.Text + ".dbo.MA_MENU" + "\n");
            query.Append("where CST_CD = '000'" + "\n");
            query.Append("" + "\n");
            query.Append("" + "\n");
            query.Append("-------------------------------------- - 품목 단위" + "\n");
            query.Append("SELECT DISTINCT" + "\n");
            query.Append("'" + gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "' AS CD_STORE\n");
            query.Append(",MNU_CD AS CD_ITEM" + "\n");
            query.Append(",CD_UNIT" + "\n");
            query.Append(",ROW_NUMBER () OVER(PARTITION BY MNU_CD ORDER BY MNU_CD) AS NO_SORT" + "\n");
            query.Append(",0 AS QT_UNIT" + "\n");
            query.Append(",'N' AS YN_MAIN" + "\n");
            query.Append(",'Y' AS YN_USE" + "\n");
            query.Append("FROM" + "\n");
            query.Append("(SELECT MNU_CD" + "\n");
            query.Append(", UNIT_CD" + "\n");
            query.Append(", USEUNIT2_CD" + "\n");
            query.Append(", USEUNIT3_CD" + "\n");
            query.Append(", USEUNIT4_CD" + "\n");
            query.Append("FROM "+ txtSaleCatalog.Text + ".dbo.MA_MENU" + "\n");
            query.Append("WHERE CST_CD = '000'" + "\n");
            query.Append(") p" + "\n");
            query.Append("UNPIVOT(CD_UNIT FOR CD_ITEM IN(UNIT_CD, USEUNIT2_CD, USEUNIT3_CD, USEUNIT4_CD))AS unpvt" + "\n");
            query.Append("" + "\n");
            query.Append("---------------------------------------단위" + "\n");
            query.Append("select" + "\n");
            query.Append("'" + gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "' AS CD_STORE\n");
            query.Append(",'POS101' AS CD_CLAS" + "\n");
            query.Append(",UNIT_CD AS CD_FLAG" + "\n");
            query.Append(",USE_UNIT AS NM_FLAG" + "\n");
            query.Append(",'Y' AS YN_SYSTEM" + "\n");
            query.Append(",USE_FLG AS YN_USE" + "\n");
            query.Append("from "+ txtSaleCatalog.Text + ".dbo.MA_USEUNIT" + "\n");
            query.Append("" + "\n");
            query.Append("------------------------------------------대분류" + "\n");
            query.Append("select" + "\n");
            query.Append("'" + gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "' AS CD_STORE\n"); 
            query.Append(",'POS102' AS CD_CLAS" + "\n");
            query.Append(",GMNU_CD AS CD_FLAG" + "\n");
            query.Append(",GMNU_NM AS NM_FLAG" + "\n");
            query.Append(",'Y' AS YN_SYSTEM" + "\n");
            query.Append(",USE_FLG AS YN_USE" + "\n");
            query.Append("from "+ txtSaleCatalog.Text + ".dbo.MA_GMENU" + "\n");

            string ConnectionStringSale = "Data Source=" + txtSaleIP.Text + ";initial Catalog=" + txtSaleCatalog.Text + ";uid=" + txtSaleID.Text + ";password=" + txtSalePassword.Text + ";";
            conn.ConnectionString = ConnectionStringSale;
            try
            {

                conn.Open();       // conn을 열고 밑에 문자열을 출력한다.

                // SqlDataAdapter 초기화
                SqlCommand cmd = new SqlCommand("exec sp_dbcmptlevel '"+ txtSaleCatalog.Text + "',100", conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                conn.Close();



                conn.Open();       // conn을 열고 밑에 문자열을 출력한다.

                // SqlDataAdapter 초기화
                SqlDataAdapter adapter = new SqlDataAdapter(query.ToString(), conn);

                DataSet ds = new DataSet();
                // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    aGridStore.Binding(ds.Tables[0], true);
                    aGridCustomerSale.Binding(ds.Tables[1], true);
                    aGridItem.Binding(ds.Tables[2], true);
                    aGridItemUnit.Binding(ds.Tables[3], true);
                    aGridUnitCode.Binding(ds.Tables[4], true);
                    aGridItemType.Binding(ds.Tables[5], true);
                }

                conn.Close();

                Console.WriteLine("데이터베이스 연결 성공...");
            }
            catch
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

        private void BtnGetDataPurchase_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();

            StringBuilder query = new StringBuilder();

            query.Append("-------------------------------- - 거래처" + "\n");
            query.Append("SELECT" + "\n");
            query.Append("'" + gridViewMain.GetFocusedRowCellValue("CD_STORE").ToString() + "' AS CD_STORE\n");
            query.Append(",CUST_CD AS CD_CUST" + "\n");
            query.Append(",CUST_NM AS NM_CUST" + "\n");
            query.Append(",SUB_NM AS NM_CEO" + "\n");
            query.Append(",BUSI_NO AS NO_BIZ" + "\n");
            query.Append(",'1' AS FG_CUST" + "\n");
            query.Append(",null AS TP_CUST_L" + "\n");
            query.Append(",null AS TP_CUST_M" + "\n");
            query.Append(",null AS TP_CUST_S" + "\n");
            query.Append(",TEL AS NO_TEL1" + "\n");
            query.Append(",SUB_HP AS NO_TEL2" + "\n");
            query.Append(",EMP_HP AS NO_HP" + "\n");
            query.Append(",TELFX AS NO_FAX1" + "\n");
            query.Append(",null AS NO_FAX2" + "\n");
            query.Append(",CAR_NO AS NO_CAR" + "\n");
            query.Append(",0 AS AM_NONPAID" + "\n");
            query.Append(",TOT_AMT AS AM_NONPAID_PO" + "\n");
            query.Append(",DISP_PNT AS NO_SORT" + "\n");
            query.Append(",USE_FLG AS YN_USE" + "\n");
            query.Append(",BIGO AS DC_RMK" + "\n");
            query.Append("FROM " + txtPurchaseCatalog.Text + ".dbo.MA_CUSTOM" + "\n");

            string ConnectionStringSale = "Data Source=" + txtPurchaseIP.Text + ";initial Catalog=" + txtPurchaseCatalog.Text + ";uid=" + txtPurchaseID.Text + ";password=" + txtPurchasePassword.Text + ";";
            conn.ConnectionString = ConnectionStringSale;
            try
            {

                conn.Open();       // conn을 열고 밑에 문자열을 출력한다.

                // SqlDataAdapter 초기화
                SqlCommand cmd = new SqlCommand("exec sp_dbcmptlevel '" + txtSaleCatalog.Text + "',100", conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                conn.Close();

                conn.Open();       // conn을 열고 밑에 문자열을 출력한다.

                // SqlDataAdapter 초기화
                SqlDataAdapter adapter = new SqlDataAdapter(query.ToString(), conn);

                DataSet ds = new DataSet();
                // Fill 메서드 실행하여 결과 DataSet을 리턴받음
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    aGridCustomerPurchase.Binding(ds.Tables[0], true);
                }

                conn.Close();

                Console.WriteLine("데이터베이스 연결 성공...");
            }
            catch
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

        private void M_MAS_STORE_MANAGE_Load(object sender, EventArgs e)
        {
            
            OnView();
        }

        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            gridViewMain.AddNewRow();
            gridViewMain.UpdateCurrentRow();

        }

        private void InitControl()
        {
            SetControl scr = new SetControl();

            scr.SetCombobox(aLookUpEditSearchArea, CH.GetCode("MAS100", true));
            scr.SetCombobox(aLookUpEditSearchBizItem, CH.GetCode("MAS101", true));

            //aGridStore.SetBinding(panelMain, gridViewStore, new object[] { txtStoreCode });
        }

        public override void OnView()
        {
            aGridMain.Binding(Search(new object[] { aLookUpEditSearchArea.EditValue, aLookUpEditSearchBizItem.EditValue, txtSearch.Text }), true);
        }

        private void SetCustomerCode()
        {

            if (gridView7.RowCount == 0)
            {
                return;
            }

            int CustCnt = A.GetInt(gridView6.GetRowCellValue(gridView6.RowCount - 1, "CD_CUST"));

            bool CustPublic = false;

            if (gridView7.GetRowCellValue(0, "CD_CUST").ToString() == "0000" && gridView7.GetRowCellValue(0, "NM_CUST").ToString() == "일반")
            {
                CustPublic = true;
                gridView7.DeleteRow(0);
            }

            gridView7.BeginUpdate();
            for (int i = 0; i < gridView7.RowCount; i++)
            {
                gridView7.SetRowCellValue(i, "CD_CUST", (CustCnt + A.GetDecimal(gridView7.GetRowCellValue(i, "CD_CUST")) + 1).ToString().PadLeft(4, '0'));
            }
            gridView7.EndUpdate();

            gridView9.BeginUpdate();
            for (int i = 0; i < gridView9.RowCount; i++)
            {
                if (!(gridView9.GetRowCellValue(i, "CD_CUST").ToString() == "0000" && CustPublic))
                {
                    gridView9.SetRowCellValue(i, "CD_CUST", (CustCnt + A.GetDecimal(gridView9.GetRowCellValue(i, "CD_CUST")) + 1).ToString().PadLeft(4, '0'));
                }
            }
            gridView9.EndUpdate();

        }

        public override void OnSave()
        {
            string storeCode = txtStoreCode.Text;

            if (ShowMessageBoxA(storeCode + "로 마이그레이선 하시겠습니까?", Bifrost.Common.MessageType.Question) == DialogResult.Yes)
            {


                int SlipCount = A.GetInt(DBHelper.ExecuteScalar("SELECT COUNT(NO_SO) AS CNT FROM POS_SOH WHERE CD_STORE = '" + storeCode + "'"));
                if (SlipCount > 0)
                {
                    ShowMessageBoxA("이미 거래가 존재하여 마이그래이션을 할 수 없습니다.\n다른 매장코드를 선택하거나 신규생성 하세요.", Bifrost.Common.MessageType.Warning);
                    return;
                }
                else
                {
                    try
                    {
                        LoadData.StartLoading(this, "Progressing....", "마이그레이션 진행중입니다.");

                        DataTable dtChangeStore = xtraGridToDatatable(aGridStore);
                        DataTable dtChangeItem = xtraGridToDatatable(aGridItem);
                        DataTable dtChangeItemUnit = xtraGridToDatatable(aGridItemUnit);
                        DataTable dtChangeUnitCode = xtraGridToDatatable(aGridUnitCode);
                        DataTable dtChangeItemType = xtraGridToDatatable(aGridItemType);
                        DataTable dtChangeCustomerSale = xtraGridToDatatable(aGridCustomerSale);
                        DataTable dtChangeCustomerPurChase = xtraGridToDatatable(aGridCustomerPurchase);
                        DataTable dtChangeItemPriceSale = xtraGridToDatatable(aGridItemPriceSale);
                        DataTable dtChangeItemPricePurchase = xtraGridToDatatable(aGridItemPricePurchase);

                        SaveAll(dtChangeStore, dtChangeItem, dtChangeItemUnit, dtChangeUnitCode, dtChangeItemType, dtChangeCustomerSale, dtChangeCustomerPurChase, dtChangeItemPriceSale, dtChangeItemPricePurchase, storeCode);

                        //BeforeSaveCode(storeCode);

                        //SaveStore(dtChangeStore);
                        //SaveItem(dtChangeItem);
                        //SaveItemUnit(dtChangeItemUnit);
                        //SaveCode(dtChangeUnitCode);
                        //SaveCode(dtChangeItemType);
                        //SaveCustomer(dtChangeCustomerSale);
                        //SaveCustomer(dtChangeCustomerPurChase);

                        //AfterSaveItemUnit(storeCode);

                        LoadData.EndLoading();

                        ShowMessageBoxA("기초정보 마이그레이션이 완료되었습니다.", Bifrost.Common.MessageType.Information);

                    }
                    catch (Exception ex)
                    {
                        LoadData.EndLoading();
                        HandleWinException(ex);
                    }
                    finally
                    {
                    }
                }
            }


        }

    }
}
