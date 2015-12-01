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
        long ram = 512;

        private TaskManager()
        {
            InitializeComponent();
            Random r = new Random();
            foreach (Process p in Process.GetProcesses())
            {
                long ws = p.WorkingSet64 / 1024;
                long ramP = (ws / ram)%100;
                int usoCPU = r.Next(1, 50);
                long usoDisco = (ramP + r.Next(1, 25))%100;
                long paged = p.PagedSystemMemorySize64;
                long nonPaged = p.NonpagedSystemMemorySize64;

                lvProcess.Items.Add(new MyItem { Name = p.ProcessName, ID = p.Id, Threads = p.Threads.Count, Memory = ramP, CPU = usoCPU, Disk = usoDisco});
                lblPaged.Content = paged.ToString();
                lblNonPaged.Content = nonPaged.ToString();

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
            long memoryP = r.Next(1, 60);
            int CPUp = r.Next(1, 50);
            long usoDisco = r.Next(1, 25);

            lvProcess.Items.Add(new MyItem { Name = processName, ID = processID, Threads = processThread, Memory = memoryP, CPU = CPUp, Disk = usoDisco });

            tbProcess.Text = string.Empty;
            tbProcess.Focus();
        }

        public void Ejecutar(string proceso)
        {
            Random rand = new Random();
            int IDproceso = rand.Next(1, 9999);
            int HilosProceso = rand.Next(1, 40);
            string NombreProceso = proceso;
            long memoryP = rand.Next(1, 60);
            int PCPU = rand.Next(1, 50);
            long usoDisco = rand.Next(1, 25);

            lvProcess.Items.Add(new MyItem { Name = NombreProceso, ID = IDproceso, Threads = HilosProceso, Memory = memoryP, CPU = PCPU, Disk = usoDisco });
        }

        public void CerrarProceso(string processName)
        {
            var selected = (from MyItem item in lvProcess.Items
                           where item.Name == processName
                           select item).FirstOrDefault();

            lvProcess.Items.RemoveAt(lvProcess.Items.IndexOf(selected));
        }

        private void btnEndTask_Click(object sender, RoutedEventArgs e)
        {
            var item = (MyItem)lvProcess.SelectedItem;
            lvProcess.Items.RemoveAt(lvProcess.Items.IndexOf(lvProcess.SelectedItem));
            if (OnProcessClosed != null)
                OnProcessClosed(item.Name);
            
        }

        public delegate void ProcessClosed(string processName);
        public event ProcessClosed OnProcessClosed;
    }

    public class MyItem
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Threads { get; set; }
        public long Memory { get; set; }
        public int CPU { get; set; }
        public long Disk { get; set; }
    }
}
