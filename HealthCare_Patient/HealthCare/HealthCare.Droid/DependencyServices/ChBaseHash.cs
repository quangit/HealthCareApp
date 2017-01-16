using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using HealthCare.DependencyServices;
using HealthCare.Droid.DependencyServices;
using Java.Security;
using Javax.Crypto;
using Javax.Crypto.Spec;

[assembly: Xamarin.Forms.Dependency(typeof(ChBaseHash))]

namespace HealthCare.Droid.DependencyServices
{
    public class ChBaseHash : IChBaseHash
    {
        public string Hmac(string key, string header)
        {
            var content = Encoding.UTF8.GetBytes(header);
            var hash = Base64.EncodeToString(SignContent(content, key), Base64Flags.Default).Trim();
            return hash;
        }

        public string InfoHash(string info)
        {
            MessageDigest md = MessageDigest.GetInstance("SHA-256");
            md.Update(Encoding.UTF8.GetBytes(info));
            byte[] result = md.Digest();
            var temp = Base64.EncodeToString(result, Base64Flags.Default).Trim();
            return temp;
        }

        public async Task<string> SigHash(string content)
        {
            byte[] buffer;
            var assembly = this.GetType().GetTypeInfo().Assembly;
            using (Stream s = assembly.GetManifestResourceStream("HealthCare.Droid.HealthCareCHBase.pfx"))
            {
                long length = s.Length;
                buffer = new byte[length];
                s.Read(buffer, 0, (int)length);
            }
            
            X509Certificate2 file = new X509Certificate2(buffer, new SecureString(), X509KeyStorageFlags.MachineKeySet);
            var rsa = (RSACryptoServiceProvider)file.PrivateKey;
            var rs = rsa.SignData(Encoding.UTF8.GetBytes(content), "SHA1");
            var data = Base64.EncodeToString(rs, Base64Flags.Default).Trim();
            return data;
        }

        private byte[] SignContent(byte[] content, string key)
        {
            try
            {
                SecretKeySpec sks = new SecretKeySpec(Base64.Decode(key, Base64Flags.Default), "HmacSHA256");
                Mac mac = Mac.GetInstance("HmacSHA256");
                mac.Init(sks);

                var temp = mac.DoFinal(content);
                return temp;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}