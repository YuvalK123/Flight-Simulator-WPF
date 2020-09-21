using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;

namespace FlightSimulatorApp.Controls.Map
{
    /// <summary>
    /// Model part of the Map component.
    /// </summary>
    public class MapModel : Model
    {

        Client _client;
        volatile private bool _stop;
        private double latitudeLimit;
        private double longitudeLimit;
        public MapModel(ViewModel vm):base(vm)
        {
            latitudeLimit = 90;
            longitudeLimit = 180;
        }
        public void SetClient(Client c)
        {
            if(_client == null)
            {
                _client = c;
            }
         }
        public void Start()
        {
            _stop = false;
            new Task(()=> LocationFromServer()).Start();
        }
        public void StopConnection()
        {
            _stop = true;
        }
        private void LocationFromServer()
        {
            double longtitude = 0, langtitude = 0;
            string longtitudeCommand = "get /position/longitude-deg\n",
                langtitudeCommand = "get /position/latitude-deg\n";
            List<string> locationCommands = new List<string>
            {
                longtitudeCommand,
                langtitudeCommand
            };
            while (!_stop)
            {
                try
                {
                    var commands = _client.SendCommands(locationCommands);
                    StartPin = 1;
                    bool isLegal = true;
                    foreach (var element in commands)
                    {
                        string tmp = CalculateLocationParams(element.Value, element.Key);
                        bool canConvert = double.TryParse(tmp, out double zero);
                        if (!canConvert)
                        {
                            isLegal = false;
                            continue;
                        }
                        if (element.Key.Contains("latitude-deg"))
                        {
                            langtitude = Math.Round(double.Parse(tmp),3);
                        }
                        else if (element.Key.Contains("longitude-deg"))
                        {
                            longtitude = Math.Round(double.Parse(tmp), 3);
                        }
                    }
                    if (isLegal)
                    {
                        PlaneLocation = new Location(langtitude, longtitude);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Data);
                }
            }
        }

        private string CalculateLocationParams(string val, string param)
        {
            bool canConvert = double.TryParse(val, out _);
            if (!canConvert)
            {
                return val;
            }
            double doubleVal = double.Parse(val);
            if (param.Contains("latitude"))
            {
                if (doubleVal > latitudeLimit)
                {
                    doubleVal = latitudeLimit;
                }
                else if (doubleVal < (-1*latitudeLimit))
                {
                    doubleVal = (-1 * latitudeLimit);
                }
            }
            else if (param.Contains("longitude"))
            {
                if (doubleVal > longitudeLimit)
                {
                    doubleVal = longitudeLimit;
                }
                else if (doubleVal < (-1*longitudeLimit))
                {
                    doubleVal = (-1*longitudeLimit);
                }
            }
            else
            {
                return val;
            }
            
            return doubleVal.ToString();
        }

        private Location planeLocation;

        public Location PlaneLocation
        {
            get
            {
                return planeLocation;
            }
            set
            {
                planeLocation = value;
                LocationText = "";
                NotifyPropertyChanged("location");
            }
        }
        private string _locationText;
        public string LocationText
        {
            get
            {
                return _locationText;
            }
            set
            {
                if (PlaneLocation != null)
                {
                    _locationText = "Latitude:" + PlaneLocation.Latitude.ToString() + 
                        "\nLongitude " + PlaneLocation.Longitude.ToString();
                }
                else
                {
                    _locationText = value;
                }
                NotifyPropertyChanged("LocationText");
            }
        }

        private int startPin = 0;
        public int StartPin
        {
            get
            {
                return startPin;
            }
            set
            {
                startPin = value;
            
                NotifyPropertyChanged("StartPin");
            }
        }
    }
}
