using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using POS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYS
{
    public partial class M_SYS_CONFIG : BifrostFormBase
    {
        public M_SYS_CONFIG()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            SetControl ctr = new SetControl();
            ctr.SetCombobox(aLookUpEditModule, CH.GetCode("SYS001", true));

            DevExpress.Utils.Svg.SvgPalette svgPalette = new DevExpress.Utils.Svg.SvgPalette();
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Paint", Color.FromArgb(242, 242, 242)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Paint High", Color.FromArgb(255, 255, 255)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Paint Shadow", Color.FromArgb(222, 222, 222)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Brush", Color.FromArgb(80, 80, 80)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Brush Light", Color.FromArgb(150, 150, 150)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Brush High", Color.FromArgb(80, 80, 80)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Brush Major", Color.FromArgb(180, 180, 180)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Brush Minor", Color.FromArgb(210, 210, 210)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Accent Paint", Color.FromArgb(23, 107, 209)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Accent Paint Light", Color.FromArgb(191, 224, 255)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Accent Brush", Color.FromArgb(255, 255, 255)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Accent Brush Light", Color.FromArgb(81, 148, 224)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Key Paint", Color.FromArgb(71, 71, 71)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Key Brush", Color.FromArgb(255, 255, 255)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Key Brush Light", Color.FromArgb(150, 150, 150)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Red", Color.FromArgb(226, 54, 66)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Green", Color.FromArgb(60, 146, 92)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Blue", Color.FromArgb(58, 116, 194)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Yellow", Color.FromArgb(252, 169, 10)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Black", Color.FromArgb(122, 122, 122)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("Gray", Color.FromArgb(190, 190, 190)));
            svgPalette.Colors.Add(new DevExpress.Utils.Svg.SvgColor("White", Color.FromArgb(255, 255, 255)));

            var commonSkin = DevExpress.Skins.CommonSkins.GetSkin(UserLookAndFeel.Default);
            commonSkin.CustomSvgPalettes.Add(new DevExpress.Utils.Svg.SvgPaletteKey(commonSkin.CustomSvgPalettes.Count, "Custom Palette"), svgPalette);
            UserLookAndFeel.Default.SetSkinStyle(commonSkin.Name, "Custom Palette");

            SetGridInit(gridView1);
            RestorePalette();

            this.FormClosed += M_POS_CONFIG_FormClosed;

        }

        private void M_POS_CONFIG_FormClosed(object sender, FormClosedEventArgs e)
        {
            SavePalette();
        }


        private void RestorePalette()
        {

            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            Bifrost.IniFile inifile = new Bifrost.IniFile();

            string SkinName = inifile.IniReadValue("ApplicationSkinName", "Skin", Path);
            string PaletteName = inifile.IniReadValue("ApplicationSkinName", "Palette", Path);

            if (SkinName == string.Empty)
            {
                //default 
                inifile.IniWriteValue("ApplicationSkinName", "Skin", "The Bezier", Path);
                SkinName = inifile.IniReadValue("ApplicationSkinName", "Skin", Path);

                if (SkinName == "The Bezier" && PaletteName != string.Empty)    
                {
                    UserLookAndFeel.Default.SetSkinStyle(SkinName, PaletteName);
                }
                else
                    SetSkin(SkinName);

            }
            else if (PaletteName != string.Empty)
            {
                UserLookAndFeel.Default.SetSkinStyle(SkinStyle.Bezier, PaletteName);
            }
        }

        private void SetSkin(string skinName)
        {
            UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        private void SavePalette()
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            Bifrost.IniFile inifile = new Bifrost.IniFile();

            //skin
            inifile.IniWriteValue("ApplicationSkinName", "Skin", UserLookAndFeel.Default.SkinName, Path);
            inifile.IniWriteValue("ApplicationSkinName", "Palette", UserLookAndFeel.Default.ActiveSvgPaletteName, Path);
        }
        private void SetGridInit(GridView view)
        {
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowAutoFilterRow = false;
            view.OptionsCustomization.AllowSort = true;
            view.OptionsCustomization.AllowFilter = false;
        }

        public override void OnView()
        {
            DataTable dt = Search(new object[] { Global.FirmCode, aLookUpEditModule.EditValue, txtSearch.Text });
            gridMain.Binding(dt, true);
        }

        public override void OnSave()
        {
            gridView1.PostEditor();
            gridView1.UpdateCurrentRow();

            DataTable dtChange = gridMain.GetChanges();

            if (dtChange == null) return;

            bool result = Save(dtChange);

            if (result)
            {
                SaveConfigData();
                ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
                if (dtChange != null) dtChange.AcceptChanges();
            }
        }

        private void SaveConfigData()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "SystemConfig.xml";

                FileInfo fileinfo = new FileInfo(path);

                if (fileinfo.Exists)
                {
                    fileinfo.Delete();
                }

                DataSet ds = DBHelper.GetDataSet("USP_SYS_CONFIG_XML_S", new object[] { Global.FirmCode });
                ds.WriteXml(path, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
