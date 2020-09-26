using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDTest.Helpers
{
    public static class BaseConfiguration
    {
        public static BrowserType TestBrowser
        {
            get
            {
                bool supportedBrowser = false;
                string setting = null;
                setting = ConfigurationManager.AppSettings["browser"];
                supportedBrowser = Enum.TryParse(setting, out BrowserType browserType);
                if (supportedBrowser)
                {
                    return browserType;
                }

                return BrowserType.None;
            }

        }
        public static string GetUrlValue
        {
            get
            {
                //Logger.Trace(CultureInfo.CurrentCulture, "Get Url value from settings file '{0}://{1}{2}'", Protocol, Host, Url);
                return string.Format(CultureInfo.CurrentCulture, "{0}://{1}{2}", Protocol, Host, Url);
            }
        }

        public static double MediumTimeout
        {
            get
            {
                double setting;

                // setting = Convert.ToDouble(ConfigurationManager.AppSettings["mediumTimeout"], CultureInfo.CurrentCulture);
                setting = Convert.ToDouble("4.2", CultureInfo.CurrentCulture);
                // Logger.Trace(CultureInfo.CurrentCulture, "Gets the mediumTimeout from settings file '{0}'", setting);
                return setting;
            }
        }
        public static string ChromeBrowserExecutableLocation
        {
            get
            {
                string setting = null;

                setting = "";
                //ConfigurationManager.AppSettings["ChromeBrowserExecutableLocation"];

                //Logger.Trace(CultureInfo.CurrentCulture, "Gets the path and file name of the Chrome browser executable from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }
        public static string PathToChromeDriverDirectory
        {
            get
            {
                string setting = null;

                setting = ConfigurationManager.AppSettings["PathToChromeDriverDirectory"];


                // Logger.Trace(CultureInfo.CurrentCulture, "Path to the directory containing Chrome Driver from settings file '{0}'", setting);
                if (string.IsNullOrEmpty(setting))
                {
                    return string.Empty;
                }

                return setting;
            }
        }
        public static string Protocol
        {
            get
            {
                string setting = "https";
                // setting = ConfigurationManager.AppSettings["protocol"];

                //Logger.Trace(CultureInfo.CurrentCulture, "Gets the protocol from settings file '{0}'", setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the application host name.
        /// </summary>
        public static string Host
        {
            get
            {
                string setting = "www.google.com";
                // setting = ConfigurationManager.AppSettings["host"];

                //Logger.Trace(CultureInfo.CurrentCulture, "Gets the protocol from settings file '{0}'", setting);
                return setting;
            }
        }


        public static string Url
        {
            get
            {
                string setting = "";

                setting = ConfigurationManager.AppSettings["url"];

                // Logger.Trace(CultureInfo.CurrentCulture, "Gets the url from settings file '{0}'", setting);
                return setting;
            }
        }

        public static string Proxy
        {
            get
            {
                string setting = null;

                setting = ConfigurationManager.AppSettings["proxy"];

                //Logger.Trace(CultureInfo.CurrentCulture, "Gets the url from settings file '{0}'", setting);
                return setting;
            }
        }
    }
}
