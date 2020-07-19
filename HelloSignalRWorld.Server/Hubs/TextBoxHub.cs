using HelloSignalRWorld.Utils.SignalR.TextBoxHub;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HelloSignalRWorld.Server.Hubs
{
    internal class TextBoxHub : Hub, ITextBoxHub
    {
        private bool _isBeeingEdited;

        public async Task StartEditing()
        {
            Debug.Assert(!_isBeeingEdited);
            _isBeeingEdited = true;
            await Clients.Others.SendAsync(nameof(ITextBoxHubClient.OnStartEditing));
        }

        public async Task Edit(string text)
        {
            Debug.Assert(_isBeeingEdited);
            await Clients.Others.SendAsync(nameof(ITextBoxHubClient.OnEditing), text);
        }

        public async Task StopEditing()
        {
            Debug.Assert(_isBeeingEdited);
            _isBeeingEdited = false;
            await Clients.Others.SendAsync(nameof(ITextBoxHubClient.OnStopEditing));
        }
    }
}
