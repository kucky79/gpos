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
    [UserRepositoryItem("RegisteraDataEdit")]
    public class RepositoryItemaDateEdit : RepositoryItemDateEdit
    {
        private static Image picBack = Images.btn_calendar;
        private static Image picBack_disable = Images.btn_calendar_disable;

        private static Image picClear = Images.txtClear;

        static RepositoryItemaDateEdit()
        {
            RegisteraDataEdit();
        }

        public const string CustomEditName = "aDateEdit";

        public RepositoryItemaDateEdit()
        {
        }

        public override string EditorTypeName
        {
            get
            {
                return CustomEditName;
            }
        }

        public static void RegisteraDataEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(aDateEdit), typeof(RepositoryItemaDateEdit), typeof(aDateEditViewInfo), new aDateEditPainter(), true, picBack));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemaDateEdit source = item as RepositoryItemaDateEdit;
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

            //기존 버튼 이미지 교체
            Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            Buttons[0].Image = picBack;

            //신규 버튼 추가
            EditorButton btnClear = new EditorButton() { Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, Image = picClear, IsDefaultButton = true };

            this.Buttons.AddRange(new EditorButton[] { btnClear });
            this.ActionButtonIndex = 0;

            //Text Clear버튼은 Default 안보이게
            if (this.Buttons.Count > 1)
            {
                this.Buttons[1].Visible = false;
            }

            
        }

        EditorButtonCollection _tmpBtnCollection
        {
            get { return Buttons; }
            set { CreateDefaultButton(); }
        }
        public override void BeginInit()
        {
            if (IsFirstInit)
            {
                _tmpBtnCollection = new EditorButtonCollection();
                _tmpBtnCollection.Assign(Buttons);
            }
            base.BeginInit();
        }

        public override void EndInit()
        {
            if (_tmpBtnCollection != null)
            {
                Buttons.Assign(_tmpBtnCollection);
                _tmpBtnCollection = null;
            }
            base.EndInit();
        }
    }
}
