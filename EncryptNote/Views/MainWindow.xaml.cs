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

namespace EncryptNote.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            richTextBox.AddHandler(RichTextBox.DragOverEvent, new DragEventHandler(RichTextBox_DragOver), true);
            richTextBox.AddHandler(RichTextBox.DropEvent, new DragEventHandler(RichTextBox_Drop), true);
        }

        private void RichTextBox_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = false;
        }

        private void RichTextBox_Drop(object sender, DragEventArgs e)
        {
            string docPath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            var orgdata = Clipboard.GetDataObject();
            BitmapImage bitmap = new BitmapImage(new Uri(docPath));

            var scaleWidth = 150 / bitmap.Width;
            var scaleHeight = 100 / bitmap.Height;
            var scaling = Math.Min(scaleWidth, scaleHeight);

            TransformedBitmap transformedBitmap = new TransformedBitmap(bitmap, new ScaleTransform(scaling, scaling));

            Clipboard.SetImage(transformedBitmap);
            richTextBox.Paste();
            EditingCommands.AlignLeft.Execute(true, richTextBox);

            Clipboard.SetDataObject(orgdata);
        }

        private void Insert_Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse image Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "jpg",
                Filter = "Image file |*.jpg;*.png;*.bmp;*.jpeg",
                Multiselect = false
            };

            var orgdata = Clipboard.GetDataObject();

            if (openFileDialog1.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog1.FileName));
                var scaleWidth = 150 / bitmap.Width;
                var scaleHeight = 100 / bitmap.Height;
                var scaling = Math.Min(scaleWidth, scaleHeight);

                TransformedBitmap transformedBitmap = new TransformedBitmap(bitmap, new ScaleTransform(scaling, scaling));

                Clipboard.SetImage(transformedBitmap);
                richTextBox.Paste();
                EditingCommands.AlignLeft.Execute(true, richTextBox);
            }

            Clipboard.SetDataObject(orgdata);
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

