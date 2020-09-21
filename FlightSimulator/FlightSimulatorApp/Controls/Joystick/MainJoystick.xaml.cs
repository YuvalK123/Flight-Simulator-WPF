using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace FlightSimulatorApp.Controls.Joystick
{
    /// <summary>
    /// View part of the Map component. it contains the joystick recieved.
    /// </summary>
    public partial class MainJoystick : UserControl,IView
    {
        public JoystickViewModel vm;
        public MainJoystick()
        {
            InitializeComponent();
            this.vm = new JoystickViewModel();
            JoystickModel model = new JoystickModel(vm);
            this.vm.SetVM(this, ref model);
            joystick.updateObservers += vm.MoveJoystick;
            rudd.DataContext = vm;
            elev.DataContext = vm;
        }
        private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void Connect(Client c)
        {
            vm.SetClient(c);
            vm.Start();
        }
        public void Disconnect(Client c)
        {
            vm.Stop();
        }

        private void Slider_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        { 
            vm.MoveSlider(thorttle.Value, ailerion.Value);
        }

    }
}
