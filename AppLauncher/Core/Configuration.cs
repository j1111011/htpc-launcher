using Newtonsoft.Json;
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

        private const string ConfigurationFileName = "configuration.cfg";

        private string      _configurationPath = null;
        private string      _backgroundImagePath = null;
        private int         _maxItemsPerRow;
        private int         _maxRows;
        private int         _maxFps;
        private bool        _fullscreen;
        private bool        _showAppText;

        private List<AppButtonData> _appButtons = new List<AppButtonData>();

        public event Action OnDataSaved;
        public event Action OnDataLoaded;

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

        public List<AppButtonData> AppButtons
        {
            get
            {
                return _appButtons;
            }

            set
            {
                _appButtons = value;
            }
        }

        public bool Fullscreen
        {
            get
            {
                return _fullscreen;
            }

            set
            {
                _fullscreen = value;
            }
        }
        public bool ShowAppText
        {
            get
            {
                return _showAppText;
            }

            set
            {
                _showAppText = value;
            }
        }
        public int MaxItemsPerRow
        {
            get
            {
                return _maxItemsPerRow;
            }

            set
            {
                _maxItemsPerRow = value;
            }
        }

        public int MaxRows
        {
            get
            {
                return _maxRows;
            }

            set
            {
                _maxRows = value;
            }
        }

        public int MaxFps
        {
            get
            {
                return _maxFps;
            }

            set
            {
                _maxFps = value;
            }
        }


        #endregion

        private Configuration()
        {
            _configurationPath      = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationFileName);
            _maxItemsPerRow         = 6;
            _maxRows                = 3;
            _maxFps                 = 30;
            _fullscreen             = false;
            _backgroundImagePath    = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/wall-default.jpg");

        }

        /// <summary>
        /// Function to create a new config file. This is only called when there is no config file.
        /// </summary>
        public void CreateConfiguration()
        {
            Configuration.Instance.AppButtons.Clear();
            Configuration.Instance.AppButtons.Add(new AppButtonData("Example Button 1", "", "", "", ""));
            Configuration.Instance.AppButtons.Add(new AppButtonData("Example Button 2", "", "", "", ""));
            Configuration.Instance.AppButtons.Add(new AppButtonData("Example Button 3", "", "", "", ""));
            SaveConfiguration();
        }

        /// <summary>
        /// Function to save and overrite the configuration
        /// </summary>
        public void SaveConfiguration()
        {
            string output = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(_configurationPath, output);

            if(OnDataSaved != null)
            {
                OnDataSaved();
            }
        }

        public void LoadConfiguration()
        {
            if (File.Exists(_configurationPath))
            {
                string input = File.ReadAllText(_configurationPath);
                try
                {
                    _instance = JsonConvert.DeserializeObject<Configuration>(input);
                    if (OnDataLoaded != null)
                    {
                        OnDataLoaded();
                    }
                }
                catch(JsonException e)
                {
                   // SaveConfiguration();
                }
            }else
            {
                CreateConfiguration();
            }
        }
    }
}
