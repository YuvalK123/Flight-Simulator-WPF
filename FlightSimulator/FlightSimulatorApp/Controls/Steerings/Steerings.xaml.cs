using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Controls.Steerings
{
    /// <summary>
    /// View class of the steerings. 
    /// </summary>
    public partial class Steerings : UserControl, IView, INotifyPropertyChanged
    {
        public SteeringViewModel vm;
        public Steerings()
        {
            InitializeComponent();
            this.vm = new SteeringViewModel();
            DataContext = vm;
            SteeringsModel model = new SteeringsModel(vm);
            this.vm.SetVM(this, ref model);

        }





        public event PropertyChangedEventHandler PropertyChanged;


        public virtual void NotifyPropertyChanged(string propName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Connect(Client c)
        {
            vm.SetClient(c);
            new Task(()=>vm.Start()).Start();
        }
        public void Disconnect(Client c)
        {
            vm.Disconnect();
        }
    }





}

