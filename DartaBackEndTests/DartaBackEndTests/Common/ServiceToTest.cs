using NLoad;

namespace BackEndSpecflowTest.Common
{
    public class MyTest : ITest
    {
        public void Initialize()
        {
            // Initialize the test, e.g., create a WCF client, load files into memory, etc.
        }

        public TestResult Execute()
        {
            // Send an http request, invoke a WCF service or whatever you want to load test.

            return TestResult.Success; // or TestResult.Failure
        }
    }

}
