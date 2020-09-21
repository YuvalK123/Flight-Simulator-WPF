using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;


namespace FlightSimulatorApp.Controls.Map
{
    /// <summary>
    ///View Model part of the Map component.
    /// </summary>
    public class MapViewModel : ViewModel
    {
        public MapViewModel() : base()
        {
            
        }

        IView view;
        MapModel model;

        public void SetVM(IView v,  MapModel m)
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
            new Task(()=>model.StopConnection()).Start();
        }
        public Location VM_location
        {
            get
            {
                return model.PlaneLocation;
            }

        }

        public string VM_locationText
        {
            get
            {
                if(model.PlaneLocation != null)
                {
                    return "Latitude:" + model.PlaneLocation.Latitude.ToString() + 
                        "\nLongitude: " + model.PlaneLocation.Longitude.ToString();
                }
                return "";
            }
        }

        public int VM_StartPin
        {
            get
            {
                return model.StartPin;
            }
            set
            {

                NotifyPropertyChanged("VM_StartPin");
            }
        }
    }
}
