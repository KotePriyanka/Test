using NUnit.Framework;
using Nimble.Automation.Accelerators;
using OpenQA.Selenium;
using Nimble.Automation.Repository;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Applying the loan to trigger the OOC and Spike income in same transcation and verify only one question is triggered for 
    // OOC and the corresponding user responses to Severance Pay
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC055_VerifySpikeAndOOCIncome_SeverancePay_NL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL();

        [TearDown]
        public void Aftermethod()
        {
            _spike.Aftermethod();

        }
        [TestCase(550, "android", "Redundancy/Severance pay", TestName = "TC055_VerifySpikeAndOOCIncome_SeverancePay_NL_550"), Category("NL"), Retry(2)]
        [TestCase(3650, "ios", "Redundancy/Severance pay", TestName = "TC055_VerifySpikeAndOOCIncome_SeverancePay_NL_3650")]
        public void TC055_VerifySpikeAndOOCIncom_SeverancePay_NL(int loanamout, string strmobiledevice, string strOOCReason)
        {
          _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_NL(loanamout, strmobiledevice, strOOCReason);
        }
    }


    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC055_VerifySpikeAndOOCIncome_SeverancePay_RL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL();
        [TearDown]
        public void Cleanup()
        {
            _spike.Cleanup();
        }

        [TestCase(550, "android", "Redundancy/Severance pay", TestName = "TC055_VerifySpikeAndOOCIncome_SeverancePay_RL_550"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(2550, "ios", "Redundancy/Severance pay", TestName = "TC055_VerifySpikeAndOOCIncome_SeverancePay_RL_2550")]
        public void TC055_VerifySpikeAndOOCIncom_SeverancePay_RL(int loanamout, string strmobiledevice, string strOOCReason)
        {
          _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_RL(loanamout, strmobiledevice, strOOCReason);
        }
    }

}
