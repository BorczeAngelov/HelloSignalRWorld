using HelloSignalRWorld.Utils.SignalR.TextBoxHub;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HelloSignalRWorld.Server.Hubs
{
    internal class TextBoxHub : Hub, ITextBoxHub
    {
        public async Task StartEditing()
        {
            await Clients.Others.SendAsync(nameof(ITextBoxHubClient.InvokeStartEditing));
        }

        public async Task Edit(string text)
        {
            await Clients.Others.SendAsync(nameof(ITextBoxHubClient.InvokeEditing), text);
        }

        public async Task StopEditing()
        {
            await Clients.All.SendAsync(nameof(ITextBoxHubClient.InvokeStopEditing));
        }
    }
}
