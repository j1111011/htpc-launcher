using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AppLauncher.Core
{
    public class ProcessLauncher
    {
        #region Singleton
        private static ProcessLauncher _instance;

        public static ProcessLauncher Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ProcessLauncher();
                }
                return _instance;
            }
        }
        #endregion

        private Process _currentProcess = null;

        #region Get/Set
        public bool IsProcessLaunched
        {
            get
            {
                if(_currentProcess != null)
                {
                    return _currentProcess.HasExited;
                }
                return false;
            }
        }
        #endregion

        private ProcessLauncher()
        {
        }

        public void Launch(string launchPath,string workingDirectory,string parameters)
        {
            if(_currentProcess == null || _currentProcess.HasExited)
            {
                Console.WriteLine("Launching " + launchPath);

                Process proc = new Process();
                
                proc.StartInfo.FileName = launchPath;
                proc.StartInfo.WorkingDirectory = workingDirectory;
                proc.StartInfo.Arguments = parameters;
                proc.Exited += CurrentProcessExited;
                proc.EnableRaisingEvents = true;
                _currentProcess = proc;
                proc.Start();
            }
        }

        private void CurrentProcessExited(object sender, EventArgs e)
        {
            _currentProcess = null;
            Console.WriteLine("Current Process Exited");
        }
    }
}
