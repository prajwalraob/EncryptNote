using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Autofac;
using EncryptNote.Views;
using Microsoft.Win32;

namespace EncryptNote.ViewModels
{
    class CreatePasswordWindowViewModel : NotifyPropertyChanged
    {
        private string _passwordPlaintext;
        public string PasswordPlaintext
        {
            get => _passwordPlaintext;
            set
            {
                _passwordPlaintext = value;
                OnPropertyChanged();
            }
        }


        private string _enterPassword;
        public string EnterPassword
        {
            get => _enterPassword;
            set
            {
                _enterPassword = value;
                OnPropertyChanged();
            }
        }


        private string _reEnterPassword;
        public string ReEnterPassword
        {
            get => _reEnterPassword; 
            set
            {
                _reEnterPassword = value;
                OnPropertyChanged();
            }
        }

        public Command SetMasterPasswordCommand { get; set; }

        public CreatePasswordWindowViewModel()
        {
            SetMasterPasswordCommand = new Command(SetMasterPassword);
        }

        private void SetMasterPassword(object wnd)
        {
            CreatePasswordWindow createPassword = wnd as CreatePasswordWindow;
            string password = createPassword.pwdBox1.Password;
            string reEntered = createPassword.pwdBox2.Password;

            if(password != reEntered)
            {
                createPassword.pwdBox2.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                createPassword.pwdBox2.ToolTip = "Passwords do not match";
                ToolTipService.SetIsEnabled(createPassword.pwdBox2, true);
                // Show tooltip around this
            }
            else 
            {
                using (ILifetimeScope scope = GlobalVariables.Container.BeginLifetimeScope())
                {
                    string hash = scope.Resolve<IGenerateEncryptedHash>().Generate(password);
                    password = "";
                    reEntered = "";
                    RegistryKey regEntry = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EncryptNote");
                    regEntry.SetValue("SecureHash", hash);
                    regEntry.Close();

                    createPassword.Hide();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.ShowDialog();
                }
            }
        }
    }
}
