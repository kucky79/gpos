using DevExpress.XtraEditors;
using Bifrost.Grid;
//using Bifrost.Adv.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System;
using System.Drawing;
using DevExpress.Utils.Drawing;
using DevExpress.LookAndFeel;

namespace Bifrost.Win.Controls
{
    public class aPanel : PanelControl
    {

        bool _readOnly = false;
        Dictionary<string, Control> _controls = null;
        Color _backColor = System.Drawing.ColorTranslator.FromHtml("#EFF3FA");
        protected Color _ItemBackColor = Color.White;


        public aPanel() : base()
        {
            this.Dock = System.Windows.Forms.DockStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "panelControl1";
            this.Size = new System.Drawing.Size(150, 150);
            this.TabIndex = 0;

            _SetPanelType = PanelType.NONE;
        }


        //private Adv.Controls.aControlHelper.ControlEnum.ReadOnly _TmpReadOnly = Adv.Controls.aControlHelper.ControlEnum.ReadOnly.None;

        [Browsable(true), Description("ReadOnly를 설정합니다."), DefaultValue(false)]
        public bool ReadOnly
        {
            get { return this._readOnly; }
            set
            {
                this._readOnly = value;
                foreach (Control Ctrls in this.Controls)
                {
                    switch (Ctrls.GetType().Name)
                    {
                        //case "aCodeNText":
                        //    //Readonly 일경우
                        //    if (value)
                        //    {
                        //        _TmpReadOnly = ((aCodeNText)Ctrls).ReadOnly;
                        //        ((aCodeNText)Ctrls).ReadOnly = Adv.Controls.aControlHelper.ControlEnum.ReadOnly.TotalReadOnly;
                        //    }
                        //    else
                        //    {
                        //        ((aCodeNText)Ctrls).ReadOnly = _TmpReadOnly;
                        //    }
                        //    break;
                        //case "aCodeText":
                        //    //Readonly 일경우
                        //    if (value)
                        //    {
                        //        _TmpReadOnly = ((aCodeText)Ctrls).ReadOnly;
                        //        ((aCodeText)Ctrls).ReadOnly = Adv.Controls.aControlHelper.ControlEnum.ReadOnly.TotalReadOnly;
                        //    }
                        //    else
                        //    {
                        //        ((aCodeText)Ctrls).ReadOnly = _TmpReadOnly;
                        //    }
                        //    break;
                        //case "aCodeTextNonPopup":
                        //    //Readonly 일경우
                        //    if (value)
                        //    {
                        //        _TmpReadOnly = ((aCodeTextNonPopup)Ctrls).ReadOnly;
                        //        ((aCodeTextNonPopup)Ctrls).ReadOnly = Adv.Controls.aControlHelper.ControlEnum.ReadOnly.TotalReadOnly;
                        //    }
                        //    else
                        //    {
                        //        ((aCodeTextNonPopup)Ctrls).ReadOnly = _TmpReadOnly;
                        //    }
                        //    break;
                        //case "aCheckBox":
                        //    ((aCheckBox)Ctrls).Properties.ReadOnly = value;
                        //    break;
                        //case "aDateEdit":
                        //    ((aDateEdit)Ctrls).Properties.ReadOnly = value;
                        //    break;
                        //case "aRadioButton":
                        //    ((aRadioButton)Ctrls).Properties.ReadOnly = value;
                        //    break;
                        //case "aLookUpEdit":
                        //    ((aLookUpEdit)Ctrls).Properties.ReadOnly = value;
                        //    break;
                        //case "aNumericText":
                        //    ((aNumericText)Ctrls).Properties.ReadOnly = value;
                        //    break;
                        //case "aMemoEdit":
                        //    ((aMemoEdit)Ctrls).Properties.ReadOnly = value;
                        //    break;
                        //case "aTextEdit":
                        //    ((aTextEdit)Ctrls).Properties.ReadOnly = value;
                        //    break;
                        //case "aMaskEdit":
                        //    ((aMaskEdit)Ctrls).Properties.ReadOnly = value;
                        //    break;
                        //case "aPeriodEdit":
                        //    ((aPeriodEdit)Ctrls).ReadOnly = value;
                        //    break;
                        //case "aDateMonth":
                        //    ((aDateMonth)Ctrls).ReadOnly = value;
                        //    break;
                        //case "aGrid":
                        //    break;
                        //case "aButton":
                        //    break;

                        default:
                            Ctrls.GetType().GetProperty("ReadOnly").SetValue(Ctrls, value, null);
                            break;
                    }
                }
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //switch (this.SetPanelType)
            //{
            //    //탭 화면 배경색
            //    case PanelType.NONE:
            //        this.Appearance.BackColor = Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));// System.Drawing.ColorTranslator.FromHtml("#D3E1F0");
            //        this.Appearance.BackColor2 = Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250))))); //System.Drawing.ColorTranslator.FromHtml("#D3E1F0");
            //        break;
            //    //컨트롤 화면 배경색
            //    case PanelType.MAINFORM:
            //        this.Appearance.BackColor = Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(253))))); //System.Drawing.ColorTranslator.FromHtml("#EFF3FA");
            //        this.Appearance.BackColor2 = Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(253))))); //System.Drawing.ColorTranslator.FromHtml("#EFF3FA");
            //        break;
            //    //기타
            //    case PanelType.ETC:
            //        break;
            //}

          
            //this.LookAndFeel.UseDefaultLookAndFeel = false;
            //this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
        }

        PanelType _SetPanelType = aPanel.PanelType.NONE;
        public PanelType SetPanelType
        {
            get { return _SetPanelType; }
            set { _SetPanelType = value; }
        }

        //protected override void InitLayout()
        //{
        //    base.InitLayout();

        //    switch (this.SetPanelType)
        //    {
        //        //탭 화면 배경색
        //        case PanelType.NONE:
        //            this.Appearance.BackColor = Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(253))))); //System.Drawing.ColorTranslator.FromHtml("#EFF3FA");
        //            this.Appearance.BackColor2 = Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(253))))); //System.Drawing.ColorTranslator.FromHtml("#EFF3FA");
        //            break;
        //        //컨트롤 화면 배경색
        //        case PanelType.MAINFORM:
        //            this.Appearance.BackColor = Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));// System.Drawing.ColorTranslator.FromHtml("#D3E1F0");
        //            this.Appearance.BackColor2 = Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250))))); //System.Drawing.ColorTranslator.FromHtml("#D3E1F0");
        //            break;
        //    }

        //    //this.Appearance.bor = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        //    this.Appearance.Options.UseBackColor = true;
        //}


        ////필요없지만, 기존거땜문에 넣음 작동은 안함
        //DevExpress.XtraEditors.Controls.BorderStyles _BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        //[Browsable(true), Description("Set Borderstyle")]

        //public DevExpress.XtraEditors.Controls.BorderStyles BorderStyle
        //{
        //    set { _BorderStyle = value;  }
        //}

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //this.Appearance.Options.UseBackColor = true;

        }

        public enum PanelType
        {
            NONE = 0,
            MAINFORM = 1,
            ETC = 99
        }

    }

}