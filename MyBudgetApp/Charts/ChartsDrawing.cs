using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyBudgetApp.Charts
{
    public static class ChartsDrawing
    {
        public static BitmapImage DonutPlot(double[] values, string[] labels)
        {
            var plt = new ScottPlot.Plot(600, 400);

            var pie = plt.AddPie(values);
            pie.Explode = true;
            pie.DonutSize = .5;
            pie.ShowPercentages = true;
            pie.SliceLabels = labels;
            plt.Legend();

            Bitmap bitmap = (plt.GetBitmap(false, 1));
            BitmapImage bitmapImage = bitmap.ToBitmapImage();
            return bitmapImage;
        }
    }
}
