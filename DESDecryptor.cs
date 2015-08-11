using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApplicationReadFromDatabaseDES
{
    class DESDecryptor
    {
        public static string Decryptor(string TextToDecrypt, string strKey)
        {
            byte[] EncryptedBytes = Convert.FromBase64String(TextToDecrypt);
            string strIV = "01234567";
            //Setup the AES provider for decrypting.            
            TripleDESCryptoServiceProvider desProvider = new TripleDESCryptoServiceProvider();
            //aesProvider.Key = System.Text.Encoding.ASCII.GetBytes(strKey);
            //aesProvider.IV = System.Text.Encoding.ASCII.GetBytes(strIV);
            desProvider.BlockSize = 64;
            desProvider.KeySize = 128;
            //My key and iv that i have used in openssl
            desProvider.Key = stringToByte(strKey, 16);
            desProvider.IV = string2byte(strIV);
            desProvider.Padding = PaddingMode.PKCS7;
            desProvider.Mode = CipherMode.CBC;


            ICryptoTransform cryptoTransform = desProvider.CreateDecryptor(desProvider.Key, desProvider.IV);
            byte[] DecryptedBytes = cryptoTransform.TransformFinalBlock(EncryptedBytes, 0, EncryptedBytes.Length);
            return System.Text.Encoding.ASCII.GetString(DecryptedBytes);
        }

        public static byte[] string2byte(string newString)
        {
            char[] CharArray = newString.ToCharArray();
            byte[] ByteArray = new byte[CharArray.Length];

            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
            }
            return ByteArray;
        }

        public static byte[] stringToByte(string newString, int charLength)
        {
            char[] CharArray = newString.ToCharArray();
            byte[] ByteArray = new byte[charLength];
            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
            }
            return ByteArray;
        }
    }
}
