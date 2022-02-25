// ====================================================================================================================
// Application:      Dell R720 iDrac Fan Controller
// Purpose:          Fan Control
// Author:           Justin Bland
// Date:             30/01/2022
// Audit Date:       
// Status            In Development
// Revisions
// ====================================================================================================================

using System;
using System.Diagnostics;


namespace Dell_Fan_Controller_Service
{
    internal class FanControl
    {
        internal void enableManualControl(string ipAddress, string username, string password)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            string ipmiToolLocation = AppDomain.CurrentDomain.BaseDirectory;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C cd " + ipmiToolLocation + "&& ipmitool -I lanplus -H " + ipAddress + " -U " + username + " -P " + password + " raw 0x30 0x30 0x01 0x00";
            process.StartInfo = startInfo;
            process.Start();

            // string res = process.StandardOutput.ReadToEnd();

            //Console.WriteLine(res);

        }

        internal void diableManualControl(string ipAddress, string username, string password)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            string ipmiToolLocation = AppDomain.CurrentDomain.BaseDirectory;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C cd " + ipmiToolLocation + "&& ipmitool -I lanplus -H " + ipAddress + " -U " + username + " -P " + password + " raw 0x30 0x30 0x01 0x01";
            process.StartInfo = startInfo;
            process.Start();

            // string res = process.StandardOutput.ReadToEnd();

            //Console.WriteLine(res);
        }

        internal void sendFanSpeedCommand(string ipAddress, string username, string password, int fanSpeed)
        {
            if (ipAddress == null )
            {
                EventLog.WriteEntry("Dell PowerEdge Fan Controller", "Dell PowerEdge iDrac IP is null", EventLogEntryType.Error);
                return;
            }
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            string ipmiToolLocation = AppDomain.CurrentDomain.BaseDirectory;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C cd " + ipmiToolLocation + "&& ipmitool -I lanplus -H " + ipAddress + " -U " + username + " -P " + password + " raw 0x30 0x30 0x02 0xff " + "0x" + fanSpeed.ToString("x16");
            process.StartInfo = startInfo;
            process.Start();

            //string res = process.StandardOutput.ReadToEnd();

            //Console.WriteLine(res);
        }
    }
}
