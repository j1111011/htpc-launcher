using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLauncher.Core
{
    public class Configuration
    {
        #region Singleton Data/Functions
        private static Configuration _instance;

        public static Configuration Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Configuration();
                }
                return _instance;
            }
        }
        #endregion

        #region Get/set
        public string BackgroundImagePath
        {
            get
            {
                return _backgroundImagePath;
            }

            set
            {
                _backgroundImagePath = value;
            }
        }
        #endregion

        string _backgroundImagePath = null;

        private Configuration()
        {
            //Load config:
            if(_backgroundImagePath == null)
            {
                _backgroundImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/wall.jpg");
            }
        }
    }
}
