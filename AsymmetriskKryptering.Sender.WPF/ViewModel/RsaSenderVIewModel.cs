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

        private string modolus;

        public string Modolus
        {
            get { return modolus; }
            set { modolus = value; OnPropertyChanged(nameof(Modolus)); }
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
            set { exponent = value; }
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
            Modolus = rsaEncryptor.Modolus;
            Exponent = rsaEncryptor.Exponent;
        }

        private void EncryptMessage(object obj)
        {
            var param = rsaEncryptor.AssignKey();
            var encryptedMessage = rsaEncryptor.Encrypt(Encoding.UTF8.GetBytes(messageToEncrypt));
            EncryptedMessageAsString = BitConverter.ToString(encryptedMessage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
