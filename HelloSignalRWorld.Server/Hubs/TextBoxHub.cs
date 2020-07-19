using HelloSignalRWorld.Server.CachedValues;
using HelloSignalRWorld.Utils.SignalR.TextBoxHub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HelloSignalRWorld.Server.Hubs
{
    internal class TextBoxHub : Hub, ITextBoxHub
    {
        public override Task OnConnectedAsync()
        {
            var cachedValue = TextBoxValueSingleton.GetInstance.TextBoxValue;
            Clients.Caller.SendAsync(nameof(ITextBoxHubClient.InvokeEditing), cachedValue);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Others.SendAsync(nameof(ITextBoxHubClient.InvokeStopEditing));
            return base.OnDisconnectedAsync(exception);
        }

        public async Task StartEditing()
        {
            await Clients.Others.SendAsync(nameof(ITextBoxHubClient.InvokeStartEditing));
        }

        public async Task Edit(string text)
        {
            TextBoxValueSingleton.GetInstance.TextBoxValue = text;
            await Clients.Others.SendAsync(nameof(ITextBoxHubClient.InvokeEditing), text);
        }

        public async Task StopEditing()
        {
            await Clients.All.SendAsync(nameof(ITextBoxHubClient.InvokeStopEditing));
        }
    }
}
