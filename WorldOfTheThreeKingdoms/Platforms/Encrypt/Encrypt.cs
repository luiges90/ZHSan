using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SanguoSeason.Encryption
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string dataToEncrypt, string password)
        {
            var data = Encoding.UTF8.GetBytes(dataToEncrypt);
            var key = Encoding.UTF8.GetBytes("seasonsalts");
            //string dataEncryptStr = Des_DataEncrypt.TripleDesDeEncryptUseIvKey(null, key, data); 
           //string dataEncryptStr = Convert.ToBase64String(dataEncryptBytes,0,dataEncryptBytes.Length); 
                        
            var bytes = Encrypt(dataToEncrypt, password, "seasonsalts", Encoding.UTF8);
            return Convert.ToBase64String(bytes.ToArray());
        }

        public static string Decrypt(string dataToDecrypt, string password)
        {
            var bytes = Convert.FromBase64String(dataToDecrypt);
            return Decrypt(bytes, password, "seasonsalts", Encoding.UTF8);
        }

        public static byte[] Encrypt(string dataToEncrypt, string password, string salt, Encoding encoding)
        {
            AesManaged aes = null;
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;

            try
            {
                //Generate a Key based on a Password, Salt and HMACSHA1 pseudo-random number generator 
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, encoding.GetBytes(salt));

                //Create AES algorithm with 256 bit key and 128-bit block size 
                aes = new AesManaged();
                aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                rfc2898.Reset(); //needed for WinRT compatibility
                aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                //Create Memory and Crypto Streams 
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

                //Encrypt Data 
                byte[] data = encoding.GetBytes(dataToEncrypt);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                //Return encrypted data 
                return memoryStream.ToArray();

            }
            catch (Exception eEncrypt)
            {
                return null;
            }
            finally
            {
                if (cryptoStream != null)
                    cryptoStream.Close();

                if (memoryStream != null)
                    memoryStream.Close();

                if (aes != null)
                    aes.Clear();

            }
        }

        public static string Decrypt(byte[] dataToDecrypt, string password, string salt, Encoding encoding)
        {
            AesManaged aes = null;
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            string decryptedText = "";
            try
            {
                //Generate a Key based on a Password, Salt and HMACSHA1 pseudo-random number generator 
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, encoding.GetBytes(salt));

                //Create AES algorithm with 256 bit key and 128-bit block size 
                aes = new AesManaged();
                aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                rfc2898.Reset(); //neede to be WinRT compatible
                aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                //Create Memory and Crypto Streams 
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

                //Decrypt Data 
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();

                //Return Decrypted String 
                byte[] decryptBytes = memoryStream.ToArray();
                decryptedText = encoding.GetString(decryptBytes, 0, decryptBytes.Length);
            }
            catch (Exception eDecrypt)
            {

            }
            finally
            {
                if (cryptoStream != null)
                    cryptoStream.Close();

                if (memoryStream != null)
                    memoryStream.Close();

                if (aes != null)
                    aes.Clear();
            }
            return decryptedText;
        }

    }


    //public class Des_DataEncrypt 
    //{ 
    //    /// <summary> 
    //    /// TripleDes  Data Encrypt With Ot Encrypt Key Operator 
    //    /// </summary> 
    //    /// <param name="sourceContent">Source Need to TripleDes Encrpt Data</param> 
    //    /// <returns>Encrypt Data Byte[] String</returns> 
    //    public static byte[] TripleDesEncryptWithOutKey(string sourceContent) 
    //    { 
    //        if (string.IsNullOrEmpty(sourceContent)) 
    //            return null; 


    //        var toEncryptSourceStr = Encoding.UTF8.GetBytes(sourceContent); 
    //        TripleDESCryptoServiceProvider tripleDesEncryptProvider = new TripleDESCryptoServiceProvider(); 
    //        ICryptoTransform encryptTransform=tripleDesEncryptProvider.CreateEncryptor(); 
    //        byte[] encryptToBytes = encryptTransform.TransformFinalBlock(toEncryptSourceStr, 0, toEncryptSourceStr.Length); 


    //        return encryptToBytes; 
    //    } 


    //    /// <summary> 
    //    /// TripleDes Data DeEncrypt With Out Encrypt Key Operator 
    //    /// </summary> 
    //    /// <param name="encryptBytes">Encrypt Byte Array</param> 
    //    /// <returns>DeEncrypt SourceContent String</returns> 
    //    public static string TripleDesDeEncryptWithOutKey(byte[] encryptBytes) 
    //    { 
    //        if (encryptBytes == null || encryptBytes.Length <= 0) 
    //            return string.Empty; 


    //        TripleDESCryptoServiceProvider tripleDesProvider = new TripleDESCryptoServiceProvider(); 
    //        ICryptoTransform deEncryptTransform = tripleDesProvider.CreateDecryptor(); 
    //        var deEncryptBytes = deEncryptTransform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length); 
    //        var deEncryptFormatStr = Encoding.UTF8.GetString(deEncryptBytes, 0, deEncryptBytes.Length); 


    //        return deEncryptFormatStr; 
    //    }


    //    /// <summary> 
    //    /// TripleDes Data Encrypt Use IVKey Operator 
    //    /// </summary> 
    //    /// <param name="sourceContent">Source Content</param> 
    //    /// <param name="encryptKey">Encrypt Key</param> 
    //    /// <returns>Encrypt Bytes  Array</returns> 
    //    public static byte[] TripleDesEncryptUseIvKey(string sourceContent, byte[] encryptIVKey)
    //    {
    //        if (string.IsNullOrEmpty(sourceContent) || encryptIVKey == null || encryptIVKey.Length <= 0)
    //            return null;


    //        var toEncryptSourceStr = Encoding.UTF8.GetBytes(sourceContent);
    //        TripleDESCryptoServiceProvider tripleDesProvider = new TripleDESCryptoServiceProvider();


    //        //No Seting Pading 


    //        var key = tripleDesProvider.Key; //Save Key 
    //        //IsolatedStorageCommon.IsolatedStorageSettingHelper.AddIsolateStorageObj("EncryptKey", key);
    //        ICryptoTransform encryptTransform = tripleDesProvider.CreateEncryptor(key, encryptIVKey);
    //        var encryptBytes = encryptTransform.TransformFinalBlock(toEncryptSourceStr, 0, toEncryptSourceStr.Length);


    //        return encryptBytes;
    //    }

    //    /// <summary> 
    //    /// Triple Des DeEncrypt Operator Use IvKey 
    //    /// </summary> 
    //    /// <param name="encryptKey">Encrypt key can be null</param> 
    //    /// <param name="ivKey">Iv</param> 
    //    /// <param name="encryptBytes">EncryptBytes</param> 
    //    /// <returns>Return String </returns> 
    //    public static string TripleDesDeEncryptUseIvKey(byte[] encryptKey, byte[] ivKey, byte[] encryptBytes)
    //    {
    //        if (encryptBytes == null || encryptBytes.Length <= 0)
    //            return string.Empty;


    //        TripleDESCryptoServiceProvider tripleDesProvider = new TripleDESCryptoServiceProvider();


    //        //if (encryptKey == null)
    //        //    encryptKey = IsolatedStorageCommon.IsolatedStorageSettingHelper.GetIsolateStorageByObj("EncryptKey") as byte[];
    //        ICryptoTransform deEncryptTransform = tripleDesProvider.CreateDecryptor(encryptKey, ivKey);
    //        var DecryptBytes = deEncryptTransform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length);
    //        string unDecryptFomatStr = Encoding.UTF8.GetString(DecryptBytes, 0, DecryptBytes.Length);


    //        return unDecryptFomatStr;
    //    } 
    // } 


}
