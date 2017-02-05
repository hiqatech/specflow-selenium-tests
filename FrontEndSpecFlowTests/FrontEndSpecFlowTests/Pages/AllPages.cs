using FrontEndSpecFlowTests.Pages;
using FrontEndSpecFlowTests.Common;
using OpenQA.Selenium;
using System;

using TechTalk.SpecFlow;

namespace FrontEndSpecFlowTests.Pages
{
    [Binding]

    class AllPages
    {

        public static string element_locator;
        public static IWebElement webelement;
        public static string CurrentPageName;

        public static IWebElement GetWebElement(string element_name)
        {
            switch (CurrentPageName)
            {
                case "SignInPage":
                    {
                        element_locator = SignInPage.getElementLocator(SignInPage.element_locators, element_name);
                        Helper.WaitToAppear(element_locator, element_name);
                        return webelement;
                    }
                case "MainPage":
                    {
                        element_locator = MainPage.getElementLocator(MainPage.element_locators, element_name);
                        Helper.WaitToAppear(element_locator, element_name);
                        return webelement;
                    }
                case "SignOutPage":
                    {
                        element_locator = SignOutPage.getElementLocator(SignOutPage.element_locators, element_name);
                        return webelement;
                    }
                default:
                    {
                        Console.WriteLine(CurrentPageName + " page has not defined yet in the test pages");
                        return webelement;

                    }
            }

        }

        public static String GetElementLocator(string element_name)
        {

            switch (CurrentPageName)
            {
                case "SignInPage":
                    {
                        element_locator = SignInPage.getElementLocator(SignInPage.element_locators, element_name);
                        return element_locator;
                    }
                case "MainPage":
                    {
                        element_locator = MainPage.getElementLocator(MainPage.element_locators, element_name);
                        return element_locator;
                    }
                case "SignOutPage":
                    {
                        element_locator = SignOutPage.getElementLocator(SignOutPage.element_locators, element_name);
                        return element_locator;
                    }
                default:
                    {
                        Console.WriteLine(CurrentPageName + " element has not defined yet in the test pages");
                        return element_locator;

                    }
            }

        }

    }

    }        


