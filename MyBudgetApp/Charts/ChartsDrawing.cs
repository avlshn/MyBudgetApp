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

        public static BitmapImage StackedBarPlot(double[] values, double[] limits, string[] labels)
        {
            if (values.Length == 0 || labels.Length == 0 || labels.Length != values.Length) return null;
            else
            {


                var plt = new ScottPlot.Plot(600, 400);

                // create sample data
                double[] valuesA = { 1, 2, 3, 2, 1, 2, 1 };
                double[] valuesB = { 3, 3, 2, 1, 3, 2, 1 };

                // to simulate stacking B on A, shift B up by A
                double[] valuesB2 = new double[valuesB.Length];
                for (int i = 0; i < valuesB.Length; i++)
                    valuesB2[i] = valuesA[i] + valuesB[i];

                // plot the bar charts in reverse order (highest first)
                plt.AddBar(valuesB2);
                plt.AddBar(valuesA);

                // adjust axis limits so there is no padding below the bar graph
                plt.SetAxisLimits(yMin: 0);

                Bitmap bitmap = (plt.GetBitmap(false, 1));
                BitmapImage bitmapImage = bitmap.ToBitmapImage();
                return bitmapImage;

                return null;
            }
        }
    }
}
