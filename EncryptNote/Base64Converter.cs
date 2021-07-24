using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptNote
{
    class Base64Converter : IBytearrayToTextConverter
    {
        public byte[] Decode(string encodedString)
        {
            byte[] plainTextBytes = Encoding.ASCII.GetBytes(encodedString);
            return plainTextBytes;
        }

        public string Encode(byte[] byteArray)
        {
            
            string returnString = Encoding.ASCII.GetString(byteArray);
            return returnString;
        }
    }
}
