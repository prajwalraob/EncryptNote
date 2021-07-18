using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Data;

namespace EncryptNote
{
    public class Base85Image : Image
    {
        public string Base85Source
        {
            get { return (string)GetValue(Base85SourceProperty); }
            set { SetValue(Base85SourceProperty, value); }
        }

        public static readonly DependencyProperty Base85SourceProperty =
            DependencyProperty.Register("Base85Source", typeof(string), typeof(Base85Image),
            new PropertyMetadata());

        //static Base85Image()
        //{
        //    SourceProperty.OverrideMetadata(typeof(Base85Image), new FrameworkPropertyMetadata(OnSourcePropertyChanged));
        //}

        //private static void OnSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    Base85Image image85 = sender as Base85Image;

        //    BitmapEncoder encoder = new JpegBitmapEncoder();
        //    byte[] bytes = null;

        //    if (image85.Source is BitmapSource bitmapSource)
        //    {
        //        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            encoder.Save(stream);
        //            bytes = stream.ToArray();
        //        }
        //    }

        //    Base85Converter base85Converter = new Base85Converter();
        //    image85.Base85Source = base85Converter.Encode(bytes);
        //}

        //private static void OnBase85SourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{

        //    Base85Image image85 = sender as Base85Image;

        //    Base85Converter base85Converter = new Base85Converter();
        //    byte[] imageData = base85Converter.Decode(image85.Base85Source);

        //    BitmapImage image = new BitmapImage();
        //    if (!(imageData == null || imageData.Length == 0))
        //    {
        //        using (var mem = new MemoryStream(imageData))
        //        {
        //            mem.Position = 0;
        //            image.BeginInit();
        //            //image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        //            //image.UriSource = new Uri("");
        //            //image.CacheOption = BitmapCacheOption.OnLoad;
        //            image.StreamSource = mem;
        //            image.EndInit();
        //        }
        //        image.Freeze();
        //    }

        //    image85.Source = image;
        //}

    }
}
