

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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
        }

        #region HeaderButtonActions

        private void ButtonExitOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonConfigOnClick(object sender, RoutedEventArgs e)
        {
          //  TabControl_Main.SelectedItem = TabItem_Config;
        }

        private void ButtonBackOnClick(object sender, RoutedEventArgs e)
        {
            //TabControl_Main.SelectedItem = TabItem_Main;
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
