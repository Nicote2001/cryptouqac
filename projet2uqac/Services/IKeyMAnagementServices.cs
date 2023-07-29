
using projet2uqac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace projet2uqac.Services
{
    public interface IKeyManagementService
    {
        void GenereateMyKeys(string name);

        List<Key> GetKeys();

        void ImportKey(string rawKey, string name);

        string exportKey(int id);

        string GetKeyString(RSAParameters key);

        public Key GetKeyById(int id);
    }
}
