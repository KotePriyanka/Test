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
    //Applying the loan to trigger the Spike question and the corresponding user responses_Other (we may contact you)
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC044_VerifySpikeIncome_Other_NL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();

        }
        [TestCase(1250, "Other (we may contact you)", "android", TestName = "TC044_VerifySpikeIncome_Other_NL_1250"), Category("NL"), Retry(2)]
        [TestCase(3250, "Other (we may contact you)", "android", TestName = "TC044_VerifySpikeIncome_Other_NL_3250")]
        public void TC044_VerifyingSpikeIncome_Other_NL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_NL(loanamout, spikeresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the Spike question and the corresponding user responses_Other (we may contact you)
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC044_VerifySpikeIncome_Other_RL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(750, "Other (we may contact you)", "android", TestName = "TC044_VerifySpikeIncome_Other_RL_750"), Category("RL"), Retry(2)]
        [TestCase(2550, "Other (we may contact you)", "android", TestName = "TC044_VerifySpikeIncome_Other_RL_2550")]
        public void TC044_VerifyingSpikeIncome_Other_RL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_RL(loanamout, spikeresponse, strmobiledevice);
        }
    }
}
