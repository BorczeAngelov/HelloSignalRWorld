using HelloSignalRWorld.Client.WPF.HubClients;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.ComponentModel;
using System.Windows;

namespace HelloSignalRWorld.Client.WPF
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        private bool _isConnected;
        private bool _isTextBoxLocked;
        private string _textBoxGlobalTextValue = "Hello SignalR World";
        private readonly TextBoxHubClient _textBoxHubClient;

        public MainWindowVM()
        {
            ConnectCommand = new DelegateCommand(Connect, arg => !IsConnected);
            ReleaseLockCommand = new DelegateCommand(ReleaseLock, arg => IsConnected);
            _textBoxHubClient = new TextBoxHubClient();

            _textBoxHubClient.InvokedStartEditing += () => IsTextBoxLocked = true;
            _textBoxHubClient.InvokedEditing += serverTextValue => TextBoxGlobalTextValue = serverTextValue;
            _textBoxHubClient.InvokedStopEditing += () => IsTextBoxLocked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand ConnectCommand { get; }
        public DelegateCommand ReleaseLockCommand { get; }

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

        public bool IsTextBoxLocked
        {
            get => _isTextBoxLocked;
            private set
            {
                if (_isTextBoxLocked != value)
                {
                    _isTextBoxLocked = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTextBoxLocked)));
                }
            }
        }

        public string TextBoxGlobalTextValue
        {
            get => _textBoxGlobalTextValue;
            set
            {
                if (_textBoxGlobalTextValue != value)
                {
                    _textBoxGlobalTextValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextBoxGlobalTextValue)));

                    if (!IsTextBoxLocked)
                    {
                        _textBoxHubClient.ServerHub.StartEditing();
                    }
                    _textBoxHubClient.ServerHub.Edit(value);
                }
            }
        }

        private async void Connect(object obj)
        {
            try
            {
                await _textBoxHubClient.ConnectWithServerHub();
                IsConnected = true;
                ConnectCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void ReleaseLock(object obj)
        {
            try
            {
                await _textBoxHubClient.ServerHub.StopEditing();
                ReleaseLockCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
