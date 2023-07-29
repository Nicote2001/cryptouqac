
using Microsoft.Extensions.Logging;
using projet2uqac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace projet2uqac.Services
{
    public class KeyManagementService: IKeyManagementService
    {
        static List<Key> keys = new List<Key>();
        static Key publicKey;
        static Key privateKey;
        static bool isGenerated = false;
        RSACryptoServiceProvider csp;
        public KeyManagementService()
        {
            
        }

        public void GenereateMyKeys(string name)
        {
            if (!isGenerated)
            {
                //générer un provider de clés de longeur 2048 (d'ou le paramètres)
                csp = new RSACryptoServiceProvider(2048);

                //exportation des clés a partir du provider
                privateKey = new Key(keys.Count(), name + " ** private key **", csp.ExportParameters(true), true);
                keys.Add(privateKey);
                publicKey = new Key(keys.Count(), name + " ** public key **", csp.ExportParameters(true), false);
                keys.Add(publicKey);

                isGenerated = true;
            }
        }

        public List<Key> GetKeys( )
        {
            return keys;
        }

        public void ImportKey(string rawKey,string name)
        {
            //get a stream from the string
            var sr = new System.IO.StringReader(rawKey);
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            keys.Add(new Key(keys.Count(),name,(RSAParameters)xs.Deserialize(sr), isPrivate: false));
        }

        public string exportKey(int id)
        {
            Key key =  keys.Where(x=>x.id == id).FirstOrDefault();

            if (key.isPrivate)
            {
                return "Tu na pas compris la théories par rapport au clé publiques et privée, va te relire";
            }
            else
            {
                return GetKeyString(key.value);
            }
        }

        public string GetKeyString(RSAParameters key)
        {
            var sw = new System.IO.StringWriter();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //serialize the key into the stream
            xs.Serialize(sw, key);
            //get the string from the stream
            return sw.ToString();
        }

        public Key GetKeyById(int id)
        {
            return keys.Where(x => x.id == id).FirstOrDefault();
        }
    }
}
