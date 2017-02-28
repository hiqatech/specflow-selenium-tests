using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using ProductTests.XClients.LinkedIn.Pages;

namespace ProductTests.Common.Steps.FrontEnd
{
    [Binding]

    class AllPages
    {

        public static string element_locator;
        public static IWebElement webelement;
        public static string CurrentPageName = null;
        public static string CurrentProductName = null;

        public static IWebElement GetWebElement(string element_name)
        {
            if (CurrentProductName.Contains("Linkedin"))
            {
                GetLinkedInWebElement(element_name);
                return webelement;
            }

           // else if (CurrentProductName.Contains("Yahoo"))
           // {
           //     GetYahooWebElement(element_name);
           //     return webelement
           // }

            else
            {
                Console.WriteLine(CurrentProductName + " page has not defined yet in the test pages");
                return webelement;
            }
                
        }

        public static String GetElementLocator(string element_name)
        {
            if (CurrentProductName.Contains("Linkedin"))
            {
                GetLinkedInElementLocator(element_name);
                return element_locator;
            }

            // else if (CurrentProductName.Contains("Yahoo"))
            // {
            //     GetYahooElementLocator(element_name);
            //     return webelement
            // }

            else
            {
                Console.WriteLine(CurrentProductName + " page has not defined yet in the test pages");
                return element_locator;
            }

        }


        public static IWebElement GetLinkedInWebElement(string element_name)
        {
            switch (CurrentPageName)
            {
                case "SignInPage":
                    {
                        element_locator = SignInPage.element_locators[element_name];
                        Helper.WaitToAppear(element_locator, element_name);
                        return webelement;
                    }
                case "MainPage":
                    {
                        element_locator = MainPage.element_locators[element_name];
                        Helper.WaitToAppear(element_locator, element_name);
                        return webelement;
                    }
                case "SignOutPage":
                    {
                        element_locator = SignOutPage.element_locators[element_name];
                        return webelement;
                    }
                default:
                    {
                        Console.WriteLine(CurrentPageName + " page has not defined yet in the test pages");
                        return webelement;

                    }
            }

        }

        public static String GetLinkedInElementLocator(string element_name)
        {

            switch (CurrentPageName)
            {
                case "SignInPage":
                    {
                        element_locator = SignInPage.element_locators[element_name];
                        return element_locator;
                    }
                case "MainPage":
                    {
                        element_locator = MainPage.element_locators[element_name];
                        return element_locator;
                    }
                case "SignOutPage":
                    {
                        element_locator = SignOutPage.element_locators[element_name];
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


