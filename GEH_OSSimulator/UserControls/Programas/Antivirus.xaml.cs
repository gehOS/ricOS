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
using System.Windows.Threading;

namespace GEH_OSSimulator.UserControls.Programas
{
    /// <summary>
    /// Interaction logic for Antivirus.xaml
    /// </summary>
    public partial class Antivirus : UserControl
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int i = 0;


        public Antivirus()
        {
            InitializeComponent();
            labelporcen.Visibility = System.Windows.Visibility.Hidden;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (i == 1000)
            {
                ProgressBarAnti.Value = 1000;
                labelporcen.Content = "100% completed...";
                dispatcherTimer.Stop();
                btnStart.IsEnabled = true;
            }

            if (i == 200)
            {
                ProgressBarAnti.Value = 200;
                labelporcen.Content = "20% completed...";
            }
            if (i == 400)
            {
                ProgressBarAnti.Value = 400;
                labelporcen.Content = "40% completed...";
            }
            if (i == 800)
            {
                ProgressBarAnti.Value = 800;
                labelporcen.Content = "80% completed...";
            }
            if (i == 900)
            {
                ProgressBarAnti.Value = 900;
                labelporcen.Content = "90% completed...";
            }


            i++;

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarAnti.Value = 0;
            labelporcen.Content = "0% completed...";
            btnStart.IsEnabled = false;
            labelporcen.Visibility = System.Windows.Visibility.Visible;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }
    }
}
