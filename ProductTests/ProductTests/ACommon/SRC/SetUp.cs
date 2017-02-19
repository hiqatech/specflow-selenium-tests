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
using TechTalk.SpecFlow;

namespace ProductTests.Common
{
    [Binding]
    public sealed class SetUp
    {
        public static IWebDriver driver = null;
        public static string current_driver = null;
        public static string testStartTime = null;
        public static string systemTime = DateTime.Now.ToString("yyyy-MM-ddTHHmmss");
        public static string testProjectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\";
        public static string defaultTestRestultDirectory = testProjectDirectory + "TestResults\\";
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
                            ChromeOptions options = new ChromeOptions();
                            options.AddArgument("--ignore-certificate-errors");
                            driver = new ChromeDriver();
                            driver.Manage().Window.Maximize();
                            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                            current_driver = "Chrome";
                            break;
                        }
                    case "Firefox":
                        {
                            string path = testProjectDirectory + "\\ffProfile";
                            FirefoxProfile ffprofile = new FirefoxProfile(path);
                            driver = new FirefoxDriver(ffprofile);
                            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                            current_driver = "Firefox";
                            break;
                        }

                    case "IE":
                        {
                            DesiredCapabilities capabilities = new DesiredCapabilities();
                            capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
                            driver = new InternetExplorerDriver();
                            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                            current_driver = "IE";
                            break;
                        }
                    case "DB":
                        {
                            Console.WriteLine("DB Tests ...");
                            goto test_does_not_need_driver;
                            current_driver = "DB";

                        }
                    case "Service":
                        {
                            Console.WriteLine("Service Tests ...");
                            goto test_does_not_need_driver;
                            current_driver = "Service";
                        }
                    default:
                        {
                            Console.WriteLine("Tests start ...");
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

            if (current_driver == "DB" || current_driver == "Service") { goto endtest; }

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

            endtest:;
        }



        [AfterTestRun]
        public static void TestTearDown()
        {

        }

        public static string responseSubFolderName = SetUp.currentTestRestultDirectory + "\\Responses_" + SetUp.systemTime + "\\";
        public static int responseCount = 0;
        public static string responseFileName = null;
        public static string responsePath = null;
        public static string fileToCompare1 = null;
        public static string fileToCompare2 = null;

        public static void StoreAnswerAs(string answer, string extension)
        {

            if (!Directory.Exists(responseSubFolderName))
            {
                responseCount = 1;

                Directory.CreateDirectory(responseSubFolderName);
            }

            responseFileName = "answer" + responseCount + extension;
            responsePath = responseSubFolderName + responseFileName;

            if (!File.Exists(responsePath))
                File.CreateText(responsePath).Close();

            if (responseCount == 1)
            {
                fileToCompare1 = responsePath;
                File.WriteAllText(responsePath, answer);
                responseCount = responseCount + 1;
            }
            else if (responseCount == 2)
            {
                fileToCompare2 = responsePath;
                File.WriteAllText(responsePath, answer);
            }
            else responseCount = 0;

            Console.WriteLine("Answer saved to " + responsePath);


        }

    }

}
