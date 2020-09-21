using FlightSimulatorApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace FlightSimulatorApp
{

/**
    Main window class initialize program. 
 */
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor for main.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SetComponentsToClient();
        }

        private void SetComponentsToClient()
        {
            c.connect += map.Connect;
            c.connect += steerings.Connect;
            c.connect += joystick.Connect;
            c.disconnect += map.Disconnect;
            c.disconnect += steerings.Disconnect;
            c.disconnect += joystick.Disconnect;

        }
    }
}
