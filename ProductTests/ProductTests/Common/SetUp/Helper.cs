using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductTests.Common
{
    [Binding]
    [TestClass]

    class Helper
    {

        //cd ProductTests\packages\SpecRun.Runner.1.5.2\tools
        //SpecRun.exe run ProductTests\ProductTests\default.srprofile /filter:"testpath:Feature:Allianz_FullSurrender"
        //SpecRun.exe run ProductTests\ProductTests\default.srprofile basefolder:ProductTests\ProductTests\bin\Debug/outputfolder:ProductTests\TestResults /report:ReportTemplate.html /filter:"testpath:Feature:Allianz_FullSurrender"
        //SpecRun.exe buildserverrun default.srprofile  /basefolder:ProductTests\ProductTests /outputfolder:ProductTests\ProductTests\TestResults  /report:ReportTemplate.html /filter:"testpath:Feature:Allianz_FullSurrender"

    }

}

