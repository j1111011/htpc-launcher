﻿

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using AppLauncher.Core;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;

namespace AppLauncher
{
    public enum EAppPage
    {
        Main,
        Exit
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private EAppPage _currentAppPage = EAppPage.Main;

        private DispatcherTimer _mouseMoveTimer;

        private List<AppButton> _activeButtonList = new List<AppButton>();

        public MainWindow()
        {
            InitializeComponent();

            SetupApplication();

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

            Configuration.Instance.OnDataLoaded += OnDataLoaded;

            Configuration.Instance.LoadConfiguration();

            if (File.Exists(Configuration.Instance.BackgroundImagePath))
            {
                Image_Background.Source = new BitmapImage(new Uri(Configuration.Instance.BackgroundImagePath));
            }

            ConfigPageButtons();
            AppController.SendWindowBack(this);
        }

        private void OnDataLoaded()
        {
            if(Configuration.Instance.AppButtons.Count == 0)
            {
                Configuration.Instance.AppButtons.Add(new AppButtonData("Example Button", @"D:\Tools\Winscp\WinSsCP.exe", @"D:\Tools\Wisnscp\", "", ""));
            }

            SetFullscreen(Configuration.Instance.Fullscreen);

            UpdateAppButtons();
        }

        /// <summary>
        /// Function that updates all the main app buttons.
        /// </summary>
        private void UpdateAppButtons()
        {
            _activeButtonList.Clear();
            Grid_MainButtons.Children.Clear();
             
            foreach(var data in Configuration.Instance.AppButtons)
            {
                AppButton newBtn = new AppButton(data,this);
                Grid_MainButtons.Children.Add(newBtn);
                _activeButtonList.Add(newBtn);
            }

            Grid_MainButtons.Columns = Math.Min(Configuration.Instance.AppButtons.Count, 6);
            Grid_MainButtons.Rows    = Math.Min(Configuration.Instance.AppButtons.Count, Configuration.Instance.AppButtons.Count/6);

            FocusOnAppButtons();
        }
        private void FocusOnAppButtons()
        {
            if (_activeButtonList.Count > 0)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(delegate ()
                {
                    Keyboard.Focus(_activeButtonList[0]);
                }));
            }
        }

        private void EnterExitPage()
        {
            // this.Stack_PageConfig.Visibility = Visibility.Visible;
            this.Grid_PageMain.Visibility = Visibility.Hidden;
            this.Grid_PageExit.Visibility = Visibility.Visible;
            _currentAppPage = EAppPage.Exit;

            ConfigPageButtons();

            Keyboard.Focus(Button_Return);

        }

        private void LeaveExitPage()
        {
            this.Grid_PageMain.Visibility = Visibility.Visible;
            this.Grid_PageExit.Visibility = Visibility.Hidden;

            _currentAppPage = EAppPage.Main;

            ConfigPageButtons();

            FocusOnAppButtons();
        }
        private void ConfigPageButtons()
        {
            switch(_currentAppPage)
            {
                case EAppPage.Main:
                    break;
               case EAppPage.Exit:
                    break;
            }
        }

        private void SetFullscreen(bool fullscreen)
        {
            if(fullscreen)
            {
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Normal;
            }
        }

        #region HeaderButtonActions

        private void ButtonExitOnClick(object sender, RoutedEventArgs e)
        {
            if(  _currentAppPage != EAppPage.Exit)
            {
                EnterExitPage();
            }
            else
            {
                LeaveExitPage();
            }
            
           // this.Close();
        }

        private void ButtonFullscreenOnClick(object sender, RoutedEventArgs e)
        {
            Configuration.Instance.Fullscreen = !Configuration.Instance.Fullscreen;
            SetFullscreen(Configuration.Instance.Fullscreen);
        }
 
        private void WindowOnMouseMove(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            RestartMouseTimer();
        }

        public void ToggleFullscreen(Object sender, ExecutedRoutedEventArgs e)
        {
            Configuration.Instance.Fullscreen = !Configuration.Instance.Fullscreen;

            SetFullscreen(Configuration.Instance.Fullscreen);
        }

        private void ButtonExitCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonExitSleepClick(object sender, RoutedEventArgs e)
        {
            LeaveExitPage();
            AppController.Sleep();
        }

        private void ButtonExitReturnClick(object sender, RoutedEventArgs e)
        {
            LeaveExitPage();
        }

        private void ButtonExitShutdownClick(object sender, RoutedEventArgs e)
        {
            Close();
            AppController.Shutdown();
        }

        #endregion

        private void ExitbuttonsIsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Button button = sender as Button;

            if(((bool)e.NewValue) == true)
            {
                Text_ExitMain.Text = (string)button.Tag;
            }
        }
    }
}
