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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace GEH_OSSimulator.UserControls.Programas
{
    /// <summary>
    /// Interaction logic for ProgramChildWindow.xaml
    /// </summary>
    public partial class ProgramChildWindow : ChildWindow
    {
        public int CPUUsage { get; set; }
        public int MemoryUsage { get; set; }
        public string ProcessName { get; set; }
        public ProgramChildWindow()
        {
            InitializeComponent();
            var r = new Random();
            CPUUsage = r.Next(4, 15);
            MemoryUsage = r.Next(2, 10);
        }

        protected override void OnCloseButtonClicked(RoutedEventArgs e)
        {
            base.OnCloseButtonClicked(e);
            if (OnProgramHidden != null)
                OnProgramHidden(this, ProcessName);
            this.IsEnabled = false;
        }

        public delegate void ProgramHidden(ProgramChildWindow sender, string proccessName);
        public event ProgramHidden OnProgramHidden;

    }
}
