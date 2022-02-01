// ====================================================================================================================
// Application:      Dell R720 iDrac Fan Controller
// Purpose:          Get CPU Details
// Author:           Justin Bland
// Date:             29/01/2022
// Audit Date:       
// Status:           In Development
// Revisions
//
// References:       https://github.com/openhardwaremonitor/openhardwaremonitor/
// ====================================================================================================================


using OpenHardwareMonitor.Hardware;
using System.Collections.Generic;
using System.Linq;

namespace Dell_Fan_Controller_Service
{
    internal class ReadCPUTemp
    {
        internal float CPUTemp(float tCase)
        {
            float temp = ((float)(tCase * 0.75));  // Change this to 90% of TCase (Default max temp)

            List<float> packageTemps = new List<float>();

            var myComputer = new Computer
            {
                CPUEnabled = true
            };
            myComputer.Open();

            foreach (var hardwareItem in myComputer.Hardware)
            {
                foreach (var sensor in hardwareItem.Sensors)
                {
                    if (sensor.Name == "CPU Package" && sensor.SensorType == SensorType.Temperature)
                    {
                        packageTemps.Add((float)sensor.Value);
                    }
                }
            }

            if (packageTemps.Count > 0)
            {
                temp = packageTemps.Max();
            }

            return temp;
        }
    }
}
