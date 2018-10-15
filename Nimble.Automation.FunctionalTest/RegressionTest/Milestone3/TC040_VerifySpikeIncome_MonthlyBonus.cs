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
    //Applying the loan to trigger the Spike question and the corresponding user responses_Monthly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC040_VerifySpikeIncome_MonthlyBonus_NL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(1050, "Monthly bonus", "android", TestName = "TC040_VerifySpikeIncome_MonthlyBonus_NL_1050"), Category("NL"), Retry(2)]
        [TestCase(3550, "Monthly bonus", "android", TestName = "TC040_VerifySpikeIncome_MonthlyBonus_NL_3550")]
        public void TC040_VerifyingSpikeIncome_MonthlyBonus_NL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_NL(loanamout, spikeresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the Spike question and the corresponding user responses_Monthly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike")]
    class TC040_VerifySpikeIncome_MonthlyBonus_RL : TestEngine
    {
        TC038_VerifyingSpikeIncome_AnnualBonus _test = new TC038_VerifyingSpikeIncome_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(950, "Monthly bonus", "android", TestName = "TC040_VerifySpikeIncome_MonthlyBonus_RL_950"), Category("RL"), Retry(2)]
        [TestCase(2800, "Monthly bonus", "android", TestName = "TC040_VerifySpikeIncome_MonthlyBonus_RL_2800")]
        public void TC040_VerifyingSpikeIncome_MonthlyBonus_RL(int loanamout, string spikeresponse, string strmobiledevice)
        {
            _test.VerifySpikeQuestionIncome_RL(loanamout, spikeresponse, strmobiledevice);
        }
    }

}
