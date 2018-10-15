using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income Inconsistent")]
    class TC019_VerifyInconsistencyIncome 
    {
        TC017_VerifyInconsistencyIncome _test = new TC017_VerifyInconsistencyIncome();


        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(1000, "Yes", "Yes", "android", TestName = "TC019_VerifyingInconsistencyIncome_NL_SACC_1000"), Category("NL"), Retry(2)]
        [TestCase(2250, "Yes", "Yes", "ios", TestName = "TC019_VerifyingInconsistencyIncome_NL_MACC_2250")]
        public void TC019_VerifyingInconsistencyIncome_NL(int loanamount, string reason1, string reason2, string mobiledevice)
        {
            _test.VerifyInconsistencyIncome_NL(loanamount, reason1, reason2, mobiledevice);
        }
    }

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income Inconsistent")]
    class TC019_VerifyInconsistencyIncome_RL
    {
        TC017_VerifyInconsistencyIncome _test = new TC017_VerifyInconsistencyIncome();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }
        [TestCase(1000, "Yes", "Yes", "android", TestName = "TC019_VerifyingInconsistencyIncome_RL_SACC_1000"), Category("RL"), Retry(2)]
        [TestCase(2250, "Yes", "Yes", "ios", TestName = "TC019_VerifyingInconsistencyIncome_RL_MACC_2250")]
        public void TC019_VerifyingInconsistencyIncome_RL(int loanamount, string reason1, string reason2, string mobiledevice)
        {
            _test.VerifyInconsistencyIncome_RL(loanamount, reason1, reason2, mobiledevice);
        }
    }
}
