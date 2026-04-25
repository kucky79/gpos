using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;

namespace Bifrost.Win.Controls
{
    [UserRepositoryItem("RegisteraDateEdit")]
    public class RepositoryItemaDateYear : RepositoryItemDateEdit
    {

        private static Image picBack = Images.btn_calendar;
        private static Image picBack_disable = Images.btn_calendar_disable;

        private static Image picClear = Images.txtClear;
        static RepositoryItemaDateYear()
        {
            RegisteraDateEdit();
        }

        public RepositoryItemaDateYear() : base()
        {
        }

        internal const string aDateEditName = "aDateEdit";

        public override string EditorTypeName
        {
            get { return aDateEditName; }
        }

        public static void RegisteraDateEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(aDateEditName, typeof(aDateYear),typeof(RepositoryItemaDateYear), typeof(DateEditViewInfo), new ButtonEditPainter(), true, picBack));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemaDateYear source = item as RepositoryItemaDateYear;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }

        public override void CreateDefaultButton()
        {
            base.CreateDefaultButton();
            Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            Buttons[0].Image = picBack;// global::Bifrost.Win.Controls.Properties.Resources.btn_calendar; //Images.btn_calendar;

        }

        EditorButtonCollection tmp
        {
            get { return Buttons; }
            set { CreateDefaultButton(); }
        }
        public override void BeginInit()
        {
            if (IsFirstInit)
            {
                tmp = new EditorButtonCollection();
                tmp.Assign(Buttons);
            }
            base.BeginInit();
        }

        public override void EndInit()
        {
            if (tmp != null)
            {
                Buttons.Assign(tmp);
                tmp = null;
            }
            base.EndInit();
        }


    }
}
