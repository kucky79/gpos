namespace Bifrost.Win.Controls
{
    partial class aPeriodEdit
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
            this.btnMonthThis = new Bifrost.Win.Controls.aButtonSizeFree();
            this.btnMonthLast = new Bifrost.Win.Controls.aButtonSizeFree();
            this.btnMonth3 = new Bifrost.Win.Controls.aButtonSizeFree();
            this.btnMonth1 = new Bifrost.Win.Controls.aButtonSizeFree();
            this.btnYear = new Bifrost.Win.Controls.aButtonSizeFree();
            this.btnMonth6 = new Bifrost.Win.Controls.aButtonSizeFree();
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
            serializableAppearanceObject1.BackColor = System.Drawing.Color.White;
            serializableAppearanceObject1.BackColor2 = System.Drawing.Color.White;
            serializableAppearanceObject1.Options.UseBackColor = true;
            this.PickerEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::Bifrost.Win.Controls.Images.btn_period, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, true)});
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
            this.PickerControl.Controls.Add(this.btnMonthThis);
            this.PickerControl.Controls.Add(this.btnMonthLast);
            this.PickerControl.Controls.Add(this.btnMonth3);
            this.PickerControl.Controls.Add(this.btnMonth1);
            this.PickerControl.Controls.Add(this.btnYear);
            this.PickerControl.Controls.Add(this.btnMonth6);
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
            this.dateNavigatorTo.AllowAnimatedContentChange = true;
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
            this.dateNavigatorTo.ShowTodayButton = false;
            this.dateNavigatorTo.ShowWeekNumbers = false;
            this.dateNavigatorTo.Size = new System.Drawing.Size(210, 266);
            this.dateNavigatorTo.TabIndex = 2;
            // 
            // dateNavigatorFrom
            // 
            this.dateNavigatorFrom.AllowAnimatedContentChange = true;
            this.dateNavigatorFrom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dateNavigatorFrom.CalendarAppearance.DayCellSpecial.FontStyleDelta = System.Drawing.FontStyle.Regular;
            this.dateNavigatorFrom.CalendarAppearance.DayCellSpecial.Options.UseFont = true;
            this.dateNavigatorFrom.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNavigatorFrom.DateTime = new System.DateTime(2046, 4, 20, 0, 0, 0, 0);
            this.dateNavigatorFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateNavigatorFrom.EditValue = new System.DateTime(2046, 4, 20, 0, 0, 0, 0);
            this.dateNavigatorFrom.FirstDayOfWeek = System.DayOfWeek.Sunday;
            this.dateNavigatorFrom.Location = new System.Drawing.Point(2, 2);
            this.dateNavigatorFrom.Name = "dateNavigatorFrom";
            this.dateNavigatorFrom.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Single;
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
            // btnMonthThis
            // 
            this.btnMonthThis.BackColor = System.Drawing.Color.White;
            this.btnMonthThis.BorderColor = System.Drawing.Color.Transparent;
            this.btnMonthThis.ForeColor = System.Drawing.Color.Black;
            this.btnMonthThis.GroupKey = null;
            this.btnMonthThis.HoverColor = System.Drawing.Color.Transparent;
            this.btnMonthThis.HoverForeColor = System.Drawing.Color.Empty;
            this.btnMonthThis.HoverImage = global::Bifrost.Win.Controls.Images.Period_this_month_HOver;
            this.btnMonthThis.Image = global::Bifrost.Win.Controls.Images.Period_this_month;
            this.btnMonthThis.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMonthThis.Location = new System.Drawing.Point(13, 241);
            this.btnMonthThis.Name = "btnMonthThis";
            this.btnMonthThis.Selected = false;
            this.btnMonthThis.SelectedColor = System.Drawing.Color.LightGray;
            this.btnMonthThis.Size = new System.Drawing.Size(72, 22);
            this.btnMonthThis.TabIndex = 10;
            this.btnMonthThis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonthThis.UnSelectOtherButtons = false;
            this.btnMonthThis.UseDefaultImages = true;
            // 
            // btnMonthLast
            // 
            this.btnMonthLast.BackColor = System.Drawing.Color.White;
            this.btnMonthLast.BorderColor = System.Drawing.Color.Transparent;
            this.btnMonthLast.ForeColor = System.Drawing.Color.Black;
            this.btnMonthLast.GroupKey = null;
            this.btnMonthLast.HoverColor = System.Drawing.Color.Transparent;
            this.btnMonthLast.HoverForeColor = System.Drawing.Color.Empty;
            this.btnMonthLast.HoverImage = global::Bifrost.Win.Controls.Images.Period_last_month_HOver;
            this.btnMonthLast.Image = global::Bifrost.Win.Controls.Images.Period_last_month;
            this.btnMonthLast.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMonthLast.Location = new System.Drawing.Point(90, 241);
            this.btnMonthLast.Name = "btnMonthLast";
            this.btnMonthLast.Selected = false;
            this.btnMonthLast.SelectedColor = System.Drawing.Color.LightGray;
            this.btnMonthLast.Size = new System.Drawing.Size(72, 22);
            this.btnMonthLast.TabIndex = 9;
            this.btnMonthLast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonthLast.UnSelectOtherButtons = false;
            this.btnMonthLast.UseDefaultImages = true;
            // 
            // btnMonth3
            // 
            this.btnMonth3.BackColor = System.Drawing.Color.White;
            this.btnMonth3.BorderColor = System.Drawing.Color.Transparent;
            this.btnMonth3.ForeColor = System.Drawing.Color.Black;
            this.btnMonth3.GroupKey = null;
            this.btnMonth3.HoverColor = System.Drawing.Color.Transparent;
            this.btnMonth3.HoverForeColor = System.Drawing.Color.Empty;
            this.btnMonth3.HoverImage = global::Bifrost.Win.Controls.Images.Period_3month_HOver;
            this.btnMonth3.Image = global::Bifrost.Win.Controls.Images.Period_3month;
            this.btnMonth3.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMonth3.Location = new System.Drawing.Point(229, 241);
            this.btnMonth3.Name = "btnMonth3";
            this.btnMonth3.Selected = false;
            this.btnMonth3.SelectedColor = System.Drawing.Color.LightGray;
            this.btnMonth3.Size = new System.Drawing.Size(57, 22);
            this.btnMonth3.TabIndex = 7;
            this.btnMonth3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonth3.UnSelectOtherButtons = false;
            this.btnMonth3.UseDefaultImages = true;
            // 
            // btnMonth1
            // 
            this.btnMonth1.BackColor = System.Drawing.Color.White;
            this.btnMonth1.BorderColor = System.Drawing.Color.Transparent;
            this.btnMonth1.ForeColor = System.Drawing.Color.Black;
            this.btnMonth1.GroupKey = null;
            this.btnMonth1.HoverColor = System.Drawing.Color.Transparent;
            this.btnMonth1.HoverForeColor = System.Drawing.Color.Empty;
            this.btnMonth1.HoverImage = global::Bifrost.Win.Controls.Images.Period_1month_HOver;
            this.btnMonth1.Image = global::Bifrost.Win.Controls.Images.Period_1month;
            this.btnMonth1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMonth1.Location = new System.Drawing.Point(167, 241);
            this.btnMonth1.Name = "btnMonth1";
            this.btnMonth1.Selected = false;
            this.btnMonth1.SelectedColor = System.Drawing.Color.LightGray;
            this.btnMonth1.Size = new System.Drawing.Size(57, 22);
            this.btnMonth1.TabIndex = 5;
            this.btnMonth1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonth1.UnSelectOtherButtons = false;
            this.btnMonth1.UseDefaultImages = true;
            // 
            // btnYear
            // 
            this.btnYear.BackColor = System.Drawing.Color.White;
            this.btnYear.BorderColor = System.Drawing.Color.Transparent;
            this.btnYear.ForeColor = System.Drawing.Color.Black;
            this.btnYear.GroupKey = null;
            this.btnYear.HoverColor = System.Drawing.Color.Transparent;
            this.btnYear.HoverForeColor = System.Drawing.Color.Empty;
            this.btnYear.HoverImage = global::Bifrost.Win.Controls.Images.Period_1year_HOVer;
            this.btnYear.Image = global::Bifrost.Win.Controls.Images.Period_1year;
            this.btnYear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnYear.Location = new System.Drawing.Point(353, 241);
            this.btnYear.Name = "btnYear";
            this.btnYear.Selected = false;
            this.btnYear.SelectedColor = System.Drawing.Color.LightGray;
            this.btnYear.Size = new System.Drawing.Size(52, 22);
            this.btnYear.TabIndex = 6;
            this.btnYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnYear.UnSelectOtherButtons = false;
            this.btnYear.UseDefaultImages = true;
            // 
            // btnMonth6
            // 
            this.btnMonth6.BackColor = System.Drawing.Color.White;
            this.btnMonth6.BorderColor = System.Drawing.Color.Transparent;
            this.btnMonth6.ForeColor = System.Drawing.Color.Black;
            this.btnMonth6.GroupKey = null;
            this.btnMonth6.HoverColor = System.Drawing.Color.Transparent;
            this.btnMonth6.HoverForeColor = System.Drawing.Color.Empty;
            this.btnMonth6.HoverImage = global::Bifrost.Win.Controls.Images.Period_6month_HOVer;
            this.btnMonth6.Image = global::Bifrost.Win.Controls.Images.Period_6month;
            this.btnMonth6.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMonth6.Location = new System.Drawing.Point(291, 241);
            this.btnMonth6.Name = "btnMonth6";
            this.btnMonth6.Selected = false;
            this.btnMonth6.SelectedColor = System.Drawing.Color.LightGray;
            this.btnMonth6.Size = new System.Drawing.Size(57, 22);
            this.btnMonth6.TabIndex = 8;
            this.btnMonth6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonth6.UnSelectOtherButtons = false;
            this.btnMonth6.UseDefaultImages = true;
            // 
            // aPeriodEdit
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
        private aButtonSizeFree btnMonth1;
        private aButtonSizeFree btnYear;
        private aButtonSizeFree btnMonthThis;
        private aButtonSizeFree btnMonthLast;
        private aButtonSizeFree btnMonth6;
        private aButtonSizeFree btnMonth3;
    }
}
