using GEH_OSSimulator.UserControls.Programas;
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
using System.Windows.Threading;

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
            CreateProgramIcon(@"http://icons.iconarchive.com/icons/dakirby309/windows-8-metro/256/Apps-Task-Manager-alt-2-Metro-icon.png", TaskManager.Instance , "TaskManager");
            CreateProgramIcon(@"http://icons.iconarchive.com/icons/tpdkdesign.net/refresh-cl/256/Windows-Run-icon.png", RunApp.Instance, "Run");
            CreateProgramIcon(@"https://cdn2.iconfinder.com/data/icons/metro-uinvert-dock/256/Other_Antivirus_Software.png", Antivirus.Instance, "Antivirus");
            CreateProgramIcon(@"https://upload.wikimedia.org/wikipedia/en/2/2a/Notepad.png", NotePad.Instance, "Notepad");
            DispatcherTimer dt = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.lblHora.Content = DateTime.Now.ToString("HH:mm:ss");
            }, this.Dispatcher);
            StartMenuGrid.Visibility = System.Windows.Visibility.Collapsed;

        }

        void CreateProgramIcon(string url, UserControl programUc, string processName) {
            var taskIcon = new Image();
            taskIcon.Source = new BitmapImage(new Uri(url));
            var imageBrush = new ImageBrush();
            imageBrush.ImageSource = taskIcon.Source;
            var taskManager = new ProgramIcon() { 
                ProgramUC = new ProgramChildWindow(),
                ProcessName = processName
            };
            taskManager.ProgramUC.IsEnabled = false;
            taskManager.ProgramUC.ProcessName = processName;
            taskManager.ProgramUC.Root.Children.Add(programUc);
            taskManager.IconImage.Source = imageBrush.ImageSource;

            if(programUc.GetType() == typeof(TaskManager))
                ((TaskManager)programUc).OnProcessClosed += Desktop_OnProcessClosed;

            taskManager.OnLoadUserControl += taskManager_OnLoadUserControl;
            IconPanel.Children.Add(taskManager);
            
        }

        void Desktop_OnProcessClosed(string processName)
        {
            if (OnHideWindow != null)
                OnHideWindow(processName);
        }

        public delegate void HideWindow(string processName);
        public event HideWindow OnHideWindow;

        void taskManager_OnLoadUserControl(ProgramChildWindow userControl)
        {
            if (!userControl.IsEnabled)
            {
                TaskManager.Instance.Ejecutar(userControl.ProcessName);
                userControl.IsEnabled = true;
            }
            if (userControl.Parent == null)
            {
                Panel.SetZIndex(userControl, Singleton.ZIndex);
                if (OnProgramOpened != null)
                    OnProgramOpened(userControl);
                userControl.MouseLeftButtonDown += userControl_MouseLeftButtonDown;
                userControl.GotFocus += userControl_GotFocus;
                userControl.HeaderDragDelta += userControl_HeaderDragDelta;
                userControl.OnProgramHidden += userControl_OnProgramHidden;
                
            }
            else {
                userControl.Show();
                
            }

        }

        void userControl_OnProgramHidden(string processName)
        {
            TaskManager.Instance.CerrarProceso(processName);
        }

        void userControl_HeaderDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            ((UIElement)sender).Focus();
        }

        void userControl_GotFocus(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex((UIElement)sender, Singleton.ZIndex);
        }

        void userControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).Focus();
        }




        private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png;";
            if (fileDialog.ShowDialog() == true) {
                var backgroundImage = new Image();
                backgroundImage.Source = new BitmapImage(new Uri(fileDialog.FileName));
                var imageBrush = new ImageBrush();
                imageBrush.ImageSource = backgroundImage.Source;
                BackgroundGrid.Background = imageBrush;
            }
        }

        public delegate void ProgramOpened(ProgramChildWindow programUC);
        public event ProgramOpened OnProgramOpened;

        private void HomeButton_Checked(object sender, RoutedEventArgs e)
        {
            StartMenuGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void HomeButton_Unchecked(object sender, RoutedEventArgs e)
        {
            StartMenuGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea apagar el sistema?", "Apagado de sistema", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) {
                if (OnSystemShutdown != null)
                    OnSystemShutdown();
            }
        }

        public delegate void SystemShutdown();
        public event SystemShutdown OnSystemShutdown;

    }
}
