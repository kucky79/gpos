using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Bifrost
{
    /// <summary>
    /// Encryptor Class
    /// </summary>
    public class Encryptor
    {
        private EncryptTransformer transformer;
        private byte[] initVec;
        private byte[] encKey;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="algId"></param>
        public Encryptor(EncryptionAlgorithm algId)
        {
            transformer = new EncryptTransformer(algId);
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] IV
        {
            get { return initVec; }
            set { initVec = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Key
        {
            get { return encKey; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesData"></param>
        /// <param name="bytesKey"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] bytesData, byte[] bytesKey)
        {
            //Set up the stream that will hold the encrypted data.
            MemoryStream memStreamEncryptedData = new MemoryStream();
            transformer.IV = initVec;
            ICryptoTransform transform = transformer.GetCryptoServiceProvider(bytesKey);
            CryptoStream encStream = new CryptoStream(memStreamEncryptedData, transform, CryptoStreamMode.Write);
            try
            {
                //Encrypt the data, write it to the memory stream.
                encStream.Write(bytesData, 0, bytesData.Length);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Error while writing encrypted data to the stream: \n" + ex.Message);
            }
            //Set the IV and key for the client to retrieve
            encKey = transformer.Key;
            initVec = transformer.IV;
            encStream.FlushFinalBlock();
            encStream.Close();
            //Send the data back.
            return memStreamEncryptedData.ToArray();
        }//end Encrypt

    }

    /// <summary>
    /// Decryptor Class
    /// </summary>
    public class Decryptor
    {
        private DecryptTransformer transformer;
        private byte[] initVec;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="algId"></param>
        public Decryptor(EncryptionAlgorithm algId)
        {
            transformer = new DecryptTransformer(algId);
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] IV
        {
            set { initVec = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesData"></param>
        /// <param name="bytesKey"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] bytesData, byte[] bytesKey)
        {
            //Set up the memory stream for the decrypted data.
            MemoryStream memStreamDecryptedData = new MemoryStream();
            //Pass in the initialization vector.
            transformer.IV = initVec;
            ICryptoTransform transform = transformer.GetCryptoServiceProvider(bytesKey);
            CryptoStream decStream = new CryptoStream(memStreamDecryptedData, transform, CryptoStreamMode.Write);
            try
            {
                decStream.Write(bytesData, 0, bytesData.Length);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Error while writing encrypted data to the stream: \n" + ex.Message);
            }
            decStream.FlushFinalBlock();
            decStream.Close();
            // Send the data back.
            return memStreamDecryptedData.ToArray();
        } //end Decrypt
    }

    /// <summary>
    /// 암호화
    /// </summary>
    public enum EncryptionAlgorithm
    {
        /// <summary>
        /// DES
        /// </summary>
        Des = 1,
        /// <summary>
        /// RC2
        /// </summary>
        Rc2,
        /// <summary>
        /// Rijindael
        /// </summary>
        Rijndael,
        /// <summary>
        /// Tirple DES
        /// </summary>
        TripleDes
    };

    /// <summary>
    /// 
    /// </summary>
    internal class EncryptTransformer
    {
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;
        private byte[] encKey;

        internal EncryptTransformer(EncryptionAlgorithm algId)
        {
            //Save the algorithm being used.
            algorithmID = algId;
        }

        internal byte[] IV
        {
            get { return initVec; }
            set { initVec = value; }
        }
        internal byte[] Key
        {
            get { return encKey; }
        }

        internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
        {
            try
            {
                // Pick the provider.
                switch (algorithmID)
                {
                    case EncryptionAlgorithm.Des:
                        {
                            DES des = new DESCryptoServiceProvider();
                            des.Mode = CipherMode.CBC;
                            // See if a key was provided
                            if (null == bytesKey)
                            {
                                encKey = des.Key;
                            }
                            else
                            {
                                des.Key = bytesKey;
                                encKey = des.Key;
                            }
                            // See if the client provided an initialization vector
                            if (null == initVec)
                            { // Have the algorithm create one
                                initVec = des.IV;
                            }
                            else
                            { //No, give it to the algorithm
                                des.IV = initVec;
                            }
                            return des.CreateEncryptor();
                        }
                    case EncryptionAlgorithm.TripleDes:
                        {
                            TripleDES des3 = new TripleDESCryptoServiceProvider();
                            des3.Mode = CipherMode.CBC;
                            // See if a key was provided
                            if (null == bytesKey)
                            {
                                encKey = des3.Key;
                            }
                            else
                            {
                                des3.Key = bytesKey;
                                encKey = des3.Key;
                            }
                            // See if the client provided an IV
                            if (null == initVec)
                            { //Yes, have the alg create one
                                initVec = des3.IV;
                            }
                            else
                            { //No, give it to the alg.
                                des3.IV = initVec;
                            }
                            return des3.CreateEncryptor();
                        }
                    case EncryptionAlgorithm.Rc2:
                        {
                            RC2 rc2 = new RC2CryptoServiceProvider();
                            rc2.Mode = CipherMode.CBC;
                            // Test to see if a key was provided
                            if (null == bytesKey)
                            {
                                encKey = rc2.Key;
                            }
                            else
                            {
                                rc2.Key = bytesKey;
                                encKey = rc2.Key;
                            }
                            // See if the client provided an IV
                            if (null == initVec)
                            { //Yes, have the alg create one
                                initVec = rc2.IV;
                            }
                            else
                            { //No, give it to the alg.
                                rc2.IV = initVec;
                            }
                            return rc2.CreateEncryptor();
                        }
                    case EncryptionAlgorithm.Rijndael:
                        {
                            Rijndael rijndael = new RijndaelManaged();
                            rijndael.Mode = CipherMode.CBC;
                            // Test to see if a key was provided
                            if (null == bytesKey)
                            {
                                encKey = rijndael.Key;
                            }
                            else
                            {
                                rijndael.Key = bytesKey;
                                encKey = rijndael.Key;
                            }
                            // See if the client provided an IV
                            if (null == initVec)
                            { //Yes, have the alg create one
                                initVec = rijndael.IV;
                            }
                            else
                            { //No, give it to the alg.
                                rijndael.IV = initVec;
                            }
                            return rijndael.CreateEncryptor();
                        }
                    default:
                        {
                            throw new CryptographicException("Algorithm ID '" + algorithmID + "' not supported.");
                        }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }

    internal class DecryptTransformer
    {
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;

        internal DecryptTransformer(EncryptionAlgorithm deCryptId)
        {
            algorithmID = deCryptId;
        }

        internal byte[] IV
        {
            set { initVec = value; }
        }

        internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
        {
            try
            {
                // Pick the provider.
                switch (algorithmID)
                {
                    case EncryptionAlgorithm.Des:
                        {
                            DES des = new DESCryptoServiceProvider();
                            des.Mode = CipherMode.CBC;
                            des.Key = bytesKey;
                            des.IV = initVec;
                            return des.CreateDecryptor();
                        }
                    case EncryptionAlgorithm.TripleDes:
                        {
                            TripleDES des3 = new TripleDESCryptoServiceProvider();
                            des3.Mode = CipherMode.CBC;
                            return des3.CreateDecryptor(bytesKey, initVec);
                        }
                    case EncryptionAlgorithm.Rc2:
                        {
                            RC2 rc2 = new RC2CryptoServiceProvider();
                            rc2.Mode = CipherMode.CBC;
                            return rc2.CreateDecryptor(bytesKey, initVec);
                        }
                    case EncryptionAlgorithm.Rijndael:
                        {
                            Rijndael rijndael = new RijndaelManaged();
                            rijndael.Mode = CipherMode.CBC;
                            return rijndael.CreateDecryptor(bytesKey, initVec);
                        }
                    default:
                        {
                            throw new CryptographicException("Algorithm ID '" + algorithmID + "' not supported.");
                        }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        } //end GetCryptoServiceProvider

    }

    public class SimpleEncryptor
    {
        // Key 값은 무조건 8자리여야한다.
        private byte[] Key { get; set; }

        // 암호화/복호화 메서드
        public string Encryptor(DesType type, string input)
        {
            var des = new DESCryptoServiceProvider()
            {
                Key = Key,
                IV = Key
            };

            var ms = new MemoryStream();

            // 익명 타입으로 transform / data 정의
            var property = new
            {
                transform = type.Equals(DesType.Encrypt) ? des.CreateEncryptor() : des.CreateDecryptor(),
                data = type.Equals(DesType.Encrypt) ? Encoding.UTF8.GetBytes(input.ToCharArray()) : Convert.FromBase64String(input)
            };

            var cryStream = new CryptoStream(ms, property.transform, CryptoStreamMode.Write);
            var data = property.data;

            cryStream.Write(data, 0, data.Length);
            cryStream.FlushFinalBlock();

            return type.Equals(DesType.Encrypt) ? Convert.ToBase64String(ms.ToArray()) : Encoding.UTF8.GetString(ms.GetBuffer());
        }

        // 생성자
        public SimpleEncryptor(string key)
        {
            Key = ASCIIEncoding.ASCII.GetBytes(key);
        }
    }

    public enum DesType
    {
        Encrypt = 0,    // 암호화
        Decrypt = 1     // 복호화
    }
}
