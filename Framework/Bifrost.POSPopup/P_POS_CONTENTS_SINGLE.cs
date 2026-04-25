using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.XtraBars.Customization.Helpers;
using DevExpress.XtraEditors;

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
    public partial class P_POS_CONTENTS_SINGLE : POSPopupBase
    {

        DevExpress.XtraEditors.SimpleButton[] _contentsBtn;
        DataTable _dt;

        DataTable _dtAll;

        public Dictionary<string, string> SelectedContent = new Dictionary<string, string>();

        public P_POS_CONTENTS_SINGLE()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }


        //폰트 크기
        static float fontSizeMain = 17F;
        static float fontSizeContents = 17F;
        static float fontSizeKeypad = 20F;


        //품목 폰트 타입
        static Font FontDefault = new Font("카이겐고딕 KR Regular", fontSizeContents);//, GraphicsUnit.Pixel, 0);
        static Font FontDeal = new Font("카이겐고딕 KR Regular", fontSizeContents, FontStyle.Bold);//, GraphicsUnit.Pixel, 0);
        static Font FontDealToday = new Font("카이겐고딕 KR Regular", fontSizeContents, FontStyle.Bold | FontStyle.Underline);//, GraphicsUnit.Pixel, 0);
        static Font FontMain = new Font("카이겐고딕 KR Regular", fontSizeMain);//, GraphicsUnit.Pixel, 0);


        //키패드 폰트 사이즈
        static Font FontKeypad = new Font("카이겐고딕 KR Regular", fontSizeKeypad);

        //컨텐츠 폰트
        static Font FontContents = new Font("카이겐고딕 KR Regular", fontSizeContents, FontStyle.Underline);

        //기본 폰트 색상
        readonly Color ColorFontDefault = Color.Empty;
        readonly Color ColorFontPress = Color.White;

        readonly Color ColorMain = Color.FromArgb(170, 203, 239);
        readonly Color ColorSub = Color.FromArgb(199, 225, 239);
        readonly Color ColorPress = Color.FromArgb(31, 85, 153);

        private readonly Color ColorNonPaid = Color.FromArgb(209, 39, 79);
        private readonly Color ColorCash = Color.FromArgb(0, 51, 204);


        //대분류 패널의 행열 개수를 위한 변수
        int RowQty = 10;
        int ColQty = 4;

        //화면에 보이는 버튼 개수
        public int ScreenQty
        {
            get { return RowQty * ColQty; }
        }

        int _currentPage = 1;

        int CurrentPage
        {
            get
            {
                return _currentPage == 0 ? 1 : _currentPage;
            }
            set
            {
                _currentPage = value;
            }
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

        public string CustomerType { get; set; } = "2";

        public string SearchParameter { get; set; } = string.Empty;


        int _maxPage = 1;

        public string ResultCode { get; set; }

        public string ResultName { get; set; } = string.Empty;

        public ContentsMode ContentsType { get; set; } = ContentsMode.Custom;

        public DataTable dtCustom { get; set; }

        public string keyCustom { get; set; }

        public string StoredProcedure { get; set; }

        public DBType dbType { get; set; }

        public object[] spParams { get; set; }

        public string SeletedCode { get; set; }

        public string SeletedName { get; set; }

        private void InitEvent()
        {
            btnNext.Click += BtnNext_Click;
            btnPre.Click += BtnPre_Click;
            btnTop.Click += BtnTop_Click;
            btnDone.Click += BtnDone_Click;

            btnClear.Click += BtnClear_Click;
            btnSearch.Click += BtnSearch_Click;
            this.Load += P_POS_CONTENTS_SINGLE_Load;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            P_POS_INITIAL P_POS_INITIAL = new P_POS_INITIAL();
            P_POS_INITIAL.ContentsType = ContentsType;

            if (P_POS_INITIAL.ShowDialog() == DialogResult.OK)
            {
                SearchParameter = P_POS_INITIAL.ResultText;
                OnSearch();
            }

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            SearchParameter = string.Empty;
            SelectedContent.Clear();
            OnSearch();
        }

        private void P_POS_CONTENTS_SINGLE_Load(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void InitItemTable()
        {
            _dt = DBHelper.GetDataTable("USP_POS_ITEM_S", new object[] { POSGlobal.StoreCode, string.Empty, string.Empty, string.Empty });

            if(_dt != null && _dt.Rows.Count > 0)
            {
                ContentsLv = int.Parse(_dt.Rows[0]["LV_CONTENTS"].ToString());
            }

            SetContents(_dt);
            SetNaviButton(_dt);

        }
        private void BtnTop_Click(object sender, EventArgs e)
        {
            InitItemTable();
            ContentsLv = 0;
        }

        private void BtnPre_Click(object sender, EventArgs e)
        {
            if (CurrentPage != 1)
            {
                CurrentPage -= 1;
            }

            SetContentsPreNext(_dt);
            SetNaviButton(_dt);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage != _maxPage)
            {
                CurrentPage += 1;
            }

            SetContentsPreNext(_dt);
            SetNaviButton(_dt);
        }

        private void InitForm()
        {
            _contentsBtn = new DevExpress.XtraEditors.SimpleButton[ScreenQty];

            if (PopupTitle == string.Empty)
            {
                PopupTitle = ContentsType == ContentsMode.Customer ? "고객 도움창" : "상품 도움창";
            }

            #region 좌측 컨트롤 패널
            flowLayoutPanelContents.SuspendLayout();
            //콘텐츠 버튼 생성 (40개) 픽스
            for (int i = 0; i < ScreenQty; i++)
            {
                _contentsBtn[i] = new DevExpress.XtraEditors.CheckButton();
                _contentsBtn[i].Name = "btnContents" + i.ToString();
                _contentsBtn[i].Size = new System.Drawing.Size(141, 62);
                _contentsBtn[i].TabIndex = i;
                _contentsBtn[i].Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                _contentsBtn[i].Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
                _contentsBtn[i].AllowFocus = false;
                _contentsBtn[i].Click += BtnContents_Click;

                if ((i / 4) % 2 == 0) //짝수
                    CH.SetButtonApperance(_contentsBtn[i], FontDefault, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);
                else
                    CH.SetButtonApperance(_contentsBtn[i], FontDefault, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);

                flowLayoutPanelContents.Controls.Add(_contentsBtn[i]);
            }
            flowLayoutPanelContents.ResumeLayout();
            #endregion 좌측 컨트롤 패널

        }

        protected override void OnOK()
        {
            this.DialogResult = DialogResult.OK;
        }

        private void BtnContents_Click(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.CheckButton)sender).Text == string.Empty) return;

            SeletedCode = ((DevExpress.XtraEditors.CheckButton)sender).Tag.ToString();
            SeletedName = ((DevExpress.XtraEditors.CheckButton)sender).Text;

            OnOK();
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

                //_contentsBtn[j].Checked = false;
                //_contentsBtn[j].Appearance.BackColor = Color.White;
                //_contentsBtn[j].Appearance.BackColor2 = Color.White;

                //_contentsBtn[j].Appearance.BackColor = Color.LightGreen;
                //_contentsBtn[j].Appearance.BackColor2 = Color.DarkGreen;

            }


            double tmp = (double)dt.Rows.Count / (double)ScreenQty;

            _maxPage = int.Parse(Math.Ceiling(tmp == 1 ? 2 : tmp).ToString());

            for (int j = 0; j < _dt.Rows.Count; j++)
            {
                _contentsBtn[j].Text = dt.Rows[j][1].ToString().Replace("\\r\\n", Environment.NewLine);
                _contentsBtn[j].Tag = dt.Rows[j][0].ToString();
                _contentsBtn[j].Enabled = true;

                if (j == ScreenQty - 1)
                    break;
            }

            //_contentsBtn[0].Checked = true;
            //_contentsBtn[0].Appearance.BackColor = Color.LightGreen;
            //_contentsBtn[0].Appearance.BackColor2 = Color.DarkGreen;

            //현재페이지 초기화     
            //_currentPage = 1;
            flowLayoutPanelContents.Show();

        }

        private void SetNaviButton(DataTable dt)
        {
            double tmp;
            tmp = (double)dt.Rows.Count / (double)ScreenQty;

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

        public void SetUpdateTotalProgress(int percentage)
        {
            progressBarContentsTop.EditValue = percentage;
            progressBarContentsBottom.EditValue = percentage;
            //Application.DoEvents();
        }


        private void SetContentsPreNext(DataTable dt)
        {
            flowLayoutPanelContents.Hide();

            for (int j = ScreenQty * (CurrentPage - 1); j < ScreenQty * CurrentPage; j++)
            {
                if (_dt.Rows.Count <= j)
                {
                    _contentsBtn[j % ScreenQty].Text = string.Empty;
                    _contentsBtn[j % ScreenQty].Tag = string.Empty;
                    _contentsBtn[j % ScreenQty].Enabled = false;
                    //_contentsBtn[j % ScreenQty].Checked = false;
                }
                else
                {
                    _contentsBtn[j % ScreenQty].Text = dt.Rows.Count > j ? dt.Rows[j][1].ToString().Replace("\\r\\n", Environment.NewLine) : string.Empty;
                    _contentsBtn[j % ScreenQty].Tag = dt.Rows.Count > j ? dt.Rows[j][0].ToString() : string.Empty;
                    _contentsBtn[j % ScreenQty].Enabled = true;

                    //if (SelectedContent.Keys.Contains(_dt.Rows[j][keyCustom].ToString()))
                    //    _contentsBtn[j % ScreenQty].Checked = true;
                    //else
                    //    _contentsBtn[j % ScreenQty].Checked = false;
                }
            }
            flowLayoutPanelContents.Show();

        }

        protected override void OnSearch()
        {
            DataColumn[] dtkey = new DataColumn[1];

            switch (ContentsType)
            {
                case ContentsMode.Customer:
                    _dt = DBHelper.GetDataTable("USP_POS_CUST_SIMPLE_S", new object[] { POSGlobal.StoreCode, SearchParameter, "Y", DBNull.Value, CustomerType });
                    
                    dtkey[0] = _dt.Columns["CD_CUST"];
                    keyCustom = "CD_CUST";
                    _dt.PrimaryKey = dtkey;
                    CurrentPage = 1;
                    //패널에 데이터 넣기
                    SetContents(_dt);
                    SetNaviButton(_dt);
                    break;
                case ContentsMode.Item:
                    if(SearchParameter == string.Empty)
                        _dt = DBHelper.GetDataTable("USP_POS_ITEM_S", new object[] { POSGlobal.StoreCode, string.Empty, string.Empty, string.Empty });
                    else
                        _dt = DBHelper.GetDataTable("USP_POS_ITEM_S", new object[] { POSGlobal.StoreCode, SearchParameter, "ALL" });

                    
                    dtkey[0] = _dt.Columns["CD_ITEM"];
                    keyCustom = "CD_ITEM";
                    _dt.PrimaryKey = dtkey;
                    CurrentPage = 1;
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        ContentsLv = int.Parse(_dt.Rows[0]["LV_CONTENTS"].ToString());
                    }
                    //패널에 데이터 넣기
                    SetContents(_dt);
                    SetNaviButton(_dt);
                    break;
                case ContentsMode.Custom:
                    _dt = dtCustom;
                    
                    dtkey[0] = _dt.Columns[keyCustom];
                    _dt.PrimaryKey = dtkey;
                    
                    CurrentPage = 1;
                    //패널에 데이터 넣기
                    SetContents(_dt);
                    SetNaviButton(_dt);
                    break;
                case ContentsMode.StoredProcedure:

                    if(dbType == DBType.Old)
                    {
                        //SearchParameter
                        List<string> paramsList = new List<string>();

                        if (spParams != null && spParams.Length > 0)
                        {
                            for (int i = 0; i < spParams.Length; i++)
                            {
                                paramsList.Add(spParams[i].ToString());
                            }
                        }
                        paramsList.Add(SearchParameter);
                        _dt = DBHelperOld.GetDataTable(StoredProcedure, paramsList.Cast<object>().ToArray() );
                        
                        dtkey[0] = _dt.Columns[keyCustom];
                        _dt.PrimaryKey = dtkey;

                        CurrentPage = 1;
                        //패널에 데이터 넣기
                        SetContents(_dt);
                        SetNaviButton(_dt);
                    }
                    else
                    {

                    }
                    
                    break;

            }

            _dtAll = _dt;
        }

    }
}
