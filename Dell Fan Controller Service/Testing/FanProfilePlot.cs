using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace Testing
{
    internal class FanProfilePlot
    {
        internal void generateDataSet(float minSpeed, float maxSpeed, float minTemp, float maxTemp, double factor)
        {
            Algorithims algorithims = new Algorithims();

            List<DataPoint> points = new List<DataPoint>();

            for (int i = 0; i < 100; i++)
            {
                float newTemp = algorithims.CalcFanSpeed(i, minSpeed, maxSpeed, minTemp, maxTemp, factor);

                DataPoint point = new DataPoint(i, newTemp);

                points.Add(point);
            }

            generatePlot(points, minSpeed, maxSpeed, minTemp, maxTemp, factor);

        }

        internal void generatePlot(List<DataPoint> pointsList, float minSpeed, float maxSpeed, float minTemp, float maxTemp, double factor)
        {
            string Title = "Current Fan Profile";
            string Subtitle = "Min Speed: " + minSpeed + "Max Speed: " + maxSpeed + 
                              "Min Temp: " + minTemp + "Max Temp: " + maxTemp + "Factor: " + factor;

            int size = pointsList.Count;
            int x =0;

            DataPoint[] pointsArray = new DataPoint[size];


            var points = new DataPoint[size];

            
            foreach (DataPoint point in pointsList)
            {
                points[x] = point;
                x++;
            }

            var plotModel1 = new PlotModel { Title = Title, Subtitle = Subtitle };
            var areaSeries1 = new AreaSeries();
            
            areaSeries1.ItemsSource = points;
            areaSeries1.DataFieldX = "X";
            areaSeries1.DataFieldY = "Y";
            plotModel1.Series.Add(areaSeries1);


            outputPlot("C:\\temp\\image.svg", plotModel1);
        }




        internal void outputPlot(string fileName, OxyPlot.IPlotModel plotModel)
        {
            using (var stream = File.Create(fileName))
            {
                var exporter = new SvgExporter { Width = 1200, Height = 800 };
                exporter.Export(plotModel, stream);
            }
        }
    }
}
