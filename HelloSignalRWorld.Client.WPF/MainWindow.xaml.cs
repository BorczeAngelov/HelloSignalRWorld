using HelloSignalRWorld.Utils.SignalR;
using HelloSignalRWorld.Utils.SignalR.TextBoxHub;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.ComponentModel;
using System.Windows;

namespace HelloSignalRWorld.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly HubConnection _connection;
        private bool _isConnected;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ConnectCommand = new DelegateCommand(Connect, arg => !IsConnected);

            _connection = new HubConnectionBuilder()
               .WithUrl(DemoConstants.LocalHostUrl + DemoConstants.TextBoxHubEndpoint)
               .Build();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand ConnectCommand { get; }

        public bool IsConnected
        {
            get => _isConnected;
            private set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsConnected)));
                }
            }
        }

        private async void Connect(object obj)
        {
            _connection.On<string>(nameof(ITextBoxHubClient.OnEditing), (text) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Console.WriteLine(text);
                });
            });

            try
            {
                await _connection.StartAsync();
                Console.WriteLine("Connection started");
                IsConnected = true;
                ConnectCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
