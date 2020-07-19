using System;
using System.Threading.Tasks;

namespace HelloSignalRWorld.Utils.SignalR.TextBoxHub
{

    public interface ITextBoxHub
    {
        Task StartEditing();
        Task Edit(string text);
        Task StopEditing();
    }

    public interface ITextBoxHubClient
    {
        void InvokeStartEditing();
        void InvokeEditing(string text);
        void InvokeStopEditing();
    }

    public interface ITextBoxHubClientTwoWayComm : ITextBoxHubClient
    {
        event Action InvokedStartEditing;
        event Action<string> InvokedEditing;
        event Action InvokedStopEditing;

        ITextBoxHub ServerHub { get; }

        Task ConnectWithServerHub();
    }
}
