using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Helper
{
    /// <summary>
    /// 프린터 커맨드 클래스
    /// </summary>
    public class PrinterCommand
    {
        /// <summary>NUL</summary>
        public static string NUL = Convert.ToString((char)0);
        /// <summary>SOH</summary>
        public static string SOH = Convert.ToString((char)1);
        /// <summary>STX</summary>
        public static string STX = Convert.ToString((char)2);
        /// <summary>ETX</summary>
        public static string ETX = Convert.ToString((char)3);
        /// <summary>EOT</summary>
        public static string EOT = Convert.ToString((char)4);
        /// <summary>ENQ</summary>
        public static string ENQ = Convert.ToString((char)5);
        /// <summary>ACK</summary>
        public static string ACK = Convert.ToString((char)6);
        /// <summary>BEL</summary>
        public static string BEL = Convert.ToString((char)7);
        /// <summary>BS</summary>
        public static string BS = Convert.ToString((char)8);
        /// <summary>TAB</summary>
        public static string TAB = Convert.ToString((char)9);
        /// <summary>VT</summary>
        public static string VT = Convert.ToString((char)11);
        /// <summary>FF</summary>
        public static string FF = Convert.ToString((char)12);
        /// <summary>CR</summary>
        public static string CR = Convert.ToString((char)13);
        /// <summary>SO</summary>
        public static string SO = Convert.ToString((char)14);
        /// <summary>SI</summary>
        public static string SI = Convert.ToString((char)15);
        /// <summary>DLE</summary>
        public static string DLE = Convert.ToString((char)16);
        /// <summary>DC1</summary>
        public static string DC1 = Convert.ToString((char)17);
        /// <summary>DC2</summary>
        public static string DC2 = Convert.ToString((char)18);
        /// <summary>DC3</summary>
        public static string DC3 = Convert.ToString((char)19);
        /// <summary>DC4</summary>
        public static string DC4 = Convert.ToString((char)20);
        /// <summary>NAK</summary>
        public static string NAK = Convert.ToString((char)21);
        /// <summary>SYN</summary>
        public static string SYN = Convert.ToString((char)22);
        /// <summary>ETB</summary>
        public static string ETB = Convert.ToString((char)23);
        /// <summary>CAN</summary>
        public static string CAN = Convert.ToString((char)24);
        /// <summary>EM</summary>
        public static string EM = Convert.ToString((char)25);
        /// <summary>SUB</summary>
        public static string SUB = Convert.ToString((char)26);
        /// <summary>ESC</summary>
        public static string ESC = Convert.ToString((char)27);
        /// <summary>FS</summary>
        public static string FS = Convert.ToString((char)28);
        /// <summary>GS</summary>
        public static string GS = Convert.ToString((char)29);
        /// <summary>RS</summary>
        public static string RS = Convert.ToString((char)30);
        /// <summary>US</summary>
        public static string US = Convert.ToString((char)31);
        /// <summary>Space</summary>
        public static string Space = Convert.ToString((char)32);

        #region 기능 커맨드 모음
        /// <summary> 프린터 초기화</summary>
        public static string InitializePrinter = ESC + "@";

        /// <summary>ASCII LF</summary>
        public static string NewLine = Convert.ToString((char)10);

        /// <summary>
        /// 라인피드
        /// </summary>
        /// <param name="val">라인피드시킬 줄 수</param>
        /// <returns>변환된 문자열</returns>
        public static string LineFeed(int val)
        {
            return PrinterCommand.ESC + "d" + PrinterCommand.DecimalToCharString(val);
        }

        public static string DoubleOn = GS + "!" + "\u0011";  // 2x sized text (double-high + double-wide)

        public static string DoubleOff = GS + "!" + "\0";

        /// <summary>볼드 On</summary>
        public static string BoldOn = ESC + "E" + PrinterCommand.DecimalToCharString(1);

        /// <summary>볼드 Off</summary>
        public static string BoldOff = ESC + "E" + PrinterCommand.DecimalToCharString(0);

        /// <summary>언더라인 On</summary>
        public static string UnderlineOn = ESC + "-" + PrinterCommand.DecimalToCharString(1);

        /// <summary>언더라인 Off</summary>
        public static string UnderlineOff = ESC + "-" + PrinterCommand.DecimalToCharString(0);

        /// <summary>흑백반전 On</summary>
        public static string ReverseOn = GS + "B" + PrinterCommand.DecimalToCharString(1);

        /// <summary>흑백반전 Off</summary>
        public static string ReverseOff = GS + "B" + PrinterCommand.DecimalToCharString(0);

        /// <summary>좌측정렬</summary>
        public static string AlignLeft = PrinterCommand.ESC + "a" + PrinterCommand.DecimalToCharString(0);

        /// <summary>가운데정렬</summary>
        public static string AlignCenter = PrinterCommand.ESC + "a" + PrinterCommand.DecimalToCharString(1);

        /// <summary>우측정렬</summary>
        public static string AlignRight = PrinterCommand.ESC + "a" + PrinterCommand.DecimalToCharString(2);

        /// <summary>커트</summary>
        public static string Cut = PrinterCommand.GS + "V" + PrinterCommand.DecimalToCharString(1);

        #endregion 기능 커맨드 모음 끝

        /// <summary>
        /// Decimal을 캐릭터 변환 후 스트링을 반환 합니다.
        /// </summary>
        /// <param name="val">커맨드 숫자</param>
        /// <returns>변환된 문자열</returns>
        public static string DecimalToCharString(decimal val)
        {
            string result = "";

            try
            {
                result = Convert.ToString((char)val);
            }
            catch { }

            return result;
        }

        /// <summary>
        /// FONT 명령어의 글자사이즈 속성을 변환 합니다.
        /// </summary>
        /// <param name="width">가로</param>
        /// <param name="height">세로</param>
        /// <returns>가로 x 세로</returns>
        public static string ConvertFontSize(int width, int height)
        {
            string result = "0";
            int _w, _h;

            //가로변환
            if (width == 1)
                _w = 0;
            else if (width == 2)
                _w = 16;
            else if (width == 3)
                _w = 32;
            else if (width == 4)
                _w = 48;
            else if (width == 5)
                _w = 64;
            else if (width == 6)
                _w = 80;
            else if (width == 7)
                _w = 96;
            else if (width == 8)
                _w = 112;
            else _w = 0;

            //세로변환
            if (height == 1)
                _h = 0;
            else if (height == 2)
                _h = 1;
            else if (height == 3)
                _h = 2;
            else if (height == 4)
                _h = 3;
            else if (height == 5)
                _h = 4;
            else if (height == 6)
                _h = 5;
            else if (height == 7)
                _h = 6;
            else if (height == 8)
                _h = 7;
            else _h = 0;

            //가로x세로
            int sum = _w + _h;
            result = PrinterCommand.GS + "!" + PrinterCommand.DecimalToCharString(sum);

            return result;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        public static bool Print(string strPort, string strPrint)
        {
            string nl = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();
            bool IsConnected = false;

            try
            {
                if (strPort.Substring(0, 3) == "LPT")
                {
                    Byte[] buffer = new byte[strPrint.Length];
                    buffer = Encoding.Default.GetBytes(strPrint);

                    SafeFileHandle fh = CreateFile(strPort, FileAccess.Write, 0, IntPtr.Zero, FileMode.OpenOrCreate, 0, IntPtr.Zero);
                    if (!fh.IsInvalid)
                    {
                        IsConnected = true;
                        FileStream fs = new FileStream(fh, FileAccess.ReadWrite);
                        fs.Write(buffer, 0, buffer.Length);

                        fs.Close();
                    }
                }
                else if (strPort.Substring(0, 3) == "COM")
                {
                    IsConnected = true;

                    SerialPort portName = new SerialPort();

                    if (portName.IsOpen)
                        portName.Close();

                    portName.Encoding = Encoding.GetEncoding("ks_c_5601-1987");
                    portName.PortName = strPort;
                    portName.BaudRate = (int)9600; //고정
                    portName.DataBits = (int)8;
                    portName.Parity = Parity.None;
                    portName.StopBits = StopBits.One;
                    portName.Handshake = Handshake.None;
                    portName.DtrEnable = true;
                    portName.DataReceived += Port_DataReceived;

                    portName.Open();
                    portName.Write(strPrint);
                    portName.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsConnected;
        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
        }


        public static bool PrintImage(string strPort, byte[] ImageByte)
        {
            string nl = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();
            bool IsConnected = false;

            try
            {
                SafeFileHandle fh = CreateFile(strPort, FileAccess.Write, 0, IntPtr.Zero, FileMode.OpenOrCreate, 0, IntPtr.Zero);
                if (!fh.IsInvalid)
                {
                    IsConnected = true;
                    FileStream fs = new FileStream(fh, FileAccess.ReadWrite);
                    fs.Write(ImageByte, 0, ImageByte.Length);

                    fs.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsConnected;
        }


    }

    public class ImagePrint
    {
        /// <summary>
        /// 이미지를 바이트 배열로 반환 합니다.
        /// </summary>
        /// <param name="logoPath">이미지 경로</param>
        /// <param name="printWidth">이미지 출력 가로길이</param>
        /// <returns></returns>
        public static byte[] GetLogo(string logoPath, int printWidth)
        {
            List<byte> byteList = new List<byte>();
            if (!File.Exists(logoPath))
                return null;
            BitmapData data = GetBitmapData(logoPath, printWidth);
            BitArray dots = data.Dots;
            byte[] width = BitConverter.GetBytes(data.Width);

            int offset = 0;
            //byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            //byteList.Add(Convert.ToByte('@'));
            byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            byteList.Add(Convert.ToByte('3'));
            byteList.Add((byte)24);
            while (offset < data.Height)
            {
                byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
                byteList.Add(Convert.ToByte('*'));
                byteList.Add((byte)33);
                byteList.Add(width[0]);
                byteList.Add(width[1]);

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;
                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;
                            int i = (y * data.Width) + x;

                            bool v = false;
                            if (i < dots.Length)
                                v = dots[i];

                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }
                        byteList.Add(slice);
                    }
                }
                offset += 24;
                byteList.Add(Convert.ToByte(0x0A));
            }
            byteList.Add(Convert.ToByte(0x1B));
            byteList.Add(Convert.ToByte('3'));
            byteList.Add((byte)30);
            return byteList.ToArray();
        }

        private static BitmapData GetBitmapData(string bmpFileName, int width)
        {
            using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
            {
                var threshold = 127;
                var index = 0;
                double multiplier = width; // 이미지 width조정
                double scale = (double)(multiplier / (double)bitmap.Width);
                int xheight = (int)(bitmap.Height * scale);
                int xwidth = (int)(bitmap.Width * scale);
                var dimensions = xwidth * xheight;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < xheight; y++)
                {
                    for (var x = 0; x < xwidth; x++)
                    {
                        var _x = (int)(x / scale);
                        var _y = (int)(y / scale);
                        var color = bitmap.GetPixel(_x, _y);
                        var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminance < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                {
                    Dots = dots,
                    Height = (int)(bitmap.Height * scale),
                    Width = (int)(bitmap.Width * scale)
                };
            }
        }

        private class BitmapData
        {
            public BitArray Dots { get; set; }

            public int Height { get; set; }

            public int Width { get; set; }
        }
    }

    public class POSPrint
    {
        SerialPort comPort;
        
        public void Connection(PrintType printType)
        {
            comPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            comPort.Open();
        }
        


        public void Print(string Contents, PrintAlign align, int FontWidth, int FontHeight, bool isEol)
        { 
        }
    }



}
