using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using System.Drawing;

namespace Bifrost.Win.Controls
{
    [UserRepositoryItem("RegisteraTextEdit")]
    public class RepositoryItemaButtonEdit : RepositoryItemButtonEdit
    {

        private static Image picBack = Images.txtClear;
        static RepositoryItemaButtonEdit()
        {
            RegisteraTextEdit();
        }

        public RepositoryItemaButtonEdit() : base() { }

        internal const string aTextEditName = "aTextEdit";

        public override string EditorTypeName
        {
            get { return aTextEditName; }
        }

        public static void RegisteraTextEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(aTextEditName, typeof(aTextEdit),typeof(RepositoryItemaButtonEdit), typeof(DateEditViewInfo), new ButtonEditPainter(), true));
        }
        public override void CreateDefaultButton()
        {
            base.CreateDefaultButton();
            Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            Buttons[0].Image = picBack;// global::Bifrost.Win.Controls.Properties.Resources.btn_calendar; //Images.btn_calendar;

            Buttons[0].Visible = false;

        }

    }
}
