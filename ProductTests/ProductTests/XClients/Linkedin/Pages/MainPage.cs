
using System.Collections.Generic;


namespace ProductTests.XClients.LinkedIn.Pages
{
    class MainPage
    {
        public static Dictionary<string, string> element_locators = new Dictionary<string, string>()
        {
            { "user_profile_image" , "button#nav-settings__dropdown-trigger > img" },
            { "user_menu_dropdown" , "button#nav-settings__dropdown-trigger > img" },
            { "user_menu_sign_out_button" , "#nav-settings__dropdown-options > li.nav-settings__dropdown-options--actions.nav-settings__no-hover" },

            { "loading_message", "?" },
            { "Complete", "Complete" }

        };

    }
}
