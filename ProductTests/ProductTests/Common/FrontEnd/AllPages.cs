using System;

using ZolCo.ProductTests.Tests.LinkedIn.Pages;

namespace ZolCo.ProductTests.Common
{

    public class AllPages
    {
        public static string CurrentPageName = null;

        public static string GetElementLocator(string elementName)
        {
            string elementLocator = null;
            if (SetUp.scenarioTitleSections["client"].Contains("Darta"))
                elementLocator = GetDartaElementLocator(elementName);
            return elementLocator;
        }

        public static string GetDartaElementLocator(string elementName)
        {
            string elementLocator = null;
            switch (CurrentPageName)
            {
                case "SignInPage":
                    {
                        elementLocator = SignInPage.elementSelectors[elementName];
                        return elementLocator;
                    }
                case "MainPage":
                    {
                        elementLocator = MainPage.elementSelectors[elementName];
                        return elementLocator;
                    }
                case "SignOutPage":
                    {
                        elementLocator = SignOutPage.elementSelectors[elementName];
                        return elementLocator;
                    }
                default:
                    {
                        Console.WriteLine(CurrentPageName + " element has not defined yet in the GetDartaElementLocator");
                        return elementLocator;
                    }
            }
        }
    }
}        


