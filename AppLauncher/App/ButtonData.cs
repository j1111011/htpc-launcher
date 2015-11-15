using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLauncher
{
    public class ButtonData
    {
        private string _name;
        private string _path;
        private string _parameters;

        #region Get/Set
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
            }
        }

        public string Parameters
        {
            get
            {
                return _parameters;
            }

            set
            {
                _parameters = value;
            }
        }
        #endregion

        public ButtonData()
        {
            _name = "WinSCP";
            _path = @"D:\Tools\Winscp\WinSCP.exe";
            _parameters = "";
        }

        public ButtonData(string name,string path, string parameters = "")
        {
            _name = "WinSCP";
            _path = @"D:\Tools\Winscp\WinSCP.exe";
            _parameters = "";
        }
    }
}
