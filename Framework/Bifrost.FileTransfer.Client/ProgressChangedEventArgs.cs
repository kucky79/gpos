using System;

namespace Bifrost.FileTransfer.Client
{

	public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
	public class ProgressChangedEventArgs : System.EventArgs {

		#region ## Declarations ##

		private long _max = 0;
		private long _value = 0;
		public long Max { get { return this._max; }}
		public long Value { get { return this._value; }}
        
		#endregion

		#region ## Constructors ##

		public ProgressChangedEventArgs(long value, long max) { this._max = max; this._value = value; }

		#endregion

	}

}