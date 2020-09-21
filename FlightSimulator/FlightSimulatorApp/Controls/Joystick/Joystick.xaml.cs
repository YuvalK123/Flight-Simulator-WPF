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
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace FlightSimulatorApp.Controls.Joystick
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl, INotifyPropertyChanged
    {
        public delegate void UpdateObservers(double elev, double rudd);
        public event UpdateObservers updateObservers;
        public Point mousewDownLocation;
        public Joystick()
        {
            InitializeComponent();
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }






        private void Knob_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Stop the mouse up animation.
            Storyboard sb = (Storyboard)Knob.FindResource("CenterKnob");
            sb.Stop();

            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                mousewDownLocation = e.GetPosition(this);
                //handle the situation that the mouse is out of the knob's borders.
                Knob.CaptureMouse();
            }
        }

        private void Knob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - mousewDownLocation.X;
                double y = e.GetPosition(this).Y - mousewDownLocation.Y;
                //check if the knob is out of border or not
                if (Math.Sqrt(x * x + y * y) < Base.Width / 2)
                {
                    //change x and y position
                    knobPosition.X = x;
                    knobPosition.Y = y;
                    // standardize the rudder and elevator value between -1 to 1.
                    double rudder = Math.Round(x / (Base.Width / 2), 2);
                    double elevator = Math.Round(y / (Base.Height / 2), 2) * (-1);
                    updateObservers?.Invoke(rudder, elevator);
                }
            }
        }

        private void Knob_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //start the animation (x,y=0)
            Storyboard sb = (Storyboard)Knob.FindResource("CenterKnob");
            sb.Begin();
            Knob.ReleaseMouseCapture();
            updateObservers?.Invoke(0, 0);
        }
    }
}
