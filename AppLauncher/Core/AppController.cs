
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AppLauncher.Core
{
    public class AppController
    {
        [DllImport("user32")]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        [DllImport("user32")]
        private static extern void LockWorkStation();

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public static void Reboot()
        {
            Process.Start("shutdown","/r /t 0");
        }

        public static void Shutdown()
        {
            Process.Start("shutdown", "/s /t 0"); 
        }

        public static void Sleep()
        {
            SetSuspendState(false, true, true);
        }

        public static void Hibernate()
        {
            SetSuspendState(true, true, true);
        }

        public static void LogOff()
        {
            ExitWindowsEx(0, 0);
        }

        public static void Lock()
        {
            LockWorkStation();
        }
    }
}
