using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=402347&clcid=0x409

namespace Blinky
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private GpioPin _bluePin;
        private Timer timer;
        private HubConnection _connection;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            //timer = new Timer(timerCallback, null, 2000, 2);
            InitGPIO();

            _connection = new HubConnectionBuilder()
                .WithUrl("http://192.168.0.108/chat")
                .ConfigureLogging(logging =>
                {
                    logging.AddDebug();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>("broadcastMessage", (s, r) =>
            {
                _bluePin.Write(GpioPinValue.Low);

                System.Threading.Thread.Sleep(2000);

                _bluePin.Write(GpioPinValue.High);
            });

            Task.Run(() => _connection.StartAsync());

            //timer = new Timer(timerCallback, null, 2000, 2);
            //InitGPIO();
            //if (_bluePin != null)
            //{

            //}
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            _bluePin = gpio.OpenPin(6);

            _bluePin.SetDriveMode(GpioPinDriveMode.Output);
        }

        private int c;

        private void timerCallback(object state)
        {
            c++;
            if ((c % 2) == 0)
            {
                //_redPin.Write(GpioPinValue.Low);
                //_bluePin.Write(GpioPinValue.High);
                _bluePin.Write(GpioPinValue.High);
            }
            else
            {
                _bluePin.Write(GpioPinValue.Low);
            }
        }
    }
}
