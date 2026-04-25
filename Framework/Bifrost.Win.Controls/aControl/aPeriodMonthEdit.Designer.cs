namespace Bifrost.Win.Controls
{
    partial class aPeriodMonthEdit
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.PickerEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            this.PickerControl = new DevExpress.XtraEditors.PopupContainerControl();
            this.dateNavigatorTo = new DevExpress.XtraScheduler.DateNavigator();
            this.dateNavigatorFrom = new DevExpress.XtraScheduler.DateNavigator();
            this.txtDtFrom = new DevExpress.XtraEditors.TextEdit();
            this.txtDtTo = new DevExpress.XtraEditors.TextEdit();
            this.Label = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.PickerEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PickerControl)).BeginInit();
            this.PickerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorTo.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorFrom.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // PickerEdit
            // 
            this.PickerEdit.AllowDrop = true;
            this.PickerEdit.Location = new System.Drawing.Point(0, 0);
            this.PickerEdit.Name = "PickerEdit";
            this.PickerEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.PickerEdit.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.PickerEdit.Properties.Appearance.Options.UseBackColor = true;
            this.PickerEdit.Properties.Appearance.Options.UseBorderColor = true;
            this.PickerEdit.Properties.AutoHeight = false;
            this.PickerEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions1.Image = global::Bifrost.Win.Controls.Images.btn_period;
            serializableAppearanceObject1.BackColor = System.Drawing.Color.White;
            serializableAppearanceObject1.BackColor2 = System.Drawing.Color.White;
            serializableAppearanceObject1.Options.UseBackColor = true;
            serializableAppearanceObject2.BackColor = System.Drawing.Color.White;
            serializableAppearanceObject2.BackColor2 = System.Drawing.Color.White;
            serializableAppearanceObject2.Options.UseBackColor = true;
            serializableAppearanceObject3.BackColor = System.Drawing.Color.White;
            serializableAppearanceObject3.BackColor2 = System.Drawing.Color.White;
            serializableAppearanceObject3.Options.UseBackColor = true;
            serializableAppearanceObject4.BackColor = System.Drawing.Color.White;
            serializableAppearanceObject4.BackColor2 = System.Drawing.Color.White;
            serializableAppearanceObject4.Options.UseBackColor = true;
            this.PickerEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null)});
            this.PickerEdit.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.PickerEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.PickerEdit.Properties.PopupControl = this.PickerControl;
            this.PickerEdit.Properties.PopupSizeable = false;
            this.PickerEdit.Properties.ShowPopupCloseButton = false;
            this.PickerEdit.Properties.ShowPopupShadow = false;
            this.PickerEdit.Size = new System.Drawing.Size(185, 24);
            this.PickerEdit.TabIndex = 0;
            this.PickerEdit.TabStop = false;
            // 
            // PickerControl
            // 
            this.PickerControl.Appearance.BackColor = System.Drawing.Color.White;
            this.PickerControl.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.PickerControl.Appearance.Options.UseBackColor = true;
            this.PickerControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.PickerControl.Controls.Add(this.dateNavigatorTo);
            this.PickerControl.Controls.Add(this.dateNavigatorFrom);
            this.PickerControl.Location = new System.Drawing.Point(2, 25);
            this.PickerControl.Margin = new System.Windows.Forms.Padding(0);
            this.PickerControl.MaximumSize = new System.Drawing.Size(420, 270);
            this.PickerControl.MinimumSize = new System.Drawing.Size(420, 270);
            this.PickerControl.Name = "PickerControl";
            this.PickerControl.Size = new System.Drawing.Size(420, 270);
            this.PickerControl.TabIndex = 1;
            // 
            // dateNavigatorTo
            // 
            this.dateNavigatorTo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dateNavigatorTo.CalendarAppearance.DayCellSpecial.FontStyleDelta = System.Drawing.FontStyle.Regular;
            this.dateNavigatorTo.CalendarAppearance.DayCellSpecial.Options.UseFont = true;
            this.dateNavigatorTo.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNavigatorTo.DateTime = new System.DateTime(2001, 11, 20, 0, 0, 0, 0);
            this.dateNavigatorTo.Dock = System.Windows.Forms.DockStyle.Right;
            this.dateNavigatorTo.EditValue = new System.DateTime(2001, 11, 20, 0, 0, 0, 0);
            this.dateNavigatorTo.FirstDayOfWeek = System.DayOfWeek.Sunday;
            this.dateNavigatorTo.Location = new System.Drawing.Point(208, 2);
            this.dateNavigatorTo.Name = "dateNavigatorTo";
            this.dateNavigatorTo.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Single;
            this.dateNavigatorTo.ShowMonthHeaders = false;
            this.dateNavigatorTo.ShowTodayButton = false;
            this.dateNavigatorTo.ShowWeekNumbers = false;
            this.dateNavigatorTo.Size = new System.Drawing.Size(210, 266);
            this.dateNavigatorTo.TabIndex = 2;
            // 
            // dateNavigatorFrom
            // 
            this.dateNavigatorFrom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dateNavigatorFrom.CalendarAppearance.DayCellSpecial.FontStyleDelta = System.Drawing.FontStyle.Regular;
            this.dateNavigatorFrom.CalendarAppearance.DayCellSpecial.Options.UseFont = true;
            this.dateNavigatorFrom.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNavigatorFrom.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.dateNavigatorFrom.DateTime = new System.DateTime(2046, 4, 20, 0, 0, 0, 0);
            this.dateNavigatorFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateNavigatorFrom.EditValue = new System.DateTime(2046, 4, 20, 0, 0, 0, 0);
            this.dateNavigatorFrom.FirstDayOfWeek = System.DayOfWeek.Sunday;
            this.dateNavigatorFrom.Location = new System.Drawing.Point(2, 2);
            this.dateNavigatorFrom.Name = "dateNavigatorFrom";
            this.dateNavigatorFrom.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Single;
            this.dateNavigatorFrom.ShowMonthHeaders = false;
            this.dateNavigatorFrom.ShowTodayButton = false;
            this.dateNavigatorFrom.ShowWeekNumbers = false;
            this.dateNavigatorFrom.Size = new System.Drawing.Size(210, 266);
            this.dateNavigatorFrom.TabIndex = 1;
            // 
            // txtDtFrom
            // 
            this.txtDtFrom.Location = new System.Drawing.Point(0, 0);
            this.txtDtFrom.Name = "txtDtFrom";
            this.txtDtFrom.Properties.AutoHeight = false;
            this.txtDtFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtDtFrom.Size = new System.Drawing.Size(75, 24);
            this.txtDtFrom.TabIndex = 2;
            // 
            // txtDtTo
            // 
            this.txtDtTo.Location = new System.Drawing.Point(87, 0);
            this.txtDtTo.Name = "txtDtTo";
            this.txtDtTo.Properties.AutoHeight = false;
            this.txtDtTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtDtTo.Size = new System.Drawing.Size(75, 24);
            this.txtDtTo.TabIndex = 3;
            // 
            // Label
            // 
            this.Label.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Label.Appearance.Font = new System.Drawing.Font("Tahoma", 7F);
            this.Label.Appearance.Options.UseBackColor = true;
            this.Label.Appearance.Options.UseFont = true;
            this.Label.Appearance.Options.UseTextOptions = true;
            this.Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Label.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Label.Location = new System.Drawing.Point(75, 0);
            this.Label.Margin = new System.Windows.Forms.Padding(0);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(12, 24);
            this.Label.TabIndex = 4;
            this.Label.Text = "~";
            this.Label.UseMnemonic = false;
            // 
            // aPeriodMonthEdit
            // 
            this.Controls.Add(this.Label);
            this.Controls.Add(this.txtDtFrom);
            this.Controls.Add(this.txtDtTo);
            this.Controls.Add(this.PickerControl);
            this.Controls.Add(this.PickerEdit);
            this.MinimumSize = new System.Drawing.Size(185, 24);
            this.Name = "aPeriodEdit";
            this.Size = new System.Drawing.Size(185, 24);
            ((System.ComponentModel.ISupportInitialize)(this.PickerEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PickerControl)).EndInit();
            this.PickerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorTo.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorFrom.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtTo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl PickerControl;

        private DevExpress.XtraScheduler.DateNavigator dateNavigatorFrom;
        private DevExpress.XtraScheduler.DateNavigator dateNavigatorTo;
        private DevExpress.XtraEditors.TextEdit txtDtFrom;
        private DevExpress.XtraEditors.TextEdit txtDtTo;
        private DevExpress.XtraEditors.LabelControl Label;

        private DevExpress.XtraEditors.PopupContainerEdit PickerEdit;
    }
}
