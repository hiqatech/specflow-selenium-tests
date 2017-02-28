using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.Common.Steps.FrontEnd;

namespace ProductTests.Common.Steps.FrontEnd
{

    [Binding]

    public class Navigates
    {
        public static string LogInStatus = null;

        [TestMethod]
        [Given(@"I navigate to the (.*) website")]
        [When (@"I navigate to the (.*) website")]
        [Then(@"I navigate to the (.*) website")]
        public void GivenINavigateTo(string page_name)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");
            SetUp.driver.Navigate().GoToUrl(page_name);
            AllPages.CurrentProductName = page_name;
        }     

        [TestMethod]
        [Given(@"I am on the (.*) page")]
        [When(@"I am on the (.*) page")]
        [Then(@"I am on the (.*) page")]
        public void GivenIAmOnThePage(string page_name)
        {
            AllPages.CurrentPageName = page_name;
        }
        
    }
}
