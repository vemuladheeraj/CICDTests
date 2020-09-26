using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDTest.Helpers
{
    public class DriverOptionsSetEventArgs : EventArgs
    {//test chaes

        public DriverOptionsSetEventArgs(DriverOptions options)
        {
            this.DriverOptions = options;
        }
        public DriverOptions DriverOptions { get; set; }
    }
}
