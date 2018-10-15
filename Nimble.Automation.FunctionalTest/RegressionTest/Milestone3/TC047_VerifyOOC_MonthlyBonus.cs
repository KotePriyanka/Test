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
    //Applying the loan to trigger the OOC question and the corresponding user responses_Monthly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC047_VerifyOOC_MonthlyBonus_NL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();

        }
        [TestCase(950, "Monthly bonus", "android", TestName = "TC047_VerifyOOC_MonthlyBonus_NL_950"), Category("NL"), Retry(2)]
        [TestCase(2800, "Monthly bonus", "android", TestName = "TC047_VerifyOOC_MonthlyBonus_NL_2800")]
        public void TC047_VerifyingOOC_MonthlyBonus_NL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_NL(loanamout, oocresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Monthly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC047_VerifyOOC_MonthlyBonus_RL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }


        [TestCase(950, "Monthly bonus", "android", TestName = "TC047_VerifyOOC_MonthlyBonus_RL_950"), Category("RL"), Retry(2)]
        [TestCase(2900, "Monthly bonus", "android", TestName = "TC047_VerifyOOC_MonthlyBonus_RL_2900")]
        public void TC047_VerifyingOOC_MonthlyBonus_RL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_RL(loanamout, oocresponse, strmobiledevice);
        }
    }

}
