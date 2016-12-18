using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using LinkedInSpecFlowTest.Steps.Env;
using System.Linq;
using LinkedInSpecFlowTest.Pages;

namespace LinkedInSpecFlowTest.Steps.Test
{

    [Binding]

    public class Selects
    {
        SignInPage SignInPage = new SignInPage(SetUp.driver);
        UserPage UserPage = new UserPage(SetUp.driver);
    

        [TestMethod]
        [Given(@"I select the (.*) element")]
        public void GivenISelectTheElement(string element_name)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");

            switch (element_name)
            {   
                case "close_cookie":
                    {
                        Helper.WaitToAppear(SignInPage.close_cookie, element_name);
                        Helper.SafeClick(SignInPage.close_cookie, "click");
                        break;
                        break;
                    }
                default:
                    {
                        Console.WriteLine(element_name + " element has not defined yet in the test steps or page factory");
                        break;
                    }

            }
            //Console.WriteLine("I have selected the " + element_name + " element");
        }

        [TestMethod]
        [When(@"I select the (.*) element")]
        public void WhenISelectTheElement(string element_name)
        {
            //Helper.WaitToDisappear(SignInPage.loading_message_locator, "loading ...");

            switch (element_name)
            {

                case "sign_in_button":
                    {
                        Helper.WaitToAppear(SignInPage.sign_in_button, element_name);
                        Helper.SafeClick(SignInPage.sign_in_button, "click");
                        break;
                    }
                default:
                    {
                        Console.WriteLine(element_name + " element has not defined yet in the test steps or page factory");
                        break;
                    }
            }

            Console.WriteLine("I have selected the " + element_name + " element");
        }


    }

}