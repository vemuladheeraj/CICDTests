using NUnit.Framework;

namespace CICDTest
{
    [TestFixture]
    [Category("Nunit Demonstration")]

    public class TestMain : ProjectTestBase
    {
        [Test,Author("Dheeraj"),Category("googletest")]
        public void BasicTest()
        {
            //Need to validate
            var context = this.DriverContext;
            var OpenPage = new WikiPage(context);
            OpenPage.OpenPage().EnterSearchValue().SeachFOrSeleniumWIKILink();
        }

     
        public void BasicTestDemo()//This is waste method need to delete later
        {
            //Need to validate
            var context = this.DriverContext;
            var OpenPage = new WikiPage(context);
            OpenPage.OpenPage().EnterSearchValue();
        }

    }
}
