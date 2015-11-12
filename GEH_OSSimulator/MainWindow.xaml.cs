using GEH_OSSimulator.UserControls;
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
using Xceed.Wpf.Toolkit;

namespace GEH_OSSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var osLoadingControl = new OS_Loading();
            osLoadingControl.OnFinishedLoading += osLoadingControl_OnFinishedLoading;
            Root.Children.Add(osLoadingControl);
            //osLoadingControl_OnFinishedLoading();
        }

        void osLoadingControl_OnFinishedLoading()
        {
            Root.Children.Clear();
            var desktopControl = new Desktop();
            desktopControl.OnProgramOpened += desktopUserControl_OnProgramOpened;
            Root.Children.Add(desktopControl);
        }

        void desktopUserControl_OnProgramOpened(ChildWindow programUC)
        {
            programUC.Show();
            Root.Children.Add(programUC);
        }
    }
}
