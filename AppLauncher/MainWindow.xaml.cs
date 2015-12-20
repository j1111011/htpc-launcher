

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using AppLauncher.Core;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;

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
            Timeline.DesiredFrameRateProperty.OverrideMetadata(
            typeof(Timeline),new FrameworkPropertyMetadata { DefaultValue = Configuration.Instance.MaxFps }
            );

            Mouse.OverrideCursor = Cursors.None;
            _mouseMoveTimer = new DispatcherTimer();
            _mouseMoveTimer.Tick += new EventHandler(MouseMoveTimerTick);
            RestartMouseTimer();

            Configuration.Instance.OnDataLoaded += OnDataLoaded;

            ProcessLauncher.Instance.OnProcessLaunched += OnProcessLaunched;
            ProcessLauncher.Instance.OnProcessExited += OnProcessExit;

            Configuration.Instance.LoadConfiguration();

            if (File.Exists(Configuration.Instance.BackgroundImagePath))
            {
                Image_Background.Source = new BitmapImage(new Uri(Configuration.Instance.BackgroundImagePath));
            }

            AppController.SendWindowBack(this);
        }

        private void OnDataLoaded()
        {
            SetFullscreen(Configuration.Instance.Fullscreen);

            UpdateAppButtons();
        }

        /// <summary>
        /// Function that updates all the main app buttons and arrange them.
        /// </summary>
        private void UpdateAppButtons()
        {
            _activeButtonList.Clear();
            Grid_MainButtons.Children.Clear();

            int maxItems = Configuration.Instance.MaxItemsPerRow * Configuration.Instance.MaxRows;
            int count = 0;
            foreach (var data in Configuration.Instance.AppButtons)
            {
                if(count >= maxItems)
                {
                    break;
                }

                AppButton newBtn = new AppButton(data,this);
                newBtn.OnButtonFocused += OnAppButtonFocused;
                newBtn.OnButtonUnfocused += OnAppButtonUnfocused;
                Grid_MainButtons.Children.Add(newBtn);
                _activeButtonList.Add(newBtn);
                count++;
            }

            Grid_MainButtons.Columns = Math.Min(Configuration.Instance.AppButtons.Count, Configuration.Instance.MaxItemsPerRow);

            int itemCount = Math.Min(maxItems, Configuration.Instance.AppButtons.Count);

            Grid_MainButtons.Rows = (int)Math.Ceiling(itemCount / (float)Configuration.Instance.MaxItemsPerRow);

            FocusOnAppButtons();
        }

        private void OnProcessLaunched()
        {
            ShowAlertMessage("Application Running");
        }

        private void OnProcessExit()
        {
            // This event can be called from another thread.
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(()=>
            {
                HideAlertMessage();
            }));
        }

        /// <summary>
        /// Function called when an app button is unfocused by keyboard.
        /// </summary>
        private void OnAppButtonUnfocused(AppButton obj)
        {
            Text_App.Text = "";
        }
        /// <summary>
        /// Function called when an app button is focused by keyboard.
        /// </summary>
        private void OnAppButtonFocused(AppButton appButton)
        {
            Text_App.Text = appButton.Data.Name;
        }

        /// <summary>
        /// Function to focus on the first app button of the applicacion. Is called with a distpacher to ensure is executed later.
        /// </summary>
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

        /// <summary>
        /// Function called when the user enter to the exit selection menu
        /// </summary>
        private void EnterExitPage()
        {
            // this.Stack_PageConfig.Visibility = Visibility.Visible;
            this.Grid_PageMain.Visibility = Visibility.Hidden;
            this.Grid_PageExit.Visibility = Visibility.Visible;
            _currentAppPage = EAppPage.Exit;

            Keyboard.Focus(Button_Return);

        }

        /// <summary>
        /// Function called when the user leaves the exit selection menu
        /// </summary>
        private void LeaveExitPage()
        {
            this.Grid_PageMain.Visibility = Visibility.Visible;
            this.Grid_PageExit.Visibility = Visibility.Hidden;

            _currentAppPage = EAppPage.Main;

            FocusOnAppButtons();
        }

        /// <summary>
        /// Function to hide the bottom-center message.
        /// </summary>
        private void HideAlertMessage()
        {
            if (Configuration.Instance.ShowAppText)
            {
                Grid_Message.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Function to show and set the bottom-center message.
        /// </summary>
        private void ShowAlertMessage(String message)
        {
            if (Configuration.Instance.ShowAppText)
            {
                Grid_Message.Visibility = Visibility.Visible;
                Text_Alert.Text = message;
            }
        }

        /// <summary>
        /// Function to set the app in fullscreen Mode
        /// </summary>
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

        /// <summary>
        /// Function to toggle The fullscreen mode
        /// </summary>
        private void ToggleFullscreen()
        {
            Configuration.Instance.Fullscreen = !Configuration.Instance.Fullscreen;
            SetFullscreen(Configuration.Instance.Fullscreen);
            FocusOnAppButtons();
        }

        #region Button Actions

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
        }

        private void WindowOnMouseMove(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            RestartMouseTimer();
        }

        private void ButtonFullscreenOnClick(object sender, RoutedEventArgs e)
        {
            ToggleFullscreen();
        }

        public void ToggleFullscreen(Object sender, ExecutedRoutedEventArgs e)
        {
            ToggleFullscreen();
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

        private void ExitbuttonsIsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Button button = sender as Button;

            if(((bool)e.NewValue) == true)
            {
                Text_ExitMain.Text = (string)button.Tag;
            }
        }

        #endregion
    }
}
