
using System.Collections.Generic;


namespace ProductTests.Pages
{
    class MainPage
    {
        public static Dictionary<string, string> element_locators = new Dictionary<string, string>()
        {
            { "user_profile_image" , "#nav-settings__dropdown-trigger > img" },
            { "user_menu_dropdown" , "#nav-settings__dropdown-trigger > img" },
            { "user_menu_sign_out_button" , "#account-sub-nav > div > div.account-sub-nav-body > ul > li.self > div > span > span.act-set-action > a" },

            { "loading_message", "?" },
            { "Complete", "Complete" }

        };

    }
}
