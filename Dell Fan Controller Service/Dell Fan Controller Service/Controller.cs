// ====================================================================================================================
// Application:      Dell R720 iDrac Fan Controller
// Purpose:          PowerEdge Controller
// Author:           Justin Bland
// Date:             30/01/2022
// Audit Date:       
// Status:           In Development
// Revisions
// ====================================================================================================================
using System.Diagnostics;

namespace Dell_Fan_Controller_Service
{
    internal class Controller
    {
        // Settings Structure Instance
        settingsStruct details = new settingsStruct(); // Structure

        // Settings Instance
        Settings settingsClass = new Settings(); // Settings Class




        // Algorithims Instance
        Algorithims algorithimsClass = new Algorithims();

        // CPU Info Instance
        ReadCPUTemp getCPUInfoClass = new ReadCPUTemp();

        // Fan Control Instance
        FanControl fanControlClass = new FanControl();


        internal void controlMain()
        {
            // Get Settings
            details = settingsClass.getSettings(); // Get Settings Method

            float lastTemp = 0;
            float currentTemp;


            currentTemp = getCPUInfoClass.CPUTemp(details.tMax);

            int fanSpeed = algorithimsClass.CalcFanSpeed(currentTemp, details.minSpeed, details.maxSpeed, details.minTemp, details.tMax, details.factor);

            int tempDiff = algorithimsClass.absDiff((int)currentTemp, (int)lastTemp);
            if (tempDiff > 5)
            {
                lastTemp = currentTemp;

                // Send New Fan Speed
                fanControlClass.sendFanSpeedCommand(details.iDracIP, details.iDracUsername, details.iDracPassword, fanSpeed);
                //Console.WriteLine(fanSpeed);


            }
        }

        internal void startControl()
        {
            // Get Settings
            details = settingsClass.getSettings(); // Get Settings Method

            fanControlClass.enableManualControl(details.iDracIP, details.iDracUsername, details.iDracPassword);
            //Console.WriteLine("Start Manual Fan Control");

            EventLog.WriteEntry("Dell PowerEdge Fan Controller", "System Fans Under App Control", EventLogEntryType.Information); // "App Name", "Message"

        }

        internal void endControl()
        {
            // Get Settings
            details = settingsClass.getSettings(); // Get Settings Method

            fanControlClass.diableManualControl(details.iDracIP, details.iDracUsername, details.iDracPassword);
            //Console.WriteLine("End Manual Fan Control");

            EventLog.WriteEntry("Dell PowerEdge Fan Controller", "System Fans Under BIOS Control", EventLogEntryType.Information); // "App Name", "Message"

        }


    }
}
