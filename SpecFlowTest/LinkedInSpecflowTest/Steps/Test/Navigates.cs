using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using LinkedInSpecFlowTest.Steps.Env;
using LinkedInSpecFlowTest.Pages;

namespace LinkedInSpecFlowTest.Steps.Test
{

    [Binding]

    public class Navigates
    {

        SignInPage SignInPage = new SignInPage(SetUp.driver);
        public static string webpage = null;

        [TestMethod]
        [Given(@"I navigate to the (.*) website")]
        public void GivenINavigateToTheWebsite(string webpage)
        {
            SetUp.driver.Navigate().GoToUrl(webpage);
            Console.WriteLine(" I navigate to the  " + webpage + "website");


            if (SetUp.current_driver == "IE" && webpage == "https://serv8684/Cattolica/Landing")
                SetUp.driver.Navigate().GoToUrl("javascript:document.getElementById('overridelink').click()");
        }

        [TestMethod]
        [Then(@"I should be on the (.*) page")]
        public void ThenAndIShouldBeOnThePage(string webpage)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");

            switch (webpage)
            {
                case "login":
                    {
                        Helper.WaitToAppear(SignInPage.email_address_entry, webpage);
                        break;
                    }
                default:
                    {
                        Console.WriteLine(webpage + " element has not defined yet in the test steps or page factory");
                        break;
                    }
            }
        }


      
    }
}
