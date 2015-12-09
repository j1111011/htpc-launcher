

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using AppLauncher.Core;
using System.Windows.Media.Imaging;
using System.IO;

namespace AppLauncher
{
    public enum EAppPage
    {
        Main,
        Configuration
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private EAppPage _currentAppPage = EAppPage.Main;

        List<AppButton> _buttons = new List<AppButton>();

        private DispatcherTimer _mouseMoveTimer;

        public MainWindow()
        {
            InitializeComponent();

            SetupApplication();

            /// Remove later
            for (int i = 0; i < 5; i++)
            {
                _buttons.Add(new AppButton());
            }


            UpdateAppButtons();
           
        }

        /// <summary>
        /// Function to restart the timer of mouse hide.
        /// </summary>
        private void RestartMouseTimer()
        {
            _mouseMoveTimer.Interval = new TimeSpan(0, 0, 2);
            if (_mouseMoveTimer.IsEnabled)
            {
                _mouseMoveTimer.Stop();
            }
            else
            {
                _mouseMoveTimer.Start();
            }
 
        }

        /// <summary>
        /// Function that hides the mouse when the timer ticks
        /// </summary>
        private void MouseMoveTimerTick(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
        }

        /// <summary>
        /// Function that setup the initial state of the window
        /// </summary>
        private void SetupApplication()
        {
            Mouse.OverrideCursor = Cursors.None;
            _mouseMoveTimer = new DispatcherTimer();
            _mouseMoveTimer.Tick += new EventHandler(MouseMoveTimerTick);
            RestartMouseTimer();

            if (File.Exists(Configuration.Instance.BackgroundImagePath))
            {
                Image_Background.Source = new BitmapImage(new Uri(Configuration.Instance.BackgroundImagePath));
            }

            ConfigPageButtons();

        }

        /// <summary>
        /// Function that updates all the main app buttons.
        /// </summary>
        private void UpdateAppButtons()
        {
            Grid_MainButtons.Children.Clear();
             
            foreach(var b in _buttons)
            {
                AppButton newBtn = new AppButton();
                newBtn.Content = "Button";
                Grid_MainButtons.Children.Add(newBtn);
            }

            Grid_MainButtons.Columns = Math.Min(_buttons.Count, 6);
            Grid_MainButtons.Rows    = Math.Min(_buttons.Count, _buttons.Count/6);
        }

        private void EnterConfiguration()
        {
           // this.Stack_PageConfig.Visibility = Visibility.Visible;
            this.Grid_PageMain.Visibility = Visibility.Hidden;
            _currentAppPage = EAppPage.Configuration;

            ConfigPageButtons();
        }

        private void LeaveConfiguration()
        {
           // this.Stack_PageConfig.Visibility = Visibility.Hidden;
            this.Grid_PageMain.Visibility = Visibility.Visible;

            _currentAppPage = EAppPage.Main;

            ConfigPageButtons();
        }

        private void ConfigPageButtons()
        {
            switch(_currentAppPage)
            {
                case EAppPage.Main:
                    Button_Back.Visibility = Visibility.Hidden;
                    Button_Config.Visibility = Visibility.Visible;
                    break;
                case EAppPage.Configuration:
                    Button_Back.Visibility = Visibility.Visible;
                    Button_Config.Visibility = Visibility.Hidden;
                    break;
            }
        }

        #region HeaderButtonActions

        private void ButtonExitOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonConfigOnClick(object sender, RoutedEventArgs e)
        {
            EnterConfiguration();
        }

        private void ButtonBackOnClick(object sender, RoutedEventArgs e)
        {
            LeaveConfiguration();
        }

        private void WindowOnMouseMove(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            RestartMouseTimer();
        }

        #endregion

        private void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(Button_Exit);
        }
    }
}
