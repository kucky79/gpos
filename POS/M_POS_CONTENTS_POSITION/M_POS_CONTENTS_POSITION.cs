using Bifrost;
using Bifrost.Common;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_CONTENTS_POSITION : POSFormBase
    {

        DevExpress.XtraEditors.CheckButton[] _contentsBtn = new DevExpress.XtraEditors.CheckButton[40];

        int _currentPage = 1;

        int CurrentPage
        {
            get
            {
                //double tmp = (double)Position / (double)_screenQty;
                //_currentPage = int.Parse(Math.Ceiling(tmp).ToString());

                return _currentPage == 0 ? 1 : _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }


        int _maxPage = 1;
        string _tmpResult = string.Empty; //키패드 숫자를 관리하기위한 변수

        DataTable _dt;
        DataTable _dtType;


        const int _screenQty = 40; //화면에 보이는 버튼 개수

        public enum ContentsMode
        {
            Customer,
            Item
        }

        private ContentsMode _contentsType = ContentsMode.Item;

        public ContentsMode ContentsType
        {
            get { return _contentsType; }
            set
            {
                _contentsType = value;
                if (value == ContentsMode.Customer)
                {
                    rboContents.SelectedIndex = 0;
                }
                else
                {
                    rboContents.SelectedIndex = 1;
                }

            }

        }
        public M_POS_CONTENTS_POSITION()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        private void InitEvent()
        {
            btnPre.Click += BtnPre_Click;
            btnNext.Click += BtnNext_Click;
            btnTop.Click += BtnTop_Click;

            btnLeft.Click += BtnLeft_Click;
            btnRight.Click += BtnRight_Click;
            btnUp.Click += BtnUp_Click;
            btnDown.Click += BtnDown_Click;

            btnChangeSort.Click += BtnChangeSort_Click;
            btnResetAtoZ.Click += BtnReset_Click;
            btnResetCode.Click += BtnReset_Click;
            btnInit.Click += BtnInit_Click;

            gridView1.FocusedRowObjectChanged += GridView1_FocusedRowObjectChanged;//;+= GridView1_FocusedRowChanged;

            rboContents.SelectedIndexChanged += RboContents_SelectedIndexChanged;
            rboCustmerType.SelectedIndexChanged += RboCustmerType_SelectedIndexChanged;
        }

        private void BtnInit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "NO_SORT", i + 1);
            }        
        }


        private void RboCustmerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnView();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            DataColumn[] dtkey = new DataColumn[1];

            string SortType = ((SimpleButton)sender).Tag.ToString();

            switch (rboContents.SelectedIndex)
            {
                case 0: //거래처구분
                    break;
                case 1: //거래처
                    _dt = ItemReposition(new object[] { POSGlobal.StoreCode, SortType }, rboContents.SelectedIndex);
                    dtkey[0] = _dt.Columns["CD_CUST"];
                    _dt.PrimaryKey = dtkey;
                    SetResetData();
                    break;
                case 2: //상품 대분류
                    _dt = ItemReposition(new object[] { POSGlobal.StoreCode, SortType }, rboContents.SelectedIndex);
                    dtkey[0] = _dt.Columns["CD_ITEM"];
                    _dt.PrimaryKey = dtkey;
                    SetResetData();
                    break;
                case 3: //상품 
                    if (ContentsLv == 1)
                    {
                        _dt = ItemReposition(new object[] { POSGlobal.StoreCode, ItemCode, SortType }, rboContents.SelectedIndex);
                        dtkey[0] = _dt.Columns["CD_ITEM"];
                        _dt.PrimaryKey = dtkey;
                        SetResetData();
                        ContentsLv = 1;
                    }
                    break;
                default:
                    break;
            }
        }

        private void SetResetData()
        {
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                _dt.Rows[i].SetModified();
            }
            gridMain.Binding(_dt, true);
            CurrentPage = 1;

            //패널에 데이터 넣기
            SetContents(_dt);
            SetNaviButton(_dt);
        }


        private void RboContents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rboContents.SelectedIndex == 1) //거래처일경우
                rboCustmerType.Visible = true;
            else
                rboCustmerType.Visible = false;

            OnView();
        }

        private void GridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            Position = gridView1.FocusedRowHandle;
            SelectButton(Position);
        }

        public override void OnSave()
        {
            try
            {
                DataTable dtChanges = gridMain.GetChanges();

                if (dtChanges == null)
                {
                    ShowMessageBoxA("변경된 내용이 존재하지 않습니다.", MessageType.Information);
                    return;
                }

                bool Result = false;
                     
                switch (rboContents.SelectedIndex)
                {
                    case 0:
                        Result = Save(dtChanges, null, null, null);
                        break;
                    case 1:
                        Result = Save(null, dtChanges, null, null);
                        break;
                    case 2:
                        Result = Save(null, null, dtChanges, null);
                        break;
                    case 3:
                        Result = Save(null, null, null, dtChanges);
                        break;
                }

              

                if (!Result)
                {
                    ShowMessageBoxA("저장이 실패하였습니다.", MessageType.Error);
                    return;
                }

                ShowMessageBoxA("저장이 완료되었습니다.", MessageType.Information);
                gridMain.AcceptChanges();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Position = gridView1.FocusedRowHandle;
            SelectButton(Position);
        }

        private void BtnChangeSort_Click(object sender, EventArgs e)
        {
            P_SORT P_SORT = new P_SORT();
            if (P_SORT.ShowDialog() == DialogResult.OK)
            {
                int orgSrot = A.GetInt(gridView1.GetFocusedRowCellValue("NO_SORT"));
                int chgSort = P_SORT.ResultQty;
                if (chgSort == 0) return;

                int moveQty = chgSort - orgSrot;
                MoveContents(moveQty);

                if (P_SORT != null)
                {
                    P_SORT.Dispose();
                }
            }
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {

            gridView1.FocusedRowObjectChanged -= GridView1_FocusedRowObjectChanged;
            //gridView1.FocusedRowChanged -= GridView1_FocusedRowChanged;
            MoveContents(4);
            //gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            gridView1.FocusedRowObjectChanged += GridView1_FocusedRowObjectChanged;
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            gridView1.FocusedRowObjectChanged -= GridView1_FocusedRowObjectChanged;
            //gridView1.FocusedRowChanged -= GridView1_FocusedRowChanged;
            MoveContents(-4);
            //gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            gridView1.FocusedRowObjectChanged += GridView1_FocusedRowObjectChanged;

        }

        private void BtnRight_Click(object sender, EventArgs e)
        {
            gridView1.FocusedRowObjectChanged -= GridView1_FocusedRowObjectChanged;
            //gridView1.FocusedRowChanged -= GridView1_FocusedRowChanged;
            MoveContents(1);
            //gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            gridView1.FocusedRowObjectChanged += GridView1_FocusedRowObjectChanged;

        }

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            gridView1.FocusedRowObjectChanged -= GridView1_FocusedRowObjectChanged;
            //gridView1.FocusedRowChanged -= GridView1_FocusedRowChanged;
            MoveContents(-1);
            //gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            gridView1.FocusedRowObjectChanged += GridView1_FocusedRowObjectChanged;

        }

        private void MoveContents(int cnt)
        {
            if(_code == string.Empty)
            {
                ShowMessageBoxA("선택한 항목이 존재하지 않습니다.", Bifrost.Common.MessageType.Warning);
                return;
            }
            gridView1.FocusedRowObjectChanged -= GridView1_FocusedRowObjectChanged;
            //gridView1.FocusedRowChanged -= GridView1_FocusedRowChanged;


            int tmpPosition = cnt + Position;

            DataTableMoveRow tmpDirection = cnt > 0 ? DataTableMoveRow.Down : DataTableMoveRow.Up;
            if (cnt < 0) cnt = cnt * -1;

            for (int i = 0; i < cnt; i++)
            {
                DataRow dr = _dt.Rows.Find(_code);
                MoveRow(_dt, dr, tmpDirection);
            }

            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                _dt.Rows[i]["NO_SORT"] = i+1;
            }

            SetContents(_dt);
            if(Position / _screenQty < tmpPosition / _screenQty)
            {
                CurrentPage += 1;
            }
            else if (Position / _screenQty > tmpPosition / _screenQty)
            {
                CurrentPage -= 1;
            }

            SetContentsPreNext();
            SetNaviButton(_dt);

            Position = _dt.Rows.IndexOf(_dt.Rows.Find(_code));
            CurrentPage = (Position / _screenQty) + 1;

            SelectButton(Position);
            //gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            gridView1.FocusedRowObjectChanged += GridView1_FocusedRowObjectChanged;

        }

        private int MoveRow(DataTable dt, DataRow row, DataTableMoveRow direction)
        {
            DataRow oldRow = row;
            DataRow newRow = dt.NewRow();

            newRow.ItemArray = oldRow.ItemArray;

            int oldRowIndex = dt.Rows.IndexOf(row);

            if (direction == DataTableMoveRow.Down)
            {
                int newRowIndex = oldRowIndex + 1;

                if (oldRowIndex < (dt.Rows.Count))
                {
                    dt.Rows.Remove(oldRow);
                    dt.Rows.InsertAt(newRow, newRowIndex);
                    return dt.Rows.IndexOf(newRow);
                }
            }

            if (direction == DataTableMoveRow.Up)
            {
                int newRowIndex = oldRowIndex - 1;

                if (oldRowIndex > 0)
                {
                    dt.Rows.Remove(oldRow);
                    dt.Rows.InsertAt(newRow, newRowIndex);
                    return dt.Rows.IndexOf(newRow);
                }
            }

            return 0;
        }

        public enum DataTableMoveRow
        {
            Up,
            Down
        }

        public override void OnView()
        {
            DataColumn[] dtkey = new DataColumn[1];
            ItemCode = string.Empty;
            DataSet ds;
            //LoadData.StartLoading(this);
            switch (rboContents.SelectedIndex)
            {
                case 0:

                break;

                case 1:
                    _dt = DBHelper.GetDataTable("USP_POS_CUST_S", new object[] { POSGlobal.StoreCode, string.Empty, "Y", string.Empty, rboCustmerType.EditValue });
                    gridColumn1.Caption = "거래처코드";
                    gridColumn1.FieldName = "CD_CUST";

                    gridColumn2.Caption = "거래처명";
                    gridColumn2.FieldName = "NM_CUST";

                    dtkey[0] = _dt.Columns["CD_CUST"];
                    _dt.PrimaryKey = dtkey;

                    gridMain.Binding(_dt, true);
                    CurrentPage = 1;
                    //패널에 데이터 넣기
                    SetContents(_dt);
                    SetNaviButton(_dt);
                    break;
                case 2:
                    ds = SearchItem(new object[] { POSGlobal.StoreCode });

                    _dt = ds.Tables[0];
                    gridColumn1.Caption = "상품코드";
                    gridColumn1.FieldName = "CD_ITEM";

                    gridColumn2.Caption = "상품명";
                    gridColumn2.FieldName = "NM_ITEM";

                    dtkey[0] = _dt.Columns["CD_ITEM"];
                    _dt.PrimaryKey = dtkey;

                    gridMain.Binding(_dt, true);
                    CurrentPage = 1;
                    //패널에 데이터 넣기
                    SetContents(_dt);
                    SetNaviButton(_dt);
                    break;
                case 3:
                    ds = SearchItem(new object[] { POSGlobal.StoreCode });

                    _dt = ds.Tables[0].Rows.Count == 0 ? ds.Tables[1] : ds.Tables[0];

                    gridColumn1.Caption = "상품코드";
                    gridColumn1.FieldName = "CD_ITEM";

                    gridColumn2.Caption = "상품명";
                    gridColumn2.FieldName = "NM_ITEM";

                    dtkey[0] = _dt.Columns["CD_ITEM"];
                    _dt.PrimaryKey = dtkey;

                    gridMain.Binding(_dt, true);
                    CurrentPage = 1;
                    ContentsLv = ds.Tables[0].Rows.Count == 0 ? 1 : 0;
                    //패널에 데이터 넣기
                    SetContents(_dt);
                    SetNaviButton(_dt);
                    break;
            }
            //LoadData.EndLoading();
               
            if (gridMain.MainView.RowCount > 0)
                SelectButton(0);
        }


        private void InitForm()
        {
            rboContents.SelectedIndex = 0;
            ContentsType = ContentsMode.Item;

            flowLayoutPanelContents.SuspendLayout();
            //콘텐츠 버튼 생성 (40개) 픽스
            for (int i = 0; i < _screenQty; i++)
            {
                _contentsBtn[i] = new DevExpress.XtraEditors.CheckButton();
                _contentsBtn[i].Name = "btnContents" + i.ToString();
                //_contentsBtn[i].AllowAllUnchecked = true;
                _contentsBtn[i].Size = new System.Drawing.Size(145, 70);
                _contentsBtn[i].TabIndex = i;
                _contentsBtn[i].Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                _contentsBtn[i].Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
                //_contentsBtn[i].AllowFocus = true;
                _contentsBtn[i].GroupIndex = 1;
                _contentsBtn[i].Click += ContentsBtn_Click;
                _contentsBtn[i].CheckedChanged += ContentsBtn_CheckedChanged;
                
                flowLayoutPanelContents.Controls.Add(_contentsBtn[i]);
            }
            flowLayoutPanelContents.ResumeLayout();

            CH.SetButtonApperanceDefault(btnLeft);
            CH.SetButtonApperanceDefault(btnRight);
            CH.SetButtonApperanceDefault(btnUp);
            CH.SetButtonApperanceDefault(btnDown);

            if (rboContents.SelectedIndex == 1) //거래처일경우
                rboCustmerType.Visible = true;
            else
                rboCustmerType.Visible = false;

            //거래처, 품목 데이터 가져오기
            OnView();
        }

        private void ContentsBtn_CheckedChanged(object sender, EventArgs e)
        {

            CheckButton btn = sender as CheckButton;

            Color tmp1 = btn.Appearance.BackColor;
            Color tmp2 = btn.Appearance.BackColor2;

            if (btn.Checked)
            {
                btn.Appearance.BackColor = Color.LightGreen;
                btn.Appearance.BackColor2 = Color.DarkGreen;
            }
            else
            {
                btn.Appearance.BackColor = Color.White;
                btn.Appearance.BackColor2 = Color.White;
            }
        }

        private string ItemCode { get; set; } = string.Empty;

        private void ContentsBtn_Click(object sender, EventArgs e)
        {
            int position = A.GetInt(((CheckButton)sender).Name.Replace("btnContents", "")) + ((CurrentPage - 1) * 40);
            //SelectButton(position);

            string _selectedCode = ((DevExpress.XtraEditors.CheckButton)sender).Tag.ToString();
            string _selectedName = ((DevExpress.XtraEditors.CheckButton)sender).Text;
            DataColumn[] dtkey = new DataColumn[1];

            if (rboContents.SelectedIndex == 3 && ContentsLv != 1)
            {
                ItemCode = _selectedCode;
                _dt = DBHelper.GetDataTable("USP_POS_ITEM_S", new object[] { POSGlobal.StoreCode, string.Empty, "L", _selectedCode });
                dtkey[0] = _dt.Columns["CD_ITEM"];
                _dt.PrimaryKey = dtkey;
                SetContents(_dt);
                SetNaviButton(_dt);

                gridMain.Binding(_dt, true);
                
                ContentsLv = 1; //컨텐츠 레벨이 0이 아닐때 상위 버튼 활성화 시킴

                //무조건 처음 아이템을 찍어줌
                BeginInvoke(new Action(() => { _contentsBtn[0].Checked = true; }));

                return;
            }

            UnCheckAll();
            SelectButton(position);
        }

        private int _contentsLv;

        public int ContentsLv
        {
            get
            {
                return _contentsLv;
            }
            set
            {
                _contentsLv = value;
                if (_contentsLv != 0)
                {
                    btnTop.Enabled = true;
                }
                else
                {
                    btnTop.Enabled = false;
                }
            }
        }


        int _position = 0;
        int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;

                double tmp = (double)_position / (double)_screenQty;
                CurrentPage = int.Parse(Math.Ceiling(tmp == 1 ? 2 : tmp).ToString());
            }
        }

        string _code = string.Empty;
        string _name = string.Empty;

        private void UnCheckAll()
        {
            for (int i = 0; i < _screenQty; i++)
            {
                _contentsBtn[i].Checked = false;
            }
        }

        private void SelectButton(int position)
        {

            CurrentPage = (position / 40) + 1;
            //gridView1.FocusedRowChanged -= GridView1_FocusedRowChanged;
            SetContentsPreNext();

            _contentsBtn[position % _screenQty].Checked = true;

            //Position = position % _screenQty;
            _code = A.GetString(_contentsBtn[position % _screenQty].Tag);
            _name = A.GetString(_contentsBtn[position % _screenQty].Text);

            gridView1.FocusedRowHandle = position;
            //gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;

        }

        private void SetContents(DataTable dt)
        {
            flowLayoutPanelContents.Hide();


            //초기화
            for (int j = 0; j < 40; j++)
            {
                _contentsBtn[j].Text = string.Empty;
                _contentsBtn[j].Tag = string.Empty;
                _contentsBtn[j].Enabled = false;

                _contentsBtn[j].Checked = false;
                _contentsBtn[j].Appearance.BackColor = Color.White;
                _contentsBtn[j].Appearance.BackColor2 = Color.White;

                //_contentsBtn[j].Appearance.BackColor = Color.LightGreen;
                //_contentsBtn[j].Appearance.BackColor2 = Color.DarkGreen;

            }


            double tmp = (double)dt.Rows.Count / (double)_screenQty;

            _maxPage = int.Parse(Math.Ceiling(tmp == 1 ? 2 : tmp).ToString());

            for (int j = 0; j < _dt.Rows.Count; j++)
            {
                _contentsBtn[j].Text = dt.Rows[j][1].ToString().Replace("(", Environment.NewLine + "(");
                _contentsBtn[j].Tag = dt.Rows[j][0].ToString();
                _contentsBtn[j].Enabled = true;

                if (j == _screenQty - 1)
                    break;
            }

            _contentsBtn[0].Checked = true;
            _contentsBtn[0].Appearance.BackColor = Color.LightGreen;
            _contentsBtn[0].Appearance.BackColor2 = Color.DarkGreen;

            //현재페이지 초기화     
            //_currentPage = 1;
            flowLayoutPanelContents.Show();

        }

        private void SetNaviButton(DataTable dt)
        {
            double tmp;
            tmp = (double)dt.Rows.Count / (double)_screenQty;

            _maxPage = int.Parse(Math.Ceiling(tmp).ToString());

            btnTop.Enabled = false;
            //btnPre.Enabled = false;
            //btnNext.Enabled = false;

            if (CurrentPage == 1) //현재 첫페이지 
            {
                if (_maxPage == 1)
                {
                    btnPre.Enabled = false;
                    btnNext.Enabled = false;

                }
                if (_maxPage > 1)//맥스 페이지가 1페이지 이상
                {
                    btnPre.Enabled = false;
                    btnNext.Enabled = true;
                }
            }
            else if (CurrentPage > 1 && _maxPage > CurrentPage) //현재 첫페이지  이상
            {
                if (_maxPage > 1)//맥스 페이지가 1페이지 이상
                {
                    btnPre.Enabled = true;
                    btnNext.Enabled = true;
                }
            }
            else if (CurrentPage == _maxPage) //현재 마지막 페이지
            {
                btnPre.Enabled = true;
                btnNext.Enabled = false;
            }
            if (_maxPage != 0)
                SetUpdateTotalProgress((int)(CurrentPage * 100 / _maxPage));
        }

        private void SetContentsPreNext()
        {
            flowLayoutPanelContents.Hide();

            for (int j = _screenQty * (CurrentPage - 1); j < _screenQty * CurrentPage; j++)
            {
                if (_dt.Rows.Count <= j)
                {
                    _contentsBtn[j % _screenQty].Text = string.Empty;
                    _contentsBtn[j % _screenQty].Tag = string.Empty;
                    _contentsBtn[j % _screenQty].Enabled = false;
                }
                else
                {
                    _contentsBtn[j % _screenQty].Text = _dt.Rows.Count > j ? _dt.Rows[j][1].ToString() : string.Empty;
                    _contentsBtn[j % _screenQty].Tag = _dt.Rows.Count > j ? _dt.Rows[j][0].ToString() : string.Empty;
                    _contentsBtn[j % _screenQty].Enabled = true;
                }
            }
            flowLayoutPanelContents.Show();

        }


        public void SetUpdateTotalProgress(int percentage)
        {
            progressBarContents.EditValue = percentage;
            //Application.DoEvents();
        }

        private void BtnPre_Click(object sender, EventArgs e)
        {

            if (CurrentPage != 1)
            {
                CurrentPage -= 1;
            }

            SetContentsPreNext();
            SetNaviButton(_dt);
            BeginInvoke(new Action(() => 
            { 
                _contentsBtn[0].Checked = true; 
                gridView1.FocusedRowHandle = ((CurrentPage - 1) * 40);
            }));

        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage != _maxPage)
            {
                CurrentPage += 1;
            }

            SetContentsPreNext();
            SetNaviButton(_dt);
            BeginInvoke(new Action(() => {
                _contentsBtn[0].Checked = true;
                gridView1.FocusedRowHandle = ((CurrentPage - 1) * 40);
            }));

        }

        private void BtnTop_Click(object sender, EventArgs e)
        {
            InitItemTable();
            ContentsLv = 0;
        }
        
        private void InitItemTable()
        {
            DataSet ds = SearchItem(new object[] { POSGlobal.StoreCode });
            _dt = ds.Tables[0];
            gridMain.Binding(_dt, true);
            SetContents(_dt);
            SetNaviButton(_dt);
        }
    }
}
