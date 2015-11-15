using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AppLauncher
{
    public class AppButton : Button
    {
        private AppButtonData _data;


        #region Get/Set

        public AppButtonData Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }
        #endregion

        public AppButton()
        {
            _data = new AppButtonData("button", @"D:\Tools\Winscp\WinSCP.exe", @"D:\Tools\Winscp\", "");
            RegisterEvents();
        }

        public AppButton(AppButtonData buttonData)
        {
            _data = buttonData;
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            Click += OnClick;
        }

        private void OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ProcessLauncher.Instance.Launch(_data.Path,_data.WorkingPath,_data.Parameters);
        }
    }
}
