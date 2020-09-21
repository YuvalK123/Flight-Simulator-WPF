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
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.Controls.Map
{
    /// <summary>
    /// View part of the Map component.
    /// </summary>
    public partial class Map : UserControl, IView
    {

        public MapViewModel vm;
        public Map()
        {
            
            InitializeComponent();
            vm = new MapViewModel();
            DataContext = vm;
            MapModel model = new MapModel(vm);
            vm.SetVM(this, model);
        }


        public void Connect(Client c)
        {
            vm.SetClient(c);
            new Task(()=>vm.Start()).Start();
        }
        public void Disconnect(Client c)
        {
            vm.Stop();
        }
    }
}
