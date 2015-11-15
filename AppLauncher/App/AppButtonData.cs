using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLauncher
{
    public struct AppButtonData
    {
        private string _name;
        private string _path;
        private string _workingPath;
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
        #endregion

        public AppButtonData(string name,string path,string workingPath,string parameters)
        {
            _name = name;
            _path = path;
            _workingPath = workingPath;
            _parameters = parameters;
        }
    }
}
