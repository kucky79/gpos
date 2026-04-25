using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System.Drawing;
using Bifrost.Win.Controls;

namespace Bifrost.Win.Controls
{
    [UserRepositoryItem("RegisteraLookUpEdit")]
    public class RepositoryItemaLookUpEdit : RepositoryItemLookUpEdit
    {

        private static Image picBack = Images.lookup_default;

        //public override void BeginInit()
        //{
        //    base.BeginInit();
        //}
       
        static RepositoryItemaLookUpEdit()
        {
            //RegisterCustomEdit();
        }
        public RepositoryItemaLookUpEdit() : base()
        {
        }
        public const string CustomEditName = "aLookUpEdit";
        public override string EditorTypeName
        {
            get
            {
                return CustomEditName;
            }
        }
        public static void RegisterCustomEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(
               new EditorClassInfo(CustomEditName,
                   typeof(aLookUpEdit),
                   typeof(RepositoryItemaLookUpEdit),
                   typeof(LookUpEditViewInfo),
                   new ButtonEditPainter(), true, null));
        }

        //public override void CreateDefaultButton()
        //{
        //    base.CreateDefaultButton();
        //    //Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
        //    //Buttons[0].Image = picBack;
        //}

    }
}
