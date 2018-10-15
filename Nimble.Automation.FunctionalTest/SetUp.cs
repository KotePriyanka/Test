using Nimble.Automation.Accelerators;
using NUnit.Framework;
using System;
using System.Configuration;
using Nimble.Automation.FunctionalTest;

[SetUpFixture]
public class SetUp
{
    ResultDbHelper _dbresults = new ResultDbHelper();
    public bool newrunflag { get; set; }
    public bool resultflag { get; set; }
    public string runguid { get; set; }
    public string Env { get; set; } = "";
    public string Browser { get; set; } = "";
    public string Build { get; set; } = "";

    // new Implementation

    //public string FinalReviewEnabled { get; set; } = "";
    //public string FinalReviewLoanType { get; set; } = "";
    //public string SelectedAccountCheckEnabled { get; set; } = "";


    //Functional Test
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        newrunflag = Convert.ToBoolean(ConfigurationManager.AppSettings["NewTestRun"]);
        resultflag = Convert.ToBoolean(ConfigurationManager.AppSettings["ResultFlag"]);
        GetBuildNumber _build = new GetBuildNumber();
        _build.GetConfigSettings();
        //new Implementation
        TestEngine.BuildNo = _build.GetCurrentBuild();
        TestEngine.FinalReviewEnabled = _build.FinalReviewEnabled();
        TestEngine.FinalReviewLoanType = _build.FinalReviewLoanType();
        TestEngine.SelectedAccountCheckEnabled = _build.SelectedAccountCheckEnabled();
        TestEngine.onlineBpaymentsIsEnabled = _build.onlineBpaymentsIsEnabled();
        TestEngine.requestAmountRestriction = _build.requestAmountRestriction();
        TestEngine.workFlowManagerSTP2NewToProduct = _build.workFlowManagerNewToProduct();
        TestEngine.calculatorIsEnabled = _build.calculatorEnabledValue();

        //Disabled to prevent errors on Trunk
        TestEngine.bsAutoRefresh = _build.bsAutoRefreshValue();

        //Prefail config Setting
        TestEngine.PrefailReschedule = _build.PrefailRescheduleValue();
        TestEngine.PrefailRescheduleTotalAllowed = _build.PrefailRescheduleTotalAllowedValue();



        if (newrunflag)
        {
            //Generate Run GUID
            runguid = _dbresults.ExecuteTestRun(Env, Browser, TestEngine.BuildNo).ToString();
            TestData.RunGuid = runguid;
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (newrunflag)
        {
            //Generate Run GUID
            _dbresults.UpdateEndTime(TestData.RunGuid.ToString());
        }

        if (resultflag)
        {
            _dbresults.UpdateScriptPassCount();
        }
    }    
}

