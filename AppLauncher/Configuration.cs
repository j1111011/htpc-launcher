using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLauncher
{
    public class Configuration
    {
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

        private Configuration()
        {

        }

 
    }
}
