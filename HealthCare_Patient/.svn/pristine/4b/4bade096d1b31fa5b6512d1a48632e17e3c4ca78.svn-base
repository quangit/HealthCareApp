using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using HealthCare.DependencyServices;
using HealthCare.WinPhone.DependencyServices;
using HealthCare.WinPhone.Renderer;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

[assembly: Xamarin.Forms.Dependency(typeof(ChBaseHash))]

namespace HealthCare.WinPhone.DependencyServices
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

            var assembly = Assembly.GetExecutingAssembly();
            using (Stream s = assembly.GetManifestResourceStream("HealthCare.WinPhone.HealthCareCHBase.pfx"))
            {
                Pkcs12Store ks = new Pkcs12Store(s, new char[] { });
                foreach (string al in ks.Aliases)
                {
                    var key = ks.GetKey(al).Key;

                    RsaPrivateCrtKeyParameters bcKeyParms = ((RsaPrivateCrtKeyParameters)key);
                    RSAParameters parms = new RSAParameters
                    {
                        Modulus = bcKeyParms.Modulus.ToByteArrayUnsigned(),
                        P = bcKeyParms.P.ToByteArrayUnsigned(),
                        Q = bcKeyParms.Q.ToByteArrayUnsigned(),
                        DP = bcKeyParms.DP.ToByteArrayUnsigned(),
                        DQ = bcKeyParms.DQ.ToByteArrayUnsigned(),
                        InverseQ = bcKeyParms.QInv.ToByteArrayUnsigned(),
                        D = bcKeyParms.Exponent.ToByteArrayUnsigned(),
                        Exponent = bcKeyParms.PublicExponent.ToByteArrayUnsigned()
                    };

                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.ImportParameters(parms);
                    var temp = Encoding.UTF8.GetBytes(content);
                    try
                    {

                        var rs = rsa.SignData(temp, "SHA1");
                        var data = Convert.ToBase64String((rs));
                        return data;
                    }
                    catch
                    {
                        HttpClient client = new HttpClient();
                        object tempData = new
                        {
                            Content = content
                        };
                        try
                        {
                            string jsonData = JsonConvert.SerializeObject(tempData);
                            var data = await client.PostAsync("http://chbase.bacsi24x7.vn/API/SigHash",
                                new StringContent(jsonData, Encoding.UTF8,
                                    "application/json"));
                            return (await data.Content.ReadAsStringAsync()).Replace("\"", "");
                        }
                        catch
                        {
                            return "";
                        }
                    }

                }
            }
            return "";
        }

    
        private byte[] SignContent(byte[] content, string key)
        {
            byte[] bytes = Base64.Decode(key);
            byte[] results = null;
            using (HMACSHA256 hmacSha256 = new HMACSHA256(bytes))
            {
                results = hmacSha256.ComputeHash(content);
            }
            return results;
        }

    }
}