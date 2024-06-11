using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBusHos9_GJYB
{
    /// <summary>
    ///  使用高级加密标准 (AES) 算法的加密解密
    /// </summary>
    public class AESHelper
    {
        const string Key = "9465CEFB719D6C00294CBD9ZDP6D2D4J";

        public static string Encrypt<T>(T t)
        {
            string s = JsonConvert.SerializeObject(t);
            return EncryptDefault(s);
        }

        public static T Decrypt<T>(string strjson)
        {
            string s = DecryptDefault(strjson);
            return JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        ///使用默认秘钥进行 AES加密
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <returns></returns>
        public static string EncryptDefault(string encryptStr)
        {
            return Encrypt(encryptStr, Key);
        }
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string encryptStr, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptStr);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            //  rDel.IV = Encoding.UTF8.GetBytes(IV);
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="toEncryptArray">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] EncryptDefault(byte[] toEncryptArray)
        {
            return Encrypt(toEncryptArray, Key);
        }
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="toEncryptArray">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] toEncryptArray, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            //  rDel.IV = Encoding.UTF8.GetBytes(IV);
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return resultArray;
        }
        /// <summary>
        /// 使用默认秘钥进行AES解密
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <returns></returns>
        public static string DecryptDefault(string decryptStr)
        {
            return Decrypt(decryptStr, Key);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Decrypt(string decryptStr, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(decryptStr);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            //  rDel.IV = Encoding.UTF8.GetBytes(IV);
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="toEncryptArray">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] DecryptDefault(byte[] toEncryptArray)
        {
            return Decrypt(toEncryptArray, Key);
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="toEncryptArray">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] toEncryptArray, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            //  rDel.IV = Encoding.UTF8.GetBytes(IV);
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return resultArray;
        }
        private static string IV = "ABCDEF0123456789";

        /// <summary>
        /// AES加密算法 （与java一致）
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <returns>返回加密后的密文字节数组</returns>
        public static string EncryptJava(string plainText)
        {
            return Convert.ToBase64String(EncryptStringToBytes_Aes(plainText, Encoding.UTF8.GetBytes(Key), Encoding.UTF8.GetBytes(IV)));
        }

        /// <summary>
        /// AES加密算法 （与java一致）
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回加密后的密文字节数组</returns>
        public static string EncryptJava(string plainText, string strKey)
        {
            return Convert.ToBase64String(EncryptStringToBytes_Aes(plainText, Encoding.UTF8.GetBytes(strKey), Encoding.UTF8.GetBytes(IV)));
        }



        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Key = Key;
                // aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;

        }


        public static byte[] EncryptJava(byte[] toEncryptArray, string key)
        {
            return EncryptStringToBytes_Aes(toEncryptArray, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(IV));
        }
        static byte[] EncryptStringToBytes_Aes(byte[] toEncryptArray, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (toEncryptArray == null || toEncryptArray.Length <= 0)
                throw new ArgumentNullException("toEncryptArray");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Key = Key;
                // aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(toEncryptArray, 0, toEncryptArray.Length);
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        /// <summary>
        /// AES解密（与java一致）
        /// </summary>
        /// <param name="cipherText">密文字节数组</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DecryptJava(string pherText)
        {
            return DecryptStringFromBytes_Aes(Convert.FromBase64String(pherText), Encoding.UTF8.GetBytes(Key), Encoding.UTF8.GetBytes(IV));
        }
        /// <summary>
        /// AES解密（与java一致）
        /// </summary>
        /// <param name="cipherText">密文字节数组</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DecryptJava(string pherText, string strKey)
        {
            return DecryptStringFromBytes_Aes(Convert.FromBase64String(pherText), Encoding.UTF8.GetBytes(strKey), Encoding.UTF8.GetBytes(IV));
        }
        public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            string plaintext = null;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Key = Key;
                // aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;

        }


        /// <summary>
        /// MD5签名
        /// </summary>
        /// <param name="encryptString">要签名的数据</param>
        /// <param name="encryptKey">签名密钥,最长32位</param>
        /// <returns></returns>
        public static string SignMD5(string encryptString, string encryptKey)
        {
            return (encryptString + encryptKey).Md5Hash();
        }

        /// <summary>
        /// MD5签名
        /// </summary>
        /// <param name="encryptString">要签名的数据</param>
        /// <returns></returns>
        public static string SignMD5(string encryptString)
        {
            return (encryptString + Key).Md5Hash();
        }

    }
    public static class DateTimeExtend
    {  /// <summary>
       ///获取32位Md5, UTF8格式
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        public static string Md5Hash(this string input)
        {
            MD5 md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
