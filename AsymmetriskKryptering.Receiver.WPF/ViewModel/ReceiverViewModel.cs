using AsymmetriskKryptering.RsaEncryption.Encryption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace AsymmetriskKryptering.Receiver.WPF.ViewModel
{
    public class ReceiverViewModel : INotifyPropertyChanged
    {
        private RsaEncryptor rsaEncryptor;
        private ICommand decrypt;
        private string exponent;
        private string modulus;
        private string d;
        private string dP;
        private string dQ;
        private string inverseQ;
        private string p;
        private string q;
        private string cipherbytes;
        private string decrypted;

        public string Modulus
        {
            get { return modulus; }
            set { modulus = value; OnPropertyChanged(nameof(Modulus)); }
        }

        public string Exponent
        {
            get { return exponent; }
            set { exponent = value; OnPropertyChanged(nameof(Exponent)); }
        }

        public string D
        {
            get { return d; }
            set { d = value; OnPropertyChanged(nameof(D)); }
        }

        public string Dp
        {
            get { return dP; }
            set { dP = value; OnPropertyChanged(nameof(Dp)); }
        }

        public string Dq
        {
            get { return dQ; }
            set { dQ = value; OnPropertyChanged(nameof(Dq)); }
        }

        public string InverseQ
        {
            get { return inverseQ; }
            set { inverseQ = value; OnPropertyChanged(nameof(InverseQ)); }
        }

        public string P
        {
            get { return p; }
            set { p = value; OnPropertyChanged(nameof(P)); }
        }

        public string Q
        {
            get { return q; }
            set { q = value; OnPropertyChanged(nameof(Q)); }
        }

        public string Cipherbytes
        {
            get { return cipherbytes; }
            set { cipherbytes = value; OnPropertyChanged(nameof(Cipherbytes)); }
        }

        public string Decrypted
        {
            get { return decrypted; }
            set { decrypted = value; OnPropertyChanged(nameof(Decrypted)); }
        }

        public ICommand Decrypt
        {
            get { return decrypt; }
            set { decrypt = value; }
        }

        public ReceiverViewModel()
        {
            Decrypt = new DelegateCommand(DecryptMessage);
            rsaEncryptor = new RsaEncryptor();
            Exponent = Convert.ToBase64String(rsaEncryptor.KeyInfo.Exponent);
            Modulus = Convert.ToBase64String(rsaEncryptor.KeyInfo.Modulus);
            D = Convert.ToBase64String(rsaEncryptor.KeyInfo.D);
            Dp = Convert.ToBase64String(rsaEncryptor.KeyInfo.DP);
            Dq = Convert.ToBase64String(rsaEncryptor.KeyInfo.DQ);
            InverseQ = Convert.ToBase64String(rsaEncryptor.KeyInfo.InverseQ);
            P = Convert.ToBase64String(rsaEncryptor.KeyInfo.P);
            Q = Convert.ToBase64String(rsaEncryptor.KeyInfo.Q);
        }

        private void DecryptMessage(object obj)
        {
            var keyValues = new Dictionary<string, string>();
            keyValues.Add(nameof(Modulus), Modulus);
            keyValues.Add(nameof(Exponent), Exponent);
            keyValues.Add(nameof(D), D);
            keyValues.Add(nameof(Dp), Dp);
            keyValues.Add(nameof(Dq), Dq);
            keyValues.Add(nameof(InverseQ), InverseQ);
            keyValues.Add(nameof(P), P);
            keyValues.Add(nameof(Q), Q);

            Decrypted = Encoding.UTF8.GetString(rsaEncryptor.Decrypt(Convert.FromBase64String(Cipherbytes), keyValues));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
