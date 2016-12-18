using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkedInSpecFlowTest.Steps.Env;
using System.Threading;
using TechTalk.SpecFlow;
using LinkedInSpecFlowTest.Pages;
using OpenQA.Selenium;

namespace LinkedInSpecFlowTest.Steps.Test
{
    [Binding]

    public class Common
    {
        public static string loginStatus = null;
        SignInPage SignInPage = new SignInPage(SetUp.driver);
        UserPage UserPage = new UserPage(SetUp.driver);
        SignOutPage SignOutPage = new SignOutPage(SetUp.driver);

        [TestMethod]
        [Given(@"I login with (.*) username and (.*) password")]
        public void ILoginWithUserNameAndPassword(string user_name, string pass_word)
        {    
            Helper.WaitToAppear(SignInPage.email_address_entry, "user_name");
            SignInPage.email_address_entry.SendKeys(user_name);
            Helper.WaitToAppear(SignInPage.pass_word_entry, "pass_word");
            SignInPage.pass_word_entry.SendKeys(pass_word);
            Helper.WaitToAppear(SignInPage.sign_in_button, "login_button");
            SignInPage.sign_in_button.Click();
            Helper.WaitToAppear(UserPage.user_profile, "user_profile");
            loginStatus = "LoggedIn";
        }

        [TestMethod]
        [Then(@"I should logout")]
        public void IShouldLogOut()
        {
            Helper.WaitToAppear(UserPage.user_profile_dropdown, "user_profile_dropdown");
            UserPage.user_profile_dropdown.Click();
            Helper.Sleep(2);
            UserPage.sign_out.Click();
            Helper.WaitToAppear(SignOutPage.sign_in, "sign_in");
            loginStatus = "LoggedOut";
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

    }

}

