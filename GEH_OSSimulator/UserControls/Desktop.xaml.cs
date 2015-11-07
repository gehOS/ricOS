﻿using GEH_OSSimulator.ViewModels;
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
            CreateProgramIcon(@"http://icons.iconarchive.com/icons/dakirby309/windows-8-metro/256/Apps-Task-Manager-alt-2-Metro-icon.png", "taskManager", "Task Manager");
            CreateProgramIcon(@"http://icons.iconarchive.com/icons/tpdkdesign.net/refresh-cl/256/Windows-Run-icon.png", "run", "Run");
            CreateProgramIcon(@"https://cdn2.iconfinder.com/data/icons/metro-uinvert-dock/256/Other_Antivirus_Software.png", "antivirus", "Antivirus");
            CreateProgramIcon(@"https://upload.wikimedia.org/wikipedia/en/2/2a/Notepad.png", "notepad", "Notepad");
            
        }

        void CreateProgramIcon(string url, string programString, string processName) {
            var taskIcon = new Image();
            taskIcon.Source = new BitmapImage(new Uri(url));
            var imageBrush = new ImageBrush();
            imageBrush.ImageSource = taskIcon.Source;
            var taskManager = new ProgramIcon() { 
                ProgramString = programString,
                ProcessName = processName
            };
            taskManager.IconImage.Source = imageBrush.ImageSource;
            
            taskManager.ButtonSelector.MouseDoubleClick += taskManager_MouseDoubleClick;
            IconPanel.Children.Add(taskManager);
            
        }

        void taskManager_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
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
    }
}
