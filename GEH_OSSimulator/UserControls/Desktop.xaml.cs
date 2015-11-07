using GEH_OSSimulator.ViewModels;
using Microsoft.Win32;
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
    /// Interaction logic for Desktop.xaml
    /// </summary>
    public partial class Desktop : UserControl
    {
        private DesktopViewModel _viewModel;
        public DesktopViewModel ViewModel { 
            get {
                if (_viewModel == null)
                    _viewModel = new DesktopViewModel();
                return _viewModel;
            }
            set {
                _viewModel = value;
            }
        }
        public Desktop()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
            
        }

        private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|";
            if (fileDialog.ShowDialog() == true) {
                var backgroundImage = new Image();
                backgroundImage.Source = new BitmapImage(new Uri(fileDialog.FileName));
                var imageBrush = new ImageBrush();
                imageBrush.ImageSource = backgroundImage.Source;
                ViewModel.BackgroundGrid.Background = imageBrush;
            }
                

        }
    }
}
