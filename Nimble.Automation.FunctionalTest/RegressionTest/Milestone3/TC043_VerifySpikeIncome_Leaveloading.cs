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
    //Applying the loan to trigger the Spike question and the corresponding user responses_Leave loading
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC043_VerifySpikeIncome_Leaveloading_NL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(1050, "Leave loading", "android", TestName = "TC043_VerifySpikeIncome_Leaveloading_NL_1050"), Category("NL"), Retry(2)]
        [TestCase(3200, "Leave loading", "android", TestName = "TC043_VerifySpikeIncome_Leaveloading_NL_3200")]
        public void TC043_VerifyingSpikeIncome_Leaveloading_NL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_NL(loanamout, spikeresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the Spike question and the corresponding user responses_Leave loading
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC043_VerifySpikeIncome_Leaveloading_RL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(950, "Leave loading", "android", TestName = "TC043_VerifySpikeIncome_Leaveloading_RL_950"), Category("RL"), Retry(2)]
        [TestCase(2600, "Leave loading", "android", TestName = "TC043_VerifySpikeIncome_Leaveloading_RL_2600")]
        public void TC043_VerifyingSpikeIncome_Leaveloading_RL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_RL(loanamout, spikeresponse, strmobiledevice);
        }
    }
}
