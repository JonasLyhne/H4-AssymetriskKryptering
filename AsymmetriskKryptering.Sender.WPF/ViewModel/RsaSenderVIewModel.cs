using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using AsymmetriskKryptering.RsaEncryption.Encryption;

namespace AsymmetriskKryptering.Sender.WPF.ViewModel
{
    public class RsaSenderVIewModel : INotifyPropertyChanged
    {
        private RsaEncryptor rsaEncryptor; 
        private ICommand encrypt;
        private string exponent;
        private string messageToEncrypt;
        private string encryptedMessageAsString;

        private string modulus;

        public string Modulus
        {
            get { return modulus; }
            set { modulus = value; OnPropertyChanged(nameof(Modulus)); }
        }


        public string EncryptedMessageAsString
        {
            get { return encryptedMessageAsString; }
            set { encryptedMessageAsString = value; OnPropertyChanged(nameof(EncryptedMessageAsString)); }
        }


        public string MessageToEncrypt
        {
            get { return messageToEncrypt; }
            set { messageToEncrypt = value; OnPropertyChanged(nameof(MessageToEncrypt)); }
        }


        public string Exponent
        {
            get { return exponent; }
            set { exponent = value; OnPropertyChanged(nameof(Exponent)); }
        }


        public ICommand Encrypt
        {
            get { return encrypt; }
            set { encrypt = value; }
        }

        public RsaSenderVIewModel()
        {
            Encrypt = new DelegateCommand(EncryptMessage);
            rsaEncryptor = new RsaEncryptor();
        }

        private void EncryptMessage(object obj)
        {
            var encryptedMessage = rsaEncryptor.Encrypt(Encoding.UTF8.GetBytes(messageToEncrypt), Convert.FromBase64String(Modulus), Convert.FromBase64String(Exponent));
            EncryptedMessageAsString = Convert.ToBase64String(encryptedMessage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
