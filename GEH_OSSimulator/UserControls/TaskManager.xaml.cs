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

namespace GEH_OSSimulator.UserControls
{
    /// <summary>
    /// Interaction logic for TaskManager.xaml
    /// </summary>
    public partial class TaskManager : UserControl
    {
        public TaskManager()
        {
            InitializeComponent();

            foreach (Process p in Process.GetProcesses())
            {
                string process = string.Format("{0} | {1} | {2}", p.ProcessName, p.Id, p.Threads.Count);
                lbProcesses.Items.Add(process);
            }
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
