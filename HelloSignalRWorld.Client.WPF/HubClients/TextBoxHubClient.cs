using HelloSignalRWorld.Utils.SignalR;
using HelloSignalRWorld.Utils.SignalR.TextBoxHub;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace HelloSignalRWorld.Client.WPF.HubClients
{
    internal class TextBoxHubClient : ITextBoxHubClientTwoWayComm
    {
        private readonly HubConnection _connection;

        public ITextBoxHub ServerHub { get; }

        public TextBoxHubClient()
        {
            _connection = new HubConnectionBuilder()
               .WithUrl(DemoConstants.LocalHostUrl + DemoConstants.TextBoxHubEndpoint)
               .Build();

            _connection.On(nameof(InvokeStartEditing), InvokeStartEditing);
            _connection.On<string>(nameof(InvokeEditing), InvokeEditing);
            _connection.On(nameof(InvokeStopEditing), InvokeStopEditing);

            ServerHub = new ServerHubImp(_connection);
        }

        public event Action InvokedStartEditing;
        public event Action<string> InvokedEditing;
        public event Action InvokedStopEditing;

        public async Task ConnectWithServerHub()
        {
            await _connection.StartAsync();
        }

        public void InvokeStartEditing()
        {
            InvokedStartEditing?.Invoke();
        }

        public void InvokeEditing(string text)
        {
            InvokedEditing?.Invoke(text);
        }

        public void InvokeStopEditing()
        {
            InvokedStopEditing?.Invoke();
        }

        private class ServerHubImp : ITextBoxHub
        {
            private readonly HubConnection _connection;

            public ServerHubImp(HubConnection connection)
            {
                this._connection = connection;
            }

            public Task StartEditing()
            {
                return _connection.InvokeAsync(nameof(StartEditing));
            }

            public Task Edit(string text)
            {
                return _connection.InvokeAsync(nameof(Edit), text);
            }

            public Task StopEditing()
            {
                return _connection.InvokeAsync(nameof(StopEditing));
            }
        }
    }
}
