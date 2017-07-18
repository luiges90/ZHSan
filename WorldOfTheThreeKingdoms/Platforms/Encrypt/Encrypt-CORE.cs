using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace SanguoSeason.Encryption
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string dataToEncrypt, string password)
        {
            var bytes = Encrypt(dataToEncrypt, password, "seasonsalts");
            return Convert.ToBase64String(bytes.ToArray());
        }

        public static string Decrypt(string dataToDecrypt, string password)
        {
            var bytes = Convert.FromBase64String(dataToDecrypt);
            return Decrypt(bytes, password, "seasonsalts");
        }

        //public static string Encrypt(string plainText, string pw, string salt)
        //{
        //    string[] algorithmNames = { "AES_CBC", "AES_ECB", "AES_CBC_PKCS7", "AES_ECB_PKCS7", "DES_CBC", "DES_ECB", "3DES_CBC", "3DES_ECB", "3DES_CBC_PKCS7", "3DES_ECB_PKCS7", "RC2_CBC", "RC2_ECB", "RC4" };

        //    string result = "";

        //    foreach (var algorithmName in algorithmNames)
        //    {
        //        uint keySize = 128;
        //        if (algorithmName.StartsWith("AES")) // AES 算法密钥长度 128 位
        //            keySize = 128;
        //        else if (algorithmName.StartsWith("DES")) // DES 算法密钥长度 64 位（56 位的密钥加上 8 位奇偶校验位）
        //            keySize = 64;
        //        else if (algorithmName.StartsWith("3DES")) // 3DES 算法密钥长度 192 位（3 重 DES）
        //            keySize = 192;
        //        else if (algorithmName.StartsWith("RC2")) // RC2 算法密钥长度可变
        //            keySize = 128;
        //        else if (algorithmName.StartsWith("RC4")) // RC4 算法密钥长度可变
        //            keySize = 128;

        //        IBuffer buffer; // 原文
        //        IBuffer encrypted; // 加密后
        //        IBuffer decrypted; // 解密后
        //        IBuffer iv = null; // 向量（CBC 模式）

        //        // 根据算法名称实例化一个对称算法提供程序
        //        SymmetricKeyAlgorithmProvider symmetricAlgorithm = SymmetricKeyAlgorithmProvider.OpenAlgorithm(algorithmName);

        //        // 创建一个随机密钥 key
        //        IBuffer key = CryptographicBuffer.GenerateRandom(keySize / 8);

        //        // 根据 key 生成 CryptographicKey 对象
        //        CryptographicKey cryptoKey = symmetricAlgorithm.CreateSymmetricKey(key);

        //        // 如果是 CBC 模式则随机生成一个向量
        //        if (algorithmName.Contains("CBC"))
        //            iv = CryptographicBuffer.GenerateRandom(symmetricAlgorithm.BlockLength);
        //        CryptographicKey cryptoKey2 = symmetricAlgorithm.CreateSymmetricKey(key);
        //        // 将需要加密的数据转换为 IBuffer 类型
        //        buffer = CryptographicBuffer.ConvertStringToBinary(plainText, BinaryStringEncoding.Utf8);
        //        string hex = "";
        //        try
        //        {
        //            // 加密数据
        //            encrypted = CryptographicEngine.Encrypt(cryptoKey, buffer, iv);
        //            hex = CryptographicBuffer.EncodeToBase64String(encrypted); //.EncodeToHexString(encrypted);

        //            result += (algorithmName + ":" + hex);

        //            encrypted = CryptographicBuffer.DecodeFromBase64String(hex); // .EncodeToHexString(
        //            // 解密数据
        //            decrypted = Windows.Security.Cryptography.Core.CryptographicEngine.Decrypt(cryptoKey2, encrypted, iv);
        //            string dec = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, decrypted);

        //            result += (" dec:" + dec + Environment.NewLine);
        //        }
        //        catch (Exception ex)
        //        {
        //        //    lblMsg.Text += ex.ToString();
        //        //    lblMsg.Text += Environment.NewLine;
        //        //    return;
        //        }                

        //    }
        //    return result;
        //}

        //public static string Decrypt(string encryptedData, string pw, string salt)
        //{
        //    foreach (var algorithmName in algorithmNames)
        //    {
        //        uint keySize = 128;
        //        if (algorithmName.StartsWith("AES")) // AES 算法密钥长度 128 位
        //            keySize = 128;
        //        else if (algorithmName.StartsWith("DES")) // DES 算法密钥长度 64 位（56 位的密钥加上 8 位奇偶校验位）
        //            keySize = 64;
        //        else if (algorithmName.StartsWith("3DES")) // 3DES 算法密钥长度 192 位（3 重 DES）
        //            keySize = 192;
        //        else if (algorithmName.StartsWith("RC2")) // RC2 算法密钥长度可变
        //            keySize = 128;
        //        else if (algorithmName.StartsWith("RC4")) // RC4 算法密钥长度可变
        //            keySize = 128;

        //        IBuffer buffer; // 原文
        //        IBuffer encrypted; // 加密后
        //        IBuffer decrypted; // 解密后
        //        IBuffer iv = null; // 向量（CBC 模式）

        //        // 根据算法名称实例化一个对称算法提供程序
        //        SymmetricKeyAlgorithmProvider symmetricAlgorithm = SymmetricKeyAlgorithmProvider.OpenAlgorithm(algorithmName);

        //        // 创建一个随机密钥 key
        //        IBuffer key = CryptographicBuffer.GenerateRandom(keySize / 8);

        //        // 根据 key 生成 CryptographicKey 对象
        //        CryptographicKey cryptoKey = symmetricAlgorithm.CreateSymmetricKey(key);

        //        // 如果是 CBC 模式则随机生成一个向量
        //        if (algorithmName.Contains("CBC"))
        //            iv = CryptographicBuffer.GenerateRandom(symmetricAlgorithm.BlockLength);

        //        // 本示例的原文为 16 个字节，是为了正常演示无填充时的加密
        //        // 什么是填充：比如 aes 要求数据长度必须是 16 的倍数，如果不是则需要通过指定的填充模式来补全数据
        //        string plainText = encryptedData;

        //        CryptographicKey cryptoKey2 = symmetricAlgorithm.CreateSymmetricKey(key);
        //        try
        //        {
        //            // 解密数据
        //            decrypted = Windows.Security.Cryptography.Core.CryptographicEngine.Decrypt(cryptoKey2, encrypted, iv);
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMsg.Text += ex.ToString();
        //            lblMsg.Text += Environment.NewLine;
        //            return;
        //        }
        //    }

        //    // 解密后的结果
        //    lblMsg.Text += algorithmName + " decrypted: " + CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, decrypted);
        //    lblMsg.Text += Environment.NewLine;
        //    lblMsg.Text += Environment.NewLine;
        //}

        public static byte[] Encrypt(string plainText, string pw, string salt)
        {
            IBuffer pwBuffer = CryptographicBuffer.ConvertStringToBinary(pw, BinaryStringEncoding.Utf8);
            IBuffer saltBuffer = CryptographicBuffer.ConvertStringToBinary(salt, BinaryStringEncoding.Utf8); //.Utf16LE);
            IBuffer plainBuffer = CryptographicBuffer.ConvertStringToBinary(plainText, BinaryStringEncoding.Utf8); //.Utf16LE);

            // Derive key material for password size 32 bytes for AES256 algorithm
            KeyDerivationAlgorithmProvider keyDerivationProvider = Windows.Security.Cryptography.Core.KeyDerivationAlgorithmProvider.OpenAlgorithm("PBKDF2_SHA1");
            // using salt and 1000 iterations
            KeyDerivationParameters pbkdf2Parms = KeyDerivationParameters.BuildForPbkdf2(saltBuffer, 1000);

            // create a key based on original key and derivation parmaters
            CryptographicKey keyOriginal = keyDerivationProvider.CreateKey(pwBuffer);
            IBuffer keyMaterial = CryptographicEngine.DeriveKeyMaterial(keyOriginal, pbkdf2Parms, 32);
            CryptographicKey derivedPwKey = keyDerivationProvider.CreateKey(pwBuffer);

            // derive buffer to be used for encryption salt from derived password key 
            IBuffer saltMaterial = CryptographicEngine.DeriveKeyMaterial(derivedPwKey, pbkdf2Parms, 16);

            // display the buffers – because KeyDerivationProvider always gets cleared after each use, they are very similar unforunately
            string keyMaterialString = CryptographicBuffer.EncodeToBase64String(keyMaterial);
            string saltMaterialString = CryptographicBuffer.EncodeToBase64String(saltMaterial);

            SymmetricKeyAlgorithmProvider symProvider = SymmetricKeyAlgorithmProvider.OpenAlgorithm("AES_CBC_PKCS7");
            // create symmetric key from derived password key
            CryptographicKey symmKey = symProvider.CreateSymmetricKey(keyMaterial);

            // encrypt data buffer using symmetric key and derived salt material
            IBuffer resultBuffer = CryptographicEngine.Encrypt(symmKey, plainBuffer, saltMaterial);
            byte[] result;
            CryptographicBuffer.CopyToByteArray(resultBuffer, out result);

            return result;
        }

        public static string Decrypt(byte[] encryptedData, string pw, string salt)
        {
            IBuffer pwBuffer = CryptographicBuffer.ConvertStringToBinary(pw, BinaryStringEncoding.Utf8);
            IBuffer saltBuffer = CryptographicBuffer.ConvertStringToBinary(salt, BinaryStringEncoding.Utf8); //.Utf16LE);
            IBuffer cipherBuffer = CryptographicBuffer.CreateFromByteArray(encryptedData);

            // Derive key material for password size 32 bytes for AES256 algorithm
            KeyDerivationAlgorithmProvider keyDerivationProvider = Windows.Security.Cryptography.Core.KeyDerivationAlgorithmProvider.OpenAlgorithm("PBKDF2_SHA1");
            // using salt and 1000 iterations
            KeyDerivationParameters pbkdf2Parms = KeyDerivationParameters.BuildForPbkdf2(saltBuffer, 1000);

            // create a key based on original key and derivation parmaters
            CryptographicKey keyOriginal = keyDerivationProvider.CreateKey(pwBuffer);
            IBuffer keyMaterial = CryptographicEngine.DeriveKeyMaterial(keyOriginal, pbkdf2Parms, 32);
            CryptographicKey derivedPwKey = keyDerivationProvider.CreateKey(pwBuffer);

            // derive buffer to be used for encryption salt from derived password key 
            IBuffer saltMaterial = CryptographicEngine.DeriveKeyMaterial(derivedPwKey, pbkdf2Parms, 16);

            // display the keys – because KeyDerivationProvider always gets cleared after each use, they are very similar unforunately
            string keyMaterialString = CryptographicBuffer.EncodeToBase64String(keyMaterial);
            string saltMaterialString = CryptographicBuffer.EncodeToBase64String(saltMaterial);

            SymmetricKeyAlgorithmProvider symProvider = SymmetricKeyAlgorithmProvider.OpenAlgorithm("AES_CBC_PKCS7");
            // create symmetric key from derived password material
            CryptographicKey symmKey = symProvider.CreateSymmetricKey(keyMaterial);

            // encrypt data buffer using symmetric key and derived salt material
            IBuffer resultBuffer = CryptographicEngine.Decrypt(symmKey, cipherBuffer, saltMaterial);
            string result = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, resultBuffer); // BinaryStringEncoding.Utf16LE, resultBuffer);
            return result;
        }

    }
}
