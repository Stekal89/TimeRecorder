using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TimeRecorder.gui
{
    public class TimeManagement
    {
        /// <summary>
        /// Save the worked time in the database.
        /// </summary>
        /// <returns>if successful saved => true, else false</returns>
        public bool SaveTime(int timeType, DateTime startTime, DateTime endTime, TimeSpan elapsedTime)
        {
            Debug.WriteLine("\tTimeManagement - 'SaveTime()'");

            /// Save the startTime in the database
            
            /// Save the endTime in the database

            /// Save the elapsedTime in the database

            return false;
        }

        /// <summary>
        /// Save the paused time in the Database.
        /// </summary>
        /// <returns>if successful saved => true, else false</returns>
        public bool PausedTimeSave()
        {
            Debug.WriteLine("\tTimeManagement - 'LoadTime()"); 

            /// Load startTime from the database
           
            /// Load endTime from the database
            
            /// Load elapsedTime from the database

            /// return a timeObject (Worktimme/PauseTime)

            return false;
        }
    }
}
