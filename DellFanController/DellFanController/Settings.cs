// ====================================================================================================================
// Application:      Dell R720 iDrac Fan Controller
// Purpose:          Application Settings
// Author:           Justin Bland
// Date:             30/01/2022
// Audit Date:       
// Status:           In Development
// Revisions
// ====================================================================================================================
using System;
using System.Diagnostics;
using System.IO;

namespace DellFanController
{
    internal struct settingsStruct
    {
        public string iDracIP;
        public string iDracUsername;
        public string iDracPassword;

        public float minSpeed;
        public float maxSpeed;

        public float minTemp;
        public float tMax;

        public bool shutdown;

        public string fanProfile;

        public double factor;
    }

    internal class Settings
    {
        internal settingsStruct getSettings()
        {
            try
            {
                string FileLocation = AppDomain.CurrentDomain.BaseDirectory + @"\Settings.ini";
                string line;

                settingsStruct configFile = new settingsStruct();

                using (StreamReader file = new StreamReader(FileLocation))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.StartsWith("iDracIP"))
                        {
                            string[] type = line.Split('=');
                            configFile.iDracIP = type[1];
                        }

                        if (line.StartsWith("iDracUsername"))
                        {
                            string[] type = line.Split('=');
                            configFile.iDracUsername = type[1];
                        }

                        if (line.StartsWith("iDracPassword"))
                        {
                            string[] type = line.Split('=');
                            configFile.iDracPassword = type[1];
                        }

                        if (line.StartsWith("minSpeed"))
                        {
                            string[] type = line.Split('=');
                            configFile.minSpeed = float.Parse(type[1]);
                        }

                        if (line.StartsWith("maxSpeed"))
                        {
                            string[] type = line.Split('=');
                            configFile.maxSpeed = float.Parse(type[1]);
                        }

                        if (line.StartsWith("tMax"))
                        {
                            string[] type = line.Split('=');
                            configFile.tMax = float.Parse(type[1]);
                        }

                        if (line.StartsWith("minTemp"))
                        {
                            string[] type = line.Split('=');
                            configFile.minTemp = float.Parse(type[1]);
                        }

                        if (line.StartsWith("shutdown"))
                        {
                            string[] type = line.Split('=');
                            if (type[1] == "1")
                            {
                                configFile.shutdown = true;
                            }
                            else
                            {
                                configFile.shutdown = false;
                            }
                        }

                        if (line.StartsWith("fanProfile"))
                        {
                            string[] type = line.Split('=');
                            configFile.fanProfile = type[1];
                        }

                        if (line.StartsWith("factor"))
                        {
                            string[] type = line.Split('=');
                            double factor = Convert.ToDouble(type[1]);
                            configFile.factor = factor;
                        }
                    }
                }
                return configFile;
            }
            catch (Exception ex)
            {
                // Add Log to Application Event Log
                string errorMsg = "Failed to Get Settings" + Environment.NewLine + ex.Message;
                EventLog.WriteEntry("Dell PowerEdge Fan Controller", errorMsg, EventLogEntryType.Error); // "App Name", "Message"

                // Stop Service Goes Here
                Console.WriteLine(ex.Message);
                // Return Empty Config
                settingsStruct configFile = new settingsStruct();
                return configFile;
            }

        }
    }
}
