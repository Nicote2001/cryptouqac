using System.Security.Cryptography;

namespace projet2uqac.Models
{
    public class Key
    {
        public Key(int id, string name, RSAParameters value, bool isPrivate)
        {
            this.id = id;
            this.name = name;
            this.value = value;
            this.isPrivate = isPrivate;
        }
        public int id { get; set; }

        public string? name { get; set; }

        public RSAParameters value { get; set; }

        public bool isPrivate { get; set; }
    }
}
