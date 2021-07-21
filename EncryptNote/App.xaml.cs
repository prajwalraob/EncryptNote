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

namespace EncryptNote
{
    public partial class App : System.Windows.Application
    {
        private void StartApplication(object sender, StartupEventArgs e)
        {
            AutofacConfig.Config();

            using (ILifetimeScope scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                string s = scope.Resolve<IGenerateEncryptedHash>().Generate("nystr12sft");

                RegistryKey regEntry = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\EncryptNote");

                if(regEntry != null)
                {
                    object entry = regEntry.GetValue("SecureHash");
                    if (entry != null)
                    {
                        EnterPasswordWindow enterPassword = new EnterPasswordWindow(entry.ToString());
                        enterPassword.ShowDialog();
                    }
                    else
                    {
                        CreatePasswordWindow createPassword = new CreatePasswordWindow();
                        createPassword.ShowDialog();
                    }
                }
                else
                {
                    CreatePasswordWindow createPassword = new CreatePasswordWindow();
                    createPassword.ShowDialog();
                }
            }
        }
    }

}
