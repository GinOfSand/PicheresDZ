using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Picheres1
{
    internal class BitmapConverter
    {
        public byte[] ConvertBitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] byteArray = null;

            if (bitmapImage != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(stream);
                    byteArray = stream.ToArray();
                }
            }

            return byteArray;
        }

        public BitmapImage ConvertByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bitmapImage = null;

            if (byteArray != null && byteArray.Length > 0)
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze(); // Чтобы избежать InvalidOperationException при использовании в многопоточной среде
                }
            }

            return bitmapImage;
        }

    }
}
