using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CICDTest.Helpers;

namespace CICDTest.Page_Objects
{
    public partial class ProjectBase
    {
        public ProjectBase(DriverContext driverContext)
        {
            this.DriverContext = driverContext;
            this.Driver = driverContext.Driver;
        }

        protected IWebDriver Driver { get; set; }
        protected DriverContext DriverContext { get; set; }
    }
}
