using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Controls.Steerings
{
    /// <summary>
    /// ViewModel part of the steerings.
    /// </summary>
    public class SteeringViewModel : ViewModel
    {

        public IView view;
        public SteeringsModel model;

        // Function sets this VM with the view and model.
        public void SetVM(IView v, ref SteeringsModel m)
        {
            this.view = v;
            // Model part.
            this.model = m;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public SteeringViewModel(){}

        public void Start()
        {
            model.Start();
        }
        public void Disconnect()
        {
            new Task(()=>model.StopConnection());
        }
        public void SetClient(Client c)
        {
            this.model.SetClient(c);
        }

        public string VM_HeadingDeg
        {
            get
            {
                return model.HeadingDeg;
            }
 
        }

        public string VM_VerticalSpeed
        {
            get
            {
                return model.VerticalSpeed;
            }

        }


        public string VM_GroundSpeed
        {
            get
            {
                return model.GroundSpeed;
            }
        }

        public string VM_IndicatedSpeed
        {
            get
            {
                return model.IndicatedSpeed;
            }
        }

        public string VM_GpsAltitude
        {
            get
            {
                return model.GpsAltitude;
            }
        }

        public string VM_InternalRollDeg
        {
            get
            {
                return model.InternalRollDeg;
            }
        }

        public string VM_InternalPitchDeg
        {
            get
            {
                return model.InternalPitchDeg;
            }

        }

        public string VM_AltimeterAltitude
        {
            get
            {
                return model.AltimeterAltitude;
            }
        }

    }
}
