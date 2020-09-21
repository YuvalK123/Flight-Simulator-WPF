using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Controls.Steerings
{
    /// <summary>
    /// Model part of the steerings.
    /// </summary>
    public class SteeringsModel : Model
    {
        Client client;
        Dictionary<string, string> commandsDict;
        List<string> commands;
        bool _stop;

        public SteeringsModel(ViewModel vm) : base(vm)
        {
            commandsDict = new Dictionary<string, string>();
            commands = new List<string>();
            InitDic();
            ResetValues();

        }

        public void SetClient(Client c)
        {
            client = c;
        }

        public void Start()
        {
            _stop = false;
            new Task(() => ClientValues()).Start();
        }

        public void StopConnection()
        {
            _stop = true;
        }

        private void InitDic()
        {
            commandsDict.Add("get /instrumentation/heading-indicator/indicated-heading-deg", "HeadingDeg");
            commandsDict.Add("get /instrumentation/gps/indicated-vertical-speed", "VerticalSpeed");
            commandsDict.Add("get /instrumentation/gps/indicated-ground-speed-kt", "GroundSpeed");
            commandsDict.Add("get /instrumentation/airspeed-indicator/indicated-speed-kt", "IndicatedSpeed");
            commandsDict.Add("get /instrumentation/gps/indicated-altitude-ft", "GpsAltitude");
            commandsDict.Add("get /instrumentation/attitude-indicator/internal-roll-deg", "InternalRollDeg");
            commandsDict.Add("get /instrumentation/attitude-indicator/internal-pitch-deg", "InternalPitchDeg");
            commandsDict.Add("get /instrumentation/altimeter/indicated-altitude-ft", "AltimeterAltitude");
            foreach (var key in commandsDict.Keys)
            {
                commands.Add(key);
            }
        }

        private void ResetValues()
        {
            var type = typeof(SteeringsModel);
            foreach (var element in commandsDict)
            {
                type.GetProperty(commandsDict[element.Key]).SetValue(this, "0");
                NotifyPropertyChanged(commandsDict[element.Key]);
            }

        }

        private void ClientValues()
        {
            if (client != null)
            {
                try
                {
                    while (client.IsConnected() && !_stop)
                    {
                        var type = typeof(SteeringsModel);
                        var dict = client.SendCommands(commands);
                        foreach (var element in dict)
                        {
                            
                            type.GetProperty(commandsDict[element.Key]).SetValue(this, element.Value);
                            NotifyPropertyChanged(commandsDict[element.Key]);
                        }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Data);
                }
            }
        }

        private string _headingDeg;
        public string HeadingDeg
        {
            get
            {
                return _headingDeg;
            }
            set
            {
                _headingDeg = value;
                NotifyPropertyChanged("HeadingDeg");
            }
        }

        private string _verticalSpeed;
        public string VerticalSpeed
        {
            get
            {
                return _verticalSpeed;
            }
            set
            {
                _verticalSpeed = value;
                NotifyPropertyChanged("VerticalSpeed");
            }
        }

        private string _groundSpeed;
        public string GroundSpeed
        {
            get
            {
                return _groundSpeed;
            }
            set
            {
                _groundSpeed = value;
                NotifyPropertyChanged("GroundSpeed");
            }
        }

        private string _indicatedSpeed;
        public string IndicatedSpeed
        {
            get
            {
                return _indicatedSpeed;
            }
            set
            {
                _indicatedSpeed = value;
                NotifyPropertyChanged("IndicatedSpeed");
            }
        }

        private string _gpsAltitude;
        public string GpsAltitude
        {
            get
            {
                return _gpsAltitude;
            }
            set
            {
                _gpsAltitude = value;
                NotifyPropertyChanged("GpsAltitude");
            }
        }

        private string _internalRollDeg;
        public string InternalRollDeg
        {
            get
            {
                return _internalRollDeg;
            }
            set
            {
                _internalRollDeg = value;
                NotifyPropertyChanged("InternalRollDeg");
            }
        }

        private string _internalPitchDeg;
        public string InternalPitchDeg
        {
            get
            {
                return _internalPitchDeg;
            }
            set
            {
                _internalPitchDeg = value;
                NotifyPropertyChanged("InternalPitchDeg");
            }
        }

        private string _altimeterAltitude;
        public string AltimeterAltitude
        {
            get
            {
                return _altimeterAltitude;
            }
            set
            {
                _altimeterAltitude = value;
                NotifyPropertyChanged("AltimeterAltitude");
            }
        }
    }
}
