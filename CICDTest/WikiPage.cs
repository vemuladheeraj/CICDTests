using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CICDTest.Helpers;
using CICDTest.Page_Objects;
using CICDTest.Types;
using System.Diagnostics;
using System.IO;
using AutoIt;
using NUnit.Framework;

namespace CICDTest
{
    public partial class WikiPage : ProjectBase
    {
        //  private DriverContext driverContext;

        public WikiPage(DriverContext driverContext) : base(driverContext)
        {
            
        }

        private readonly ElementLocator
            googleSearch = new ElementLocator(Locator.Xpath, "//input[contains(@class,'gLFyf gsfi')]");
        //basicAuthLink = new ElementLocator(Locator.XPath, "//a[contains(text(),'Auth')]"),
        //dropdownPageByLinkTextLocator = new ElementLocator(Locator.LinkText, "Dropdown"),
        //partialLinkTextLocator = new ElementLocator(Locator.PartialLinkText, "Drag");

        private readonly By search = By.XPath("//input[contains(@class,'gLFyf gsfi')]");
        private readonly By searchBtn = By.Name("btnK");
        private readonly By seleniumhqlink = By.PartialLinkText("SeleniumHQ Browser Automation");


        public WikiPage OpenPage()
        {
            var url = BaseConfiguration.GetUrlValue;
            this.Driver.NavigateTo(new Uri(url));
            return this;
        }


        public WikiPage EnterSearchValue()
        {
            this.Driver.FindElement(search).SendKeys("Selenium HQ");
            System.Threading.Thread.Sleep(2000);
            // this.Driver.FindElements(searchBtn)[0].Click();
            Console.WriteLine("Before Timeout");
            var projectDirectory = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;            
            AutoItX.WinWaitActive(title: "Google - Google Chrome", timeout: 15);
            Console.WriteLine("After Timeout");
            AutoItX.Send("{ENTER}");
            Console.WriteLine("Clicked Enter");
            return this;
        }

        public WikiPage SeachFOrSeleniumWIKILink()
        {
            
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("After Timeout of 5sec");
            this.Driver.FindElement(seleniumhqlink).Click();
            Console.WriteLine("HQ link clicked");
            System.Threading.Thread.Sleep(5000);           
            string url=this.Driver.Url;
            Assert.AreEqual("https://www.selenium.dev/", url);
            Console.Out.Write(url);
            return this;
        }




    }
}
