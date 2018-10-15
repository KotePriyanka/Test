using NUnit.Framework;
using Nimble.Automation.Accelerators;
using OpenQA.Selenium;
using Nimble.Automation.Repository;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Applying the loan to trigger the OOC and Spike income in same transcation and verify only one question is triggered for 
    // OOC and the corresponding user responses to Leaveloading bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC057_VerifySpikeAndOOCIncome_Leaveloading_NL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL();

        [TearDown]
        public void Aftermethod()
        {
            _spike.Aftermethod();
        }

        [TestCase(700, "android", "Leave loading", TestName = "TC057_VerifySpikeAndOOCIncome_Leaveloading_NL_700"), Category("NL"), Retry(2)]
        [TestCase(3700, "ios", "Leave loading", TestName = "TC057_VerifySpikeAndOOCIncome_Leaveloading_NL_3700")]
        public void TC057_VerifySpikeAndOOCIncom_Leaveloading_NL(int loanamout, string strmobiledevice, string strOOCReason)
        {
         _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_NL(loanamout, strmobiledevice, strOOCReason);
        }
    }


    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC057_VerifySpikeAndOOCIncome_Leaveloading_RL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL();

        [TearDown]
        public void Cleanup()
        {
            _spike.Cleanup();
        }

        [TestCase(650, "android", "Leave loading", TestName = "TC057_VerifySpikeAndOOCIncome_Leaveloading_RL_650"), Category("RL"), Retry(2)]
        [TestCase(2750, "ios", "Leave loading", TestName = "TC057_VerifySpikeAndOOCIncome_Leaveloading_RL_2750")]
        public void TC057_VerifySpikeAndOOCIncom_Leaveloading_RL(int loanamout, string strmobiledevice, string strOOCReason)
        {
         _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_RL(loanamout, strmobiledevice, strOOCReason);
        }
    }

}
