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
    //Applying the loan to trigger the OOC question and the corresponding user responses_Other (we may contact you)
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC051_VerifyOOC_Other_NL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(1100, "Other (we may contact you)", "android", TestName = "TC051_VerifyOOC_Other_NL_1100"), Category("NL"), Retry(2)]
        [TestCase(2950, "Other (we may contact you)", "android", TestName = "TC051_VerifyOOC_Other_NL_2950")]
        public void TC051_VerifyingOOC_Other_NL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_NL(loanamout, oocresponse, strmobiledevice);
        }
    }

    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Other (we may contact you)
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC051_VerifyOOC_Other_RL : TestEngine
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(1100, "Other (we may contact you)", "android", TestName = "TC051_VerifyOOC_Other_RL_1100"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(1700, "Other (we may contact you)", "android", TestName = "TC051_VerifyOOC_Other_RL_1700")]
        public void TC051_VerifyingOOC_Other_RL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_RL(loanamout, oocresponse, strmobiledevice);
        }
    }
}
