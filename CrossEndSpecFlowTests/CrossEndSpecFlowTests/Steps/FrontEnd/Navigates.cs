using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using CrossEndSpecFlowTests.Common;

namespace CrossEndSpecFlowTests.Steps
{

    [Binding]

    public class Navigates
    {
        public static string LogInStatus = null;

        [TestMethod]
        [Given(@"I navigate to the (.*) website")]
        public void GivenINavigateTo(string page_name)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");
            SetUp.driver.Navigate().GoToUrl(page_name);
        }

        [TestMethod]
        [When(@"I navigate to the (.*) website")]
        public void WhenINavigateTo(string page_name)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");
            SetUp.driver.Navigate().GoToUrl(page_name);
        }

        [TestMethod]
        [Then(@"I navigated to the (.*) website")]
        public void ThenINavigateTo(string page_name)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");
            SetUp.driver.Navigate().GoToUrl(page_name);    
        }

        [TestMethod]
        [Given(@"I am on the (.*) page")]
        public void GivenIAmOnThePage(string page_name)
        {
            AllPages.CurrentPageName = page_name;
        }

        [TestMethod]
        [When(@"I am on the (.*) page")]
        public void WhenIAmOnThePage(string page_name)
        {
            AllPages.CurrentPageName = page_name;
        }

        [TestMethod]
        [Then(@"I am on the (.*) page")]
        public void ThenIAmOnThePage(string page_name)
        {
            AllPages.CurrentPageName = page_name;
        }
    }
}
