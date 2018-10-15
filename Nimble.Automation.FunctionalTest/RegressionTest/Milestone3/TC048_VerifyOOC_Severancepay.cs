using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.Milestone3
{
    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Redundancy/Severance pay
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC048_VerifyOOC_Severancepay_NL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();

        }
        [TestCase(1000, "Redundancy/Severance pay", "android", TestName = "TC048_VerifyOOC_Severancepay_NL_1000"), Category("NL"), Retry(2)]
        [TestCase(2850, "Redundancy/Severance pay", "android", TestName = "TC048_VerifyOOC_Severancepay_NL_2850")]
        public void TC048_VerifyingOOC_Severancepay_NL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_NL(loanamout, oocresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Redundancy/Severance pay
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC048_VerifyOOC_Severancepay_RL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(1000, "Redundancy/Severance pay", "android", TestName = "TC048_VerifyOOC_Severancepay_RL_1000"), Category("RL"), Retry(2)]
        [TestCase(2850, "Redundancy/Severance pay", "android", TestName = "TC048_VerifyOOC_Severancepay_RL_2850")]
        public void TC048_VerifyingOOC_Severancepay_RL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_RL(loanamout, oocresponse, strmobiledevice);
        }
    }
}
