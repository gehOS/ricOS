﻿using System;
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
    /// Interaction logic for NotePad.xaml
    /// </summary>
    public partial class NotePad : UserControl
    {
        private static NotePad _instance;
        public static NotePad Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NotePad();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        public NotePad()
        {
            InitializeComponent();
        }
    }
}
