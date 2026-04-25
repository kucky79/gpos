using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Drawing;

namespace Bifrost.Win.Controls
{
    [ToolboxItem(true)]
    public class aLookUpEdit : LookUpEdit
    {
        private const int FIXED_HEIGHT = 24;
        private static Image picBack = Images.lookup_default;
        //private static Image picBack_disable = Images.lookup_disable;
        private static Image picBack_disable = Images.lookup_default;



        static aLookUpEdit()
        {
            RepositoryItemaLookUpEdit.RegisterCustomEdit();
        }
        public aLookUpEdit() : base()
        {
            RepositoryItemaLookUpEdit.RegisterCustomEdit();
        }
        //private void CreateExtraButtons()
        //{
        //    EditorButton b = new EditorButton()
        //    {
        //        Kind = ButtonPredefines.Close,
        //        IsDefaultButton = true
        //    };
        //    Properties.Buttons.Add(b);
        //}
        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemaLookUpEdit.CustomEditName;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemaLookUpEdit Properties
        {
            get
            {
                return base.Properties as RepositoryItemaLookUpEdit;
            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            this.Properties.NullText = string.Empty;
            //if (this.Properties.Buttons.Count > 0)
            //{
            //    this.Properties.Buttons[0].Kind = ButtonPredefines.Glyph;
            //    this.Properties.Buttons[0].Image = picBack;
            //}
        }

        public override object EditValue
        {
            get
            {
                return base.EditValue;
            }

            set
            {
                base.EditValue = value;
                this.IsModified = true;
            }
        }
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            //this.Properties.AutoHeight = false;

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //if (this.Properties.Buttons.Count > 0)
            //{
            //    if (this.Properties.Buttons[0].Kind == ButtonPredefines.Combo)
            //    {
            //        this.Properties.Buttons[0].Kind = ButtonPredefines.Glyph;
            //        this.Properties.Buttons[0].Image = picBack;
            //    }
            //}

            //if (this.Properties.ReadOnly == true)
            //{
            //    this.Properties.Appearance.BackColor = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            //    this.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            //    this.Properties.Appearance.Options.UseBackColor = true;
            //    this.Properties.Appearance.Options.UseBorderColor = true;
            //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            //    if (this.Properties.Buttons.Count > 0)
            //    {
            //        this.Properties.Buttons[0].Image = picBack_disable;

            //        this.Properties.Buttons[0].Appearance.BackColor = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            //        this.Properties.Buttons[0].Appearance.BackColor2 = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            //        this.Properties.Buttons[0].Appearance.Options.UseBackColor = true;
            //    }

            //}
            //else
            //{
            //    this.Properties.Appearance.BackColor = System.Drawing.Color.White;
            //    this.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            //    this.Properties.Appearance.Options.UseBackColor = true;
            //    this.Properties.Appearance.Options.UseBorderColor = true;
            //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            //    if (this.Properties.Buttons.Count > 0)
            //    {
            //        this.Properties.Buttons[0].Image = picBack;

            //        this.Properties.Buttons[0].Appearance.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            //        this.Properties.Buttons[0].Appearance.BackColor2 = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            //        this.Properties.Buttons[0].Appearance.Options.UseBackColor = true;
            //    }
            //}
            //if (this.Size.Height != FIXED_HEIGHT)
            //    this.Size = new Size(this.Size.Width, FIXED_HEIGHT);
        }
    }
}
