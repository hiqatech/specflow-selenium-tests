using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BackEndSpecFlowTests.Steps
{

    [Binding]
    class ValueShouldBe
    {

        [TestMethod]
        [Then(@"The query_result should be (.*)")]
        public void GivenIUpdateInTheSQLDB(string value)
        {
            Assert.IsTrue(value == DataBaseRead.query_result_string);
        }

    }
}
