using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Helper
{
    public partial class RptViewer : XtraForm
    {
        public RptViewer(IDocumentSource report)
        {
            InitializeComponent();
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}