using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Web;
using System.Security.Principal;
using System.Diagnostics;

namespace Bifrost.Helper
{
    public class HttpFileHelper
    {
        private static string _fileServerIP = string.Empty;
        private static string _path = string.Empty;
        private static IniFile _iniFile = null;

        private static void GetServerInfo()
        {
            #region GetserverIP
            _path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            _iniFile = new IniFile();

            //FileServer
            _fileServerIP = _iniFile.IniReadValue("FileServer", "IP", _path);
            #endregion
        }

        /// <summary>
        /// File Upload orignal FileName
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="folder"></param>
        public static void Upload(string fullPath, string folder)
        {
            char[] delimiterChars = { '\\', '.' };
            string[] words = fullPath.Split(delimiterChars);

            string FileName = words[words.Length - 2];
            Upload(FileName, fullPath, folder);
        }

        /// <summary>
        /// File Upload Custom FileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullPath"></param>
        /// <param name="folder"></param>
        public static void Upload(string fileReName, string fullPath, string folder)
        {
            try
            {
                GetServerInfo();

                char[] delimiterChars = { '\\', '.' };
                string[] words = fullPath.Split(delimiterChars);

                string FileNameExtension = words[words.Length-1];
                fileReName = fileReName + "." + FileNameExtension;
                string UpFileName = HttpUtility.UrlEncode(fileReName, Encoding.UTF8);

                WebClient _webClient = new WebClient();
                _webClient.Credentials = CredentialCache.DefaultCredentials;
                _webClient.Encoding = Encoding.UTF8;
                //byte[] fileContents = File.ReadAllBytes(UpFileName);

                string UpFileDate = DateTime.Now.ToString("yyyy/MM/dd hh:mm");
                //Uri uri = new Uri("http://" + FileServerIP + "/FileUpload.aspx?FileName=" + UpFileName + "&Folder=" + folder + "&Date" + UpFileDate);

                Uri uri = new Uri("http://" + _fileServerIP + "/ISYWebService/HttpFile/FileUpload.aspx?FileName=" + UpFileName + "&Folder=" + folder + "&Date" + UpFileDate);

                _webClient.UploadProgressChanged += UploadProgressCallback;
                _webClient.UploadFileCompleted += UploadFileCompleted;

                // myWebClient.Credentials = new NetworkCredential("test", "test");  //사용자 인증을 위하여
                //string cc = myWebClient.DownloadString(uri); //사용자 인증체크 위하여 인증 안되면 Exception 발생
                // Console.WriteLine("--- : " + cc);

                _webClient.UploadFileAsync(uri, "POST", fullPath);

                Console.WriteLine("File upload started.");
                _webClient.Dispose();
            }
            catch (WebException ex)
            {
                Console.WriteLine("\nResponse Exception :\n{0}", ex.ToString());
            }
        }

        private static void UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            try
            {
                string Result = Encoding.UTF8.GetString(e.Result);//파일 업로드 완료후 리턴된 string
                Console.WriteLine("Return Value : " + Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Return Execption : " + ex);
            }

        }

        private static void UploadProgressCallback(object sender, UploadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0} uploaded {1} of {2} bytes. {3} % complete...", (string)e.UserState, e.BytesSent, e.TotalBytesToSend, Math.Truncate(((double)e.BytesSent / (double)e.TotalBytesToSend) * 100));
            //progressBar1.Value = int.Parse(Math.Truncate(((double)e.BytesSent / (double)e.TotalBytesToSend) * 100).ToString());
        }

        /// <summary>
        /// File Upload Custom FileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullPath"></param>
        /// <param name="folder"></param>
        public static void MultiUpload(string[] fullPaths, string folder)
        {
            string fullPath;
            string fileReName = "";
            string UpFileName = "";
            //WebClient _webClient = null;
            Uri uri = null;

            string UpFileDate = DateTime.Now.ToString("yyyy/MM/dd hh:mm");

            //string str_url = "";




            try
            {
                GetServerInfo();



                for (int i = 0; i < fullPaths.Length; i++)
                {

                    using (WebClient _webClient = new WebClient())
                    {
                        _webClient.Credentials = CredentialCache.DefaultCredentials;
                        _webClient.Encoding = Encoding.UTF8;

                        fullPath = fullPaths[i];
                        fileReName = System.IO.Path.GetFileName(fullPath);

                        //서버쪽에서 UTF-8로 디코딩함
                        UpFileName = HttpUtility.UrlEncode(fileReName, Encoding.UTF8);
                        //UpFileName = HttpUtility.UrlEncode(fileReName, Encoding.GetEncoding("euc-kr")).Replace("+", "%20");

                        uri = new Uri("http://" + _fileServerIP + "/ISYWebService/HttpFile/FileUpload.aspx?FileName=" + UpFileName + "&Folder=" + folder + "&Date" + UpFileDate);

                        //_webClient.UploadProgressChanged += UploadProgressCallback;
                        //_webClient.UploadFileCompleted += ploadFileCompleted;


                        //_webClient.UploadFileAsync(uri, "POST", fullPath);
                        _webClient.UploadFile(uri, "POST", fullPath);
                    }




                    //_webClient = new WebClient();

                    //str_url = "http://"+ FileServerIP + "/ISYWebService/HttpFileUpload/Upload/"+ folder + "/" + fileReName;

                    //if(!RemoteFileExists(str_url))
                    //{
                    //    throw new Exception("File Upload Fail..");
                    //}

                }

                //while (_Cnt_FileUpladResult != fullPaths.Length)
                //{

                //}
                //for (int i = 0; i < fullPaths.Length; i++)
                //{
                //    fileReName = System.IO.Path.GetFileName(fullPaths[i]);
                //    UpFileName = HttpUtility.UrlEncode(fileReName, Encoding.UTF8);
                //    str_url = "http://" + FileServerIP + "/ISYWebService/HttpFileUpload/Upload/" + folder + "/" + fileReName;

                //    if (!RemoteFileExists(str_url))
                //    {
                //        throw new Exception("File Upload Fail..");
                //    }

                //}
            }
            catch
            {
                throw new Exception("Some attachments are open. Please close the opened files.");
            }
            finally
            {
                //if (_webClient != null)
                //    _webClient.Dispose();
            }
        }

        /// <summary>
        /// File Upload Custom FileName 대용량
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullPath"></param>
        /// <param name="folder"></param>
        public static void MultiUpload2(string[] fullPaths, string folder)
        {
            string FilePath;
            string fileReName = "";
           // string UpFileName = "";

            //creating the ServiceSoapClient which will allow to connect to the service.
            SVCWeb.FileUpload_M web = null;
            FileStream fs = null;
            try
            {
                GetServerInfo();

                web = new SVCWeb.FileUpload_M();

                web.Url = @"http://" + _fileServerIP + @"/ISYWebService/HttpFile/FileUpload_M.asmx";
                

                for (int i = 0; i < fullPaths.Length; i++)
                {

                    FilePath = fullPaths[i];
                    fileReName = System.IO.Path.GetFileName(FilePath);

                    int Offset = 0; // starting offset.

                    //define the chunk size
                    int ChunkSize = 65536; // 64 * 1024 kb

                    //define the buffer array according to the chunksize.
                    byte[] Buffer = new byte[ChunkSize];


                    //opening the file for read.
                    fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);


                    long FileSize = new FileInfo(FilePath).Length; // File size of file being uploaded.
                                                                   // reading the file.
                    fs.Position = Offset;

                    int BytesRead = 0;

                    while (Offset != FileSize) // continue uploading the file chunks until offset = file size.
                    {
                        BytesRead = fs.Read(Buffer, 0, ChunkSize); // read the next chunk 
                                                                   // (if it exists) into the buffer. 
                                                                   // the while loop will terminate if there is nothing left to read
                                                                   // check if this is the last chunk and resize the buffer as needed 
                                                                   // to avoid sending a mostly empty buffer 
                                                                   // (could be 10Mb of 000000000000s in a large chunk)
                        if (BytesRead != Buffer.Length)
                        {
                            ChunkSize = BytesRead;
                            byte[] TrimmedBuffer = new byte[BytesRead];
                            Array.Copy(Buffer, TrimmedBuffer, BytesRead);
                            Buffer = TrimmedBuffer; // the trimmed buffer should become the new 'buffer'
                        }
                        // send this chunk to the server. it is sent as a byte[] parameter, 
                        // but the client and server have been configured to encode byte[] using MTOM. 
                        bool ChunkAppened = web.UploadFile(folder+@"\", fileReName, Buffer, Offset);

                        if (!ChunkAppened)
                        {
                            break;
                        }

                        // Offset is only updated AFTER a successful send of the bytes. 
                        Offset += BytesRead; // save the offset position for resume
                    }

                }
                    
                

                //byte[] Filestream;

                //for (int i = 0; i < fullPaths.Length; i++)
                //{

                //    fullPath = fullPaths[i];
                //    fileReName = System.IO.Path.GetFileName(fullPath);

                //    //서버쪽에서 UTF-8로 디코딩함
                //    UpFileName = HttpUtility.UrlEncode(fileReName, Encoding.UTF8);
                //    UpFileName = fileReName;

                //    Filestream = ReadBinaryFile(fullPath);

                //    //string strSaveTO = @"http://" + FileServerIP + @"/ISYWebService/HttpFile/Upload/" + folder + @"/" + UpFileName;  // 파일의 경로입니다.
                //    string strSaveTO = @"D:\AIMS_WEB_SERVER\HttpFileUpload\Upload\"+ folder+@"\" + UpFileName;  // 파일의 경로입니다.
                //    //MessageBox.Show(strSaveTO, "Error");
                //    string sss = web.UploadFile(Filestream, strSaveTO);

                //}

              
            }
            catch
            {
                throw new Exception("Some attachments are open. Please close the opened files.");
                
            }
            finally
            {
                fs.Close();
                fs.Dispose();
                //if (_webClient != null)
                //    _webClient.Dispose();
            }

        }

        /// <summary>
        /// File Download Custom folder
        /// </summary>
        /// <param name="KeyPath"></param>
        /// <param name="FileName"></param>
        /// <param name="DefaultPath"></param>
        //private static bool _FileDownResult = false;
        public static bool Download(string KeyPath, string FileName, bool DefaultPath)
        {
            try
            {
                GetServerInfo();

                string downloadPath = string.Empty;
                if (DefaultPath)
                {
                    downloadPath = Application.StartupPath + @"\\TempDownload";
                    string[] split = KeyPath.Split('\\');
                    string _path = string.Empty;
                    foreach (string s in split)
                    {
                        _path = _path + "\\" + s;
                        DirectoryInfo di = new DirectoryInfo(downloadPath + _path + "/");
                        if (di.Exists == false) di.Create();
                        //폴더에 권한주기
                    }
                    downloadPath = downloadPath + "\\" + KeyPath;
                    SetDirectorySecurity(downloadPath);

                }
                else
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.ShowDialog();
                    downloadPath = dialog.SelectedPath;
                }
                WebClient webClient = new WebClient();
                webClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                string FileURI = "http://" + _fileServerIP + "/ISYWebService/HttpFile/Upload/" + KeyPath + "/" + FileName;

                char[] delimiterChars = { '/' };
                string[] words = FileURI.Split(delimiterChars);

                webClient.DownloadFileAsync(new Uri(FileURI), downloadPath + "\\" + FileName);
                webClient.Dispose();
                return true;
            }
            catch
            {
                return false;
                //MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// File Download default folder
        /// </summary>
        /// <param name="KeyPath"></param>
        /// <param name="FileName"></param>
        public static bool Download(string KeyPath, string FileName)
        {
            return Download(KeyPath, FileName, true);
        }


        private static string _FileDownResult = string.Empty;
        private static string downloadPath = string.Empty;
        private static string downloaFile = string.Empty;

        /// <summary>
        /// File Download Custom folder
        /// </summary>
        /// <param name="KeyPath"></param>
        /// <param name="FileName"></param>
        /// <param name="DefaultPath"></param>
        public static void SaveRun(string KeyPath, string FileName)
        {
            try
            {
                GetServerInfo();

                downloadPath = string.Empty;
                downloaFile = FileName;

                downloadPath = Application.StartupPath + @"\\TempDownload\\" + KeyPath;

                DirectoryInfo di = new DirectoryInfo(downloadPath);
                if (di.Exists == false) di.Create();
                //폴더에 권한주기
                SetDirectorySecurity(downloadPath);

                //WebClient webClient = new WebClient();
                //webClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                //webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;
                ////webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                ////webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                //string FileURI = "http://" + FileServerIP + "/ISYWebService/HttpFile/Upload/" + KeyPath + "/" + downloaFile;

                //char[] delimiterChars = { '/' };
                //string[] words = FileURI.Split(delimiterChars);

                //_FileDownResult = "";
                //webClient.DownloadFile(new Uri(FileURI), downloadPath + "\\" + downloaFile);

                //int i = 0;

                //while(_FileDownResult=="")
                //{
                //    i++;
                //}


                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                    // nastaveni ze webClient ma pouzit Windows Authentication
                    webClient.UseDefaultCredentials = true;
                    // spusteni stahovani

                    string FileURI = "http://" + _fileServerIP + "/ISYWebService/HttpFile/Upload/" + KeyPath + "/" + downloaFile;

             
                    webClient.DownloadFile(new Uri(FileURI), downloadPath + "\\" + downloaFile);
                }

                Process.Start(downloadPath + "\\" + downloaFile);


                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
                //MessageBox.Show(ex.ToString());
            }
        }

        private static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                request.Timeout = 30000; // miliseconds
                
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        private static void WebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _FileDownResult = "N";
                // there was an error, do something
            }
            else if (!e.Cancelled)
            {
                _FileDownResult = "Y";
            }
        }

        private static void SetDirectorySecurity(string linePath)
        {
            DirectorySecurity dSecurity = Directory.GetAccessControl(linePath);
            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            dSecurity.AddAccessRule(new FileSystemAccessRule(everyone,
                                                                FileSystemRights.FullControl,
                                                                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                                                PropagationFlags.None,
                                                                AccessControlType.Allow));
            Directory.SetAccessControl(linePath, dSecurity);
        }

        private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
        }

        private static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            //_FileDownResult = true;
            //MessageBox.Show("Download completed!");
        }

        public static void PdfCheck()
        {
            string Path_32 = @"C:\Program Files (x86)\wkhtmltopdf\bin";
            string Path_64 = @"C:\Program Files\wkhtmltopdf\bin";

            DirectoryInfo di1 = new DirectoryInfo(Path_64);
            DirectoryInfo di2 = new DirectoryInfo(Path_32);

            if (di1.Exists == false && di2.Exists == false)
            {
                
                ProcessStartInfo _processStartInfo = new ProcessStartInfo();
                _processStartInfo.WorkingDirectory = Application.StartupPath;
                _processStartInfo.FileName = @"wkhtmltox.exe";
                _processStartInfo.Arguments = " /S ";
                _processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process myProcess = Process.Start(_processStartInfo);
                myProcess.WaitForExit();


            }
        }

        public static void HtmlToPdf(string slipNo, StringBuilder bodyHtml)
        {
            PdfCheck();

            string FilePath = Application.StartupPath + @"\wkhtmltopdf.exe";
            string HtmlPath = Application.StartupPath + @"\Temp.html";

            //string HtmlPath = Application.StartupPath + @"\TempDownload\\GRW\" + str_No_APP + ".html";
            //string PdfPath = Application.StartupPath + @"\TempDownload\\GRW\" + str_No_APP + ".pdf";

            File.WriteAllText(HtmlPath, bodyHtml.ToString());

           

            ProcessStartInfo _processStartInfo = new ProcessStartInfo();
            _processStartInfo.WorkingDirectory = Application.StartupPath;
            _processStartInfo.FileName = @"wkhtmltopdf.exe";
            _processStartInfo.Arguments = "Temp.html " + slipNo + ".pdf";
            _processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process myProcess = Process.Start(_processStartInfo);
            myProcess.WaitForExit();

            //var process = Process.Start(FilePath + " Temp.html "+ str_No_APP+".pdf");

            //process.WaitForExit();

            Process.Start(Application.StartupPath + @"\"+ slipNo + ".pdf");

        }


        #region file binary control
        private static byte[] ReadBinaryFile(string fullpath)
        {

            string str_Dir = System.IO.Path.GetDirectoryName(fullpath);
            FileStream fileStream = null;

            if (!Directory.Exists(str_Dir))
            {
                return new byte[0];
            }


            if (File.Exists(fullpath))
            {
                try
                {
                    ///Open and read a file 
                    fileStream = File.OpenRead(fullpath);

                    return ConvertStreamToByteBuffer(fileStream);
                }
                catch
                {
                    return new byte[0];
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Dispose();
                    }
                }
            }
            else
            {
                return new byte[0];
            }
        }


        private static byte[] ConvertStreamToByteBuffer(System.IO.Stream theStream)
        {
           // int b1;

            try
            {

                //System.IO.MemoryStream tempStream = new System.IO.MemoryStream();


                byte[] output = new byte[theStream.Length];

                byte[] fileData = null;

                using (BinaryReader binaryReader = new BinaryReader(theStream))
                {
                    fileData = binaryReader.ReadBytes((int)theStream.Length);
                }

                return fileData;
                //theStream.CopyTo(tempStream);
                //return tempStream.ToArray();

                //while ((b1 = theStream.ReadByte()) != -1)
                //{
                //    tempStream.WriteByte(((byte)b1));
                //}

                //return tempStream.ToArray();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        #endregion

    }
}