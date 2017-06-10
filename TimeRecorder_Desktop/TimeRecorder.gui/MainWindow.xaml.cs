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

            Debug.Indent();
            Debug.WriteLine("MainWindow - Initialize components!");
            Debug.Unindent();

        }

        #region WorkTimer

        /// <summary>
        /// Starting the work-timer.
        /// </summary>
        /// <param name="sender">btnStart</param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("MainWindow - Startbutton was clicked!");
            Debug.Unindent();

            if (!workingDayIsOver)
            {
                lblState.Content = "Working";
                btnStart.Visibility = Visibility.Hidden;
                btnPause.Visibility = Visibility.Visible;
                btnStop.Visibility = Visibility.Visible;

                StartTimer("btnStart_Click()", pauseTimer, "PauseTimer", workTimer, "WorkTomer");
            }
            else
            {
                Debug.WriteLine("MainWindow - Working day is over!");
                Debug.Unindent();
            }

           
        }

        /// <summary>
        /// Counts the time of the work-time.
        /// </summary>
        /// <param name="sender">btnStart_Click</param>
        /// <param name="e"></param>
        private void WorkTimer_Tick(object sender, EventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("MainWindow - 'WorkTimer_Tick'");

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
        /// Starting the pause-time.
        /// </summary>
        /// <param name="sender">btnPause</param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("MainWindow - Pausebutton was clicked!");
            Debug.Unindent();

            lblState.Content = "Pause";
            btnStart.Visibility = Visibility.Visible;
            btnPause.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Visible;

            StartTimer("btnPause_Click()", workTimer, "WorkTimer", pauseTimer, "PauseTimer");
        }

        /// <summary>
        /// Counts the time of the pause-work.
        /// </summary>
        /// <param name="sender">btnPause_Click</param>
        /// <param name="e"></param>
        private void PauseTimer_Tick(object sender, EventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("MainWindow - 'PauseTimer_Tick()'");
            
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
            Debug.WriteLine($"MainWindow - '{eventName}'");
            Debug.Unindent();
            
            try
            {
                if (stoppedTimer.IsEnabled)
                {
                    StopTimerAndAddOutput(eventName, stoppedTimer, stoppedTimerName);
                }

                if (!startedTimer.IsEnabled)
                {
                    startTime = DateTime.Now;

                    Debug.Indent();
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

        #region Stop
        
        /// <summary>
        /// This boolean is for the check, if the working day is over
        /// </summary>
        bool workingDayIsOver = false;

        /// <summary>
        /// Stops the current TimeObject (work/pause) and it is also the stop of the working day!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Debug.Indent();
            Debug.WriteLine("MainWindow - Stopbutton was clicked!");
            Debug.Unindent();

            try
            {
                if (workTimer.IsEnabled)
                    StopTimerAndAddOutput("btnStop_Click()", workTimer, "WorkTimer");
                else if (pauseTimer.IsEnabled)
                    StopTimerAndAddOutput("btnStop_Click()", pauseTimer, "PauseTimer");

                lblState.Content = "Working day is over";
                btnStart.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Hidden;
                btnStop.Visibility = Visibility.Hidden;

                workingDayIsOver = true;
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.Indent();
                Debug.WriteLine("Error in 'btnStop_Click()'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
            }
        }

        #endregion

        /// <summary>
        /// Stops the current timer, execute the Save-method of the BL, execute the Load-method of the BL 
        /// and adds the current TimeObject to the User-ListView.
        /// </summary>
        /// <param name="eventName">Name of the area where this method is called</param>
        /// <param name="stoppedTimer">Timer which should be stopped</param>
        /// <param name="stoppedTimerName">Name of the timer, which should be stopped</param>
        /// <returns></returns>
        private bool StopTimerAndAddOutput(string eventName, DispatcherTimer stoppedTimer, string stoppedTimerName)
        {
            bool result = false;

            Debug.Indent();
            Debug.WriteLine("MainWindow - 'StopTimerAndAddOutput()'");

            try
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

                    result = true;
                }

                actualTime = new TimeSpan(0, 0, 0, 0);
                Debug.WriteLine("Reset time.");
                Debug.Unindent();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Debug.WriteLine("Error in 'StopTimerAndAddOutput()'");
                Debug.WriteLine($"Error Message: {ex.Message}");
                Debug.Unindent();
            }

            return result;
        }
        
    }
}
