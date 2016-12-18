using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LinkedInSpecFlowTest.Pages
{
    class SignOutPage
    {
        private IWebDriver driver;

        public const string sign_in_locator = "div#header-banner li:nth-child(3) > a";
        [FindsBy(How = How.CssSelector, Using = sign_in_locator)]
        [CacheLookup]
        public IWebElement sign_in { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        public const string loading_message_locator = "div#blockUILoadingDiv";
        [FindsBy(How = How.CssSelector, Using = loading_message_locator)]
        [CacheLookup]
        public IWebElement loading_message { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        public SignOutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }




    }
}
