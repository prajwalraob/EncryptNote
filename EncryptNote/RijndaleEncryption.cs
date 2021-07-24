using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptNote
{
    class RijndaleEncryption : IEncrypt
    {
        private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
        
        public string Decrypt(string encryptedContent, string password)
        {
            using (ILifetimeScope scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                IBytearrayToTextConverter converter = scope.Resolve<IBytearrayToTextConverter>();

                // Check arguments.
                if (encryptedContent == null || encryptedContent.Length <= 0)
                    throw new ArgumentNullException("cipherText");
                if (password == null || password.Length <= 0)
                    throw new ArgumentNullException("Key");

                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;
                byte[] passwordBytes = converter.Decode(password);
                byte[] encrytedBytes = converter.Decode(encryptedContent);

                // Create an Rijndael object
                // with the specified key and IV.
                using (Rijndael rijAlg = Rijndael.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
                    rijAlg.Key = pdb.GetBytes(32);
                    rijAlg.IV = pdb.GetBytes(16);

                    // Create a decryptor to perform the stream transform.
                    ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(encrytedBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }

                return plaintext;
            }
        }

        public string Encrypt(string content, string password)
        {
            using (ILifetimeScope scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                IBytearrayToTextConverter converter = scope.Resolve<IBytearrayToTextConverter>();
                // Check arguments.
                if (content == null || content.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (password == null || password.Length <= 0)
                    throw new ArgumentNullException("Key");

                //var passwordBytes = converter.Decode(password);

                string encrypted;
                // Create an Rijndael object
                // with the specified key and IV.
                using (Rijndael rijAlg = Rijndael.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
                    rijAlg.Key = pdb.GetBytes(32);
                    rijAlg.IV = pdb.GetBytes(16);

                    // Create an encryptor to perform the stream transform.
                    ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(content);
                            }
                            encrypted = converter.Encode(msEncrypt.ToArray());
                        }
                    }
                }

                // Return the encrypted bytes from the memory stream.
                return encrypted;
            }
        }
    }
}
