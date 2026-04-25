using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public partial class M_POS_SALE
    {
        private DevExpress.XtraEditors.PanelControl panelKeypadItem;
        private DevExpress.XtraEditors.TextEdit txtPadQty;
        private DevExpress.XtraEditors.TextEdit txtPadItem;
        private DevExpress.XtraEditors.SimpleButton btnPadPrice;
        private DevExpress.XtraEditors.SimpleButton btnPadCancel;
        private DevExpress.XtraEditors.SimpleButton btnPadDelItem;
        private DevExpress.XtraEditors.SimpleButton btnPadInit;
        private DevExpress.XtraEditors.SimpleButton btnPadMinus;
        private DevExpress.XtraEditors.TextEdit txtItemDescrip;
        private DevExpress.XtraEditors.SimpleButton btnPadEA;
        private DevExpress.XtraEditors.SimpleButton btnPadConfirm;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit4;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit3;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit2;
        private DevExpress.XtraEditors.SimpleButton btnPadItemUnit1;
        private DevExpress.XtraEditors.SimpleButton btnPadPoint;
        private DevExpress.XtraEditors.SimpleButton btnPadBackSpace;
        private DevExpress.XtraEditors.SimpleButton btnPadClear;
        private DevExpress.XtraEditors.SimpleButton btnPad000;
        private DevExpress.XtraEditors.SimpleButton btnPad00;
        private DevExpress.XtraEditors.SimpleButton btnPad0;
        private DevExpress.XtraEditors.SimpleButton btnPad3;
        private DevExpress.XtraEditors.SimpleButton btnPad2;
        private DevExpress.XtraEditors.SimpleButton btnPad1;
        private DevExpress.XtraEditors.SimpleButton btnPad6;
        private DevExpress.XtraEditors.SimpleButton btnPad5;
        private DevExpress.XtraEditors.SimpleButton btnPad4;
        private DevExpress.XtraEditors.SimpleButton btnPad9;
        private DevExpress.XtraEditors.SimpleButton btnPad8;
        private DevExpress.XtraEditors.SimpleButton btnPad7;

        private void InitializeKeypadScreen()
        {
            panelKeypadItem = new DevExpress.XtraEditors.PanelControl();
            txtPadQty = new DevExpress.XtraEditors.TextEdit();
            txtPadItem = new DevExpress.XtraEditors.TextEdit();
            btnPadPrice = new DevExpress.XtraEditors.SimpleButton();
            btnPadCancel = new DevExpress.XtraEditors.SimpleButton();
            btnPadDelItem = new DevExpress.XtraEditors.SimpleButton();
            btnPadInit = new DevExpress.XtraEditors.SimpleButton();
            btnPadMinus = new DevExpress.XtraEditors.SimpleButton();
            txtItemDescrip = new DevExpress.XtraEditors.TextEdit();
            btnPadEA = new DevExpress.XtraEditors.SimpleButton();
            btnPadConfirm = new DevExpress.XtraEditors.SimpleButton();
            btnPadItemUnit4 = new DevExpress.XtraEditors.SimpleButton();
            btnPadItemUnit3 = new DevExpress.XtraEditors.SimpleButton();
            btnPadItemUnit2 = new DevExpress.XtraEditors.SimpleButton();
            btnPadItemUnit1 = new DevExpress.XtraEditors.SimpleButton();
            btnPadPoint = new DevExpress.XtraEditors.SimpleButton();
            btnPadBackSpace = new DevExpress.XtraEditors.SimpleButton();
            btnPadClear = new DevExpress.XtraEditors.SimpleButton();
            btnPad000 = new DevExpress.XtraEditors.SimpleButton();
            btnPad00 = new DevExpress.XtraEditors.SimpleButton();
            btnPad0 = new DevExpress.XtraEditors.SimpleButton();
            btnPad3 = new DevExpress.XtraEditors.SimpleButton();
            btnPad2 = new DevExpress.XtraEditors.SimpleButton();
            btnPad1 = new DevExpress.XtraEditors.SimpleButton();
            btnPad6 = new DevExpress.XtraEditors.SimpleButton();
            btnPad5 = new DevExpress.XtraEditors.SimpleButton();
            btnPad4 = new DevExpress.XtraEditors.SimpleButton();
            btnPad9 = new DevExpress.XtraEditors.SimpleButton();
            btnPad8 = new DevExpress.XtraEditors.SimpleButton();
            btnPad7 = new DevExpress.XtraEditors.SimpleButton();

            ((System.ComponentModel.ISupportInitialize)(this.panelKeypadItem)).BeginInit();
            this.panelKeypadItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPadQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPadItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemDescrip.Properties)).BeginInit();
            this.SuspendLayout();

            // 
            // panelKeypadItem
            // 

            this.panelKeypadItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKeypadItem.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelKeypadItem.Appearance.Options.UseBackColor = true;
            this.panelKeypadItem.Controls.Add(this.txtPadQty);
            this.panelKeypadItem.Controls.Add(this.txtPadItem);
            this.panelKeypadItem.Controls.Add(this.btnPadPrice);
            this.panelKeypadItem.Controls.Add(this.btnPadCancel);
            this.panelKeypadItem.Controls.Add(this.btnPadDelItem);
            this.panelKeypadItem.Controls.Add(this.btnPadInit);
            this.panelKeypadItem.Controls.Add(this.btnPadMinus);
            this.panelKeypadItem.Controls.Add(this.txtItemDescrip);
            this.panelKeypadItem.Controls.Add(this.btnPadEA);
            this.panelKeypadItem.Controls.Add(this.btnPadConfirm);
            this.panelKeypadItem.Controls.Add(this.btnPadItemUnit4);
            this.panelKeypadItem.Controls.Add(this.btnPadItemUnit3);
            this.panelKeypadItem.Controls.Add(this.btnPadItemUnit2);
            this.panelKeypadItem.Controls.Add(this.btnPadItemUnit1);
            this.panelKeypadItem.Controls.Add(this.btnPadPoint);
            this.panelKeypadItem.Controls.Add(this.btnPadBackSpace);
            this.panelKeypadItem.Controls.Add(this.btnPadClear);
            this.panelKeypadItem.Controls.Add(this.btnPad000);
            this.panelKeypadItem.Controls.Add(this.btnPad00);
            this.panelKeypadItem.Controls.Add(this.btnPad0);
            this.panelKeypadItem.Controls.Add(this.btnPad3);
            this.panelKeypadItem.Controls.Add(this.btnPad2);
            this.panelKeypadItem.Controls.Add(this.btnPad1);
            this.panelKeypadItem.Controls.Add(this.btnPad6);
            this.panelKeypadItem.Controls.Add(this.btnPad5);
            this.panelKeypadItem.Controls.Add(this.btnPad4);
            this.panelKeypadItem.Controls.Add(this.btnPad9);
            this.panelKeypadItem.Controls.Add(this.btnPad8);
            this.panelKeypadItem.Controls.Add(this.btnPad7);
            this.panelKeypadItem.Location = new System.Drawing.Point(0, 0);
            this.panelKeypadItem.Name = "panelKeypadItem";
            this.panelKeypadItem.Size = navPayContents.Size;// new System.Drawing.Size(629, 939);
            this.panelKeypadItem.TabIndex = 81;
            this.panelKeypadItem.Visible = false;
            

            // 
            // txtPadQty
            // 
            this.txtPadQty.EditValue = "0";
            this.txtPadQty.Enabled = false;
            this.txtPadQty.Location = new System.Drawing.Point(81, 170);
            this.txtPadQty.Name = "txtPadQty";
            this.txtPadQty.Properties.AllowFocused = false;
            this.txtPadQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.txtPadQty.Properties.Appearance.Options.UseFont = true;
            this.txtPadQty.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPadQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPadQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPadQty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPadQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPadQty.Properties.NullValuePrompt = "0";
            this.txtPadQty.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPadQty.Properties.ReadOnly = true;
            this.txtPadQty.Size = new System.Drawing.Size(282, 30);
            this.txtPadQty.TabIndex = 88;
            // 
            // txtPadItem
            // 
            this.txtPadItem.Location = new System.Drawing.Point(81, 73);
            this.txtPadItem.Name = "txtPadItem";
            this.txtPadItem.Properties.AllowFocused = false;
            this.txtPadItem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.txtPadItem.Properties.Appearance.Options.UseFont = true;
            this.txtPadItem.Properties.ReadOnly = true;
            this.txtPadItem.Size = new System.Drawing.Size(378, 40);
            this.txtPadItem.TabIndex = 82;
            // 
            // btnPadPrice
            // 
            this.btnPadPrice.AllowFocus = false;
            this.btnPadPrice.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadPrice.Appearance.Options.UseFont = true;
            this.btnPadPrice.Location = new System.Drawing.Point(177, 605);
            this.btnPadPrice.Name = "btnPadPrice";
            this.btnPadPrice.Size = new System.Drawing.Size(90, 90);
            this.btnPadPrice.TabIndex = 110;
            this.btnPadPrice.Text = "원";
            // 
            // btnPadCancel
            // 
            this.btnPadCancel.AllowFocus = false;
            this.btnPadCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadCancel.Appearance.Options.UseFont = true;
            this.btnPadCancel.Location = new System.Drawing.Point(465, 605);
            this.btnPadCancel.Name = "btnPadCancel";
            this.btnPadCancel.Size = new System.Drawing.Size(90, 90);
            this.btnPadCancel.TabIndex = 112;
            this.btnPadCancel.Text = "취소";
            // 
            // btnPadDelItem
            // 
            this.btnPadDelItem.AllowFocus = false;
            this.btnPadDelItem.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadDelItem.Appearance.Options.UseFont = true;
            this.btnPadDelItem.Location = new System.Drawing.Point(465, 61);
            this.btnPadDelItem.Name = "btnPadDelItem";
            this.btnPadDelItem.Size = new System.Drawing.Size(90, 59);
            this.btnPadDelItem.TabIndex = 81;
            this.btnPadDelItem.Text = "삭제";
            // 
            // btnPadInit
            // 
            this.btnPadInit.AllowFocus = false;
            this.btnPadInit.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadInit.Appearance.Options.UseFont = true;
            this.btnPadInit.Location = new System.Drawing.Point(369, 126);
            this.btnPadInit.Name = "btnPadInit";
            this.btnPadInit.Size = new System.Drawing.Size(90, 90);
            this.btnPadInit.TabIndex = 84;
            this.btnPadInit.Text = "초기화";
            // 
            // btnPadMinus
            // 
            this.btnPadMinus.AllowFocus = false;
            this.btnPadMinus.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadMinus.Appearance.Options.UseFont = true;
            this.btnPadMinus.Location = new System.Drawing.Point(465, 126);
            this.btnPadMinus.Name = "btnPadMinus";
            this.btnPadMinus.Size = new System.Drawing.Size(90, 90);
            this.btnPadMinus.TabIndex = 85;
            this.btnPadMinus.Text = "─";
            // 
            // txtItemDescrip
            // 
            this.txtItemDescrip.Location = new System.Drawing.Point(81, 134);
            this.txtItemDescrip.Name = "txtItemDescrip";
            this.txtItemDescrip.Properties.AllowFocused = false;
            this.txtItemDescrip.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.txtItemDescrip.Properties.Appearance.Options.UseFont = true;
            this.txtItemDescrip.Properties.ReadOnly = true;
            this.txtItemDescrip.Size = new System.Drawing.Size(282, 30);
            this.txtItemDescrip.TabIndex = 86;
            // 
            // btnPadEA
            // 
            this.btnPadEA.AllowFocus = false;
            this.btnPadEA.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadEA.Appearance.Options.UseFont = true;
            this.btnPadEA.Location = new System.Drawing.Point(81, 605);
            this.btnPadEA.Name = "btnPadEA";
            this.btnPadEA.Size = new System.Drawing.Size(90, 90);
            this.btnPadEA.TabIndex = 109;
            this.btnPadEA.Text = "묶음";
            // 
            // btnPadConfirm
            // 
            this.btnPadConfirm.AllowFocus = false;
            this.btnPadConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadConfirm.Appearance.Options.UseFont = true;
            this.btnPadConfirm.Location = new System.Drawing.Point(273, 605);
            this.btnPadConfirm.Name = "btnPadConfirm";
            this.btnPadConfirm.Size = new System.Drawing.Size(186, 90);
            this.btnPadConfirm.TabIndex = 111;
            this.btnPadConfirm.Text = "확인";
            // 
            // btnPadItemUnit4
            // 
            this.btnPadItemUnit4.AllowFocus = false;
            this.btnPadItemUnit4.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadItemUnit4.Appearance.Options.UseFont = true;
            this.btnPadItemUnit4.Enabled = false;
            this.btnPadItemUnit4.Location = new System.Drawing.Point(465, 509);
            this.btnPadItemUnit4.Name = "btnPadItemUnit4";
            this.btnPadItemUnit4.Size = new System.Drawing.Size(90, 90);
            this.btnPadItemUnit4.TabIndex = 108;
            // 
            // btnPadItemUnit3
            // 
            this.btnPadItemUnit3.AllowFocus = false;
            this.btnPadItemUnit3.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadItemUnit3.Appearance.Options.UseFont = true;
            this.btnPadItemUnit3.Enabled = false;
            this.btnPadItemUnit3.Location = new System.Drawing.Point(465, 413);
            this.btnPadItemUnit3.Name = "btnPadItemUnit3";
            this.btnPadItemUnit3.Size = new System.Drawing.Size(90, 90);
            this.btnPadItemUnit3.TabIndex = 103;
            // 
            // btnPadItemUnit2
            // 
            this.btnPadItemUnit2.AllowFocus = false;
            this.btnPadItemUnit2.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadItemUnit2.Appearance.Options.UseFont = true;
            this.btnPadItemUnit2.Enabled = false;
            this.btnPadItemUnit2.Location = new System.Drawing.Point(465, 318);
            this.btnPadItemUnit2.Name = "btnPadItemUnit2";
            this.btnPadItemUnit2.Size = new System.Drawing.Size(90, 90);
            this.btnPadItemUnit2.TabIndex = 98;
            // 
            // btnPadItemUnit1
            // 
            this.btnPadItemUnit1.AllowFocus = false;
            this.btnPadItemUnit1.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadItemUnit1.Appearance.Options.UseFont = true;
            this.btnPadItemUnit1.Enabled = false;
            this.btnPadItemUnit1.Location = new System.Drawing.Point(465, 222);
            this.btnPadItemUnit1.Name = "btnPadItemUnit1";
            this.btnPadItemUnit1.Size = new System.Drawing.Size(90, 90);
            this.btnPadItemUnit1.TabIndex = 94;
            // 
            // btnPadPoint
            // 
            this.btnPadPoint.AllowFocus = false;
            this.btnPadPoint.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadPoint.Appearance.Options.UseFont = true;
            this.btnPadPoint.Location = new System.Drawing.Point(369, 509);
            this.btnPadPoint.Name = "btnPadPoint";
            this.btnPadPoint.Size = new System.Drawing.Size(90, 90);
            this.btnPadPoint.TabIndex = 107;
            this.btnPadPoint.Text = ".";
            // 
            // btnPadBackSpace
            // 
            this.btnPadBackSpace.AllowFocus = false;
            this.btnPadBackSpace.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadBackSpace.Appearance.Options.UseFont = true;
            this.btnPadBackSpace.Location = new System.Drawing.Point(369, 413);
            this.btnPadBackSpace.Name = "btnPadBackSpace";
            this.btnPadBackSpace.Size = new System.Drawing.Size(90, 90);
            this.btnPadBackSpace.TabIndex = 102;
            this.btnPadBackSpace.Text = "←";
            // 
            // btnPadClear
            // 
            this.btnPadClear.AllowFocus = false;
            this.btnPadClear.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPadClear.Appearance.Options.UseFont = true;
            this.btnPadClear.Location = new System.Drawing.Point(369, 222);
            this.btnPadClear.Name = "btnPadClear";
            this.btnPadClear.Size = new System.Drawing.Size(90, 185);
            this.btnPadClear.TabIndex = 93;
            this.btnPadClear.Text = "CLS";
            // 
            // btnPad000
            // 
            this.btnPad000.AllowFocus = false;
            this.btnPad000.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad000.Appearance.Options.UseFont = true;
            this.btnPad000.Location = new System.Drawing.Point(273, 509);
            this.btnPad000.Name = "btnPad000";
            this.btnPad000.Size = new System.Drawing.Size(90, 90);
            this.btnPad000.TabIndex = 106;
            this.btnPad000.Text = "000";
            // 
            // btnPad00
            // 
            this.btnPad00.AllowFocus = false;
            this.btnPad00.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad00.Appearance.Options.UseFont = true;
            this.btnPad00.Location = new System.Drawing.Point(177, 509);
            this.btnPad00.Name = "btnPad00";
            this.btnPad00.Size = new System.Drawing.Size(90, 90);
            this.btnPad00.TabIndex = 105;
            this.btnPad00.Text = "00";
            // 
            // btnPad0
            // 
            this.btnPad0.AllowFocus = false;
            this.btnPad0.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad0.Appearance.Options.UseFont = true;
            this.btnPad0.Location = new System.Drawing.Point(81, 509);
            this.btnPad0.Name = "btnPad0";
            this.btnPad0.Size = new System.Drawing.Size(90, 90);
            this.btnPad0.TabIndex = 104;
            this.btnPad0.Text = "0";
            // 
            // btnPad3
            // 
            this.btnPad3.AllowFocus = false;
            this.btnPad3.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad3.Appearance.Options.UseFont = true;
            this.btnPad3.Location = new System.Drawing.Point(273, 413);
            this.btnPad3.Name = "btnPad3";
            this.btnPad3.Size = new System.Drawing.Size(90, 90);
            this.btnPad3.TabIndex = 101;
            this.btnPad3.Text = "3";
            // 
            // btnPad2
            // 
            this.btnPad2.AllowFocus = false;
            this.btnPad2.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad2.Appearance.Options.UseFont = true;
            this.btnPad2.Location = new System.Drawing.Point(177, 413);
            this.btnPad2.Name = "btnPad2";
            this.btnPad2.Size = new System.Drawing.Size(90, 90);
            this.btnPad2.TabIndex = 100;
            this.btnPad2.Text = "2";
            // 
            // btnPad1
            // 
            this.btnPad1.AllowFocus = false;
            this.btnPad1.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad1.Appearance.Options.UseFont = true;
            this.btnPad1.Location = new System.Drawing.Point(81, 413);
            this.btnPad1.Name = "btnPad1";
            this.btnPad1.Size = new System.Drawing.Size(90, 90);
            this.btnPad1.TabIndex = 99;
            this.btnPad1.Text = "1";
            // 
            // btnPad6
            // 
            this.btnPad6.AllowFocus = false;
            this.btnPad6.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad6.Appearance.Options.UseFont = true;
            this.btnPad6.Location = new System.Drawing.Point(273, 318);
            this.btnPad6.Name = "btnPad6";
            this.btnPad6.Size = new System.Drawing.Size(90, 90);
            this.btnPad6.TabIndex = 97;
            this.btnPad6.Text = "6";
            // 
            // btnPad5
            // 
            this.btnPad5.AllowFocus = false;
            this.btnPad5.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad5.Appearance.Options.UseFont = true;
            this.btnPad5.Location = new System.Drawing.Point(177, 318);
            this.btnPad5.Name = "btnPad5";
            this.btnPad5.Size = new System.Drawing.Size(90, 90);
            this.btnPad5.TabIndex = 96;
            this.btnPad5.Text = "5";
            // 
            // btnPad4
            // 
            this.btnPad4.AllowFocus = false;
            this.btnPad4.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad4.Appearance.Options.UseFont = true;
            this.btnPad4.Location = new System.Drawing.Point(81, 318);
            this.btnPad4.Name = "btnPad4";
            this.btnPad4.Size = new System.Drawing.Size(90, 90);
            this.btnPad4.TabIndex = 95;
            this.btnPad4.Text = "4";
            // 
            // btnPad9
            // 
            this.btnPad9.AllowFocus = false;
            this.btnPad9.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad9.Appearance.Options.UseFont = true;
            this.btnPad9.Location = new System.Drawing.Point(273, 222);
            this.btnPad9.Name = "btnPad9";
            this.btnPad9.Size = new System.Drawing.Size(90, 90);
            this.btnPad9.TabIndex = 92;
            this.btnPad9.Text = "9";
            // 
            // btnPad8
            // 
            this.btnPad8.AllowFocus = false;
            this.btnPad8.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad8.Appearance.Options.UseFont = true;
            this.btnPad8.Location = new System.Drawing.Point(177, 222);
            this.btnPad8.Name = "btnPad8";
            this.btnPad8.Size = new System.Drawing.Size(90, 90);
            this.btnPad8.TabIndex = 91;
            this.btnPad8.Text = "8";
            // 
            // btnPad7
            // 
            this.btnPad7.AllowFocus = false;
            this.btnPad7.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnPad7.Appearance.Options.UseFont = true;
            this.btnPad7.Location = new System.Drawing.Point(81, 222);
            this.btnPad7.Name = "btnPad7";
            this.btnPad7.Size = new System.Drawing.Size(90, 90);
            this.btnPad7.TabIndex = 90;
            this.btnPad7.Text = "7";
            ((System.ComponentModel.ISupportInitialize)(this.panelKeypadItem)).EndInit();
            this.panelKeypadItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPadQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPadItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemDescrip.Properties)).EndInit();
            this.ResumeLayout();
        }

    }
}
