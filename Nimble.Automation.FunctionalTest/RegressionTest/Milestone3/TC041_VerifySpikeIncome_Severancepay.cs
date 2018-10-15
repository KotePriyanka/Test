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
    //Applying the loan to trigger the Spike question and the corresponding user responses_Redundancy/Severance pay
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC041_VerifySpikeIncome_Severancepay_NL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(800, "Redundancy/Severance pay", "android", TestName = "TC041_VerifySpikeIncome_Severancepay_NL_800"), Category("NL"), Retry(2)]
        [TestCase(3000, "Redundancy/Severance pay", "android", TestName = "TC041_VerifySpikeIncome_Severancepay_NL_3000")]
        public void TC041_VerifyingSpikeIncome_Severancepay_NL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_NL(loanamout, spikeresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the Spike question and the corresponding user responses_Redundancy/Severance pay
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC041_VerifySpikeIncome_Severancepay_RL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(800, "Redundancy/Severance pay", "android", TestName = "TC041_VerifySpikeIncome_Severancepay_RL_800"), Category("RL"), Retry(2)]
        [TestCase(2700, "Redundancy/Severance pay", "android", TestName = "TC041_VerifySpikeIncome_Severancepay_RL_2700")]
        public void TC041_VerifyingSpikeIncome_Severancepay_RL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_RL(loanamout, spikeresponse, strmobiledevice);
        }
    }
}
