using Bifrost;
using Bifrost.Common;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using POS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_INV_OPEN : POSFormBase
    {
        SimpleButton[] _contentsBtn;

        //대분류 패널의 행열 개수를 위한 변수
        readonly int RowQty = 10;
        readonly int ColQty = 4;

        //키패드 미리 생성
        P_KEYPAD P_KEYPAD = new P_KEYPAD();
        // 팝업창 위치 고정
        Point PopupPoint = new Point(100, 100);


        public int ScreenQty
        {
            get { return RowQty * ColQty; }
        }

        DataTable _dtItem;

        DataTable _dtItemAll;

        string _tmpResult = string.Empty; //키패드 숫자를 관리하기위한 변수


        public M_POS_INV_OPEN()
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            OnView();
        }

        private void InitForm()
        {
            CH.SetButtonApperanceSub(btnPadMinus);
            CH.SetButtonApperanceSub(btnPadEA);
            CH.SetButtonApperanceSub(btnPadMinus);
            CH.SetButtonApperanceSub(btnPadPoint);
            CH.SetButtonApperanceSub(btnPadClear);
            CH.SetButtonApperanceSub(btnPadBackSpace);

            CH.SetButtonApperanceMain(btnPad1);
            CH.SetButtonApperanceMain(btnPad2);
            CH.SetButtonApperanceMain(btnPad3);
            CH.SetButtonApperanceMain(btnPad4);
            CH.SetButtonApperanceMain(btnPad5);
            CH.SetButtonApperanceMain(btnPad6);
            CH.SetButtonApperanceMain(btnPad7);
            CH.SetButtonApperanceMain(btnPad8);
            CH.SetButtonApperanceMain(btnPad9);
            CH.SetButtonApperanceMain(btnPad0);
            CH.SetButtonApperanceMain(btnPad00);
            CH.SetButtonApperanceMain(btnPad000);

            CH.SetButtonApperanceDefault(btnPadItemUnit1);
            CH.SetButtonApperanceDefault(btnPadItemUnit2);
            CH.SetButtonApperanceDefault(btnPadItemUnit3);
            CH.SetButtonApperanceDefault(btnPadItemUnit4);
            CH.SetButtonApperanceDefault(btnPadDelItem);
            CH.SetButtonApperanceDefault(btnPadConfirm);
            CH.SetButtonApperanceDefault(btnPadInit);
            CH.SetButtonApperanceDefault(btnPadCancel);
            CH.SetButtonApperanceDefault(btnPadPrice);

            SetGridInit(gridView1);

            InitContentsPanel();
            panelContents.BringToFront();

            panelContents.Visible = true;

            _dtItem = SearchItem(new object[] { POSGlobal.StoreCode });

            _dtItemAll = SearchItem(new object[] { POSGlobal.StoreCode, string.Empty, "ALL", string.Empty });

            _contentsBtn = new DevExpress.XtraEditors.SimpleButton[ScreenQty];

            ContentsLv = 0;

            #region 좌측 컨트롤 패널
            flowLayoutPanelContents.SuspendLayout();
            //콘텐츠 버튼 생성 (40개) 픽스
            for (int i = 0; i < ScreenQty; i++)
            {
                _contentsBtn[i] = new DevExpress.XtraEditors.SimpleButton();
                _contentsBtn[i].Name = "btnContents" + i.ToString();
                _contentsBtn[i].Size = new Size(144, 70);
                _contentsBtn[i].TabIndex = i;
                _contentsBtn[i].Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                //_contentsBtn[i].Appearance.Font = new Font("카이겐고딕 KR Regular", 12F, FontStyle.Regular);
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


            SetContents();
            SetNaviButton();
        }

        private void InitEvent()
        {
            InitKeypadEvent();

            #region Contents 패널 버튼
            btnContentsSearch.Click += BtnCtsCustomerSearch_Click;             //Contents 검색
            btnInit.Click += BtnCtsInit_Click;                                 //초기화버튼


            btnTop.Click += BtnCtsTop_Click;                                   //상위 버튼
            btnPre.Click += BtnCtsPre_Click;                                   //이전 버튼
            btnNext.Click += BtnCtsNext_Click;                                 //다음 버튼
            btnClose.Click += BtnCtsClose_Click;                               //컨텐츠 닫기

            #endregion Contents 패널 버튼

            gridView1.FocusedRowChanged += GridViewItem_FocusedRowChanged;
            gridView1.InitNewRow += GridView_InitNewRow;
            gridView1.DoubleClick += GridViewItem_DoubleClick;

        }


        private void BtnCtsTop_Click(object sender, EventArgs e)
        {
            InitItemTable();
        }

        private void InitItemTable()
        {
            _dtItem = SearchItem(new object[] { POSGlobal.StoreCode });
            SetContents();
            SetNaviButton();
            ContentsLv = 0;
        }

        string _itemCode = string.Empty;
        string _itemName = string.Empty;

        private decimal _Qt = 0;
        private decimal _QtUnit = 0;
        private decimal _itemPrice = 0;
        private string _itemDescript = string.Empty;
        private string _unitCode = string.Empty;
        private string _unitName = string.Empty;
        private string _vatType = string.Empty;
        private decimal _vatRate = 0.0m;

        private int ClickRowHandle;


        bool IsItemTypeFix = false;

        private bool _isKeyPadItemModify = false;
        public bool IsKeyPadItemModify
        {
            get { return _isKeyPadItemModify; }
            set
            {
                _isKeyPadItemModify = value;
                btnPadDelItem.Visible = value;
            }
        }

        private void BtnContents_Click(object sender, EventArgs e)
        {
            if (((SimpleButton)sender).Text == string.Empty) return;


            _itemCode = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
            _itemName = ((DevExpress.XtraEditors.SimpleButton)sender).Text;

            DataRow[] dr = _dtItem.Select("CD_ITEM = '" + _itemCode + "'");
            string ItemFlag = string.Empty;

            if (dr.Length > 0)
            {
                ItemFlag = A.GetString(dr[0]["FG_ITEM"]);
                IsItemTypeFix = A.GetString(dr[0]["CD_CLAS_REF"]) == "Y" ? true : false;
            }
            //대분류일경우 다시 버튼 배치
            if (ItemFlag == "L")
            {
                _dtItem = _dtItemAll.Select("TP_ITEM_L = '" + _itemCode + "'").CopyToDataTable();//( DBHelper.GetDataTable("USP_POS_ITEM_S", new object[] { POSGlobal.StoreCode, string.Empty, ItemFlag, _itemCode });
                SetContents();
                SetNaviButton();
                ContentsLv = 1; //컨텐츠 레벨이 0이 아닐때 상위 버튼 활성화 시킴
                return;
            }

            _dtItem = _dtItemAll;
            //키패드 페이지 보여줌
            //navPay.SelectedPage = navPayKeypad;
            panelContents.Visible = false;
            panelKeypadItem.Visible = true;

            IsKeyPadItemModify = false;

            _vatType = string.Empty;
            _vatRate = 0;
            _Qt = 0;
            _QtUnit = 0;
            _itemPrice = 0;
            _itemDescript = string.Empty;
            _unitCode = string.Empty;
            _unitName = string.Empty;
            SetResultText();
            //단위 넣기
            GetItemUnit();

        }

        private void BtnCtsCustomerSearch_Click(object sender, EventArgs e)
        {
            //P_SEARCH_INITIAL P_SEARCH_INITIAL = new P_SEARCH_INITIAL();
            //if (txtCust.Text == string.Empty)
            //    P_SEARCH_INITIAL.ContentsType = ContentsMode.Customer;
            //else
            //    P_SEARCH_INITIAL.ContentsType = ContentsMode.Item;

            //if (P_SEARCH_INITIAL.ShowDialog() == DialogResult.OK)
            //{
            //    ContentsType = P_SEARCH_INITIAL.ContentsType;// ContentsMode.Customer;
            //    if (ContentsType == ContentsMode.Customer)
            //    {
            //        if (P_SEARCH_INITIAL.ResultText == string.Empty)
            //            _dtCust = _dtCustAll;
            //        else
            //            _dtCust = SearchCust(new object[] { POSGlobal.StoreCode, P_SEARCH_INITIAL.ResultText, "Y", dtpSale.Text });
            //    }
            //    else
            //    {
            //        if (P_SEARCH_INITIAL.ResultText == string.Empty) //전체검색
            //            _dtItem = SearchItem(new object[] { POSGlobal.StoreCode, P_SEARCH_INITIAL.ResultText, string.Empty, string.Empty });
            //        else
            //            _dtItem = SearchItem(new object[] { POSGlobal.StoreCode, P_SEARCH_INITIAL.ResultText, "ALL", string.Empty });
            //    }
            //    SetContents();
            //    SetNaviButton();
            //    //SetItemTypeScreen(IsItemTypeScreen);
            //    IsContentScreen = true;
            //}
        }
        private void BtnCtsInit_Click(object sender, EventArgs e)
        {
            InitItemTable();
        }
        private void BtnCtsPre_Click(object sender, EventArgs e)
        {

            if (_currentPage != 1)
            {
                _currentPage -= 1;
            }

            SetContentsPreNext();
            SetNaviButton();

        }
        private void BtnCtsNext_Click(object sender, EventArgs e)
        {
            if (_currentPage != _maxPage)
            {
                _currentPage += 1;
            }

            SetContentsPreNext();
            SetNaviButton();

        }
        private void BtnCtsClose_Click(object sender, EventArgs e)
        {
            //navBottom.SelectedPage = navigationPage1;
            //IsItemTypeScreen = false;
            panelContents.Visible = false;

            //splitMain.SplitterPosition = 205;
            //SetItemTypeScreen(IsItemTypeScreen);
        }

        private void GridViewItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;

            if (panelContents.Visible == false)
            {
                //선택한 로우 저장
                ClickRowHandle = view.FocusedRowHandle;

                //navPay.SelectedPage = navPayKeypad;
                panelKeypadItem.Visible = true;

                IsKeyPadItemModify = true;

                _itemCode = A.GetString(view.GetRowCellValue(view.FocusedRowHandle, "CD_ITEM"));
                _itemName = A.GetString(view.GetRowCellValue(view.FocusedRowHandle, "NM_ITEM"));
                PadQty = A.GetDecimal(view.GetRowCellValue(view.FocusedRowHandle, "QT"));
                PadQtyUnit = A.GetDecimal(view.GetRowCellValue(view.FocusedRowHandle, "QT_UNIT"));
                PadItemPrice = A.GetDecimal(view.GetRowCellValue(view.FocusedRowHandle, "UM"));
                PadItemDescript = A.GetString(view.GetRowCellValue(view.FocusedRowHandle, "DC_ITEM"));
                PadUnitCode = A.GetString(view.GetRowCellValue(view.FocusedRowHandle, "CD_UNIT"));
                PadUnitName = A.GetString(view.GetRowCellValue(view.FocusedRowHandle, "NM_UNIT"));

                txtPadItem.Text = _itemName;

                //단위 넣기
                GetItemUnit();
                SetResultText();
            }
        }
        private void GridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            DataTable _dt = gridView1.DataSource as DataTable;

            //decimal maxNoLine = A.GetDecimal(_dt.Compute("MAX(NO_LINE)", ""));
            //++maxNoLine;

            view.SetRowCellValue(e.RowHandle, view.Columns["CD_ITEM"], _itemCode);
            view.SetRowCellValue(e.RowHandle, view.Columns["NM_ITEM"], _itemName);
            //view.SetRowCellValue(e.RowHandle, view.Columns["QT"], _Qt);
            view.SetRowCellValue(e.RowHandle, view.Columns["QT_OPEN"], _QtUnit);
            view.SetRowCellValue(e.RowHandle, view.Columns["UM_OPEN"], _itemPrice);
            view.SetRowCellValue(e.RowHandle, view.Columns["AM_OPEN"], _Qt * _QtUnit * _itemPrice);
            //view.SetRowCellValue(e.RowHandle, view.Columns["DC_ITEM"], _itemDescript);
            view.SetRowCellValue(e.RowHandle, view.Columns["CD_UNIT"], _unitCode);
            view.SetRowCellValue(e.RowHandle, view.Columns["NM_UNIT"], _unitName);
            //view.SetRowCellValue(e.RowHandle, view.Columns["FG_VAT"], _vatType);
            //view.SetRowCellValue(e.RowHandle, view.Columns["AM_VAT"], _Qt * _QtUnit * _itemPrice * _vatRate);
            //view.SetRowCellValue(e.RowHandle, view.Columns["AM_NET"], (_Qt * _QtUnit * _itemPrice) - (_Qt * _QtUnit * _itemPrice * _vatRate));
            //view.SetRowCellValue(e.RowHandle, view.Columns["YN_RETURN"], "N");
            //view.SetRowCellValue(e.RowHandle, view.Columns["NO_LINE"], maxNoLine);
        }

        private void GridViewItem_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                //if (VerifyCheck(sender) == false) return;

                //선택한 로우 저장
                ClickRowHandle = info.RowHandle;

                //navPay.SelectedPage = navPayKeypad;
                panelContents.Visible = false;
                //panelKeypadItem.Visible = true;
                GridViewItem_FocusedRowChanged(sender, null);
            }
        }


        #region Init ContentsPanel 
        private DevExpress.XtraEditors.PanelControl panelContents;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelContents;
        private DevExpress.XtraEditors.ProgressBarControl progressBarContentTop;
        private System.Windows.Forms.FlowLayoutPanel panelTop;
        private DevExpress.XtraEditors.SimpleButton btnContentsSearch;
        private DevExpress.XtraEditors.SimpleButton btnFavorite;
        private DevExpress.XtraEditors.SimpleButton btnItemType;
        private DevExpress.XtraEditors.ProgressBarControl progressBarContents;
        private System.Windows.Forms.FlowLayoutPanel panelBottom;
        private DevExpress.XtraEditors.SimpleButton btnTop;
        private DevExpress.XtraEditors.SimpleButton btnPre;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnInit;

        private void InitContentsPanel()
        {
            panelContents = new DevExpress.XtraEditors.PanelControl();
            flowLayoutPanelContents = new System.Windows.Forms.FlowLayoutPanel();
            progressBarContentTop = new DevExpress.XtraEditors.ProgressBarControl();
            panelTop = new System.Windows.Forms.FlowLayoutPanel();
            btnContentsSearch = new DevExpress.XtraEditors.SimpleButton();
            btnFavorite = new DevExpress.XtraEditors.SimpleButton();
            btnItemType = new DevExpress.XtraEditors.SimpleButton();
            progressBarContents = new DevExpress.XtraEditors.ProgressBarControl();
            panelBottom = new System.Windows.Forms.FlowLayoutPanel();
            btnTop = new DevExpress.XtraEditors.SimpleButton();
            btnPre = new DevExpress.XtraEditors.SimpleButton();
            btnNext = new DevExpress.XtraEditors.SimpleButton();
            btnClose = new DevExpress.XtraEditors.SimpleButton();
            btnInit = new DevExpress.XtraEditors.SimpleButton();

            ((System.ComponentModel.ISupportInitialize)(panelContents)).BeginInit();
            panelContents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(progressBarContentTop.Properties)).BeginInit();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(progressBarContents.Properties)).BeginInit();
            panelBottom.SuspendLayout();
            // 
            // panelContents
            // 
            panelContents.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelContents.Controls.Add(flowLayoutPanelContents);
            panelContents.Controls.Add(progressBarContentTop);
            panelContents.Controls.Add(panelTop);
            panelContents.Controls.Add(progressBarContents);
            panelContents.Controls.Add(panelBottom);
            panelContents.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContents.Location = new Point(0, 0);
            panelContents.Margin = new System.Windows.Forms.Padding(0);
            panelContents.Name = "panelContents";
            panelContents.Size = new Size(604, 890);
            panelContents.TabIndex = 82;
            panelContents.Visible = false;
            panelContents.Parent = panelDetail;
            // 
            // flowLayoutPanelContents
            // 
            flowLayoutPanelContents.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanelContents.Location = new Point(0, 81);
            flowLayoutPanelContents.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanelContents.Name = "flowLayoutPanelContents";
            flowLayoutPanelContents.Size = new Size(604, 729);
            flowLayoutPanelContents.TabIndex = 88;
            // 
            // progressBarContentTop
            // 
            progressBarContentTop.Dock = System.Windows.Forms.DockStyle.Top;
            progressBarContentTop.Location = new Point(0, 76);
            progressBarContentTop.Name = "progressBarContentTop";
            progressBarContentTop.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            progressBarContentTop.Size = new Size(604, 5);
            progressBarContentTop.TabIndex = 87;
            progressBarContentTop.Visible = false;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnContentsSearch);
            panelTop.Controls.Add(btnFavorite);
            panelTop.Controls.Add(btnInit);
            panelTop.Controls.Add(btnItemType);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(604, 76);
            panelTop.TabIndex = 82;
            panelTop.Visible = false;
            // 
            // btnCustomerSearch
            // 
            btnContentsSearch.AllowFocus = false;
            btnContentsSearch.Appearance.Font = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
            btnContentsSearch.Appearance.ForeColor = Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            btnContentsSearch.Appearance.Options.UseFont = true;
            btnContentsSearch.Appearance.Options.UseForeColor = true;
            btnContentsSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnContentsSearch.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_search_icon_normal;
            btnContentsSearch.ImageOptions.SvgImageSize = new Size(16, 16);
            btnContentsSearch.Location = new Point(3, 3);
            btnContentsSearch.Name = "btnCustomerSearch";
            btnContentsSearch.Size = new Size(144, 70);
            btnContentsSearch.TabIndex = 83;
            btnContentsSearch.Text = "검색";
            // 
            // btnFavorite
            // 
            btnFavorite.AllowFocus = false;
            btnFavorite.Appearance.Font = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
            btnFavorite.Appearance.ForeColor = Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            btnFavorite.Appearance.Options.UseFont = true;
            btnFavorite.Appearance.Options.UseForeColor = true;
            btnFavorite.Appearance.Options.UseTextOptions = true;
            btnFavorite.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnFavorite.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_star_icon_normal;
            btnFavorite.ImageOptions.SvgImageSize = new Size(16, 16);
            btnFavorite.Location = new Point(153, 3);
            btnFavorite.Name = "simpleButton18";
            btnFavorite.Size = new Size(144, 70);
            btnFavorite.TabIndex = 84;
            btnFavorite.Text = "즐겨찾기";
            // 
            // btnInit
            // 
            btnInit.AllowFocus = false;
            btnInit.Appearance.Font = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
            btnInit.Appearance.ForeColor = Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            btnInit.Appearance.Options.UseFont = true;
            btnInit.Appearance.Options.UseForeColor = true;
            btnInit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnInit.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_reset_icon_normal;
            btnInit.ImageOptions.SvgImageSize = new Size(16, 16);
            btnInit.Location = new Point(303, 3);
            btnInit.Name = "btnInit";
            btnInit.Size = new Size(144, 70);
            btnInit.TabIndex = 135;
            btnInit.Text = "초기화";
            // 
            // btnItemType
            // 
            btnItemType.AllowFocus = false;
            btnItemType.Appearance.Font = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
            btnItemType.Appearance.ForeColor = Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            btnItemType.Appearance.Options.UseFont = true;
            btnItemType.Appearance.Options.UseForeColor = true;
            btnItemType.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnItemType.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_section_icon_normal;
            btnItemType.ImageOptions.SvgImageSize = new Size(16, 16);
            btnItemType.Location = new Point(453, 3);
            btnItemType.Name = "btnItemType";
            btnItemType.Size = new Size(144, 70);
            btnItemType.TabIndex = 86;
            btnItemType.Text = "대분류";
            // 
            // progressBarContents
            // 
            progressBarContents.Dock = System.Windows.Forms.DockStyle.Bottom;
            progressBarContents.Location = new Point(0, 810);
            progressBarContents.Name = "progressBarContents";
            progressBarContents.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            progressBarContents.Size = new Size(604, 5);
            progressBarContents.TabIndex = 89;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnTop);
            panelBottom.Controls.Add(btnPre);
            panelBottom.Controls.Add(btnNext);
            panelBottom.Controls.Add(btnClose);
            panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelBottom.Location = new Point(0, 815);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(604, 75);
            panelBottom.TabIndex = 90;
            // 
            // btnTop
            // 
            btnTop.AllowFocus = false;
            btnTop.Appearance.Font = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
            btnTop.Appearance.ForeColor = Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            btnTop.Appearance.Options.UseFont = true;
            btnTop.Appearance.Options.UseForeColor = true;
            btnTop.Appearance.Options.UseTextOptions = true;
            btnTop.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnTop.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_up_icon_normal;
            btnTop.ImageOptions.SvgImageSize = new Size(20, 20);
            btnTop.Location = new Point(3, 3);
            btnTop.Name = "btnTop";
            btnTop.Size = new Size(144, 70);
            btnTop.TabIndex = 91;
            // 
            // btnPre
            // 
            btnPre.AllowFocus = false;
            btnPre.Appearance.Font = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
            btnPre.Appearance.ForeColor = Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            btnPre.Appearance.Options.UseFont = true;
            btnPre.Appearance.Options.UseForeColor = true;
            btnPre.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnPre.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_back_icon_normal;
            btnPre.ImageOptions.SvgImageSize = new Size(20, 20);
            btnPre.Location = new Point(153, 3);
            btnPre.Name = "btnPre";
            btnPre.Size = new Size(144, 70);
            btnPre.TabIndex = 92;
            // 
            // btnNext
            // 
            btnNext.AllowFocus = false;
            btnNext.Appearance.Font = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
            btnNext.Appearance.ForeColor = Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            btnNext.Appearance.Options.UseFont = true;
            btnNext.Appearance.Options.UseForeColor = true;
            btnNext.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            btnNext.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_next_icon_normal;
            btnNext.ImageOptions.SvgImageSize = new Size(20, 20);
            btnNext.Location = new Point(303, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(144, 70);
            btnNext.TabIndex = 93;
            // 
            // btnClose
            // 
            btnClose.AllowFocus = false;
            btnClose.Appearance.Font = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
            btnClose.Appearance.ForeColor = Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(146)))));
            btnClose.Appearance.Options.UseFont = true;
            btnClose.Appearance.Options.UseForeColor = true;
            btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnClose.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_close_icon_normal;
            btnClose.ImageOptions.SvgImageSize = new Size(20, 20);
            btnClose.Location = new Point(453, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(144, 70);
            btnClose.TabIndex = 94;

            ((System.ComponentModel.ISupportInitialize)(panelContents)).EndInit();
            panelContents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(progressBarContentTop.Properties)).EndInit();
            panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(progressBarContents.Properties)).EndInit();
            panelBottom.ResumeLayout(false);
        }

        #endregion Init ContentsPanel 

        private void SetGridInit(GridView view)
        {
            view.OptionsView.ShowGroupPanel = false;
            //view.OptionsView.ColumnAutoWidth = false;
            view.OptionsView.ShowAutoFilterRow = false;
            view.OptionsCustomization.AllowSort = true;
            view.OptionsCustomization.AllowFilter = false;

            //그리드 높이
            view.UserCellPadding = new Padding(0, 5, 0, 5);

        
        }

        //품목 폰트 타입
        readonly Font FontDefault = new Font("카이겐고딕 KR Regular", 18F, FontStyle.Regular, GraphicsUnit.Pixel, 0);
        readonly Font FontDeal = new Font("카이겐고딕 KR Regular", 18F, FontStyle.Regular | FontStyle.Underline, GraphicsUnit.Pixel, 0);

        //키패드 폰트 사이즈
        readonly Font FontKeypad = new Font("카이겐고딕 KR Regular", 20F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));

        //컨텐츠 폰트
        readonly Font FontContents = new Font("카이겐고딕 KR Regular", 15F, (FontStyle.Underline));

        //기본 폰트 색상
        readonly Color ColorFontDefault = Color.Empty;
        readonly Color ColorFontPress = Color.White;

        readonly Color ColorMain = Color.FromArgb(170, 203, 239);
        readonly Color ColorSub = Color.FromArgb(199, 225, 239);
        readonly Color ColorPress = Color.FromArgb(31, 85, 153);

        private readonly Color ColorNonPaid = Color.FromArgb(209, 39, 79);
        private readonly Color ColorCash = Color.FromArgb(82, 141, 215);

        int _currentPage = 1;
        int _maxPage = 1;

        string _selectedCode = "CD_ITEM";
        string _selectedName = "NM_ITEM";

        private void SetContentsPreNext()
        {
            flowLayoutPanelContents.Hide();

            for (int j = ScreenQty * (_currentPage - 1); j < ScreenQty * _currentPage; j++)
            {

                if (_dtItem.Rows.Count <= j)
                {
                    _contentsBtn[j % ScreenQty].Text = string.Empty;
                    _contentsBtn[j % ScreenQty].Tag = string.Empty;
                    _contentsBtn[j % ScreenQty].Enabled = false;
                }
                else
                {
                    _contentsBtn[j % ScreenQty].Text = _dtItem.Rows.Count > j ? _dtItem.Rows[j][_selectedName].ToString().Replace("\\r\\n", Environment.NewLine) : string.Empty;
                    _contentsBtn[j % ScreenQty].Tag = _dtItem.Rows.Count > j ? _dtItem.Rows[j][_selectedCode].ToString() : string.Empty;
                    _contentsBtn[j % ScreenQty].Enabled = true;
                }
                _contentsBtn[j % ScreenQty].ForeColor = Color.Empty;
            }
            flowLayoutPanelContents.Show();

        }

        private void SetContents()
        {
            flowLayoutPanelContents.Hide();
            //초기화
            for (int j = 0; j < ScreenQty; j++)
            {
                _contentsBtn[j].Text = string.Empty;
                _contentsBtn[j].Tag = string.Empty;
                _contentsBtn[j].Enabled = false;
                _contentsBtn[j].Font = FontDefault;
            }

            _maxPage = _dtItem.Rows.Count / ScreenQty;

            int loopCnt = _dtItem.Rows.Count < 40 ? _dtItem.Rows.Count : ScreenQty;

            for (int j = 0; j < loopCnt; j++)
            {
                _contentsBtn[j].Text = _dtItem.Rows[j][_selectedName].ToString().Replace("\\r\\n", Environment.NewLine);
                _contentsBtn[j].Tag = _dtItem.Rows[j][_selectedCode].ToString();
                _contentsBtn[j].Enabled = true;
                _contentsBtn[j].ForeColor = Color.Empty;

                if (j == ScreenQty - 1)
                    break;
            }

            //현재페이지 초기화     
            _currentPage = 1;
            flowLayoutPanelContents.Show();
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

        private void SetNaviButton()
        {
            double tmp;
            tmp = (double)_dtItem.Rows.Count / (double)ScreenQty;

            _maxPage = int.Parse(Math.Ceiling(tmp).ToString());

            if (_currentPage == 1) //현재 첫페이지 
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
            else if (_currentPage > 1 && _maxPage > _currentPage) //현재 첫페이지  이상
            {
                if (_maxPage > 1)//맥스 페이지가 1페이지 이상
                {
                    btnPre.Enabled = true;
                    btnNext.Enabled = true;
                }
            }
            else if (_currentPage == _maxPage) //현재 마지막 페이지
            {
                btnPre.Enabled = true;
                btnNext.Enabled = false;
            }
            if (_maxPage != 0)
                SetUpdateTotalProgress((int)(_currentPage * 100 / _maxPage));
        }
        public void SetUpdateTotalProgress(int percentage)
        {
            progressBarContents.EditValue = percentage;
            progressBarContentTop.EditValue = percentage;

            //Application.DoEvents();
        }


        public override void OnView()
        {
            DataTable dt = Search(new object[] { POSGlobal.StoreCode });
            gridMain.Binding(dt, true);
        }

        public override void OnSave()
        {
            gridView1.PostEditor();
            gridView1.UpdateCurrentRow();

            DataTable dtChange = gridMain.GetChanges();

            if (dtChange == null) return;

            bool result = Save(dtChange);

            if (result)
            {
                ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
                if (dtChange != null) dtChange.AcceptChanges();
            }
        }

    }
}
