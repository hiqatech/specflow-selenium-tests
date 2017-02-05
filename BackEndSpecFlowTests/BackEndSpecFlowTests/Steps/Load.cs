using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace BackEndSpecFlowTests.Common
{
    class Load
    {

        [TestMethod]
        [Given(@"I am running load test")]
        public void IAmRunningLoadTest()
        {

            //  var loadTest = ITest<MyTest>()
            //.WithNumberOfThreads(5)
            //.WithDurationOf(TimeSpan.FromMinutes(5))
            //.OnHeartbeat((s, e) => Console.WriteLine(e.Throughput))
            //.Build();

            //  var result = loadTest.Run();

        }

        private object ITest<T>()
        {
            throw new NotImplementedException();
        }

    }
}
