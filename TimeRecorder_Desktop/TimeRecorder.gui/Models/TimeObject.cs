using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.gui.Models
{

    /// <summary>
    /// Type of time
    /// </summary>
    public enum TimeType
    {
        Work,
        Pause
    }

    public class TimeObject
    {
        /// <summary>
        /// Type of time (Enumeration)
        /// </summary>
        public TimeType TimeType { get; set; }

        /// <summary>
        /// Description of work
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Start of the time (workTime/pauseTime)
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End of the time (workTime/pauseTime)
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Time of work/pause
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }
    }
}
