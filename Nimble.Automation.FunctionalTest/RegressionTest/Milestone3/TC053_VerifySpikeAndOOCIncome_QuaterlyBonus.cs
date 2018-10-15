using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Applying the loan to trigger the OOC and Spike income in same transcation and verify only one question is triggered for 
    // OOC and the corresponding user responses to Quarterly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC053_VerifySpikeAndOOCIncome_QuarterlyBonus_NL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL();

        [TearDown]
        public void Aftermethod()
        {
            _spike.Aftermethod();
        }

        [TestCase(400, "android", "Quarterly bonus", TestName = "TC053_VerifySpikeAndOOCIncome_QuarterlyBonus_NL_400"), Category("NL"), Retry(2)]
        [TestCase(3400, "ios", "Quarterly bonus", TestName = "TC053_VerifySpikeAndOOCIncome_QuarterlyBonus_NL_3400")]
        public void TC053_VerifySpikeAndOOCIncom_QuarterlyBonus_NL(int loanamout, string strmobiledevice, string strOOCReason)
        {
         _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_NL(loanamout, strmobiledevice, strOOCReason);            
        }
    }


    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC053_VerifySpikeAndOOCIncome_QuarterlyBonus_RL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL();
        [TearDown]
        public void Cleanup()
        {
            _spike.Cleanup();
        }

        [TestCase(400, "android", "Quarterly bonus", TestName = "TC053_VerifySpikeAndOOCIncome_QuarterlyBonus_RL_400"), Category("RL"), Retry(2)]
        [TestCase(2400, "ios", "Quarterly bonus", TestName = "TC053_VerifySpikeAndOOCIncome_QuarterlyBonus_RL_2400")]
        public void TC053_VerifySpikeAndOOCIncom_QuarterlyBonus_RL(int loanamout, string strmobiledevice, string strOOCReason)
        {
          _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_RL(loanamout, strmobiledevice, strOOCReason);            
        }
    }

}
