using System.Threading.Tasks;

namespace HelloSignalRWorld.Utils.SignalR.TextBoxHub
{
    public interface ITextBoxHubClient
    {
        void OnStartEditing();
        void OnEditing(string text);
        void OnStopEditing();
    }

    public interface ITextBoxHub
    {
        Task StartEditing();
        Task Edit(string text);
        Task StopEditing();
    }
}
