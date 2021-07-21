using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EncryptNote.Views
{
    /// <summary>
    /// Interaction logic for EnterPasswordWindow.xaml
    /// </summary>
    public partial class EnterPasswordWindow : Window
    {
        public string RegistryValue { get; set; }
        public EnterPasswordWindow(string registryVal)
        {
            InitializeComponent();
            RegistryValue = registryVal;
        }
    }
}
