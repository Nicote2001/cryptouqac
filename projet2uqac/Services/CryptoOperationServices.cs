
using Microsoft.Extensions.Logging;
using projet2uqac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace projet2uqac.Services
{
    public class CryptoOperationServices: ICryptoOperationServices
    {

        public CryptoOperationServices()
        {
            
        }

        public string DecryptMessage(string cryptedMessage, Key key)
        {
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider();

            var cipherDataAsByte = Convert.FromBase64String(cryptedMessage);
            rsaCryptoServiceProvider.ImportParameters(key.value);
            var encryptedData = rsaCryptoServiceProvider.Decrypt(cipherDataAsByte, false);
            Console.WriteLine(Encoding.UTF8.GetString(encryptedData));

            return Encoding.UTF8.GetString(encryptedData);
        }


        //encrypter un message avec un clé publique
        public string EncryptMessage(string message, Key key)
        {
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider();

            rsaCryptoServiceProvider.ImportParameters(key.value);

            var message_format = Encoding.UTF8.GetBytes(message);
            var encryptedMessage = rsaCryptoServiceProvider.Encrypt(message_format, false);

            return Convert.ToBase64String(encryptedMessage);

            Console.ReadLine();
        }

    }
}
