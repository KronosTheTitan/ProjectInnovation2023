using System.Threading;

namespace Mirror.Transports.SimpleWeb.SimpleWeb.Common
{
    internal static class Utils
    {
        public static void CheckForInterupt()
        {
            // sleep in order to check for ThreadInterruptedException
            Thread.Sleep(1);
        }
    }
}
