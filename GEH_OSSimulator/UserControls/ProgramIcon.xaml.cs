using GEH_OSSimulator.UserControls.Programas;
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

namespace GEH_OSSimulator.UserControls
{
    /// <summary>
    /// Interaction logic for ProgramIcon.xaml
    /// </summary>
    public partial class ProgramIcon : UserControl
    {
        public ImageBrush ImageSource { get; set; }
        public ProgramChildWindow ProgramUC { get; set; }
        public string ProcessName { get; set; }
        public ProgramIcon()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
             SelectorGrid.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            SelectorGrid.Visibility = Visibility.Hidden;
        }

        private void ButtonSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OnLoadUserControl != null)
                OnLoadUserControl(ProgramUC);
        }

        public delegate void LoadUserControl(ProgramChildWindow userControl);
        public event LoadUserControl OnLoadUserControl;

        private void ButtonSelector_Click(object sender, RoutedEventArgs e)
        {
            if (!ProgramUC.IsEnabled)
            {
                TaskManager.Instance.ModificarMemoriaCPU(ProgramUC.MemoryUsage, ProgramUC.CPUUsage);    
            }
            
            if (OnLoadUserControl != null)
                OnLoadUserControl(ProgramUC);
        }

    }
}
