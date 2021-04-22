using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace AsymmetriskKryptering.RsaEncryption.Encryption
{
    public class RsaEncryptor
    {
        private const string Container = "PrivateContainer";

        public string Modolus { get; private set; }

        public string Exponent { get; private set; }

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

        public byte[] Encrypt(byte[] inputToEncrypt)
        {
            var csp = new CspParameters { KeyContainerName = Container };

            using var rsa = new RSACryptoServiceProvider(2048, csp);
            return rsa.Encrypt(inputToEncrypt, true);
        }

        public byte[] Decrypt(byte[] inputToDecrypt)
        {
            var csp = new CspParameters { KeyContainerName = Container };

            using var rsa = new RSACryptoServiceProvider(2048, csp);
            return rsa.Decrypt(inputToDecrypt, true);
        }

        private void CreateKey()
        {
            var csp = new CspParameters { KeyContainerName = Container };
            using var rsa = new RSACryptoServiceProvider(2048, csp);
            var myParam = rsa.ExportParameters(true);
            Modolus = BitConverter.ToString(myParam.Modulus);
            Exponent = BitConverter.ToString(myParam.Exponent);
        }
    }
}
