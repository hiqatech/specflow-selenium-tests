using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZolCo.ProductTests.Common;
using ZolCo.ProductTests.Utilities;

namespace ZolCo.Tests.LinkedIn
{
    [Binding]
    [TestClass]
    public class SpecSteps
    {
        public static string current_distribution = null;

        [Given(@"I login with (.*) username and (.*) password")]
        [When(@"I login with (.*) username and (.*) password")]
        [Then(@"I login with (.*) username and (.*) password")]
        public void ISignInWithUserNameAndPassword(string userName, string passWord)
        {
            WebHelp.SafeType(AllPages.GetElementLocator("user_name_entry"), userName);
            WebHelp.SafeType(AllPages.GetElementLocator("pass_word_entry"), passWord);
            WebHelp.SafeSelect(AllPages.GetElementLocator("sign_in_button"), "safeclick");
           
            AllPages.CurrentPageName = "MainPage";
            Assert.IsTrue(WebHelp.WaitToAppear(AllPages.GetElementLocator("user_profile_image")));         
        }

        [Given(@"I am using the (.*) page")]
        [When(@"I am using the (.*) page")]
        [Then(@"I am using the (.*) page")]
        public static void IAmUsingDistributionPage(string distribution)
        {
            current_distribution = distribution ;
        }

        [Given(@"I sign into this distribution")]
        [When(@"I sign into this distribution")]
        [Then(@"I sign into this distribution")]
        public static void ISignIntoThisDistribution()
        {
            AllPages.CurrentPageName = "SignInPage";

            WebHelp.SafeType(AllPages.GetElementLocator("user_name_entry"), SetUp.currentEnvironment["WebPIUser"]);
            WebHelp.SafeType(AllPages.GetElementLocator("pass_word_entry"), SetUp.currentEnvironment["WebPIPassword"]);
           
            WebHelp.SafeSelect(AllPages.GetElementLocator("sign_in_button"), "safeclick");

            Assert.IsTrue(WebHelp.WaitToAppear(AllPages.GetElementLocator("user_profile_welcome_text")));
            AllPages.CurrentPageName = "MainPage";

            WebHelp.SafeSelect(AllPages.GetElementLocator("data_base_dropdown"), "safeclick");
            if (SetUp.currentEnvironment["Environment"]=="UAT")
                WebHelp.SafeSelect(AllPages.GetElementLocator(SetUp.currentEnvironment["DataBase"]), "safeclick");
            else
                WebHelp.SafeSelect(AllPages.GetElementLocator(SetUp.currentEnvironment["DataBase"] + SetUp.currentEnvironment["Environment"]), "safeclick");
        }

        [Given(@"I should signout")]
        [When(@"I should signout")]
        [Then(@"I should signout")]
        public static void IShouldSignOut()
        {
            AllPages.CurrentPageName = "MainPage";
            WebHelp.SafeSelect(AllPages.GetElementLocator("user_menu_dropdown"), "safeclick");
            WebHelp.SafeSelect(AllPages.GetElementLocator("user_menu_sign_out_button"), "safeclick");
            
            AllPages.CurrentPageName = "SignOutPage";
            Assert.IsTrue(WebHelp.isDisplayed(AllPages.GetElementLocator("user_name_entry")));
        }

    }
}
