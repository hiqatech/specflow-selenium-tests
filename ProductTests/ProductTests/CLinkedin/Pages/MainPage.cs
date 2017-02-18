
using System.Collections.Generic;


namespace ProductTests.Pages
{
    class MainPage
    {
        public static Dictionary<string, string> element_locators = new Dictionary<string, string>()
        {
            { "user_profile_image" , "#account-nav > ul > li.nav-item.account-settings-tab" },
            { "user_menu_dropdown" , "#account-nav > ul > li.nav-item.account-settings-tab" },
            { "user_menu_sign_out_button" , "#account-sub-nav > div > div.account-sub-nav-body > ul > li.self > div > span > span.act-set-action > a" },

            { "loading_message", "?" },
            { "Complete", "Complete" }

        };

    }
}
