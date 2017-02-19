﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ProductTests.CLinkedin.Features.BackEnd.SignIn
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("BackEndNewBusiness", SourceFile="CLinkedin\\Features\\BackEnd\\SignIn\\NewBusiness.feature", SourceLine=0)]
    public partial class BackEndNewBusinessFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "NewBusiness.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "BackEndNewBusiness", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Service - I can create a Allianz Challange Plus New Business", new string[] {
                "Smoke"}, SourceLine=3)]
        public virtual void Service_ICanCreateAAllianzChallangePlusNewBusiness()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Service - I can create a Allianz Challange Plus New Business", new string[] {
                        "Smoke"});
#line 4
this.ScenarioSetup(scenarioInfo);
#line 5
testRunner.Given("I connect to the SERV8604 server DartaUATR1 database", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table1.AddRow(new string[] {
                        "database",
                        "R1"});
            table1.AddRow(new string[] {
                        "distribution",
                        "01"});
            table1.AddRow(new string[] {
                        "username",
                        "x"});
            table1.AddRow(new string[] {
                        "locale",
                        "IT"});
            table1.AddRow(new string[] {
                        "agentid",
                        "x"});
            table1.AddRow(new string[] {
                        "company",
                        "DAR"});
            table1.AddRow(new string[] {
                        "timeout",
                        "x"});
            table1.AddRow(new string[] {
                        "userclientreference",
                        "x\tx"});
#line 6
testRunner.And("I ping the DartaBusinessServices with", ((string)(null)), table1, "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table2.AddRow(new string[] {
                        "ApplicationSignedDate",
                        "system_date-5"});
            table2.AddRow(new string[] {
                        "Address",
                        "Dublin"});
#line 17
testRunner.And("I have a NewBusinessProposal to AddProposal", ((string)(null)), table2, "And ");
#line 21
testRunner.And("I send POST request to AddProposal", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
testRunner.When("I run the batch process", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Table",
                        "Entry",
                        "Value"});
            table3.AddRow(new string[] {
                        "BP_BasicPolicy",
                        "PolicyStatus",
                        "I"});
            table3.AddRow(new string[] {
                        "PH_PaymentHistory",
                        "Details",
                        "IN FORCE"});
#line 23
testRunner.Then("The DataBase values shoudl be", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.TestRunCleanup()]
        public virtual void TestRunCleanup()
        {
            TechTalk.SpecFlow.TestRunnerManager.GetTestRunner().OnTestRunEnd();
        }
    }
}
#pragma warning restore
#endregion
