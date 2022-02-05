using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Dell_PowerEdge_Fan_Controller__Interface_
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            new Thread(SampleFunction).Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        void SampleFunction()
        {
            // Gets executed on a seperate thread and 
            // doesn't block the UI while sleeping
            for (int i = 0; true; i++)
            {
                updateServiceButtons();
                Thread.Sleep(1000);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeApplication();
        }

        void updateServiceButtons()
        {
            if (InvokeRequired)
            {
                //this.Invoke(new Action<string>(updateServiceButtons), new object[] { });
                this.Invoke(new MethodInvoker(updateServiceButtons));
                return;
            }

            string status = getServiceStatus();
            if (status == "Running")
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnRestart.Enabled = true;
            }
            else if (status == "Stopped")
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnRestart.Enabled = false;
            }
            else if (status == "Paused")
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnRestart.Enabled = false;
            }
            else if (status == "Stopping")
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnRestart.Enabled = false;
            }
            else if (status == "Starting")
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnRestart.Enabled = false;
            }
            else if (status == "Status Changing")
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnRestart.Enabled = false;
            }
            else
            {
                btnStart.Enabled = false;
                btnStop.Enabled = false;
                btnRestart.Enabled = false;
            }

            lblStatus.Text = status;
        }

        string getServiceStatus()
        {
            try
            {
                ServiceController sc = new ServiceController("Dell Fan Controller");

                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        return "Running";
                    case ServiceControllerStatus.Stopped:
                        return "Stopped";
                    case ServiceControllerStatus.Paused:
                        return "Paused";
                    case ServiceControllerStatus.StopPending:
                        return "Stopping";
                    case ServiceControllerStatus.StartPending:
                        return "Starting";
                    default:
                        return "Status Changing";
                }
            }
            catch (Exception ex)
            {
                return "Service Not Found";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController("Dell Fan Controller");
            if ((service.Status.Equals(ServiceControllerStatus.Running)) ||
                (service.Status.Equals(ServiceControllerStatus.StartPending)))
            {
                // Do Nothing
            }

            else service.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController("Dell Fan Controller");
            if ((service.Status.Equals(ServiceControllerStatus.Stopped)) ||
                (service.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                // Do Nothing
            }

            else service.Stop();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController("Dell Fan Controller");

            service.Stop();

            service.WaitForStatus(ServiceControllerStatus.Stopped);

            service.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeApplication();
        }

        private void closeApplication()
        {
            //Application.Exit();
            Environment.Exit(Environment.ExitCode);

        }

        private void updateChart(List<fanProfile> profile)
        {
            try
            {
                var objChart = chart1.ChartAreas[0];
                objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;

                // Fan Speed
                objChart.AxisX.Minimum = 0;
                objChart.AxisX.Maximum = 100;
                objChart.AxisX.Title = "CPU Temperature °C";
                objChart.AxisX.TitleFont = new System.Drawing.Font("Microsoft San Serif",10);
                objChart.AxisY.TitleFont = new System.Drawing.Font("Microsoft San Serif",10);
                objChart.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft San Serif", 8);
                objChart.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft San Serif", 8);

                // Temperature
                objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
                objChart.AxisY.Minimum = 0;
                objChart.AxisY.Maximum = 100;
                objChart.AxisY.Title = "Fan Speed %"; 

                 //clear
                 chart1.Series.Clear();

                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

                chart1.Series.Add("Profile");
                chart1.Series["Profile"].ChartType = SeriesChartType.Line;

                chart1.Series["Profile"].Color = Color.Red;

                Random random = new Random();
                for (int i = 0; i < (profile.Count ); i++)
                {
                    chart1.Series["Profile"].Points.AddXY(profile[i].Temp, Convert.ToInt32(profile[i].Percent));
                    Console.WriteLine(profile[i].Temp +" - "+ profile[i].Percent);


                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }

        }

        private void dataChange()
        {
            // Generate New Dataset
            float minSpeed = float.Parse(textBox3.Text);
            float maxSpeed = float.Parse(textBox8.Text);
            float minTemp = float.Parse(textBox7.Text);
            float maxTemp = float.Parse(textBox6.Text);
            double factor = float.Parse(textBox9.Text);

            List<fanProfile> fanProfiles = new List<fanProfile>();


            for (int i = (0); i < (101); i++)
            {
                int speed = CalcFanSpeed(i, minSpeed, maxSpeed, minTemp, maxTemp, factor);

                fanProfile point = new fanProfile();

                point.Temp = i;
                point.Percent = speed;

                fanProfiles.Add(point);
            }
            // Do Stuff Here
            updateChart(fanProfiles);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            

            // Change Graph
            dataChange();
        }



        internal int CalcFanSpeed(float temp, float minSpeed, float maxSpeed, float minTemp, float maxTemp, double factor)
        {
            if (temp < minTemp)
            {
                temp = minTemp;
            }

            if (temp > maxTemp)
            {
                temp = maxTemp;
            }



            float newSpeed = minSpeed;

            int counter = (int)(temp - minTemp);

            for (int z = 0; z < counter; z++)
            {
                newSpeed = (float)(newSpeed + (newSpeed * factor));
            }




            if (newSpeed < minSpeed)
            {
                newSpeed = minSpeed;
            }

            if (newSpeed > maxSpeed)
            {
                newSpeed = maxSpeed;
            }


            return (int)newSpeed;

        }
    }
    
    public struct fanProfile
    { 
        public int Temp;
        public int Percent;
    }

}
