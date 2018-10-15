using NUnit.Framework;
using Nimble.Automation.Accelerators;
using OpenQA.Selenium;
using Nimble.Automation.Repository;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Applying the loan to trigger the OOC and Spike income in same transcation and verify only one question is triggered for 
    // OOC and the corresponding user responses to Reimbursement
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC056_VerifySpikeAndOOCIncome_Reimbursement_NL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_NL();
        [TearDown]
        public void Aftermethod()
        {
            _spike.Aftermethod();

        }

        [TestCase(650, "android", "Company expense reimbursement", TestName = "TC056_VerifySpikeAndOOCIncome_Reimbursement_NL_650"), Category("NL"), Retry(2)]
        [TestCase(3600, "ios", "Company expense reimbursement", TestName = "TC056_VerifySpikeAndOOCIncome_Reimbursement_NL_3600")]
        public void TC056_VerifySpikeAndOOCIncom_Reimbursement_NL(int loanamout, string strmobiledevice, string strOOCReason)
        {
          _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_NL(loanamout, strmobiledevice, strOOCReason);
        }
    }


    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC056_VerifySpikeAndOOCIncome_Reimbursement_RL : TestEngine
    {
        TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL _spike = new TC052_VerifySpikeAndOOCIncome_AnnualBonus_RL();
        [TearDown]
        public void Cleanup()
        {
            _spike.Cleanup();
        }

        [TestCase(600, "android", "Company expense reimbursement", TestName = "TC056_VerifySpikeAndOOCIncome_Reimbursement_RL_600"), Category("RL"), Retry(2)]
        [TestCase(2600, "ios", "Company expense reimbursement", TestName = "TC056_VerifySpikeAndOOCIncome_Reimbursement_RL_2600")]
        public void TC056_VerifySpikeAndOOCIncom_Reimbursement_RL(int loanamout, string strmobiledevice, string strOOCReason)
        {
         _spike.TC052_VerifySpikeAndOOCIncom_AnnualBonus_RL(loanamout, strmobiledevice, strOOCReason);
        }
    }

}
