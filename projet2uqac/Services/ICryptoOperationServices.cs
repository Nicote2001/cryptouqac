
using projet2uqac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace projet2uqac.Services
{
    public interface ICryptoOperationServices
    {
        string DecryptMessage(string message, Key key);

        string EncryptMessage(string message, Key key);
    }

}
