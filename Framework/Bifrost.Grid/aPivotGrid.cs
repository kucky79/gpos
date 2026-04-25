using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Bifrost.Grid
{
    public partial class aPivotGrid : Component
    {
        public aPivotGrid()
        {
            InitializeComponent();
        }

        public aPivotGrid(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Binding(DataTable dt)
        {
            this.pivotGridControl1.DataSource = dt;
        }

        public void Setgrid(bool ShowDataHeaders, bool ShowColumnHeaders)
        {

        }
    }
}
