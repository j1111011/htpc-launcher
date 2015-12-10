using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AppLauncher.Core
{
    public class AppButton : Button
    {
        private AppButtonData _data;

        private Image _mainImage;

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

 
        public AppButton(AppButtonData buttonData)
        {
            _data = buttonData;

            if (String.IsNullOrEmpty(_data.IconPath))
            {
                _data.IconPath = "pack://application:,,,/Images/Icon.png";
            }

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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainImage = (Image)Template.FindName("Image_Main", this);
            _mainImage.Source = new BitmapImage(new Uri(_data.IconPath));
        }
    }
}
