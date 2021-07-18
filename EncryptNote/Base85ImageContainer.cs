using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace EncryptNote
{
    public class Base85ImageContainer : BlockUIContainer
    {
        public string Base85Source
        {
            get { return (string)GetValue(Base85SourceProperty); }
            set { SetValue(Base85SourceProperty, value); }
        }

        public static readonly DependencyProperty Base85SourceProperty =
            DependencyProperty.Register("Base85Source", typeof(string), typeof(Base85ImageContainer),
            new FrameworkPropertyMetadata(OnBase85SourceChanged));

        private static void OnBase85SourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Base85ImageContainer image85 = sender as Base85ImageContainer;

            Base85Converter base85Converter = new Base85Converter();
            byte[] imageData = base85Converter.Decode(image85.Base85Source);

            BitmapImage image = new BitmapImage();
            if (!(imageData == null || imageData.Length == 0))
            {
                using (var mem = new MemoryStream(imageData))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
            }

            image85.Child = new Base85Image() { Source = image };
        }
    }
}
