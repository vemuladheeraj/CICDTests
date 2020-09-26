using CICDTest.Helpers;

namespace CICDTest
{
    public class TestBase
    {
        public string[] SaveTestDetailsIfTestFailed(DriverContext driverContext)
        {
            if (driverContext.IsTestFailed)
            {
                var screenshots = driverContext.TakeScreenshot();
                //var pageSource = this.SavePageSource(driverContext);

                //var returnList = new List<string>();
                //// returnList.AddRange(screenshots);
                //if (pageSource != null)
                //{
                //    returnList.Add(pageSource);
                //}

                //return returnList.ToArray();
            }

            return null;
        }
        /*
        /// <summary>
        /// Save Page Source.
        /// </summary>
        /// <param name="driverContext">
        /// Driver context includes.
        /// </param>
        /// <returns>Path to the page source.</returns>
        public string SavePageSource(DriverContext driverContext)
        {
            return driverContext.SavePageSource(driverContext.TestTitle);
        }

        /// <summary>
        /// Fail Test If Verify Failed and clear verify messages.
        /// </summary>
        /// <param name="driverContext">Driver context includes.</param>
        /// <returns>True if test failed.</returns>
        public bool IsVerifyFailedAndClearMessages(DriverContext driverContext)
        {
            if (driverContext.VerifyMessages.Count.Equals(0))
            {
                return false;
            }

            driverContext.VerifyMessages.Clear();
            return true;
        }

        */
    }
}