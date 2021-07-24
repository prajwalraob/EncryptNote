using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Autofac;
using EncryptNote.Views;

namespace EncryptNote.ViewModels
{
    class EnterPasswordWindowViewModel : NotifyPropertyChanged
    {
        public Command EnterCommand { get; set; }

        public EnterPasswordWindowViewModel()
        {
            EnterCommand = new Command(Enter);
        }

        private void Enter(object wnd)
        {
            using (ILifetimeScope scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                EnterPasswordWindow enterPassword = wnd as EnterPasswordWindow;
                string hash = HashFactory.GetHashMethod(HashType.MD5).Generate(enterPassword.pwdBox.Password);
                enterPassword.pwdBox.Password = "";

                if(hash == enterPassword.RegistryValue)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    enterPassword.Owner = mainWindow;
                    enterPassword.Close();
                }
                else
                {
                    enterPassword.pwdBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }

        }

    }
}
