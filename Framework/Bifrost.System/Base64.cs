using System;

namespace Bifrost
{
	/// <summary>
	/// Base64에 대한 요약 설명입니다.
	/// </summary>
	public class Base64
	{
		/// <summary>
		/// Base64 인코딩을 합니다
		/// </summary>
		/// <param name="src">입력문자열</param>
		/// <param name="enc">Encoding Object</param>
		/// <returns></returns>
		public static string Base64Encode( string src, System.Text.Encoding enc)
		{
			byte[] arr = enc.GetBytes(src);
			return Convert.ToBase64String(arr);
		}

		/// <summary>
		/// Base64 디코딩을 합니다
		/// </summary>
		/// <param name="src">입력문자열</param>
		/// <param name="enc">Encoding Object</param>
		/// <returns></returns>
		public static string Base64Decode(string src, System.Text.Encoding enc)
		{
			byte[] arr = Convert.FromBase64String(src);
			return enc.GetString(arr);
		}
	}
}
