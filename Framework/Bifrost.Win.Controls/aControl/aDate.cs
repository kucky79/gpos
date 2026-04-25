using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using System.Windows.Forms;

namespace NF.Framework.Win.Controls
{
    [UserRepositoryItem("RegisteraDate")]
    public class RepositoryItemCustomEdit1 : RepositoryItemDateEdit
    {
        static RepositoryItemCustomEdit1()
        {
            RegisterCustomEdit1();
        }

        public const string CustomEditName = "aDate";

        public RepositoryItemCustomEdit1()
        {
        }

        public override string EditorTypeName
        {
            get { return CustomEditName; }
        }

        public static void RegisterCustomEdit1()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(aDate), typeof(RepositoryItemCustomEdit1), typeof(CustomEdit1ViewInfo), new CustomEdit1Painter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemCustomEdit1 source = item as RepositoryItemCustomEdit1;
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
            Buttons[0].Kind = ButtonPredefines.Glyph;
        }
    }

    [ToolboxItem(true)]
    public class aDate : DateEdit
    {
        private const int FIXED_HEIGHT = 24;

        static aDate()
        {
            RepositoryItemCustomEdit1.RegisterCustomEdit1();
        }

        public aDate()
        {

        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomEdit1 Properties
        {
            get { return base.Properties as RepositoryItemCustomEdit1; }
        }

        public override string EditorTypeName
        {
            get { return RepositoryItemCustomEdit1.CustomEditName; }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new CustomEdit1PopupForm(this);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            base.Properties.AutoHeight = false;
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);

            base.Properties.Appearance.BackColor = System.Drawing.Color.White;
            base.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(225)))));
            base.Properties.Appearance.Options.UseBackColor = true;
            base.Properties.Appearance.Options.UseBorderColor = true;
            base.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            if (base.Size.Height != FIXED_HEIGHT)
                base.Size = new Size(base.Size.Width, FIXED_HEIGHT);
        }
    }

    public class CustomEdit1ViewInfo : DateEditViewInfo
    {
        public CustomEdit1ViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class CustomEdit1Painter : ButtonEditPainter
    {
        public CustomEdit1Painter()
        {
        }

        protected override void DrawButton(ButtonEditViewInfo viewInfo, EditorButtonObjectInfoArgs info)
        {
            base.DrawButton(viewInfo, info);
            if (info.Button.Kind == ButtonPredefines.Glyph)
            {
                info.Graphics.DrawImage(Images.btn_Del_default, info.Bounds);
            }
        }

    }

    public class CustomEdit1PopupForm : VistaPopupDateEditForm
    {
        public CustomEdit1PopupForm(aDate ownerEdit) : base(ownerEdit)
        {
        }
    }
}
