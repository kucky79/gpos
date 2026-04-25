using System;
using System.Data;

namespace Bifrost
{
	/// <summary>
	/// TypeManager에 대한 요약 설명입니다.
	/// </summary>
	public class TypeManager
	{
		/// <summary>
		/// TypeManager의 생성자
		/// </summary>
		public TypeManager()
		{
			//
			// TODO: 여기에 생성자 논리를 추가합니다.
			//
		}

		/// <summary>
		/// <b>SqlDbType추출</b><br/>
		/// </summary>
		/// <param name="Value">값</param>
		/// <param name="oType">SqlDbType</param>
		/// <returns>결과스트링</returns>
		public static string GetValue(object Value, SqlDbType oType)
		{
			string strValue = "";
			if ( Value == DBNull.Value )
			{
				return string.Empty;
			}

			try
			{
				strValue = Value.ToString();
				switch (oType)
				{
					case SqlDbType.BigInt :
					case SqlDbType.Int :
					case SqlDbType.SmallInt :
					case SqlDbType.TinyInt :
						if ( CheckDigit(strValue) != true )
							return string.Empty;
						else
							return strValue;
					default:
						return strValue;
				}
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// <b>Digit체크</b><br/>
		/// </summary>
		/// <param name="Value">값</param>
		/// <returns>결과 Bool값</returns>
		private static bool CheckDigit(string Value)
		{
			try
			{
				for ( int i = 0 ; i < Value.Length ; i++ )
				{
					if ( Char.IsDigit(Value[i]) != true )
						return false;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return true;
		}
	}
}
