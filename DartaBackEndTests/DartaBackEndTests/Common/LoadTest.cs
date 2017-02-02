using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLoad;
using System;
using TechTalk.SpecFlow;

namespace BackEndSpecflowTest.Common
{
    [Binding]
    class LoadTest
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
