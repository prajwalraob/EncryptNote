using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using EncryptNote.Views;
using EncryptNote.Models;
using Autofac;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;

namespace EncryptNote
{
    public partial class App : System.Windows.Application
    {
        private void StartApplication(object sender, StartupEventArgs e)
        {
            AutofacConfig.Config();

            using (ILifetimeScope scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                //IEncrypt encrypt = scope.Resolve<IEncrypt>();
                //string password = "nystr12sft";

                //string enc = encrypt.Encrypt("Here be dragons!", password);
                //string result = encrypt.Decrypt(enc, password);

                RegistryKey regEntry = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\EncryptNote");
                if(regEntry != null)
                {
                    object entry = regEntry.GetValue("SecureHash");
                    if (entry != null)
                    {
                        EnterPasswordWindow enterPassword = new EnterPasswordWindow(entry.ToString());
                        enterPassword.Show();
                    }
                    else
                    {
                        CreatePasswordWindow createPassword = new CreatePasswordWindow();
                        createPassword.Show();
                    }
                }
                else
                {
                    CreatePasswordWindow createPassword = new CreatePasswordWindow();
                    createPassword.Show();
                }
            }
        }
    }

}
