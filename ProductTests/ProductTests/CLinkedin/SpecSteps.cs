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
        public void ISignInWithUserNameAndPassword(string user_name, string pass_word)
        {
            Helper.Sleep(1200);
            webelement = AllPages.GetWebElement("user_name_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys(user_name);
            webelement = AllPages.GetWebElement("pass_word_entry");
            Helper.SafeClick(webelement, "safeclick");
            webelement.SendKeys(pass_word);    
            webelement = AllPages.GetWebElement("sign_in_button");
            Helper.SafeClick(webelement, "safeclick"); 
            AllPages.CurrentPageName = "MainPage";
            element_locator = AllPages.GetElementLocator("user_profile_image");
            Assert.IsTrue(Helper.WaitToAppear(element_locator, "user_profile_image"));
            Navigates.LogInStatus = "LoggedIn";
        }

        [TestMethod]
        [Given(@"I should signout")]
        [When(@"I should signout")]
        [Then(@"I should signout")]
        public void IShouldSignOut()
        {
            AllPages.CurrentPageName = "MainPage";
            webelement = AllPages.GetWebElement("user_menu_dropdown");
            Helper.SafeClick(webelement, "safeclick");
            webelement = AllPages.GetWebElement("user_menu_sign_out_button");
            Helper.SafeClick(webelement, "safeclick");
            AllPages.CurrentPageName = "SignOutPage";
            element_locator = AllPages.GetElementLocator("user_name_entry");
            Assert.IsTrue(Helper.WaitToAppear(element_locator, "user_name_entry")); 
            Navigates.LogInStatus = "LoggedOut";
        }

       

    }

}

