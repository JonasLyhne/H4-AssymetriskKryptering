using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AsymmetriskKryptering.RsaEncryption.Encryption
{
    public class RsaEncryptor
    {
        private const string Container = "PrivateContainer";

        public RSAParameters KeyInfo { get; set; }

        public RsaEncryptor()
        {
            this.CreateKey();
        }

        public CspParameters AssignKey()
        {
            CspParameters cspParameters = new CspParameters(1);
            cspParameters.KeyContainerName = Container;
            cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParameters.ProviderName = "Microsoft Strong Cryptographic Provider";
            return cspParameters;
        }

        public void DeleteKey()
        {
            var csp = new CspParameters { KeyContainerName = Container };
        }

        /// <summary>
        /// Encrypts a byte array using public key information 
        /// </summary>
        /// <param name="inputToEncrypt"></param>
        /// <param name="modulus"></param>
        /// <param name="exponent"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] inputToEncrypt, byte[] modulus, byte[] exponent)
        {
            var csp = new CspParameters { KeyContainerName = Container };
            

            using var rsa = new RSACryptoServiceProvider(2048, csp);
            rsa.ImportParameters(CreateRSAParametersWithPublicKeyInformation(modulus, exponent));
            var result = rsa.Encrypt(inputToEncrypt, false);
            rsa.Clear();
            return result;
        }

        /// <summary>
        /// Decrypts a byte array using private key Information
        /// </summary>
        /// <param name="inputToDecrypt"></param>
        /// <param name="pu"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] inputToDecrypt, Dictionary<string, string> rsaParameterInformation)
        {
            var csp = new CspParameters { KeyContainerName = Container };
            
            using var rsa = new RSACryptoServiceProvider(2048, csp);
            rsa.ImportParameters(CreateRSAParamentersWithPrivateKeyInformation(rsaParameterInformation));
            var result = rsa.Decrypt(inputToDecrypt, false);
            rsa.Clear();
            return result;
        }

        /// <summary>
        /// Creates the public and privateKey information for Encryption and Decryption
        /// </summary>
        private void CreateKey()
        {
            var csp = new CspParameters { KeyContainerName = Container };
            using var rsa = new RSACryptoServiceProvider(2048, csp);
            KeyInfo = rsa.ExportParameters(true);
        }

        /// <summary>
        /// Creates and returns a RSAParameters object containing public key information
        /// </summary>
        /// <param name="modulus">The Modulus byte array</param>
        /// <param name="exponent">The Exponent byte array</param>
        private RSAParameters CreateRSAParametersWithPublicKeyInformation(byte[] modulus, byte[] exponent)
        {
            var rsa = new RSAParameters();
            rsa.Modulus = modulus;
            rsa.Exponent = exponent;

            return rsa;
        }

        /// <summary>
        /// Creates and returns an RSAParameters object from external values
        /// </summary>
        /// <param name="kvp"></param>
        /// <returns></returns>
        private RSAParameters CreateRSAParamentersWithPrivateKeyInformation(Dictionary<string, string> kvp)
        {
            var rsa = new RSAParameters();
            rsa.Modulus = Convert.FromBase64String(kvp["Modulus"]);
            rsa.Exponent = Convert.FromBase64String(kvp["Exponent"]);
            rsa.D = Convert.FromBase64String(kvp["D"]);
            rsa.DP = Convert.FromBase64String(kvp["Dp"]);
            rsa.DQ = Convert.FromBase64String(kvp["Dq"]);
            rsa.InverseQ = Convert.FromBase64String(kvp["InverseQ"]);
            rsa.P = Convert.FromBase64String(kvp["P"]);
            rsa.Q = Convert.FromBase64String(kvp["Q"]);
            return rsa;
        }
    }
}
