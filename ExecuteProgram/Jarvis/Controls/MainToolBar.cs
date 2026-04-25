using Bifrost.Helper;
using Bifrost.Win;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Bifrost.Controls
{
    public partial class MainToolBar : UserControlBase
    {
        private string _maintTitle;

        private string _saleDt = DateTime.Now.ToString("yyyy-MM-dd");

        Timer _timer = new Timer();

        public string SaleDt
        {
            get
            {
                return _saleDt;
            }

            set
            {
                _saleDt = value;
                if (_saleDt != null)
                    lblSaleDt.Text = FormatStringToDate(_saleDt);
            }
        }

        public string MainTitle
        {
            get
            {
                return _maintTitle;
            }

            set
            {
                _maintTitle = value;
                lblTitle.Text = _maintTitle;
            }
        }

        public static string FormatStringToDate(string tdrDate)
        {
            return DateTime.ParseExact(tdrDate.Replace("-", ""), "yyyyMMdd", null).ToString("yyyy-MM-dd");
        }

        public MainToolBar()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            btnExit.Click += BtnExit_Click; ;
            btnHome.Click += BtnHome_Click; ;
            btnLogout.Click += BtnLogout_Click;

            btnMin.Click += BtnMin_Click;
            btnRestore.Click += BtnRestore_Click;

            panelTop.MouseDown += PanelTop_MouseDown;
            panelTop.MouseMove += PanelTop_MouseMove;
            panelTop.MouseUp += PanelTop_MouseUp;

            panelTop.DoubleClick += PanelTop_DoubleClick;

            _timer.Interval = 1000;
            _timer.Start();
            _timer.Tick += _timer_Tick;

           
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            MdiForm.FormSizeChange();

            if (MdiForm.WindowState == FormWindowState.Maximized)
            {
                btnRestore.ToolTip = "이전 크기로 복원";
            }
            else
            {
                btnRestore.ToolTip = "최대화";
            }
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            MdiForm.FormMin();
        }

        private void PanelTop_DoubleClick(object sender, EventArgs e)
        {
            MdiForm.FormSizeChange();


        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void PanelTop_MouseUp(object sender, MouseEventArgs e)
        {
            MdiForm.ToolBar_MouseUp(e);
        }

        private void PanelTop_MouseMove(object sender, MouseEventArgs e)
        {
            MdiForm.ToolBar_MouseMove(e);
        }

        private void PanelTop_MouseDown(object sender, MouseEventArgs e)
        {
            MdiForm.ToolBar_MouseDown(e);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            MdiForm.FormLogOut();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            MdiForm.OnHomeClick();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            MdiForm.FormClose();
        }
    }
}
