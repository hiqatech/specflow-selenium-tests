using SpecFlowFrontEndTest.Common;
using SpecFlowFrontEndTest.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;
using TechTalk.SpecFlow;
using SpecFlowFrontEndTest.Pages;

namespace SpecFlowFrontEndTest.Steps
{
    [Binding]

    public class Common
    {
        public IWebElement webelement = null;
        public string element_locator = null;

        [TestMethod]
        [Given(@"I signin with (.*) username and (.*) password")]
        public void ISignInWithUserNameAndPassword(string user_name, string pass_word)
        {
            Helper.Sleep(1000);
            
            webelement = AllPages.GetWebElement("user_name_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys(user_name);
            webelement = AllPages.GetWebElement("pass_word_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys(pass_word);
            
            webelement = AllPages.GetWebElement("signin_button");
            Helper.SafeClick(webelement, "safeclick");
           
            element_locator = AllPages.GetElementLocator("user_profile_image");
            Assert.IsTrue(Helper.WaitToAppear(element_locator, "user_profile_image"));
            Navigates.LogInStatus = "LoggedIn";
        }

        [TestMethod]
        [Then(@"I should signout")]
        public void IShouldSignOut()
        {
            webelement = AllPages.GetWebElement("user_menu_dropdown");
            Actions actions = new Actions(SetUp.driver);
            actions.MoveToElement(webelement);
            actions.Perform();
            webelement = AllPages.GetWebElement("user_menu_sign_out_button");
            webelement.Click();
            AllPages.CurrentPageName = "SignOutPage";
            //webelement = AllPages.GetWebElement("close_cooky_policy_alert");
            //webelement.Click();
            element_locator = AllPages.GetElementLocator("signed_out_message");
            Assert.IsTrue(Helper.WaitToAppear(element_locator, "signed_out_message"));
 
            Navigates.LogInStatus = "LoggedOut";
        }

        [TestMethod]
        [Given(@"I wait (.*) sec")]
        public void GivenIWaitSec(int wait)
        {
            Thread.Sleep(wait*1000);
        }

        [TestMethod]
        [When(@"I wait (.*) sec")]
        public void WhenIWaitSec(int wait)
        {
            Thread.Sleep(wait * 1000);
        }

 	[TestMethod]
        [Then(@"I wait (.*) sec")]
        public void ThenIWaitSec(int wait)
        {
            Thread.Sleep(wait*1000);
        }

	[TestMethod]
        [Given(@"I generate random (.*) char long number starts with (.*)")]
        public void GivenGenerateRandomNumber(int length , string startswith)
        {
            random_string = Helper.GenerateRandomString(length, startswith);
            Console.WriteLine(random_string);
        }

        [TestMethod]
        [When(@"I generate random (.*) char long number starts with (.*)")]
        public void WhenGenerateRandomNumber(int length, string startswith)
        {
            random_string = Helper.GenerateRandomString(length, startswith);
            Console.WriteLine(random_string);
        }

        [TestMethod]
        [Then(@"I generate random (.*) char long number starts with (.*)")]
        public void ThenGenerateRandomNumber(int length, string startswith)
        {
            random_string = Helper.GenerateRandomString(length, startswith);
            Console.WriteLine(random_string);
        }

    }

}

