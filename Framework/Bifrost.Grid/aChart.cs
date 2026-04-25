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
    public partial class aChart : Component
    {
        public aChart()
        {
            InitializeComponent();
        }

        public aChart(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Binding(DataTable dt)
        {
        }
    }
}
