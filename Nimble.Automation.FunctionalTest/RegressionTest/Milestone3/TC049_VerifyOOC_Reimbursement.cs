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
    //Applying the loan to trigger the OOC question and the corresponding user responses_Company expense reimbursement
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC049_VerifyOOC_Reimbursement_NL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(1050, "Company expense reimbursement", "android", TestName = "TC049_VerifyOOC_Reimbursement_NL_1050"), Category("NL"), Retry(2)]
        [TestCase(2900, "Company expense reimbursement", "android", TestName = "TC049_VerifyOOC_Reimbursement_NL_2900")]
        public void TC049_VerifyingOOC_Reimbursement_NL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_NL(loanamout, oocresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Company expense reimbursement
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC049_VerifyOOC_Reimbursement_RL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(1050, "Company expense reimbursement", "android", TestName = "TC049_VerifyOOC_Reimbursement_RL_1050"), Category("RL"), Retry(2)]
        [TestCase(2800, "Company expense reimbursement", "android", TestName = "TC049_VerifyOOC_Reimbursement_RL_2800")]
        public void TC049_VerifyingOOC_Reimbursement_RL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_RL(loanamout, oocresponse, strmobiledevice);
        }
    }
}
