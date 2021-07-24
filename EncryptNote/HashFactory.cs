using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptNote
{
    class HashFactory
    {
        public static IGenerateEncryptedHash GetHashMethod(HashType hashType)
        {
            IGenerateEncryptedHash returnVal;

            switch(hashType)
            {
                case HashType.MD5:
                    returnVal = new GenerateMD5Hash();
                    break;
                case HashType.SHA256:
                    returnVal = new GenerateSHA256Hash();
                    break;
                default:
                    returnVal = null;
                    break;
            };

            return returnVal;
              
        }
    }

    enum HashType
    {
        MD5 = 0,
        SHA256 = 1,
    }
}
