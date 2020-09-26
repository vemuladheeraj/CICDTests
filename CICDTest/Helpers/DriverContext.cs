using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDTest.Helpers
{
    public partial class DriverContext
    {
        //private static ChromeDriverService service;
        public string TestTitle { get; set; }
        //public Collection<ErrorDetail> VerifyMessages
        //{
        //    get
        //    {
        //        return this.verifyMessages;
        //    }
        //}
        public IWebDriver driver;
        public IWebDriver Driver
        {
            get
            {
                return this.driver;
            }
        }
        public event EventHandler<DriverOptionsSetEventArgs> DriverOptionsSet;
        /// <param name="options">The options.</param>

        public object CrossBrowserEnvironment { get; private set; }
        public Screenshot TakeScreenshot()
        {
            try
            {
                var screenshotDriver = (ITakesScreenshot)this.driver;
                var screenshot = screenshotDriver.GetScreenshot();
                return screenshot;
            }
            catch (NullReferenceException)
            {
                Console.Out.WriteLine("Test failed but was unable to get webdriver screenshot.");
            }
            catch (UnhandledAlertException)
            {
                Console.Out.WriteLine("Test failed but was unable to get webdriver screenshot.");
            }

            return null;
        }
        private void SetupRemoteWebDriver()
        {
            NameValueCollection driverCapabilitiesConf = new NameValueCollection();
            NameValueCollection settings = new NameValueCollection();
            var browserType = "Chrome";//this.GetBrowserTypeForRemoteDriver(settings);
            switch (browserType)
            {
                //case "Firefox":
                //    FirefoxOptions firefoxOptions = new FirefoxOptions();
                //    firefoxOptions.Proxy = this.CurrentProxy();
                //    this.SetRemoteDriverBrowserOptions(driverCapabilitiesConf, settings, firefoxOptions);
                //    this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(firefoxOptions).ToCapabilities());
                //    break;               
                //case BrowserType.Edge:
                //    EdgeOptions egEdgeOptions = new EdgeOptions();
                //    this.SetRemoteDriverOptions(driverCapabilitiesConf, settings, egEdgeOptions);
                //    this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(egEdgeOptions).ToCapabilities());
                //    break;
                //case BrowserType.IE:
                //case BrowserType.InternetExplorer:
                //    InternetExplorerOptions internetExplorerOptions = new InternetExplorerOptions();
                //    internetExplorerOptions.Proxy = this.CurrentProxy();
                //    this.SetRemoteDriverBrowserOptions(driverCapabilitiesConf, settings, internetExplorerOptions);
                //    this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(internetExplorerOptions).ToCapabilities());
                //    break;
                case "Chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    //chromeOptions.Proxy = this.CurrentProxy();
                    //this.SetRemoteDriverBrowserOptions(driverCapabilitiesConf, settings, chromeOptions);
                    //this.driver = new RemoteWebDriver(BaseConfiguration.RemoteWebDriverHub, this.SetDriverOptions(chromeOptions).ToCapabilities());
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", this.CrossBrowserEnvironment));
            }
        }
        public void Start()
        {
            switch (BaseConfiguration.TestBrowser)
            {

                case BrowserType.Chrome:
                    if (!string.IsNullOrEmpty(BaseConfiguration.ChromeBrowserExecutableLocation))
                    {
                        this.ChromeOptions.BinaryLocation = BaseConfiguration.ChromeBrowserExecutableLocation;
                    }

                    //ChromeOptions optionss = new ChromeOptions();
                    //optionss.BinaryLocation = @"C:\Users\vemul\source\repos\AutomationCICDTest\CICDTest\packages\Selenium.WebDriver.ChromeDriver.83.0.4103.3900\driver\win32\chromedriver.exe";
                    //DesiredCapabilities cap = DesiredCapabilities.

                    //var chrome;
                    driver = string.IsNullOrEmpty(this.GetBrowserDriversFolder(BaseConfiguration.PathToChromeDriverDirectory)) ? new ChromeDriver(this.SetDriverOptions(this.ChromeOptions)) : new ChromeDriver(this.GetBrowserDriversFolder(BaseConfiguration.PathToChromeDriverDirectory), this.SetDriverOptions(this.ChromeOptions));
                    //try
                    //{
                    //    // driver = new RemoteWebDriver(new Uri("http://192.168.1.102:4444/wd/hub"), ChromeOptions);

                    //    driver = new RemoteWebDriver(new Uri("http://192.168.0.102:4444/wd/hub"), ChromeOptions.ToCapabilities());




                    //}
                    //catch (Exception t)
                    //{

                    //    Console.Out.WriteLine(t);
                    //}

                    //this.driver = string.IsNullOrEmpty(this.GetBrowserDriversFolder(BaseConfiguration.PathToChromeDriverDirectory)) ? new ChromeDriver(this.SetDriverOptions(this.ChromeOptions)) : new ChromeDriver(this.GetBrowserDriversFolder(BaseConfiguration.PathToChromeDriverDirectory), this.SetDriverOptions(this.ChromeOptions));
                    driver.Manage().Window.Maximize();
                    break;

                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", BaseConfiguration.TestBrowser));
            }

            //if (BaseConfiguration.EnableEventFiringWebDriver)
            //{
            //    this.driver = new MyEventFiringWebDriver(this.driver);
            //}
        }
        private T SetDriverOptions<T>(T options)
          where T : DriverOptions
        {
            this.DriverOptionsSet?.Invoke(this, new DriverOptionsSetEventArgs(options));
            return options;
        }
        private ChromeOptions ChromeOptions
        {
            get
            {
                ChromeOptions options = new ChromeOptions();

                // retrieving settings from config file
                NameValueCollection chromePreferences = null;
                NameValueCollection chromeExtensions = null;
                NameValueCollection chromeArguments = null;

                chromePreferences = ConfigurationManager.GetSection("ChromePreferences") as NameValueCollection;
                chromeExtensions = ConfigurationManager.GetSection("ChromeExtensions") as NameValueCollection;
                chromeArguments = ConfigurationManager.GetSection("ChromeArguments") as NameValueCollection;

                options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
                // options.AddUserProfilePreference("download.default_directory", this.DownloadFolder);
                options.AddUserProfilePreference("download.prompt_for_download", false);
                // options.BinaryLocation = @"C:\Users\vemul\source\repos\AutomationCICDTest\CICDTest\packages\Selenium.WebDriver.ChromeDriver.83.0.4103.3900\driver\win32\chromedriver.exe";
                options.BinaryLocation = BaseConfiguration.ChromeBrowserExecutableLocation;
                // set browser proxy for chrome
                if (!string.IsNullOrEmpty(BaseConfiguration.Proxy))
                {
                    //  options.Proxy = this.CurrentProxy();
                }

                // if there are any extensions
                if (chromeExtensions != null)
                {
                    // loop through all of them
                    for (var i = 0; i < chromeExtensions.Count; i++)
                    {
                        // Logger.Trace(CultureInfo.CurrentCulture, "Installing extension {0}", chromeExtensions.GetKey(i));
                        try
                        {
                            options.AddExtension(chromeExtensions.GetKey(i));
                        }
                        catch (FileNotFoundException)
                        {
                            // Logger.Trace(CultureInfo.CurrentCulture, "Installing extension {0}", this.CurrentDirectory + FilesHelper.Separator + chromeExtensions.GetKey(i));
                            //  options.AddExtension(this.CurrentDirectory + FilesHelper.Separator + chromeExtensions.GetKey(i));
                        }
                    }
                }

                // if there are any arguments
                if (chromeArguments != null)
                {
                    // loop through all of them
                    for (var i = 0; i < chromeArguments.Count; i++)
                    {
                        //Logger.Trace(CultureInfo.CurrentCulture, "Setting Chrome Arguments {0}", chromeArguments.GetKey(i));
                        options.AddArgument(chromeArguments.GetKey(i));
                    }
                }

                // custom preferences
                // if there are any settings
                if (chromePreferences == null)
                {
                    return options;
                }

                // loop through all of them
                for (var i = 0; i < chromePreferences.Count; i++)
                {
                    // Logger.Trace(CultureInfo.CurrentCulture, "Set custom preference '{0},{1}'", chromePreferences.GetKey(i), chromePreferences[i]);

                    // and verify all of them
                    switch (chromePreferences[i])
                    {
                        // if current settings value is "true"
                        case "true":
                            options.AddUserProfilePreference(chromePreferences.GetKey(i), true);
                            break;

                        // if "false"
                        case "false":
                            options.AddUserProfilePreference(chromePreferences.GetKey(i), false);
                            break;

                        // otherwise
                        default:
                            int temp;

                            // an attempt to parse current settings value to an integer. Method TryParse returns True if the attempt is successful (the string is integer) or return False (if the string is just a string and cannot be cast to a number)
                            if (int.TryParse(chromePreferences.Get(i), out temp))
                            {
                                options.AddUserProfilePreference(chromePreferences.GetKey(i), temp);
                            }
                            else
                            {
                                options.AddUserProfilePreference(chromePreferences.GetKey(i), chromePreferences[i]);
                            }

                            break;
                    }
                }

                return options;
            }
        }
        private string GetBrowserDriversFolder(string folder)
        {

            return folder;
        }
        public bool IsTestFailed { get; set; }
    }
}
