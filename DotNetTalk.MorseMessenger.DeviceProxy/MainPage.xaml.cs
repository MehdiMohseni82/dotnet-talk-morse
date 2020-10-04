using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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
using DotNetTalk.MorseMessenger.Common;
using Microsoft.Extensions.Logging;


namespace DotNetTalk.MorseMessenger.DeviceProxy
{
    public sealed partial class MainPage : Page
    {
        private HubConnection _connection;
        private GpioPin _bluePin;
        private Timer timer;

        public MainPage()
        {
            this.InitializeComponent();

            InitializePins();
            Task.Run(PrepareConnection);
        }

        private async void PrepareConnection()
        {
            try
            {
                _connection = new HubConnectionBuilder()
                    .WithUrl("https://morse-message-api.dotnet-talk.com/morse-hub")
                    .ConfigureLogging(logging =>
                    {
                        logging.AddDebug();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .WithAutomaticReconnect()
                    .Build();

                _connection.On<string>("broadcastMessage", (message) =>
                {
                    if (message == null)
                    {
                        return;
                    }

                    _ = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => AppendMessage(message));

                    foreach (var c in message.ToLower())
                    {
                        var characterEnumArray = DotNetTalk.MorseMessenger.Common.MorseCodeTranslator.Get(c);
                        SendToLight(characterEnumArray);
                    }
                });

                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                AppendMessage($"Error connecting: {ex}");
            }
        }

        private void SendToLight(IEnumerable<MorseSymbol> characterEnumArray)
        {
            if (characterEnumArray == null)
            {
                return;
            }

            foreach (var character in characterEnumArray)
            {
                switch (character)
                {
                    case MorseSymbol.Dot:
                        _bluePin.Write(GpioPinValue.High);
                        Thread.Sleep(400);
                        _bluePin.Write(GpioPinValue.Low);
                        break;
                    case MorseSymbol.Dash:
                        _bluePin.Write(GpioPinValue.High);
                        Thread.Sleep(1200);
                        _bluePin.Write(GpioPinValue.Low);
                        break;
                    case MorseSymbol.Space:
                        Thread.Sleep(1200);
                        break;
                }

                Thread.Sleep(400);
            }
        }

        private void AppendMessage(string message)
        {
            MessagesListView.Items?.Add(message);
        }

        private void InitializePins()
        {
            var gpioController = GpioController.GetDefault();
            _bluePin = gpioController.OpenPin(6);

            _bluePin.SetDriveMode(GpioPinDriveMode.Output);
            _bluePin.Write(GpioPinValue.Low);
        }
    }
}