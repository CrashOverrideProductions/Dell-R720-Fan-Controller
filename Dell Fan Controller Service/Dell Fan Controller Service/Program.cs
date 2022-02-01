// ====================================================================================================================
// Application:      Dell R720 iDrac Fan Controller
// Purpose:          Application Entry Point
// Author:           Justin Bland
// Date:             30/01/2022
// Audit Date:       
// Status            In Development
// Revisions
// ====================================================================================================================

using System.ServiceProcess;

namespace Dell_Fan_Controller_Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
