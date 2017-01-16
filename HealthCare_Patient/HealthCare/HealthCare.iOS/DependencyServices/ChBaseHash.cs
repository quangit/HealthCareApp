using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HealthCare.DependencyServices;
using HealthCare.iOS.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(ChBaseHash))]

namespace HealthCare.iOS.DependencyServices
{
    public class ChBaseHash : IChBaseHash
    {
        public string Hmac(string key, string header)
        {
            var hash = Convert.ToBase64String(SignContent(Encoding.UTF8.GetBytes(header), key));
            return hash;
        }

        public string InfoHash(string info)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(info);

            byte[] results = null;
            using (System.Security.Cryptography.SHA256 shaManaged = new SHA256Managed())
            {
                results = shaManaged.ComputeHash(bytes, 0, bytes.Length);
            }

            string returnValue = Convert.ToBase64String(results);

            return returnValue;
        }

        public async Task<string> SigHash(string content)
        {
            byte[] buffer;
            var assembly = this.GetType().GetTypeInfo().Assembly;
            using (Stream s = assembly.GetManifestResourceStream("HealthCare.iOS.HealthCareCHBase.pfx"))
            {
                long length = s.Length;
                buffer = new byte[length];
                s.Read(buffer, 0, (int)length);
            }

            X509Certificate2 file = new X509Certificate2(buffer, new SecureString(), X509KeyStorageFlags.MachineKeySet);
            var rsa = (RSACryptoServiceProvider)file.PrivateKey;
            var rs = rsa.SignData(Encoding.UTF8.GetBytes(content), "SHA1");
            var data = Convert.ToBase64String(rs);
            return data;
        }

        private byte[] SignContent(byte[] content, string key)
        {
            byte[] bytes = Convert.FromBase64String(key);
            byte[] results = null;
            using (HMACSHA256 hmacSha256 = new HMACSHA256(bytes))
            {
                results = hmacSha256.ComputeHash(content);
            }
            return results;
        }
    }
}