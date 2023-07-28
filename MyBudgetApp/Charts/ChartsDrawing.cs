using ScottPlot;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace MyBudgetApp.Charts
{
    public static class ChartsDrawing
    {
        public static BitmapImage DonutPlot(double[] values, string[] labels)
        {
            //TODO: Default image if empty
            if (values.Length == 0 || labels.Length == 0 || labels.Length != values.Length) return null;
            else
            {
                var plt = new ScottPlot.Plot(600, 400);

                var pie = plt.AddPie(values);
                pie.Explode = true;
                pie.DonutSize = .6;
                pie.Size = .7;
                pie.SliceLabelPosition = 0.6;
                pie.SliceLabelColors = pie.SliceFillColors;
                pie.ShowPercentages = true;
                pie.SliceLabels = labels;
                plt.Legend();

                Bitmap bitmap = (plt.GetBitmap(false, 1));
                BitmapImage bitmapImage = bitmap.ToBitmapImage();
                return bitmapImage;
            }
        }

        public static BitmapImage StackedBarPlot(List<double> values, List<double> limits, List<string> labels)
        {
            if (values.Count == 0 || labels.Count == 0 || labels.Count != values.Count) return null;
            else
            {

                
                
                var plt = new ScottPlot.Plot(600, 400);

                plt.Legend();
                plt.XTicks(labels.ToArray());

                plt.AddBar(limits.ToArray(), Color.Blue);
                plt.AddBar(values.ToArray(), Color.Yellow);

                // adjust axis limits so there is no padding below the bar graph
                plt.SetAxisLimits(yMin: 0);

                Bitmap bitmap = (plt.GetBitmap(false, 1));
                BitmapImage bitmapImage = bitmap.ToBitmapImage();
                return bitmapImage;

            }
        }
    }
}
