using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace LumiaLoyalty.Core.Services
{
    public interface ITripleDESCryptoService
    {
        string Key { get; set; }
        string Encrypt(string toEncrypt, bool useHashing);
    }

    public class TripleDESCryptoService : ITripleDESCryptoService
    {
        private byte[] keyDES3;
        public string Key { get; set; }

        public string Encrypt(string toEncrypt, bool useHashing)
        {
            var engine = new DesEdeEngine();
            keyDES3 = Encoding.UTF8.GetBytes(Key);
            byte[] ptBytes = Encoding.UTF8.GetBytes(toEncrypt);
            KeyParameter keyparam = new DesEdeParameters(keyDES3);


            var bufferedCipher = CipherUtilities.GetCipher("DESede/ECB/PKCS7PADDING");
            bufferedCipher.Init(true, keyparam);
            var resultArray = bufferedCipher.DoFinal(ptBytes);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// generating a Triple DES key
        /// </summary>
        public void InitDES3Key()
        {

            CipherKeyGenerator cipherKeyGenerator = new CipherKeyGenerator();
            cipherKeyGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 192));
            //192 specifies the size of key in bits i.e 24 bytes

            keyDES3 = cipherKeyGenerator.GenerateKey();
            //BigInteger bigInteger = new BigInteger(keyDES3);
            //Console.WriteLine(bigInteger.ToString(16));
        }

        /// <summary>
        /// Encryption using DES3 algorithm in ECB mode 
        /// </summary>
        /// <param name="message">Input message bytes</param>
        /// <returns>Encrypted message bytes</returns>
        public byte[] EncryptDES3(byte[] message)
        {
            DesEdeEngine desedeEngine = new DesEdeEngine();
            BufferedBlockCipher bufferedCipher = new BufferedBlockCipher(desedeEngine);

            // Create the KeyParameter for the DES3 key generated. 
            KeyParameter keyparam = ParameterUtilities.CreateKeyParameter("DESEDE", keyDES3);
            //byte[] output = new byte[bufferedCipher.GetOutputSize(message.Length)];
            bufferedCipher.Init(true, keyparam);
            try
            {
                return bufferedCipher.DoFinal(message);
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        /// <summary>
        /// Encryption using DES3 algorithm in CBC mode 
        /// </summary>
        /// <param name="message">Input message bytes</param>
        /// <returns>Encrypted message bytes</returns>
        public byte[] EncryptDES3_CBC(byte[] message)
        {
            DesEdeEngine desedeEngine = new DesEdeEngine();
            BufferedBlockCipher bufferedCipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(desedeEngine));
            KeyParameter keyparam = ParameterUtilities.CreateKeyParameter("DESEDE", keyDES3);
            byte[] output = new byte[bufferedCipher.GetOutputSize(message.Length)];
            bufferedCipher.Init(true, keyparam);
            try
            {
                output = bufferedCipher.DoFinal(message);
            }
            catch (Exception ex)
            {


            }
            return output;
        }
    }
}
