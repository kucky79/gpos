using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Util
{
    public class FontLibrary
    {
        private static FontLibrary inst = new FontLibrary();
        public PrivateFontCollection privateFont = new PrivateFontCollection();
        public static FontFamily[] Families
        {
            get
            {
                return inst.privateFont.Families;
            }
        }

        public FontLibrary()
        {
            AddFontFromMemory();
        }

        private void AddFontFromMemory()
        {
            //List<byte[]> fonts = new List<byte[]>();
            //fonts.Add(Properties.Resources.본고딕KR_Regular);

            //foreach (byte[] font in fonts)
            //{
            //    IntPtr fontBuffer = Marshal.AllocCoTaskMem(font.Length);
            //    Marshal.Copy(font, 0, fontBuffer, font.Length);
            //    privateFont.AddMemoryFont(fontBuffer, font.Length);
            //}
        }

        static readonly PrivateFontCollection _privateFonts = new PrivateFontCollection();

        //public static Font GetDefaultFont
        //{
        //    get
        //    { 
        //        _privateFonts.AddFontFile(Properties.Resources.본고딕KR_Regular.ToString());// Path.Combine(FilePath, "NanumGothic.ttf"));
        //                                                                                //privateFonts.AddFontFile(Path.Combine(FilePath, "NanumGothicRegular.ttf"));
        //                                                                                //privateFonts.AddFontFile(Path.Combine(FilePath, "NanumGothicExtraRegular.ttf"));
        //                                                                                //privateFonts.AddFontFile(Path.Combine(FilePath, "NanumGothicLight.ttf"));

        //        Font font9 = new Font(_privateFonts.Families[0], 9f);
        //        return font9;
        //    }
        //}


    }
}
