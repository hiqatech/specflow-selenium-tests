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
namespace ProductTests.CLinkedin.Features.FrontEnd.SignIn
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("Linkedin SignIn Tests", SourceFile="CLinkedin\\Features\\FrontEnd\\SignIn\\SignIN.feature", SourceLine=0)]
    public partial class LinkedinSignInTestsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SignIN.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Linkedin SignIn Tests", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        public virtual void Chrome_ICanSigninAsAUserWithCorrectUsernameAndPassword(string user_Name, string pass_Word, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Regression"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chrome - I can signin as a user with correct username and password", @__tags);
#line 5
this.ScenarioSetup(scenarioInfo);
#line 6
testRunner.Given("I navigate to the https://www.linkedin.com/ website", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 7
testRunner.And("I am on the SignInPage page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
testRunner.And(string.Format("I enter {0} as the user_name_entry", user_Name), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
testRunner.And(string.Format("I enter {0} as the pass_word_entry", pass_Word), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
testRunner.When("I select the sign_in_button element", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
testRunner.And("I am on the MainPage page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
testRunner.Then("I should see the user_profile_image element", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 13
testRunner.And("I select the user_menu_dropdown element", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
testRunner.And("I select the user_menu_sign_out_button element", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
testRunner.And("I am on the SignOutPage page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
testRunner.And("I wait 20 sec", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
testRunner.And("I should see the user_name_entry element", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Chrome - I can signin as a user with correct username and password, kiszols@yahoo" +
            ".com", new string[] {
                "Regression"}, SourceLine=21)]
        public virtual void Chrome_ICanSigninAsAUserWithCorrectUsernameAndPassword_KiszolsYahoo_Com()
        {
            this.Chrome_ICanSigninAsAUserWithCorrectUsernameAndPassword("kiszols@yahoo.com", "Stridentb52", ((string[])(null)));
#line hidden
        }
        
        public virtual void Chrome_ICanSigninSignoutWithCorrectUsernameAndPassword(string user_Name, string pass_Word, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Regression"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chrome - I can signin/signout with correct username and password", @__tags);
#line 25
this.ScenarioSetup(scenarioInfo);
#line 26
testRunner.Given("I navigate to the https://www.linkedin.com/ website", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
testRunner.And("I am on the SignInPage page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
testRunner.And(string.Format("I login with {0} username and {1} password", user_Name, pass_Word), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
testRunner.And("I should signout", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Chrome - I can signin/signout with correct username and password, kiszols@yahoo.c" +
            "om", new string[] {
                "Regression"}, SourceLine=32)]
        public virtual void Chrome_ICanSigninSignoutWithCorrectUsernameAndPassword_KiszolsYahoo_Com()
        {
            this.Chrome_ICanSigninSignoutWithCorrectUsernameAndPassword("kiszols@yahoo.com", "Stridentb52", ((string[])(null)));
#line hidden
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
