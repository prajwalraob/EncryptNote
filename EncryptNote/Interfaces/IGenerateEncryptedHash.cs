using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptNote
{
    interface IGenerateEncryptedHash
    {
        string Generate(string password);
    }
}
