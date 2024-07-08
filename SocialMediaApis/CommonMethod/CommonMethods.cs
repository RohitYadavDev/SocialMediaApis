using Microsoft.Extensions.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace SocialMediaApis.CommonMethod
{
    public class CommonMethods
    {
        public static readonly string key = "A6Cd67sfas";
        public IConfiguration Configuration { get; }
        public CommonMethods(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static string Encryptword(string clearText)
        {
            string encryptionKey = "AESHAPPIFOC76hdua";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}
