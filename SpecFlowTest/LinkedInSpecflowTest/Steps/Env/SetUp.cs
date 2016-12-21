﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace LinkedInSpecFlowTest.Steps.Env
{
    [Binding]
    public sealed class SetUp
    {
        public static IWebDriver driver = null;
        public static string current_driver = null;
        public static string testStartTime = null;
        public static string systemTime = DateTime.Now.ToString("yyyy-MM-ddTHHmmss");
        public static string defaultTestRestultDirectory = "D:\\ZolCo\\VisualProjects\\SpecFlowTest\\TestResults\\";
        public static string currentTestRestultDirectory = null;
        [BeforeTestRun]
        public static void TestSetup()
        {
            testStartTime = systemTime;
            currentTestRestultDirectory = defaultTestRestultDirectory + "TestResults_" + testStartTime;
            currentTestRestultDirectory = currentTestRestultDirectory.Remove(currentTestRestultDirectory.Length - 2);

            DirectoryInfo dInfo = new DirectoryInfo(defaultTestRestultDirectory);
            foreach (FileInfo fInfo in dInfo.GetFiles())
            {
                if ((fInfo.Name.Contains(".html") || (fInfo.Name.Contains(".log"))))
                {
                    string testFile = fInfo.Name.ToString();
                    string sourceFile = defaultTestRestultDirectory + "\\" + testFile;
                    string testName = Path.GetFileNameWithoutExtension(testFile);
                    testName = testName.Split('_')[2];
                    testName = testName.Remove(testName.Length - 2);

                    foreach (DirectoryInfo dirInfo in dInfo.GetDirectories())
                    {
                        if (dirInfo.Name.Contains(testName))
                        {
                            string testDir = dirInfo.Name.ToString();
                            string targetFile = defaultTestRestultDirectory + "\\" + testDir + "\\" + testFile;
                            File.Move(sourceFile, targetFile);
                            File.Delete(sourceFile);
                        }
                    }
                }
            }

            if (!Directory.Exists(currentTestRestultDirectory))
                Directory.CreateDirectory(currentTestRestultDirectory);
        }


        [BeforeScenario()]
        public static void CDriverSetup()
        {
            string scenarioTitle = ScenarioContext.Current.ScenarioInfo.Title.ToString();
            List<string> result = scenarioTitle.Split(' ').ToList();
            foreach (string browser in result)
            {
                switch (browser)
                {
                    case "Chrome":
                        {
                            //ChromeOptions options = new ChromeOptions();
                            //options.AddArgument("--ignore-certificate-errors");
                            driver = new ChromeDriver();
                            current_driver = "Chrome";
                            break;
                        }
                    case "Firefox":
                        {
                            //string path = @"C:\Product-Tests\ffProfile";
                            //FirefoxProfile ffprofile = new FirefoxProfile(path);
                            driver = new FirefoxDriver();
                            current_driver = "Firefox";
                            break;
                        }

                    case "IE":
                        {
                            //DesiredCapabilities capabilities = new DesiredCapabilities();
                            //capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
                            driver = new InternetExplorerDriver();
                            current_driver = "IE";
                            break;
                        }
                    case "DB":
                        {
                            Console.WriteLine("Connecting to DB ...");
                            goto test_does_not_need_driver;
                        }
                }

            }

            Console.WriteLine("Starting " + current_driver + " driver");
            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            //driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));

            test_does_not_need_driver:;
        }


        [AfterScenario]
        public static void DriverClose()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                string testImage = "testTailedImage" + systemTime + ".jpg";
                var screenshotdriver = SetUp.driver as ITakesScreenshot;
                var screenshot = screenshotdriver.GetScreenshot();
                string imagefullpath = Path.Combine(currentTestRestultDirectory, testImage);
                screenshot.SaveAsFile(imagefullpath, ImageFormat.Jpeg);

                Console.WriteLine("Test Failed ScreenShoot saved with " + testImage.ToString() + " name at " + systemTime + " to " + currentTestRestultDirectory);
            }

            driver.Close();
            driver.Quit();
        }

        [AfterTestRun]
        public static void TestTearDown()
        {
            Console.WriteLine("Test Run Finished, you will find the results in the " + defaultTestRestultDirectory);
        }

    }


}