using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bifrost.Grid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bifrost.Helper;

namespace Bifrost.Win.Controls.aControl
{
    public partial class aNavButton : UserControl
    {

        private GridView girdView;

        private aGrid _navGrid;

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

        public aGrid NavGrid
        {
            get { return _navGrid; }
            set
            {
                _navGrid = value;
                girdView = value.MainView as GridView;
                _navGrid.DataSourceChanged += _navGrid_DataSourceChanged;
            }
        }

        private void _navGrid_DataSourceChanged(object sender, EventArgs e)
        {
            GridView gridViewItem = ((aGrid)sender).MainView as GridView;

            //if (gridViewItem.RowCount == 0)
            //{
            //    this.Visible = false;
            //    return;
            //}

            //DevExpress.XtraEditors.VScrollBar vScrollBar = NavGrid.Controls.OfType<DevExpress.XtraEditors.VScrollBar>().FirstOrDefault();
            //if (vScrollBar != null && vScrollBar.Visible)
            //    this.Visible = true;
            //else
            //    this.Visible = false;

            //GridViewInfo viewInfo = gridViewItem.GetViewInfo() as GridViewInfo;
            //if (viewInfo.VScrollBarPresence == ScrollBarPresence.Visible)
            //{
            //    this.Visible = true;
            //}
            //else
            //{
            //    this.Visible = false;
            //}

        }

        private void GirdView_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            
        }


        public aNavButton()
        {
            InitializeComponent();
            InitEvent();
        }

        private void InitEvent()
        {
            CH.SetButtonApperance(btnGridUpMax, FontDefault, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);
            CH.SetButtonApperance(btnGridUp, FontDefault, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);
            CH.SetButtonApperance(btnGridDownMax, FontDefault, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);
            CH.SetButtonApperance(btnGridDown, FontDefault, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);


            btnGridUp.Click += BtnGridUp_Click;
            btnGridUpMax.Click += BtnGridUpMax_Click;
            btnGridDown.Click += BtnGridDown_Click;
            btnGridDownMax.Click += BtnGridDownMax_Click;

            this.DockChanged += ANavButton_DockChanged;
            this.SizeChanged += ANavButton_SizeChanged;

        }

        private void ANavButton_SizeChanged(object sender, EventArgs e)
        {

            int btnHeight = this.Size.Height / 5;
            int btnWidth = 150;

            btnGridUpMax.Size = new Size(btnWidth, btnHeight);
            btnGridUp.Size = new Size(btnWidth, btnHeight);

            btnGridDownMax.Size = new Size(btnWidth, btnHeight);
            btnGridDown.Size = new Size(btnWidth, btnHeight);
        }

        private void ANavButton_DockChanged(object sender, EventArgs e)
        {
            switch (this.Dock)
            {
                case DockStyle.None:
                    break;
                case DockStyle.Top:
                    btnGridUpMax.Dock = DockStyle.Left;
                    btnGridUp.Dock = DockStyle.Left;

                    btnGridDownMax.Dock = DockStyle.Right;
                    btnGridDown.Dock = DockStyle.Right;
                    break;
                case DockStyle.Bottom:
                    btnGridUpMax.Dock = DockStyle.Left;
                    btnGridUp.Dock = DockStyle.Left;

                    btnGridDownMax.Dock = DockStyle.Right;
                    btnGridDown.Dock = DockStyle.Right;
                    break;
                case DockStyle.Left:
                    btnGridUpMax.Dock = DockStyle.Top;
                    btnGridUp.Dock = DockStyle.Top;

                    btnGridDownMax.Dock = DockStyle.Bottom;
                    btnGridDown.Dock = DockStyle.Bottom;
                    break;
                case DockStyle.Right:
                    btnGridUpMax.Dock = DockStyle.Top;
                    btnGridUp.Dock = DockStyle.Top;

                    btnGridDownMax.Dock = DockStyle.Bottom;
                    btnGridDown.Dock = DockStyle.Bottom;
                    break;
                case DockStyle.Fill:
                    break;
                default:
                    break;
            }

            //BeginInvoke(new Action(() => { OnPaint(e); }));
        }

        #region 그리드 하단 조작 업다운 버튼 이벤트
        private void BtnGridDownMax_Click(object sender, EventArgs e)
        {
            if (girdView.RowCount > 0)
                girdView.FocusedRowHandle = girdView.RowCount - 1;
        }

        private void BtnGridUpMax_Click(object sender, EventArgs e)
        {
            if (girdView.RowCount == 0)
                return;

            girdView.FocusedRowHandle = 0;
        }

        private void BtnGridDown_Click(object sender, EventArgs e)
        {
            if (girdView.RowCount == 0)
                return;

            int index = (girdView.GetViewInfo() as GridViewInfo).RowsInfo[0].RowHandle + (girdView.GetViewInfo() as GridViewInfo).RowsInfo.Count - 1;
            if (index >= 0)
            {
                girdView.FocusedRowHandle = index;
                girdView.TopRowIndex = index;
            }
            else
                girdView.FocusedRowHandle = 0;
        }

        private void BtnGridUp_Click(object sender, EventArgs e)
        {
            if (girdView.RowCount == 0)
                return;

            int index = (girdView.GetViewInfo() as GridViewInfo).RowsInfo[0].RowHandle - (girdView.GetViewInfo() as GridViewInfo).RowsInfo.Count + 1;
            if (index >= 0)
                girdView.FocusedRowHandle = index;
            else
                girdView.FocusedRowHandle = 0;
        }
        #endregion 그리드 하단 조작 업다운 버튼 이벤트

    }
}
