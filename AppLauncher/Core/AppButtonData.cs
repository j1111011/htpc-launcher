using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLauncher.Core
{
    public struct AppButtonData
    {
        private string _name;
        private string _path;
        private string _workingPath;
        private string _parameters;
        private string _iconPath;

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

        public string WorkingPath
        {
            get
            {
                return _workingPath;
            }

            set
            {
                _workingPath = value;
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

        public string IconPath
        {
            get
            {
                return _iconPath;
            }

            set
            {
                _iconPath = value;
            }
        }
        #endregion

        public AppButtonData(string name,string path,string workingPath,string parameters,string iconPath)
        {
            _name           = name;
            _path           = path;
            _workingPath    = workingPath;
            _parameters     = parameters;
            _iconPath       = iconPath;
        }
    }
}
