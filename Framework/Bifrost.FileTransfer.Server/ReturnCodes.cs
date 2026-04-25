 using System;

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