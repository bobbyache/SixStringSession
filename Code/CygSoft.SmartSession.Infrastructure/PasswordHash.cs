using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Infrastructure
{
    public class PasswordHash
    {
        public string Go(string s)
        {
            byte[] _encoded = new UTF8Encoding()
                .GetBytes(s);

            byte[] _hash = ((HashAlgorithm)CryptoConfig
                .CreateFromName("MD5"))
                .ComputeHash(_encoded);

            string r = BitConverter.ToString(_hash)
               .Replace("-", string.Empty);

            return r;
        }
    }
}
