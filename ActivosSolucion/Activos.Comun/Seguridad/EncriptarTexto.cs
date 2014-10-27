using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Activos.Comun.Seguridad
{
    public class EncriptarTexto : Base
    {
        public static string Desencriptar(string Texto)
        {
            byte[] inputByteArray = new byte[Texto.Length];
            string mSEncryptionKey = "act387&9e0";
            byte[] mIV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] mkey = new byte[8];

            try
            {
                mkey = Encoding.UTF8.GetBytes(mSEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                inputByteArray = Convert.FromBase64String(Texto);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(mkey, mIV), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;

                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Encriptar(string Texto)
        {
            string mSEncryptionKey = "act387&9e0";
            byte[] mIV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] mkey = new byte[8];

            try
            {
                mkey = Encoding.UTF8.GetBytes(mSEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                Byte[] inputByteArray = Encoding.UTF8.GetBytes(Texto);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(mkey, mIV), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
