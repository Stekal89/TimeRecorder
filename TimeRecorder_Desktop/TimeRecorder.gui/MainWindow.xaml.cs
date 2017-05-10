using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TimeRecorder.gui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialize the timer
        /// </summary>
        DispatcherTimer workTimer = new DispatcherTimer();
        DispatcherTimer pauseTimer = new DispatcherTimer();


        /// <summary>
        /// worked time, paused time
        /// </summary>
        TimeSpan time = new TimeSpan(0,0,0,0,0);

        public MainWindow()
        {
            InitializeComponent();

            workTimer.Interval = new TimeSpan(0, 0, 0, 1);
            workTimer.Tick += WorkTimer_Tick;

            pauseTimer.Interval = new TimeSpan(0, 0, 0, 1);
            pauseTimer.Tick += PauseTimer_Tick;

        }

        #region WorkTimer

        /// <summary>
        /// Counts the time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Debug.Indent();
                Debug.WriteLine("MainWindow - 'WorkTimer_Tick'");

                time += new TimeSpan(0,0,0,1);
                lblState.Content = "Working";
                lbltime.Content = time.ToString();
                btnStart.Visibility = Visibility.Hidden;
                btnPause.Visibility = Visibility.Visible;
                btnStop.Visibility = Visibility.Visible;

                Debug.Indent();
                Debug.WriteLine($"time: {time.ToString()}");
                Debug.Unindent();
                Debug.Unindent();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.Indent();
                Debug.WriteLine("Error in 'WorkTimer_Tick()'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
                Debug.Unindent();
            }
        }
        
        /// <summary>
        /// Starting the timer.
        /// </summary>
        /// <param name="sender">btnStart</param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("MainWindow - 'btnStart_Click()'");

            try
            {
                if (!workTimer.IsEnabled)
                {
                    Debug.Indent();
                    Debug.WriteLine("Timer gets successfull started");
                    Debug.Unindent();
                    workTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.Indent();
                Debug.WriteLine("Error in 'btnStart_Click()'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
                Debug.Unindent();
            }
        }

        #endregion

        #region PauseTimer

        private void PauseTimer_Tick(object sender, EventArgs e)
        {
            Debugger.Break();
            Debug.Indent();
            Debug.WriteLine("MainWindow - 'PauseTimer_Tick()'");

            try
            {

                Debug.Unindent();
                Debug.Unindent();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.Indent();
                Debug.WriteLine("Error in 'PauseTimer_Tick()'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
                Debug.Unindent();
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("MainWindow - 'btnPause_Click()'");

            try
            {
                if (!pauseTimer.IsEnabled)
                {
                    Debug.Indent();
                    Debug.WriteLine("Timer gets successfull paused");
                    Debug.Unindent();
                    Debug.Unindent();
                    pauseTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.Indent();
                Debug.WriteLine("Error in 'btnPause_Click()'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
                Debug.Unindent();
            }
        }

        #endregion
    }
}
