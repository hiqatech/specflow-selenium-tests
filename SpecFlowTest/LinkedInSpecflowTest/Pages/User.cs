using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LinkedInSpecFlowTest.Pages
{
    class UserPage
    {

        private IWebDriver driver;

        public const string user_profile_locator = "nav#header-navigation-utilities a > img";
        [FindsBy(How = How.CssSelector, Using = user_profile_locator)]
        [CacheLookup]
        public IWebElement user_profile { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        public const string user_profile_dropdown_locator = "#header-navigation-utilities > ul > li.activity-tab.account-settings-tab > a";
        [FindsBy(How = How.CssSelector, Using = user_profile_dropdown_locator)]
        [CacheLookup]
        public IWebElement user_profile_dropdown { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        public const string sign_out_locator = "#account-sub-nav > div > div.account-sub-nav-body > ul > li.self > div > span > span.act-set-action > a";
        [FindsBy(How = How.CssSelector, Using = sign_out_locator)]
        [CacheLookup]
        public IWebElement sign_out { get; set; }
        /*  ------------------------------------------------------------------------------------------------*/

        
        public UserPage (IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }




    }
}
