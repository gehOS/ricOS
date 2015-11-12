using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GEH_OSSimulator.UserControls
{
    /// <summary>
    /// Interaction logic for OS_Loading.xaml
    /// </summary>
    public partial class OS_Loading : UserControl
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int i = 0;
        string _loadText;

        public OS_Loading()
        {
            InitializeComponent();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (i == 2000)
            {
                LabelRicOS.Visibility = System.Windows.Visibility.Hidden;
                loadingBar.Visibility = System.Windows.Visibility.Hidden;
                loadingText.Visibility = System.Windows.Visibility.Hidden;
                labelBienvenido.Visibility = System.Windows.Visibility.Visible;
            }

            if (i == 2200) {
                if (OnFinishedLoading != null)
                    OnFinishedLoading();
                dispatcherTimer.Stop();
            }


            if (i == 0)
                _loadText = "Loading drivers...";

            if (i == 500)
                _loadText = "Loading system...";

            if (i == 1000)
                _loadText = "Loading programs...";

            if (i == 1500)
                _loadText = "Loading desktop...";

            if (i == 1750)
                _loadText = "Loading icons...";

            if (i == 1900)
                _loadText = "Almost done ...";

            if (i == 1990)
            {
                _loadText = "Done ...";
            }


            loadingBar.Value = i;
            loadingText.Content = _loadText;

            i++;
         
        }
        private void gridOS_Initialized(object sender, EventArgs e)
        {
            labelBienvenido.Visibility = System.Windows.Visibility.Hidden;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        public delegate void FinishedLoading();
        public event FinishedLoading OnFinishedLoading;
    }
}
