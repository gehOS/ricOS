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

        public List<Process> AllProcesses { get; set; }

        string currentTime;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timerDos = new DispatcherTimer();
        Stopwatch stopwatch = new Stopwatch();
        long ram = 512;

        int paged;
        int nonpaged;
        int totalMemory;
        int totalCPU;
        
        private TaskManager()
        {
            InitializeComponent();
            AllProcesses = new List<Process>();
            AllProcesses.AddRange(Process.GetProcesses());
            var r = new Random();
            paged = r.Next(750, 860);
            nonpaged = r.Next(450, 550);
            totalMemory = r.Next(55, 65);
            totalCPU = r.Next(30, 55);
            lblPaged.Content = paged.ToString() + " MB";
            lblNonPaged.Content = nonpaged.ToString() + " MB";
            lblTotalMem.Content = totalMemory.ToString() + " %";
            lblTotalCPUu.Content = totalCPU.ToString() + " %";
            foreach (Process p in AllProcesses)
            {
                long ws = p.WorkingSet64 / 1024;
                long ramP = (ws / ram) % 100;
                int usoCPU = r.Next(1, 50);
                long usoDisco = (ramP + r.Next(1, 25)) % 100;

                lvProcess.Items.Add(new MyItem { Name = p.ProcessName, ID = p.Id, Threads = p.Threads.Count, Memory = ramP, CPU = usoCPU, Disk = usoDisco });
            }

            timer.Tick += new EventHandler(timer_Tick);
            timerDos.Tick += timerDos_Tick;
            timerDos.Interval = new TimeSpan(0, 0, 2);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
            stopwatch.Start();
            timerDos.Start();

        }

        void timerDos_Tick(object sender, EventArgs e)
        {
            ReloadInfo();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = stopwatch.Elapsed;
            currentTime = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            lblClock.Content = currentTime;
        }

        int multiplier = 1;
        void ReloadInfo() {
            Random r = new Random();
            var selection = lvProcess.SelectedItem;
            multiplier = multiplier * -1;
            var processItems = lvProcess.Items.Cast<MyItem>().ToList();
            lvProcess.Items.Clear();
            foreach (var obj in processItems) {
                var item = (MyItem)obj;
                item.CPU += (item.CPU / 10)*multiplier;
                item.Memory += (item.Memory / 10) * multiplier;
                item.Disk += (item.Disk / 10) * multiplier;
                lvProcess.Items.Add(item);
                lvProcess.SelectedItem = selection;
            }

            paged += (paged / 10) * multiplier;
            nonpaged += (nonpaged / 10) * multiplier;
            totalMemory += (totalMemory/ 10) * multiplier;
            totalCPU += (totalCPU / 10) * multiplier;


            lblPaged.Content = paged.ToString() + " MB";
            lblNonPaged.Content = nonpaged.ToString() + " MB";
            lblTotalMem.Content = totalMemory.ToString() + " %";
            lblTotalCPUu.Content = totalCPU.ToString() + " %";
            
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

            if(item != null)
                lvProcess.Items.RemoveAt(lvProcess.Items.IndexOf(lvProcess.SelectedItem));
            if (OnProcessClosed != null)
                try {
                    OnProcessClosed(item.Name);
                }
                catch
                {
                    MessageBox.Show("Seleccione el proceso que quiere matar");
                }
            
        }

        public void ModificarMemoriaCPU(int memoria, int cpu) {
            totalMemory += memoria;
            if (totalMemory > 100)
            {
                totalMemory = 100;
                MessageBox.Show("Alerta de Memoria.");
            }
            lblTotalMem.Content = totalMemory;
            totalCPU += cpu;
            if (totalCPU > 100)
            {
                totalCPU = 100;
                MessageBox.Show("Alerta de Procesador.");
            }
            lblTotalCPUu.Content = totalCPU;
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
