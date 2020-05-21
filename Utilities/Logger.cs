using NLog;

namespace Utilities
{
    public static class Logger
    {
        private static readonly NLog.Logger log = LogManager.GetCurrentClassLogger();

        public static void Info(string message) => log.Info(message);
    }
}
