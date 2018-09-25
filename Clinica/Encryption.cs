using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Clinica
{
    public static class Encryption
    {
        private static readonly byte[] KEY =
        System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encryptKey"].ToString());

        private static readonly byte[] IV = KEY;

        private static DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        public static Stream GetEncryptedStream(FileStream pFs)
        {
            CryptoStream cryptoStream = new CryptoStream(pFs, des.CreateEncryptor(KEY, IV), CryptoStreamMode.Write);

            return cryptoStream;
        }

        public static Stream GetDecryptedStream(FileStream pFs)
        {
            CryptoStream cryptoStream = new CryptoStream(pFs, des.CreateDecryptor(KEY, IV), CryptoStreamMode.Read);

            return cryptoStream;
        }
    }
}