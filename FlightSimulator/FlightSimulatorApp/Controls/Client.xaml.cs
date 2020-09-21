using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Controls;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Controls
{
    /// <summary>
    /// Client class connects to the remote server and sends commands with App.config IP and port.
    /// </summary>
    public partial class Client : UserControl, INotifyPropertyChanged
    {

        static TcpClient _server = null;
        static StreamReader _reader = null;
        static StreamWriter _writer = null;
        Mutex _mutex = new Mutex();
        public delegate void ConnectEvent(Client client);
        public ConnectEvent connect;
        public ConnectEvent disconnect;
        bool _stop = true;
        private int _port;
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                NotifyPropertyChanged("Port");
            }
        }
        private string _IP;
        public string Ip
        {
            get
            {
                return _IP;
            }
            set
            {
                _IP = value;
                NotifyPropertyChanged("Ip");
            }
        }

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public Client():base()
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
            Status = "offline";
            connect = ConnectFunc;
            disconnect = DisconnectFunc;
            DataContext = this;
            if (int.TryParse(ConfigurationManager.AppSettings["port"].ToString(), out int zero))
            {
                Port = int.Parse(ConfigurationManager.AppSettings["port"].ToString());
            }
            else
            {
                Port = 5402;
            }
            
            Ip = ConfigurationManager.AppSettings["IP"].ToString();
        }

        private void ConnectFunc(Client client)
        {
            
            while (!IsConnected() && !_stop)
            {
                try
                {
                    if(_server == null)
                    {
                        _server = new TcpClient(_IP, _port);
                        Status = "server connected";
                    }
                    else
                    {
                        _server.Connect(_IP, _port);
                        Status = "server connected";
                    }
                        _reader = new StreamReader(_server.GetStream());
                        _reader.BaseStream.ReadTimeout = 10000;
                        Status = "reader connected";
                        _writer = new StreamWriter(_server.GetStream());
                        Status = "writer connected";
                    Status = "connected";
                    new Task(() => DisconnectIfCrash()).Start();
                }
                catch (Exception e)
                {
                    
                    Console.WriteLine(e.Data);
                }
            }
        }

        private void DisconnectFunc(Client client)
        {
            try
            {
                if (_server != null)
                {
                    _server.Close();
                    _server = null;
                }
                if (_reader != null)
                {
                    _reader.Dispose();
                    _reader = null;
                }
                if (_writer != null)
                {
                    _writer.Dispose();
                    _writer = null;
                }
                Status = "disconnectd";

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Data);
            }
        }

        public Dictionary<string, string> SendCommands(List<string> commands)
        {
            string s, get = "get", set = "set";
            var values = new Dictionary<string, string>();

            foreach (string command in commands)
            {
                _mutex.WaitOne();
                _writer.WriteLine(command);
                _writer.Flush();
                try
                {
                    s = _reader.ReadLine();
                    if (command.Contains(get))
                    {
                        values.Add(command, s);
                    }
                    else if (command.Contains(set))
                    {
                        //If needed speciel command
                    }
                    else
                    {
                        Console.WriteLine("not legal command");
                    }

                }
                catch (TimeoutException e)
                {
                    if (!_stop)
                    {
                        Console.WriteLine(e.Data);
                        Status = "timeout";
                    }
                    else
                    {
                        throw new Exception("stop");
                    }

                    
                }
                catch (InvalidOperationException e)
                {
                    if (!_stop)
                    {
                        Console.WriteLine(e.Data);
                        Status = "Sslow reading from server.";
                        throw new Exception("stop");
                    }
                    else
                    {
                        throw new Exception("stop");
                    }
                }
                catch (Exception e)
                {
                    if (!IsConnected())
                    {
                        disconnect(this);
                        _mutex.ReleaseMutex();
                        throw new Exception("stop");

                    }
                    else if (!_stop)
                    {
                        Console.WriteLine(e.Data);
                        Status = "Slow reading from server.";
                    }

                }
                _mutex.ReleaseMutex();
            }

            return values;
        }

        private void DisconnectIfCrash()
        {
            while (true)
            {
                if (!IsConnected())
                {
                    _stop = true;
                    disconnect(this);
                    break;
                }
                else
                {
                    Thread.Sleep(2000);
                }
            }
        }

        public bool IsConnected()
        {
            if(_server != null)
            {
                return _server.Connected;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void Connect_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (_stop)
            {
                Status = "connecting";
                _stop = false;
                new Task(() => connect(this)).Start();
            }

            
        }
        private void Disonnect_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //if (!_stop)
            //{
                _stop = true;
                disconnect(this);
            //}
        }

        private void Exit_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


    }
}
