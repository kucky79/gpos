using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace Bifrost.Grid
{
    public partial class aTree : TreeList
    {
        private SetControlBinding _binding = null;


        public aTree()
        {
            InitializeComponent();
        }

        protected override void InitLayout()
        {
            
            this.OptionsBehavior.Editable = false;
            this.OptionsBehavior.KeepSelectedOnClick = false;
            this.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.OptionsSelection.MultiSelect = true;
            this.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            this.OptionsNavigation.AutoFocusNewNode = true;

            base.InitLayout();
        }

        public void SetBinding(Control container,  object[] EnableControlsIfAdded)
        {
            _binding = new SetControlBinding(this,  container, EnableControlsIfAdded);
        }
        //============================진영 10-26 수정
        //============================ColumnInVislble 추가
        public void ColumnVislble(string[] objColumn, bool vislbleColumn)
        {
            for (int i = 0; i < objColumn.Length; i++)
            {
                this.Columns[objColumn[i]].Visible = vislbleColumn;
            }
        }

        public void ColumnVislble(string[] objColumn)
        {
            ColumnVislble(objColumn, false);
        }

        //public void ColumnInVislble(string[] objColumn)
        //{
        //    for (int i = 0; i < objColumn.Length; i++)
        //    {
        //        this.Columns[objColumn[i]].Visible = true;
        //    }
        //}
        //=================================
        public void ColumnReadOnly(string[] objColumn)
        {

            for (int i = 0; i < objColumn.Length; i++)
            {
                this.Columns[objColumn[i]].OptionsColumn.ReadOnly = false;
            }
        }

        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    TreeListHitInfo hi = CalcHitInfo(e.Location);
        //    DevExpress.XtraTreeList.Nodes.TreeListNode node = hi.Node;
        //    if (node != null)
        //    {
        //        if (Control.ModifierKeys == Keys.Control && Selection.Count > 0 && Selection.Contains(node))
        //            return;
        //    }
        //    base.OnMouseDown(e);
        //}

        private DataTable _dt = new DataTable();
        /// <summary>
        /// 그리드에 있는 dataSource 의 상태를 가져옵니다.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetChanges()
        {
            _dt = this.DataSource as DataTable;
            DataTable dtChange = _dt.GetChanges();
            return dtChange;
        }

        /// <summary>
        /// 그리드컨트롤에 있는 Datasoruce AcceptChange
        /// </summary>
        public void AcceptChanges()
        {
            if (_dt != null)
                _dt.AcceptChanges();
        }
    }
}
