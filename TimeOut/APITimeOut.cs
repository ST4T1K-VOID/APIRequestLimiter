using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace APITimeOut
{
    /// <summary>
    /// times and counts the amount of request per second to prevent exceeding API request limit
    /// </summary>
    internal class TimeOut
    {
        private System.Timers.Timer timer;
        private int requestCount = 0;
        private bool timedOut = false;

        public int MaxRequestsPerSecond { get; set; }
        public bool ThrowError { get; set; } = true;

        /// <summary>
        /// initilizes a new instance of Timeout class
        /// </summary>
        /// <param name="RequestsPerSecond">set the amout of API requests that can be sent within a second</param>
        /// <param name="throwError"> true: will throw an exception when trying to send a API request while limit is exceeded,<br/>
        /// false: will return false when attempting to send an API request while the limit is exceeded </param>
        public TimeOut(int RequestsPerSecond, bool throwError)
        {
            MaxRequestsPerSecond = RequestsPerSecond;
            ThrowError = throwError;
            InitTimer();
        }

        private void InitTimer()
        {
            // sets the timer in milliseconds (100ms = 1s)
            timer = new System.Timers.Timer(100);
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// when called: checks if the API request limit has been reached/exceeded
        /// </summary>
        /// <returns>
        /// True: if request limit has not been exceeded <br/>
        /// False: if request limit has been exceeded and throwError has been set to false <br/><br/>
        /// Throws exception: if request limit has been exceeded and throwError has been set to true
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// thrown when request limit has been exceeded and throwError has been set to true
        /// </exception>
        public bool ValidateWithinLimit()
        {
            if (requestCount == MaxRequestsPerSecond && timedOut == false)
            {
                timedOut = true;
                if (ThrowError == true)
                {
                    throw new InvalidOperationException("Exceeded maximum API requests");
                }
                else
                {
                    return false;
                }
            }
            // only accessed if throwError == false
            else if (timedOut)
            {
                return false;
            }


            if (requestCount == 0)
            {
                timer.Start();
                requestCount++;
                return true;
            }
            else
            {
                requestCount++;
                return true;
            }
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            timer.Start();
            requestCount = 0;
            timedOut = false;
        }
    }
}
