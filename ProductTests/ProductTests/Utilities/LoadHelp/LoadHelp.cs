using System;

namespace ZolCo.ProductTests.Utilities
{
   
    public class LoadHelp
    {

        public static void IAmRunningLoadTest()
        {
            //var loadTest = ITest<MyTest>()
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
