using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Configuration;

namespace AppLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ButtonData> _buttons = new List<ButtonData>();

        private DispatcherTimer _mouseMoveTimer;

        public MainWindow()
        {
            InitializeComponent();

            Mouse.OverrideCursor = Cursors.None;
            _mouseMoveTimer = new DispatcherTimer();
            _mouseMoveTimer.Tick += new EventHandler(MouseMoveTimerTick);
            RestartMouseTimer();

            for (int i = 0; i < 5; i++)
            {
                _buttons.Add(new ButtonData()); 
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
        /// Function that updates all the main app buttons.
        /// </summary>
        private void UpdateAppButtons()
        {
            Grid_MainButtons.Children.Clear();

            foreach(var b in _buttons)
            {
                Button newBtn = new Button();
                newBtn.Content = b.Name;
                newBtn.PreviewKeyDown += ButtonPreviewKeyDown;
                Grid_MainButtons.Children.Add(newBtn);
            }
        }

        private void ButtonPreviewKeyDown(object sender, KeyEventArgs e)
        {
            Button button = sender as Button;

            if (e.Key == Key.Up)
            {

              //  e.Handled = true;
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
