using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LinkedInSpecFlowTest.Pages
{
    class SignInPage
    {

        private IWebDriver driver;

        
        public const string close_cookie_locator = "button#dismiss-alert";
        [FindsBy(How = How.CssSelector, Using = close_cookie_locator)]
        [CacheLookup]
        public IWebElement close_cookie { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        public const string email_address_entry_locator = "input#login-email";
        [FindsBy(How = How.CssSelector, Using = email_address_entry_locator)]
        [CacheLookup]
        public IWebElement email_address_entry { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        public const string pass_word_entry_locator = "input#login-password";
        [FindsBy(How = How.CssSelector, Using = pass_word_entry_locator)]
        [CacheLookup]
        public IWebElement pass_word_entry { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        public const string sign_in_button_locator = "input#login-submit";
        [FindsBy(How = How.CssSelector, Using = sign_in_button_locator)]
        [CacheLookup]
        public IWebElement sign_in_button { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        public const string loading_message_locator = "div#blockUILoadingDiv";
        [FindsBy(How = How.CssSelector, Using = loading_message_locator)]
        [CacheLookup]
        public IWebElement loading_message { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/
        
        public SignInPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }




    }
}
