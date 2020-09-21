using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.Controls.Joystick
{
    /// <summary>
    /// ViewModel part of the Map component.
    /// </summary>
    public class JoystickViewModel : ViewModel
    {
        JoystickModel model;
        IView view;


        public JoystickViewModel()
        {

        }


        public void MoveJoystick(double rudd, double elev)
        {
            model.MoveJoystick(rudd,elev);
        }
        public void MoveSlider(double throttle, double ailerion)
        {
            model.MoveSlider(throttle, ailerion);
        }

        public void SetVM(IView v, ref JoystickModel m)
        {
            this.view = v;

            //model
            this.model = m;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void SetClient(Client c)
        {
            model.SetClient(c);
        }
        public void Start()
        {
            model.Start();
        }
        public void Stop()
        {
            model.StopConnection();
        }

        private double _VM_rudder;
        private double _VM_elevator;
        private double _VM_throttle;
        private double _VM_ailerion;
        public double VM_rudder
        {

            get
            {
                return model.Rudder;
            }
            set
            {

                _VM_rudder = value;
                NotifyPropertyChanged("VM_Rudder");
            }
        }

        public double VM_elevator
        {
            set
            {
                _VM_elevator = value;
                NotifyPropertyChanged("VM_Elevator");
            }
            get
            {
                return model.Elevator;
            }
        }

        public double VM_throttle
        {
            set
            {
                _VM_throttle = value;
                NotifyPropertyChanged("VM_Throttle");
            }
            get
            {
                return model.Throttle;
            }
        }

        public double VM_ailerion
        {
            set
            {
                _VM_ailerion = value;
                NotifyPropertyChanged("VM_Ailerion");
            }
            get
            {
                return model.Ailerion;
            }
        }

    }
}
