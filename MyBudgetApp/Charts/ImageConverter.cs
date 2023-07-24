using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyBudgetApp.Charts
{
    public static class ImageConverter
    {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            BitmapImage bitmapImage = new();

            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

    }
}
//var plt = new ScottPlot.Plot(600, 400);


//double[] values = { 778, 1000, 1000, 76, 43 };
//var pie = plt.AddPie(values);
//pie.Explode = true;
//pie.DonutSize = .3;

//plt.SaveFig("image.jpg");
//bitmap = plt.GetBitmap(false, 1);
