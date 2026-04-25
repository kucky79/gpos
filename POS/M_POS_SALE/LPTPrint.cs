using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    class LPTPrint
    {
        /// <summary>
        /// Summary description for Class1.
        /// </summary>

        class FileWork
        {
            const uint GENERIC_READ = 0x80000000;
            const uint GENERIC_WRITE = 0x40000000;
            const uint OPEN_EXISTING = 3;

            IntPtr handle;

            [DllImport("kernel32", SetLastError = true)]
            static extern unsafe IntPtr CreateFile(
                string FileName, //file name
                uint DesiredAccess, //access mode
                uint ShareMode, //share mode
                uint SecurityAttributes, //security attributes
                uint CreationDisposition, //how to create
                uint FlagsAndAttributes, //file attributes
                int hTemplateFile //handle to template file
                );

            [DllImport("kernel32", SetLastError = true)]
            static extern unsafe bool ReadFile(
                IntPtr hFile,
                void* pBuffer,
                int NumberOfBytesToRead,
                int* pNumberOfBytesRead,
                int Overlapped
                );



            [DllImport("kernel32", SetLastError = true)]
            static extern unsafe bool WriteFile(
                IntPtr hFile,
                void* pBuffer,
                int nBytesToWrite,
                int* nBytesWritten,
                int Overlapped
                );


            [DllImport("kernel32", SetLastError = true)]
            static extern int GetLastError();

            [DllImport("kernel32", SetLastError = true)]
            static extern unsafe bool CloseHandle(IntPtr hObject);

            public FileWork()
            {
                //
                // TODO: Add constructor logic here
                //
            }

            public bool Open(string FileName)
            {
                handle = CreateFile(
                    FileName,
                    GENERIC_READ | GENERIC_WRITE,
                    0,
                    0,
                    OPEN_EXISTING,
                    0,
                    0);

                if (handle != IntPtr.Zero)
                    return true;
                else
                    return false;
            }

            public unsafe int Write(string cont)
            {
                int i = 0, n = 0;
                int Len = cont.Length;
                ASCIIEncoding e = new ASCIIEncoding();
                byte[] Buffer = e.GetBytes(cont);

                fixed (byte* p = Buffer)
                {
                    i = 0;
                    if (!WriteFile(handle, p + i, Len + 1, &n, 0))
                        return 0;
                }
                
                return n;
            }

            public bool Close()
            {
                return CloseHandle(handle);
            }
        }

        public class PrintTool
        {
            public PrintTool()
            {
            }
            
            public int SendToPrint(string printer, string buffer)
            {
                FileWork fw = new FileWork();

                int iR = 0;

                if (fw.Open(printer))
                {
                    iR = fw.Write(buffer);
                }

                fw.Close();

                return iR;
            }
        }
    }
}
