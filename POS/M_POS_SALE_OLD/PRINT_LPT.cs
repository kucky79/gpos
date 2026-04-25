using Bifrost.Helper;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace POS
{
    public static class PRINT_LPT
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        public static bool Print(string strPort, string strPrint)
        {
            string nl = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();
            bool IsConnected = false;

            try
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
            catch (Exception ex)
            {
                throw ex;
            }

            return IsConnected;
        }

        public static bool PrintImage(string strPort, byte[] ImageByte)
        {
            string nl = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();
            bool IsConnected = false;

            try
            {
                //Byte[] buffer = new byte[strPrint.Length];
                //buffer = Encoding.Default.GetBytes(strPrint);

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
}
