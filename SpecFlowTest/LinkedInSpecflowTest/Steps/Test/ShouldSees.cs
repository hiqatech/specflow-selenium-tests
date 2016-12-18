using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using System;
using LinkedInSpecFlowTest.Steps.Env;
using LinkedInSpecFlowTest.Pages;

namespace LinkedInSpecFlowTest.Steps.Test
{

    [Binding]

    public class ShouldSees
    {
        public static string loginStatus = null;

        SignInPage SignInPage = new SignInPage(SetUp.driver);
        UserPage UserPage = new UserPage(SetUp.driver);
        SignOutPage SignOutPage = new SignOutPage(SetUp.driver);

        [TestMethod]
        [Then(@"I should see the (.*) element")]
        public void ThenIShouldSeeTheElement(string element)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");

            switch (element)
            {
                case "user_profile":
                    {        
                        Helper.WaitToAppear(UserPage.user_profile, element);
                        Helper.TakeScreenShot();
                        loginStatus = "LoggedIn";
                        break;
                    }

                case "sign_in":
                    {
                        Helper.WaitToAppear(SignOutPage.sign_in, element);
                        Helper.TakeScreenShot();
                        loginStatus = "LoggedOut";
                        break;
                    }
                default:
                    {
                        Console.WriteLine(element + " element has not defined in the test steps");
                        break;
                    }

            }

            Console.WriteLine("I have checked the " + element + " element is visible");
        }
    }
}
