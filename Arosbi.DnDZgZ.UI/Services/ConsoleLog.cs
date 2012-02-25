

namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.Diagnostics;

    using WP7Contrib.Logging;

    public class ConsoleLog : ILog
    {
        private const string DateFormat = "dd-MM-yyyy HH:mm:ss.fff";

        public ILog Write(string message)
        {
            var msg = string.Format("{0} -> {1}", DateTime.Now.ToString(DateFormat), message);
            Debug.WriteLine(msg);
            return this;
        }

        public ILog Write(string message, params object[] args)
        {
            var text = string.Format(message, args);
            var msg = string.Format("{0} -> {1}", DateTime.Now.ToString(DateFormat), text);
            Debug.WriteLine(msg);
            return this;
        }

        public ILog Write(string message, Exception exception)
        {
            var msg = string.Format("{0} -> {1}", DateTime.Now.ToString(DateFormat), message);
            Debug.WriteLine(msg);
            return this;
        }

        public ILog WriteDiagnostics()
        {
            return this;
        }
    }
}
