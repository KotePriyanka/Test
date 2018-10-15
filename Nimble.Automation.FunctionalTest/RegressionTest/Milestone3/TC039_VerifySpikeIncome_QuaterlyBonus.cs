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
    //Applying the loan to trigger the Spike question and the corresponding user responses_Quarterly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC039_VerifySpikeIncome_QuaterlyBonus_NL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(1000, "Quarterly bonus", "android", TestName = "TC039_VerifySpikeIncome_QuaterlyBonus_NL_1000"), Category("NL"), Retry(2)]
        [TestCase(3500, "Quarterly bonus", "android", TestName = "TC039_VerifySpikeIncome_QuaterlyBonus_NL_3500")]
        public void TC039_VerifyingSpikeIncome_QuaterlyBonus_NL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_NL(loanamout, spikeresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the Spike question and the corresponding user responses_Quarterly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC039_VerifySpikeIncome_QuaterlyBonus_RL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(1250, "Quarterly bonus", "android", TestName = "TC039_VerifySpikeIncome_QuaterlyBonus_RL_1250"), Category("RL"), Retry(2)]
        [TestCase(2850, "Quarterly bonus", "android", TestName = "TC039_VerifySpikeIncome_QuaterlyBonus_RL_2850")]
        public void TC039_VerifyingSpikeIncome_QuaterlyBonus_RL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_RL(loanamout, spikeresponse, strmobiledevice);
        }
    }
}
