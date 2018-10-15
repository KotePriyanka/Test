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
    //Applying the loan to trigger the Spike question and the corresponding user responses_Company expense reimbursement
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC042_VerifySpikeIncome_Reimbursement_NL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(750, "Company expense reimbursement", "android", TestName = "TC042_VerifySpikeIncome_Reimbursement_NL_750"), Category("NL"), Retry(2)]
        [TestCase(3100, "Company expense reimbursement", "android", TestName = "TC042_VerifySpikeIncome_Reimbursement_NL_3100")]
        public void TC042_VerifyingSpikeIncome_Reimbursement_NL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_NL(loanamout, spikeresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the Spike question and the corresponding user responses_Company expense reimbursement
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC042_VerifySpikeIncome_Reimbursement_RL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(850, "Company expense reimbursement", "android", TestName = "TC042_VerifySpikeIncome_Reimbursement_RL_850"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(2650, "Company expense reimbursement", "android", TestName = "TC042_VerifySpikeIncome_Reimbursement_RL_2650")]
        public void TC042_VerifyingSpikeIncome_Reimbursement_RL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_RL(loanamout, spikeresponse, strmobiledevice);
        }
    }
}
