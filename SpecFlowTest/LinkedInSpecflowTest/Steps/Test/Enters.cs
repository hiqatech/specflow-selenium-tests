using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using LinkedInSpecFlowTest.Steps.Env;
using LinkedInSpecFlowTest.Pages;

namespace LinkedInSpecFlowTest.Steps.Test
{
    [Binding]

    public class Enters
    {

        SignInPage SignInPage = new SignInPage(SetUp.driver);
        UserPage UserPage = new UserPage(SetUp.driver);

        [TestMethod]
        [Given(@"I enter (.*) as the (.*)")]
        public void GivenIEnterAsThe(string value, string element_name)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");

            switch (element_name)
            {

                case "user_name":
                    {
                        Helper.WaitToAppear(SignInPage.email_address_entry, element_name);
                        SignInPage.email_address_entry.SendKeys(value);
                        break;
                    }

                case "pass_word":
                    {
                        Helper.WaitToAppear(SignInPage.pass_word_entry, element_name);
                        SignInPage.pass_word_entry.SendKeys(value);
                        break;
                    }
                default:
                    {
                        Console.WriteLine(element_name + " element has not defined yet in the test steps or page factory");
                        break;
                    }

            }

            Console.WriteLine("I entered " + value + " into the " + element_name + " element");
        }

    }
}
