using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using HealthCare.DependencyServices;
[assembly: Xamarin.Forms.Dependency(typeof(HealthCare.WinPhone.DependencyServices.SHA256))]
namespace HealthCare.WinPhone.DependencyServices
{
   public class SHA256: ISHA256
    {
        public string GenerateSha256(string txt)
       {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(txt), 0, Encoding.UTF8.GetByteCount(txt));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
       }

        public static implicit operator SHA256(SHA256Managed v)
        {
            throw new NotImplementedException();
        }
    }
}
