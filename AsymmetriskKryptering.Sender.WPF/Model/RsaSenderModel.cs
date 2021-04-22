using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetriskKryptering.Sender.WPF.Model
{
    public class RsaSenderModel
    {
        private string textToEncrypt;

        public string TextToEncrypt
        {
            get { return textToEncrypt; }
            set { textToEncrypt = value; }
        }

        private byte[] encryptedMessage;

        public byte[] EncryptedMessage
        {
            get { return encryptedMessage; }
            set { encryptedMessage = value; }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


    }
}
