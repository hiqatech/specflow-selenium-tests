
using System.Collections.Generic;


namespace ProductTests.Pages
{
    class SignOutPage
    {

             public static Dictionary<string, string> element_locators = new Dictionary<string, string>()
        {
            { "sign_in_button", "#top-header > div > ul > li:nth-child(3)" },
            { "signed_out_message", "#page-title > h1" },
            { "close_cooky_policy_alert", "#dismiss-alert" },

            

            { "loading_message", "?" },
            { "Complete", "Complete" }


         };

    }
}
