using Newtonsoft.Json;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public struct DataItems
    {
        public string SensorName;
        public string SensorType;
        public string SensorValue;
    }

    public struct Processor
    {
        public string HardwareName;
        public string HardwareIdent;
        public List<DataItems> DataItems;
    }

    public struct timeStamp
    {
        public DateTime TimeStamp;
        public List<Processor> Processors;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var myComputer = new Computer
            {
                CPUEnabled = true
            };
            myComputer.Open();

            List<Processor> myProcessors = new List<Processor>();

            foreach (var hardwareItem in myComputer.Hardware)
            {
                Processor processors = new Processor();
                processors.HardwareName = hardwareItem.Name;
                processors.HardwareIdent = hardwareItem.Identifier.ToString();

                List<DataItems> dataItems = new List<DataItems>();
                processors.DataItems = dataItems;

                foreach (var sensor in hardwareItem.Sensors)
                {
                    DataItems dataItem = new DataItems();
                    dataItem.SensorName = sensor.Name;
                    dataItem.SensorType = sensor.SensorType.ToString();
                    dataItem.SensorValue = sensor.Value.ToString();

                    Console.WriteLine(sensor.SensorType + ", " + sensor.Value);

                    processors.DataItems.Add(dataItem);
                }
                
                myProcessors.Add(processors);
            }

            Console.WriteLine(JsonConvert.SerializeObject(myProcessors, Formatting.Indented));



            Console.ReadLine();
        }
    }
}
