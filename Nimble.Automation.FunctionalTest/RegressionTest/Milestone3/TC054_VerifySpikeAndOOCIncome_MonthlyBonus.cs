using NUnit.Framework;
using Nimble.Automation.Accelerators;
using OpenQA.Selenium;
using Nimble.Automation.Repository;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Applying the loan to trigger the OOC and Spike income in same transcation and verify only one question is triggered for 
    // OOC and the corresponding user responses to Monthly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC054_VerifySpikeAndOOCIncome_MonthlyBonus_NL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL();

        [TearDown]
        public void Aftermethod()
        {
            _spike.Aftermethod();
        }

        [TestCase(450, "android", "Monthly bonus", TestName = "TC054_VerifySpikeAndOOCIncome_MonthlyBonus_NL_450"), Category("NL"), Retry(2)]
        [TestCase(3600, "ios", "Monthly bonus", TestName = "TC054_VerifySpikeAndOOCIncome_MonthlyBonus_NL_3600")]
        public void TC054_VerifySpikeAndOOCIncom_QuaterlyBonus_NL(int loanamout, string strmobiledevice, string strOOCReason)
        {
            _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_NL(loanamout, strmobiledevice, strOOCReason);
        }
    }


    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC054_VerifySpikeAndOOCIncome_MonthlyBonus_RL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL();

        [TearDown]
        public void Cleanup()
        {
            _spike.Cleanup();
        }


        [TestCase(500, "android", "Monthly bonus", TestName = "TC054_VerifySpikeAndOOCIncome_MonthlyBonus_RL_500"), Category("RL"), Retry(2)]
        [TestCase(2450, "ios", "Monthly bonus", TestName = "TC054_VerifySpikeAndOOCIncome_MonthlyBonus_RL_2450")]
        public void TC054_VerifySpikeAndOOCIncom_QuaterlyBonus_RL(int loanamout, string strmobiledevice, string strOOCReason)
        {
            _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_RL(loanamout, strmobiledevice, strOOCReason);
        }
    }

}
