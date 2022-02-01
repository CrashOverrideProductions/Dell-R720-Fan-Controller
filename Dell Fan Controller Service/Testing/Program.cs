using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Algorithims algorithims = new Algorithims();
            FanProfilePlot fanProfilePlot = new FanProfilePlot();

            float minSpeed = 26;
            float maxSpeed = 60;
            float minTemp = 40;
            float maxTemp = 60;
            double factor = 0.04;

            fanProfilePlot.generateDataSet(minSpeed, maxSpeed, minTemp, maxTemp, factor);

            

          //  Console.ReadLine();
        }
    }
}
