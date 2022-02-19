using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dell_PowerEdge_Fan_Controller__Interface_
{
    internal struct settingsFile
    {
        public string iDracIP;
        public string iDracUsername;
        public string iDracPassword;
        public string MinSpeed;
        public string MaxSpeed;
        public string MinTemp;
        public string MaxTemp;
        public string Factor;
        public string Shutdown;
    }

    internal class Settings
    {
        public settingsFile getSettingsFile(string FileLocation)
        {
            settingsFile settings = new settingsFile();

            if (FileLocation == null)
            {
                return settings;
            }
            else
            {
                    string line;

                    using (StreamReader file = new StreamReader(FileLocation))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.ToLower().StartsWith("idracip"))
                            {
                                string[] var = line.Split('=');
                                settings.iDracIP = var[1];
                            }

                            if (line.ToLower().StartsWith("idracusername"))
                            {
                                string[] var = line.Split('=');
                                settings.iDracUsername = var[1];
                            }

                            if (line.ToLower().StartsWith("idracpassword"))
                            {
                                string[] var = line.Split('=');
                                settings.iDracPassword = var[1];
                            }

                            if (line.ToLower().StartsWith("minspeed"))
                            {
                                string[] var = line.Split('=');
                                settings.MinSpeed = var[1];
                            }

                            if (line.ToLower().StartsWith("maxspeed"))
                            {
                                string[] var = line.Split('=');
                                settings.MaxSpeed = var[1];
                            }

                            if (line.ToLower().StartsWith("mintemp"))
                            {
                                string[] var = line.Split('=');
                                settings.MinTemp = var[1];
                            }

                            if (line.ToLower().StartsWith("tmax"))
                            {
                                string[] var = line.Split('=');
                                settings.MaxTemp = var[1];
                            }

                            if (line.ToLower().StartsWith("shutdown"))
                            {
                                string[] var = line.Split('=');
                                settings.Shutdown = var[1];
                            }

                            if (line.ToLower().StartsWith("factor"))
                            {
                                string[] var = line.Split('=');
                                settings.Factor = var[1];
                            }


                        }
                    }
                
                return settings;
            }
        }

        public void saveSettingsFile(string FileLocation, settingsFile settings)
        {

            // Create Data to Write to File
            string[] settingsArray = {
                                "iDracIP="+settings.iDracIP,
                                "iDracUsername="+settings.iDracUsername,
                                "iDracPassword="+settings.iDracPassword,
                                "minSpeed="+settings.MinSpeed,
                                "maxSpeed="+settings.MaxSpeed,
                                "minTemp="+settings.MinTemp,
                                "tMax="+settings.MaxSpeed,
                                "shutdown="+settings.Shutdown,
                                "factor="+settings.Factor
                                };


            // Write File
            File.WriteAllLines(FileLocation, settingsArray);

        }
    }
}
