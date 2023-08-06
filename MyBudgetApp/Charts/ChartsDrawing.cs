using ScottPlot;
using ScottPlot.Drawing.Colormaps;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace MyBudgetApp.Charts;

public static class ChartsDrawing
{
    internal static BitmapImage DonutPlot(List<CategorySammary> CatList)
    {
        double[] values = CatList.Select(p => p.Value).ToArray();
        string[] labels = CatList.Select(p =>p.Label).ToArray();
        //TODO: Default image if empty
        if (CatList.Count==0) return null;
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

    internal static BitmapImage StackedBarPlot(List<CategorySammary> CatList)
    {
        if (CatList.Count == 0) return null;
        else
        {
            List<CategorySammary> OverspendedList = new List<CategorySammary>();

            string[] barLabelsText;
            double[] barLabelsPositions;

            int position = 0;
            double AxisMax = 0;

            //Separating categories with overspending into separate list and defining  
            //positions on plot.

            for (int i = 0; i < CatList.Count; i++)
            {

                if (CatList[i].isOverspended)
                {
                    if (AxisMax < CatList[i].Value) AxisMax = CatList[i].Value;
                    position++;
                    CatList[i].Position = position*10;
                    OverspendedList.Add(CatList[i]);
                    CatList.RemoveAt(i);
                    i--;
                }
                else if (AxisMax < CatList[i].Limit) AxisMax = CatList[i].Limit;

            }



            //Defining positions of the rest of categories

            foreach (var cat in CatList)
            {
                position++;
                cat.Position = position*10;
            }

            //AxisMax = AxisMax * 1.5 /position;

            //foreach (var cat in OverspendedList)
            //{
            //    cat.Position = cat.Position*AxisMax;
            //}

            var plt = new ScottPlot.Plot(800, 400);
            plt.SetAxisLimits(xMin: 0, xMax: AxisMax*1.05, yMin: 0, yMax: position*10+10 );
            plt.Legend(location: Alignment.UpperRight);


            if (OverspendedList.Count > 0)
            {
                //Generating input data
                var values = OverspendedList.Select(p => p.Value).ToArray();
                var limits = OverspendedList.Select(p => p.Limit).ToArray();
                var positions = OverspendedList.Select(p => p.Position).ToArray();
                var labels = OverspendedList.Select(p => p.Label).ToArray();

                barLabelsPositions = (double[])positions.Clone();
                barLabelsText = (string[])labels.Clone();



                //plt.XTicks(positions, labels);

                var bar1 = plt.AddBar(values, positions, Color.Red); //plotting values than limits at the same positions 
                var bar2 = plt.AddBar(limits, positions, Color.BlueViolet);
                bar1.BarWidth = 8;
                bar2.BarWidth = 8;
                bar1.Orientation = Orientation.Horizontal;
                bar2.Orientation = Orientation.Horizontal;

            }
            else
            {
                barLabelsPositions = null;
                barLabelsText = null;
            }

            //CatList = CatList.OrderByDescending(x => x.Value).ToList();

            if (CatList.Count > 0)
            {
                //Generating input data
                var values = CatList.Select(p => p.Value).ToArray();
                var limits = CatList.Select(p => p.Limit).ToArray();
                var positions = CatList.Select(p => p.Position).ToArray();
                var labels = CatList.Select(p => p.Label).ToArray();

                if (barLabelsPositions != null)
                {
                    barLabelsPositions = barLabelsPositions.Concat(positions).ToArray();

                }
                else barLabelsPositions = (double[])positions.Clone();

                if (barLabelsText != null)
                {
                    barLabelsText = barLabelsText.Concat(labels).ToArray();
                }
                else barLabelsText = (string[])labels.Clone();
                
                var bar3 = plt.AddBar(limits, positions, Color.BlueViolet);
                var bar4 = plt.AddBar(values, positions, Color.Blue); //plotting values than limits at the same positions 
                bar3.BarWidth = 8;
                bar4.BarWidth = 8;
                bar3.Orientation = Orientation.Horizontal;
                bar4.Orientation = Orientation.Horizontal;

            }

            //for (int i = 0; i < barLabelsText.Length; i++)
            //{
            //    barLabelsText[i] = barLabelsText[i].Replace(" ", System.Environment.NewLine);
            //}


            plt.YTicks(barLabelsPositions, barLabelsText);

            Bitmap bitmap = (plt.GetBitmap(false, 2));
            BitmapImage bitmapImage = bitmap.ToBitmapImage();
            return bitmapImage;
        }
    }
}
