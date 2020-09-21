using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace FlightSimulatorApp.Controls.Joystick
{
    /// <summary>
    /// Model part of the Joystick component.
    /// </summary>
    public class JoystickModel : Model
    {
        private Client client;
        volatile private bool _stop;
        public JoystickModel(ViewModel vm) : base(vm)
        {

        }

        public void SetClient(Client c)
        {
            client = c;
        }

        public void MoveJoystick(double rudd, double elev)
        {
            Rudder = rudd;
            Elevator = elev;
            new Task(SendToClient).Start();
        }

        public void MoveSlider(double throt, double ailer)
        {
            Throttle = throt;
            Ailerion = ailer;
            new Task(SendToClient).Start();
        }

        public void Start()
        {
            _stop = false;
        }
        public void StopConnection()
        {
            _stop = true;
        }

        // Send the joystick values(rudder,elevator) to the simulator.
        public void SendToClient()
        {
            //double zero = 0;
            string rudderC = "set /controls/flight/rudder " + Rudder.ToString();
            string elevatorC = "set /controls/flight/elevator " + Elevator.ToString();
            string throttleC = "set /controls/engines/current-engine/throttle " + Throttle.ToString();
            string ailerionC = "set /controls/flight/aileron " + Ailerion.ToString();
            List<string> joystickCommands = new List<string>
            {
                rudderC,
                elevatorC,
                throttleC,
                ailerionC
            };
            try
            {
                
                if (client.IsConnected() && !_stop)
                {
                    var commands = client.SendCommands(joystickCommands);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Data);
            }
        }

        private double _rudder;
        private double _elevator;
        private double _throttle;
        private double _ailerion;

        public double Rudder
        {
            set
            {
                _rudder = value;
                NotifyPropertyChanged("Rudder");
            }
            get
            {
                return _rudder;
            }
        }

        public double Elevator
        {
            set
            {
                _elevator = value;
                NotifyPropertyChanged("Elevator");
            }
            get
            {
                return _elevator;
            }
        }

        public double Throttle
        {
            set
            {
                _throttle = value;
                NotifyPropertyChanged("Throttle");
            }
            get
            {
                return _throttle;
            }
        }

        public double Ailerion
        {
            set
            {
                _ailerion = value;
                NotifyPropertyChanged("Ailerion");
            }
            get
            {
                return _ailerion;
            }
        }
    }
}
