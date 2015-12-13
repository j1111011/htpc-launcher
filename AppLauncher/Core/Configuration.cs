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
        private string _configurationPath = null;
 
        private string _backgroundImagePath = null;
        private bool   _fullscreen = false;
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
        #endregion

        private Configuration()
        {
            _configurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationFileName);
            //Load config:
            if (_backgroundImagePath == null)
            {
                _backgroundImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/wall-default.jpg");
            }
        }
  
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
                SaveConfiguration();
            }
        }
    }
}
