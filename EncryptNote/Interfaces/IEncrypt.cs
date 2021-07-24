using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptNote
{
    interface IEncrypt
    {
        string Encrypt(string content, string password);
        string Decrypt(string encryptedContent, string password);
    }
}
