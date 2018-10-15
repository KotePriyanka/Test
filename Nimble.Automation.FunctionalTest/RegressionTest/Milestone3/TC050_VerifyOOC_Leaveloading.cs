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
    //Applying the loan to trigger the OOC question and the corresponding user responses_Leave loading
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC050_VerifyOOC_Leaveloading_NL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }


        [TestCase(1100, "Leave loading", "android", TestName = "TC050_VerifyOOC_Leaveloading_NL_1100"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(2950, "Leave loading", "android", TestName = "TC050_VerifyOOC_Leaveloading_NL_2950")]

        public void TC050_VerifyingOOC_Leaveloading_NL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_NL(loanamout, oocresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Leave loading
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC050_VerifyOOC_Leaveloading_RL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(1100, "Leave loading", "android", TestName = "TC050_VerifyOOC_Leaveloading_RL_1100"), Category("RL"), Retry(2)]
        [TestCase(2750, "Leave loading", "android", TestName = "TC050_VerifyOOC_Leaveloading_RL_2750")]

        public void TC050_VerifyingOOC_Leaveloading_RL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_RL(loanamout, oocresponse, strmobiledevice);
        }
    }
}
