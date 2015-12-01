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

namespace GEH_OSSimulator.UserControls.Programas
{
    /// <summary>
    /// Interaction logic for Run.xaml
    /// </summary>
    public partial class RunApp : UserControl
    {
        private static RunApp _instance;
        public static RunApp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RunApp();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private RunApp()
        {
            InitializeComponent();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "Run...";
        }
    }
}
