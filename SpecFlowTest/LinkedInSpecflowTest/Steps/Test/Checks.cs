using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using LinkedInSpecFlowTest.Steps.Env;
using LinkedInSpecFlowTest.Pages;

namespace LinkedInSpecFlowTest.Steps.Test
{
    [Binding]

    public class Checks
    {

        SignInPage SignInPage = new SignInPage(SetUp.driver);
        UserPage UserPage = new UserPage(SetUp.driver);

        [TestMethod]
        [Then(@"The (.*) checkbox should be selected")]
        public void ThenTheCheckboxShouldBeSelected(string element)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");

            switch (element)
            {
                case "login_terms_and_conditions?":
                    {
                        Helper.WaitToAppear(UserPage.user_profile, "user_profile");
                        Assert.IsTrue(UserPage.user_profile.Selected);
                        break;
                    }
                default:
                    {
                        Console.WriteLine(element + " element has not defined yet in the test steps or page factory");
                        break;
                    }
            }

        }
    }
}
