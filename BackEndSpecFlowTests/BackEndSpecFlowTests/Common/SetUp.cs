using System;
using System.IO;
using TechTalk.SpecFlow;

namespace BackEndSpecFlowTests.Common
{
    [Binding]
    public sealed class SetUp
    {

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

        [AfterTestRun]
        public static void TestTearDown()
        {
            Console.WriteLine("Test Run Finished, you will find the results in the " + defaultTestRestultDirectory);
        }
    }
}