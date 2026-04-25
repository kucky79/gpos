using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bifrost.FileTransfer.Client
{
    public enum ProcessState { Waiting, Running, Completed, Failed }


    /// <summary>
    /// Inherits FMService proxy
    /// to upload, download, handle files at server
    /// through webservice
    ///		
    /// 
    /// </summary>
    public sealed class FileManager : Bifrost.FileTransfer.Client.IFMService.FMService
    {

        #region Declarations
        private BackgroundWorker ThreadWorker = new BackgroundWorker();

        private int _bufferSize = 100000;
        private int _sBytes = 0;
        private int _fileSize = 0;
        private int _sentBytes
        {

            set
            {
                this._sBytes = value;
                RaiseProgressChangedEvent(new ProgressChangedEventArgs(this._sBytes, this._fileSize));
            }
            get { return this._sBytes; }
        }

        private bool _overwrite = false;
        private bool _isUploading = false;

        private string _instanceId = string.Empty;
        private string _filename = string.Empty;
        private string _localFileName = string.Empty;
        private bool _reserveFileTime = false;


        private DateTime _fileCreationTime;
        private DateTime _fileLastWriteTime;
        private DateTime _fileLastAccessTime;

        private string _SubFolder = string.Empty;

        private System.Net.CookieContainer _cookies = new System.Net.CookieContainer();
        private Bifrost.FileTransfer.Client.ProcessState _state = Bifrost.FileTransfer.Client.ProcessState.Waiting;

        public event Bifrost.FileTransfer.Client.ProgressChangedEventHandler ProgressChanged;
        private void RaiseProgressChangedEvent(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        public event System.EventHandler StateChanged;
        private void RaiseStateChangedEvent(EventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        public int BufferSize { get { return this._bufferSize; } set { this._bufferSize = value; } }
        public int SentBytes { get { return this._sentBytes; } }
        public int Filesize { get { return this._fileSize; } }

        public string InstanceId { get { return this._instanceId; } }

        public Bifrost.FileTransfer.Client.ProcessState State
        {
            get { return this._state; }
            set
            {
                this._state = value;
                RaiseStateChangedEvent(new System.EventArgs());
            }
        }

        #endregion

        #region Constructors

        public FileManager()
        {

            this.CookieContainer = this._cookies;
            this.ThreadWorker.DoWork += new DoWorkEventHandler(threadWorker_DoWork);
            this.ThreadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(threadWorker_ProgressChanged);
            this.ThreadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadWorker_RunWorkerCompleted);
            this.ThreadWorker.WorkerReportsProgress = true;
        }

        public void InitializeInstance()
        {
            if (this._instanceId == string.Empty)
                this._instanceId = this.Initialization();
        }

        #endregion

        #region Upload

        /// <summary>
        /// Start the Uploading process
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="SubFolder"></param>
        public void StartUpload(string fileName, Bifrost.SubFolder subFolder)
        {
            this._filename = System.IO.Path.GetFileName(fileName);
            this._SubFolder = subFolder.ToString();

            System.IO.FileInfo File = new System.IO.FileInfo(fileName);
            this._fileSize = (int)File.Length;
            this._sentBytes = 0;

            this.State = ProcessState.Running;
            this.InitializeInstance();
            this.BeginUpload(this._instanceId, this._filename, this._SubFolder);
            this._isUploading = true;

            ThreadWorker.RunWorkerAsync();
        }

        public void StartUpload(string fileName, string _subFolder)
        {
            this._filename = System.IO.Path.GetFileName(fileName);
            this._SubFolder = _subFolder;

            System.IO.FileInfo File = new System.IO.FileInfo(fileName);
            this._fileSize = (int)File.Length;
            this._sentBytes = 0;

            this.State = ProcessState.Running;
            this.InitializeInstance();
            this.BeginUpload(this._instanceId, this._filename, this._SubFolder);
            this._isUploading = true;

            ThreadWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Process the upload process, using Background worker component
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int ProcessUpload(System.ComponentModel.BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e)
        {
            System.IO.FileStream FileStream = new System.IO.FileStream(this._filename, System.IO.FileMode.Open);

            byte[] buffer = new byte[_bufferSize];

            // read
            int bytesRead = 0;
            int iSentBytes = 0;
            ProcessState processState = ProcessState.Running;
            try
            {
                do
                {
                    FileStream.Seek(iSentBytes, System.IO.SeekOrigin.Begin);
                    bytesRead = FileStream.Read(buffer, 0, _bufferSize);
                    if (bytesRead == 0)
                    {
                        // Finished
                        worker.ReportProgress(100, iSentBytes);
                        processState = ProcessState.Completed;
                        break;
                    }
                    else
                    {
                        // send				
                        UploadReturnCodes retCode = (UploadReturnCodes)this.AppendChunk(this._instanceId, buffer, iSentBytes, bytesRead);
                        if (retCode != UploadReturnCodes.Success)
                        {
                            processState = ProcessState.Failed;
                            break;
                        }

                        // Update status
                        iSentBytes += bytesRead;
                        worker.ReportProgress(Convert.ToInt32(iSentBytes * 100 / this._fileSize), iSentBytes);
                    }

                } while (true);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                processState = ProcessState.Failed;
            }
            finally
            {
                FileStream.Close();
            }

            return (int)processState;
        }

        #endregion

        #region Upload with no progress info

        /// <summary>
        /// Upload file to server
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="targetServerFolder"></param>
        /// <param name="subPath"></param>
        public bool StartUpload(string fileName, string targetServerFolder, string subPath)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }

            bool bUploaded = false;
            try
            {
                this.State = ProcessState.Running;
                this.InitializeInstance();
                this.BeginUpload(this._instanceId, fileName, this._SubFolder);
                this._isUploading = true;
                byte[] buffer = File.ReadAllBytes(fileName);
                bUploaded = this.UploadFileOnly(targetServerFolder, subPath, buffer);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                try
                {
                    this.EndUpload(this._instanceId);
                }
                catch
                {
                }
            }
            return bUploaded;
        }

        #endregion

        #region Download

        /// <summary>
        /// Start the Downloading process
        /// </summary>
        /// <param name="fileName">GUID File</param>
        /// <param name="SubFolder"></param>
        public void StartDownload(string fileName, string localFileName, bool overwrite)
        {
            this._filename = fileName; //System.IO.Path.GetFileName(fileName);
            this._overwrite = overwrite;
            this._localFileName = localFileName;

            this.State = ProcessState.Running;
            this.InitializeInstance();

            string[] fileInfos = this.BeginDownload(this._instanceId, this._filename);
            this._fileSize = Convert.ToInt32(fileInfos[0]);
            if (fileInfos.Length == 4)
            {
                this._fileCreationTime = DateTime.Parse(fileInfos[1]);
                this._fileLastWriteTime = DateTime.Parse(fileInfos[2]);
                this._fileLastAccessTime = DateTime.Parse(fileInfos[3]);
            }

            this._sentBytes = 0; // Received bytes
            this._isUploading = false;

            ThreadWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Process the upload process, using Background worker component
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int ProcessDownload(System.ComponentModel.BackgroundWorker worker,
            System.ComponentModel.DoWorkEventArgs e)
        {
            System.IO.FileInfo File = new System.IO.FileInfo(this._localFileName);
            if (!File.Exists || this._overwrite)
                System.IO.File.Create(this._localFileName).Close();

            // Open file to save
            System.IO.FileStream fs = new System.IO.FileStream(this._localFileName, System.IO.FileMode.Append);

            // read
            int iReceivedBytes = 0;
            ProcessState processState = ProcessState.Running;
            try
            {
                byte[] buffer = new byte[_bufferSize];

                do
                {
                    // read
                    int bytesRead = this.GetChunk(this._instanceId, iReceivedBytes, this._bufferSize, this._fileSize, out buffer);
                    if (bytesRead == 0)
                    {
                        worker.ReportProgress(100, iReceivedBytes);
                        processState = ProcessState.Completed;
                        break;
                    }

                    // save to local file
                    fs.Write(buffer, 0, bytesRead);
                    iReceivedBytes += bytesRead;

                    // report progress
                    worker.ReportProgress(Convert.ToInt32(iReceivedBytes * 100 / this._fileSize), iReceivedBytes);
                } while (true);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                processState = ProcessState.Failed;
            }
            finally
            {
                fs.Close();

                /// 
                /// update file time
                /// 
                if (_reserveFileTime)
                {
                    FileInfo fi = new FileInfo(this._localFileName);
                    fi.CreationTimeUtc = _fileCreationTime;
                    fi.LastWriteTimeUtc = _fileLastWriteTime;
                    fi.LastAccessTimeUtc = _fileLastAccessTime;
                }
            }

            return (int)processState;
        }

        #endregion

        #region File Up/Down processing

        private void threadWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this._sentBytes = (int)e.UserState;
        }

        private void threadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if ((ProcessState)e.Result == ProcessState.Completed)
                {
                    if (this._isUploading)
                    {
                        this.EndUpload(this._instanceId);
                    }
                    else
                    {
                        this.EndDownload(this._instanceId);
                    }
                }
            }
            catch
            {
            }

            this.State = (ProcessState)e.Result;
        }

        private void threadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = sender as System.ComponentModel.BackgroundWorker;
            if (_isUploading)
                e.Result = ProcessUpload(worker, e);
            else
                e.Result = ProcessDownload(worker, e);
        }

        /// <summary>
        /// Reset current instance id
        /// </summary>
        public void KillInstance()
        {
            this._instanceId = string.Empty;
        }

        #endregion

        #region Upload Sequencely

        /// <summary>
        /// Start upload processing
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="subSystemType"></param>
        public void StartUploadSync(string fileName, SubFolder subFolder)
        {
            this._filename = System.IO.Path.GetFileName(fileName);
            this._SubFolder = subFolder.ToString();

            System.IO.FileInfo File = new System.IO.FileInfo(fileName);
            this._fileSize = (int)File.Length;
            this._sentBytes = 0;

            this.State = ProcessState.Running;
            this.InitializeInstance();
            this.BeginUpload(this._instanceId, this._filename, this._SubFolder);
            this._isUploading = true;

            System.IO.FileStream FileStream = new System.IO.FileStream(this._filename, System.IO.FileMode.Open);
            byte[] buffer = new byte[_bufferSize];

            // read
            int bytesRead = 0;
            int iSentBytes = 0;
            ProcessState processState = ProcessState.Running;
            try
            {
                do
                {
                    FileStream.Seek(iSentBytes, System.IO.SeekOrigin.Begin);
                    bytesRead = FileStream.Read(buffer, 0, _bufferSize);
                    if (bytesRead == 0)
                    {
                        // Finished
                        this._sentBytes = iSentBytes;
                        processState = ProcessState.Completed;
                        break;
                    }
                    else
                    {
                        // send				
                        UploadReturnCodes retCode = (UploadReturnCodes)this.AppendChunk(this._instanceId, buffer, iSentBytes, bytesRead);
                        if (retCode != UploadReturnCodes.Success)
                        {
                            processState = ProcessState.Failed;
                            break;
                        }

                        // Update status
                        iSentBytes += bytesRead;
                        this._sentBytes = iSentBytes;
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                processState = ProcessState.Failed;
            }
            finally
            {
                FileStream.Close();
            }

            if (processState == ProcessState.Completed)
            {
                try
                {
                    this.EndUpload(this._instanceId);
                }
                catch
                {
                }
            }
            this.State = processState;
        }

        public void StartUploadSync(string fileName, string subFolder)
        {
            this._filename = System.IO.Path.GetFileName(fileName);
            this._SubFolder = subFolder;

            System.IO.FileInfo File = new System.IO.FileInfo(fileName);
            this._fileSize = (int)File.Length;
            this._sentBytes = 0;

            this.State = ProcessState.Running;
            this.InitializeInstance();
            this.BeginUpload(this._instanceId, this._filename, this._SubFolder);
            this._isUploading = true;

            System.IO.FileStream FileStream = new System.IO.FileStream(this._filename, System.IO.FileMode.Open);
            byte[] buffer = new byte[_bufferSize];

            // read
            int bytesRead = 0;
            int iSentBytes = 0;
            ProcessState processState = ProcessState.Running;
            try
            {
                do
                {
                    FileStream.Seek(iSentBytes, System.IO.SeekOrigin.Begin);
                    bytesRead = FileStream.Read(buffer, 0, _bufferSize);
                    if (bytesRead == 0)
                    {
                        // Finished
                        this._sentBytes = iSentBytes;
                        processState = ProcessState.Completed;
                        break;
                    }
                    else
                    {
                        // send				
                        UploadReturnCodes retCode = (UploadReturnCodes)this.AppendChunk(this._instanceId, buffer, iSentBytes, bytesRead);
                        if (retCode != UploadReturnCodes.Success)
                        {
                            processState = ProcessState.Failed;
                            break;
                        }

                        // Update status
                        iSentBytes += bytesRead;
                        this._sentBytes = iSentBytes;
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                processState = ProcessState.Failed;
            }
            finally
            {
                FileStream.Close();
            }

            if (processState == ProcessState.Completed)
            {
                try
                {
                    this.EndUpload(this._instanceId);
                }
                catch
                {
                }
            }
            this.State = processState;
        }

        #endregion

        #region Download sync

        /// <summary>
        /// Start downloading sync
        /// </summary>		
        public void StartDownloadSync(string fileName, string localFileName, bool overwrite)
        {
            StartDownloadSync(fileName, localFileName, overwrite, false);
        }

        /// <summary>
        /// Start downloading sync
        /// </summary>
        public void StartDownloadSync(string fileName, string localFileName, bool overwrite, bool reserveFileTime)
        {
            this._filename = fileName; //System.IO.Path.GetFileName(fileName);
            this._overwrite = overwrite;
            this._localFileName = localFileName;
            this._reserveFileTime = reserveFileTime;

            this.State = ProcessState.Running;
            this.InitializeInstance();
            string[] fileInfos = this.BeginDownload(this._instanceId, this._filename);
            this._fileSize = Convert.ToInt32(fileInfos[0]);
            if (fileInfos.Length == 4)
            {
                this._fileCreationTime = DateTime.Parse(fileInfos[1]);
                this._fileLastWriteTime = DateTime.Parse(fileInfos[2]);
                this._fileLastAccessTime = DateTime.Parse(fileInfos[3]);
            }
            this._sentBytes = 0; // Received bytes
            this._isUploading = false;

            System.IO.FileInfo File = new System.IO.FileInfo(this._localFileName);
            if (!File.Exists || this._overwrite)
                System.IO.File.Create(this._localFileName).Close();

            // Open file to save
            System.IO.FileStream fs = new System.IO.FileStream(this._localFileName, System.IO.FileMode.Append);

            // read
            int iReceivedBytes = 0;
            ProcessState processState = ProcessState.Running;
            try
            {
                byte[] buffer = new byte[_bufferSize];

                do
                {
                    // read
                    int bytesRead = this.GetChunk(this._instanceId, iReceivedBytes, this._bufferSize, this._fileSize, out buffer);
                    if (bytesRead == 0)
                    {
                        this._sentBytes = iReceivedBytes;
                        processState = ProcessState.Completed;
                        break;
                    }

                    // save to local file
                    fs.Write(buffer, 0, bytesRead);
                    iReceivedBytes += bytesRead;

                    // report progress
                    //worker.ReportProgress(Convert.ToInt32(iReceivedBytes * 100 / this._fileSize), iReceivedBytes);
                    this._sentBytes = iReceivedBytes;
                } while (true);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                processState = ProcessState.Failed;
            }
            finally
            {
                fs.Close();

                /// 
                /// update file time
                /// 
                if (_reserveFileTime)
                {
                    FileInfo fi = new FileInfo(this._localFileName);
                    fi.CreationTimeUtc = _fileCreationTime;
                    fi.LastWriteTimeUtc = _fileLastWriteTime;
                    fi.LastAccessTimeUtc = _fileLastAccessTime;
                }
            }

            if (processState == ProcessState.Completed)
                this.EndDownload(this._instanceId);

            this.State = processState;
        }


        #endregion

    }

}

namespace Bifrost.FileTransfer
{

    public enum UploadReturnCodes : int
    {

        FileAlreadyExists = -5,
        IOError = -4,
        TransferCorrupted = -3,
        CannotFindTempFile = -2,
        InstanceNotFound = -1,
        Success = 0

    }

    public enum DownloadReturnCodes : int
    {

        IOError = -4,
        TransferCorrupted = -3,
        CannotFindFile = -2,
        InstanceNotFound = -1,
        Success = 0

    }

}