using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;

namespace GEH_OSSimulator.UserControls
{
    /// <summary>
    /// Interaction logic for TaskManager.xaml
    /// </summary>
    public partial class TaskManager : UserControl
    {
        private static TaskManager _instance;
        public static TaskManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TaskManager();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        string currentTime;
        DispatcherTimer timer = new DispatcherTimer();
        Stopwatch stopwatch = new Stopwatch();

        private TaskManager()
        {
            InitializeComponent();

            foreach (Process p in Process.GetProcesses())
            {
                string process = string.Format("{0} | {1} | {2}", p.ProcessName, p.Id, p.Threads.Count);
                lbProcesses.Items.Add(process);
            }

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
            stopwatch.Start();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = stopwatch.Elapsed;
            currentTime = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            lblClock.Content = currentTime;
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int processID = r.Next(1, 9999);
            int processThread = r.Next(1, 40);
            string processName = tbProcess.Text;

            string UserProcess = string.Format("{0} | {1} | {2}", processName, processID.ToString(), processThread.ToString());
            lbProcesses.Items.Add(UserProcess);

            tbProcess.Text = string.Empty;
            tbProcess.Focus();
        }

        public void Ejecutar(string proceso)
        {
            Random rand = new Random();
            int IDproceso = rand.Next(1, 9999);
            int HilosProceso = rand.Next(1, 40);
            string NombreProceso = proceso;

            string ProcesoInput = string.Format("{0} | {1} | {2}", NombreProceso, IDproceso.ToString(), HilosProceso.ToString());
            lbProcesses.Items.Add(ProcesoInput);
        }
    }
}
