using System.Collections.Generic;

namespace ZolCo.ProductTests.Tests.LinkedIn.Pages
{
    class SignInPage
    {

        public static Dictionary<string, string> elementSelectors = new Dictionary<string, string>()
        {
            { "select_language_dropdown", "" },
            { "english_language_selection", "" },
            { "accept_cookies_button", "button#dismiss-alert" },
            { "user_name_entry", "input#login-email" },
            { "pass_word_entry", "input#login-password" },
            { "sign_in_button", "input#login-submit" },

            { "loading_message", "?" },
            { "Complete", "Complete" }

         };

        }
    }
