using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Runtime.Serialization;

namespace Bifrost.Data
{
	[Serializable()]
	public class MenuAccessData
	{
		public MenuAccessData()
		{
		}

		private bool _AllowCreate = true;

		/// <summary>
		/// 신규 
		/// </summary>
		public bool AllowCreate
		{
			get { return _AllowCreate; }
			set { _AllowCreate = value; }
		}

		private bool _AllowRead = true;

		/// <summary>
		/// 조회
		/// </summary>
		public bool AllowRead
		{
			get { return _AllowRead; }
			set { _AllowRead = value; }
		}

		private bool _AllowUpdate = true;

		/// <summary>
		/// 수정
		/// </summary>
		public bool AllowUpdate
		{
			get { return _AllowUpdate; }
			set { _AllowUpdate = value; }
		}

		private bool _AllowDelete = true;

		/// <summary>
		/// 삭제 
		/// </summary>
		public bool AllowDelete
		{
			get { return _AllowDelete; }
			set { _AllowDelete = value; }
		}

		private bool _AllowPrint = true;

		/// <summary>
		///	출력
		/// </summary>
		public bool AllowPrint
		{
			get { return _AllowPrint; }
			set { _AllowPrint = value; }
		}

		private bool _AllowExportExcel = true;

		/// <summary>
		/// Excel export 
		/// </summary>
		public bool AllowExportExcel
		{
			get { return _AllowExportExcel; }
			set { _AllowExportExcel = value; }
		}
	

		public MenuAccessData(bool read, bool create, bool insert, bool update, bool delete, bool excel, bool print)
		{
            this._AllowCreate = create;
            this._AllowRead = read;
			this._AllowUpdate = update;
			this._AllowDelete = delete;
			this._AllowExportExcel = excel;
			this._AllowPrint = print;
		}

		//Deserialization constructor.
		public MenuAccessData(SerializationInfo info, StreamingContext ctxt)
		{
			//Get the values from info and assign them to the appropriate properties
			this._AllowCreate = (bool)info.GetValue("AllowCreate", typeof(bool));
			this._AllowRead = (bool)info.GetValue("AllowRead", typeof(bool));
			this._AllowUpdate = (bool)info.GetValue("AllowUpdate", typeof(bool));
			this._AllowDelete = (bool)info.GetValue("AllowDelete", typeof(bool));
			this._AllowExportExcel = (bool)info.GetValue("AllowExportExcel", typeof(bool));
			this._AllowPrint = (bool)info.GetValue("AllowPrint", typeof(bool));
		}
		        
		//Serialization function.
		public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("AllowRead", this._AllowRead);
            info.AddValue("AllowCreate", this._AllowCreate);
            info.AddValue("AllowInsert", this._AllowCreate);
            info.AddValue("AllowUpdate", this._AllowUpdate);
            info.AddValue("AllowDelete", this._AllowDelete);
            info.AddValue("AllowExportExcel", this._AllowExportExcel);
            info.AddValue("AllowPrint", this._AllowPrint);

		}
	}
}
