using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using LinkedInSpecFlowTest.Steps.Env;
using LinkedInSpecFlowTest.Pages;

namespace LinkedInSpecFlowTest.Steps.Test
{

    [Binding]
    
    public class ShouldNSees
    {

        public static string loginStatus = null;

        SignInPage SignInPage = new SignInPage(SetUp.driver);
        UserPage UserPage = new UserPage(SetUp.driver);
        SignOutPage SignOutPage = new SignOutPage(SetUp.driver);

        [TestMethod]
        [Then(@"I should not see the (.*) element")]
        public void ThenIShouldNotSeeTheElement(string element)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");

            switch (element)
            {
                case "user_name":
                    {
                        Helper.WaitToDisappear(SignInPage.email_address_entry, element);
                        Helper.TakeScreenShot();
                        loginStatus = "LoggedOut";
                        break;
                    }

                case "user_profile":
                    {
                        Helper.WaitToDisappear(UserPage.user_profile, element);
                        Helper.TakeScreenShot();
                        loginStatus = "LoggedOut";
                        break;
                    }
                case "sign_in":
                    {
                        Helper.WaitToDisappear(SignOutPage.sign_in, element);
                        Helper.TakeScreenShot();
                        loginStatus = "LoggedIn";
                        break;
                    }
                default:
                    {
                        Console.WriteLine(element + " element has not defined yet in the test steps or page factory");
                        break;
                    }

            }

            Console.WriteLine("I have checked the " + element + " element is not visible");
        }

    }
}
