﻿using GEH_OSSimulator.UserControls;
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
            //Prod
            var osLoadingControl = new OS_Loading();
            osLoadingControl.OnFinishedLoading += osLoadingControl_OnFinishedLoading;
            Root.Children.Add(osLoadingControl);

            //Debug
            //osLoadingControl_OnFinishedLoading();
        }

        void osLoadingControl_OnFinishedLoading()
        {
            Root.Children.Clear();
            var desktopControl = new Desktop();
            desktopControl.OnProgramOpened += desktopUserControl_OnProgramOpened;
            desktopControl.OnSystemShutdown += desktopControl_OnSystemShutdown;
            desktopControl.OnHideWindow += desktopControl_OnHideWindow;
            Root.Children.Add(desktopControl);
        }

        void desktopControl_OnHideWindow(string processName)
        {
            var childWindows = (from UIElement item in Root.Children
                                where item.GetType() == typeof(ProgramChildWindow)
                                select item).Cast<ProgramChildWindow>().ToList();

            ProgramChildWindow hideChildWindow = childWindows.FirstOrDefault(x => x.ProcessName == processName);
            hideChildWindow.IsEnabled = false;
            hideChildWindow.Close();
        }

        void desktopControl_OnSystemShutdown()
        {
            Root.Children.Clear();
            var shutdown = new SystemShutdown();
            shutdown.OnClose += shutdown_OnClose;
            Root.Children.Add(shutdown);
        }

        void shutdown_OnClose()
        {
            this.Close();
        }

        void desktopUserControl_OnProgramOpened(ChildWindow programUC)
        {
            programUC.Show();
            Root.Children.Add(programUC);
        }
    }
}
