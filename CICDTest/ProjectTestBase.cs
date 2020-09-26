using NUnit.Framework;
using NUnit.Framework.Interfaces;
using CICDTest.Helpers;

namespace CICDTest
{
    public class ProjectTestBase : TestBase
    {
        private readonly DriverContext driverContext = new DriverContext();
        protected DriverContext DriverContext
        {
            get
            {
                return this.driverContext;
            }
        }
        public TestContext TestContext { get; set; }
        [OneTimeSetUp]
        public void BeforeClass()
        {
            this.DriverContext.Start();
        }
        [OneTimeTearDown]
        public void AfterClass()
        {
            //PrintPerformanceResultsHelper.PrintAverageDurationMillisecondsInAppVeyor(this.DriverContext.PerformanceMeasures);
            //PrintPerformanceResultsHelper.PrintPercentiles90DurationMillisecondsInAppVeyor(this.DriverContext.PerformanceMeasures);
            //PrintPerformanceResultsHelper.PrintAverageDurationMillisecondsInTeamcity(this.DriverContext.PerformanceMeasures);
            //PrintPerformanceResultsHelper.PrintPercentiles90DurationMillisecondsinTeamcity(this.DriverContext.PerformanceMeasures);
            this.DriverContext.Driver.Quit();
        }

        [SetUp]
        public void BeforeTest()
        {
            this.DriverContext.TestTitle = TestContext.CurrentContext.Test.Name;
        }

        [TearDown]
        public void AfterTest()
        {
            this.DriverContext.IsTestFailed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed;// || !this.driverContext.VerifyMessages.Count.Equals(0);
            var filePaths = this.SaveTestDetailsIfTestFailed(this.driverContext);
        }
    }
}