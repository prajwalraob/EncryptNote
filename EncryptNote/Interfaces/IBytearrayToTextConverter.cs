using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptNote
{
    interface IBytearrayToTextConverter
    {
        string Encode(byte[] byteArray);
        byte[] Decode(string encodedString);
    }
}
