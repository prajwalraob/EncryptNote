using System;
using IO = System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Globalization;
using System.Xml;
using System.Windows.Markup;
using System.IO;
using System.Xml.Serialization;
using EncryptNote.Models;
using Autofac;

namespace EncryptNote.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Initialize();
            InitializeComponent();
        }

        private void Initialize()
        {
            using (ILifetimeScope scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                Type noteItemModelType = scope.Resolve<INoteItemModel>().GetType();
                string[] notes = Directory.GetFiles(GlobalVariables.NotesDirectory, "*.noteinfo");

                foreach (string item in notes)
                {
                    using (StreamReader streamReader = new StreamReader(item))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(noteItemModelType);
                        object noteItem = xmlSerializer.Deserialize(streamReader);
                        if (noteItem != null)
                        {
                            GlobalVariables.NotesList.Add(noteItem as INoteItemModel);
                        }
                    }
                }
            }
        }
        private void Listbox_lostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            notesListBox.UnselectAll();
        }

        private void Listbox_lostFocus(object sender, RoutedEventArgs e)
        {
            notesListBox.UnselectAll();
        }
    }
}

