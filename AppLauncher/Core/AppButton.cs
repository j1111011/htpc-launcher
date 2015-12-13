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
        private MainWindow _parentWindow;
        private Image _mainImage;
        private Image _mainBack;


        public event Action<AppButton> OnButtonFocused;
        public event Action<AppButton> OnButtonUnfocused;
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

 
        public AppButton(AppButtonData buttonData,MainWindow parentWindow)
        {
            _parentWindow = parentWindow;
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

            IsKeyboardFocusedChanged += KeyboardFocusedChanged;
        }

        private void KeyboardFocusedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Button button = sender as Button;

            if (((bool)e.NewValue) == true)
            {
                if(OnButtonFocused!=null)
                {
                    OnButtonFocused(this);
                }
            }else
            {
                if(OnButtonUnfocused!=null)
                {
                    OnButtonUnfocused(this);
                }
            }
        }

        private void OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ProcessLauncher.Instance.Launch(_data.Path,_data.WorkingPath,_data.Parameters);
            AppController.SendWindowBack(_parentWindow);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainImage = (Image)Template.FindName("Image_Main", this);

            Uri imageUri;
            bool imageValid = true;
            if (Uri.TryCreate(_data.IconPath, UriKind.Absolute, out imageUri))
            {
                try
                {
                    _mainImage.Source = new BitmapImage(imageUri);
                }
                catch (Exception e)
                {
                    imageValid = false;
                }
            }else
            {
                imageValid = false;
            }

            if(!imageValid)
            {
                _data.IconPath = "pack://application:,,,/Images/Icon.png";
                _mainImage.Source = new BitmapImage(new Uri(_data.IconPath));
            }
          
            _mainBack = (Image)Template.FindName("Image_Back", this);
            _mainBack.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Glow.png"));
        }
    }
}
