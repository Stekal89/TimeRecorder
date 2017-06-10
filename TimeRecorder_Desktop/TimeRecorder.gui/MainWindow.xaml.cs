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
using TimeRecorder.gui.Models;

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
        /// The start- and the endtimes for the workedtime and the pausedtime.
        /// It must be a member variable, because I need it for the saveing of the TimeObject.
        /// </summary>
        DateTime startTime = new DateTime();
        DateTime endTime = new DateTime();

        /// <summary>
        /// worked time, paused time
        /// </summary>
        TimeSpan actualTime = new TimeSpan(0,0,0,0);

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
        /// Starting the timer.
        /// </summary>
        /// <param name="sender">btnStart</param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            lblState.Content = "Working";
            btnStart.Visibility = Visibility.Hidden;
            btnPause.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Visible;

            StartTimer("btnStart_Click()", pauseTimer, "PauseTimer", workTimer, "WorkTomer");
        }

        /// <summary>
        /// Counts the time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkTimer_Tick(object sender, EventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("\nMainWindow - 'WorkTimer_Tick'");

            try
            {
                actualTime += new TimeSpan(0,0,0,1);
                lbltime.Content = actualTime.ToString();

                Debug.WriteLine($"time: {actualTime.ToString()}");
                Debug.Unindent();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.WriteLine("Error in 'WorkTimer_Tick()'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
            }
        }
        
        #endregion

        #region PauseTimer

        /// <summary>
        /// Starts the paused time.
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            lblState.Content = "Pause";
            btnStart.Visibility = Visibility.Visible;
            btnPause.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Visible;

            StartTimer("btnPause_Click()", workTimer, "WorkTimer", pauseTimer, "PauseTimer");
        }

        private void PauseTimer_Tick(object sender, EventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("\nMainWindow - 'PauseTimer_Tick()'");
            
            try
            {
                actualTime += new TimeSpan(0, 0, 0, 1);
                lbltime.Content = actualTime.ToString();

                Debug.WriteLine($"time: {actualTime.ToString()}");
                Debug.Unindent();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.WriteLine("Error in 'PauseTime_Tick()'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
            }
        }

        #endregion

        /// <summary>
        /// Checks which timer is enabled, if the wrong timer is enabled, this timer gets stopped.
        /// The other timer get be started.
        /// </summary>
        /// <param name="eventName">Name of the event, which is executed</param>
        /// <param name="stoppedTimer">stoppedTimer">The timer which has been to stop, if it is enabled</param>
        /// <param name="stoppedTimerName">The name of the timer, which has been to stop</param>
        /// <param name="startedTimer">The timer which has been to start, if it is not enabled</param>
        /// <param name="startedTimerName">The name of the timer, which has been to start</param>
        private void StartTimer(string eventName, DispatcherTimer stoppedTimer, string stoppedTimerName, DispatcherTimer startedTimer, string startedTimerName)
        {
            Debug.Indent();
            Debug.WriteLine($"\nMainWindow - '{eventName}'");
            
            try
            {
                if (stoppedTimer.IsEnabled)
                {
                    endTime = DateTime.Now;
                    stoppedTimer.Stop();
                    Debug.WriteLine($"{stoppedTimer} stopped! At: {endTime}.");

                    TimeObject to = CreateTimeObject(stoppedTimerName);

                    if (to != null)
                    {
                        /*
                        /// Save the time of the stopped timer
                        TimeManagement tm = new TimeManagement();
                        */
                        
                        /// Load data in the ListView
                        lvUser.Items.Add(to);
                    }
                    
                    actualTime = new TimeSpan(0, 0, 0, 0);
                    Debug.WriteLine("Reset time.");
                }

                if (!startedTimer.IsEnabled)
                {
                    startTime = DateTime.Now;
                    Debug.WriteLine($"{startedTimer} gets successfull started at: {startTime}");
                    Debug.Unindent();

                    startedTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.WriteLine($"Error in '{eventName}'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
            }
        }

        /// <summary>
        /// Checks the Method, which is executed and initialize a new object of TimeObject.
        /// </summary>
        /// <param name="stoppedTimeType">old timeType (WorkTimer/PauseTimer)</param>
        /// <returns>TimeObject</returns>
        private TimeObject CreateTimeObject(string stoppedTimeType)
        {
            if (stoppedTimeType == "WorkTimer")
                return new TimeObject() { TimeType = TimeType.Work, Description = "Something to work!", StartTime = startTime, EndTime = endTime, ElapsedTime = endTime - startTime };
            else
                return new TimeObject() { TimeType = TimeType.Pause, Description = "", StartTime = startTime, EndTime = endTime, ElapsedTime = endTime - startTime };
        }

            /// StopButton (btnStop) => Events

        
    }
}
