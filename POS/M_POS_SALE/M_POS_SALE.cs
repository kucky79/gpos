using Bifrost;
using Bifrost.Common;
using Bifrost.Helper;
using Bifrost.Win;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_SALE : POSFormBase
    {
        #region 변수모음
        //터치 버튼 생성자
        SimpleButton[] _contentsBtn;
        SimpleButton[] _ItemTypeBtn;

        //키패드 미리 생성
        P_KEYPAD P_KEYPAD = new P_KEYPAD();


        //터치버튼 네비게이션용 변수
        int _currentPage = 1;
        int _maxPage = 1;

        string _tmpResult = string.Empty; //키패드 숫자를 관리하기위한 변수

        //터치버튼 용 데이터 테이블
        DataTable _dtCust;
        DataTable _dtItem;

        DataTable _dtCustAll;
        DataTable _dtItemAll;
        DataTable _dtItemTypeAll;
        DataTable _dtItemFavorite;

        //메인 데이터테이블
        DataTable _dtH = new DataTable();
        DataTable _dtL = new DataTable();
        DataTable _dtPay = new DataTable();

        DataTable _dtTempH = new DataTable();
        DataTable _dtTempL = new DataTable();
        DataTable _dtTempPay = new DataTable();

        bool IsFirst { get; set; } = true;

        bool _IsContentScreen = false;

        bool IsContentScreen
        {
            get { return _IsContentScreen; }
            set
            {
                _IsContentScreen = value;
                panelContents.Visible = value;

                //대분류 상태에 따라 보여줌
                if (_IsItemTypeScreen)
                    navBottom.SelectedPage = navPageItemType;
                else
                    navBottom.SelectedPage = navPageSaleInfo;
            }
        }

        bool _IsItemTypeScreen = false;

        //대분류 켜기
        bool IsItemTypeScreen
        {
            get { return _IsItemTypeScreen; }
            set
            {
                _IsItemTypeScreen = value;

                if (value)
                    navBottom.SelectedPage = navPageItemType;
                else
                    navBottom.SelectedPage = navPageSaleInfo;

                //if (value)
                //    splitMain.SplitterPosition = ItemTypeQty * 68;
                //else
                //    splitMain.SplitterPosition = 205;
            }
        }

        //이전, 다음 판매번호를 가져오기위한 변수
        private string SalesNoPre { get; set; } = string.Empty;
        private string SalesNoNext { get; set; } = string.Empty;

        string SlipFlag { get; set; } = "A";

        private JobMode _jobStatus = JobMode.New;

        public JobMode JobStatus
        {
            get { return _jobStatus; }
            set
            {
                _jobStatus = value;
                if (value == JobMode.Read)
                {
                    txtCust.ForeColor = ColorNonPaid;
                    lblCustLsatSaleDt.ForeColor = ColorNonPaid;
                    lblCustCarNo.ForeColor = ColorNonPaid;
                    lblCustTel.ForeColor = ColorNonPaid;

                    gridViewItem.Appearance.Row.ForeColor = ColorNonPaid;
                    btnCtrReorder.Enabled = true;
                    btnCtrPrint.Enabled = true;
                }
                else
                {
                    txtCust.ForeColor = Color.Empty;
                    lblCustLsatSaleDt.ForeColor = Color.Empty;
                    lblCustCarNo.ForeColor = Color.Empty;
                    lblCustTel.ForeColor = Color.Empty;

                    gridViewItem.Appearance.Row.ForeColor = Color.Empty;
                    btnCtrReorder.Enabled = false;
                    btnCtrPrint.Enabled = false;
                }
            }

        }
        private PayMode _payMode = PayMode.None;

        // 팝업창 위치 고정
        Point PopupPoint = new Point(100, 100);


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

        #endregion

        public M_POS_SALE()
        {
            InitializeComponent();
            InitKeyPad();

            CH.SetButtonApperanceSub(btnKeyClear);
            CH.SetButtonApperanceSub(btnKeyBackSpace);
            CH.SetButtonApperanceSub(btnKeyMinus);
            CH.SetButtonApperanceSub(btnKeyPoint);

            CH.SetButtonApperanceMain(btnKey1);
            CH.SetButtonApperanceMain(btnKey2);
            CH.SetButtonApperanceMain(btnKey3);
            CH.SetButtonApperanceMain(btnKey4);
            CH.SetButtonApperanceMain(btnKey5);
            CH.SetButtonApperanceMain(btnKey6);
            CH.SetButtonApperanceMain(btnKey7);
            CH.SetButtonApperanceMain(btnKey8);
            CH.SetButtonApperanceMain(btnKey9);
            CH.SetButtonApperanceMain(btnKey0);
            CH.SetButtonApperanceMain(btnKey00);

            InitContentsPanel();
            InitConfig();
            InitEvent();

            //패널 순서
            panelContents.Parent = panelDetail;
            panelContents.SendToBack();
            panelPayInfo.SendToBack();
            panelKeypadItem.BringToFront();

            //키패드 팝업 위치
            P_KEYPAD.Location = panelContents.PointToScreen(PopupPoint);
            P_KEYPAD.StartPosition = FormStartPosition.Manual;

            gridViewItem.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            gridViewItem.Appearance.Row.ForeColor = Color.Empty;
            //gridViewItem.Appearance.Row.Options.UseForeColor = true;
            //CustomDrawRowIndicator(gridItem, gridViewItem);
        }

        #region Init ContentsPanel 
        private DevExpress.XtraEditors.PanelControl panelContents;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelContents;
        private DevExpress.XtraEditors.ProgressBarControl progressBarContentTop;
        private System.Windows.Forms.FlowLayoutPanel panelTop;
        private DevExpress.XtraEditors.ProgressBarControl progressBarContents;
        private System.Windows.Forms.FlowLayoutPanel panelBottom;
        private DevExpress.XtraEditors.SimpleButton btnTop;
        private DevExpress.XtraEditors.SimpleButton btnPre;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnInit;
        private DevExpress.XtraEditors.SimpleButton btnContentsSearch;
        private DevExpress.XtraEditors.SimpleButton btnFavorite;
        private DevExpress.XtraEditors.SimpleButton btnItemType;

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
            // 
            // btnCustomerSearch
            // 
            btnContentsSearch.AllowFocus = false;
            btnContentsSearch.Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents);
            btnContentsSearch.Appearance.ForeColor = Color.FromArgb(54, 86, 146);
            btnContentsSearch.Appearance.Options.UseFont = true;
            btnContentsSearch.Appearance.Options.UseForeColor = true;
            btnContentsSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnContentsSearch.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_search_icon_normal;
            btnContentsSearch.ImageOptions.SvgImageSize = new Size(16, 16);
            btnContentsSearch.Location = new Point(3, 3);
            btnContentsSearch.Name = "btnCustomerSearch";
            btnContentsSearch.Size = new Size(145, 70);
            btnContentsSearch.TabIndex = 83;
            btnContentsSearch.Text = "검색";
            // 
            // btnFavorite
            // 
            btnFavorite.AllowFocus = false;
            btnFavorite.Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents);
            btnFavorite.Appearance.ForeColor = Color.FromArgb(54, 86, 146);
            btnFavorite.Appearance.Options.UseFont = true;
            btnFavorite.Appearance.Options.UseForeColor = true;
            btnFavorite.Appearance.Options.UseTextOptions = true;
            btnFavorite.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnFavorite.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_star_icon_normal;
            btnFavorite.ImageOptions.SvgImageSize = new Size(16, 16);
            btnFavorite.Location = new Point(153, 3);
            btnFavorite.Name = "simpleButton18";
            btnFavorite.Size = new Size(145, 70);
            btnFavorite.TabIndex = 84;
            btnFavorite.Text = "즐겨찾기";
            // 
            // btnInit
            // 
            btnInit.AllowFocus = false;
            btnInit.Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents);
            btnInit.Appearance.ForeColor = Color.FromArgb(54, 86, 146);
            btnInit.Appearance.Options.UseFont = true;
            btnInit.Appearance.Options.UseForeColor = true;
            btnInit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnInit.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_reset_icon_normal;
            btnInit.ImageOptions.SvgImageSize = new Size(16, 16);
            btnInit.Location = new Point(303, 3);
            btnInit.Name = "btnInit";
            btnInit.Size = new Size(145, 70);
            btnInit.TabIndex = 135;
            btnInit.Text = "초기화";
            // 
            // btnItemType
            // 
            btnItemType.AllowFocus = false;
            btnItemType.Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents);
            btnItemType.Appearance.ForeColor = Color.FromArgb(54, 86, 146);
            btnItemType.Appearance.Options.UseFont = true;
            btnItemType.Appearance.Options.UseForeColor = true;
            btnItemType.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnItemType.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_section_icon_normal;
            btnItemType.ImageOptions.SvgImageSize = new Size(16, 16);
            btnItemType.Location = new Point(453, 3);
            btnItemType.Name = "btnItemType";
            btnItemType.Size = new Size(145, 70);
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
            btnTop.Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents);
            btnTop.Appearance.ForeColor = Color.FromArgb(54, 86, 146);
            btnTop.Appearance.Options.UseFont = true;
            btnTop.Appearance.Options.UseForeColor = true;
            btnTop.Appearance.Options.UseTextOptions = true;
            btnTop.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnTop.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_up_icon_normal;
            btnTop.ImageOptions.SvgImageSize = new Size(20, 20);
            btnTop.Location = new Point(3, 3);
            btnTop.Name = "btnTop";
            btnTop.Size = new Size(145, 70);
            btnTop.TabIndex = 91;
            // 
            // btnPre
            // 
            btnPre.AllowFocus = false;
            btnPre.Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents);
            btnPre.Appearance.ForeColor = Color.FromArgb(54, 86, 146);
            btnPre.Appearance.Options.UseFont = true;
            btnPre.Appearance.Options.UseForeColor = true;
            btnPre.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnPre.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_back_icon_normal;
            btnPre.ImageOptions.SvgImageSize = new Size(20, 20);
            btnPre.Location = new Point(153, 3);
            btnPre.Name = "btnPre";
            btnPre.Size = new Size(145, 70);
            btnPre.TabIndex = 92;
            // 
            // btnNext
            // 
            btnNext.AllowFocus = false;
            btnNext.Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents);
            btnNext.Appearance.ForeColor = Color.FromArgb(54, 86, 146);
            btnNext.Appearance.Options.UseFont = true;
            btnNext.Appearance.Options.UseForeColor = true;
            btnNext.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            btnNext.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_page_next_icon_normal;
            btnNext.ImageOptions.SvgImageSize = new Size(20, 20);
            btnNext.Location = new Point(303, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(145, 70);
            btnNext.TabIndex = 93;
            // 
            // btnClose
            // 
            btnClose.AllowFocus = false;
            btnClose.Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents);
            btnClose.Appearance.ForeColor = Color.FromArgb(54, 86, 146);
            btnClose.Appearance.Options.UseFont = true;
            btnClose.Appearance.Options.UseForeColor = true;
            btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnClose.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_close_icon_normal;
            btnClose.ImageOptions.SvgImageSize = new Size(20, 20);
            btnClose.Location = new Point(453, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(145, 70);
            btnClose.TabIndex = 94;

            ((System.ComponentModel.ISupportInitialize)(panelContents)).EndInit();
            panelContents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(progressBarContentTop.Properties)).EndInit();
            panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(progressBarContents.Properties)).EndInit();
            panelBottom.ResumeLayout(false);
        }

        #endregion Init ContentsPanel 

        #region Init Keypad panel

        private DevExpress.XtraEditors.SeparatorControl separatorControlKeypad1;
        private DevExpress.XtraEditors.SeparatorControl separatorControlKeypad2;

        private DevExpress.XtraEditors.PanelControl panelKeypadItem;
        private DevExpress.XtraEditors.LabelControl txtPadItem;
        private DevExpress.XtraEditors.TextEdit txtPadQty;
        private DevExpress.XtraEditors.SimpleButton btnPadPrice;
        private DevExpress.XtraEditors.SimpleButton btnPadCancel;
        private DevExpress.XtraEditors.SimpleButton btnPadDelItem;
        private DevExpress.XtraEditors.SimpleButton btnPadInit;
        private DevExpress.XtraEditors.SimpleButton btnPadMinus;
        private DevExpress.XtraEditors.LabelControl txtItemDescrip;
        private DevExpress.XtraEditors.SimpleButton btnPadEA;
        private DevExpress.XtraEditors.SimpleButton btnPadConfirm;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit4;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit3;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit2;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit1;
        private DevExpress.XtraEditors.SimpleButton btnPadPoint;
        private DevExpress.XtraEditors.SimpleButton btnPadBackSpace;
        private DevExpress.XtraEditors.SimpleButton btnPadClear;
        private DevExpress.XtraEditors.SimpleButton btnPad000;
        private DevExpress.XtraEditors.SimpleButton btnPad00;
        private DevExpress.XtraEditors.SimpleButton btnPad0;
        private DevExpress.XtraEditors.SimpleButton btnPad3;
        private DevExpress.XtraEditors.SimpleButton btnPad2;
        private DevExpress.XtraEditors.SimpleButton btnPad1;
        private DevExpress.XtraEditors.SimpleButton btnPad6;
        private DevExpress.XtraEditors.SimpleButton btnPad5;
        private DevExpress.XtraEditors.SimpleButton btnPad4;
        private DevExpress.XtraEditors.SimpleButton btnPad9;
        private DevExpress.XtraEditors.SimpleButton btnPad8;
        private DevExpress.XtraEditors.SimpleButton btnPad7;
        private DevExpress.XtraEditors.ToggleSwitch swFavorite;

        private void InitKeyPad()
        {

            panelKeypadItem = new DevExpress.XtraEditors.PanelControl();
            separatorControlKeypad1 = new DevExpress.XtraEditors.SeparatorControl();
            separatorControlKeypad2 = new DevExpress.XtraEditors.SeparatorControl();

            txtPadItem = new DevExpress.XtraEditors.LabelControl();
            txtPadQty = new DevExpress.XtraEditors.TextEdit();
            btnPadPrice = new DevExpress.XtraEditors.SimpleButton();
            btnPadCancel = new DevExpress.XtraEditors.SimpleButton();
            btnPadDelItem = new DevExpress.XtraEditors.SimpleButton();
            btnPadInit = new DevExpress.XtraEditors.SimpleButton();
            btnPadMinus = new DevExpress.XtraEditors.SimpleButton();
            txtItemDescrip = new DevExpress.XtraEditors.LabelControl();
            btnPadEA = new DevExpress.XtraEditors.SimpleButton();
            btnPadConfirm = new DevExpress.XtraEditors.SimpleButton();
            btnPadItemUnit4 = new DevExpress.XtraEditors.SimpleButton();
            btnPadItemUnit3 = new DevExpress.XtraEditors.SimpleButton();
            btnPadItemUnit2 = new DevExpress.XtraEditors.SimpleButton();
            btnPadItemUnit1 = new DevExpress.XtraEditors.SimpleButton();
            btnPadPoint = new DevExpress.XtraEditors.SimpleButton();
            btnPadBackSpace = new DevExpress.XtraEditors.SimpleButton();
            btnPadClear = new DevExpress.XtraEditors.SimpleButton();
            btnPad000 = new DevExpress.XtraEditors.SimpleButton();
            btnPad00 = new DevExpress.XtraEditors.SimpleButton();
            btnPad0 = new DevExpress.XtraEditors.SimpleButton();
            btnPad3 = new DevExpress.XtraEditors.SimpleButton();
            btnPad2 = new DevExpress.XtraEditors.SimpleButton();
            btnPad1 = new DevExpress.XtraEditors.SimpleButton();
            btnPad6 = new DevExpress.XtraEditors.SimpleButton();
            btnPad5 = new DevExpress.XtraEditors.SimpleButton();
            btnPad4 = new DevExpress.XtraEditors.SimpleButton();
            btnPad9 = new DevExpress.XtraEditors.SimpleButton();
            btnPad8 = new DevExpress.XtraEditors.SimpleButton();
            btnPad7 = new DevExpress.XtraEditors.SimpleButton();
            swFavorite = new DevExpress.XtraEditors.ToggleSwitch();


            panelKeypadItem.Location = new Point(0, 0);
            panelKeypadItem.Name = "panelKeypadItem";
            panelKeypadItem.Size = panelDetail.Size;
            panelKeypadItem.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
            panelKeypadItem.TabIndex = 1;
            panelKeypadItem.Visible = false;
            panelKeypadItem.Parent = panelDetail;
            panelKeypadItem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            // 
            // separatorControlKeypad1
            // 
            this.separatorControlKeypad1.AutoSizeMode = true;
            this.separatorControlKeypad1.BackColor = System.Drawing.Color.Transparent;
            this.separatorControlKeypad1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(84)))), ((int)(((byte)(151)))));
            this.separatorControlKeypad1.LineThickness = 2;
            this.separatorControlKeypad1.Location = new System.Drawing.Point(54, 108);
            this.separatorControlKeypad1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.separatorControlKeypad1.Margin = new System.Windows.Forms.Padding(0);
            this.separatorControlKeypad1.Name = "separatorControlKeypad1";
            this.separatorControlKeypad1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.separatorControlKeypad1.Size = new System.Drawing.Size(504, 4);
            this.separatorControlKeypad1.TabIndex = 265;
            // 
            // separatorControlKeypad2
            // 
            this.separatorControlKeypad2.AutoSizeMode = true;
            this.separatorControlKeypad2.BackColor = System.Drawing.Color.Transparent;
            this.separatorControlKeypad2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(84)))), ((int)(((byte)(151)))));
            this.separatorControlKeypad2.LineThickness = 2;
            this.separatorControlKeypad2.Location = new System.Drawing.Point(54, 263);
            this.separatorControlKeypad2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.separatorControlKeypad2.Margin = new System.Windows.Forms.Padding(0);
            this.separatorControlKeypad2.Name = "separatorControlKeypad2";
            this.separatorControlKeypad2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.separatorControlKeypad2.Size = new System.Drawing.Size(368, 4);
            this.separatorControlKeypad2.TabIndex = 264;
            // 
            // txtPadQty
            // 
            this.txtPadQty.EditValue = "0";
            this.txtPadQty.Enabled = false;
            this.txtPadQty.Location = new System.Drawing.Point(54, 215);
            this.txtPadQty.Name = "txtPadQty";
            this.txtPadQty.Properties.AllowFocused = false;
            this.txtPadQty.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.txtPadQty.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(84)))), ((int)(((byte)(151)))));
            this.txtPadQty.Properties.Appearance.Options.UseFont = true;
            this.txtPadQty.Properties.Appearance.Options.UseForeColor = true;
            this.txtPadQty.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPadQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPadQty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtPadQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPadQty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPadQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPadQty.Properties.NullValuePrompt = "0";
            this.txtPadQty.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPadQty.Properties.ReadOnly = true;
            this.txtPadQty.Size = new System.Drawing.Size(368, 52);
            this.txtPadQty.TabIndex = 263;
            // 
            // txtItemDescrip
            // 
            this.txtItemDescrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemDescrip.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.txtItemDescrip.Appearance.Options.UseFont = true;
            this.txtItemDescrip.Appearance.Options.UseTextOptions = true;
            this.txtItemDescrip.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtItemDescrip.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.txtItemDescrip.Location = new System.Drawing.Point(54, 118);
            this.txtItemDescrip.Name = "txtItemDescrip";
            this.txtItemDescrip.Size = new System.Drawing.Size(504, 57);
            this.txtItemDescrip.TabIndex = 262;
            this.txtItemDescrip.Text = "";
            // 
            // txtPadItem
            // 
            this.txtPadItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPadItem.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 23F);
            this.txtPadItem.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(84)))), ((int)(((byte)(151)))));
            this.txtPadItem.Appearance.Options.UseFont = true;
            this.txtPadItem.Appearance.Options.UseForeColor = true;
            this.txtPadItem.Appearance.Options.UseTextOptions = true;
            this.txtPadItem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPadItem.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.txtPadItem.Location = new System.Drawing.Point(54, 49);
            this.txtPadItem.Name = "txtPadItem";
            this.txtPadItem.Size = new System.Drawing.Size(504, 57);
            this.txtPadItem.TabIndex = 235;
            this.txtPadItem.Text = "상품명";
            // 
            // btnPadPrice
            // 
            this.btnPadPrice.AllowFocus = false;
            this.btnPadPrice.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadPrice.Appearance.Options.UseFont = true;
            this.btnPadPrice.Location = new System.Drawing.Point(150, 667);
            this.btnPadPrice.Name = "btnPadPrice";
            this.btnPadPrice.Size = new System.Drawing.Size(90, 90);
            this.btnPadPrice.TabIndex = 258;
            this.btnPadPrice.Text = "원";
            // 
            // btnPadCancel
            // 
            this.btnPadCancel.AllowFocus = false;
            this.btnPadCancel.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadCancel.Appearance.Options.UseFont = true;
            this.btnPadCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnPadCancel.ImageOptions.SvgImage = global::POS.Properties.Resources.bright_close_icon_normal;
            this.btnPadCancel.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnPadCancel.Location = new System.Drawing.Point(438, 667);
            this.btnPadCancel.Name = "btnPadCancel";
            this.btnPadCancel.Size = new System.Drawing.Size(120, 90);
            this.btnPadCancel.TabIndex = 260;
            this.btnPadCancel.Text = "취소";
            // 
            // btnPadDelItem
            // 
            this.btnPadDelItem.AllowFocus = false;
            this.btnPadDelItem.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadDelItem.Appearance.Options.UseFont = true;
            this.btnPadDelItem.Location = new System.Drawing.Point(54, 765);
            this.btnPadDelItem.Name = "btnPadDelItem";
            this.btnPadDelItem.Size = new System.Drawing.Size(504, 90);
            this.btnPadDelItem.TabIndex = 261;
            this.btnPadDelItem.Text = "삭제";
            // 
            // btnPadInit
            // 
            this.btnPadInit.AllowFocus = false;
            this.btnPadInit.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadInit.Appearance.Options.UseFont = true;
            this.btnPadInit.Location = new System.Drawing.Point(438, 188);
            this.btnPadInit.Name = "btnPadInit";
            this.btnPadInit.Size = new System.Drawing.Size(120, 90);
            this.btnPadInit.TabIndex = 236;
            this.btnPadInit.Text = "초기화";
            // 
            // btnPadMinus
            // 
            this.btnPadMinus.AllowFocus = false;
            this.btnPadMinus.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadMinus.Appearance.Options.UseFont = true;
            this.btnPadMinus.Location = new System.Drawing.Point(342, 380);
            this.btnPadMinus.Name = "btnPadMinus";
            this.btnPadMinus.Size = new System.Drawing.Size(90, 90);
            this.btnPadMinus.TabIndex = 237;
            this.btnPadMinus.Text = "─";
            // 
            // btnPadEA
            // 
            this.btnPadEA.AllowFocus = false;
            this.btnPadEA.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadEA.Appearance.Options.UseFont = true;
            this.btnPadEA.Location = new System.Drawing.Point(54, 667);
            this.btnPadEA.Name = "btnPadEA";
            this.btnPadEA.Size = new System.Drawing.Size(90, 90);
            this.btnPadEA.TabIndex = 257;
            this.btnPadEA.Text = "묶음";
            // 
            // btnPadConfirm
            // 
            this.btnPadConfirm.AllowFocus = false;
            this.btnPadConfirm.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadConfirm.Appearance.Options.UseFont = true;
            this.btnPadConfirm.Location = new System.Drawing.Point(246, 667);
            this.btnPadConfirm.Name = "btnPadConfirm";
            this.btnPadConfirm.Size = new System.Drawing.Size(186, 90);
            this.btnPadConfirm.TabIndex = 259;
            this.btnPadConfirm.Text = "확인";
            // 
            // btnPadItemUnit4
            // 
            this.btnPadItemUnit4.AllowFocus = false;
            this.btnPadItemUnit4.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadItemUnit4.Appearance.Options.UseFont = true;
            this.btnPadItemUnit4.Enabled = false;
            this.btnPadItemUnit4.Location = new System.Drawing.Point(438, 571);
            this.btnPadItemUnit4.Name = "btnPadItemUnit4";
            this.btnPadItemUnit4.Size = new System.Drawing.Size(120, 90);
            this.btnPadItemUnit4.TabIndex = 256;
            // 
            // btnPadItemUnit3
            // 
            this.btnPadItemUnit3.AllowFocus = false;
            this.btnPadItemUnit3.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadItemUnit3.Appearance.Options.UseFont = true;
            this.btnPadItemUnit3.Enabled = false;
            this.btnPadItemUnit3.Location = new System.Drawing.Point(438, 475);
            this.btnPadItemUnit3.Name = "btnPadItemUnit3";
            this.btnPadItemUnit3.Size = new System.Drawing.Size(120, 90);
            this.btnPadItemUnit3.TabIndex = 251;
            // 
            // btnPadItemUnit2
            // 
            this.btnPadItemUnit2.AllowFocus = false;
            this.btnPadItemUnit2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadItemUnit2.Appearance.Options.UseFont = true;
            this.btnPadItemUnit2.Enabled = false;
            this.btnPadItemUnit2.Location = new System.Drawing.Point(438, 380);
            this.btnPadItemUnit2.Name = "btnPadItemUnit2";
            this.btnPadItemUnit2.Size = new System.Drawing.Size(120, 90);
            this.btnPadItemUnit2.TabIndex = 246;
            // 
            // btnPadItemUnit1
            // 
            this.btnPadItemUnit1.AllowFocus = false;
            this.btnPadItemUnit1.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadItemUnit1.Appearance.Options.UseFont = true;
            this.btnPadItemUnit1.Enabled = false;
            this.btnPadItemUnit1.Location = new System.Drawing.Point(438, 284);
            this.btnPadItemUnit1.Name = "btnPadItemUnit1";
            this.btnPadItemUnit1.Size = new System.Drawing.Size(120, 90);
            this.btnPadItemUnit1.TabIndex = 242;
            // 
            // btnPadPoint
            // 
            this.btnPadPoint.AllowFocus = false;
            this.btnPadPoint.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadPoint.Appearance.Options.UseFont = true;
            this.btnPadPoint.Location = new System.Drawing.Point(342, 571);
            this.btnPadPoint.Name = "btnPadPoint";
            this.btnPadPoint.Size = new System.Drawing.Size(90, 90);
            this.btnPadPoint.TabIndex = 255;
            this.btnPadPoint.Text = ".";
            // 
            // btnPadBackSpace
            // 
            this.btnPadBackSpace.AllowFocus = false;
            this.btnPadBackSpace.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadBackSpace.Appearance.Options.UseFont = true;
            this.btnPadBackSpace.Location = new System.Drawing.Point(342, 475);
            this.btnPadBackSpace.Name = "btnPadBackSpace";
            this.btnPadBackSpace.Size = new System.Drawing.Size(90, 90);
            this.btnPadBackSpace.TabIndex = 250;
            this.btnPadBackSpace.Text = "←";
            // 
            // btnPadClear
            // 
            this.btnPadClear.AllowFocus = false;
            this.btnPadClear.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPadClear.Appearance.Options.UseFont = true;
            this.btnPadClear.Location = new System.Drawing.Point(342, 284);
            this.btnPadClear.Name = "btnPadClear";
            this.btnPadClear.Size = new System.Drawing.Size(90, 90);
            this.btnPadClear.TabIndex = 241;
            this.btnPadClear.Text = "CLS";
            // 
            // btnPad000
            // 
            this.btnPad000.AllowFocus = false;
            this.btnPad000.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad000.Appearance.Options.UseFont = true;
            this.btnPad000.Location = new System.Drawing.Point(246, 571);
            this.btnPad000.Name = "btnPad000";
            this.btnPad000.Size = new System.Drawing.Size(90, 90);
            this.btnPad000.TabIndex = 254;
            this.btnPad000.Text = "000";
            // 
            // btnPad00
            // 
            this.btnPad00.AllowFocus = false;
            this.btnPad00.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad00.Appearance.Options.UseFont = true;
            this.btnPad00.Location = new System.Drawing.Point(150, 571);
            this.btnPad00.Name = "btnPad00";
            this.btnPad00.Size = new System.Drawing.Size(90, 90);
            this.btnPad00.TabIndex = 253;
            this.btnPad00.Text = "00";
            // 
            // btnPad0
            // 
            this.btnPad0.AllowFocus = false;
            this.btnPad0.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad0.Appearance.Options.UseFont = true;
            this.btnPad0.Location = new System.Drawing.Point(54, 571);
            this.btnPad0.Name = "btnPad0";
            this.btnPad0.Size = new System.Drawing.Size(90, 90);
            this.btnPad0.TabIndex = 252;
            this.btnPad0.Text = "0";
            // 
            // btnPad3
            // 
            this.btnPad3.AllowFocus = false;
            this.btnPad3.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad3.Appearance.Options.UseFont = true;
            this.btnPad3.Location = new System.Drawing.Point(246, 475);
            this.btnPad3.Name = "btnPad3";
            this.btnPad3.Size = new System.Drawing.Size(90, 90);
            this.btnPad3.TabIndex = 249;
            this.btnPad3.Text = "3";
            // 
            // btnPad2
            // 
            this.btnPad2.AllowFocus = false;
            this.btnPad2.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad2.Appearance.Options.UseFont = true;
            this.btnPad2.Location = new System.Drawing.Point(150, 475);
            this.btnPad2.Name = "btnPad2";
            this.btnPad2.Size = new System.Drawing.Size(90, 90);
            this.btnPad2.TabIndex = 248;
            this.btnPad2.Text = "2";
            // 
            // btnPad1
            // 
            this.btnPad1.AllowFocus = false;
            this.btnPad1.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad1.Appearance.Options.UseFont = true;
            this.btnPad1.Location = new System.Drawing.Point(54, 475);
            this.btnPad1.Name = "btnPad1";
            this.btnPad1.Size = new System.Drawing.Size(90, 90);
            this.btnPad1.TabIndex = 247;
            this.btnPad1.Text = "1";
            // 
            // btnPad6
            // 
            this.btnPad6.AllowFocus = false;
            this.btnPad6.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad6.Appearance.Options.UseFont = true;
            this.btnPad6.Location = new System.Drawing.Point(246, 380);
            this.btnPad6.Name = "btnPad6";
            this.btnPad6.Size = new System.Drawing.Size(90, 90);
            this.btnPad6.TabIndex = 245;
            this.btnPad6.Text = "6";
            // 
            // btnPad5
            // 
            this.btnPad5.AllowFocus = false;
            this.btnPad5.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad5.Appearance.Options.UseFont = true;
            this.btnPad5.Location = new System.Drawing.Point(150, 380);
            this.btnPad5.Name = "btnPad5";
            this.btnPad5.Size = new System.Drawing.Size(90, 90);
            this.btnPad5.TabIndex = 244;
            this.btnPad5.Text = "5";
            // 
            // btnPad4
            // 
            this.btnPad4.AllowFocus = false;
            this.btnPad4.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad4.Appearance.Options.UseFont = true;
            this.btnPad4.Location = new System.Drawing.Point(54, 380);
            this.btnPad4.Name = "btnPad4";
            this.btnPad4.Size = new System.Drawing.Size(90, 90);
            this.btnPad4.TabIndex = 243;
            this.btnPad4.Text = "4";
            // 
            // btnPad9
            // 
            this.btnPad9.AllowFocus = false;
            this.btnPad9.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad9.Appearance.Options.UseFont = true;
            this.btnPad9.Location = new System.Drawing.Point(246, 284);
            this.btnPad9.Name = "btnPad9";
            this.btnPad9.Size = new System.Drawing.Size(90, 90);
            this.btnPad9.TabIndex = 240;
            this.btnPad9.Text = "9";
            // 
            // btnPad8
            // 
            this.btnPad8.AllowFocus = false;
            this.btnPad8.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad8.Appearance.Options.UseFont = true;
            this.btnPad8.Location = new System.Drawing.Point(150, 284);
            this.btnPad8.Name = "btnPad8";
            this.btnPad8.Size = new System.Drawing.Size(90, 90);
            this.btnPad8.TabIndex = 239;
            this.btnPad8.Text = "8";
            // 
            // btnPad7
            // 
            this.btnPad7.AllowFocus = false;
            this.btnPad7.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSizeKeypad);
            this.btnPad7.Appearance.Options.UseFont = true;
            this.btnPad7.Location = new System.Drawing.Point(54, 284);
            this.btnPad7.Name = "btnPad7";
            this.btnPad7.Size = new System.Drawing.Size(90, 90);
            this.btnPad7.TabIndex = 238;
            this.btnPad7.Text = "7";
            // 
            // swFavorite
            // 
            this.swFavorite.Location = new System.Drawing.Point(422, 5);
            this.swFavorite.Name = "swFavorite";
            this.swFavorite.Properties.AllowFocused = false;
            this.swFavorite.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F);
            this.swFavorite.Properties.Appearance.Options.UseFont = true;
            this.swFavorite.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.swFavorite.Properties.OffText = "♡";
            this.swFavorite.Properties.OnText = "♥";
            this.swFavorite.Size = new System.Drawing.Size(150, 40);
            this.swFavorite.TabIndex = 266;

            panelKeypadItem.SuspendLayout();

            txtPadItem.Parent = panelKeypadItem;
            txtPadQty.Parent = panelKeypadItem;

            btnPadPrice.Parent = panelKeypadItem;
            btnPadCancel.Parent = panelKeypadItem;
            btnPadDelItem.Parent = panelKeypadItem;
            btnPadInit.Parent = panelKeypadItem;
            btnPadMinus.Parent = panelKeypadItem;
            txtItemDescrip.Parent = panelKeypadItem;
            btnPadEA.Parent = panelKeypadItem;
            btnPadConfirm.Parent = panelKeypadItem;
            btnPadItemUnit4.Parent = panelKeypadItem;
            btnPadItemUnit3.Parent = panelKeypadItem;
            btnPadItemUnit2.Parent = panelKeypadItem;
            btnPadItemUnit1.Parent = panelKeypadItem;
            btnPadPoint.Parent = panelKeypadItem;
            btnPadBackSpace.Parent = panelKeypadItem;
            btnPadClear.Parent = panelKeypadItem;
            btnPad000.Parent = panelKeypadItem;
            btnPad00.Parent = panelKeypadItem;
            btnPad0.Parent = panelKeypadItem;
            btnPad3.Parent = panelKeypadItem;
            btnPad2.Parent = panelKeypadItem;
            btnPad1.Parent = panelKeypadItem;
            btnPad6.Parent = panelKeypadItem;
            btnPad5.Parent = panelKeypadItem;
            btnPad4.Parent = panelKeypadItem;
            btnPad9.Parent = panelKeypadItem;
            btnPad8.Parent = panelKeypadItem;
            btnPad7.Parent = panelKeypadItem;
            separatorControlKeypad1.Parent = panelKeypadItem;
            separatorControlKeypad2.Parent = panelKeypadItem;
            swFavorite.Parent = panelKeypadItem;
            panelKeypadItem.ResumeLayout(false);

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
        }
        #endregion Init Keypad panel

        private void InitEvent()
        {
            //마우스 커서 숨기기
            //ShowCursor(false);
            Load += FormLoad;

            #region Keypad NUM
            btnKey00.Click += BtnKeyAlphabet_Click;
            btnKey0.Click += BtnKeyAlphabet_Click;
            btnKey1.Click += BtnKeyAlphabet_Click;
            btnKey2.Click += BtnKeyAlphabet_Click;
            btnKey3.Click += BtnKeyAlphabet_Click;
            btnKey4.Click += BtnKeyAlphabet_Click;
            btnKey5.Click += BtnKeyAlphabet_Click;
            btnKey6.Click += BtnKeyAlphabet_Click;
            btnKey7.Click += BtnKeyAlphabet_Click;
            btnKey8.Click += BtnKeyAlphabet_Click;
            btnKey9.Click += BtnKeyAlphabet_Click;

            btnKeyClear.Click += BtnKeyClear_Click;
            btnKeyBackSpace.Click += BtnKeyBackSpace_Click;
            btnKeyMinus.Click += BtnKeyMinus_Click;
            btnKeyPoint.Click += BtnKeyPoint_Click;
            #endregion

            #region 우측하단 컨트롤 버튼
            btnCtrPayDeposit.Click += BtnCtrPayDeposit_Click;               //입금
            btnCtrPayNon.Click += BtnCtrPayNon_Click;                       //외상
            btnCtrPayCredit.Click += BtnCtrPayCredit_Click;                 //신용카드
            btnCtrPayCancel.Click += BtnCtrPayCancel_Click;                 //결제취소
            btnCtrPayDone.Click += BtnCtrPayDone_Click;                     //결제완료
            btnCtrFindItem.Click += BtnCtrFindItem_Click;                   //상품찾기
            btnCtrDiscount.Click += BtnCtrDiscount_Click;                   //할인
            btnCtrDiscountCancel.Click += BtnCtrDiscountCancel_Click;       //할인취소
            btnCtrCancel.Click += BtnCtrCancel_Click;                       //작업취소
            btnCtrTmpSave.Click += BtnCtrTmpSave_Click;                     //임시저장
            btnCtrClose.Click += BtnCtrClose_Click;                         //종료
            btnCtrReturn.Click += BtnCtrReturn_Click;                       //반품
            btnCtrModify.Click += BtnCtrModify_Click;                       //수정
            btnCtrPrint.Click += BtnCtrPrint_Click;                            //영수증 재출력
            btnCtrReorder.Click += BtnCtrReorder_Click;                     //재주문
            #endregion

            #region Contents 패널 버튼
            btnContentsSearch.Click += BtnCtsCustomerSearch_Click;             //Contents 검색
            btnFavorite.Click += BtnCtsFavorite_Click;                         //즐겨찾기
            btnInit.Click += BtnCtsInit_Click;                                 //초기화버튼
            btnItemType.Click += BtnCtsItemType_Click;                         //대분류 버튼


            btnTop.Click += BtnCtsTop_Click;                                   //상위 버튼
            btnPre.Click += BtnCtsPre_Click;                                   //이전 버튼
            btnNext.Click += BtnCtsNext_Click;                                 //다음 버튼
            btnClose.Click += BtnCtsClose_Click;                               //컨텐츠 닫기

            navBottom.SelectedPageChanged += NavBottom_SelectedPageChanged;
            #endregion Contents 패널 버튼

            #region 그리드 조작 버튼
            btnGridUp.Click += BtnGridUp_Click;
            btnGridUpMax.Click += BtnGridUpMax_Click;
            btnGridDown.Click += BtnGridDown_Click;
            btnGridDownMax.Click += BtnGridDownMax_Click;
            #endregion 그리드 조작 버튼

            #region 대분류 패널
            btnItemTypeAdd.Click += BtnItemTypeAdd_Click;
            btnItemTypeDel.Click += BtnItemTypeDel_Click;
            #endregion 대분류 패널

            #region 메인 상단 버튼
            btnSalePre.Click += BtnMainSalePre_Click;                           //이전판매
            btnSaleNext.Click += BtnMainSaleNext_Click;                         //다음판매
            #endregion 메인 상단 버튼

            #region Grid 관련 
            gridViewItem.InitNewRow += GridView_InitNewRow;
            gridViewItem.DoubleClick += GridViewItem_DoubleClick;
            gridViewItem.RowCountChanged += GridViewItem_RowCountChanged;
            gridViewItem.FocusedRowChanged += GridViewItem_FocusedRowChanged;

            splitMain.SplitterPositionChanged += SplitMain_SplitterPositionChanged;

            //gridViewItem.CustomDrawRowIndicator += GridViewItem_CustomDrawRowIndicator;
            //gridViewItem.CustomDrawColumnHeader += GridViewItem_CustomDrawColumnHeader;
            gridViewItem.OptionsView.ShowGroupPanel = false;
            //gridViewItem.OptionsView.ColumnAutoWidth = false;
            gridViewItem.OptionsView.ShowAutoFilterRow = false;
            gridViewItem.OptionsCustomization.AllowSort = true;
            gridViewItem.OptionsCustomization.AllowFilter = false;

            //그리드 금액 합계 표기
            gridViewItem.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM"].SummaryItem.DisplayFormat = "합계 : {0:##,##0.####}";

            //gridViewItem.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "AM", "{0:#,###}");


            gridViewItem.Columns["AM_VAT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_VAT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            //그리드 높이
            gridViewItem.UserCellPadding = new Padding(0, 3, 0, 3);
            #endregion Grid 관련 

            dtpSale.DateTimeChanged += DtpSale_DateTimeChanged;
            panelKeypadMain.VisibleChanged += PanelKeypad_VisibleChanged;
            panelKeypadItem.VisibleChanged += PanelKeypadItem_VisibleChanged;

            //키패드 이벤트 모음
            InitKeypadEvent();

            //panelKeypadItem.Dock = DockStyle.Fill;// = panelPayInfo.Size;
            panelContents.Dock = DockStyle.Fill;//.Size = panelPayInfo.Size;
            flowLayoutPanelContents.SizeChanged += FlowLayoutPanelContents_SizeChanged;
        }

        private void FlowLayoutPanelContents_SizeChanged(object sender, EventArgs e)
        {
            int btnWidth = (flowLayoutPanelContents.Width - (ColQty * 2 * 3)) / ColQty;
            int btnHeight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((flowLayoutPanelContents.Height - (RowQty * 2 * 3)) / Convert.ToDouble(RowQty))));

            for (int i = 0; i < ScreenQty; i++)
            {
                _contentsBtn[i].Size = new Size(btnWidth, btnHeight);
            }
        }

        public int MaxRowCount
        {
            get { return (gridItem.Height - 80) / 39; }
        }

        private void SplitMain_SplitterPositionChanged(object sender, EventArgs e)
        {
            //MaxRowCount = (gridItem.Height - 80) / 39;
            GridNavVisible();
        }

        private int ClickRowHandle;


        private int _itemTypeQty = 0;

        private int ItemTypeQty
        {
            get { return _itemTypeQty; }
            set
            {
                _itemTypeQty = value;
                if (IsItemTypeScreen)
                    splitMain.SplitterPosition = ItemTypeQty * 68;
                else
                    splitMain.SplitterPosition = 204;
            }
        }

        private bool IsWorkCancelQuestion { get; set; }

        private bool IsFavorite { get; set; } = false;

        private bool IsSlipInputing { get; set; } = false;

        private bool _lastSlipYN = false;
        public bool LastSlipYN
        {
            get
            {
                return _lastSlipYN;
            }
            set
            {
                _lastSlipYN = value;
                btnSaleNext.Enabled = value == true ? false : true;
            }
        }

        private InitButtonMode InitBtnMode { get; set; } = InitButtonMode.Normal;

        private void FormActivated(object sender, EventArgs e)
        {
            InitConfig();
        }

        //영수증 변수 모음
        int RptReceitSpace = 0;
        int RptReceitCount = 1;
        int RptWorkSheetSpace = 0;
        int RptWorkSheetCount = 1;
        bool RptWorkSheetMinus = true;

        string RptSaleTime;

        int RptReceitNo = 2;

        TempOrderType TmpOrderType { get; set; } = TempOrderType.Order;
        ContentsItem ContentsItemType { get; set; } = ContentsItem.Item;
        private void InitConfig()
        {
            //20200706 Bifrost.Win POSFormBase에 통합
            ////프린터포트
            //POSConfig configPrintPort = POSConfigHelper.GetConfig("PRT001");
            //PrintPort = configPrintPort.ConfigValue;

            // 영수증 하단 여백 설정
            Bifrost.Helper.POSConfig cfgReceitSpace = POSConfigHelper.GetConfig("RPT004");
            RptReceitSpace = A.GetInt(cfgReceitSpace.ConfigValue);
            // 영수증 출력 장수설정
            Bifrost.Helper.POSConfig cfgReceitCount = POSConfigHelper.GetConfig("RPT005");
            RptReceitCount = A.GetInt(cfgReceitCount.ConfigValue);

            //영수증 판매시간 / 판매일 설정
            Bifrost.Helper.POSConfig cfgPrintDate = POSConfigHelper.GetConfig("RPT002");
            RptSaleTime = cfgPrintDate.ConfigValue;

            //작업서 마이너스 수량 안보이게 설정
            Bifrost.Helper.POSConfig cfgWorkSheet = POSConfigHelper.GetConfig("RPT010");
            RptWorkSheetMinus = A.GetString(cfgWorkSheet.ConfigValue) == "Y" ? true : false;

            // 작업서 하단 여백 설정
            Bifrost.Helper.POSConfig cfgWorkSheetSpace = POSConfigHelper.GetConfig("RPT011");
            RptWorkSheetSpace = A.GetInt(cfgWorkSheetSpace.ConfigValue);
            // 작업서 출력매수 설정
            Bifrost.Helper.POSConfig cfgWorkSheetCount = POSConfigHelper.GetConfig("RPT012");
            RptWorkSheetCount = A.GetInt(cfgWorkSheetCount.ConfigValue);

            Bifrost.Helper.POSConfig cfgReceitNo = POSConfigHelper.GetConfig("RPT006");
            RptReceitNo = A.GetInt(cfgReceitNo.ConfigValue);

            //좌우 바뀜 디폴트는 L임
            POSConfig configScreen = POSConfigHelper.GetConfig("POS001");

            if (configScreen.ConfigValue == "R")
            {
                splitContainerMain.SplitterPosition = splitContainerMain.Panel2.Width;
                splitContainerMain.SwapPanels();

                panelTopInfo.Dock = DockStyle.Left;
            }

            //그리드 위아래 움직이는 버튼 디폴트는 Y임
            //20191219 자동 설정으로 변경
            //POSConfig configGridNav = POSConfigHelper.GetConfig("POS002");

            //if (configGridNav.ConfigValue == "Y")
            //{
            //    panelGridNav.Visible = true;
            //}
            //else
            //{
            //    panelGridNav.Visible = false;
            //}

            //디폴트는 Y임
            POSConfig configItemTypeQty = POSConfigHelper.GetConfig("POS003");
            //splitMain.SplitterPosition = A.GetInt(configItemTypeQty.ConfigValue) * 67;
            ItemTypeQty = A.GetInt(configItemTypeQty.ConfigValue);

            //부가세 설정 화면 보이기 디폴트는 N임
            POSConfig configVatYN = POSConfigHelper.GetConfig("POS004");

            if (A.GetString(configVatYN.ConfigValue) == "Y")
            {
                panelSaleVatAmt.Visible = true;
                //20191206 합계는 나오게
                //panelSaleSumAmt.Visible = true;
            }
            else
            {
                panelSaleVatAmt.Visible = false;
                //20191206 합계는 나오게
                //panelSaleSumAmt.Visible = false;
            }

            //대분류 상하버튼 화면 보이기 디폴트는 N임
            POSConfig configTypeUpDownBtnYN = POSConfigHelper.GetConfig("POS006");

            if (A.GetString(configTypeUpDownBtnYN.ConfigValue) == "Y")
            {
                panelUpDown.Visible = true;

                btnItemTypeAdd.Visible = true;
                btnItemTypeDel.Visible = true;
            }
            else
            {
                panelUpDown.Visible = false;
            }

            //매출액 그리드에 있어서 일단 숨김
            panelSaleAmt.Visible = true;

            //작업취소시 확인여부 기본은 N
            POSConfig configWorkCancelYN = POSConfigHelper.GetConfig("POS007");
            if (A.GetString(configWorkCancelYN.ConfigValue) == "Y")
            {
                IsWorkCancelQuestion = true;
            }
            else
            {
                IsWorkCancelQuestion = false;
            }

            //터치 화면 설정(대분류 / 상품)
            Bifrost.Helper.POSConfig cfgScreen = POSConfigHelper.GetConfig("POS010");
            ContentsItemType = cfgScreen.ConfigValue == "I" ? ContentsItem.Item : ContentsItem.ItemType;

            //초기화버튼 설정 POS014 C : 거래처 / N :원래대로 선택되어있는 
            POSConfig configInitBtn = POSConfigHelper.GetConfig("POS014");
            InitBtnMode = configInitBtn.ConfigValue == "C" ? InitButtonMode.Customer : InitButtonMode.Normal;

            //글꼴 크기
            Bifrost.Helper.POSConfig cfgFontSize = POSConfigHelper.GetConfig("POS015");

            fontSizeContents = A.GetFloat(cfgFontSize.ConfigValue);
            fontSizeMain = A.GetFloat(cfgFontSize.ConfigValue);

            FontDefault = new Font("카이겐고딕 KR Regular", fontSizeContents);
            FontDeal = new Font("카이겐고딕 KR Regular", fontSizeContents, FontStyle.Bold);
            FontDealToday = new Font("카이겐고딕 KR Regular", fontSizeContents, FontStyle.Bold | FontStyle.Underline);
            FontMain = new Font("카이겐고딕 KR Regular", fontSizeMain);
            FontContents = new Font("카이겐고딕 KR Regular", fontSizeContents);

            //dudtn

            #region 메인 키패드 폰트 사이즈

            lblSaleInfoPreAmt.Font = FontMain;
            lblSaleInfoVat.Font = FontMain;
            lblSaleInfoSaleAmt.Font = FontMain;
            lblSaleInfoDiscountAmt.Font = FontMain;
            lblSaleInfoGroupSumAmt.Font = FontMain;
            lblSaleInfoTotalAmt.Font = FontMain;
            lblInputAmt.Font = FontMain;
            labelControl13.Font = FontMain;
            lblCreditCardAmt.Font = FontMain;
            labelControl14.Font = FontMain;
            lblTotalNonpaidAmt.Font = FontMain;
            labelControl15.Font = FontMain;

            btnKey0.Font = FontKeypad;
            btnKey00.Font = FontKeypad;
            btnKey1.Font = FontKeypad;
            btnKey2.Font = FontKeypad;
            btnKey3.Font = FontKeypad;
            btnKey4.Font = FontKeypad;
            btnKey5.Font = FontKeypad;
            btnKey6.Font = FontKeypad;
            btnKey7.Font = FontKeypad;
            btnKey8.Font = FontKeypad;
            btnKey9.Font = FontKeypad;
            btnKeyClear.Font = FontKeypad;
            btnKeyBackSpace.Font = FontKeypad;
            btnKeyMinus.Font = FontKeypad;
            btnKeyPoint.Font = FontKeypad;

            btnCtrPayDeposit.Font = FontMain;
            btnCtrPayCredit.Font = FontMain;
            btnCtrPayNon.Font = FontMain;
            btnCtrPayCancel.Font = FontMain;
            btnCtrPayDone.Font = FontMain;
            btnCtrDiscountCancel.Font = FontMain;
            btnCtrDiscount.Font = FontMain;

            btnCtrFindItem.Font = FontMain;
            btnCtrModify.Font = FontMain;
            btnCtrTmpSave.Font = FontMain;
            btnCtrPrint.Font = FontMain;
            btnCtrCancel.Font = FontMain;
            btnCtrReturn.Font = FontMain;
            btnCtrReorder.Font = FontMain;
            btnCtrClose.Font = FontMain;
            #endregion 메인 키패드 폰트 사이즈

            #region 컨턴츠 폰트 사이즈
            btnTop.Font = FontContents;
            btnPre.Font = FontContents;
            btnNext.Font = FontContents;
            btnClose.Font = FontContents;
            btnInit.Font = FontContents;
            btnContentsSearch.Font = FontContents;
            btnFavorite.Font = FontContents;
            btnItemType.Font = FontContents;

            //for (int i = 0; i < ScreenQty; i++)
            //{
            //    _contentsBtn[i].Font = FontContents;
            //    _ItemTypeBtn[i].Font = FontContents;
            //}
            #endregion 컨턴츠 폰트 사이즈

            //영수증설정
            Bifrost.Helper.POSConfig cfgReceit = POSConfigHelper.GetConfig("RPT003");

            switch (cfgReceit.ConfigValue)
            {
                case "D":
                    ReceitType = ReceitMode.Double;
                    break;
                case "S":
                    ReceitType = ReceitMode.Single;
                    break;
                default:
                    ReceitType = ReceitMode.Etc;
                    break;
            }


            ////전표 이전 다음 클릭시 다시 최초로 돌아왔을때 날짜 설정
            Bifrost.Helper.POSConfig cfgPreNextDt = POSConfigHelper.GetConfig("POS016");
            IsFixSaleDt = cfgPreNextDt.ConfigValue == "Y" ? true : false;

            Bifrost.Helper.POSConfig cfgTmpOrder = POSConfigHelper.GetConfig("POS017");
            if(cfgTmpOrder.ConfigValue == "O")
            {
                TmpOrderType = TempOrderType.Order;
            }
            else if (cfgTmpOrder.ConfigValue == "C")
            {
                TmpOrderType = TempOrderType.Customer;
            }

            //임시저장 작업서 바로 출력 설정
            Bifrost.Helper.POSConfig cfgTmpPrintDirect = POSConfigHelper.GetConfig("POS018");
            TmpPrintDirect = cfgTmpPrintDirect.ConfigValue;

            //임시저장 작업서 신규만 출력
            Bifrost.Helper.POSConfig cfgTmpPrintNew = POSConfigHelper.GetConfig("POS019");
            TmpPrintNewItem = cfgTmpPrintNew.ConfigValue;

            //신용카드 여부
            Bifrost.Helper.POSConfig cfgUseCredit = POSConfigHelper.GetConfig("POS020");
            UseCreditCard = cfgUseCredit.ConfigValue == "Y" ? true : false;

            int panelPayHeight = 0;

            if (UseCreditCard)
            {
                panelPay02.Visible = true;
                panelPaymentCredit.Visible = true;

                panelPayHeight = (panelPay.Height - (3 * 2 * 5)) / 5;

                panelPay01.Height = panelPayHeight;
                panelPay02.Height = panelPayHeight;
                panelPay03.Height = panelPayHeight;
                panelPay04.Height = panelPayHeight;
                panelPay05.Height = panelPayHeight;
            }
            else
            {
                panelPay02.Visible = false;
                panelPaymentCredit.Visible = false;

                panelPayHeight = (panelPay.Height - (3 * 2 * 4)) / 4;

                panelPay01.Height = panelPayHeight;
                panelPay03.Height = panelPayHeight;
                panelPay04.Height = panelPayHeight;
                panelPay05.Height = panelPayHeight;
            }
        }
        string TmpPrintDirect = string.Empty;
        string TmpPrintNewItem = string.Empty;
        bool UseCreditCard = false;
        
        private bool IsFixSaleDt { get; set; } = true;

        ReceitMode ReceitType { get; set; } = ReceitMode.Double;

        private void FormLoad(object sender, EventArgs e)
        {
            InitForm();

            InitConfig();
            Activated += FormActivated;

        }

        private void PanelKeypad_VisibleChanged(object sender, EventArgs e)
        {

            btnPadDelItem.Visible = false; //기본은 삭제 버튼 안보임

            //txtItem.Text = KeypadItemName;
            //txtItemDescrip.Text = ItemDescript;
            //SetResultText();
            ////txtQty.Text = A.GetString(ItemPrice);
            //GetItemUnit();

            //if (KeyMode == Mode.Modify)
            //{
            //    btnPadDelItem.Visible = true;
            //}
            //else
            //{
            //    txtItem.Size = new Size(324, 30);
            //}

            //IsTopVisible = false;
            //if (IsTopVisible == false)
            //{
            //    Size = new Size(399, 553);
            //}
        }
        private void PanelKeypadItem_VisibleChanged(object sender, EventArgs e)
        {
            //if (panelKeypadItem.Visible == true)
            //{
            //    PadQty = 0;
            //    PadQtyUnit = 0;
            //    PadItemPrice = 0;
            //    PadItemDescript = string.Empty;
            //    PadUnitCode = string.Empty;
            //    PadUnitName = string.Empty;
            //    SetResultText();
            //}
            //panelKeypadItem.Dock = DockStyle.Fill;
            //panelKeypadItem.Appearance.BackColor = Color.FromArgb(50, 0, 0, 0);
            //panelKeypadItem.Appearance.BackColor2 = Color.FromArgb(50, 0, 0, 0);
            //panelKeypadItem.Appearance.Options.UseBackColor = true;
            //panelKeypadItem.LookAndFeel.UseDefaultLookAndFeel = false;
            //panelKeypadItem.LookAndFeel.Style = LookAndFeelStyle.Flat;
            //panelKeypadItem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //panelKeypadItem.BackColor = Color.FromArgb(50, 0, 0, 0);
        }
        private void NavBottom_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (navBottom.SelectedPage == navPageSaleInfo)
                splitMain.SplitterPosition = 205;
            else
                splitMain.SplitterPosition = ItemTypeQty * 68;
        }
        private void DtpSale_DateTimeChanged(object sender, EventArgs e)
        {
            //외상 금액 떄문에 거래처 재검색
            _dtCust = SearchCust(new object[] { POSGlobal.StoreCode, string.Empty, "Y", dtpSale.Text });
            SetContents();
            SetNaviButton();

            //현재날짜 기준으로 앞뒤 전표 가져오기
            CheckPreNextSlipNoByDate();
        }

        #region 메인화면 버튼 이벤트
        private void BtnMainSaleNext_Click(object sender, EventArgs e)
        {
            if (txtCust.Text == string.Empty)
            {
                ShowMessageBoxA("먼저 거래처를 선택해 주세요.", MessageType.Warning);
                return;
            }

            //20191202 메세지 삭제 마지막은 그냥 화면 표시
            //if (_salesNoNext == string.Empty)
            //{
            //    ShowMessageBoxA("마지막 거래입니다.", MessageType.Information);
            //    return;
            //}

            if (SalesNoNext == string.Empty)
                TempSlipView();
            //else if (_salesNoNext == string.Empty && LastSlipYN == false)
            //    return;
            else
                Search(SalesNoNext);
        }

        private void BtnMainSalePre_Click(object sender, EventArgs e)
        {
            if (txtCust.Text == string.Empty)
            {
                ShowMessageBoxA("먼저 거래처를 선택해 주세요.", MessageType.Warning);
                return;
            }

            if (SalesNoPre == string.Empty)
            {
                ShowMessageBoxA("최초 거래입니다.", MessageType.Information);
                return;
            }

            //신규일때 최초만 저장
            if (JobStatus == JobMode.New)
                TempSlipSave();

            Search(SalesNoPre);
        }
        #endregion 메인화면 버튼 이벤트

        #region 대분류패널 버튼 이벤트
        private void BtnItemTypeDel_Click(object sender, EventArgs e)
        {
            if (splitMain.SplitterPosition <= 205) return;

            panelUpDown.Hide();
            splitMain.SplitterPosition -= 67;
            panelUpDown.Show();
        }

        private void BtnItemTypeAdd_Click(object sender, EventArgs e)
        {
            panelUpDown.Hide();
            splitMain.SplitterPosition += 67;
            panelUpDown.Show();
        }
        #endregion 대분류패널 버튼 이벤트

        #region 컨텐츠패널 버튼 이벤트
        private void BtnCtsItemType_Click(object sender, EventArgs e)
        {
            if (IsItemTypeScreen)
                IsItemTypeScreen = false;
            else
                IsItemTypeScreen = true;
        }
        private void BtnCtsFavorite_Click(object sender, EventArgs e)
        {
            if (CustomerCode == string.Empty)
            {
                ShowMessageBoxA("먼저 거래처를 선택해 주세요!", MessageType.Warning);
                return;
            }

            if (IsFavorite)
            {
                InitItemTable();
                ContentsType = ContentsMode.Item;
                IsFavorite = false;
            }
            else
            {
                _dtItemFavorite = SearchItemFavorite(new object[] { POSGlobal.StoreCode, CustomerCode });
                ContentsType = ContentsMode.Favorite;
                IsFavorite = true;
            }
            SetContents();
            SetNaviButton();
        }
        private void BtnCtsInit_Click(object sender, EventArgs e)
        {
            switch (InitBtnMode)
            {
                case InitButtonMode.Customer:
                    if (gridViewItem.RowCount > 0)
                    {
                        if (ShowMessageBoxA("상품이 추가되어있습니다.\n취소하시겠습니까?", MessageType.Question) == DialogResult.Yes)
                        {
                            OnInsert();
                        }
                    }
                    else
                    {
                        OnInsert();
                    }
                    //ContentsType = ContentsMode.Customer;

                    //_dtCust = _dtCustAll;// SearchCust(new object[] { POSGlobal.StoreCode, string.Empty, "Y", dtpSale.Text });
                    //SetContents();
                    //SetNaviButton();
                    break;
                case InitButtonMode.Normal:
                    if (ContentsType == ContentsMode.Customer)
                    {
                        _dtCust = _dtCustAll;// SearchCust(new object[] { POSGlobal.StoreCode, string.Empty, "Y", dtpSale.Text });
                        SetContents();
                        SetNaviButton();
                    }
                    else
                    {
                        ContentsType = ContentsMode.Item;
                        InitItemTable();
                    }
                    break;
                default:
                    break;
            }
        }
        private void BtnCtsTop_Click(object sender, EventArgs e)
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
            navBottom.SelectedPage = navPageSaleInfo;

            //splitMain.SplitterPosition = 205;
            //SetItemTypeScreen(IsItemTypeScreen);
        }
        private void BtnCtsCustomerSearch_Click(object sender, EventArgs e)
        {
            //if (gridViewItem.RowCount > 0)
            //{
            //    ShowMessageBoxA("이미 등록된 상품이 있습니다.", MessageType.Warning);
            //    return;
            //}

            P_POS_INITIAL P_POS_INITIAL = new P_POS_INITIAL();
            if (txtCust.Text == string.Empty)
                P_POS_INITIAL.ContentsType = ContentsMode.Customer;
            else
                P_POS_INITIAL.ContentsType = ContentsMode.Item;

            if (P_POS_INITIAL.ShowDialog() == DialogResult.OK)
            {
                ContentsType = P_POS_INITIAL.ContentsType;// ContentsMode.Customer;
                if (ContentsType == ContentsMode.Customer)
                {
                    if (P_POS_INITIAL.ResultText == string.Empty)
                        _dtCust = _dtCustAll;
                    else
                        _dtCust = SearchCust(new object[] { POSGlobal.StoreCode, P_POS_INITIAL.ResultText, "Y", dtpSale.Text });

                    //SetContents(_dtCust);
                    //SetNaviButton(_dtCust);
                }
                else
                {
                    if (P_POS_INITIAL.ResultText == string.Empty) //전체검색
                        _dtItem = SearchItem(new object[] { POSGlobal.StoreCode, P_POS_INITIAL.ResultText, string.Empty, string.Empty });
                    else
                        _dtItem = SearchItem(new object[] { POSGlobal.StoreCode, P_POS_INITIAL.ResultText, "ALL", string.Empty });

                    //SetContents(_dtItem);
                    //SetNaviButton(_dtItem);
                    ContentsLv = 1;
                }
                SetContents();
                SetNaviButton();
                //SetItemTypeScreen(IsItemTypeScreen);
                IsContentScreen = true;
            }
        }
        #endregion 컨텐츠패널 버튼 이벤트

        #region 우측하단 컨트롤패널버튼 이벤트 모음
        private void BtnCtrClose_Click(object sender, EventArgs e)
        {
            //OnHomeClick();

            if (ShowMessageBoxA(this.MenuData.MenuName +  "을 닫으시겠습니까?", MessageType.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
        private void BtnCtrTmpSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if(SlipNoTemp!= null) SlipNoTemp.Clear();

                //if(VerifyCheck(sender) == false) return;
                if (SlipFlag != "T")
                {
                    if (JobStatus == JobMode.Read || JobStatus == JobMode.Modify)
                    {
                        ShowMessageBoxA("이미 거래완료된 건은 임시저장이 불가능합니다.", MessageType.Warning);
                        return;
                    }
                }

                decimal payAmt = A.GetDecimal(txtClsPayAmt.EditValue);
                decimal creditAmt = A.GetDecimal(txtClsCreditAmt.EditValue);
                decimal nonPayAmt = A.GetDecimal(txtClsNonPaidAmt.EditValue);

                if (payAmt != 0 && creditAmt != 0 && nonPayAmt != 0)
                {
                    ShowMessageBoxA("결제정보가 등록된 건은 임시저장 할수 없습니다.", MessageType.Warning);
                    return;
                }
                string SaleDt = dtpSale.Text;

                //로우가 없을경우 불러오기
                if (gridViewItem.RowCount == 0)
                {
                    P_POS_ORDER_TMP P_POS_ORDER_TMP = new P_POS_ORDER_TMP();
                    P_POS_ORDER_TMP.OrderType = TmpOrderType;
                    P_POS_ORDER_TMP.SlipDt = SaleDt;
                    P_POS_ORDER_TMP.CustomerCode = CustomerCode;
                    P_POS_ORDER_TMP.CustomerName = CustomerName;
                    P_POS_ORDER_TMP.SlipType = "S";

                    P_POS_ORDER_TMP.AutoSearch = true;


                    if (P_POS_ORDER_TMP.ShowDialog() == DialogResult.OK)
                    {
                        DataRow returnRow = (DataRow)P_POS_ORDER_TMP.ReturnData["ReturnData"];

                        if (returnRow != null)
                        {
                            DataSet _dsTmp;
                            switch (TmpOrderType)
                            {
                                case TempOrderType.Order:
                                    if(SlipNoTemp != null) SlipNoTemp.Clear();
                                    SlipNoTemp = new List<string>();
                                    SlipNoTemp.Add(A.GetString(returnRow["NO_SLIP"]));
                                    //Search(A.GetString(returnRow["NO_SO"]));

                                    #region 행추가
                                    _dsTmp = SearchTempOrderByOrder(new object[] { POSGlobal.StoreCode, A.GetString(returnRow["NO_SLIP"]), "S" });

                                    if (_dsTmp.Tables[1].Rows.Count > 0)
                                    {
                                        gridViewItem.BeginUpdate();

                                        for (int i = 0; i < _dsTmp.Tables[1].Rows.Count; i++)
                                        {
                                            _itemCode = A.GetString(_dsTmp.Tables[1].Rows[i]["CD_ITEM"]);
                                            _itemName = A.GetString(_dsTmp.Tables[1].Rows[i]["NM_ITEM"]);
                                            _Qt = A.GetDecimal(_dsTmp.Tables[1].Rows[i]["QT"]);
                                            _QtUnit = A.GetDecimal(_dsTmp.Tables[1].Rows[i]["QT_UNIT"]);
                                            _itemPrice = A.GetDecimal(_dsTmp.Tables[1].Rows[i]["UM"]);
                                            _itemDescript = A.GetString(_dsTmp.Tables[1].Rows[i]["DC_ITEM"]);
                                            _unitCode = A.GetString(_dsTmp.Tables[1].Rows[i]["CD_UNIT"]);
                                            _unitName = A.GetString(_dsTmp.Tables[1].Rows[i]["NM_UNIT"]);
                                            _vatType = A.GetString(_dsTmp.Tables[1].Rows[i]["FG_VAT"]);
                                            _vatRate = A.GetVatRate(_vatType);
                                            _TmpLastFlag = "OLD";

                                            gridViewItem.AddNewRow();
                                        }

                                        gridViewItem.EndUpdate();

                                        gridViewItem.BestFitColumns();
                                        gridViewItem.UpdateCurrentRow();
                                        gridViewItem.UpdateSummary();

                                    }
                                    #endregion

                                    //SlipFlag = A.GetString(_dsTmp.Tables[0].Rows[0]["FG_SO"]);
                                    CustomerCode = A.GetString(_dsTmp.Tables[0].Rows[0]["CD_CUST"]);
                                    CustomerName = A.GetString(_dsTmp.Tables[0].Rows[0]["NM_CUST"]);
                                    dtpSale.Text = A.GetString(_dsTmp.Tables[0].Rows[0]["DT_SLIP"]);

                                    break;
                                case TempOrderType.Customer:
                                    CustomerCode = A.GetString(returnRow["CD_CUST"]);
                                    dtpSale.Text = A.GetString(returnRow["DT_SLIP"]);

                                    #region 행추가
                                    _dsTmp = SearchTempOrderByCustomer(new object[] { POSGlobal.StoreCode, dtpSale.Text, CustomerCode, "S" });

                                    //SlipFlag = A.GetString(_dsTmp.Tables[0].Rows[0]["FG_SO"]);
                                    CustomerCode = A.GetString(_dsTmp.Tables[0].Rows[0]["CD_CUST"]);
                                    CustomerName = A.GetString(_dsTmp.Tables[0].Rows[0]["NM_CUST"]);
                                    //dtpSale.Text = A.GetString(_dsTmp.Tables[0].Rows[0]["DT_SO"]);

                                    SlipNoTemp = (from r in _dsTmp.Tables[0].AsEnumerable() select r.Field<string>("NO_SLIP").ToString()).ToList();

                                    if (_dsTmp.Tables[1].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < _dsTmp.Tables[1].Rows.Count; i++)
                                        {
                                            _itemCode = A.GetString(_dsTmp.Tables[1].Rows[i]["CD_ITEM"]);
                                            _itemName = A.GetString(_dsTmp.Tables[1].Rows[i]["NM_ITEM"]);
                                            _Qt = A.GetDecimal(_dsTmp.Tables[1].Rows[i]["QT"]);
                                            _QtUnit = A.GetDecimal(_dsTmp.Tables[1].Rows[i]["QT_UNIT"]);
                                            _itemPrice = A.GetDecimal(_dsTmp.Tables[1].Rows[i]["UM"]);
                                            _itemDescript = A.GetString(_dsTmp.Tables[1].Rows[i]["DC_ITEM"]);
                                            _unitCode = A.GetString(_dsTmp.Tables[1].Rows[i]["CD_UNIT"]);
                                            _unitName = A.GetString(_dsTmp.Tables[1].Rows[i]["NM_UNIT"]);
                                            _vatType = A.GetString(_dsTmp.Tables[1].Rows[i]["FG_VAT"]);
                                            _vatRate = A.GetVatRate(_vatType);
                                            _TmpLastFlag = "OLD";

                                            gridViewItem.AddNewRow();
                                        }
                                        gridViewItem.BestFitColumns();
                                        gridViewItem.UpdateCurrentRow();
                                        gridViewItem.UpdateSummary();
                                    }
                                    #endregion
                                    
                                    break;
                                default:
                                    break;
                            }

                            SetCustomerInfo();

                            decimal NonpaidAmt = A.GetDecimal(DBHelper.ExecuteScalar("USP_POS_GET_CUST_NONPAID_AMT", new object[] { POSGlobal.StoreCode, CustomerCode, string.Empty, dtpSale.Text }));
                            txtPreNonPaidAmt.EditValue = NonpaidAmt;

                            SetGridFooter();
                            CalcAmt(null);

                            txtNoSlip.Text = string.Empty;
                            JobStatus = JobMode.New; //수정으로 변경
                            
                            ContentsType = ContentsMode.Item;
                            //_dtItem = ContentsItemType == ContentsItem.ItemType ? _dtItemType : _dtItemTypeAll;
                            InitItemTable();

                            SetContents();
                            IsContentScreen = true;
                        }
                        if (P_POS_ORDER_TMP != null)
                        {
                            P_POS_ORDER_TMP.Dispose();
                        }
                    }
                }
                //로우가 있으면 저장
                else
                {
                    SlipFlag = "T"; //일반전표 : A, 반품 전표: R, 임시저장 전표 : T, 수금만 있는 전표 :P
                    #region 전표번호 채번
                    if (txtNoSlip.Text == string.Empty)
                    {
                        SlipNo = A.GetPOSSlipNo(POSGlobal.StoreCode, SaleDt, "TSO", 4);
                    }
                    else
                    {
                        SlipNo = txtNoSlip.Text;
                    }
                    #endregion 전표번호 채번

                    #region 프리폼 정보 테이블에 넣어줌
                    if (_dtH.Rows.Count == 0)
                    {
                        DataRow drNew = _dtH.NewRow();
                        drNew["NO_SO"] = SlipNo;
                        drNew["DT_SO"] = SaleDt;
                        drNew["CD_CUST"] = CustomerCode;
                        drNew["FG_SO"] = "T"; //일반전표 : A, 반품 전표: R, 임시저장 전표 : T, 수금만 있는 전표 :P

                        _dtH.Rows.Add(drNew);
                    }
                    else
                    {
                        _dtH.Rows[0]["NO_SO"] = SlipNo;
                        _dtH.Rows[0]["DT_SO"] = SaleDt;
                        _dtH.Rows[0]["CD_CUST"] = CustomerCode;
                        _dtH.Rows[0]["FG_SO"] = "T"; //일반전표 : A, 반품 전표: R, 임시저장 전표 : T, 수금만 있는 전표 :P
                    }
                    #endregion 프리폼 정보 테이블에 넣어줌

                    DataTable dtHChange = _dtH.GetChanges();
                    DataTable dtLChange = gridItem.GetChanges();

                    List<object> listParam = new List<object>();
                    listParam.Add(SlipNo);
                    listParam.Add(SaleDt);
                    listParam.Add(CustomerCode);
                    listParam.Add("S");

                    if (dtHChange == null && dtLChange == null)
                    {
                        ShowMessageBoxA("변경된 내용이 존재하지 않습니다.", MessageType.Warning);
                        return;
                    }

                    if (SaveTemp(dtHChange, dtLChange, listParam))
                    {
                        if (dtHChange != null) dtHChange.AcceptChanges();
                        if (dtLChange != null) gridItem.AcceptChanges();

                        //20200624 임시 전표가 있으면, 삭제
                        if (SlipNoTemp != null && SlipNoTemp.Count > 0)
                        {
                            for (int i = 0; i < SlipNoTemp.Count; i++)
                            {
                                DeleteTemp(new object[] { POSGlobal.StoreCode, SlipNoTemp[i], "S" });
                            }
                            SlipNoTemp.Clear();
                        }

                        //ShowMessageBoxA("임시 저장되었습니다.", MessageType.Information);
                        //txtNoSlip.Text = SlipNo;
                        //Search(SlipNo);

                        if (TmpPrintDirect == "Y")
                            PrintWorkSheet();
                        else
                            BtnCtrPrint_Click(null, null);

                        OnInsert();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }

        }
        private void BtnCtrCancel_Click(object sender, EventArgs e)
        {
            if (IsWorkCancelQuestion)
            {
                if (ShowMessageBoxA("작업을 취소하시겠습니까?", MessageType.Question) == DialogResult.Yes)
                {
                    OnInsert();
                }
            }
            else
            {
                LoadData.StartLoading(this, "작업 취소", "화면을 로딩하고 있습니다.");
                OnInsert();
                LoadData.EndLoading();
            }
        }
        private void BtnCtrPayDone_Click(object sender, EventArgs e)
        {
            if (VerifyCheck(sender) == false) return;

            if (_payMode == PayMode.None)
            {
                ShowMessageBoxA("결제정보를 입력해주세요!", MessageType.Warning);
                return;
            }

            decimal ReceiveAmt = A.GetDecimal(txtReceiveAmt.EditValue);
            decimal ClsPayAmt = A.GetDecimal(txtClsPayAmt.EditValue);
            decimal ClsCreditAmt = A.GetDecimal(txtClsCreditAmt.EditValue);
            decimal NonPaidAmt = A.GetDecimal(txtClsNonPaidAmt.EditValue);
            decimal TotalNonPaidAmt = A.GetDecimal(txtTotalNonPaidAmt.EditValue);


            if (ReceiveAmt - (ClsPayAmt + ClsCreditAmt + NonPaidAmt) != 0)
            {
                ShowMessageBoxA("결제정보를 입력해주세요!", MessageType.Warning);
                return;
            }

            if (!IsNonPaid)
            {
                if (ReceiveAmt != (ClsPayAmt + ClsCreditAmt))
                {
                    ShowMessageBoxA(CustomerName + " 거래처는 외상 또는 선입금이 불가능합니다.", MessageType.Warning);
                    //결제정보 취소
                    BtnCtrPayCancel_Click(null, null);
                    return;
                }
            }

            //전표 유형 정상으로 변경
            SlipFlag = "A";
            LoadData.StartLoading(this, "결제완료", "작업중입니다.");
            OnSave();
            LoadData.EndLoading();
        }

        private void BtnCtrPayCancel_Click(object sender, EventArgs e)
        {
            if (VerifyCheck(sender) == false) return;

            _payMode = PayMode.None;

            txtClsPayAmt.EditValue = 0;
            txtClsNonPaidAmt.EditValue = 0;
            txtClsCreditAmt.EditValue = 0;
            txtTotalNonPaidAmt.EditValue = 0;
            txtDiscountAmt.EditValue = 0;

            CalcAmt(null);
            SetGridFooter();
        }

        private void BtnCtrPayNon_Click(object sender, EventArgs e)
        {
            if (VerifyCheck(sender) == false) return;
            if (IsNonPaid)
            {
                _payMode = PayMode.Paid;
                CalcAmt(sender);
            }
            else
            {
                ShowMessageBoxA(CustomerName + " 거래처는 외상이 불가능합니다.", MessageType.Warning);
                return;
            }
        }

        private void BtnCtrPayCredit_Click(object sender, EventArgs e)
        {
            if (VerifyCheck(sender) == false) return;
            _payMode = PayMode.Paid;
            CalcAmt(sender);
        }

        private void BtnCtrPayDeposit_Click(object sender, EventArgs e)
        {
            if (VerifyCheck(sender) == false) return;
            _payMode = PayMode.Paid;
            CalcAmt(sender);
        }

        private void BtnCtrDiscountCancel_Click(object sender, EventArgs e)
        {
            if (VerifyCheck(sender) == false) return;
            CalcAmt(sender);
        }

        private void BtnCtrDiscount_Click(object sender, EventArgs e)
        {
            if (VerifyCheck(sender) == false) return;
            CalcAmt(sender);
        }

        private void BtnCtrReturn_Click(object sender, EventArgs e)
        {
            if (SlipNo == string.Empty)
            {
                ShowMessageBoxA("조회된 거래건이 없습니다.\n먼저 이전거래를 조회하세요.", MessageType.Information);
                return;
            }
            else if (_slipNoPay == string.Empty)
            {
                ShowMessageBoxA("임시저장건은 반품이 불가능합니다.", MessageType.Information);
                return;
            }

            if (ShowMessageBoxA("반품처리 하시겠습니까?", MessageType.Question) == DialogResult.Yes)
            {
                bool result = SaveReturn(new object[] { POSGlobal.StoreCode, SlipNo, POSGlobal.EmpCode });
                if (result)
                    OnInsert();
            }
        }

        private void BtnCtrFindItem_Click(object sender, EventArgs e)
        {
            if (VerifyCheck(sender) == false) return;

            //if (txtCust.Text == string.Empty)
            //{
            //    ShowMessageBoxA("먼저 거래처를 선택해 주세요.", MessageType.Warning);
            //    return;
            //}

            //SetItemTypeScreen(IsItemTypeScreen);

            //if(_dtPay.Rows.Count > 0)
            //{
            //    ShowMessageBoxA("이미 결제 완료된 주문건입니다.", MessageType.Warning);
            //    return;
            //}
            //SetContents();
            IsContentScreen = true;
        }

        private void BtnCtrReorder_Click(object sender, EventArgs e)
        {

            if (ShowMessageBoxA("재주문하시겠습니까?", MessageType.Question) == DialogResult.Yes)
            {
                //기존정보 임시 저장
                DataTable dtReorderHeader = _dtH.Copy();
                DataTable dtReorderLine = _dtL.Copy();

                //decimal NonpaidAmt = A.GetDecimal(txtPreNonPaidAmt.EditValue);
                string tmpCustInfo = lblCustLsatSaleDt.Text;
                string tmpCustCarNo = lblCustCarNo.Text;
                string tmpCustTel = lblCustTel.Text;
                bool tmpIsNonPaid = IsNonPaid;
                string tmpSlipno = SlipNo;
                //신규등록
                OnInsert();

                CustomerCode = A.GetString(dtReorderHeader.Rows[0]["CD_CUST"]);
                CustomerName = A.GetString(dtReorderHeader.Rows[0]["NM_CUST"]);

                //외상잔액
                decimal NonpaidAmt = A.GetDecimal(DBHelper.ExecuteScalar("USP_POS_GET_CUST_NONPAID_AMT", new object[] { POSGlobal.StoreCode, CustomerCode, SlipNo, dtpSale.Text }));

                txtPreNonPaidAmt.EditValue = NonpaidAmt;
                txtReceiveAmt.EditValue = NonpaidAmt;

                SalesNoPre = tmpSlipno;
                lblCustLsatSaleDt.Text = tmpCustInfo;
                lblCustCarNo.Text = tmpCustCarNo;
                lblCustTel.Text = tmpCustTel;
                IsNonPaid = tmpIsNonPaid;
                txtCust.Text = CustomerName;
                ContentsType = ContentsMode.Item;
                SetContents();

                //그리드 행추가
                for (int i = 0; i < dtReorderLine.Rows.Count; i++)
                {
                    _itemCode = A.GetString(dtReorderLine.Rows[i]["CD_ITEM"]);
                    _itemName = A.GetString(dtReorderLine.Rows[i]["NM_ITEM"]);
                    _vatType = A.GetString(dtReorderLine.Rows[i]["FG_VAT"]);
                    _vatRate = A.GetVatRate(A.GetString(dtReorderLine.Rows[i]["FG_VAT"]));
                    _Qt = A.GetDecimal(dtReorderLine.Rows[i]["QT"]);
                    _QtUnit = A.GetDecimal(dtReorderLine.Rows[i]["QT_UNIT"]);
                    _itemPrice = A.GetDecimal(dtReorderLine.Rows[i]["UM"]);
                    _itemDescript = A.GetString(dtReorderLine.Rows[i]["DC_ITEM"]);
                    _unitCode = A.GetString(dtReorderLine.Rows[i]["CD_UNIT"]);
                    _unitName = A.GetString(dtReorderLine.Rows[i]["NM_UNIT"]);

                    gridViewItem.AddNewRow();
                }

                gridViewItem.BestFitColumns();
                gridViewItem.UpdateCurrentRow();
                gridViewItem.UpdateSummary();

                SetGridFooter();
                CalcAmt(null);
            }

        }

        private void BtnCtrModify_Click(object sender, EventArgs e)
        {
            if (JobStatus == JobMode.Read)
            {
                if (ShowMessageBoxA("수정하시겠습니까?", MessageType.Question) == DialogResult.Yes)
                {
                    BtnCtrPayCancel_Click(null, null);
                    JobStatus = JobMode.Modify;
                    ((SimpleButton)sender).Text = "수정취소";
                }
            }
            else if (JobStatus == JobMode.Modify)
            {
                if (ShowMessageBoxA("수정취소하시겠습니까?", MessageType.Question) == DialogResult.Yes)
                {
                    Search(SlipNo);
                    JobStatus = JobMode.Read;
                    ((SimpleButton)sender).Text = "수정";

                    //수정 취소시 신규
                    OnInsert();
                }
            }
            else
            {
                ShowMessageBoxA("조회된 거래건이 없습니다.\n먼저 이전거래를 조회하세요.", MessageType.Warning);
                return;
            }
        }

        private void BtnCtrPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (SlipNo == string.Empty)
                {
                    ShowMessageBoxA("조회된 거래건이 없습니다.\n먼저 이전거래를 조회하세요.", MessageType.Information);
                    return;
                }
                //else  if(_slipNoPay == string.Empty)
                //{
                //    ShowMessageBoxA("임시저장건은 영수증 출력이 불가능합니다.", MessageType.Information);
                //    return;
                //}

                Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
                string PrintType = cfgPrint.ConfigValue;

                Bifrost.Helper.POSConfig cfgPrintSequence = POSConfigHelper.GetConfig("PRT003");
                string PrintSequence = cfgPrintSequence.ConfigValue;

                // 거래명세서 종이 설정
                Bifrost.Helper.POSConfig cfgPrintPaper = POSConfigHelper.GetConfig("PRT011");
                string PrtPaperType = cfgPrintPaper.ConfigValue;

                string result = string.Empty;
               

                switch (SlipFlag)
                {
                    case "A"://정상 판매
                        P_POS_PRINT_SALE P_POS_PRINT_SALE = new P_POS_PRINT_SALE();
                        if (P_POS_PRINT_SALE.ShowDialog() == DialogResult.OK)
                        {
                            result = P_POS_PRINT_SALE.ResultPrint;

                            switch (result)
                            {
                                case "A": //영수증 작업서

                                    if (PrintSequence == "R")
                                    {
                                        PrintReceit();
                                        Thread.Sleep(100);
                                        PrintWorkSheet();
                                    }
                                    else if (PrintSequence == "W")
                                    {
                                        PrintWorkSheet();
                                        Thread.Sleep(100);
                                        PrintReceit();
                                    }
                                    break;
                                case "R": //영수증
                                    PrintReceit();
                                    break;
                                case "W": //작업서
                                    PrintWorkSheet();
                                    break;
                                case "I": //거래명세   
                                    if(PrtPaperType == "A5")
                                        POSPrintHelper.POSReportPrint("POS_SALE_RPT01", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                    else if (PrtPaperType == "A4")
                                        POSPrintHelper.POSReportPrint("POS_SALE_RPT02", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                    break;
                                case "N": //미발행
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "P"://수금만
                        P_POS_PRINT P_POS_PRINT = new P_POS_PRINT();
                        P_POS_PRINT.StartPosition = FormStartPosition.Manual;
                        P_POS_PRINT.Location = this.PointToScreen(new Point(this.Size.Width / 2 - P_POS_PRINT.Size.Width / 2, this.Size.Height / 2 - P_POS_PRINT.Size.Height / 2));

                        switch (PrintType)
                        {
                            case "A": //둘다
                                
                                P_POS_PRINT.PrintText = new string[] { "입금표\n(일반)", "입금표\n(감열지)" };
                                P_POS_PRINT.PrintTag = new string[] { "P", "T" };

                                if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                                {
                                    result = P_POS_PRINT.ResultPrint;

                                    switch (result)
                                    {
                                        case "T":
                                            PrintNonpaidReceit();
                                            break;
                                        case "P":
                                            POSPrintHelper.POSReportPrint("POS_SALE_RPT01", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                            break;
                                    }
                                }

                                break;
                            case "T": //감열지
                                P_POS_PRINT.PrintText = new string[] { "입금표\n(감열지)" };
                                P_POS_PRINT.PrintTag = new string[] { "T" };

                                if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                                {
                                    result = P_POS_PRINT.ResultPrint;

                                    switch (result)
                                    {
                                        case "T":
                                            PrintNonpaidReceit();
                                            break;
                                    }
                                }
                                break;
                            case "P": //일반
                                P_POS_PRINT.PrintText = new string[] { "입금표\n(일반)" };
                                P_POS_PRINT.PrintTag = new string[] { "P" };

                                if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                                {
                                    result = P_POS_PRINT.ResultPrint;

                                    switch (result)
                                    {
                                        case "P":
                                            if (PrtPaperType == "A5")
                                                POSPrintHelper.POSReportPrint("POS_SALE_RPT01", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                            else if (PrtPaperType == "A4")
                                                POSPrintHelper.POSReportPrint("POS_SALE_RPT02", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                            break;
                                    }
                                }
                                break;
                        }
                        break;
                    case "T"://임시
                        P_POS_PRINT_WORKSHEET P_POS_PRINT_WORKSHEET = new P_POS_PRINT_WORKSHEET();
                        if (P_POS_PRINT_WORKSHEET.ShowDialog() == DialogResult.OK)
                        {
                            result = P_POS_PRINT_WORKSHEET.ResultPrint;

                            switch (result)
                            {
                                case "W": //작업서
                                    PrintWorkSheet();
                                    break;
                                case "N": //미발행
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }


        #endregion 우측하단 컨트롤패널버튼 이벤트 모음

        #region 그리드 하단 조작 업다운 버튼 이벤트
        private void BtnGridDownMax_Click(object sender, EventArgs e)
        {
            if (gridViewItem.RowCount > 0)
                gridViewItem.FocusedRowHandle = gridViewItem.RowCount - 1;
        }

        private void BtnGridUpMax_Click(object sender, EventArgs e)
        {
            if (gridViewItem.RowCount == 0)
                return;

            gridViewItem.FocusedRowHandle = 0;
        }

        private void BtnGridDown_Click(object sender, EventArgs e)
        {
           if (gridViewItem.RowCount == 0)
                return;

            int index = (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo[0].RowHandle + (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo.Count - 1;
            if (index >= 0)
            {
                gridViewItem.FocusedRowHandle = index;
                gridViewItem.TopRowIndex = index;
            }
            else
                gridViewItem.FocusedRowHandle = 0;
        }

        private void BtnGridUp_Click(object sender, EventArgs e)
        {
            if (gridViewItem.RowCount == 0)
                return;

            int index = (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo[0].RowHandle - (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo.Count;
            if (index >= 0)
                gridViewItem.FocusedRowHandle = index;
            else
                gridViewItem.FocusedRowHandle = 0;
        }
        #endregion 그리드 하단 조작 업다운 버튼 이벤트

        #region 키패드 관련 이벤트 모음
        private void BtnKeyAlphabet_Click(object sender, EventArgs e)
        {
            _tmpResult += ((DevExpress.XtraEditors.SimpleButton)sender).Text;
            txtQty.Text = _tmpResult;
        }

        private void BtnKeyMinus_Click(object sender, EventArgs e)
        {
            if (A.GetDecimal(txtQty.EditValue) == 0)
            {
                txtQty.Text = "-";
                _tmpResult = "-";
            }
            else if (A.GetDecimal(txtQty.EditValue) > 0)
            {
                txtQty.EditValue = A.GetDecimal(txtQty.EditValue) * (-1);
            }
        }

        private void BtnKeyBackSpace_Click(object sender, EventArgs e)
        {
            if (A.GetDecimal(_tmpResult) != 0)
            {
                //마지막이 . 일땐 .까지 같이 지우기
                if (_tmpResult[_tmpResult.Length - 1].ToString() == ".")
                    _tmpResult = _tmpResult.Substring(0, _tmpResult.Length - 2);
                else
                    _tmpResult = _tmpResult.Substring(0, _tmpResult.Length - 1);

                txtQty.Text = _tmpResult;
            }
        }

        private void BtnKeyPoint_Click(object sender, EventArgs e)
        {
            if (!A.GetDecimal(_tmpResult).ToString().Contains("."))
            {
                _tmpResult += ((DevExpress.XtraEditors.SimpleButton)sender).Text;
                txtQty.Text = _tmpResult;
            }
        }

        private void BtnKeyClear_Click(object sender, EventArgs e)
        {
            ClearKeyPad();
        }

        private void ClearKeyPad()
        {
            _tmpResult = string.Empty;
            txtQty.EditValue = 0;
        }
        #endregion 키패드 관련 이벤트 모음

        #region 그리드 관련 이벤트
        //상품 더블클릭시 키패드 실행
        private void GridViewItem_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                if (VerifyCheck(sender) == false) return;

                if (_payMode == PayMode.Paid)
                {
                    ShowMessageBoxA("결제취소 후 상품 수정 가능합니다.", MessageType.Warning);
                    return;
                }
                //string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                //MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));

                if (swPopup.IsOn)
                {
                    //선택한 로우 저장
                    ClickRowHandle = info.RowHandle;

                    //navPay.SelectedPage = navPayKeypad;
                    panelKeypadItem.Visible = true;
                    GridViewItem_FocusedRowChanged(sender, null);
                }
                else
                {
                    P_KEYPAD.CustomerCode = CustomerCode;
                    P_KEYPAD.KeypadItemCode = _itemCode = A.GetString(view.GetRowCellValue(info.RowHandle, "CD_ITEM"));
                    P_KEYPAD.KeypadItemName = _itemName = A.GetString(view.GetRowCellValue(info.RowHandle, "NM_ITEM"));
                    P_KEYPAD.Qty = A.GetDecimal(view.GetRowCellValue(info.RowHandle, "QT"));
                    P_KEYPAD.QtyUnit = A.GetDecimal(view.GetRowCellValue(info.RowHandle, "QT_UNIT"));
                    P_KEYPAD.ItemPrice = A.GetDecimal(view.GetRowCellValue(info.RowHandle, "UM"));
                    P_KEYPAD.ItemDescript = A.GetString(view.GetRowCellValue(info.RowHandle, "DC_ITEM"));
                    P_KEYPAD.UnitCode = A.GetString(view.GetRowCellValue(info.RowHandle, "CD_UNIT"));
                    P_KEYPAD.UnitName = A.GetString(view.GetRowCellValue(info.RowHandle, "NM_UNIT"));
                    P_KEYPAD.KeyMode = P_KEYPAD.Mode.Modify;

                    if (P_KEYPAD.ShowDialog() == DialogResult.OK)
                    {
                        if (P_KEYPAD.isDel)
                        {
                            view.DeleteRow(info.RowHandle);
                        }
                        else
                        {
                            DataRow[] dr = _dtItemAll.Select("CD_ITEM = '" + _itemCode + "'");
                            _vatType = A.GetString(dr[0]["FG_VAT"]);
                            _vatRate = A.GetDecimal(dr[0]["RT_VAT"]);
                            _Qt = A.GetDecimal(P_KEYPAD.Qty);
                            _QtUnit = A.GetDecimal(P_KEYPAD.QtyUnit);
                            _itemPrice = A.GetDecimal(P_KEYPAD.ItemPrice);
                            _itemDescript = A.GetString(P_KEYPAD.ItemDescript);
                            _unitCode = A.GetString(P_KEYPAD.UnitCode);

                            view.SetRowCellValue(info.RowHandle, view.Columns["CD_ITEM"], _itemCode);
                            view.SetRowCellValue(info.RowHandle, view.Columns["NM_ITEM"], _itemName);
                            view.SetRowCellValue(info.RowHandle, view.Columns["QT"], _Qt);
                            view.SetRowCellValue(info.RowHandle, view.Columns["QT_UNIT"], _QtUnit);
                            view.SetRowCellValue(info.RowHandle, view.Columns["UM"], _itemPrice);
                            view.SetRowCellValue(info.RowHandle, view.Columns["AM"], _Qt * _QtUnit * _itemPrice);
                            view.SetRowCellValue(info.RowHandle, view.Columns["DC_ITEM"], _itemDescript);
                            view.SetRowCellValue(info.RowHandle, view.Columns["CD_UNIT"], _unitCode);
                            view.SetRowCellValue(info.RowHandle, view.Columns["NM_UNIT"], _unitName);

                            view.SetRowCellValue(info.RowHandle, view.Columns["FG_VAT"], _vatType);
                            view.SetRowCellValue(info.RowHandle, view.Columns["AM_VAT"], _Qt * _QtUnit * _itemPrice * _vatRate / 100);
                            view.SetRowCellValue(info.RowHandle, view.Columns["AM_NET"], (_Qt * _QtUnit * _itemPrice) - (_Qt * _QtUnit * _itemPrice * _vatRate / 100));

                        }

                        gridViewItem.UpdateCurrentRow();
                        gridViewItem.UpdateSummary();
                        txtPayAmt.EditValue = gridViewItem.Columns["AM"].SummaryItem.SummaryValue;
                        txtVatAmt.EditValue = gridViewItem.Columns["AM_VAT"].SummaryItem.SummaryValue;
                        //txtReceiveAmt.EditValue = A.GetDecimal(gridViewItem.Columns["AM"].SummaryItem.SummaryValue) + A.GetDecimal(gridViewItem.Columns["AM_VAT"].SummaryItem.SummaryValue);

                        CalcAmt(null);

                    }
                }

            }
        }
        
        private void GridViewItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;

            if (panelKeypadItem.Visible)
            {
                //초기화는 단가
                PadType = KeypadType.Price;
                txtPadQty.EditValue = 0;

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

        private void GridViewItem_RowCountChanged(object sender, EventArgs e)
        {
            GridNavVisible();
        }

        private void GridNavVisible()
        {
            if (gridViewItem.RowCount == 0) return;

            //int tmp = (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo[0] == null ? 0 : (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo[0].RowHandle;
            //int index = (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo.Count;

            //int rowHandle = gridViewItem.FocusedRowHandle;

            if (gridViewItem.RowCount > MaxRowCount)
                panelGridNav.Visible = true;
            else
                panelGridNav.Visible = false;

            gridViewItem.MoveLast();
        }

        private void GridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            DataTable _dt = gridItem.DataSource as DataTable;

            decimal maxNoLine = A.GetDecimal(_dt.Compute("MAX(NO_LINE)", ""));
            ++maxNoLine;

            view.SetRowCellValue(e.RowHandle, view.Columns["CD_ITEM"], _itemCode);
            view.SetRowCellValue(e.RowHandle, view.Columns["NM_ITEM"], _itemName);
            view.SetRowCellValue(e.RowHandle, view.Columns["QT"], _Qt);
            view.SetRowCellValue(e.RowHandle, view.Columns["QT_UNIT"], _QtUnit);
            view.SetRowCellValue(e.RowHandle, view.Columns["UM"], _itemPrice);
            view.SetRowCellValue(e.RowHandle, view.Columns["AM"], _Qt * _QtUnit * _itemPrice);
            view.SetRowCellValue(e.RowHandle, view.Columns["DC_ITEM"], _itemDescript);
            view.SetRowCellValue(e.RowHandle, view.Columns["CD_UNIT"], _unitCode);
            view.SetRowCellValue(e.RowHandle, view.Columns["NM_UNIT"], _unitName);
            view.SetRowCellValue(e.RowHandle, view.Columns["FG_VAT"], _vatType);
            view.SetRowCellValue(e.RowHandle, view.Columns["AM_VAT"], _Qt * _QtUnit * _itemPrice * _vatRate / 100);
            view.SetRowCellValue(e.RowHandle, view.Columns["AM_NET"], (_Qt * _QtUnit * _itemPrice) - (_Qt * _QtUnit * _itemPrice * _vatRate / 100));
            view.SetRowCellValue(e.RowHandle, view.Columns["YN_RETURN"], "N");
            view.SetRowCellValue(e.RowHandle, view.Columns["NO_LINE"], maxNoLine);
            view.SetRowCellValue(e.RowHandle, view.Columns["FG_LAST"], _TmpLastFlag);

            //키패드 초기화
            PadQty = 0;
            PadQtyUnit = 0;
            PadItemPrice = 0;
            PadItemDescript = string.Empty;
            PadUnitCode = string.Empty;
            PadUnitName = string.Empty;
            txtPadQty.EditValue = 0;
            SetResultText();

        }
        #endregion 그리드 관련 이벤트

        bool IsItemTypeFix = false;

        private void SetCustomerInfo()
        {
            DataRow[] dr = _dtCustAll.Select("CD_CUST = '" + CustomerCode + "'");

            if (dr.Length > 0)
            {
                CustomerName = A.GetString(dr[0]["NM_CUST"]);
                //20200527
                //SalesNoPre = A.GetString(dtCustInfo.Rows[0]["NO_SO_LAST"]);
                IsNonPaid = A.GetString(dr[0]["YN_NONPAID"]) == "Y" ? true : false;

                //string LastDt = A.GetString(dtCustInfo.Rows[0]["DT_SO"]) == string.Empty ? string.Empty : "※ 최근거래일 : " + A.GetString(dtCustInfo.Rows[0]["DT_SO"]) + " / 방문횟수 : " + A.GetString(dtCustInfo.Rows[0]["QT_VISIT"]);
                //string CarNo = A.GetString(dtCustInfo.Rows[0]["NO_CAR"]) == string.Empty ? string.Empty : "▶ 차량번호 : " + A.GetString(dtCustInfo.Rows[0]["NO_CAR"]);
                //string TelNo = A.GetString(dtCustInfo.Rows[0]["NO_TEL1"]) == string.Empty ? string.Empty : " ▶ 전화번호 : " + A.GetString(dtCustInfo.Rows[0]["NO_TEL1"]);

                string LastDt = A.GetString(dr[0]["DT_LAST_SO"]) == string.Empty ? string.Empty : A.GetString(dr[0]["DT_LAST_SO"]);
                string VisitQt = A.GetString(dr[0]["QT_VISIT_SO"]) + " 회";
                string CarNo = A.GetString(dr[0]["NO_CAR"]) == string.Empty ? string.Empty : A.GetString(dr[0]["NO_CAR"]);
                string TelNo = A.GetString(dr[0]["NO_TEL1"]) == string.Empty ? string.Empty : A.GetString(dr[0]["NO_TEL1"]);

                lblCustLsatSaleDt.Text = LastDt;
                lblCustCarNo.Text = CarNo;
                lblCustTel.Text = VisitQt;
            }
        }
        /// <summary>
        /// 컨텐츠 패널 터치 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnContents_Click(object sender, EventArgs e)
        {
            if (((SimpleButton)sender).Text == string.Empty) return;

            DataRow[] dr;

            //거래처 선택
            switch (ContentsType)
            {
                case ContentsMode.Customer:
                    CustomerCode = ((SimpleButton)sender).Tag.ToString();

                    //외상잔액
                    decimal NonpaidAmt = A.GetDecimal(DBHelper.ExecuteScalar("USP_POS_GET_CUST_NONPAID_AMT", new object[] { POSGlobal.StoreCode, CustomerCode, SlipNo, dtpSale.Text }));

                    txtPreNonPaidAmt.EditValue = NonpaidAmt;
                    txtReceiveAmt.EditValue = NonpaidAmt;

                    //DataRow[] dr = _dtCust.Select("CD_CUST = '" + CustomerCode + "'");
                    //txtPreNonPaidAmt.EditValue = A.GetDecimal(dr[0]["AM_NONPAID_SUM"]);

                    ////거래처정보 가져오기
                    SetCustomerInfo();
                    ////DataTable dtCustInfo = SearchCustInfo(new object[] { POSGlobal.StoreCode, CustomerCode });
                    //dr = _dtCustAll.Select("CD_CUST = '" + CustomerCode + "'");

                    //if (dr.Length > 0)
                    //{
                    //    CustomerName = A.GetString(dr[0]["NM_CUST"]);
                    //    //20200527
                    //    //SalesNoPre = A.GetString(dtCustInfo.Rows[0]["NO_SO_LAST"]);
                    //    IsNonPaid = A.GetString(dr[0]["YN_NONPAID"]) == "Y" ? true : false;

                    //    //string LastDt = A.GetString(dtCustInfo.Rows[0]["DT_SO"]) == string.Empty ? string.Empty : "※ 최근거래일 : " + A.GetString(dtCustInfo.Rows[0]["DT_SO"]) + " / 방문횟수 : " + A.GetString(dtCustInfo.Rows[0]["QT_VISIT"]);
                    //    //string CarNo = A.GetString(dtCustInfo.Rows[0]["NO_CAR"]) == string.Empty ? string.Empty : "▶ 차량번호 : " + A.GetString(dtCustInfo.Rows[0]["NO_CAR"]);
                    //    //string TelNo = A.GetString(dtCustInfo.Rows[0]["NO_TEL1"]) == string.Empty ? string.Empty : " ▶ 전화번호 : " + A.GetString(dtCustInfo.Rows[0]["NO_TEL1"]);

                    //    string LastDt = A.GetString(dr[0]["DT_LAST_SO"]) == string.Empty ? string.Empty : A.GetString(dr[0]["DT_LAST_SO"]);
                    //    string VisitQt = A.GetString(dr[0]["QT_VISIT_SO"]) + " 회";
                    //    string CarNo = A.GetString(dr[0]["NO_CAR"]) == string.Empty ? string.Empty : A.GetString(dr[0]["NO_CAR"]);
                    //    string TelNo = A.GetString(dr[0]["NO_TEL1"]) == string.Empty ? string.Empty : A.GetString(dr[0]["NO_TEL1"]);

                    //    lblCustLsatSaleDt.Text = LastDt;
                    //    lblCustCarNo.Text = CarNo;
                    //    lblCustTel.Text = VisitQt;
                    //}

                    txtCust.Text = CustomerName;

                    for (int j = 0; j < _dtItem.Rows.Count; j++)
                    {
                        _contentsBtn[j].Text = _dtItem.Rows[j]["NM_ITEM"].ToString();
                        _contentsBtn[j].Tag = _dtItem.Rows[j]["CD_ITEM"].ToString();

                        if (j == ScreenQty - 1)
                            break;
                    }

                    ContentsType = ContentsMode.Item;
                    InitItemTable();

                    //20200527 선택한 날짜 기준으로 전표 번로 가져오려고 메서드 바꿈
                    //선택한 거래처의 앞 뒤 전표 번호 가져오기
                    //CheckPreNextSlipNo(string.Empty);
                    CheckPreNextSlipNoByDate();
                    break;
                case ContentsMode.Item:
                    if (_payMode == PayMode.Paid)
                    {
                        ShowMessageBoxA("결제취소 후 상품 추가 가능합니다.", MessageType.Warning);
                        return;
                    }

                    _itemCode = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
                    _itemName = ((DevExpress.XtraEditors.SimpleButton)sender).Text.Replace("\r\n", string.Empty);

                    dr = _dtItem.Select("CD_ITEM = '" + _itemCode + "'");

                    //대분류일경우
                    if (ContentsItemType == ContentsItem.ItemType)
                    {
                        if (ContentsLv == 0)
                        {
                            if (dr.Length > 0)
                            {
                                IsItemTypeFix = A.GetString(dr[0]["CD_CLAS_REF"]) == "Y" ? true : false;
                            }

                            _dtItem = _dtItemAll.Select("TP_ITEM_L = '" + _itemCode + "'").CopyToDataTable();
                            SetContents();
                            SetNaviButton();
                            ContentsLv = 1; //컨텐츠 레벨이 0이 아닐때 상위 버튼 활성화 시킴
                            return;
                        }
                    }


                    //키패드 팝업으로할지 패널로 할지 설정
                    if (swPopup.IsOn)
                    {
                        //20200426
                        //_dtItem = _dtItemAll;
                        //키패드 페이지 보여줌
                        //navPay.SelectedPage = navPayKeypad;
                        panelKeypadItem.Visible = true;

                        //키패드 초기화
                        PadQty = 0;
                        PadQtyUnit = 0;
                        PadItemPrice = 0;
                        PadItemDescript = string.Empty;
                        PadUnitCode = string.Empty;
                        PadUnitName = string.Empty;
                        txtPadQty.EditValue = 0;
                        SetResultText();

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
                        //키패드초기화
                        ClearKeyPad();
                    }
                    else
                    {
                        P_KEYPAD.Location = panelContents.PointToScreen(PopupPoint);
                        P_KEYPAD.StartPosition = FormStartPosition.Manual;
                        P_KEYPAD.CustomerCode = CustomerCode;
                        P_KEYPAD.KeypadItemCode = _itemCode;
                        P_KEYPAD.KeypadItemName = _itemName;
                        P_KEYPAD.KeyMode = P_KEYPAD.Mode.New;

                        if (P_KEYPAD.ShowDialog() == DialogResult.OK)
                        {
                            _vatType = A.GetString(dr[0]["FG_VAT"]);
                            _vatRate = A.GetDecimal(dr[0]["RT_VAT"]);
                            _Qt = A.GetDecimal(P_KEYPAD.Qty);
                            _QtUnit = A.GetDecimal(P_KEYPAD.QtyUnit);
                            _itemPrice = A.GetDecimal(P_KEYPAD.ItemPrice);
                            _itemDescript = A.GetString(P_KEYPAD.ItemDescript);
                            _unitCode = A.GetString(P_KEYPAD.UnitCode);
                            _unitName = A.GetString(P_KEYPAD.UnitName);

                            gridViewItem.AddNewRow();
                            gridViewItem.BestFitColumns();

                            gridViewItem.UpdateCurrentRow();
                            gridViewItem.UpdateSummary();

                            SetGridFooter();

                            //전표유형 정상으로 바꿈
                            //20191211 왜 다시 정상으로 바꾸었을까? 바꿀 이유가 없는데.. 전표 유형 주석처리함
                            //SlipFlag = "A";
                            CalcAmt(null);

                            //20191024 계속 분류에 남게 초기화 취소
                            //InitItemTable();
                        }
                    }
                    break;
                case ContentsMode.Favorite:
                    if (_payMode == PayMode.Paid)
                    {
                        ShowMessageBoxA("결제취소 후 상품 추가 가능합니다.", MessageType.Warning);
                        return;
                    }

                    _itemCode = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
                    _itemName = ((DevExpress.XtraEditors.SimpleButton)sender).Text.Replace("\r\n", string.Empty);

                    dr = _dtItem.Select("CD_ITEM = '" + _itemCode + "'");

                    //대분류일경우
                    //if (ContentsItemType == ContentsItem.ItemType)
                    //{
                    //    if (ContentsLv == 0)
                    //    {
                    //        if (dr.Length > 0)
                    //        {
                    //            IsItemTypeFix = A.GetString(dr[0]["CD_CLAS_REF"]) == "Y" ? true : false;
                    //        }

                    //        _dtItem = _dtItemAll.Select("TP_ITEM_L = '" + _itemCode + "'").CopyToDataTable();
                    //        SetContents();
                    //        SetNaviButton();
                    //        ContentsLv = 1; //컨텐츠 레벨이 0이 아닐때 상위 버튼 활성화 시킴
                    //        return;
                    //    }
                    //}


                    //키패드 팝업으로할지 패널로 할지 설정
                    if (swPopup.IsOn)
                    {
                        //20200426
                        //_dtItem = _dtItemAll;
                        //키패드 페이지 보여줌
                        //navPay.SelectedPage = navPayKeypad;
                        panelKeypadItem.Visible = true;

                        //키패드 초기화
                        PadQty = 0;
                        PadQtyUnit = 0;
                        PadItemPrice = 0;
                        PadItemDescript = string.Empty;
                        PadUnitCode = string.Empty;
                        PadUnitName = string.Empty;
                        txtPadQty.EditValue = 0;
                        SetResultText();

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
                        //키패드초기화
                        ClearKeyPad();
                    }
                    else
                    {
                        P_KEYPAD.Location = panelContents.PointToScreen(PopupPoint);
                        P_KEYPAD.StartPosition = FormStartPosition.Manual;
                        P_KEYPAD.CustomerCode = CustomerCode;
                        P_KEYPAD.KeypadItemCode = _itemCode;
                        P_KEYPAD.KeypadItemName = _itemName;
                        P_KEYPAD.KeyMode = P_KEYPAD.Mode.New;

                        if (P_KEYPAD.ShowDialog() == DialogResult.OK)
                        {
                            _vatType = A.GetString(dr[0]["FG_VAT"]);
                            _vatRate = A.GetDecimal(dr[0]["RT_VAT"]);
                            _Qt = A.GetDecimal(P_KEYPAD.Qty);
                            _QtUnit = A.GetDecimal(P_KEYPAD.QtyUnit);
                            _itemPrice = A.GetDecimal(P_KEYPAD.ItemPrice);
                            _itemDescript = A.GetString(P_KEYPAD.ItemDescript);
                            _unitCode = A.GetString(P_KEYPAD.UnitCode);
                            _unitName = A.GetString(P_KEYPAD.UnitName);

                            gridViewItem.AddNewRow();
                            gridViewItem.BestFitColumns();

                            gridViewItem.UpdateCurrentRow();
                            gridViewItem.UpdateSummary();

                            SetGridFooter();

                            //전표유형 정상으로 바꿈
                            //20191211 왜 다시 정상으로 바꾸었을까? 바꿀 이유가 없는데.. 전표 유형 주석처리함
                            //SlipFlag = "A";
                            CalcAmt(null);

                            //20191024 계속 분류에 남게 초기화 취소
                            //InitItemTable();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void ItemType_Click(object sender, EventArgs e)
        {
            if (ContentsType == ContentsMode.Customer) return;

            if (ContentsType == ContentsMode.Favorite || ContentsType == ContentsMode.Item)
            {
                ContentsType = ContentsMode.Item;
                //InitItemTable();

                _itemCode = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
                _itemName = ((DevExpress.XtraEditors.SimpleButton)sender).Text;

                DataRow[] dr = _dtItemType.Select("CD_ITEM = '" + _itemCode + "'");
                string ItemFlag = A.GetString(dr[0]["FG_ITEM"]);

                //대분류일경우 다시 버튼 배치
                if (ContentsItemType == ContentsItem.ItemType)
                {
                    if (_dtItemAll.Select("TP_ITEM_L = '" + _itemCode + "'").Length > 0)
                    {
                        _dtItem = _dtItemAll.Select("TP_ITEM_L = '" + _itemCode + "'").CopyToDataTable();//( DBHelper.GetDataTable("USP_POS_ITEM_S", new object[] { POSGlobal.StoreCode, string.Empty, ItemFlag, _itemCode });
                        SetContents();
                        SetNaviButton();
                        ContentsLv = 1; //컨텐츠 레벨이 0이 아닐때 상위 버튼 활성화 시킴
                        return;
                    }
                }
            }
        }

        #region Contents 데이터 바인딩 
        //DataTable _dtContents;

        //private void SetContents(DataTable _dt)
        //{
        //    _dtContents = _dt;
        //    flowLayoutPanelContents.Hide();

        //    //초기화
        //    for (int j = 0; j < ScreenQty; j++)
        //    {
        //        _contentsBtn[j].Text = string.Empty;
        //        _contentsBtn[j].Tag = string.Empty;
        //        _contentsBtn[j].Enabled = false;
        //        _contentsBtn[j].Font = FontDefault;
        //        _contentsBtn[j].ForeColor = default;
        //    }

        //    //데이터 넣기
        //    double tmp = (double)_dt.Rows.Count / (double)ScreenQty;
        //    _maxPage = int.Parse(Math.Ceiling(tmp).ToString());

        //    int loopCnt = _dt.Rows.Count < ScreenQty ? _dt.Rows.Count : ScreenQty;

        //    string ContentsText;

        //    for (int j = 0; j < loopCnt; j++)
        //    {
        //        ContentsText = _dt.Rows[j][_selectedName].ToString().Substring(0, 1) == "(" ? _dt.Rows[j][_selectedName].ToString() : _dt.Rows[j][_selectedName].ToString().Replace("(", Environment.NewLine + "(");

        //        _contentsBtn[j].Text = ContentsText;
        //        _contentsBtn[j].Tag = _dt.Rows[j][_selectedCode].ToString();
        //        _contentsBtn[j].Enabled = true;

        //        if (ContentsType == ContentsMode.Customer)
        //            SetCustomerContents(j);

        //        if (j == ScreenQty - 1)
        //            break;
        //    }

        //    //현재페이지 초기화     
        //    _currentPage = 1;
        //    flowLayoutPanelContents.Show();
        //}

        //private void SetNaviButton(DataTable _dt)
        //{
        //    double tmp = 0;
        //    tmp = (double)_dt.Rows.Count / (double)ScreenQty;

        //    _maxPage = int.Parse(Math.Ceiling(tmp).ToString());

        //    if (_currentPage == 1) //현재 첫페이지 
        //    {
        //        if (_maxPage == 1)
        //        {
        //            btnPre.Enabled = false;
        //            btnNext.Enabled = false;

        //        }
        //        if (_maxPage > 1)//맥스 페이지가 1페이지 이상
        //        {
        //            btnPre.Enabled = false;
        //            btnNext.Enabled = true;
        //        }
        //    }
        //    else if (_currentPage > 1 && _maxPage > _currentPage) //현재 첫페이지  이상
        //    {
        //        if (_maxPage > 1)//맥스 페이지가 1페이지 이상
        //        {
        //            btnPre.Enabled = true;
        //            btnNext.Enabled = true;
        //        }
        //    }
        //    else if (_currentPage == _maxPage) //현재 마지막 페이지
        //    {
        //        btnPre.Enabled = true;
        //        btnNext.Enabled = false;
        //    }
        //    if (_maxPage != 0)
        //        SetUpdateTotalProgress((int)(_currentPage * 100 / _maxPage));
        //}

        private void SetContentsPreNext(DataTable _dt)
        {
            //초기화
            for (int j = 0; j < ScreenQty; j++)
            {
                _contentsBtn[j].Text = string.Empty;
                _contentsBtn[j].Tag = string.Empty;
                _contentsBtn[j].Enabled = false;
                _contentsBtn[j].Font = FontDefault;
            }

            flowLayoutPanelContents.Hide();

            string strCode = string.Empty;
            string strName = string.Empty;

            for (int j = ScreenQty * (_currentPage - 1); j < ScreenQty * _currentPage; j++)
            {
                if (_dt.Rows.Count <= j)
                {
                    _contentsBtn[j % ScreenQty].Text = string.Empty;
                    _contentsBtn[j % ScreenQty].Tag = string.Empty;
                    _contentsBtn[j % ScreenQty].Enabled = false;
                    if (ContentsType == ContentsMode.Customer)
                        SetCustomerContents(j);
                }
                else
                {
                    strCode = _dt.Rows[j][_selectedCode].ToString();
                    strName = _dt.Rows[j][_selectedName].ToString();

                    if (strName != string.Empty)
                        strName = strName.Substring(0, 1) == "(" ? strName : strName.Replace("(", Environment.NewLine + "(");

                    _contentsBtn[j % ScreenQty].Text = _dt.Rows.Count > j ? strName : string.Empty;
                    _contentsBtn[j % ScreenQty].Tag = _dt.Rows.Count > j ? strCode : string.Empty;
                    _contentsBtn[j % ScreenQty].Enabled = true;
                    if (ContentsType == ContentsMode.Customer)
                        SetCustomerContents(j);
                }
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

            //데이터 넣기
            if (ContentsType == ContentsMode.Customer)
            {
                double tmp;
                if (ContentsType == ContentsMode.Customer)
                    tmp = (double)_dtCust.Rows.Count / (double)ScreenQty;
                else
                    tmp = (double)_dtItem.Rows.Count / (double)ScreenQty;

                _maxPage = int.Parse(Math.Ceiling(tmp).ToString());

                int loopCnt = _dtCust.Rows.Count < ScreenQty ? _dtCust.Rows.Count : ScreenQty;
                string ContentsText;

                for (int j = 0; j < loopCnt; j++)
                {
                    ContentsText = _dtCust.Rows[j][_selectedName].ToString().Substring(0, 1) == "(" ? _dtCust.Rows[j][_selectedName].ToString() : _dtCust.Rows[j][_selectedName].ToString().Replace("(", Environment.NewLine + "(");

                    _contentsBtn[j].Text = ContentsText;
                    _contentsBtn[j].Tag = _dtCust.Rows[j][_selectedCode].ToString();
                    _contentsBtn[j].Enabled = true;

                    SetCustomerContents(j);

                    if (j == ScreenQty - 1)
                        break;
                }
            }
            else if (ContentsType == ContentsMode.Item)
            {
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
            }
            else if (ContentsType == ContentsMode.Favorite)
            {
                _maxPage = _dtItemFavorite.Rows.Count / ScreenQty;

                int loopCnt = _dtItemFavorite.Rows.Count < 40 ? _dtItemFavorite.Rows.Count : ScreenQty;

                for (int j = 0; j < loopCnt; j++)
                {
                    _contentsBtn[j].Text = _dtItemFavorite.Rows[j][_selectedName].ToString().Replace("\\r\\n", Environment.NewLine);
                    _contentsBtn[j].Tag = _dtItemFavorite.Rows[j][_selectedCode].ToString();
                    _contentsBtn[j].Enabled = true;
                    _contentsBtn[j].ForeColor = Color.Empty;

                    if (j == ScreenQty - 1)
                        break;
                }
            }

            //현재페이지 초기화     
            _currentPage = 1;
            flowLayoutPanelContents.Show();
        }

        private void SetNaviButton()
        {
            double tmp = 0;

            switch (ContentsType)
            {
                case ContentsMode.Customer:
                    tmp = (double)_dtCust.Rows.Count / (double)ScreenQty;
                    break;
                case ContentsMode.Item:
                    tmp = (double)_dtItem.Rows.Count / (double)ScreenQty;
                    break;
                case ContentsMode.Favorite:
                    tmp = (double)_dtItemFavorite.Rows.Count / (double)ScreenQty;
                    break;
                case ContentsMode.Custom:
                    break;
                default:
                    tmp = (double)_dtItem.Rows.Count / (double)ScreenQty;
                    break;
            }

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

        private void SetContentsPreNext()
        {
            flowLayoutPanelContents.Hide();

            string strCode = string.Empty;
            string strName = string.Empty;

            for (int j = ScreenQty * (_currentPage - 1); j < ScreenQty * _currentPage; j++)
            {
                switch (ContentsType)
                {
                    case ContentsMode.Customer:
                        if (_dtCust.Rows.Count <= j)
                        {
                            _contentsBtn[j % ScreenQty].Text = string.Empty;
                            _contentsBtn[j % ScreenQty].Tag = string.Empty;
                            _contentsBtn[j % ScreenQty].Enabled = false;
                            SetCustomerContents(j);
                        }
                        else
                        {
                            strCode = _dtCust.Rows[j][_selectedCode].ToString();
                            strName = _dtCust.Rows[j][_selectedName].ToString();

                            if (strName != string.Empty)
                                strName = strName.Substring(0, 1) == "(" ? strName : strName.Replace("(", Environment.NewLine + "(");

                            _contentsBtn[j % ScreenQty].Text = _dtCust.Rows.Count > j ? strName : string.Empty;
                            _contentsBtn[j % ScreenQty].Tag = _dtCust.Rows.Count > j ? strCode : string.Empty;
                            _contentsBtn[j % ScreenQty].Enabled = true;
                            SetCustomerContents(j);
                        }
                        break;
                    case ContentsMode.Item:
                        if (_dtItem.Rows.Count <= j)
                        {
                            _contentsBtn[j % ScreenQty].Text = string.Empty;
                            _contentsBtn[j % ScreenQty].Tag = string.Empty;
                            _contentsBtn[j % ScreenQty].Enabled = false;
                        }
                        else
                        {
                            _contentsBtn[j % ScreenQty].Text = _dtItem.Rows.Count > j ? _dtItem.Rows[j][_selectedName].ToString() : string.Empty;
                            _contentsBtn[j % ScreenQty].Tag = _dtItem.Rows.Count > j ? _dtItem.Rows[j][_selectedCode].ToString() : string.Empty;
                            _contentsBtn[j % ScreenQty].Enabled = true;
                        }
                        _contentsBtn[j % ScreenQty].ForeColor = Color.Empty;
                        break;
                    case ContentsMode.Favorite:
                        if (_dtItemFavorite.Rows.Count <= j)
                        {
                            _contentsBtn[j % ScreenQty].Text = string.Empty;
                            _contentsBtn[j % ScreenQty].Tag = string.Empty;
                            _contentsBtn[j % ScreenQty].Enabled = false;
                        }
                        else
                        {
                            _contentsBtn[j % ScreenQty].Text = _dtItemFavorite.Rows.Count > j ? _dtItemFavorite.Rows[j][_selectedName].ToString() : string.Empty;
                            _contentsBtn[j % ScreenQty].Tag = _dtItemFavorite.Rows.Count > j ? _dtItemFavorite.Rows[j][_selectedCode].ToString() : string.Empty;
                            _contentsBtn[j % ScreenQty].Enabled = true;
                        }
                        _contentsBtn[j % ScreenQty].ForeColor = Color.Empty;
                        break;
                    case ContentsMode.Custom:
                        break;
                    default:
                        break;
                }
            }
            flowLayoutPanelContents.Show();

        }

        private void SetCustomerContents(int _dtRow)
        {
            //당일거래유무
            if (_dtCust.Rows.Count > _dtRow && A.GetString(_dtCust.Rows[_dtRow]["DT_LAST"]) == POSGlobal.SaleDt)
            {
                //외상금액 설정 밀줕 설정
                if (_dtCust.Rows.Count > _dtRow && A.GetDecimal(_dtCust.Rows[_dtRow]["AM_NONPAID_SUM"]) > 0)
                {
                    _contentsBtn[_dtRow % ScreenQty].Appearance.Font = FontDealToday;
                }
                else if (_dtCust.Rows.Count > _dtRow && A.GetDecimal(_dtCust.Rows[_dtRow]["AM_NONPAID_SUM"]) < 0)
                {
                    _contentsBtn[_dtRow % ScreenQty].Appearance.Font = FontDealToday;
                    _contentsBtn[_dtRow % ScreenQty].Text = "㉦ " + _contentsBtn[_dtRow % ScreenQty].Text;
                }
                else
                {
                    _contentsBtn[_dtRow % ScreenQty].Appearance.Font = FontDeal;
                }


                //거래발생+외상(빨간색) / 거래발생+현금or카드(파란색)
                if (A.GetDecimal(_dtCust.Rows[_dtRow]["AM_NONPAID_TODAY"]) != 0) //당일 외상거래
                {
                    _contentsBtn[_dtRow % ScreenQty].ForeColor = ColorNonPaid;
                }
                else if (A.GetDecimal(_dtCust.Rows[_dtRow]["AM_PAY_TODAY"]) != 0) //당일 현금거래 
                {
                    _contentsBtn[_dtRow % ScreenQty].ForeColor = ColorCash;
                }
                else
                {
                    _contentsBtn[_dtRow % ScreenQty].ForeColor = Color.Empty;
                }

            }
            else
            {
                //외상금액 설정 밀줕 설정
                if (_dtCust.Rows.Count > _dtRow && A.GetDecimal(_dtCust.Rows[_dtRow]["AM_NONPAID_SUM"]) > 0)
                {
                    _contentsBtn[_dtRow % ScreenQty].Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents, FontStyle.Underline);
                }
                else if (_dtCust.Rows.Count > _dtRow && A.GetDecimal(_dtCust.Rows[_dtRow]["AM_NONPAID_SUM"]) < 0)
                {
                    _contentsBtn[_dtRow % ScreenQty].Appearance.Font = new Font("카이겐고딕 KR Regular", fontSizeContents, FontStyle.Underline);
                    _contentsBtn[_dtRow % ScreenQty].Text = "㉦ " + _contentsBtn[_dtRow % ScreenQty].Text;
                }
                else
                {
                    _contentsBtn[_dtRow % ScreenQty].Appearance.Font = FontDefault;
                    _contentsBtn[_dtRow % ScreenQty].ForeColor = Color.Empty;
                }

                _contentsBtn[_dtRow % ScreenQty].ForeColor = Color.Empty;

            }
        }

        private void SetItemType(DataTable _dt)
        {
            //초기화
            for (int j = 0; j < 40; j++)
            {
                _ItemTypeBtn[j].Text = string.Empty;
                _ItemTypeBtn[j].Tag = string.Empty;
                _ItemTypeBtn[j].Enabled = false;
            }

            //데이터 넣기
            for (int j = 0; j < _dt.Rows.Count; j++)
            {
                _ItemTypeBtn[j].Text = _dt.Rows[j]["NM_ITEM"].ToString().Replace("\\r\\n", Environment.NewLine);
                _ItemTypeBtn[j].Tag = _dt.Rows[j]["CD_ITEM"].ToString();
                _ItemTypeBtn[j].Enabled = true;

                if (j == ScreenQty - 1)
                    break;
            }
        }
        #endregion Contents 데이터 바인딩 

        /// <summary>
        /// 금액 계산
        /// </summary>
        /// <param name="sender"></param>
        private void CalcAmt(object sender)
        {
            decimal PreNonPaidAmt = A.GetDecimal(txtPreNonPaidAmt.EditValue); //외상잔액
            decimal PaySumAmt = A.GetDecimal(txtPaySumAmt.EditValue);       //매출합계금액
            decimal ReceiveAmt = A.GetDecimal(txtReceiveAmt.EditValue);     //받을금액 = 매출 합계금액 + 외상잔액
            decimal InputAmt = A.GetDecimal(txtQty.EditValue);              //키패드입력금액
            decimal SaleAmt = A.GetDecimal(txtPayAmt.EditValue);             //매출금액
            decimal VatAmt = A.GetDecimal(txtVatAmt.EditValue);             //부가세

            decimal PayAmt = A.GetDecimal(txtClsPayAmt.EditValue);          //입금금액
            decimal CreditAmt = A.GetDecimal(txtClsCreditAmt.EditValue);    //신용카드
            decimal NonPayAmt = A.GetDecimal(txtClsNonPaidAmt.EditValue);   //외상금액 = 매출합계금액 -  (입금금액 + 신용카드)
            decimal TotalNonPayAmt = A.GetDecimal(txtTotalNonPaidAmt.EditValue);   //외상금액 = 매출합계금액 -  (입금금액 + 신용카드)
            
            decimal DiscountAmt = A.GetDecimal(txtDiscountAmt.EditValue);   //할인금액
            //계산되는 금액 : 받을 금액 - (신용카드 + 외상금액)
            decimal ClacAmt = ReceiveAmt - (PayAmt + CreditAmt + TotalNonPayAmt);

            //20200702 선수금인 상태에서도 입금 가능하게 변경함
            //받을 금액이 0보다 작으면 암것도 안함
            //if (gridViewItem.RowCount > 0 && ClacAmt <= 0)
            //{
                //ClearKeyPad();
                //return;
            //}


            if (sender != null)
            {
                #region
                switch (((DevExpress.XtraEditors.SimpleButton)sender).Name)
                {
                    case "btnCtrPayDeposit": //입금버튼
                                             //키패드 입력 금액이 없으면 자동으로 전체금액이 입금 금액
                        if (A.GetDecimal(txtQty.EditValue) == 0)
                        {
                            if (ClacAmt > 0)
                            {
                                txtClsPayAmt.EditValue = ClacAmt;
                                txtClsNonPaidAmt.EditValue = ReceiveAmt > PaySumAmt ? ClacAmt - ReceiveAmt : 0;
                            }
                        }
                        else
                        {
                            txtClsPayAmt.EditValue = InputAmt;
                            txtClsNonPaidAmt.EditValue = ReceiveAmt - (InputAmt + CreditAmt);
                        }

                        break;

                    case "btnCtrPayCredit": //신용카드 버튼
                                            //키패드 입력 금액이 없으면 자동으로 전체금액이 입금 금액
                        if (A.GetDecimal(txtQty.EditValue) == 0)
                        {
                            if (ClacAmt > 0)
                            {
                                txtClsCreditAmt.EditValue = ClacAmt;
                                txtClsNonPaidAmt.EditValue = ReceiveAmt > PaySumAmt ? PaySumAmt - ReceiveAmt : 0;
                            }
                        }
                        else
                        {
                            txtClsCreditAmt.EditValue = InputAmt;
                            txtClsNonPaidAmt.EditValue = ReceiveAmt - (InputAmt + PayAmt);
                        }
                        break;

                    case "btnCtrPayNon": //외상버튼
                                         //키패드 입력 금액이 없으면 자동으로 전체금액이 입금 금액
                        if (A.GetDecimal(txtQty.EditValue) == 0)
                        {
                            txtClsNonPaidAmt.EditValue = ReceiveAmt - (PayAmt + CreditAmt);
                            txtTotalNonPaidAmt.EditValue = A.GetDecimal(txtReceiveAmt.EditValue) - (A.GetDecimal(txtClsPayAmt.EditValue) + A.GetDecimal(txtClsCreditAmt.EditValue));
                        }
                        else
                        {
                            txtClsNonPaidAmt.EditValue = InputAmt;
                        }
                        break;
                    case "btnCtrDiscount": //할인버튼
                        txtDiscountAmt.EditValue = InputAmt;
                        txtPaySumAmt.EditValue = SaleAmt - InputAmt;
                        txtReceiveAmt.EditValue = PreNonPaidAmt + SaleAmt - InputAmt;
                        txtClsNonPaidAmt.EditValue = PreNonPaidAmt + SaleAmt - InputAmt - (PayAmt + CreditAmt);

                        break;
                    case "btnCtrDiscountCancel": //할인취소버튼
                        txtDiscountAmt.EditValue = 0;
                        txtPaySumAmt.EditValue = SaleAmt - InputAmt;
                        txtReceiveAmt.EditValue = PreNonPaidAmt + SaleAmt - InputAmt;
                        txtClsNonPaidAmt.EditValue = PreNonPaidAmt + SaleAmt - InputAmt - (PayAmt + CreditAmt);

                        break;
                    default:
                        break;
                }

                if (A.GetDecimal(txtQty.EditValue) != 0)
                    ClearKeyPad();
                #endregion
            }
            else
            {
                //외상잔액
                txtClsNonPaidAmt.EditValue = ReceiveAmt - (PayAmt + CreditAmt);
                //합계금액
                txtPaySumAmt.EditValue = SaleAmt - InputAmt - DiscountAmt;
                //총금액
                txtReceiveAmt.EditValue = PreNonPaidAmt + SaleAmt - InputAmt - DiscountAmt;
            }

            //총외상잔액은 입금액(신용카드 포함)이 판매액이 전부 입금되거나, 혹은 판매액보다 금액이 클때 들어가고
            //혹은 적은 입금액과 외상 버튼 클릭시 총 외상잔액에 표현되게 한다.
            //총외상잔액 = 외상잔액 + 매출액 + 외상금액 - (입금액 + 신용카드)
            if (PayAmt + CreditAmt >= ReceiveAmt)
            {
                //총외상잔액
                txtTotalNonPaidAmt.EditValue = A.GetDecimal(txtReceiveAmt.EditValue) - (A.GetDecimal(txtClsPayAmt.EditValue) + A.GetDecimal(txtClsCreditAmt.EditValue));
            }
            else
            {

            }
        }

        #region 임시저장

        private string _slipFlagTmp;
        private void TempSlipSave()
        {
            #region 프리폼 정보 테이블에 넣어줌
            _dtTempH = _dtH.Clone();
            _dtTempL = _dtL.Clone();
            _dtTempPay = _dtPay.Clone();

            _slipFlagTmp = SlipFlag;

            JobStatus = JobMode.NewAfterRead;
            IsSlipInputing = true;

            //POS_SOH table
            if (_dtTempH.Rows.Count == 0)
            {
                DataRow drNew = _dtTempH.NewRow();
                drNew["NO_SO"] = string.Empty;
                drNew["DT_SO"] = dtpSale.Text;
                drNew["CD_CUST"] = CustomerCode;
                drNew["NM_CUST"] = CustomerName;
                drNew["YN_RETURN"] = "N";
                drNew["FG_SO"] = SlipFlag;
                drNew["AM_DISCOUNT"] = 0;

                _dtTempH.Rows.Add(drNew);
            }
            //else
            //{
            //    _dtTempH.Rows[0]["NO_SO"] = string.Empty;
            //    _dtTempH.Rows[0]["DT_SO"] = dtpSale.Text;
            //    _dtTempH.Rows[0]["CD_CUST"] = CustomerCode;
            //    _dtTempH.Rows[0]["NM_CUST"] = CustomerName;
            //    _dtTempH.Rows[0]["YN_RETURN"] = "N";
            //    _dtTempH.Rows[0]["FG_SO"] = SlipFlag;
            //    _dtTempH.Rows[0]["AM_DISCOUNT"] = txtDiscountAmt.EditValue;
            //}

            //POS_SOL
            _dtTempL = gridItem.DataSource as DataTable;

            //POS_CLSH table
            if (_dtTempPay.Rows.Count == 0)
            {
                DataRow drNew = _dtTempPay.NewRow();

                //결제테이블 데이터 업데이트
                drNew["NO_CLS"] = string.Empty;
                drNew["DT_CLS"] = dtpSale.Text;
                drNew["CD_CUST"] = CustomerCode;
                drNew["YN_RETURN"] = "N";
                drNew["NO_EMP"] = POSGlobal.EmpCode;
                drNew["NO_SO"] = SlipNo;
                drNew["AM_PAY"] = A.GetDecimal(txtClsPayAmt.EditValue);
                drNew["AM_CREDIT"] = A.GetDecimal(txtClsCreditAmt.EditValue);
                drNew["AM_NONPAID"] = A.GetDecimal(txtClsNonPaidAmt.EditValue);

                _dtTempPay.Rows.Add(drNew);
            }
            //else
            //{
            //   _dtTempPay.Rows[0]["NO_CLS"] = string.Empty;
            //   _dtTempPay.Rows[0]["DT_CLS"] = dtpSale.Text;
            //   _dtTempPay.Rows[0]["CD_CUST"] = CustomerCode;
            //   _dtTempPay.Rows[0]["YN_RETURN"] = "N";
            //   _dtTempPay.Rows[0]["NO_EMP"] = POSGlobal.EmpCode;
            //   _dtTempPay.Rows[0]["NO_SO"] = SlipNo;
            //   _dtTempPay.Rows[0]["AM_PAY"] = A.GetDecimal(txtClsPayAmt.EditValue);
            //   _dtTempPay.Rows[0]["AM_CREDIT"] = A.GetDecimal(txtClsCreditAmt.EditValue);
            //   _dtTempPay.Rows[0]["AM_NONPAID"] = A.GetDecimal(txtClsNonPaidAmt.EditValue);
            //}
            SlipNo = string.Empty;
            _slipNoPay = string.Empty;
            #endregion 프리폼 정보 테이블에 넣어줌
        }

        private void TempSlipView()
        {
            if (_dtTempH.Rows.Count == 0) return;

            if (_dtTempH.Rows.Count == 1)
            {
                _dtH = _dtTempH.Copy();
                _dtL = _dtTempL.Copy();
                _dtPay = _dtTempPay.Copy();

                SlipFlag = _slipFlagTmp;
                if (SlipFlag == "P")
                {
                    panelPaid.Visible = true;
                }
                else
                {
                    panelPaid.Visible = false;
                }

                CustomerCode = _dtTempH.Rows[0]["CD_CUST"].ToString();
                CustomerName = _dtTempH.Rows[0]["NM_CUST"].ToString();
                txtCust.Text = CustomerName = _dtTempH.Rows[0]["NM_CUST"].ToString();

                dtpSale.DateTimeChanged -= DtpSale_DateTimeChanged;
                
                //20200527 고정이면 영업날짜로 아니면 입력한 날짜로 다시 바꿈
                dtpSale.Text = IsFixSaleDt ? POSGlobal.SaleDt : A.GetString(_dtTempH.Rows[0]["DT_SO"]);
                dtpSale.DateTimeChanged += DtpSale_DateTimeChanged;
                txtDiscountAmt.EditValue = A.GetDecimal(_dtTempH.Rows[0]["AM_DISCOUNT"]);

                //결제정보 테이블에 데이터가 있을때만 조회
                if (_dtTempPay.Rows.Count == 1)
                {
                    txtClsPayAmt.EditValue = A.GetDecimal(_dtTempPay.Rows[0]["AM_PAY"]);
                    txtClsCreditAmt.EditValue = A.GetDecimal(_dtTempPay.Rows[0]["AM_CREDIT"]);
                    txtClsNonPaidAmt.EditValue = A.GetDecimal(_dtTempPay.Rows[0]["AM_NONPAID"]);
                    _slipNoPay = _dtTempPay.Rows[0]["NO_CLS"].ToString();
                }
                else
                {
                    txtClsPayAmt.EditValue = 0;
                    txtClsCreditAmt.EditValue = 0;
                    txtClsNonPaidAmt.EditValue = 0;
                    _slipNoPay = string.Empty;
                }
                //화면 조작 모드 
                JobStatus = JobMode.NewAfterRead;
                SlipNo = string.Empty;
            }
            gridItem.Binding(_dtTempL, true);

            //외상잔액
            txtPreNonPaidAmt.EditValue = A.GetDecimal(DBHelper.ExecuteScalar("USP_POS_GET_CUST_NONPAID_AMT", new object[] { POSGlobal.StoreCode, CustomerCode, SlipNo, dtpSale.Text }));

            txtTotalNonPaidAmt.EditValue = 0;
            SetGridFooter();
            CalcAmt(null);

            //선택한 전표의 앞뒤 전표 가져오기
            CheckPreNextSlipNo(SlipNo);
            //gridViewItem.BestFitColumns();

            //총외상잔액
            //결제정보 테이블에 데이터가 있을때만 조회
            //if (_dsTemp.Tables[2].Rows.Count == 1)
            //    txtTotalNonPaidAmt.EditValue = A.GetDecimal(txtPreNonPaidAmt.EditValue) + A.GetDecimal(txtPaySumAmt.EditValue) - (A.GetDecimal(txtClsPayAmt.EditValue) + A.GetDecimal(txtClsCreditAmt.EditValue));

            IsContentScreen = true;
            LastSlipYN = true;
        }
        #endregion 임시저장

        private bool VerifyCheck(object sender)
        {
            bool result = true;
            object[] VerifyObj = new object[] { gridViewItem, btnCtrPayDeposit, btnCtrPayCredit, btnCtrPayNon, btnCtrDiscount, btnCtrDiscountCancel, btnCtrPayCancel, btnCtrPayDone, btnCtrFindItem };

            

            foreach (object obj in VerifyObj)
            {
                if (sender == obj)
                {
                    if (JobStatus == JobMode.Read)
                    {
                        ShowMessageBoxA("결제완료된 거래입니다.", MessageType.Warning);
                        result = false;
                        break;
                    }
                    else
                    {
                        if (obj != btnCtrFindItem && CustomerCode == string.Empty)
                        {
                            ShowMessageBoxA("거래처를 먼저 선택해주세요.", MessageType.Warning);
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private void SetGridFooter()
        {
            decimal PreNonPaidAmt = A.GetDecimal(txtPreNonPaidAmt.EditValue);       //외상잔액
            decimal ReceiveAmt = A.GetDecimal(txtReceiveAmt.EditValue);             //받을금액
            decimal DiscountAmt = A.GetDecimal(txtDiscountAmt.EditValue);             //할인액


            decimal SaleAmt = A.GetDecimal(gridViewItem.Columns["AM"].SummaryItem.SummaryValue);
            decimal VatAmt = A.GetDecimal(gridViewItem.Columns["AM_VAT"].SummaryItem.SummaryValue);

            txtPayAmt.EditValue = SaleAmt;
            txtVatAmt.EditValue = VatAmt;

            //txtPaySumAmt.EditValue = SaleAmt + VatAmt;

            //txtClsPayAmt.EditValue = 0;
            //txtClsNonPaidAmt.EditValue = 0;
            //txtClsCreditAmt.EditValue = 0;
            //txtTotalNonPaidAmt.EditValue = 0;
            //txtDiscountAmt.EditValue = 0;

            txtPaySumAmt.EditValue = SaleAmt - DiscountAmt;
            txtReceiveAmt.EditValue = SaleAmt + PreNonPaidAmt - DiscountAmt;
            //외상금액 = 받을금액 - (입금금액 + 신용카드)
            txtClsNonPaidAmt.EditValue = SaleAmt + PreNonPaidAmt - DiscountAmt - (A.GetDecimal(txtClsPayAmt.EditValue) + A.GetDecimal(txtClsCreditAmt.EditValue));

            //if (_jobMode == JobMode.New)
            //{
            //    //txtReceiveAmt.EditValue = PayAmt + VatAmt + PreNonPaidAmt;
            //}
            //else
            //{
            //    // txtReceiveAmt.EditValue = 0;
            //}
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
                if (_contentsLv == 0)
                {
                    btnTop.Enabled = false;
                }
                else
                {
                    btnTop.Enabled = true;
                }
            }
        }

        public void SetUpdateTotalProgress(int percentage)
        {
            progressBarContents.EditValue = percentage;
            progressBarContentTop.EditValue = percentage;

            //Application.DoEvents();
        }

        //거래처별 외상여부 변수
        bool IsNonPaid { get; set; } = true;

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

        //품목 초기화
        private void InitItemTable()
        {
            //_dtItem = _dtItemAll;// DBHelper.GetDataTable("USP_POS_ITEM_S", new object[] { POSGlobal.StoreCode, string.Empty, string.Empty, string.Empty });
            if (ContentsItemType == ContentsItem.Item)
            {
                _dtItem = _dtItemAll;
                ContentsLv = 1;
            }
            else if (ContentsItemType == ContentsItem.ItemType)
            {
                _dtItem = _dtItemTypeAll;
                ContentsLv = 0;
            }

            SetContents();
            SetNaviButton();
        }

        private string _itemCode;
        private string _itemName;

        private decimal _Qt = 0;
        private decimal _QtUnit = 0;
        private decimal _itemPrice = 0;
        private string _itemDescript = string.Empty;
        private string _unitCode = string.Empty;
        private string _unitName = string.Empty;
        private string _vatType = string.Empty;
        private decimal _vatRate = 0.0m;
        private string _TmpLastFlag = "NEW";

        //대분류 데이터셋
        DataTable _dtItemType;

        //대분류 패널의 행열 개수를 위한 변수
        readonly int RowQty = 10;
        readonly int ColQty = 4;

        //화면에 보이는 버튼 개수
        public int ScreenQty
        {
            get { return RowQty * ColQty; }
        }

        string _selectedCode = "CD_CUST";
        string _selectedName = "NM_CUST";

        private string CustomerCode { get; set; }

        private string _CustomerName = string.Empty;

        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                txtCust.Text = value;
                _CustomerName = value;
            }
        }

        private ContentsMode _contentsType = ContentsMode.Customer;

        public ContentsMode ContentsType
        {
            get { return _contentsType; }
            set
            {
                _contentsType = value;
                if (value == ContentsMode.Customer)
                {
                    _selectedCode = "CD_CUST";
                    _selectedName = "NM_CUST";
                    btnCtrFindItem.Text = "거래처\n찾기";

                }
                else
                {
                    _selectedCode = "CD_ITEM";
                    _selectedName = "NM_ITEM";
                    btnCtrFindItem.Text = "상품\n찾기";
                }

            }
        }

        private void InitForm()
        {
            //버튼 40개 생성해야 하므로 픽스
            //거래처, 품목 데이터 가져오기
            
            dtpSale.DateTimeChanged -= DtpSale_DateTimeChanged;
            dtpSale.Text = POSGlobal.SaleDt;
            dtpSale.DateTimeChanged += DtpSale_DateTimeChanged;

            CH.SetDateEditFont(dtpSale, 16F);

            _dtCust = SearchCust(new object[] { POSGlobal.StoreCode, string.Empty, "Y", dtpSale.Text });
            DataSet dsItemAll = SearchItemALL(new object[] { POSGlobal.StoreCode, "S" });

            _dtItemType = dsItemAll.Tables[0];
            _dtItem = dsItemAll.Tables[1];

            _dtItemAll = _dtItem.Copy();
            _dtCustAll = _dtCust.Copy();
            _dtItemTypeAll = _dtItemType.Copy();

            //좌측 페널
            //panelContents.Parent = panelDetail;
            //panelContents.Size = panelDetail.Size;// w Size(470, 890);
            //panelContents.Location = new Point(0, 0);
            //gridViewItem.OptionsView.ShowFooter = true;

            //GridColumn column = gridViewItem.Columns["AM"];
            //column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            _contentsBtn = new DevExpress.XtraEditors.SimpleButton[ScreenQty];

            IsFirst = true;

            #region 좌측 컨트롤 패널
            flowLayoutPanelContents.SuspendLayout();
            //콘텐츠 버튼 생성 (40개) 픽스
            for (int i = 0; i < ScreenQty; i++)
            {
                _contentsBtn[i] = new DevExpress.XtraEditors.SimpleButton();
                _contentsBtn[i].Name = "btnContents" + i.ToString();
                _contentsBtn[i].Size = new Size(145, 70);
                _contentsBtn[i].TabIndex = i;
                _contentsBtn[i].Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
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

            #region 대분류 컨트롤 패널

            //flowLayoutPanelItemType.Size = new Size(600, 300);

            _ItemTypeBtn = new DevExpress.XtraEditors.SimpleButton[ScreenQty];

            flowLayoutPanelItemType.SuspendLayout();
            //콘텐츠 버튼 생성 (40개) 픽스
            for (int i = 0; i < RowQty * ColQty; i++)
            {
                _ItemTypeBtn[i] = new DevExpress.XtraEditors.SimpleButton();
                _ItemTypeBtn[i].Name = "btnItemType" + i.ToString();
                _ItemTypeBtn[i].Size = new Size(155, 62);
                _ItemTypeBtn[i].TabIndex = i;
                _ItemTypeBtn[i].Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                _ItemTypeBtn[i].Appearance.Font = FontDefault;
                _ItemTypeBtn[i].AllowFocus = false;
                _ItemTypeBtn[i].Click += ItemType_Click;
                _ItemTypeBtn[i].Parent = flowLayoutPanelItemType;

                if ((i / 4) % 2 == 0) //짝수
                    CH.SetButtonApperance(_ItemTypeBtn[i], FontDefault, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);
                else
                    CH.SetButtonApperance(_ItemTypeBtn[i], FontDefault, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            }
            flowLayoutPanelItemType.ResumeLayout();
            #endregion 대분류 컨트롤 패널

            //패널에 데이터 넣기
            SetItemType(_dtItemTypeAll);
            //SetContents();
            //SetNaviButton();
            OnInsert();

            IsFirst = false;
        }

        #region 오버라이드 메서드
        public override void OnInsert()
        {
            try
            {
                //키패드 초기화
                ClearKeyPad();
                //그리드 초기화
                SlipFlag = "A";
                Search(A.GetDummyString);
                dtpSale.DateTimeChanged -= DtpSale_DateTimeChanged;
                dtpSale.Text = POSGlobal.SaleDt;
                dtpSale.DateTimeChanged += DtpSale_DateTimeChanged;
                //거레처 초기화

                if (!IsFirst)
                {
                    _dtCust = SearchCust(new object[] { POSGlobal.StoreCode, string.Empty, "Y", dtpSale.Text });
                    _dtCustAll = _dtCust;
                }

                lblCustLsatSaleDt.Text = string.Empty;
                lblCustCarNo.Text = string.Empty;
                lblCustTel.Text = string.Empty;

                DataRow drNewHeader = _dtH.NewRow();
                _dtH.Rows.Add(drNewHeader);

                DataRow drNewCls = _dtPay.NewRow();
                _dtPay.Rows.Add(drNewCls);

                SlipNo = txtNoSlip.Text = string.Empty;
                _slipNoPay = string.Empty;
                CustomerCode = string.Empty;
                CustomerName = string.Empty;

                //외상잔액 (조회에서 대신해줌 주석)
                //object obj = DBHelper.ExecuteScalar("USP_POS_GET_CUST_NONPAID_AMT", new object[] { POSGlobal.StoreCode, CustomerCode, SlipNo, dtpSale.Text });
                //txtPreNonPaidAmt.EditValue = A.GetDecimal(obj);

                JobStatus = JobMode.New;
                ContentsType = ContentsMode.Customer;

                SetContents();
                SetNaviButton();
                SetGridFooter();

                txtClsPayAmt.EditValue = 0;
                txtClsCreditAmt.EditValue = 0;
                txtClsNonPaidAmt.EditValue = 0;
                txtTotalNonPaidAmt.EditValue = 0;
                txtDiscountAmt.EditValue = 0;

                txtPreNonPaidAmt.EditValue = 0;
                txtPayAmt.EditValue = 0;
                txtVatAmt.EditValue = 0;
                txtDiscountAmt.EditValue = 0;
                txtPaySumAmt.EditValue = 0;
                txtReceiveAmt.EditValue = 0;

                //SetItemTypeScreen(IsItemTypeScreen);
                ContentsLv = 0;
                IsContentScreen = true;

                btnCtrModify.Text = "수정";
                _payMode = PayMode.None;

                //임시저장 초기화
                if (_dtTempH != null) _dtTempH.Clear();
                if (_dtTempL != null) _dtTempL.Clear();
                if (_dtTempPay != null) _dtTempPay.Clear();

                //임시저장 초기화
                if (_dtH != null) _dtH.Clear();
                if (_dtL != null) _dtL.Clear();
                if (_dtPay != null) _dtPay.Clear();

                if(SlipNoTemp!= null) SlipNoTemp.Clear();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public override void OnView()
        {
            try
            {
                Search(txtNoSlip.Text);

                if (gridViewItem.RowCount == 0)
                {
                    ShowMessageBoxA("조회 내역이 존재하지 않습니다.\n다시 확인해주세요", MessageType.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private string _slipNo = string.Empty;

        public string SlipNo
        {
            get { return _slipNo; }
            set
            {
                _slipNo = value;
                txtNoSlip.Text = value;
            }
        }

        List<string> SlipNoTemp { get; set; }

        string _slipNoPay = string.Empty;
        public override void OnSave()
        {
            try
            {
                string SaleDt = dtpSale.Text;

                #region 전표번호 채번
                if (txtNoSlip.Text == string.Empty)
                {
                    SlipNo = A.GetPOSSlipNo(POSGlobal.StoreCode, SaleDt, "", 4);
                }
                else
                {
                    SlipNo = txtNoSlip.Text;
                }

                if (_slipNoPay == string.Empty)
                    _slipNoPay = A.GetPOSSlipNo(POSGlobal.StoreCode, SaleDt, "PAY", 4);
                #endregion 전표번호 채번

                //미수금 입금시 FG_SO = "P"  A:일반  R:반품  P:판매없이 수금
                if (gridViewItem.RowCount == 0)
                {
                    SlipFlag = "P";
                }

                #region 프리폼 정보 테이블에 넣어줌
                if (_dtH.Rows.Count == 0)
                {
                    DataRow drNew = _dtH.NewRow();
                    drNew["NO_SO"] = SlipNo;
                    drNew["DT_SO"] = SaleDt;
                    drNew["CD_CUST"] = CustomerCode;
                    drNew["YN_RETURN"] = "N";
                    drNew["FG_SO"] = SlipFlag;
                    drNew["AM_DISCOUNT"] = txtDiscountAmt.EditValue;

                    _dtH.Rows.Add(drNew);
                }
                else
                {
                    _dtH.Rows[0]["NO_SO"] = SlipNo;
                    _dtH.Rows[0]["DT_SO"] = SaleDt;
                    _dtH.Rows[0]["CD_CUST"] = CustomerCode;
                    _dtH.Rows[0]["YN_RETURN"] = "N";
                    _dtH.Rows[0]["FG_SO"] = SlipFlag;
                    _dtH.Rows[0]["AM_DISCOUNT"] = txtDiscountAmt.EditValue;
                }


                if (_dtPay.Rows.Count == 0)
                {
                    DataRow drNew = _dtPay.NewRow();

                    //결제테이블 데이터 업데이트
                    drNew["NO_CLS"] = _slipNoPay;
                    drNew["DT_CLS"] = SaleDt;
                    drNew["CD_CUST"] = CustomerCode;
                    drNew["YN_RETURN"] = "N";
                    drNew["NO_EMP"] = POSGlobal.EmpCode;
                    drNew["NO_SO"] = SlipNo;
                    drNew["AM_PAY"] = A.GetDecimal(txtClsPayAmt.EditValue);
                    drNew["AM_CREDIT"] = A.GetDecimal(txtClsCreditAmt.EditValue);
                    drNew["AM_NONPAID"] = A.GetDecimal(txtClsNonPaidAmt.EditValue);

                    _dtPay.Rows.Add(drNew);
                }
                else
                {
                    _dtPay.Rows[0]["NO_CLS"] = _slipNoPay;
                    _dtPay.Rows[0]["DT_CLS"] = SaleDt;
                    _dtPay.Rows[0]["CD_CUST"] = CustomerCode;
                    _dtPay.Rows[0]["YN_RETURN"] = "N";
                    _dtPay.Rows[0]["NO_EMP"] = POSGlobal.EmpCode;
                    _dtPay.Rows[0]["NO_SO"] = SlipNo;
                    _dtPay.Rows[0]["AM_PAY"] = A.GetDecimal(txtClsPayAmt.EditValue);
                    _dtPay.Rows[0]["AM_CREDIT"] = A.GetDecimal(txtClsCreditAmt.EditValue);
                    _dtPay.Rows[0]["AM_NONPAID"] = A.GetDecimal(txtClsNonPaidAmt.EditValue);
                }

                //                if(_jobMode == JobMode.Modify)
                //                {
                //                    if (_dtH.Rows[0].RowState != DataRowState.Modified) _dtH.Rows[0].SetModified();
                ////                    if (_dtPay.Rows[0].RowState != DataRowState.Modified) _dtPay.Rows[0].SetModified();
                //                }
                //                else
                //                {
                //                    _dtH.AcceptChanges();
                //                    _dtPay.AcceptChanges();

                //                    _dtH.Rows[0].SetAdded();
                //                    _dtPay.Rows[0].SetAdded();
                //                }
                #endregion 프리폼 정보 테이블에 넣어줌

                DataTable dtHChange = _dtH.GetChanges();
                DataTable dtLChange = gridItem.GetChanges();
                DataTable dtPayChange = _dtPay.GetChanges();

                List<object> listParam = new List<object>();
                listParam.Add(SlipNo);
                listParam.Add(SaleDt);
                listParam.Add(CustomerCode);

                if (dtHChange == null && dtLChange == null && dtPayChange == null)
                {
                    ShowMessageBoxA("변경된 내용이 존재하지 않습니다.", MessageType.Warning);
                    return;
                }

                if (Save(dtHChange, dtLChange, dtPayChange, listParam))
                {
                    if (dtHChange != null) dtHChange.AcceptChanges();
                    if (dtLChange != null) gridItem.AcceptChanges();
                    if (dtPayChange != null) dtPayChange.AcceptChanges();

                    SaveAfterUpdate(new object[] { POSGlobal.StoreCode, SlipNo });

                    //20200629 임시 전표가 있으면, 상태 업데이트
                    if (SlipNoTemp!= null && SlipNoTemp.Count > 0)
                    {
                        for (int i = 0; i < SlipNoTemp.Count; i++)
                        {
                            UpdateTemp(new object[] { POSGlobal.StoreCode, SlipNoTemp[i], "S" });
                        }
                        SlipNoTemp.Clear();
                    }
                    //저장후 재계산 보이기
                    SetGridFooter();
                    //CalcAmt(null);
                    //ShowMessageBoxA("저장이 완료되었습니다.", MessageType.Information);
                    //txtNoSlip.Text = _slipNo;
                    Search(SlipNo);
                    //출력
                    BtnCtrPrint_Click(null, null);
                    //자동 신규
                    OnInsert();

                    IsSlipInputing = false;
                }

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public override void OnDelete()
        {
            try
            {
                bool result = false;

                if (ShowMessageBoxA("삭제하시겠습니까?", MessageType.Question) == DialogResult.OK)
                {
                    result = Delete(new object[] { POSGlobal.StoreCode, txtNoSlip.Text });

                    if (result)
                        ShowMessageBoxA("삭제가 완료되었습니다.", MessageType.Information);
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }
        #endregion 오버라이드 메서드

        DataTable _dtHInit;
        DataTable _dtLInit;
        DataTable _dtPayInit;

        private void Search(string pSlipNo)
        {
            //날짜 변환시 거래처 가져오는거 주석
            dtpSale.DateTimeChanged -= DtpSale_DateTimeChanged;

            string[] obj = new string[] { POSGlobal.StoreCode, pSlipNo };
            DataSet ds;

            //최초 조회시 빈 데이터를 넣어둠
            if (IsFirst)
            {
                ds = SearchSalesOrder(obj);

                _dtH = _dtHInit = ds.Tables[0];
                _dtL = _dtLInit = ds.Tables[1];
                _dtPay = _dtPayInit = ds.Tables[2];
            }
            else
            {
                // 재조회시 더미일때는 빈 데이터 셋 넣음
                if (pSlipNo == A.GetDummyString)
                {
                    _dtH = _dtHInit.Clone();
                    _dtL = _dtLInit.Clone();
                    _dtPay = _dtPayInit.Clone();
                }
                else
                {
                    ds = SearchSalesOrder(obj);

                    _dtH = ds.Tables[0];
                    _dtL = ds.Tables[1];
                    _dtPay = ds.Tables[2];
                }
            }
            if (_dtH.Rows.Count == 1)
            {
                SlipFlag = A.GetString(_dtH.Rows[0]["FG_SO"]);
                CustomerCode = A.GetString(_dtH.Rows[0]["CD_CUST"]);
                CustomerName = A.GetString(_dtH.Rows[0]["NM_CUST"]);
                dtpSale.Text = A.GetString(_dtH.Rows[0]["DT_SO"]);
                txtDiscountAmt.EditValue = A.GetDecimal(_dtH.Rows[0]["AM_DISCOUNT"]);
                txtVatAmt.EditValue = A.GetDecimal(_dtH.Rows[0]["AM_VAT"]);

                //20200626
                txtPayAmt.EditValue = A.GetDecimal(_dtH.Rows[0]["AM"]);
                
                //결제정보 테이블에 데이터가 있을때만 조회
                if (_dtPay.Rows.Count == 1)
                {
                    txtClsPayAmt.EditValue = A.GetDecimal(_dtPay.Rows[0]["AM_PAY"]);
                    txtClsCreditAmt.EditValue = A.GetDecimal(_dtPay.Rows[0]["AM_CREDIT"]);
                    txtClsNonPaidAmt.EditValue = A.GetDecimal(_dtPay.Rows[0]["AM_NONPAID"]);
                    _slipNoPay = _dtPay.Rows[0]["NO_CLS"].ToString();
                }
                else
                {
                    txtClsPayAmt.EditValue = 0;
                    txtClsCreditAmt.EditValue = 0;
                    txtClsNonPaidAmt.EditValue = 0;
                    _slipNoPay = string.Empty;
                }
                //화면 조작 모드 
                JobStatus = JobMode.Read;
                SlipNo = txtNoSlip.Text = pSlipNo;
                //대분류 창 숨기기
                navBottom.SelectedPage = navPageSaleInfo;
                //컨텐츠창 숨기기
                panelContents.Visible = false;
                //IsItemTypeScreen = false;

                
            }
            
            if (SlipFlag == "P")
            {
                panelPaid.Visible = true;
            }
            else
            {
                panelPaid.Visible = false;
            }

            gridViewItem.RowCountChanged -= GridViewItem_RowCountChanged;
            gridItem.Binding(_dtL, true);
            GridNavVisible();
            gridViewItem.RowCountChanged += GridViewItem_RowCountChanged;

            DataRow[] dr = _dtCustAll.Select("CD_CUST = '" + CustomerCode + "'");

            //외상잔액
            if (pSlipNo != A.GetDummyString)
            {
                txtPreNonPaidAmt.EditValue = A.GetDecimal(DBHelper.ExecuteScalar("USP_POS_GET_CUST_NONPAID_AMT", new object[] { POSGlobal.StoreCode, CustomerCode, pSlipNo, dtpSale.Text })); //A.GetDecimal(dr[0]["AM_NONPAID_SUM"]);//
                //선택한 거래처의 앞 뒤 전표 번호 가져오기
                CheckPreNextSlipNo(pSlipNo);
            }

            //20200708 키패드 초기화
            ClearKeyPad();
            //결제정보 페이지 보이기
            //navPay.SelectedPage = navPayInfo;

            //SetGridFooter();
            CalcAmt(null);
            
            //총외상잔액
            //결제정보 테이블에 데이터가 있을때만 조회
            if (_dtPay.Rows.Count == 1)
                txtTotalNonPaidAmt.EditValue = A.GetDecimal(txtReceiveAmt.EditValue) - (A.GetDecimal(txtClsPayAmt.EditValue) + A.GetDecimal(txtClsCreditAmt.EditValue));
            else
                txtTotalNonPaidAmt.EditValue = 0;

            dtpSale.DateTimeChanged += DtpSale_DateTimeChanged;
            
            

        }

        private void CheckPreNextSlipNo(string pSlipNo)
        {
            //선택한 거래처의 앞 뒤 전표 번호 가져오기
            DataTable dsSalesNo = DBHelper.GetDataTable("USP_GET_SO_NO", new object[] { POSGlobal.StoreCode, CustomerCode, pSlipNo, POSGlobal.SaleDt });

            if (dsSalesNo != null && dsSalesNo.Rows.Count > 0)
            {
                SalesNoPre = A.GetString(dsSalesNo.Rows[0]["NO_SO_PRE"]);
                SalesNoNext = A.GetString(dsSalesNo.Rows[0]["NO_SO_NEXT"]);

                if (JobStatus == JobMode.New || JobStatus == JobMode.NewAfterRead)
                    LastSlipYN = true;
                else
                    LastSlipYN = false;
            }
            else
            {
                SalesNoPre = string.Empty;
                SalesNoNext = string.Empty;

                btnSaleNext.Enabled = false;
                btnSalePre.Enabled = false;
            }
        }

        private void CheckPreNextSlipNoByDate()
        {
            //선택한 거래처의 앞 뒤 전표 번호 가져오기
            DataTable dsSalesNo = DBHelper.GetDataTable("USP_GET_SO_NO_BY_DATE", new object[] { POSGlobal.StoreCode, CustomerCode, dtpSale.Text });

            if (dsSalesNo != null && dsSalesNo.Rows.Count > 0)
            {
                SalesNoPre = A.GetString(dsSalesNo.Rows[0]["NO_SO_PRE"]);
                SalesNoNext = A.GetString(dsSalesNo.Rows[0]["NO_SO_NEXT"]);

                if (JobStatus == JobMode.New || JobStatus == JobMode.NewAfterRead)
                    LastSlipYN = true;
                else
                    LastSlipYN = false;
            }
            else
            {
                SalesNoPre = string.Empty;
                SalesNoNext = string.Empty;

                btnSaleNext.Enabled = false;
                btnSalePre.Enabled = false;
            }
        }

        #region ENUM 모음
        public enum JobMode
        {
            New,
            Modify,
            Read,
            NewAfterRead,
        }

        private enum PayMode
        {
            None,
            Paid
        }

        private enum InitButtonMode
        {
            Customer,
            Normal
        }

        enum ReceitMode
        {
            Single,
            Double,
            Etc
        }

        enum ContentsItem
        {
            Item,
            ItemType
        }

        #endregion ENUM 모음
    }
}
