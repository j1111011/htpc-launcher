
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

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

        #region Other functions
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int X,
        int Y,
        int cx,
        int cy,
        uint uFlags);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;

        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        public static void SendWindowBack(Window window)
        {
            var hWnd = new WindowInteropHelper(window).Handle;
            SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
        }
        #endregion
    }
}
