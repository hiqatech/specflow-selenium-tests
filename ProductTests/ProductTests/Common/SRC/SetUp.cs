using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace ProductTests.Common
{
    [Binding]

    public sealed class SetUp
    {
        public static IWebDriver webDriver = null;
        public static string current_driver = null;
        public static string testStartTime = null;
        public static string systemTime = DateTime.Now.ToString("yyyy-MM-ddTHHmmss");
        public static string testProjectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\";
        public static string defaultTestRestultDirectory = testProjectDirectory + "TestResults\\";
        public static string currentTestRestultDirectory = null;
        public static string testSuiteType = null;


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

                            try
                            {
                                File.Move(sourceFile, targetFile);
                                File.Delete(sourceFile);
                            }
                            catch (IOException ex)
                            {
                                Thread.Sleep(250);
                            }

                        }
                    }
                }
            }

            if (!Directory.Exists(currentTestRestultDirectory))
                Directory.CreateDirectory(currentTestRestultDirectory); 

        }


        [BeforeScenario()]
        public static void Setup()
        {
            string scenarioTags = ScenarioContext.Current.ScenarioInfo.Tags.ToList()[0].ToString();
            if (scenarioTags.Contains("Smoke"))
                testSuiteType = "Smoke";
            else if (scenarioTags.Contains("Regression"))
                testSuiteType = "Regression";
            else if (scenarioTags.Contains("Acceptance"))
                testSuiteType = "Acceptance";
            Console.WriteLine("***************");
            Console.WriteLine("Tests are starting ...");

        }


        [TestMethod]
        [Given(@"I start the WebDriver with (.*) browser")]
        [When(@"I start the WebDriver with (.*) browser")]
        [Then(@"I start the WebDriver with (.*) browser")]
        public static void DriverSetup(string browser)
        {
                switch (browser)
                {
                    case "Chrome":
                        {
                            ChromeOptions options = new ChromeOptions();
                            options.AddArgument("--ignore-certificate-errors");
                            webDriver = new ChromeDriver();
                            webDriver.Manage().Window.Maximize();
                            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                            current_driver = "Chrome";
                            break;
                        }
                    case "Firefox":
                        {
                            string path = testProjectDirectory + "\\ffProfile";
                            FirefoxProfile ffprofile = new FirefoxProfile(path);
                            webDriver = new FirefoxDriver(ffprofile);
                            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                            current_driver = "Firefox";
                            break;
                        }
                    case "IE":
                        {
                            DesiredCapabilities capabilities = new DesiredCapabilities();
                            capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
                            webDriver = new InternetExplorerDriver();
                            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                            current_driver = "IE";
                            break;
                        }
                    
                    default:
                        {
                        Console.WriteLine("Webdriver " + browser + "not implemented in the test setup");
                        break;
                    }
                }

            Console.WriteLine("Starting " + current_driver + " driver");
            webDriver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            //driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));
   
        }


        [AfterScenario]
        public static void DriverClose()
        {

            if ((current_driver == "Chrome") || (current_driver == "Firefox") || (current_driver == "IE"))
            {

            if (ScenarioContext.Current.TestError != null)
            {
                string testImage = "testTailedImage" + systemTime + ".jpg";
                var screenshotdriver = SetUp.webDriver as ITakesScreenshot;
                var screenshot = screenshotdriver.GetScreenshot();
                string imagefullpath = Path.Combine(currentTestRestultDirectory, testImage);
                screenshot.SaveAsFile(imagefullpath, ImageFormat.Jpeg);

                Console.WriteLine("Test Failed ScreenShoot saved with " + testImage.ToString() + " name at " + systemTime + " to " + currentTestRestultDirectory);
            }

            webDriver.Close();
            webDriver.Quit();

           }
        }



        [AfterTestRun]
        public static void TestTearDown()
        {
            Console.WriteLine("***************");
            Console.WriteLine("Tests are finishing ...");
        }

       

    }

}
