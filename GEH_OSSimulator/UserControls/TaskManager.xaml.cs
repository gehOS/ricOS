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
    }
}
