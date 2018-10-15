using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture]
    public class GetBuildNumber
    {
        private HomeDetails _homeDetails = null;
        // private LoanPurposeDetails _loanPurposeDetails = null;
        // private PersonalDetails _personalDetails = null;
        // private LoanSetUpDetails _loanSetUpDetails = null;
        // private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;

        DateTime starttime { get; set; } = DateTime.Now;
        ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [Test]
        public void GetConfigSettings()
        {
            _driver = _testengine.GetConfigValues();
            _homeDetails = new HomeDetails(_driver, "RL");
        }

        public string GetCurrentBuild()
        {
            return _homeDetails.GetBuildNumber();
        }

        public string FinalReviewEnabled()
        {
            return _homeDetails.GetFinalReviewEnabled();
        }

        public string FinalReviewLoanType()
        {
            return _homeDetails.GetFinalReviewLoanType();
        }

        public string SelectedAccountCheckEnabled()
        {
            return _homeDetails.GetSelectedAccountCheckEnabled();
        }

        public string onlineBpaymentsIsEnabled()
        {
            return _homeDetails.getOnlineBpayPaymentEnabled();
        }

        public string requestAmountRestriction()
        {
            return _homeDetails.requestAmountRestrictionEnabled();
        }

        public string workFlowManagerNewToProduct()
        {
            return _homeDetails.workflowManagerSTP2NewToProduct();
        }

        public string calculatorEnabledValue()
        {
            return _homeDetails.calculatorEnabled();
        }

        public bool bsAutoRefreshValue()
        {
            return _homeDetails.bsAutoRefreshEnabled();
        }

        public bool PrefailRescheduleValue()
        {
            return _homeDetails.PrefailRescheduleEnabled();
        }

        public string PrefailRescheduleTotalAllowedValue()
        {
            return _homeDetails.PrefailRescheduleTotalAllowed();
        }

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
        }
    }
}
