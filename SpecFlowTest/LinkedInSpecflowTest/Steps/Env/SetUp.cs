using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Drawing.Imaging;
using System.IO;
using TechTalk.SpecFlow;

namespace LinkedInSpecFlowTest.Steps.Env
{
    [Binding]
    public sealed class SetUp
    {
        public static string current_driver = null;
        public static string systemTime = null;
        public static IWebDriver driver = null;
        public static string TestResultDirectory = "D:\\ZolCo\\VisualProjects\\SpecFlowTest\\TestResults";

        [BeforeScenario()]
        public void CDriverSetup()
        {
            string scenarioTitle =ScenarioContext.Current.ScenarioInfo.Title.ToString();

            if (scenarioTitle.Contains("Chrome")) {
                driver = new ChromeDriver();
                current_driver = "Chrome";
            }
            if (scenarioTitle.Contains("Firefox"))
            {
                driver = new FirefoxDriver();
                current_driver = "Firefox";
            }
            if (scenarioTitle.Contains("IE"))
            {
                driver = new InternetExplorerDriver();
                current_driver = "IE";
            }

            Console.WriteLine("Starting " + current_driver + " driver");
            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            //driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));

            systemTime = DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss");
        }


        [AfterScenario]
        public void DriverClose()
        {

            if (ScenarioContext.Current.TestError != null)
        {
            string testImage = "testImage" + systemTime + ".jpg";
            var screenshotdriver = SetUp.driver as ITakesScreenshot;
            var screenshot = screenshotdriver.GetScreenshot();
            string imagefullpath = Path.Combine(TestResultDirectory, testImage);
            screenshot.SaveAsFile(imagefullpath, ImageFormat.Jpeg);

            Console.WriteLine("Test Failed ScreenShoot saved with " + testImage.ToString() + " name at " + systemTime + " to " + TestResultDirectory);
        }

            driver.Close();
            driver.Quit();
        }
   

    }



}
