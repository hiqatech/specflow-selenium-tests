using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.Pages;

namespace ProductTests.Common.Steps.FrontEnd
{
    [Binding]

    public class Common
    {
        public IWebElement webelement = null;
        public string element_locator = null;
        public static string random_string = null;

        [TestMethod]
        [Given(@"I login with (.*) username and (.*) password")]
        [When(@"I login with (.*) username and (.*) password")]
        [Then(@"I login with (.*) username and (.*) password")]
        public void ILoginWithUserNameAndPassword(string user_name, string pass_word)
        {
            Helper.Sleep(1200);
            webelement = AllPages.GetWebElement("super_user_login_button");
            webelement.Click();
            webelement = AllPages.GetWebElement("user_name_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys(user_name);
            webelement = AllPages.GetWebElement("pass_word_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys(pass_word);
            webelement = AllPages.GetWebElement("security_question_school_age_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys("x");
            webelement = AllPages.GetWebElement("security_question_school_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys("x");
            webelement = AllPages.GetWebElement("security_question_color_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys("x");
            webelement = AllPages.GetWebElement("security_question_number_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys("x");
            webelement = AllPages.GetWebElement("security_question_music_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys("x");
            webelement = AllPages.GetWebElement("accept_cookies_button");
            Helper.SafeClick(webelement, "safeclick");
            webelement = AllPages.GetWebElement("login_button");
            Helper.SafeClick(webelement, "safeclick");
            webelement = AllPages.GetWebElement("accept_regulatory_button");
            Helper.SafeClick(webelement, "safeclick");
            element_locator = AllPages.GetElementLocator("user_profile_welcome_text");
            Assert.IsTrue(Helper.WaitToAppear(element_locator, "user_profile_welcome_text"));
            Navigates.LogInStatus = "LoggedIn";
        }

        [TestMethod]
        [Given(@"I should logout")]
        [When(@"I should logout")]
        [Then(@"I should logout")]
        public void IShouldLogOut()
        {
            webelement = SetUp.driver.FindElement(By.LinkText("log_out_button"));
            Helper.SafeClick(webelement, "safeclick");
            webelement = AllPages.GetWebElement("user_name_entry");
            Assert.IsTrue(Helper.isDisplayed(webelement, "user_name_entry"));
 
            Navigates.LogInStatus = "LoggedOut";
        }

       

    }

}

