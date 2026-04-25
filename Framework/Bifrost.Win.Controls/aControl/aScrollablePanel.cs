using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Bifrost.Adv.Controls;

namespace Bifrost.Win.Controls
{
    public class aScrollablePanel : XtraScrollableControl
    {
        private aScrollablePanelHelper _scrollHelper;

        public aScrollablePanel() : base()
        {
            _scrollHelper = new aScrollablePanelHelper(this);
            _scrollHelper.EnableScrollOnMouseWheel();

            //_control1 = new XtraUserControl1();
            //foreach (var control in _control1.MyControls) { mainScroller.Controls.Add(control); }
            _scrollHelper.SubscribeToMouseWheel(this.Controls);

        }

        bool _ReadOnly = false;
        //private Adv.Controls.aControlHelper.ControlEnum.ReadOnly _TmpReadOnly = Adv.Controls.aControlHelper.ControlEnum.ReadOnly.None;

        [Browsable(true), Description("ReadOnly를 설정합니다."), DefaultValue(false)]
        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;
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
                        //        ((aCodeNText)Ctrls).ReadOnly = Adv.Controls.aControlHelper.ControlEnum.ReadOnly.None;

                        //        //((aCodeNText)Ctrls).ReadOnly = _TmpReadOnly;
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
                        //        ((aCodeText)Ctrls).ReadOnly = Adv.Controls.aControlHelper.ControlEnum.ReadOnly.None;

                        //        //((aCodeText)Ctrls).ReadOnly = _TmpReadOnly;
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
                        //        ((aCodeTextNonPopup)Ctrls).ReadOnly = Adv.Controls.aControlHelper.ControlEnum.ReadOnly.None;

                        //        //((aCodeTextNonPopup)Ctrls).ReadOnly = _TmpReadOnly;
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
    }
}
