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
    //Applying the loan to trigger the OOC question and the corresponding user responses_Quarterly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC046_VerifyOOC_QuaterlyBonus_NL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(900, "Quarterly bonus", "android", TestName = "TC046_VerifyOOC_QuaterlyBonus_NL_900"), Category("NL"), Retry(2)]
        [TestCase(2750, "Quarterly bonus", "android", TestName = "TC046_VerifyOOC_QuaterlyBonus_NL_2750")]
        public void TC046_VerifyingOOC_QuaterlyBonus_NL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_NL(loanamout, oocresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Quarterly bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC046_VerifyOOC_QuaterlyBonus_RL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(900, "Quarterly bonus", "android", TestName = "TC046_VerifyOOC_QuaterlyBonus_RL_900"), Category("RL"), Retry(2)]
        [TestCase(3950, "Quarterly bonus", "android", TestName = "TC046_VerifyOOC_QuaterlyBonus_RL_3950")]
        public void TC046_VerifyingOOC_QuaterlyBonus_RL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_RL(loanamout, oocresponse, strmobiledevice);
        }
    }

}
