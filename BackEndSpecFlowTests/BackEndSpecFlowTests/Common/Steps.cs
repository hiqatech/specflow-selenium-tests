using System;
using System.IO;

namespace BackEndSpecFlowTests.Common
{
    class Steps
    {
        
        public static string responseSubFolderName = SetUp.currentTestRestultDirectory + "\\Responses_"+ SetUp.systemTime + "\\";
        public static int responseCount = 0;
        public static string responseFileName = null;
        public static string responsePath = null;
        public static string fileToCompare1 = null;
        public static string fileToCompare2 = null;

        public static void StoreAnswerAs(string answer,string extension)
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
