namespace HelloSignalRWorld.Server.CachedValues
{
    internal class TextBoxValueSingleton
    {
        private static TextBoxValueSingleton _instance;
        private static readonly object _instanceSyncLock = new object();

        private const string TextBoxValueStartingValue = "Hello SignalR World";
        private string _textBoxValue = TextBoxValueStartingValue;
        private readonly object _textBoxValueSyncLock = new object();

        private TextBoxValueSingleton()
        {
        }

        internal static TextBoxValueSingleton GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceSyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TextBoxValueSingleton();
                        }
                    }
                }
                return _instance;
            }
        }

        internal string TextBoxValue
        {
            get
            {
                lock (_textBoxValueSyncLock)
                {
                    return _textBoxValue;
                }
            }
            set
            {
                lock (_textBoxValueSyncLock)
                {
                    _textBoxValue = value;
                }
            }
        }
    }
}
