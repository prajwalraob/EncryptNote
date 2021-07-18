using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EncryptNote
{
    class Image85Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string val = value as string;

            if(val != null)
            {
                Base85Converter base85Converter = new Base85Converter();
                byte[] imageData = base85Converter.Decode(val);

                BitmapImage image = new BitmapImage();
                if (!(imageData == null || imageData.Length == 0))
                {
                    using (var mem = new MemoryStream(imageData))
                    {
                        mem.Position = 0;
                        image.BeginInit();
                        //image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        //image.UriSource = new Uri("");
                        //image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = mem;
                        image.EndInit();
                    }
                    image.Freeze();
                }

                return image;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage image = value as BitmapImage;

            if(image != null)
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                byte[] bytes = null;

                if (image is BitmapSource bitmapSource)
                {
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    using (MemoryStream stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        bytes = stream.ToArray();
                    }
                }

                Base85Converter base85Converter = new Base85Converter();
                string encodedString = base85Converter.Encode(bytes);
                return encodedString;
            }
            return null;
        }

        
    }
}
