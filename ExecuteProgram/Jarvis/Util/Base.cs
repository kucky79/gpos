using System;
using System.Xml;
using System.Text;
using System.Runtime.InteropServices;

namespace Bifrost
{
	/// <summary>
	/// Base에 대한 요약 설명입니다.
	/// </summary>
	/// 
	public class Base : SystemBase
	{		
		/// <summary>
		/// Machine.confing파일 설정정보 가져오기
		/// </summary>
		/// <param name="confingString">설정키값</param>
		/// <returns>결과스트링</returns>
		public static string GetConfigString(string confingString)
		{
			try
			{
				//후에 Machine.confing파일외이 다른걸로 String을 처리할때를 대비해서 ..
				if (System.Configuration.ConfigurationManager.AppSettings[confingString] == null)
					return "";
				else
					return System.Configuration.ConfigurationManager.AppSettings[confingString].ToString();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		
		#region 암복호화
		/// <summary>
		///   암호화 할때 Key와 IV(Initialization Vector)를 하드코딩하여 간단한 암호화를 수행
		///   하는 함수, 알고리즘은 DES알고리즘을 사용한다.
		///   리턴되는 값은 Base64인코딩된 값이다.
		///   암호화 해제할때는 SimpleDecrypt()를 사용한다.
		/// </summary>
		/// <param name="stringSource">소스스트링</param>
		/// <returns>인크립션스트링</returns>
		public static string BifrostEncrypt(string stringSource)
		{   
			EncryptionAlgorithm algorithm = EncryptionAlgorithm.Des;
			byte[] cipherText = null;
			byte[] key = Encoding.UTF8.GetBytes("44EEFF12");
			byte[] IV = Encoding.UTF8.GetBytes("6789ABCD");
			try
			{
				Encryptor enc = new Encryptor(algorithm);    
				enc.IV = IV;
				cipherText = enc.Encrypt(Encoding.UTF8.GetBytes(stringSource), key);    
				return Convert.ToBase64String(cipherText);
			}
			catch(System.Exception ex)
			{
				throw new System.Exception("Error BifrostEncrypt()-" + ex.Message);
			}
		}

		/// <summary>
		/// 간단한 암호화 해제를 수, SimpleEncrypt()통해 암호화 되어 있는 문자열을 원래의 문자열로 바꿔준다.
		/// </summary>
		/// <param name="stringSource">소스스트링</param>
		/// <returns>디크립션스트링</returns>
		public static string BifrostDecrypt(string stringSource)
		{
			EncryptionAlgorithm algorithm = EncryptionAlgorithm.Des;
			byte[] key = Encoding.UTF8.GetBytes("44EEFF12");
			byte[] IV = Encoding.UTF8.GetBytes("6789ABCD");
			try
			{
				Decryptor dec = new Decryptor(algorithm);   
				dec.IV = IV;
				byte[] encryptText = Convert.FromBase64String(stringSource);
				byte[] plainText = dec.Decrypt(encryptText, key);
				return Encoding.UTF8.GetString(plainText);
			}
			catch(System.Exception ex)
			{
				throw new System.Exception("Error BifrostDecrypt()-" + ex.Message);
			}
		}
		#endregion
	}
}
